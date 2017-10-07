using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  public interface IService
  {
    TResolve Resolve<TResolve>(params object[] args);
    TResolve Resolve<TResolve>(string named, params object[] args);
    IEnumerable<TResolve> ResolveAll<TResolve>(params object[] args);
  }
}
