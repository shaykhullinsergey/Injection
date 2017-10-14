using System;
using System.Collections.Generic;
using System.Text;

namespace Shaykhullin.Injection.App
{
  internal class AppContainerEntityProvider<TRegister> : IContainerBuilder
  {
    protected readonly IContainerBuilder builder;
    protected readonly IDependencyContainer container;

    protected AppContainerEntityProvider(IContainerBuilder builder, 
      IDependencyContainer container)
    {
      this.builder = builder;
      this.container = container;
    }

    public IContainer Container
    {
      get
      {
        container.Register<TRegister, TRegister>(new AppTransientCreationalBehaviour<TRegister>(null));
        return builder.Container;
      }
    }

    public IContainerEntity<TNext> Register<TNext>()
    {
      container.Register<TRegister, TRegister>(new AppTransientCreationalBehaviour<TRegister>(null));
      return new AppContainerEntity<TNext>(builder, container);
    }
  }
}
