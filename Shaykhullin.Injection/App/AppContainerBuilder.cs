using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  public class AppContainerBuilder : IContainerBuilder
  {
    private readonly IDependencyContainer container;

    public AppContainerBuilder()
    {
      container = new AppDependencyContainer();
      Container = new AppContainer(container);

      Register<IContainer>()
        .Returns(s => Container)
        .Singleton();
    }

    public IContainer Container { get; }

    public IContainerEntity<TRegister> Register<TRegister>()
    {
      return new AppContainerEntity<TRegister>(this, container);
    }
  }
}
