using System;

namespace Shaykhullin.Injection
{
  public interface IContainerEntity<in TRegister> : IContainerSelector
  {
    IContainerSelector As<TResolve>();
    IReturnsEntity Returns(Func<IContainer, TRegister> returns);
  }
}
