using System;

namespace Shaykhullin.Injection.App
{
  internal class AppSingletonCreationalBehaviour<TRegister> : ICreationalBehaviour
  {
    private object instance;

    public AppSingletonCreationalBehaviour(TRegister returns, params object[] args)
    {
      instance = returns != null 
        ? returns 
        : Activator.CreateInstance(typeof(TRegister), args);
    }

    public TResolve Create<TResolve>(params object[] args)
    {
      return (TResolve)instance;
    }
  }
}
