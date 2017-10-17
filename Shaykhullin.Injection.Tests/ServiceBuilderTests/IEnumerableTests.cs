using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shaykhullin.Injection.Tests.ServiceBuilderTests
{
  public class IEnumerableTests
  {
    class A
    {
    }

    class A1 : A
    {
    }

    class A2 : A1
    {
    }

    class A3 : A
    {
    }

    class B
    {
      [Inject]
      public IEnumerable<A> Enumerable { get; set; }
    }
    
    [Fact]
    public void IEnumerableWorks()
    {
      var container = new AppContainerBuilder()
        .Register<A>()
        .Register<A1>()
          .As<A>()
        .Register<A2>()
          .As<A>()
        .Register<A3>()
          .As<A>()
        .Register<B>()
      .Container;

      var b = container.Resolve<B>();
      Assert.Equal(b.Enumerable.Count(), 4);
    }  
  }
}