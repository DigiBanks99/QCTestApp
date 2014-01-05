using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class WishlistList : BaseList<Wishlist>
  {
    public override object AddNew()
    {
      Wishlist item = new Wishlist();
      this.Add(item);
      item.Initialize();
      return item;
    }

    public Wishlist GetWishlistByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as Wishlist;
    }
  }
}
