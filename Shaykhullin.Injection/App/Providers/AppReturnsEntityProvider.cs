using System;

namespace Shaykhullin.Injection.App
{
  internal class AppReturnsEntityProvider<TRegister> : IContainerBuilder
  {
    protected readonly IContainerBuilder builder;
    protected readonly IDependencyContainer container;
    protected readonly Func<TRegister> returns;

    protected AppReturnsEntityProvider(IContainerBuilder builder, 
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
        container.Register<TRegister, TRegister>(new AppTransientCreationalBehaviour<TRegister>(returns));
        return builder.Container;
      }
    }

    public IContainerEntity<TNext> Register<TNext>()
    {
      container.Register<TRegister, TRegister>(new AppTransientCreationalBehaviour<TRegister>(returns));
      return new AppContainerEntity<TNext>(builder, container);
    }
  }
}
