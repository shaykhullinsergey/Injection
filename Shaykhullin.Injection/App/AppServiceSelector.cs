using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  internal class AppServiceSelector<TRegister, TResolve> 
    : IServiceSelector<TRegister, TResolve>
  {
    private IServiceBuilder builder;
    private IDependencyContainer<AppDependency> container;

    public AppServiceSelector(IServiceBuilder builder, IDependencyContainer<AppDependency> container)
    {
      this.builder = builder;
      this.container = container;
    }

    public IService Service
    {
      get
      {
        container.Register(new AppDependency(typeof(TRegister), typeof(TResolve)),
          new AppTransientCreationalBehaviour<TRegister>(null));

        return builder.Service;
      }
    }

    public IServiceEntity<TNext> Register<TNext>()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TResolve)),
        new AppTransientCreationalBehaviour<TRegister>(null));

      return new AppServiceEntity<TNext>(builder, container);
    }

    public IServiceBuilder Singleton(params object[] args)
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
        new AppSingletonCreationalBehaviour<TRegister>(null, args));

      return builder;
    }
  }
}