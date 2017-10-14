using System;
using System.Collections.Generic;
using System.Linq;

namespace Shaykhullin.Injection.App
{
  internal class AppContainer : IContainer
  {
    private readonly IDependencyContainer container;

    public AppContainer(IDependencyContainer container)
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
      return Utils.ResolveAll<TResolve>(container);
    }
  }
}