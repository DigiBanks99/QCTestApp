using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.Objects;

namespace QCTestApp.Objects.Shopping
{
  public class WishlistItemList : BaseList<WishlistItem>
  {
    public override object AddNew()
    {
      WishlistItem wlItem = new WishlistItem();
      this.Add(wlItem);
      wlItem.Initialize();
      return wlItem;
    }

    public WishlistItem GetWishListItemByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as WishlistItem;
    }
  }
}
