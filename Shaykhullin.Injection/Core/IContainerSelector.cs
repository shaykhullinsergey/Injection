namespace Shaykhullin.Injection
{
  public interface IContainerSelector : IContainerBuilder
  {
    IContainerBuilder Singleton(params object[] args);
  }
}
