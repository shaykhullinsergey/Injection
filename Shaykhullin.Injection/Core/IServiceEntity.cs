using System;

namespace Shaykhullin.Injection
{
  public interface IServiceEntity<TRegister> 
    : IServiceBuilder, IServiceSelector<TRegister, TRegister>
  {
    IServiceSelector<TRegister, TResolve> As<TResolve>();
    IReturnsEntity<TRegister> Returns(Func<IService, TRegister> returns);
  }
}
