using System;

namespace Shaykhullin.Injection
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class InjectAttribute : Attribute
  {
    public object[] Args { get; }
    public Type Register { get; set; }

    public InjectAttribute(Type register)
    {
      Register = register ?? throw new ArgumentNullException(nameof(register));
    }

    public InjectAttribute(params object[] args)
    {
      Args = args ?? throw new ArgumentNullException(nameof(args));
    }

    public InjectAttribute(Type register, params object[] args)
    {
      Args = args ?? throw new ArgumentNullException(nameof(args));
      Register = register ?? throw new ArgumentNullException(nameof(register));
    }
  }
}