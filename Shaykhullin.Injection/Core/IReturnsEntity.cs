namespace Shaykhullin.Injection
{
  public interface IReturnsEntity<TRegister> 
    : IServiceBuilder, IReturnsSelector<TRegister, TRegister>
  {
    IReturnsSelector<TRegister, TResolve> As<TResolve>();
  }
}
