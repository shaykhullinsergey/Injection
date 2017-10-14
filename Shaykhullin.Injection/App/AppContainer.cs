using System.Collections.Generic;

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
      return Utils.Resolve<TResolve>(container, 
        container.Get<TResolve>(),
        args);
    }

    public TResolve Resolve<TResolve, TRegister>(params object[] args)
    {
      return Utils.Resolve<TResolve>(container, 
        container.Get<TRegister, TResolve>(), 
        args);
    }

    public IEnumerable<TResolve> ResolveAll<TResolve>()
    {
      return Utils.ResolveAll<TResolve>(container);
    }
  }
}