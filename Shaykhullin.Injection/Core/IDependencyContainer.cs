using System;
using System.Collections.Generic;
using System.Reflection;

namespace Shaykhullin.Injection
{
  internal interface IDependencyContainer
  {
    ICreationalBehaviour Get<TResolve>();
    ICreationalBehaviour Get<TRegister, TResolve>();
    ICreationalBehaviour Get(Type property, Type resolve);
    
    IEnumerable<ICreationalBehaviour> GetAll<TResolve>();
    IEnumerable<ICreationalBehaviour> GetAll(Type resolve);
    
    void Register<TRegister, TResolve>(ICreationalBehaviour behaviour);
  }
}
