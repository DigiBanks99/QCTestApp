using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QCTestApp.Objects;
using QCTestApp.Objects.Shopping;
using QCTestApp.FrameWork;
using QCTestApp.DataAccess;


namespace QCTestFE.Controllers
{
  public class AddtocartController : ApiController
  {
    [HttpPost]
    public Info Post(object[] paramters)
    {
      Info info = new Info();
      try
      {
        string ids = "";
        for (int i = 0; i < paramters.Length; i++)
          ids += string.Format("{0}{1}", ids.Length > 0 ? "," : string.Empty, Convert.ToString(paramters[i]));

        ItemList items = new ItemList();
        string qry = string.Format("SELECT * FROM [Shopping].[Item] WHERE [ItemID] IN ({0})", ids);
        DataAccess.ReadObjectData(items, qry);
        foreach (var item in items)
          item.AddToCart();
      }
      catch (Exception ex)
      {
        info.Message = ex.Message;
        info.Success = false;
      }
      return info;
    }
  }
}
