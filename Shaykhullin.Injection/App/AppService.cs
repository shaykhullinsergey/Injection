using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection.App
{
  internal class AppService : IService
  {
    private IDependencyContainer<AppDependency> container;

    public AppService(IDependencyContainer<AppDependency> container)
    {
      this.container = container;
    }

    public TResolve Resolve<TResolve>(params object[] args)
    {
      return Resolve<TResolve, TResolve>(args);
    }

    public TResolve Resolve<TResolve, TRegister>(params object[] args)
    {
      var creator = container.Get(new AppDependency(typeof(TRegister), typeof(TResolve)))
        ?? throw new NotSupportedException($"Type {typeof(TResolve).Name} is not registered in container");

      return AppUtils.ResolveInstance<TResolve>(container, creator, args);
    }

    public void ResolveFor<TResolve>(TResolve instance)
    {
      AppUtils.ResolveInstanceRecursive(container, instance);
    }

    public IEnumerable<TResolve> ResolveAll<TResolve>()
    {
      foreach (var creator in container.GetAll<TResolve>())
      {
        yield return AppUtils.ResolveInstance<TResolve>(container, creator);
      }
    }
  }
}