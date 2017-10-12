using System;
using System.Collections.Generic;
using System.Text;

namespace Shaykhullin.Injection
{
  internal interface IDependency
  {
    Type Register { get; set; }
    Type Resolve { get; set; }
  }
}
