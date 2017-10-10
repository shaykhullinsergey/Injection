using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal interface IDependencyContainer<TDependencyKey>
  {
    void Register(TDependencyKey dependency, ICreationalBehaviour behaviour);
    ICreationalBehaviour Get(TDependencyKey dependency);
    IEnumerable<ICreationalBehaviour> GetAll<TResolve>();
  }
}
