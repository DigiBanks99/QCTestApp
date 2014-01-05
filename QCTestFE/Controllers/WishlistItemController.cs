using QCTestApp.DataAccess;
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
    public string Post(Item item, Wishlist wishlist)
    {
      try
      {
        DataAccess.ReadObjectData(item);
        item.AddToWishList(wishlist);
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
