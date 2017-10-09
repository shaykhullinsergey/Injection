using System;

namespace Shaykhullin.Injection.App
{
	internal class AppSingletonCreationalBehaviour<TRegister> : ICreationalBehaviour
	{
		private Lazy<object> lazy;

		public AppSingletonCreationalBehaviour(Func<TRegister> returns, params object[] args)
		{
			lazy = returns == null
				? new Lazy<object>(() => AppUtils.CreateInstance<TRegister>(args))
				: new Lazy<object>(() => returns());
		}

		public TResolve Create<TResolve>(params object[] args)
		{
			return (TResolve)lazy.Value;
		}
	}
}
