namespace Shaykhullin.Injection.App
{
  internal class AppServiceSelectorProvider<TRegister, TResolve> : IServiceBuilder
  {
    protected readonly IServiceBuilder builder;
    protected readonly IDependencyContainer container;

    public AppServiceSelectorProvider(IServiceBuilder builder, 
      IDependencyContainer container)
    {
      this.builder = builder;
      this.container = container;
    }

    public IService Service
    {
      get
      {
        container.Register<TRegister, TResolve>(new AppTransientCreationalBehaviour<TRegister>(null));
        return builder.Service;
      }
    }

    public IServiceEntity<TNext> Register<TNext>()
    {
      container.Register<TRegister, TResolve>(new AppTransientCreationalBehaviour<TRegister>(null));
      return new AppServiceEntity<TNext>(builder, container);
    }
  }
}
