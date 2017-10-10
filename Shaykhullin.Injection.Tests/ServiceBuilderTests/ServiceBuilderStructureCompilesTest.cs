using System;
using System.Collections.Generic;
using System.Text;

namespace Shaykhullin.Injection.Tests.ServiceBuilderTests
{
  public class ServiceBuilderStructureCompilesTest
  {
    private class A { }

    public void ServiceBuilderStructure()
    {
      var service = new AppServiceBuilder()
       .Register<A>()
       .Register<A>()
         .Singleton()
       .Register<A>()
         .As<A>()
         .Singleton()
       .Register<A>()
         .Returns(s => new A())
       .Register<A>()
         .Returns(s => new A())
         .As<A>()
       .Register<A>()
         .Returns(s => new A())
         .As<A>()
         .Singleton()
       .Register<A>()
         .Returns(s => new A())
         .As<A>()
         .Singleton()
       .Register<A>()
         .As<A>()
         .Singleton()
       .Service;
    }
  }
}
