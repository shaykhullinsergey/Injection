using Shaykhullin.Injection.App;
using System;

namespace Shaykhullin.Injection
{
  internal class AppReturnsEntity<TRegister> : IReturnsEntity<TRegister>
  {
    private IServiceBuilder builder;
    private IDependencyContainer<AppDependency> container;
    private Func<TRegister> returns;

    public AppReturnsEntity(IServiceBuilder builder, IDependencyContainer<AppDependency> container, Func<TRegister> returns)
    {
      this.builder = builder;
      this.container = container;
      this.returns = returns;
    }

    public IService Service
    {
      get
      {
        container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
          new AppTransientCreationalBehaviour<TRegister>(returns));

        return builder.Service;
      }
    }

    public IReturnsSelector<TRegister, TResolve> As<TResolve>()
    {
      return new AppReturnsSelector<TRegister, TResolve>(builder, container, returns);
    }

    public IServiceEntity<TNext> Register<TNext>()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
        new AppTransientCreationalBehaviour<TRegister>(returns));

      return new AppServiceEntity<TNext>(builder, container);
    }

    public IServiceBuilder Singleton()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
        new AppSingletonCreationalBehaviour<TRegister>(returns, null));

      return builder;
    }
  }
}