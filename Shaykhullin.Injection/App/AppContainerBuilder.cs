using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  public class AppContainerBuilder : IContainerBuilder
  {
    private readonly IDependencyContainer container;
    public IContainer Container => new AppContainer(container);
    
    public AppContainerBuilder()
    {
      container = new AppDependencyContainer();
    }

    public IContainerEntity<TRegister> Register<TRegister>()
    {
      return new AppContainerEntity<TRegister>(this, container);
    }
  }
}
