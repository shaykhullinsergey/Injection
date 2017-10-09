namespace Shaykhullin.Injection
{
  public interface IParamsCreationalSelector<TRegister, TResolve> : IServiceBuilder
  {
    IServiceBuilder Singleton(params object[] args);
  }
}
