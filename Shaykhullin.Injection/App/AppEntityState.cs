using System;

namespace Shaykhullin.Injection.App
{
  internal class AppEntityState<TRegister>
  {
    public IServiceBuilder Builder { get; set; }
    public IDependencyContainer Container { get; set; }
    public Func<IService, TRegister> Returns { get; set; }
    public string Named { get; set; }

    public void Deconstruct(out IService service, out Func<IService, TRegister> returns)
    {
      service = Builder.Service;
      returns = Returns;
    }
  }
}
