namespace Shaykhullin.Injection.App
{
  internal class AppEntityState<TRegister>
  {
    public IServiceBuilder Builder { get; set; }
    public IDependencyContainer Container { get; set; }
    public TRegister Returns { get; set; }
    public string Named { get; set; }
  }
}
