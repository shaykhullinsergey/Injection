using System;

namespace Shaykhullin.Injection.App
{
  internal class AppSingletonCreationalBehaviour<TRegister> : ICreationalBehaviour
  {
    private readonly Lazy<object> lazy;

    public IMetaInfo MetaInfo { get; }

    public AppSingletonCreationalBehaviour(Func<TRegister> returns, object[] args)
    {
      MetaInfo = new AppMetaInfo<TRegister>();

      lazy = returns == null
        ? new Lazy<object>(() => Utils.CreateInstance<TRegister>(args))
        : new Lazy<object>(() => returns());
    }

    public TResolve Create<TResolve>(params object[] args)
    {
      if(args?.Length != 0)
      {
        throw new InvalidOperationException(
          $"Can't resolve singleton of type {typeof(TRegister)} using arguments");
      }

      return (TResolve)lazy.Value;
    }
  }
}