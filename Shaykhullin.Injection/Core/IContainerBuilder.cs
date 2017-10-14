namespace Shaykhullin.Injection
{
  public interface IContainerBuilder
  {
    IContainer Container { get; }
    IContainerEntity<TRegister> Register<TRegister>();
  }
}
