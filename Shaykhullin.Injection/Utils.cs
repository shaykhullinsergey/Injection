using System;
using System.Linq;

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

    public static TResolve Resolve<TResolve>(IDependencyContainer container,
      ICreationalBehaviour creator, params object[] args)
    {
      var instance = creator.Create<TResolve>(args);

      var (fields, props) = creator.Meta;

      foreach (var (field, inject) in fields.Where(p => p.Field.GetValue(instance) == null))
      {
        var propCreator = container.Get(field.FieldType, inject.Resolve ?? field.FieldType);
        field.SetValue(instance, Resolve<object>(container, propCreator, inject.Args));
      }

      foreach (var (property, inject) in props.Where(p => p.Property.GetValue(instance) == null))
      {
        var propCreator = container.Get(property.PropertyType, inject.Resolve ?? property.PropertyType);
        property.SetValue(instance, Resolve<object>(container, propCreator, inject.Args));
      }

      return instance;
    }
  }
}
