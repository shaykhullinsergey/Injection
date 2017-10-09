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
					.AsSingleton()
				.Register<A>()
					.Returns(s => new B())
					.As<A>()
					.AsSingleton()
				.Register<A>()
					.As<A>()
					.Singleton()
				.Service;
		}

		[Fact]
		public void EntityReturnWorks()
		{
			var service = new AppServiceBuilder()
				.Register<A>()
				.Register<B>()
			.Service;

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
			var service = new AppServiceBuilder()
				.Register<H>()
					.Singleton(3, 4)
				.Service;

			var h = service.Resolve<H>();

			Assert.Equal(3, h.X);
			Assert.Equal(4, h.Y);
		}
		
		[Fact]
		public void ResolveFor()
		{
			var g = new G(2);

			var service = new AppServiceBuilder()
				.Register<G>()
					.Returns(s => g)
					.AsSingleton()
				.Service;

			var f = new F();

			service.ResolveFor(f);

			Assert.Equal(f.G, g);
		}

		[Fact]
		public void ReturnsServiceWorks()
		{
			var g = new G(10);

			var service = new AppServiceBuilder()
				.Register<F>()
					.Returns(s => new F { G = s.Resolve<G>() })
					.AsSingleton()
				.Register<G>()
					.Returns(s => g)
					.AsSingleton()
				.Service;

			var f = service.Resolve<F>();
			Assert.Equal(f.G, g);
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

		//[Fact]
		//public void NamedAndArgsInjectResolves()
		//{
		//  var service = new AppServiceBuilder()
		//    .Register<D>()
		//      .AsSingleton()
		//    .Register<E>()
		//      .Named("FirstE")
		//      .AsSingleton(0, 0)
		//    .Register<E>()
		//      .Named("SecondE")
		//      .AsTransient()
		//    .Service;

		//  var d = service.Resolve<D>();

		//  Assert.True(d.E.A == 1);
		//  Assert.True(d.E.B == 2);
		//}

		//[Fact]
		//public void NamedAndArgsResolves()
		//{
		//  var service = new AppServiceBuilder()
		//    .Register<E>()
		//      .Named("SecondE")
		//      .AsTransient()
		//    .Service;

		//  var e = service.Resolve<E>("SecondE", 3, 4);

		//  Assert.True(e.A == 3);
		//  Assert.True(e.B == 4);
		//}

		//[Fact]
		//public void EmptyInjectNotResolvesNamed()
		//{
		//  var service = new AppServiceBuilder()
		//    .Register<B>()
		//      .Named("B")
		//      .AsSingleton()
		//    .Register<C>()
		//      .AsSingleton()
		//    .Service;

		//  var c = service.Resolve<C>();

		//  Assert.Equal(null, c.B);
		//}

		[Fact]
		public void EmptyInjectResolves()
		{
			var service = new AppServiceBuilder()
			  .Register<B>()
				.Singleton()
			  .Register<C>()
				.Singleton()
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
			  .Singleton()
			.Register<B>()
			  .As<A>()
			  .Singleton()
			.Service;
			});
		}

		//[Fact]
		//public void ResolveNamedReturnsCorrent()
		//{
		//  var service = new AppServiceBuilder()
		//    .Register<A>()
		//      .AsTransient()
		//    .Register<B>()
		//      .Named("B")
		//      .As<A>()
		//      .AsTransient()
		//    .Service;

		//  Assert.True(service.Resolve<A>("B") is B);
		//}

		//[Fact]
		//public void ResolveAllReturnsAllOfType()
		//{
		//  var service = new AppServiceBuilder()
		//    .Register<A>()
		//      .AsTransient()
		//    .Register<B>()
		//      .Named("B")
		//      .As<A>()
		//      .AsTransient()
		//    .Register<C>()
		//      .Named("C")
		//      .As<A>()
		//      .AsTransient()
		//    .Service;

		//  var resolved = service.ResolveAll<A>();

		//  Assert.True(resolved.Count() == 3);
		//}

		[Fact]
		public void TransientReferencesNotEqual()
		{
			var service = new AppServiceBuilder()
			  .Register<A>()
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
				.Singleton()
			  .Service;

			var a1 = service.Resolve<A>();
			var a2 = service.Resolve<A>();

			Assert.True(ReferenceEquals(a1, a2));
		}

	}
}
