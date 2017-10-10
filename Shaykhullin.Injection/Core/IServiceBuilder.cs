namespace Shaykhullin.Injection
{
  public interface IServiceBuilder
  {
    IService Service { get; }
    IServiceEntity<TRegister> Register<TRegister>();
  }
}
