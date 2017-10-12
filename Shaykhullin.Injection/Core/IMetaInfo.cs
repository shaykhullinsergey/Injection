using System.Reflection;
using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal interface IMetaInfo
  {
    void Deconstruct(out IEnumerable<(FieldInfo field, InjectAttribute inject)> fields,
     out IEnumerable<(PropertyInfo prop, InjectAttribute inject)> props);
  }
}
