using System;
using System.Collections.Generic;
using System.Linq;

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

    public ICreationalBehaviour Get(Type register, Type resolve)
    {
      dependencies.TryGetValue((register, resolve), out var creator);

      if (creator == null)
      {
        throw new InvalidOperationException($"Dependency of type {resolve.Name} was not registered");
      }

      return creator;
    }

    public IEnumerable<ICreationalBehaviour> GetAll<TResolve>()
    {
      return dependencies.Where(pair => pair.Key.resolve == typeof(TResolve))
        .Select(pair => pair.Value);
    }

    public void Register<TRegister, TResolve>(ICreationalBehaviour creator)
    {
      dependencies.Add((typeof(TRegister), typeof(TResolve)), creator);
    }
  }
}