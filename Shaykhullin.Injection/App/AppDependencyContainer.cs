using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  internal class AppDependencyContainer : IDependencyContainer
  {
    private readonly Dictionary<(Type register, Type resolve), ICreationalBehaviour> dependencies =
      new Dictionary<(Type, Type), ICreationalBehaviour>();

    public ICreationalBehaviour Get<TResolve>()
    {
      var creator = dependencies.FirstOrDefault(pair => pair.Key.resolve == typeof(TResolve)).Value;

      if(creator == null)
      {
        throw new InvalidOperationException($"Dependency of type {typeof(TResolve).Name} was not registered");
      }

      return creator;
    }

    public ICreationalBehaviour Get<TRegister, TResolve>()
    {
      return Get(typeof(TRegister), typeof(TResolve));
    }

    public IEnumerable<ICreationalBehaviour> GetAll<TResolve>()
    {
      return GetAll(typeof(TResolve));
    }

    public void Register<TRegister, TResolve>(ICreationalBehaviour creator)
    {
      dependencies.Add((typeof(TRegister), typeof(TResolve)), creator);
    }

    public ICreationalBehaviour Get(Type register, Type resolve)
    {
      return dependencies.TryGetValue((register, resolve), out var creator)
        ? creator
        : throw new InvalidOperationException($"Dependency of type {resolve.Name} was not registered");
      
      
//        : type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
//          ? new AppIEnumerableCreationalBehaviour(property, this)
//          : throw new InvalidOperationException($"Dependency of type {resolve.Name} was not registered"); 
    }

    public IEnumerable<ICreationalBehaviour> GetAll(Type resolve)
    {
      return dependencies.Where(pair => pair.Key.resolve == resolve)
        .Select(pair => pair.Value);
    }
  }
}