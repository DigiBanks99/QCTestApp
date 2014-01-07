using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QCTestApp.Objects;

namespace QCTestFE
{
  public class Info
  {
    public string Message { get; set; }
    public IBase Object { get; set; }
    public IBaseList ObjectList { get; set; }
    public bool Success { get; set; }

    public Info()
    {
      Message = string.Empty;
      Success = true;
    }
  }
}