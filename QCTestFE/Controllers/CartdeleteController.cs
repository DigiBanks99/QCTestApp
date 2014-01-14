using QCTestApp.DataAccess;
using QCTestApp.FrameWork;
using QCTestApp.Objects.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QCTestFE.Controllers
{
  public class CartdeleteController : ApiController
  {
    [HttpPost]
    public Info Post(object[] parameters)
    {
      Info info = new Info();
      try
      {
        Cart currentCart = Tools.ActiveUser.ActiveCart;
        string ids = string.Empty;
        for (int i = 0; i < parameters.Length; i++)
          ids = string.Format("{0}{1}", ids.Length > 0 ? "," : string.Empty, Convert.ToString(parameters[i]));

        CartOrderRelList cartOrderRelList = new CartOrderRelList();
        DataAccess.ReadObjectData(cartOrderRelList, string.Format("SELECT * FROM [Shopping].[CartOrderRel] WHERE [OrderID] IN ({0})", ids));
        string cids = string.Empty;
        foreach (var cartOrder in cartOrderRelList)
          cids = string.Format("{0}{1}", cids.Length > 0 ? "," : string.Empty, cartOrder.CartOrderRelID);
        DataAccess.DeleteDB(new CartOrderRel(), cids);
        DataAccess.DeleteDB(new Order(), ids);
        
        OrderList remainingOrders = new OrderList();
        Tools.ActiveUser.ActiveCart.FetchCartOrderItems();
        foreach (Order order in Tools.ActiveUser.ActiveCart.OrderItems)
        {
          if (order.DispatchDate == null)
            remainingOrders.Add(order);
        }
        info.ObjectList = remainingOrders;
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
