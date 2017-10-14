using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace Shaykhullin.Injection.Tests.ServiceBuilderTests
{
  public class LegacyTests
  {
    class A
    {

    }

    class B : A
    {
      public int Int { get; set; }
    }

    class C : A
    {
      [Inject]
      public B B { get; set; }
    }

    class D
    {
      [Inject(1, 2)]
      public E E { get; set; }
    }

    class E
    {
      public int A { get; set; }
      public int B { get; set; }

      public E(int a, int b)
      {
        A = a;
        B = b;
      }
    }

    public class F
    {
      [Inject(12)]
      public G G { get; set; }
    }

    public class G
    {
      public int A { get; set; }

      public G(int a)
      {
        A = a;
      }
    }

    public class H
    {
      public int X { get; set; }
      public int Y { get; set; }

      public H(int x, int y)
      {
        X = x;
        Y = y;
      }
    }

    // [Fact]
    private void TestTree()
    {
      var service = new AppContainerBuilder()
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
          .Returns(s => new B())
          .As<A>()
          .Singleton()
        .Register<A>()
          .As<A>()
          .Singleton()
        .Container;
    }

    [Fact]
    public void EntityReturnWorks()
    {
      var service = new AppContainerBuilder()
        .Register<A>()
        .Register<B>()
      .Container;

      var a1 = service.Resolve<A>();
      var a2 = service.Resolve<A>();

      var b1 = service.Resolve<B>();
      var b2 = service.Resolve<B>();

      Assert.NotEqual(a1, a2);
      Assert.NotEqual(b1, b2);
    }

    [Fact]
    public void SingletonArgsWorks()
    {
      var service = new AppContainerBuilder()
        .Register<H>()
          .Singleton(3, 4)
        .Container;

      var h = service.Resolve<H>();

      Assert.Equal(3, h.X);
      Assert.Equal(4, h.Y);
    }


    [Fact]
    public void ResolveAllReturnsAllOfType()
    {
      var service = new AppContainerBuilder()
        .Register<A>()
        .Register<B>()
          .As<A>()
        .Register<C>()
          .As<A>()
        .Register<B>()
        .Container;

      var resolved = service.ResolveAll<A>();

      Assert.True(resolved.Count() == 3);
    }

    [Fact]
    public void ReturnsServiceWorks()
    {
      var g = new G(10);

      var service = new AppContainerBuilder()
        .Register<F>()
          .Returns(s => new F { G = s.Resolve<G>() })
          .Singleton()
        .Register<G>()
          .Returns(s => g)
          .Singleton()
        .Container;

      var f = service.Resolve<F>();
      Assert.Equal(f.G, g);
    }

    [Fact]
    public void ReturnsReturns()
    {
      var b = new B();

      var service = new AppContainerBuilder()
        .Register<B>()
        .Returns(s => b)
        .Singleton()
        .Container;

      Assert.Equal(b, service.Resolve<B>());
    }



    [Fact]
    public void NamedAndArgsResolves()
    {
      var service = new AppContainerBuilder()
        .Register<E>()
        .Container;

      var e = service.Resolve<E>(3, 4);

      Assert.True(e.A == 3);
      Assert.True(e.B == 4);
    }

    [Fact]
    public void EmptyInjectResolves()
    {
      var service = new AppContainerBuilder()
        .Register<B>()
          .Singleton()
        .Register<C>()
          .Singleton()
        .Container;

      var b = service.Resolve<B>();
      var c = service.Resolve<C>();

      Assert.Equal(b, c.B);
    }

    [Fact]
    public void ResolveNamedReturnsCorrent()
    {
      var service = new AppContainerBuilder()
        .Register<A>()
        .Register<B>()
          .As<A>()
        .Container;

      Assert.True(service.Resolve<A, B>() is B);
    }



    [Fact]
    public void TransientReferencesNotEqual()
    {
      var service = new AppContainerBuilder()
        .Register<A>()
        .Container;

      var a1 = service.Resolve<A>();
      var a2 = service.Resolve<A>();

      Assert.False(ReferenceEquals(a1, a2));
    }

    [Fact]
    public void SingletonReferencesEqual()
    {
      var service = new AppContainerBuilder()
        .Register<A>()
          .Singleton()
        .Container;

      var a1 = service.Resolve<A>();
      var a2 = service.Resolve<A>();

      Assert.True(ReferenceEquals(a1, a2));
    }

    class V
    {
      [Inject(Resolve = typeof(int))]
      public IEnumerable<N> Ns { get; set; }
    }

    class N
    {
    }

    class N1 : N
    {

    }

    class N2 : N
    {

    }

    [Fact]
    public void ReturnsIEnumerable()
    {
      var container = new AppContainerBuilder()
        .Register<N>()
        .Register<N1>().As<N>()
        .Register<N2>().As<N>()
        .Register<V>()
      .Container;

      var v = container.Resolve<V>();

      Assert.Equal(v.Ns.Count(), 3);
    }
  }
}
