using System;
using System.Collections;
using System.Collections.Generic;

namespace Shaykhullin.Injection.App
{
  internal class AppIEnumerableCreationalBehaviour : ICreationalBehaviour
  {
    private readonly Type register;
    private readonly IDependencyContainer container;

    public AppIEnumerableCreationalBehaviour(Type register, IDependencyContainer container)
    {
      this.register = register;
      this.container = container;
      MetaInfo = new AppMetaInfo<AppIEnumerableCreationalBehaviour>();
    }

    public TResolve Create<TResolve>(object[] args)
    {
      var argType = register.GetGenericArguments()[0];

      var myList = (IList)Activator.CreateInstance(
        typeof(List<>).MakeGenericType(argType));

      foreach (var c in container.GetAll(argType))
      {
        myList.Add(Utils.Resolve<object>(container, c, args));
      }

      return (TResolve)myList;
    }

    public IMetaInfo MetaInfo { get; }
  }
}