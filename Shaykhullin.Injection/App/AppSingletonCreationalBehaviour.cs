using System;

namespace Shaykhullin.Injection.App
{
  internal class AppSingletonCreationalBehaviour<TRegister> : ICreationalBehaviour
  {
    private IService service;
    private Func<IService, TRegister> returns;
    private object instance;

    public AppSingletonCreationalBehaviour(AppEntityState<TRegister> state, params object[] args)
    {
      (service, returns) = state;
    }

    public TResolve Create<TResolve>(params object[] args)
    {
      if(instance == null)
      {
        instance = returns != null
          ? returns(service)
          : Activator.CreateInstance(typeof(TRegister), args);

        returns = null;
        service = null;
      }

      return (TResolve)instance;
    }
  }
}
