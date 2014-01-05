using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class OrderList : BaseList<Order>
  {
    public override object AddNew()
    {
      Order item = new Order();
      this.Add(item);
      item.Initialize();
      return item;
    }

    public Order GetOrderByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as Order;
    }
  }
}
