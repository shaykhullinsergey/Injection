using System;
using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  internal class AppReturnsSelector<TRegister, TResolve> 
    : AppReturnsSelectorProvider<TRegister, TResolve>, 
      IReturnsSelector<TRegister, TResolve>
  {
    public AppReturnsSelector(IServiceBuilder builder, IDependencyContainer<AppDependency> container, 
      Func<TRegister> returns) : base(builder, container, returns) { }

    public IServiceBuilder Singleton()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TResolve)),
        new AppSingletonCreationalBehaviour<TRegister>(returns, null));

      return builder;
    }
  }
}