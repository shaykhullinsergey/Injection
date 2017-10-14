using System;
using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  internal class AppContainerEntity<TRegister>
    : AppContainerEntityProvider<TRegister>, 
      IContainerEntity<TRegister>
  {
    public AppContainerEntity(IContainerBuilder builder, IDependencyContainer container)
      : base(builder, container) { }

    public IContainerSelector As<TResolve>()
    {
      return new AppContainerSelector<TRegister, TResolve>(builder, container);
    }

    public IReturnsEntity Returns(Func<IContainer, TRegister> returns)
    {
      return new AppReturnsEntity<TRegister>(builder, container, () => returns(builder.Container));
    }

    public IContainerBuilder Singleton(params object[] args)
    {
      container.Register<TRegister, TRegister>(new AppSingletonCreationalBehaviour<TRegister>(null, args));
      return builder;
    }
  }
}