using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal interface IDependencyContainer
  {
    ICreationalBehaviour Get<TResolve>();
    ICreationalBehaviour Get<TRegister, TResolve>();
    ICreationalBehaviour Get(Type register, Type resolve);
    IEnumerable<ICreationalBehaviour> GetAll<TResolve>();
    void Register<TRegister, TResolve>(ICreationalBehaviour behaviour);
  }
}
