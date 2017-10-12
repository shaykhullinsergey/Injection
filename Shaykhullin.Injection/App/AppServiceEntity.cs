using Shaykhullin.Injection.App;
using System;

namespace Shaykhullin.Injection
{
  internal class AppServiceEntity<TRegister>
    : AppServiceEntityProvider<TRegister>, 
      IServiceEntity<TRegister>
  {
    public AppServiceEntity(IServiceBuilder builder, IDependencyContainer container)
      : base(builder, container) { }

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
      container.Register(new AppDependency<TRegister, TRegister>(),
        new AppSingletonCreationalBehaviour<TRegister>(null, args));

      return builder;
    }
  }
}