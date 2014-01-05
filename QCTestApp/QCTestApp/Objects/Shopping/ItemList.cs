using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.Objects;

namespace QCTestApp.Objects.Shopping
{
  public class ItemList : BaseList<Item>
  {
    public override object AddNew()
    {
      Item item = new Item();
      this.Add(item);
      item.Initialize();
      return item;
    }

    public Item GetItemByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as Item;
    }
  }
}
