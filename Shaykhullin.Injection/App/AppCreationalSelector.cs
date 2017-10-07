using System;

namespace Shaykhullin.Injection.App
{
  internal class AppCreationalSelector<TRegister, TResolve> : ICreationalSelector<TRegister, TResolve>
  {
    private AppEntityState<TRegister> state;

    public AppCreationalSelector(AppEntityState<TRegister> state)
    {
      this.state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public IServiceBuilder AsSingleton(params object[] args)
    {
      state.Container.Register(new AppDependency(typeof(TResolve), state.Named),
        new AppSingletonCreationalBehaviour<TRegister>(state, args));

      return state.Builder;
    }

    public IServiceBuilder AsTransient()
    {
      state.Container.Register(new AppDependency(typeof(TResolve), state.Named),
        new AppTransientCreationalBehaviour<TRegister>(state));

      return state.Builder;
    }
  }
}
