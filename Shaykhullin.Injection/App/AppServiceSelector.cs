using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  internal class AppServiceSelector<TRegister, TResolve> 
    : AppServiceSelectorProvider<TRegister, TResolve>,
      IServiceSelector<TRegister, TResolve>
  {
    public AppServiceSelector(IServiceBuilder builder, IDependencyContainer<AppDependency> container) 
      : base(builder, container) { }

    public IServiceBuilder Singleton(params object[] args)
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
        new AppSingletonCreationalBehaviour<TRegister>(null, args));

      return builder;
    }
  }
}