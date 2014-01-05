using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.Objects;

namespace QCTestApp.Objects.Shopping
{
  public class CartList : BaseList<Cart>
  {
    public override object AddNew()
    {
      Cart cart = new Cart();
      this.Add(cart);
      cart.Initialize();
      return cart;
    }

    public Cart GetCartItemByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as Cart;
    }
  }
}
