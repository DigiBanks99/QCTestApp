using QCTestApp.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class Item : Base
  {
    public const string COL_ITEMID = "ItemID";
    public const string COL_CODE = "Code";
    public const string COL_ITEMNAME = "ItemName";
    public const string COL_SHORTDESCRIPTION = "ShortDescription";
    public const string COL_DESCRIPTION = "Description";
    public const string COL_PRICE = "Price";
    public const string COL_CATEGORYID = "CategoryID";

    public Item()
    { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 7;
      base.Initialize();
    }

    public override string GetObjectName()
    {
      return "Item";
    }

    public override string GetSchemaName()
    {
      return "Shopping";
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_ITEMID;
      _columnList[1] = COL_CODE;
      _columnList[2] = COL_ITEMNAME;
      _columnList[3] = COL_SHORTDESCRIPTION;
      _columnList[4] = COL_DESCRIPTION;
      _columnList[5] = COL_PRICE;
      _columnList[6] = COL_CATEGORYID;
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_ITEMID: return ItemID;
        case COL_CODE: return Code;
        case COL_ITEMNAME: return ItemName;
        case COL_SHORTDESCRIPTION: return ShortDescription;
        case COL_DESCRIPTION: return Description;
        case COL_PRICE: return Price;
        case COL_CATEGORYID: return CategoryID;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_ITEMID: ItemID = Convert.ToInt32(value); break;
        case COL_CODE: Code = Convert.ToString(value); break;
        case COL_ITEMNAME: ItemName = Convert.ToString(value); break;
        case COL_SHORTDESCRIPTION: ShortDescription = Convert.ToString(value); break;
        case COL_DESCRIPTION: Description = Convert.ToString(value); break;
        case COL_PRICE: Price = Convert.ToDecimal(value); break;
        case COL_CATEGORYID: CategoryID = Convert.ToInt32(value); break;
      }
      base.SetValue(propertyName, value);
    }
    #endregion //Overrides

    #region Properties
    private int _itemID;
    public int ItemID
    {
      get { return _itemID; }
      set { SetProperty<int>(COL_ITEMID, ref _itemID, value); }
    }

    private string _code;
    public string Code
    {
      get { return _code; }
      set { SetProperty<string>(COL_CODE, ref _code, value); }
    }

    private string _itemName;
    public string ItemName
    {
      get { return _itemName; }
      set { SetProperty<string>(COL_ITEMNAME, ref _itemName, value); }
    }

    private string _shortDescription;
    public string ShortDescription
    {
      get { return _shortDescription; }
      set { SetProperty<string>(COL_SHORTDESCRIPTION, ref _shortDescription, value); }
    }

    private string _description;
    public string Description
    {
      get { return _description; }
      set { SetProperty<string>(COL_DESCRIPTION, ref _description, value); }
    }

    private decimal _price;
    public decimal Price
    {
      get { return _price; }
      set { SetProperty<decimal>(COL_PRICE, ref _price, value); }
    }

    private int _categoryID;
    public int CategoryID
    {
      get { return _categoryID; }
      set { SetProperty<int>(COL_CATEGORYID, ref _categoryID, value); }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      _itemID = reader.GetInt32(0);
      _code = reader.GetString(1);
      _itemName = reader.GetString(2);
      _shortDescription = reader.GetString(3);
      _description = reader.GetString(4);
      _price = reader.GetDecimal(5);
      _categoryID = reader.GetInt32(6);
    }
    #endregion //Data Access

    #region Public Methods
    public void AddToCart(int quantity = 1)
    {
      var cartOrder = Tools.ActiveUser.ActiveCart.CartOrderItems.AddNew() as CartOrderRel;
      var order = Tools.ActiveUser.ActiveCart.OrderItems.AddNew() as Order;
      order.Quantity = quantity;
      order.PricePerUnit = this.Price;
      order.ItemID = this.ItemID;
      order.Status = Constants.STATUS_NEW;
      order.OrderDate = DateTime.Now;
      order.DispatchDate = null;
      order.CompletionDate = null;
      order.Save();
      cartOrder.OrderID = order.OrderID;
      cartOrder.CartID = Tools.ActiveUser.ActiveCart.CartID;
      cartOrder.Save();
    }

    public void AddToWishList(Wishlist wishlist)
    {
      var wishlistItem = wishlist.WishlistItems.AddNew() as WishlistItem;
      wishlistItem.ItemID = this.ItemID;
      wishlistItem.WishlistID = wishlist.WishlistID;
      wishlistItem.DateAdded = DateTime.Now;
      wishlistItem.Save();
    }
    #endregion //Public Methods
  }
}
