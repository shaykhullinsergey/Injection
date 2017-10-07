using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shaykhullin.Injection.App
{
  internal class AppService : IService
  {
    private IDependencyContainer container;

    public AppService(IDependencyContainer container)
    {
      this.container = container ?? throw new ArgumentNullException(nameof(container));
    }

    public TResolve Resolve<TResolve>(params object[] args)
    {
      return Resolve<TResolve>(null, args);
    }

    public TResolve Resolve<TResolve>(string named, params object[] args)
    {
      var creator = container.Get(typeof(TResolve), named)
        ?? throw new NotSupportedException($"Type {typeof(TResolve).Name} is not registered in container");

      return ResolveInstance<TResolve>(creator, args);
    }

    public IEnumerable<TResolve> ResolveAll<TResolve>(params object[] args)
    {
      foreach (var creator in container.GetAll<TResolve>())
      {
        yield return ResolveInstance<TResolve>(creator, args);
      }
    }

    private TResolve ResolveInstance<TResolve>(ICreationalBehaviour creator, params object[] args)
    {
      var instance = creator.Create<TResolve>(args);
      ResolveInstanceRecursive(instance);
      return instance;
    }

    private void ResolveInstanceRecursive(object instance)
    {
      var fieldsInfo = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(field => field.IsDefined(typeof(InjectAttribute)))
        .Select(field => (field, field.GetCustomAttribute<InjectAttribute>()));

      foreach (var info in fieldsInfo)
      {
        var (field, inject) = info;

        var creator = container.Get(field.FieldType, inject.Name);

        if (creator != null)
        {
          field.SetValue(instance, creator.Create<object>(inject.Args));
          ResolveInstanceRecursive(field.GetValue(instance));
        }
      }

      var propertiesInfo = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Where(p => p.IsDefined(typeof(InjectAttribute)))
        .Select(p => (p, p.GetCustomAttribute<InjectAttribute>()));

      foreach (var info in propertiesInfo)
      {
        var (property, inject) = info;

        var creator = container.Get(property.PropertyType, inject.Name);

        if (creator != null)
        {
          property.SetValue(instance, creator.Create<object>(inject.Args));
          ResolveInstanceRecursive(property.GetValue(instance));
        }
      }
    }
  }
}
