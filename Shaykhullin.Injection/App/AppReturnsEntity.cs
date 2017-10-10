using System;
using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  internal class AppReturnsEntity<TRegister> 
    : AppReturnsEntityProvider<TRegister>, 
      IReturnsEntity<TRegister>
  {
    public AppReturnsEntity(IServiceBuilder builder, IDependencyContainer<AppDependency> container, 
      Func<TRegister> returns) : base(builder, container, returns) { }

    public IReturnsSelector<TRegister, TResolve> As<TResolve>()
    {
      return new AppReturnsSelector<TRegister, TResolve>(builder, container, returns);
    }

    public IServiceBuilder Singleton()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
        new AppSingletonCreationalBehaviour<TRegister>(returns, null));

      return builder;
    }
  }
}