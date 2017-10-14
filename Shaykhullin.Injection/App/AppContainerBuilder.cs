using Shaykhullin.Injection.App;

namespace Shaykhullin.Injection
{
  public class AppContainerBuilder : IContainerBuilder
  {
    private readonly IDependencyContainer dependencyContainer;
    public IContainer Container { get; }
    
    public AppContainerBuilder()
    {
      dependencyContainer = new AppDependencyContainer();
      Container = new AppContainer(dependencyContainer);
    }

    public IContainerEntity<TRegister> Register<TRegister>()
    {
      return new AppContainerEntity<TRegister>(this, dependencyContainer);
    }
  }
}
