using System;
using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection.App
{
  internal class AppReturnsEntity<TRegister> 
    : AppReturnsEntityProvider<TRegister>, 
      IReturnsEntity
  {
    public AppReturnsEntity(IContainerBuilder builder, IDependencyContainer container, 
      Func<TRegister> returns) : base(builder, container, returns) { }

    public IReturnsSelector As<TResolve>()
    {
      return new AppReturnsSelector<TRegister, TResolve>(builder, container, returns);
    }

    public IContainerBuilder Singleton()
    {
      container.Register<TRegister, TRegister>(new AppSingletonCreationalBehaviour<TRegister>(returns, null));
      return builder;
    }
  }
}