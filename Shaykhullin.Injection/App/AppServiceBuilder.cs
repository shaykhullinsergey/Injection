using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  public class AppServiceBuilder : IServiceBuilder
  {
    private readonly IDependencyContainer container;

    public AppServiceBuilder()
    {
      container = new AppDependencyContainer();
      Service = new AppService(container);

      Register<IService>()
        .Returns(s => Service)
        .Singleton();
    }

    public IService Service { get; }

    public IServiceEntity<TRegister> Register<TRegister>()
    {
      return new AppServiceEntity<TRegister>(this, container);
    }
  }
}
