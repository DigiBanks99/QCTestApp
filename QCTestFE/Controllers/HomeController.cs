using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.InteropServices;
using QCTestApp.Objects;
using QCTestApp.Objects.Security;
using QCTestApp.DataAccess;
using QCTestApp.FrameWork;
using System.Web.Mvc;

namespace QCTestFE.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      //Initialize App
      InitializeApp();

      //Setup DOM
      return View();
    }

    /// <summary>
    /// Loads the default parameters for the app and sets up the initial user
    /// </summary>
    public void InitializeApp()
    {
      Tools.LoadCache();
      User activeUser = null;
      if (Tools.UserCache.Count > 0)
        activeUser = Tools.UserCache[0];
      else
      {
        activeUser = QCTestApp.Objects.Security.User.NewUser();
        activeUser.UserName = "Shopper";
        activeUser.Code = "SHOPR";
        activeUser.Save();
      }
      Tools.ActiveUser = activeUser;
    }
  }
}