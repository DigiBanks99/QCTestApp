﻿using QCTestApp.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class Wishlist : Base
  {
    public const string COL_WISHLISTID = "WishlistID";
    public const string COL_USERID = "UserID";
    public const string COL_CODE = "Code";
    public const string COL_DATECREATED = "DateCreated";
    public const string COL_WISHLISTNAME = "WishlistName";
    public const string COL_CATEGORYID = "CategoryID";

    public const string CHILD_WISHLISTITEMS = "WishlistItems";

    private bool _wishlistItemsLoaded;

    public Wishlist() { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 6;
      base.Initialize();
      Code = Tools.GenCode(Tools.ActiveUser.Code);
      DateCreated = DateTime.Now;
      WishlistItems = new WishlistItemList();
      _wishlistItemsLoaded = false;
    }

    public override string GetObjectName()
    {
      return "Wishlist";
    }

    public override string GetSchemaName()
    {
      return "Shopping";
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_WISHLISTID;
      _columnList[1] = COL_USERID;
      _columnList[2] = COL_CODE;
      _columnList[3] = COL_DATECREATED;
      _columnList[4] = COL_WISHLISTNAME;
      _columnList[5] = COL_CATEGORYID;
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_WISHLISTID: return WishlistID;
        case COL_USERID: return UserID;
        case COL_CODE: return Code;
        case COL_DATECREATED: return DateCreated;
        case COL_WISHLISTNAME: return WishlistName;
        case COL_CATEGORYID: return CategoryID;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_WISHLISTID: WishlistID = Convert.ToInt32(value); break;
        case COL_USERID: UserID = Convert.ToInt32(value); break;
        case COL_CODE: Code = Convert.ToString(value); break;
        case COL_DATECREATED: DateCreated = Convert.ToDateTime(value); break;
        case COL_WISHLISTNAME: WishlistName = Convert.ToString(value); break;
        case COL_CATEGORYID: CategoryID = Convert.ToInt32(value); break;
      }
      base.SetValue(propertyName, value);
    }

    public override void UpdateChildren()
    {
      base.UpdateChildren();
    }

    public override void LoadChildren()
    {
      LoadWishlistItems();
    }
    #endregion //Overrides

    #region Properties
    private int _wishlistID;
    public int WishlistID
    {
      get { return _wishlistID; }
      set { SetProperty<int>(COL_WISHLISTID, ref _wishlistID, value); }
    }

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

    private DateTime _dateCreated;
    public DateTime DateCreated
    {
      get { return _dateCreated; }
      set { SetProperty<DateTime>(COL_DATECREATED, ref _dateCreated, value); }
    }

    private string _wishlistName;
    public string WishlistName
    {
      get { return _wishlistName; }
      set { SetProperty<string>(COL_WISHLISTNAME, ref _wishlistName, value); }
    }

    private int? _categoryID;
    public int? CategoryID
    {
      get { return _categoryID; }
      set { SetProperty<int?>(COL_CATEGORYID, ref _categoryID, value); }
    }

    private WishlistItemList _wishlistItems;
    public WishlistItemList WishlistItems
    {
      get { return _wishlistItems; }
      set { SetProperty<WishlistItemList>(CHILD_WISHLISTITEMS, ref _wishlistItems, value); }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      int? nullInt = null;
      _wishlistID = reader.IsDBNull(reader.GetOrdinal(COL_WISHLISTID)) ? -1 : reader.GetInt32(0);
      _userID = reader.IsDBNull(reader.GetOrdinal(COL_USERID)) ? -1 : reader.GetInt32(1);
      _code = reader.IsDBNull(reader.GetOrdinal(COL_CODE)) ? string.Empty : reader.GetString(2);
      _dateCreated = reader.IsDBNull(reader.GetOrdinal(COL_DATECREATED)) ? DateTime.MinValue : reader.GetDateTime(3);
      _wishlistName = reader.IsDBNull(reader.GetOrdinal(COL_WISHLISTNAME)) ? string.Empty : reader.GetString(4);
      _categoryID = reader.IsDBNull(reader.GetOrdinal(COL_CATEGORYID)) ? nullInt : reader.GetInt32(5);
    }
    #endregion //Data Access

    #region Public Methods
    public WishlistItemList GetWishlistItems()
    {
      return _wishlistItems;
    }

    public void AddItemToList(Item item)
    {
      AddItemToList(item.ItemID);
    }

    public void AddItemToList(int itemID)
    {
      var wlItem = WishlistItems.AddNew() as WishlistItem;
      wlItem.ItemID = itemID;
      wlItem.WishlistID = this.WishlistID;
      wlItem.DateAdded = DateTime.Now;
      wlItem.Save();
    }
    #endregion //Public Methods

    #region Private Methods
    public void LoadWishlistItems()
    {
      if (_wishlistItemsLoaded)
        return;

      string qry = "SELECT * FROM [Shopping].[WishlistItem] WHERE [WishlistID] = " + this.WishlistID;
      DataAccess.DataAccess.ReadObjectData(this.WishlistItems, qry);
      _wishlistItemsLoaded = true;
    }
    #endregion //Private Methods

    #region Events
    public override void OnPropertyChangedEvent(object sender, FrameWork.QCEventArguments.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == COL_WISHLISTID)
      {
        LoadWishlistItems();
      }
      base.OnPropertyChangedEvent(sender, e);
    }
    #endregion //Events
  }
}
