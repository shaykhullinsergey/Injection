using System;

namespace Shaykhullin.Injection.App
{
  internal class AppTransientCreationalBehaviour<TRegister> : ICreationalBehaviour
  {
    private Func<TRegister> returns;

    public AppTransientCreationalBehaviour(Func<TRegister> returns)
    {
      this.returns = returns;
    }

    public TResolve Create<TResolve>(params object[] args)
    {
      return (TResolve)(returns != null && args?.Length == 0
        ? returns()
        : (object)Utils.CreateInstance<TRegister>(args));
    }
  }
}