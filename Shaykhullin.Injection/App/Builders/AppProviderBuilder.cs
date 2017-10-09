using Shaykhullin.Injection.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shaykhullin.Injection
{
    internal class AppProviderBuilder<TRegister> : IServiceBuilder
    {
		protected IServiceBuilder builder;
		protected IDependencyContainer container;
		protected Func<TRegister> returns;

		public AppProviderBuilder(IServiceBuilder builder, IDependencyContainer container, Func<TRegister> returns)
		{
			this.builder = builder;
			this.container = container;
			this.returns = returns;
		}

		public IService Service
		{
			get
			{
				AppUtils.RegisterTransientInstance<TRegister, TRegister>(container, returns);
				return builder.Service;
			}
		}

		public IServiceEntity<TNextRegister> Register<TNextRegister>()
		{
			AppUtils.RegisterTransientInstance<TRegister, TRegister>(container, returns);
			return new AppServiceEntity<TNextRegister>(builder, container);
		}
	}
}
