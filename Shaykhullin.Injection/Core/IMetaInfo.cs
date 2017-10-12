using System.Reflection;
using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal interface IMetaInfo
  {
    void Deconstruct(out IEnumerable<(FieldInfo Field, InjectAttribute Inject)> fields,
     out IEnumerable<(PropertyInfo Property, InjectAttribute Inject)> props);
  }
}
