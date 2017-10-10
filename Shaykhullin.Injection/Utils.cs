﻿using System;
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

    public static TResolve ResolveInstance<TResolve>(IDependencyContainer<AppDependency> container, ICreationalBehaviour creator, params object[] args)
    {
      var instance = creator.Create<TResolve>(args);
      ResolveInstanceRecursive(container, instance);
      return instance;
    }

    public static void ResolveInstanceRecursive(IDependencyContainer<AppDependency> container, object instance)
    {
      var fieldsInfo = instance.GetType()
        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(field => field.IsDefined(typeof(InjectAttribute)))
        .Select(field => (field: field,
          inject: field.GetCustomAttribute<InjectAttribute>()));

      foreach (var info in fieldsInfo)
      {
        var (field, inject) = info;

        if (field.GetValue(instance) != null)
        {
          continue;
        }

        var creator = container.Get(new AppDependency(
          field.FieldType, inject.Resolve ?? field.FieldType));

        if (creator != null)
        {
          field.SetValue(instance, creator.Create<object>(inject.Args));
          ResolveInstanceRecursive(container, field.GetValue(instance));
        }
      }

      var propertiesInfo = instance.GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Where(property => property.IsDefined(typeof(InjectAttribute)))
        .Select(property => (property: property, 
          inject: property.GetCustomAttribute<InjectAttribute>()));

      foreach (var info in propertiesInfo)
      {
        var (property, inject) = info;

        if (property.GetValue(instance) != null)
        {
          continue;
        }

        var creator = container.Get(new AppDependency(
          property.PropertyType, inject.Resolve ?? property.PropertyType));

        if (creator != null)
        {
          property.SetValue(instance, creator.Create<object>(inject.Args));
          ResolveInstanceRecursive(container, property.GetValue(instance));
        }
      }
    }
  }

}