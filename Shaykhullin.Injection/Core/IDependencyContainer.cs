using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal interface IDependencyContainer
  {
    void Register(IDependency dependency, ICreationalBehaviour behaviour);
    ICreationalBehaviour Get(Type type, string name = null);
    IEnumerable<ICreationalBehaviour> GetAll<TResolve>();
  }
}
