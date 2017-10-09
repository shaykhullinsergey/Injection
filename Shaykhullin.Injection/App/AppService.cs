using System;
using System.Collections.Generic;
using static Shaykhullin.Injection.AppUtils;

namespace Shaykhullin.Injection.App
{
	internal class AppService : IService
	{
		private IDependencyContainer container;

		public AppService(IDependencyContainer container)
		{
			this.container = container ?? throw new ArgumentNullException(nameof(container));
		}

		public TResolve Resolve<TResolve>(params object[] args)
		{
			var creator = container.Get(new AppDependency(typeof(TResolve)))
			   ?? throw new NotSupportedException($"Type {typeof(TResolve).Name} is not registered in container");

			return ResolveInstance<TResolve>(container, creator, args);
		}

		public IEnumerable<TResolve> ResolveAll<TResolve>(params object[] args)
		{
			foreach (var creator in container.GetAll<TResolve>())
			{
				yield return ResolveInstance<TResolve>(container, creator, args);
			}
		}

		public void ResolveFor<TResolve>(TResolve instance)
		{
			ResolveInstanceRecursive(container, instance);
		}
	}
}
