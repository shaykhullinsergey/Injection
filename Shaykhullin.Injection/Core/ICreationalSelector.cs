namespace Shaykhullin.Injection
{
  public interface ICreationalSelector<TRegister, TResolve>
  {
    IServiceBuilder AsTransient();
    IServiceBuilder AsSingleton(params object[] args);
  }
}
