using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  internal class AppServiceSelector<TRegister, TResolve> 
    : AppServiceSelectorProvider<TRegister, TResolve>,
      IServiceSelector<TRegister, TResolve>
  {
    public AppServiceSelector(IServiceBuilder builder, IDependencyContainer container) 
      : base(builder, container) { }

    public IServiceBuilder Singleton(params object[] args)
    {
      container.Register<TRegister, TRegister>(new AppSingletonCreationalBehaviour<TRegister>(null, args));
      return builder;
    }
  }
}