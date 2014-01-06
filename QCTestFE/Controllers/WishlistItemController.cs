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
    public string Post(int?[] baseItems)//Item item, Wishlist wishlist)
    {
      try
      {
        if (baseItems == null || baseItems.Length < 2)
          return "DEV Error: C# - Incorrect parameters were passed through.";
        var itemID = baseItems[0] as int?;
        var wishlistID = baseItems[1] as int?;
        if (itemID == null || wishlistID == null)
          return "DEV Error: C# - Either itemID or wishlistID is null";
        
        //Get object info
        Wishlist wishlist = new Wishlist();
        wishlist.WishlistID = wishlistID.Value;

        wishlist.AddItemToList(itemID.Value);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return ex.Message;
      }
      return string.Empty;
    }
  }
}
