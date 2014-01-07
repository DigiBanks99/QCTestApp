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

      [HttpPost]
      public Info Post(WishlistInfo obj)
      {
        Info info = new Info();
        try
        {
          Wishlist wl = new Wishlist();
          wl.WishlistName = obj.WishlistName;
          wl.CategoryID = obj.CategoryID;
          wl.Code = Tools.GenCode(Tools.ActiveUser.UserName);
          wl.UserID = Tools.ActiveUser.UserID;
          wl.DateCreated = DateTime.Now;
          wl.Save();
        }
        catch (Exception ex)
        {
          info.Message = ex.Message;
          info.Success = false;
        }

        return info;
      }
    }

  public class WishlistInfo : Info
  {
    public string WishlistName { get; set; }
    public int CategoryID { get; set; }
  }
}
