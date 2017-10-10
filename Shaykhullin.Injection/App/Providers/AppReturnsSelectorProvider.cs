using System;

namespace Shaykhullin.Injection.App
{
  internal class AppReturnsSelectorProvider<TRegister, TResolve> : IServiceBuilder
  {
    protected readonly IServiceBuilder builder;
    protected readonly IDependencyContainer<AppDependency> container;
    protected readonly Func<TRegister> returns;

    public AppReturnsSelectorProvider(IServiceBuilder builder,
      IDependencyContainer<AppDependency> container, Func<TRegister> returns)
    {
      this.builder = builder;
      this.container = container;
      this.returns = returns;
    }

    public IService Service
    {
      get
      {
        container.Register(new AppDependency(typeof(TRegister), typeof(TResolve)),
          new AppTransientCreationalBehaviour<TRegister>(returns));
        return builder.Service;
      }
    }

    public IServiceEntity<TNext> Register<TNext>()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TResolve)),
        new AppTransientCreationalBehaviour<TRegister>(returns));

      return new AppServiceEntity<TNext>(builder, container);
    }
  }
}
