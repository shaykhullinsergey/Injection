using System;

namespace Shaykhullin.Injection
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class InjectAttribute : Attribute
  {
    public object[] Args { get; }
    public Type Resolve { get; set; }

    public InjectAttribute(Type resolve)
    {
      Resolve = resolve ?? throw new ArgumentNullException(nameof(resolve));
    }

    public InjectAttribute(params object[] args)
    {
      Args = args ?? throw new ArgumentNullException(nameof(args));
    }

    public InjectAttribute(Type resolve, params object[] args)
    {
      Args = args ?? throw new ArgumentNullException(nameof(args));
      Resolve = resolve ?? throw new ArgumentNullException(nameof(resolve));
    }
  }
}
