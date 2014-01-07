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
    public class OrderController : ApiController
    {
      // GET api/<controller>
      [HttpGet]
      public Info Get()
      {
        Info info = new Info();
        try
        {
          OrderList orders = new OrderList();
          string qry = "SELECT * FROM [Shopping].[Order]";
          DataAccess.ReadObjectData(orders, qry);
          info.ObjectList = orders;
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
