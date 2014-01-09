using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.Objects.Security;
using QCTestApp.Objects.Shopping;
using QCTestApp.DataAccess;
using QCTestApp.FrameWork.QCEventArguments;
using QCTestApp.Objects;

namespace QCTestApp.FrameWork
{
  public static class Tools
  {
    public delegate void ObjectLoadedHandler(IBase sender, ObjectLoadedEventArgs e);
    public static event ObjectLoadedHandler OnObjectLoaded;

    /// <summary>
    /// This will generate a unique code in the following format:
    /// "[USR][YYYY][MM][DD][RRR]"
    /// Where RRR is three random numbers.
    /// </summary>
    /// <param name="str">The user code or any other string</param>
    /// <returns></returns>
    public static string GenCode(string str)
    {
      Random random = new Random();
      int rand = random.Next(0, 999);
      return string.Format("{0}{1}{2}{3}{4}", str == null || str.Length < 5 ? str : str.Substring(0, 5), DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, rand.ToString());
    }

    /// <summary>
    /// Initializes the tools and cache Items
    /// </summary>
    public static void LoadCache()
    {
      ItemPicturesCache.Initialize();
      UserCache.Initialize();
      ItemCache.Initialize();
      CategoryCache.Initialize();
    }

    private static User _activeUser = null;
    public static User ActiveUser
    {
      get
      { return _activeUser; }
      set
      {
        if (_activeUser == value)
          return;

        _activeUser = value;
        OnObjectLoaded += OnObjectLoadedEvent;
        OnObjectLoadedEvent(_activeUser, new ObjectLoadedEventArgs(_activeUser.GetObjectName(), value));
        OnObjectLoaded += OnObjectLoadedEvent;
      }
    }

    public static void OnObjectLoadedEvent(IBase sender, ObjectLoadedEventArgs e) 
    {
      if (sender is User)
      {
        var user = sender as User;
        if (user == null)
          return;

        user.SetupChildren();
        user.Save();
      }
    }

    #region Cache Items
    private static ItemPictureList _itemPicturesCache = null;
    public static ItemPictureList ItemPicturesCache
    {
      get
      {
        if (_itemPicturesCache == null)
        {
          _itemPicturesCache = new ItemPictureList();
          string qry = "SELECT * FROM [Shopping].[ItemPicture]";
          DataAccess.DataAccess.ReadObjectData(_itemPicturesCache, qry);
        }
        return _itemPicturesCache;
      }
    }

    private static UserList _userCache = null;
    public static UserList UserCache
    {
      get
      {
        if (_userCache == null)
        {
          _userCache = new UserList();
          string qry = "SELECT * FROM [Security].[User]";
          DataAccess.DataAccess.ReadObjectData(_userCache, qry);
        }
        return _userCache;
      }
    }

    private static ItemList _itemCache = null;
    public static ItemList ItemCache
    {
      get
      {
        if (_itemCache == null)
        {
          _itemCache = new ItemList();
          string qry = "SELECT * FROM [Shopping].[Item]";
          DataAccess.DataAccess.ReadObjectData(_itemCache, qry);
        }
        return _itemCache;
      }
    }

    private static CategoryList _categoryCache = null;
    public static CategoryList CategoryCache
    {
      get
      {
        if (_categoryCache == null)
        {
          _categoryCache = new CategoryList();
          string qry = "SELECT * FROM [Shopping].[Category]";
          DataAccess.DataAccess.ReadObjectData(_categoryCache, qry);
        }
        return _categoryCache;
      }
    }
    #endregion //Cache Items
  }
}

