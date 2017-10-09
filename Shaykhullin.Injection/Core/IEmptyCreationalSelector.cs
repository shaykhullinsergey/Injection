namespace Shaykhullin.Injection
{
	public interface IEmptyCreationalSelector<TRegister, TResolve> : IServiceBuilder
	{
		IServiceBuilder AsSingleton();
	}
}
