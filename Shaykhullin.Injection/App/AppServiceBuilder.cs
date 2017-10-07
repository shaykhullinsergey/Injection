using Shaykhullin.Injection.App;

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
        .Returns(s => Service)
        .AsSingleton();
    }

    public IServiceEntity<TRegister> Register<TRegister>()
    {
      return new AppServiceEntity<TRegister>(new AppEntityState<TRegister>
      {
        Builder = this,
        Container = container
      });
    }
  }
}
