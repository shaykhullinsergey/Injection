using System;

namespace Shaykhullin.Injection
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  public class InjectAttribute : Attribute
  {
    public object[] Args { get; set; }

    public InjectAttribute(params object[] args)
    {
      Args = args ?? throw new ArgumentNullException(nameof(args));
    }
  }
}
