using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal class AppDependencyContainer
    : IDependencyContainer
  {
    private readonly Dictionary<IDependency, ICreationalBehaviour> dependencies =
      new Dictionary<IDependency, ICreationalBehaviour>();


    /// <summary>
    /// Searches for dependency or throws exception
    /// </summary>
    /// <param name="dependency"></param>
    /// <exception cref="NotSupportedException"></exception>
    /// <returns></returns>
    public ICreationalBehaviour Get(IDependency dependency)
    {
      dependencies.TryGetValue(dependency, out var creator);

      if(creator == null)
      {
        throw new NotSupportedException($"Type {dependency.Resolve} is not registered in container");
      }

      return creator;
    }

    /// <summary>
    /// Gets all dependencies registered as TResolve
    /// </summary>
    /// <typeparam name="TResolve"></typeparam>
    /// <returns></returns>
    public IEnumerable<ICreationalBehaviour> GetAll<TResolve>()
    {
      foreach (var dependency in dependencies)
      {
        if (dependency.Key.Resolve == typeof(TResolve))
          yield return dependency.Value;
      }
    }


    /// <summary>
    /// Registers new dependency in container
    /// </summary>
    /// <param name="dependency"></param>
    /// <param name="behaviour"></param>
    public void Register(IDependency dependency, ICreationalBehaviour behaviour)
    {
      dependencies.Add(dependency, behaviour);
    }
  }
}