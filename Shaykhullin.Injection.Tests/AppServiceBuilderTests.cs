using System;
using System.Linq;
using Xunit;

namespace Shaykhullin.Injection.Tests
{
  public class AppServiceBuilderTests
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
      [Inject(1, 2, Name = "SecondE")]
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

    [Fact]
    public void ReturnsReturns()
    {
      var b = new B();

      var service = new AppServiceBuilder()
        .Register<B>()
          .Returns(s => b)
          .AsSingleton()
        .Service;

      Assert.Equal(b, service.Resolve<B>());
    }

    [Fact]
    public void NamedAndArgsInjectResolves()
    {
      var service = new AppServiceBuilder()
        .Register<D>()
          .AsSingleton()
        .Register<E>()
          .Named("FirstE")
          .AsSingleton(0, 0)
        .Register<E>()
          .Named("SecondE")
          .AsTransient()
        .Service;

      var d = service.Resolve<D>();

      Assert.True(d.E.A == 1);
      Assert.True(d.E.B == 2);
    }

    [Fact]
    public void NamedAndArgsResolves()
    {
      var service = new AppServiceBuilder()
        .Register<E>()
          .Named("SecondE")
          .AsTransient()
        .Service;

      var e = service.Resolve<E>("SecondE", 3, 4);

      Assert.True(e.A == 3);
      Assert.True(e.B == 4);
    }

    [Fact]
    public void EmptyInjectNotResolvesNamed()
    {
      var service = new AppServiceBuilder()
        .Register<B>()
          .Named("B")
          .AsSingleton()
        .Register<C>()
          .AsSingleton()
        .Service;

      var c = service.Resolve<C>();

      Assert.Equal(null, c.B);
    }

    [Fact]
    public void EmptyInjectResolves()
    {
      var service = new AppServiceBuilder()
        .Register<B>()
          .AsSingleton()
        .Register<C>()
          .AsSingleton()
        .Service;

      var b = service.Resolve<B>();
      var c = service.Resolve<C>();

      Assert.Equal(b, c.B);
    }

    [Fact]
    public void CantRegisterTwoSimilarDependencies()
    {
      Assert.Throws<ArgumentException>(() => 
      {
        var service = new AppServiceBuilder()
          .Register<A>()
            .AsSingleton()
          .Register<B>()
            .As<A>()
            .AsSingleton()
          .Service;
      });
    }

    [Fact]
    public void ResolveNamedReturnsCorrent()
    {
      var service = new AppServiceBuilder()
        .Register<A>()
          .AsTransient()
        .Register<B>()
          .Named("B")
          .As<A>()
          .AsTransient()
        .Service;

      Assert.True(service.Resolve<A>("B") is B);
    }

    [Fact]
    public void ResolveAllReturnsAllOfType()
    {
      var service = new AppServiceBuilder()
        .Register<A>()
          .AsTransient()
        .Register<B>()
          .Named("B")
          .As<A>()
          .AsTransient()
        .Register<C>()
          .Named("C")
          .As<A>()
          .AsTransient()
        .Service;

      var resolved = service.ResolveAll<A>();

      Assert.True(resolved.Count() == 3);
    }

    [Fact]
    public void TransientReferencesNotEqual()
    {
      var service = new AppServiceBuilder()
        .Register<A>()
          .AsTransient()
        .Service;

      var a1 = service.Resolve<A>();
      var a2 = service.Resolve<A>();

      Assert.False(ReferenceEquals(a1, a2));
    }

    [Fact]
    public void SingletonReferencesEqual()
    {
      var service = new AppServiceBuilder()
        .Register<A>()
          .AsSingleton()
        .Service;

      var a1 = service.Resolve<A>();
      var a2 = service.Resolve<A>();

      Assert.True(ReferenceEquals(a1, a2));
    }

  }
}
