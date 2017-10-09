using System;
using System.Collections.Generic;
using System.Text;

namespace Shaykhullin.Injection.App
{
	internal class AppEmptyCreationalSelector<TRegister, TResolve> 
		: AppProviderBuilder<TRegister>, IEmptyCreationalSelector<TRegister, TResolve>
	{
		public AppEmptyCreationalSelector(IServiceBuilder builder, IDependencyContainer container, Func<TRegister> returns)
			: base(builder, container, returns)
		{
		}

		public IServiceBuilder AsSingleton()
		{
			container.Register(new AppDependency(typeof(TResolve)),
			  new AppSingletonCreationalBehaviour<TRegister>(returns, null));

			return builder;
		}
	}
}
