using Shaykhullin.Injection.App;
using System;

namespace Shaykhullin.Injection
{
  internal class AppReturnsSelector<TRegister, TResolve> 
    : IReturnsSelector<TRegister, TResolve>
  {
    private IServiceBuilder builder;
    private IDependencyContainer<AppDependency> container;
    private Func<TRegister> returns;

    public AppReturnsSelector(IServiceBuilder builder, 
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

    public IServiceBuilder Singleton()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TResolve)),
        new AppSingletonCreationalBehaviour<TRegister>(returns, null));

      return builder;
    }
  }
}