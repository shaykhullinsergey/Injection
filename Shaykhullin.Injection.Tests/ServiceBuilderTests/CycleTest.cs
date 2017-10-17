using System;
using Xunit;

namespace Shaykhullin.Injection.Tests.ServiceBuilderTests
{
  public class CycleTest
  {
    class A
    {
      [Inject]
      public B B { get; set; }
    }

    class B
    {
      [Inject]
      public A A { get; set; }
    }
    
    [Fact]
    public void CycleTests()
    {
        var container = new AppContainerBuilder()
          .Register<A>()
            .Returns(c => new A { B = c.Resolve<B>()})
            .Singleton()
          .Register<B>()
            .Returns(c => new B { A = c.Resolve<A>()})
          .Container;

        var a = container.Resolve<A>();
    } 
  }
}