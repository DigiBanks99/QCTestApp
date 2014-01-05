using QCTestApp.DataAccess;
using QCTestApp.FrameWork;
using QCTestApp.Objects.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QCTestFE.Controllers
{
    public class WishlistController : ApiController
    {
      [HttpGet]
      public WishlistList Get()
      {
        WishlistList wishlistList = new WishlistList();
        string qry = "SELECT * FROM [Shopping].[Wishlist] WHERE [UserID] = " + Tools.ActiveUser.UserID;
        DataAccess.ReadObjectData(wishlistList, qry);
        return wishlistList;
      }

      [HttpGet]
      public Wishlist Get(int id)
      {
        Wishlist wishlist = new Wishlist();
        wishlist.WishlistID = id;
        DataAccess.ReadObjectData(wishlist);
        return wishlist;
      }
    }
}
