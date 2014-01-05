using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.Objects;

namespace QCTestApp.Objects.Shopping
{
  public class ItemPictureList : BaseList<ItemPicture>
  {
    public override object AddNew()
    {
      ItemPicture item = new ItemPicture();
      this.Add(item);
      _baseList.Add(item);
      item.Initialize();
      return item;
    }

    public ItemPicture GetItemPictureByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as ItemPicture;
    }
  }
}
