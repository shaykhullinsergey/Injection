using System;

namespace Shaykhullin.Injection
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class InjectAttribute : Attribute
  {
    public string Name { get; set; }
    public object[] Args { get; set; }

    public InjectAttribute(params object[] args)
    {
      Args = args ?? throw new ArgumentNullException(nameof(args));
    }
  }
}
