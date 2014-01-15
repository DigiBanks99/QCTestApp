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
      public Info Get()
      {
        Info info = new Info();
        try
        {
          WishlistList wishlistList = new WishlistList();
          string qry = "SELECT * FROM [Shopping].[Wishlist] WHERE [UserID] = " + Tools.ActiveUser.UserID;
          DataAccess.ReadObjectData(wishlistList, qry);
          info.ObjectList = wishlistList;
        }
        catch (Exception ex)
        {
          info.Message = ex.Message;
          info.Success = false;
        }
        return info;
      }

      [HttpGet]
      public Info Get(int id)
      {
        Info info = new Info();
        try
        {
          Wishlist wishlist = new Wishlist();
          wishlist.WishlistID = id;
          DataAccess.ReadObjectData(wishlist);
          info.Object = wishlist;
        }
        catch (Exception ex)
        {
          info.Message = ex.Message;
          info.Success = false;
        }
        return info;
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
          if (obj.CategoryID != null || obj.CategoryID == -1)
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
    public int? CategoryID { get; set; }
  }
}
