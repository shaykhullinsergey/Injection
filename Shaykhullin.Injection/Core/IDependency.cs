using System;

namespace Shaykhullin.Injection
{
  internal interface IDependency
  {
    string Name { get; }
    Type Type { get; }
  }
}
