using System;

namespace Shaykhullin.Injection.App
{
  internal class AppReturnsSelector<TRegister, TResolve> 
    : AppReturnsSelectorProvider<TRegister, TResolve>, 
      IReturnsSelector
  {
    public AppReturnsSelector(IContainerBuilder builder, IDependencyContainer container, 
      Func<TRegister> returns) : base(builder, container, returns) { }

    public IContainerBuilder Singleton()
    {
      container.Register<TRegister, TResolve>(new AppSingletonCreationalBehaviour<TRegister>(returns, null));
      return builder;
    }
  }
}