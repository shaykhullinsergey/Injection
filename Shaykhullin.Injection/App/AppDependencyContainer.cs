using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal class AppDependencyContainer
    : IDependencyContainer<AppDependency>
  {
    private readonly Dictionary<AppDependency, ICreationalBehaviour> dependencies =
      new Dictionary<AppDependency, ICreationalBehaviour>();

    public ICreationalBehaviour Get(AppDependency dependency)
    {
      dependencies.TryGetValue(dependency, out var creator);

      if(creator == null)
      {
        throw new NotSupportedException($"Type {dependency.Resolve} is not registered in container");
      }

      return creator;
    }

    public IEnumerable<ICreationalBehaviour> GetAll<TResolve>()
    {
      foreach (var dependency in dependencies)
      {
        if (dependency.Key.Resolve == typeof(TResolve))
          yield return dependency.Value;
      }
    }

    public void Register(AppDependency dependency, ICreationalBehaviour behaviour)
    {
      dependencies.Add(dependency, behaviour);
    }
  }
}