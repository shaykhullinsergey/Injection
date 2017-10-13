using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection.App
{
  internal class AppService : IService
  {
    private IDependencyContainer container;

    public AppService(IDependencyContainer container)
    {
      this.container = container;
    }

    public TResolve Resolve<TResolve>(params object[] args)
    {
      var creator = container.Get<TResolve>();
      return Utils.Resolve<TResolve>(container, creator, args);
    }

    public TResolve Resolve<TResolve, TRegister>(params object[] args)
    {
      var creator = container.Get<TRegister, TResolve>();
      return Utils.Resolve<TResolve>(container, creator, args);
    }

    public IEnumerable<TResolve> ResolveAll<TResolve>()
    {
      foreach (var creator in container.GetAll<TResolve>())
      {
        yield return Utils.Resolve<TResolve>(container, creator);
      }
    }
  }
}