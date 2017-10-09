using System;

namespace Shaykhullin.Injection.App
{
	internal class AppServiceEntity<TRegister> : AppProviderBuilder<TRegister>, IServiceEntity<TRegister>
	{
		public AppServiceEntity(IServiceBuilder builder, IDependencyContainer container) : base(builder, container, null)
		{
		}

		public IParamsCreationalSelector<TRegister, TResolve> As<TResolve>()
		{
			return new AppParamsCreationalSelector<TRegister, TResolve>(builder, container);
		}

		public IServiceBuilder Singleton(params object[] args)
		{
			return new AppParamsCreationalSelector<TRegister, TRegister>(builder, container)
			  .Singleton(args);
		}

		public IReturnsEntity<TRegister> Returns(Func<IService, TRegister> returns)
		{
			return new AppReturnsEntity<TRegister>(builder, container, () => returns(builder.Service));
		}
	}
}
