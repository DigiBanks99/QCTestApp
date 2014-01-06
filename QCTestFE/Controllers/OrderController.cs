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
      public OrderList Get()
      {
        OrderList orders = new OrderList();
        string qry = "SELECT * FROM [Shopping].[Order]";
        DataAccess.ReadObjectData(orders, qry);
        return orders;
      }

      // GET api/<controller>/5
      [HttpGet]
      public Order Get(int id)
      {
        Order order = new Order();
        order.OrderID = id;
        DataAccess.ReadObjectData(order);
        return order;
      }

      [HttpPost]
      public OrderInfo Post(object[] parameters)
      {
        OrderInfo info = new OrderInfo();
        try
        {
          if (parameters == null || parameters.Length < 2)
          {
            info.Message = "DEV ERROR: C# - parameters were not sufficiently initialised for OrderController";
            return info;
          }

          int orderID = Convert.ToInt32(parameters[0]);
          int quantity = Convert.ToInt32(parameters[1]);

          if (orderID == null || quantity == null)
          {
            info.Message = "DEV ERROR: C# - either the OrderID or the Quantity was not initialised for OrderController";
            return info;
          }

          Order order = new Order();
          order.OrderID = orderID;
          DataAccess.ReadObjectData(order);
          order.Quantity = quantity;
          order.Save();
          info.Order = order;
        }
        catch (Exception ex)
        {
          info.Message = ex.Message;
          return info;
        }

        return info;
      }
    }

  public class OrderInfo
  {
    public Order Order { get; set; }
    public string Message { get; set; }
  }
}
