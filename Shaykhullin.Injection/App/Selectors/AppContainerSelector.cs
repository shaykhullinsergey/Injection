using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  internal class AppContainerSelector<TRegister, TResolve> 
    : AppContainerSelectorProvider<TRegister, TResolve>,
      IContainerSelector
  {
    public AppContainerSelector(IContainerBuilder builder, IDependencyContainer container) 
      : base(builder, container) { }

    public IContainerBuilder Singleton(params object[] args)
    {
      container.Register<TRegister, TRegister>(
        new AppSingletonCreationalBehaviour<TRegister>(null, args));
      
      return builder;
    }
  }
}