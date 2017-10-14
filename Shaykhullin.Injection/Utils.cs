using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

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
      var (fields, props) = creator.MetaInfo;

      foreach (var (field, inject) in fields.Where(p => p.Field.GetValue(instance) == null))
      {
        var fieldType = field.FieldType;
        if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
          field.SetValue(instance, ResolveList(fieldType.GetGenericArguments()[0]));
        }
        else
        {
          field.SetValue(instance, Resolve<object>(container, 
            container.Get(fieldType, inject.Resolve ?? fieldType), 
            inject.Args));
        }
      }

      foreach (var (property, inject) in props.Where(p => p.Property.GetValue(instance) == null))
      {
        var propType = property.PropertyType;
        if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
          property.SetValue(instance, ResolveList(propType.GetGenericArguments()[0]));
        }
        else
        {
          property.SetValue(instance, Resolve<object>(container, 
            container.Get(propType, inject.Resolve ?? propType), 
            inject.Args));
        }
      }

      return instance;
      
      IList ResolveList(Type genericArgument)
      {
        var list = (IList)Activator.CreateInstance(
          typeof(List<>).MakeGenericType(genericArgument));

        foreach (var c in container.GetAll(genericArgument))
        {
          list.Add(Resolve<object>(container, c));
        }

        return list;
      }
    }

    public static IEnumerable<TResolve> ResolveAll<TResolve>(IDependencyContainer container)
    {
      return container.GetAll<TResolve>()
        .Select(creator => Resolve<TResolve>(container, creator));
    }
  }
}
