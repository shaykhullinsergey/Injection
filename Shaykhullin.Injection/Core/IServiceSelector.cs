namespace Shaykhullin.Injection
{
  public interface IServiceSelector<TRegister, TResolve> : IServiceBuilder
  {
    IServiceBuilder Singleton(params object[] args);
  }
}
