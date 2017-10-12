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

      foreach (var info in fields)
      {
        var (field, inject) = info;

        if (field.GetValue(instance) != null)
        {
          continue;
        }

        var fieldCreator = container.Get(new AppDependency(
          field.FieldType, inject.Resolve ?? field.FieldType));

        field.SetValue(instance, Resolve<object>(container, fieldCreator, inject.Args));
      }

      foreach (var info in props)
      {
        var (prop, inject) = info;

        if (prop.GetValue(instance) != null)
        {
          continue;
        }

        var propCreator = container.Get(new AppDependency(
          prop.PropertyType, inject.Resolve ?? prop.PropertyType));

        prop.SetValue(instance, Resolve<object>(container, propCreator, inject.Args));
      }

      return instance;
    }

    public static void ResolveInstanceRecursive<TInstance>(IDependencyContainer<AppDependency> container, 
      TInstance instance)
    {
      var fieldsInfo = instance.GetType()
        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(field => field.IsDefined(typeof(InjectAttribute)))
        .Select(field => (field: field,
          inject: field.GetCustomAttribute<InjectAttribute>()));

      var propertiesInfo = instance.GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Where(property => property.IsDefined(typeof(InjectAttribute)))
        .Select(property => (property: property,
          inject: property.GetCustomAttribute<InjectAttribute>()));

      foreach (var info in fieldsInfo)
      {
        var (field, inject) = info;

        if (field.GetValue(instance) != null)
        {
          continue;
        }

        var fieldCreator = container.Get(new AppDependency(
          field.FieldType, inject.Resolve ?? field.FieldType));

        field.SetValue(instance, Resolve<object>(container, fieldCreator, inject.Args));
      }

      foreach (var info in propertiesInfo)
      {
        var (property, inject) = info;

        if (property.GetValue(instance) != null)
        {
          continue;
        }

        var propCreator = container.Get(new AppDependency(
          property.PropertyType, inject.Resolve ?? property.PropertyType));

        property.SetValue(instance, Resolve<object>(container, propCreator, inject.Args));
      }
    }
  }

}
