using System.Collections.Generic;
using System.Reflection;

namespace Shaykhullin.Injection
{
  internal interface ICreationalBehaviour
  {
    TResolve Create<TResolve>(object[] args);
    IMetaInfo Meta { get; }
  }
}
