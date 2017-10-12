using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal interface IDependencyContainer
  {
    void Register(IDependency dependency, ICreationalBehaviour behaviour);
    ICreationalBehaviour Get(IDependency dependency);
    IEnumerable<ICreationalBehaviour> GetAll<TResolve>();
  }
}
