using System;
using System.IO;
using System.Xml.Serialization;

namespace Shaykhullin.Injection.App
{
  internal class AppTransientCreationalBehaviour<TRegister> : ICreationalBehaviour
  {
    private IService service;
    private Func<IService, TRegister> returns;

    public AppTransientCreationalBehaviour(AppEntityState<TRegister> state)
    {
      (service, returns) = state;
    }

    public TResolve Create<TResolve>(params object[] args)
    {
      return (TResolve)(returns != null 
        ? DeepCopy(returns(service)) 
        : Activator.CreateInstance(typeof(TRegister), args));
    }

    private TRegister DeepCopy(TRegister returns)
    {
      using (var ms = new MemoryStream())
      {
        var serializer = new XmlSerializer(typeof(TRegister));
        serializer.Serialize(ms, returns);
        ms.Position = 0;
        return (TRegister)serializer.Deserialize(ms);
      }
    }
  }
}
