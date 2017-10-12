using System;
using System.Collections.Generic;

namespace Shaykhullin.Injection
{
  internal class AppDependency<TRegister, TResolve> : IDependency
  {
    public Type Register { get; set; }
    public Type Resolve { get; set; }

    /// <summary>
    /// Try to use this constructor to register type
    /// </summary>
    public AppDependency()
    {
      Register = typeof(TRegister);
      Resolve = typeof(TResolve);
    }

    /// <summary>
    /// Try to use only for reflection types
    /// </summary>
    /// <param name="register"></param>
    /// <param name="resolve"></param>
    public AppDependency(Type register, Type resolve)
    {
      Register = register ?? throw new ArgumentNullException(nameof(register));
      Resolve = resolve ?? throw new ArgumentNullException(nameof(resolve));
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as AppDependency<TRegister, TResolve>);
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
