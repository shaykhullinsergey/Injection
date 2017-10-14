using System;

namespace Shaykhullin.Injection.App
{
  internal class AppReturnsSelectorProvider<TRegister, TResolve> : IContainerBuilder
  {
    protected readonly IContainerBuilder builder;
    protected readonly IDependencyContainer container;
    protected readonly Func<TRegister> returns;

    protected AppReturnsSelectorProvider(IContainerBuilder builder,
      IDependencyContainer container, Func<TRegister> returns)
    {
      this.builder = builder;
      this.container = container;
      this.returns = returns;
    }

    public IContainer Container
    {
      get
      {
        container.Register<TRegister, TResolve>(new AppTransientCreationalBehaviour<TRegister>(returns));
        return builder.Container;
      }
    }

    public IContainerEntity<TNext> Register<TNext>()
    {
      container.Register<TRegister, TResolve>(new AppTransientCreationalBehaviour<TRegister>(returns));
      return new AppContainerEntity<TNext>(builder, container);
    }
  }
}
