using System;

namespace Shaykhullin.Injection
{
	public interface IServiceEntity<TRegister> : IParamsCreationalSelector<TRegister, TRegister>, IServiceBuilder
	{
		IParamsCreationalSelector<TRegister, TResolve> As<TResolve>();
		IReturnsEntity<TRegister> Returns(Func<IService, TRegister> returns);
	}
}
