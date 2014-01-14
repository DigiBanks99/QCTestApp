using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class CartOrderRelList : BaseList<CartOrderRel>
  {
    public override object AddNew()
    {
      CartOrderRel item = new CartOrderRel();
      this.Add(item);
      item.Initialize();
      return item;
    }

    public CartOrderRel GetCartOrderByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as CartOrderRel;
    }

    public void ProcessOrders(int[] ids = null)
    {
      foreach (CartOrderRel order in this)
      {
        if (ids != null)
        {
          if (ids.Contains(order.OrderID))
            order.Process();
        }
        else
          order.Process();
      }
    }

    public void DeleteItems(int[] ids)
    {
      
    }
  }
}
