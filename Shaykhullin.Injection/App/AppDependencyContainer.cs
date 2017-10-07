using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection.App
{
  internal class AppDependencyContainer : IDependencyContainer
  {
    private Dictionary<IDependency, ICreationalBehaviour> dependencies =
      new Dictionary<IDependency, ICreationalBehaviour>();

    public ICreationalBehaviour Get(Type type, string name = null)
    {
      dependencies.TryGetValue(new AppDependency(type, name), out var creator);
      return creator;
    }

    public IEnumerable<ICreationalBehaviour> GetAll<TResolve>()
    {
      foreach (var dependency in dependencies)
      {
        if (dependency.Key.Type == typeof(TResolve))
          yield return dependency.Value;
      }
    }

    public void Register(IDependency dependency, ICreationalBehaviour behaviour)
    {
      dependencies.Add(dependency, behaviour);
    }
  }
}
