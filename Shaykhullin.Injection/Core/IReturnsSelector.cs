namespace Shaykhullin.Injection
{
  public interface IReturnsSelector : IContainerBuilder
  {
    IContainerBuilder Singleton();
  }
}
