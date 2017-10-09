using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection.App
{
  internal class AppDependency : IDependency
  {
    public Type Type { get; }

    public AppDependency(Type type)
    {
      Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public override bool Equals(object obj)
    {
      var key = obj as AppDependency;
      return key != null &&
             EqualityComparer<Type>.Default.Equals(Type, key.Type);
    }

    public override int GetHashCode()
    {
      return unchecked(-243844509 * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type));
    }
  }
}
