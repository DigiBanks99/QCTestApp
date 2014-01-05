using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Security
{
  public class UserList : BaseList<User>
  {
    public override object AddNew()
    {
      User item = new User();
      this.Add(item);
      item.Initialize();
      return item;
    }

    public User GetItemByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as User;
    }
  }
}
