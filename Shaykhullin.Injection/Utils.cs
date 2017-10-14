using System;
using System.Collections.Generic;
using System.Linq;

namespace Shaykhullin.Injection
{
  internal static class Utils
  {
    public static TInstance CreateInstance<TInstance>(params object[] args)
    {
      return args?.Length == 0
        ? Activator.CreateInstance<TInstance>()
        : (TInstance) Activator.CreateInstance(typeof(TInstance), args);
    }

    public static TResolve Resolve<TResolve>(IDependencyContainer container,
      ICreationalBehaviour creator, params object[] args)
    {
      var (instance, (fields, props)) = (creator.Create<TResolve>(args), creator.MetaInfo);

      foreach (var (field, inject) in fields.Where(p => p.Field.GetValue(instance) == null))
      {
        field.SetValue(instance, Resolve<object>(container,
          container.Get(field.FieldType, inject.Register ?? field.FieldType),
          inject.Args));
      }

      foreach (var (property, inject) in props.Where(p => p.Property.GetValue(instance) == null))
      {
        property.SetValue(instance, Resolve<object>(container,
          container.Get(property.PropertyType, inject.Register ?? property.PropertyType),
          inject.Args));
      }

      return instance;
    }

    public static IEnumerable<TResolve> ResolveAll<TResolve>(IDependencyContainer container)
    {
      return container.GetAll<TResolve>()
        .Select(creator => Resolve<TResolve>(container, creator));
    }
  }
}