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
using System.Data.SqlClient;

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
      try
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
        activeUser.SetupChildren();
      }
      catch (SqlException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (IndexOutOfRangeException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (NullReferenceException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (TimeoutException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}