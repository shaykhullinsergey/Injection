using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shaykhullin.Injection
{
  internal static class Utils
  {
    public static TInstance CreateInstance<TInstance>(params object[] args)
    {
      return args?.Length == 0
        ? Activator.CreateInstance<TInstance>()
        : (TInstance)Activator.CreateInstance(typeof(TInstance), args);
    }

    public static TResolve Resolve<TResolve>(IDependencyContainer<AppDependency> container, 
      ICreationalBehaviour creator, params object[] args)
    {
      var instance = creator.Create<TResolve>(args);
      var (fields, props) = creator.Meta;

      foreach (var (field, inject) in fields.Where(p => p.Field.GetValue(instance) != null))
      {
        var propCreator = container.Get(new AppDependency(
          field.FieldType, inject.Resolve ?? field.FieldType));

        field.SetValue(instance, Resolve<object>(container, propCreator, inject.Args));
      }

      foreach (var (property, inject) in props.Where(p => p.Property.GetValue(instance) != null))
      {
        var propCreator = container.Get(new AppDependency(
          property.PropertyType, inject.Resolve ?? property.PropertyType));

        property.SetValue(instance, Resolve<object>(container, propCreator, inject.Args));
      }

      return instance;
    }

    public static void ResolveInstanceRecursive<TInstance>(IDependencyContainer<AppDependency> container, 
      TInstance instance)
    {
      var fields = instance.GetType()
        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(field => field.IsDefined(typeof(InjectAttribute)))
        .Select(field => (Field: field,
          Inject: field.GetCustomAttribute<InjectAttribute>()));

      var props = instance.GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Where(property => property.IsDefined(typeof(InjectAttribute)))
        .Select(property => (Property: property,
          Inject: property.GetCustomAttribute<InjectAttribute>()));

      foreach (var (field, inject) in fields.Where(p => p.Field.GetValue(instance) != null))
      {
        var propCreator = container.Get(new AppDependency(
          field.FieldType, inject.Resolve ?? field.FieldType));

        field.SetValue(instance, Resolve<object>(container, propCreator, inject.Args));
      }

      foreach (var (property, inject) in props.Where(p => p.Property.GetValue(instance) != null))
      {
        var propCreator = container.Get(new AppDependency(
          property.PropertyType, inject.Resolve ?? property.PropertyType));

        property.SetValue(instance, Resolve<object>(container, propCreator, inject.Args));
      }
    }
  }

}
