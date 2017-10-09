using System;
using System.Collections.Generic;
using System.Text;

namespace Shaykhullin.Injection
{
	public interface IReturnsEntity<TRegister> : IServiceBuilder
	{
		IEmptyCreationalSelector<TRegister, TResolve> As<TResolve>();
		IServiceBuilder AsSingleton();
	}
}
