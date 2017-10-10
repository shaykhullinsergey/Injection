using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  public interface IService
  {
    TResolve Resolve<TResolve>(params object[] args);
    TResolve Resolve<TResolve, TRegister>(params object[] args);
    IEnumerable<TResolve> ResolveAll<TResolve>();
    void ResolveFor<TResolve>(TResolve instance);
  }
}
