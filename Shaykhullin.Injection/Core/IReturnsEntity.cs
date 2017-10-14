namespace Shaykhullin.Injection
{
  public interface IReturnsEntity : IReturnsSelector
  {
    IReturnsSelector As<TResolve>();
  }
}
