using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.Objects;
using QCTestApp.Objects.Shopping;
using QCTestApp.FrameWork;

namespace QCTestApp.Objects.Security
{
  public class User : Base
  {
    public const string COL_USERID = "UserID";
    public const string COL_CODE = "Code";
    public const string COL_USERNAME = "UserName";

    public const string CHILD_CARTS = "Carts";
    public const string CHILD_WISHLISTS = "Wishlists";

    public User() { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 3;
      base.Initialize();
    }

    public override string GetSchemaName()
    {
      return "Security";
    }

    public override string GetObjectName()
    {
      return "User";
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_USERID: return UserID;
        case COL_CODE: return Code;
        case COL_USERNAME: return UserName;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_USERID: UserID = Convert.ToInt32(value); return;
        case COL_CODE: Code = Convert.ToString(value); return;
        case COL_USERNAME: UserName = Convert.ToString(value); return;
      }
      base.SetValue(propertyName, value);
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_USERID;
      _columnList[1] = COL_CODE;
      _columnList[2] = COL_USERNAME;
    }

    public override void UpdateChildren()
    {
      base.UpdateChildren();

      foreach (var cart in Carts)
        cart.UserID = UserID;
      foreach (var wishlist in Wishlists)
        wishlist.UserID = UserID;
    }

    public override void SaveChildren()
    {
      Carts.Save();
      Wishlists.Save();

      base.SaveChildren();
    }
    #endregion //Overrides

    #region Properties
    private int _userID;

    public int UserID
    {
      get { return _userID; }
      set { SetProperty<int>(COL_USERID, ref _userID, value); }
    }

    private string _code;

    public string Code
    {
      get { return _code; }
      set { SetProperty<string>(COL_CODE, ref _code, value); }
    }

    private string _userName;

    public string UserName
    {
      get { return _userName; }
      set { SetProperty<string>(COL_USERNAME, ref _userName, value); }
    }

    private CartList _carts;
    public CartList Carts
    {
      get { return _carts; }
      set { SetProperty<CartList>(CHILD_CARTS, ref _carts, value); }
    }

    private WishlistList _wishlists;
    public WishlistList Wishlists
    {
      get { return _wishlists; }
      set { SetProperty<WishlistList>(CHILD_WISHLISTS, ref _wishlists, value); }
    }

    private Cart _activeCart;
    public Cart ActiveCart
    {
      get { return _activeCart; }
      set { _activeCart = value; }
    }

    private Cart _pendingCart;
    public Cart PendingCart
    {
      get { return _pendingCart; }
      set { _pendingCart = value; }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      if (reader.IsDBNull(0))
        Console.WriteLine("\nCan't read value for UserID... T_T");
      _userID = reader.GetInt32(0);
      _code = reader.GetString(1);
      _userName = reader.GetString(2);
    }
    #endregion //Data Access

    #region Public Methods
    public CartList GetCarts()
    {
      return _carts;
    }

    public WishlistList GetWishlists()
    {
      return _wishlists;
    }

    public void SetupChildren()
    {
      CreateCarts();
      CreateWishlists();
    }
    #endregion //Public Methods

    #region Private Methods
    private void CreateCarts()
    {
      if (Carts != null)
        return;
      Carts = new CartList();
      LoadCarts();
      if (Carts.Count > 0)
      {
        foreach (var crt in Carts)
        {
          if (crt.Status != Constants.STATUS_NEW && crt.Status != Constants.STATUS_ACTIVE)
            break;

          _activeCart = crt;
          _activeCart.Status = Constants.STATUS_ACTIVE;
          _activeCart.Save();
          return;
        }
      }
      var cart = Carts.AddNew() as Cart;
      _activeCart = cart;
      _activeCart.Status = Constants.STATUS_ACTIVE;
    }

    private void LoadCarts()
    {
      string qry = string.Format("SELECT * FROM [Shopping].[Cart] WHERE [UserID] = {0} AND ([Status] = '{1}' OR [Status] = '{2}')", UserID, Constants.STATUS_ACTIVE, Constants.STATUS_NEW);
      DataAccess.DataAccess.ReadObjectData(Carts, qry);
    }

    private void CreateWishlists()
    {
      if (Wishlists != null)
        return;
      Wishlists = new WishlistList();
      LoadWishlists();
      if (Wishlists.Count > 0)
        return;
      var wishlist = Wishlists.AddNew() as Wishlist;
      wishlist.WishlistName = "My Wishlist";
    }

    private void LoadWishlists()
    {
      string qry = "SELECT * FROM [Shopping].[Wishlist] WHERE [UserID] = " + UserID;
      DataAccess.DataAccess.ReadObjectData(Wishlists, qry);
    }
    #endregion //Private Methods

    #region Factory Methods
    public static User NewUser()
    {
      return new User();
    }
    #endregion //Factory Methods

    #region Events
    public override void OnPropertyChangedEvent(object sender, FrameWork.QCEventArguments.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == COL_CODE)
      {
        if ((string)e.Value != null && ((string)e.Value).Length > 1)
        {
          if (Tools.ActiveUser == null)
            Tools.ActiveUser = this;
          SetupChildren();
          Save();
        }
      }
      base.OnPropertyChangedEvent(sender, e);
    }
    #endregion //Events
  }
}
