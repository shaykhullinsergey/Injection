using Xunit;

namespace Shaykhullin.Injection.Tests.ServiceBuilderTests
{
  public class RecursiveTests
  {
    class A
    {
    }

    class B
    {
      [Inject]
      public A A { get; set; }
    }

    class C
    {
      [Inject]
      public B B { get; set; }
    }

    class D
    {
      [Inject]
      public C C { get; set; }
    }

    [Fact]
    public void DContainsCorrectA()
    {
      var a = new A();
      
      var container = new AppContainerBuilder()
        .Register<A>()
          .Returns(c => a)
        .Register<B>()
        .Register<C>()
        .Register<D>()
        .Container;

      var d = container.Resolve<D>();
      
      Assert.Equal(d.C.B.A, a);
    }
  }
}