using System;
using System.Collections.Generic;
using System.Text;

namespace Shaykhullin.Injection.App
{
	internal class AppReturnsEntity<TRegister> : AppProviderBuilder<TRegister>, IReturnsEntity<TRegister>
	{
		public AppReturnsEntity(IServiceBuilder builder, IDependencyContainer container, Func<TRegister> returns) 
			: base(builder, container, returns)
		{
		}

		public IEmptyCreationalSelector<TRegister, TResolve> As<TResolve>()
		{
			return new AppEmptyCreationalSelector<TRegister, TResolve>(builder, container, returns);
		}

		public IServiceBuilder AsSingleton()
		{
			return new AppEmptyCreationalSelector<TRegister, TRegister>(builder, container, returns)
				 .AsSingleton();
		}
	}
}
