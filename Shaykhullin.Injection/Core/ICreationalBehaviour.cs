namespace Shaykhullin.Injection
{
  internal interface ICreationalBehaviour
  {
    TResolve Create<TResolve>(object[] args);
  }
}
