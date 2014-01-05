using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class WishlistItem : Base
  {
    public const string COL_WISHLISTITEMID = "WishlistItemID";
    public const string COL_WISHLISTID = "WishlistID";
    public const string COL_ITEMID = "ItemID";
    public const string COL_DATEADDED = "DateAdded";

    public WishlistItem() { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 4;
      base.Initialize();
    }

    public override string GetObjectName()
    {
      return "WishlistItem";
    }

    public override string GetSchemaName()
    {
      return "Shopping";
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_WISHLISTITEMID;
      _columnList[1] = COL_WISHLISTID;
      _columnList[2] = COL_ITEMID;
      _columnList[3] = COL_DATEADDED;
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_WISHLISTITEMID: return WishlistItemID;
        case COL_WISHLISTID: return WishlistID;
        case COL_ITEMID: return ItemID;
        case COL_DATEADDED: return DateAdded;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_WISHLISTITEMID: WishlistItemID = Convert.ToInt32(value); break;
        case COL_WISHLISTID: WishlistID = Convert.ToInt32(value); break;
        case COL_ITEMID: ItemID = Convert.ToInt32(value); break;
        case COL_DATEADDED: DateAdded = Convert.ToDateTime(value); break;
      }
      base.SetValue(propertyName, value);
    }
    #endregion //Overrides

    #region Properties
    private int _wishlistItemID;
    public int WishlistItemID
    {
      get { return _wishlistItemID; }
      set { SetProperty<int>(COL_WISHLISTITEMID, ref _wishlistItemID, value); }
    }

    private int _wishlistID;
    public int WishlistID
    {
      get { return _wishlistID; }
      set { SetProperty<int>(COL_WISHLISTID, ref _wishlistID, value); }
    }

    private int _itemID;
    public int ItemID
    {
      get { return _itemID; }
      set { SetProperty<int>(COL_ITEMID, ref _itemID, value); }
    }

    private DateTime _dateAdded;
    public DateTime DateAdded
    {
      get { return _dateAdded; }
      set { SetProperty<DateTime>(COL_DATEADDED, ref _dateAdded, value); }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      _wishlistItemID = reader.GetInt32(0);
      _wishlistID = reader.GetInt32(1);
      _itemID = reader.GetInt32(2);
      _dateAdded = reader.GetDateTime(3);
    }
    #endregion //Data Access
  }
}
