using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Shaykhullin.Injection.App
{
    internal class AppIEnumerableCreationalBehaviour : ICreationalBehaviour
    {
        private readonly PropertyInfo property;
        private readonly IDependencyContainer container;

        public AppIEnumerableCreationalBehaviour(PropertyInfo property, IDependencyContainer container)
        {
            this.property = property;
            this.container = container;
        }
        
        public TResolve Create<TResolve>(object[] args)
        {
            var argType = property.PropertyType.GetGenericArguments()[0];

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