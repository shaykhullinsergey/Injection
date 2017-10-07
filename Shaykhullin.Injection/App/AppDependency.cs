using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection.App
{
  internal class AppDependency : IDependency
  {
    public Type Type { get; }
    public string Name { get; }

    public AppDependency(Type type, string name)
    {
      Type = type ?? throw new ArgumentNullException(nameof(type));
      Name = name;
    }

    public override bool Equals(object obj)
    {
      var key = obj as AppDependency;
      return key != null &&
             Name == key.Name &&
             EqualityComparer<Type>.Default.Equals(Type, key.Type);
    }

    public override int GetHashCode()
    {
      var hashCode = -243844509;
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
      hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
      return hashCode;
    }
  }
}
