using System;

namespace Shaykhullin.Injection.App
{
  internal class AppServiceEntity<TRegister> : IServiceEntity<TRegister>
  {
    private AppEntityState<TRegister> state;

    public AppServiceEntity(AppEntityState<TRegister> state)
    {
      this.state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public ICreationalSelector<TRegister, TResolve> As<TResolve>()
    {
      return new AppCreationalSelector<TRegister, TResolve>(state);
    }

    public IServiceBuilder AsSingleton(params object[] args)
    {
      return new AppCreationalSelector<TRegister, TRegister>(state)
        .AsSingleton(args);
    }

    public IServiceBuilder AsTransient()
    {
      return new AppCreationalSelector<TRegister, TRegister>(state)
        .AsTransient();
    }

    public IServiceEntity<TRegister> Named(string named)
    {
      state.Named = named;
      return this;
    }

    public IServiceEntity<TRegister> Returns(TRegister returns)
    {
      state.Returns = returns;
      return this;
    }
  }
}
