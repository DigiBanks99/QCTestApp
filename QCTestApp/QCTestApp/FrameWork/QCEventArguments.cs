using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.FrameWork.QCEventArguments
{
  public class PropertyChangedEventArgs
  {
    public PropertyChangedEventArgs(string propertyName, object value)
    {
      PropertyName = propertyName;
      Value = value;
    }

    public string PropertyName { get; set; }
    public object Value { get; set; }
  }

  public class ObjectLoadedEventArgs
  {
    public ObjectLoadedEventArgs(string objectName, object value)
    {
      ObjectName = objectName;
      Value = value;
    }

    public string ObjectName { get; set; }
    public object Value { get; set; }
  }
}
