using System;

namespace Shaykhullin.Injection.App
{
  internal class AppReturnsEntityProvider<TRegister> : IServiceBuilder
  {
    protected readonly IServiceBuilder builder;
    protected readonly IDependencyContainer container;
    protected readonly Func<TRegister> returns;

    public AppReturnsEntityProvider(IServiceBuilder builder, 
      IDependencyContainer container, Func<TRegister> returns)
    {
      this.builder = builder;
      this.container = container;
      this.returns = returns;
    }

    public IService Service
    {
      get
      {
        container.Register<TRegister, TRegister>(new AppTransientCreationalBehaviour<TRegister>(returns));
        return builder.Service;
      }
    }

    public IServiceEntity<TNext> Register<TNext>()
    {
      container.Register<TRegister, TRegister>(new AppTransientCreationalBehaviour<TRegister>(returns));
      return new AppServiceEntity<TNext>(builder, container);
    }
  }
}
