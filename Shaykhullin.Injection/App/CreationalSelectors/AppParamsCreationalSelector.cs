namespace Shaykhullin.Injection.App
{
	internal class AppParamsCreationalSelector<TRegister, TResolve> 
		: AppProviderBuilder<TRegister>, IParamsCreationalSelector<TRegister, TResolve>
	{
		public AppParamsCreationalSelector(IServiceBuilder builder, IDependencyContainer container) 
			: base(builder, container, null)
		{
		}
		
		public IServiceBuilder Singleton(params object[] args)
		{
			container.Register(new AppDependency(typeof(TResolve)),
			  new AppSingletonCreationalBehaviour<TRegister>(null, args));

			return builder;
		}
	}
}
