using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal class AppDependency : IEquatable<AppDependency>
  {
    public Type Register { get; set; }
    public Type Resolve { get; set; }

    public AppDependency(Type register, Type resolve)
    {
      Register = register ?? throw new ArgumentNullException(nameof(register));
      Resolve = resolve ?? throw new ArgumentNullException(nameof(resolve));
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as AppDependency);
    }

    public bool Equals(AppDependency other)
    {
      return other != null &&
             EqualityComparer<Type>.Default.Equals(Register, other.Register) &&
             EqualityComparer<Type>.Default.Equals(Resolve, other.Resolve);
    }

    public override int GetHashCode()
    {
      var hashCode = -1044441629;
      hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Register);
      hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Resolve);
      return hashCode;
    }
  }
}
