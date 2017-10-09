using System.Collections.Generic;

namespace Shaykhullin.Injection
{
	public interface IService
	{
		TResolve Resolve<TResolve>(params object[] args);
		void ResolveFor<TResolve>(TResolve instance);
		IEnumerable<TResolve> ResolveAll<TResolve>(params object[] args);
	}
}
