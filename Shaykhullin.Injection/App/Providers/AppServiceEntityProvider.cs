using System;
using System.Collections.Generic;
using System.Text;

namespace Shaykhullin.Injection.App
{
  internal class AppServiceEntityProvider<TRegister> : IServiceBuilder
  {
    protected readonly IServiceBuilder builder;
    protected readonly IDependencyContainer<AppDependency> container;

    public AppServiceEntityProvider(IServiceBuilder builder, 
      IDependencyContainer<AppDependency> container)
    {
      this.builder = builder;
      this.container = container;
    }

    public IService Service
    {
      get
      {
        container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
          new AppTransientCreationalBehaviour<TRegister>(null));

        return builder.Service;
      }
    }

    public IServiceEntity<TNext> Register<TNext>()
    {
      container.Register(new AppDependency(typeof(TRegister), typeof(TRegister)),
        new AppTransientCreationalBehaviour<TRegister>(null));

      return new AppServiceEntity<TNext>(builder, container);
    }
  }
}
