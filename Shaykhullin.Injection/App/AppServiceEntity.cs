using Shaykhullin.Injection.App;
using System;

namespace Shaykhullin.Injection
{
  internal class AppServiceEntity<TRegister>
    : IServiceEntity<TRegister>
  {
    private IServiceBuilder builder;
    private IDependencyContainer<AppDependency> container;

    public AppServiceEntity(IServiceBuilder builder, IDependencyContainer<AppDependency> container)
    {
      this.builder = builder;
      this.container = container;
    }

    public IService Service
    {
      get
      {
        container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
          new AppTransientCreationalBehaviour<TRegister>(null));

        return builder.Service;
      }
    }

    public IServiceEntity<TNext> Register<TNext>()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
        new AppTransientCreationalBehaviour<TRegister>(null));

      return new AppServiceEntity<TNext>(builder, container);
    }

    public IServiceSelector<TRegister, TResolve> As<TResolve>()
    {
      return new AppServiceSelector<TRegister, TResolve>(builder, container);
    }

    public IReturnsEntity<TRegister> Returns(Func<IService, TRegister> returns)
    {
      return new AppReturnsEntity<TRegister>(builder, container, () => returns(builder.Service));
    }

    public IServiceBuilder Singleton(params object[] args)
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
        new AppSingletonCreationalBehaviour<TRegister>(null, args));

      return builder;
    }
  }
}