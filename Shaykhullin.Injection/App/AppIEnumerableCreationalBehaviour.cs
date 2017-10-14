using System;
using System.Collections;
using System.Collections.Generic;

namespace Shaykhullin.Injection.App
{
    internal class AppIEnumerableCreationalBehaviour : ICreationalBehaviour
    {
        private readonly Type memberType;
        private readonly IDependencyContainer container;

        public AppIEnumerableCreationalBehaviour(Type memberType, IDependencyContainer container)
        {
            this.memberType = memberType;
            this.container = container;
            MetaInfo = new AppMetaInfo<AppIEnumerableCreationalBehaviour>();
        }
        
        public TResolve Create<TResolve>(object[] args)
        {
            var argType = memberType.GetGenericArguments()[0];

            var listType = typeof(List<>).MakeGenericType(argType);
            var myList = (IList)Activator.CreateInstance(listType);

            foreach (var c in container.GetAll(argType))
            {
                myList.Add(Utils.Resolve<object>(container, c));
            }

            return (TResolve)myList;
        }

        public IMetaInfo MetaInfo { get; }
    }
}