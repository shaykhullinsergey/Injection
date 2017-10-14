namespace Shaykhullin.Injection.App
{
  internal class AppContainerSelectorProvider<TRegister, TResolve> : IContainerBuilder
  {
    protected readonly IContainerBuilder builder;
    protected readonly IDependencyContainer container;

    protected AppContainerSelectorProvider(IContainerBuilder builder, 
      IDependencyContainer container)
    {
      this.builder = builder;
      this.container = container;
    }

    public IContainer Container
    {
      get
      {
        container.Register<TRegister, TResolve>(
          new AppTransientCreationalBehaviour<TRegister>(null));
        
        return builder.Container;
      }
    }

    public IContainerEntity<TNext> Register<TNext>()
    {
      container.Register<TRegister, TResolve>(
        new AppTransientCreationalBehaviour<TRegister>(null));
      
      return new AppContainerEntity<TNext>(builder, container);
    }
  }
}
