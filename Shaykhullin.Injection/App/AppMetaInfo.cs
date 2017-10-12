using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shaykhullin.Injection.App
{
  internal class AppMetaInfo<TRegister> : IMetaInfo
  {
    private readonly IEnumerable<(FieldInfo field, InjectAttribute inject)> fields;
    private readonly IEnumerable<(PropertyInfo prop, InjectAttribute inject)> props;

    public AppMetaInfo()
    {
      fields = typeof(TRegister)
        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(field => field.IsDefined(typeof(InjectAttribute)))
        .Select(field => (field: field,
          inject: field.GetCustomAttribute<InjectAttribute>()));

      props = typeof(TRegister)
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Where(property => property.IsDefined(typeof(InjectAttribute)))
        .Select(property => (property: property,
          inject: property.GetCustomAttribute<InjectAttribute>()));
    }

    public void Deconstruct(out IEnumerable<(FieldInfo field, InjectAttribute inject)> fields,
      out IEnumerable<(PropertyInfo prop, InjectAttribute inject)> props)
    {
      fields = this.fields;
      props = this.props;
    }
  }
}
