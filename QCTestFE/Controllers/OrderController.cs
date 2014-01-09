using QCTestApp.DataAccess;
using QCTestApp.FrameWork;
using QCTestApp.Objects.Shopping;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QCTestFE.Controllers
{
    public class OrderController : ApiController
    {
      // GET api/<controller>
      [HttpGet]
      public Info Get()
      {
        Info info = new Info();
        try
        {
          if (Tools.ActiveUser != null && Tools.ActiveUser.ActiveCart != null)
          {
            info.ObjectList = Tools.ActiveUser.ActiveCart.OrderItems;
            return info;
          }

          if (Tools.ActiveUser.ActiveCart != null)
          {
            info.Message = "DEV Error: CS - The Active Cart has not been set.";
            info.Success = false;
            return info;
          }

          CartOrderRelList orderRels = new CartOrderRelList();
          string qry = "SELECT * FROM [Shopping].[CartOrderRel] WHERE [CartID] = " + Tools.ActiveUser.ActiveCart.CartID;
          DataAccess.ReadObjectData(orderRels, qry);

          foreach (var orderRel in orderRels)
            qry += string.Format("{0}{1}", qry.Length > 0 ? "," : string.Empty, orderRel.OrderID);

          OrderList orders = new OrderList();
          qry = string.Format("SELECT * FROM [Shopping].[Order] IN ({0})", qry);
          DataAccess.ReadObjectData(orders, qry);
          info.ObjectList = orders;
        }
        catch (SqlException ex)
        {
          info.Message = ex.Message;
          info.Success = false;
        }
        catch (Exception ex)
        {
          info.Message = ex.Message;
          info.Success = false;
        }
        return info;
      }

      // GET api/<controller>/5
      [HttpGet]
      public Info Get(int id)
      {
        Info info = new Info();
        try
        {
          Order order = new Order();
          order.OrderID = id;
          DataAccess.ReadObjectData(order);
          info.Object = order;
        }
        catch (Exception ex)
        {
          info.Message = ex.Message;
          info.Success = false;
        }
        return info;
      }

      [HttpPost]
      public Info Post(object[] parameters)
      {
        Info info = new Info();
        try
        {
          if (parameters == null || parameters.Length < 2)
          {
            info.Message = "DEV ERROR: C# - parameters were not sufficiently initialised for OrderController";
            info.Success = false;
            return info;
          }

          int orderID = Convert.ToInt32(parameters[0]);
          int quantity = Convert.ToInt32(parameters[1]);

          if (orderID == null || quantity == null)
          {
            info.Message = "DEV ERROR: C# - either the OrderID or the Quantity was not initialised for OrderController";
            info.Success = false;
            return info;
          }

          Order order = new Order();
          order.OrderID = orderID;
          DataAccess.ReadObjectData(order);
          order.Quantity = quantity;
          order.Save();
          info.Object = order;
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
