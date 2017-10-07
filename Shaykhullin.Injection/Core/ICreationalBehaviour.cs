namespace Shaykhullin.Injection
{
  internal interface ICreationalBehaviour
  {
    TResolve Create<TResolve>(params object[] args);
  }
}
