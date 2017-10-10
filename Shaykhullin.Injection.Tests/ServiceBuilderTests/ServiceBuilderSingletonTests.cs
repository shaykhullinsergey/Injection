using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Shaykhullin.Injection.Tests.ServiceBuilderTests
{
  public class ServiceBuilderSingletonTests
  {
    private class TestA { }

    [Fact]
    public void SingletonReturnsSameObject()
    {
      var service = new AppServiceBuilder()
        .Register<TestA>()
        .Singleton()
      .Service;

      var a1 = service.Resolve<TestA>();
      var a2 = service.Resolve<TestA>();

      Assert.Equal(a1, a2);
    }

    private class TestB
    {
      public int X { get; set; }
      public int Y { get; set; }

      public TestB(int x, int y)
      {
        X = x;
        Y = y;
      }
    }

    [Fact]
    public void SingletonWithParamsInstantiateCorrect()
    {
      var service = new AppServiceBuilder()
        .Register<TestB>()
          .Singleton(1, 2)
        .Service;

      var b = service.Resolve<TestB>();

      Assert.Equal(1, b.X);
      Assert.Equal(2, b.Y);
    }

    private class B
    {
      public int X { get; set; }
      public int Y { get; set; }

      public B(int x, int y)
      {
        X = x;
        Y = y;
      }
    }
    private class C
    {
      [Inject(1, 2)]
      public B B { get; set; }
    }


    /// <summary>
    /// Inject is more valuable than returns statement
    /// If in container registered transient instance with returns
    /// It will be ignored if Inject attribute or resolve will have arguments
    /// </summary>
    [Fact]
    public void RetrunsVsInject()
    {
      var service = new AppServiceBuilder()
        .Register<B>()
          .Returns(s => new B(3, 4))
        .Register<C>()
        .Service;

      var c = service.Resolve<C>();
    }

    /// <summary>
    /// If singleton, used Inject args attribute will be ignored (??? throws exception)
    /// </summary>
    [Fact]
    public void SingletonVsInject()
    {
      Assert.Throws<InvalidOperationException>(() =>
      {
        var service = new AppServiceBuilder()
          .Register<B>()
            .Singleton(3, 4)
          .Register<C>()
          .Service;

        var c = service.Resolve<C>();
      });
    }
  }
}
