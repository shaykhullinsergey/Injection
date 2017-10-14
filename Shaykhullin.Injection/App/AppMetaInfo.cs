using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Shaykhullin.Injection.App
{
  internal class AppMetaInfo<TRegister> : IMetaInfo
  {
    private readonly IEnumerable<(FieldInfo field, InjectAttribute inject)> fieldTypes;
    private readonly IEnumerable<(PropertyInfo prop, InjectAttribute inject)> propertyTypes;

    public AppMetaInfo()
    {
      fieldTypes = typeof(TRegister)
        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(field => field.IsDefined(typeof(InjectAttribute)))
        .Select(field => (field: field,
          inject: field.GetCustomAttribute<InjectAttribute>()));

      propertyTypes = typeof(TRegister)
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Where(property => property.IsDefined(typeof(InjectAttribute)))
        .Select(property => (property: property,
          inject: property.GetCustomAttribute<InjectAttribute>()));
    }

    public void Deconstruct(out IEnumerable<(FieldInfo Field, InjectAttribute Inject)> fields,
      out IEnumerable<(PropertyInfo Property, InjectAttribute Inject)> properties)
    {
      fields = fieldTypes;
      properties = propertyTypes;
    }
  }
}
