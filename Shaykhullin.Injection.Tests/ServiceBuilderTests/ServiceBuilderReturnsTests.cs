using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Shaykhullin.Injection.Tests.ServiceBuilderTests
{
  public class ServiceBuilderReturnsTests
  {
    private class A { }
    private class B : A { }

    [Fact]
    public void ReturnsWorks()
    {
      var a = new A();

      var service = new AppContainerBuilder()
        .Register<A>()
          .Returns(s => a)
        .Container;

      Assert.Equal(a, service.Resolve<A>());
    }

    [Fact]
    public void ReturnsDoubleTypesWorks()
    {
      var service = new AppContainerBuilder()
        .Register<A>()
          .Returns(s => new A())
        .Register<B>()
          .Returns(s => new B())
          .As<A>()
        .Container;

      var ais = service.ResolveAll<A>();

      Assert.True(ais.Count() == 2);
    }

    private class C { }
    private class D
    {
      public C Test { get; set; }

      public D(C test)
      {
        Test = test ?? throw new ArgumentNullException(nameof(test));
      }
    }

    [Fact]
    public void ReturnsServiceInitializationWorks()
    {
      var c = new C();

      var service = new AppContainerBuilder()
        .Register<D>()
          .Returns(s => new D(s.Resolve<C>()))
        .Register<C>()
          .Returns(s => c)
        .Container;

      Assert.Equal(c, service.Resolve<D>().Test);
    }
  }
}
