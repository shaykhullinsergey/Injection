using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  public interface IContainer
  {
    TResolve Resolve<TResolve>(params object[] args);
    TResolve Resolve<TResolve, TRegister>(params object[] args);
    IEnumerable<TResolve> ResolveAll<TResolve>();
  }
}
