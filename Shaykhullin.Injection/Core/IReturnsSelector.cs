namespace Shaykhullin.Injection
{
  public interface IReturnsSelector<TRegister, TResolve> : IServiceBuilder
  {
    IServiceBuilder Singleton();
  }
}
