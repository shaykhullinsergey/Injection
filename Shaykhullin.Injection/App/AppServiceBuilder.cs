using Shaykhullin.Injection.App;
using System;

namespace Shaykhullin.Injection
{
  public class AppServiceBuilder : IServiceBuilder
  {
    private IDependencyContainer container;
    public IService Service { get; }

    public AppServiceBuilder()
    {
      container = new AppDependencyContainer();
      Service = new AppService(container);

      Register<IService>()
        .Returns(Service)
        .AsSingleton();
    }

    public IServiceEntity<TRegister> Register<TRegister>() =>
      new AppServiceEntity<TRegister>(new AppEntityState<TRegister>
      {
        Builder = this,
        Container = container
      });
  }
}
