using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QCTestApp.FrameWork;
using QCTestApp.DataAccess;
using QCTestApp.Objects;
using QCTestApp.Objects.Shopping;

namespace QCTestFE.Controllers
{
    public class CartController : ApiController
    {
      [HttpPost]
      public Info Post(object[] parameters)
      {
        Info info = new Info();
        try
        {
          Cart currentCart = Tools.ActiveUser.ActiveCart;
          if (parameters.Length == 0)
          {
            currentCart.CartOrderItems.ProcessOrders();
            currentCart.Checkout();
          }
          else
          {
            int[] ids = new int[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
              ids[i] = Convert.ToInt32(parameters[i]);

            currentCart.CartOrderItems.ProcessOrders(ids);
            currentCart.Checkout(ids);
          }
          OrderList remainingOrders = new OrderList();
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
