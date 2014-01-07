using QCTestApp.DataAccess;
using QCTestApp.Objects;
using QCTestApp.Objects.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QCTestFE.Controllers
{
  public class WishlistItemController : ApiController
  {
    [HttpPost]
    public Info Post(int?[] baseItems)//Item item, Wishlist wishlist)
    {
      Info info = new Info();
      try
      {
        if (baseItems == null || baseItems.Length < 2)
        {
          info.Message = "DEV Error: C# - Incorrect parameters were passed through.";
          info.Success = false;
          return info;
        }
        var itemID = baseItems[0] as int?;
        var wishlistID = baseItems[1] as int?;
        if (itemID == null || wishlistID == null)
        {
          info.Message = "DEV Error: C# - Either itemID or wishlistID is null.";
          info.Success = false;
          return info;
        }
        
        //Get object info
        Wishlist wishlist = new Wishlist();
        wishlist.WishlistID = wishlistID.Value;

        wishlist.AddItemToList(itemID.Value);
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
