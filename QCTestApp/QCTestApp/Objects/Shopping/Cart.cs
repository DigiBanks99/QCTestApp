using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.FrameWork;

namespace QCTestApp.Objects.Shopping
{
  public class Cart : Base
  {
    public const string COL_CARTID = "CartID";
    public const string COL_USERID = "UserID";
    public const string COL_CODE = "Code";
    public const string COL_DATECREATED = "DateCreated";
    public const string COL_CARTTOTAL = "CartTotal";
    public const string COL_ITEMCOUNT = "ItemCount";
    public const string COL_STATUS = "Status";

    public const string CHILD_CARTORDERITEMS = "CartOrderItems";
    public const string CHILD_ORDERITEMS = "OrderItems";

    private bool _cartOrderItemsLoaded;
    private bool _orderItemsLoaded;

    public Cart() { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 7;
      base.Initialize();
      Status = Constants.STATUS_ACTIVE;
      Code = Tools.GenCode(Tools.ActiveUser.Code);
      DateCreated = DateTime.Now;
      CartOrderItems = new CartOrderRelList();
      OrderItems = new OrderList();
      _cartOrderItemsLoaded = false;
      _orderItemsLoaded = false;
    }

    public override string GetObjectName()
    {
      return "Cart";
    }

    public override string GetSchemaName()
    {
      return "Shopping";
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_CARTID;
      _columnList[1] = COL_USERID;
      _columnList[2] = COL_CODE;
      _columnList[3] = COL_DATECREATED;
      _columnList[4] = COL_CARTTOTAL;
      _columnList[5] = COL_ITEMCOUNT;
      _columnList[6] = COL_STATUS;
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_CARTID: return CartID;
        case COL_USERID: return UserID;
        case COL_CODE: return Code;
        case COL_DATECREATED: return DateCreated;
        case COL_CARTTOTAL: return CartTotal;
        case COL_ITEMCOUNT: return ItemCount;
        case COL_STATUS: return Status;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_CARTID: CartID = Convert.ToInt32(value); break;
        case COL_USERID: UserID = Convert.ToInt32(value); break;
        case COL_CODE: Code = Convert.ToString(value); break;
        case COL_DATECREATED: DateCreated = Convert.ToDateTime(value); break;
        case COL_CARTTOTAL: CartTotal = Convert.ToInt32(value); break;
        case COL_ITEMCOUNT: ItemCount = Convert.ToInt32(value); break;
        case COL_STATUS: Status = Convert.ToChar(value); break;
      }
      base.SetValue(propertyName, value);
    }

    public override void LoadChildren()
    {
      LoadCartOrderItems();
      LoadOrderItems();
      base.LoadChildren();
    }
    #endregion //Overrides

    #region Properties
    private int _cartID;
    public int CartID
    {
      get { return _cartID; }
      set { SetProperty<int>(COL_CARTID, ref _cartID, value); }
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

    private decimal _cartTotal;
    public decimal CartTotal
    {
      get { return _cartTotal; }
      set { SetProperty<decimal>(COL_CARTTOTAL, ref _cartTotal, value); }
    }

    private int _itemCount;
    public int ItemCount
    {
      get { return _itemCount; }
      set { SetProperty<int>(COL_ITEMCOUNT, ref _itemCount, value); }
    }

    private char _status;
    public char Status
    {
      get { return _status; }
      set { SetProperty<char>(COL_STATUS, ref _status, value); }
    }

    private CartOrderRelList _cartOrderItems;
    public CartOrderRelList CartOrderItems
    {
      get { return _cartOrderItems; }
      set { SetProperty<CartOrderRelList>(CHILD_CARTORDERITEMS, ref _cartOrderItems, value); }
    }

    private OrderList _orderItems;
    public OrderList OrderItems
    {
      get { return _orderItems; }
      set { SetProperty<OrderList>(CHILD_ORDERITEMS, ref _orderItems, value); }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      _cartID = reader.GetInt32(0);
      _userID = reader.GetInt32(1);
      _code = reader.GetString(2);
      _dateCreated = reader.GetDateTime(3);
      _cartTotal = reader.GetDecimal(4);
      _itemCount = reader.GetInt32(5);
      _status = reader.GetString(6).ToCharArray()[0];
    }
    #endregion //Data Access

    #region Public Methods
    public CartOrderRelList GetCartOrderItems()
    {
      return _cartOrderItems;
    }

    public OrderList GetOrderItems()
    {
      return _orderItems;
    }

    public void Checkout(int[] ids = null)
    {
      if (ids == null)
        SetStatus(Constants.STATUS_ORDERED);
      else
        SetStatus(Constants.STATUS_ORDERED, ids);
      bool cleared = true;
      foreach (var order in OrderItems)
      {
        if (order.DispatchDate == null)
        {
          cleared = false;
          break;
        }
      }
      if (!cleared)
        return;

      Tools.ActiveUser.PendingCart = Tools.ActiveUser.ActiveCart;
      Tools.ActiveUser.PendingCart.Status = Constants.STATUS_ORDERED;
      Tools.ActiveUser.PendingCart.Save();
      Tools.ActiveUser.ActiveCart = new Cart();
      Tools.ActiveUser.ActiveCart.UserID = Tools.ActiveUser.UserID;
      Tools.ActiveUser.ActiveCart.Save();
      Tools.ActiveUser.Save();
    }

    public void Dispatch()
    {
      SetStatus(Constants.STATUS_DISPATCHED);
      Tools.ActiveUser.PendingCart.Status = Constants.STATUS_DISPATCHED;
      Tools.ActiveUser.PendingCart.Save();
      Tools.ActiveUser.Save();
    }

    public void CompleteTransaction()
    {
      SetStatus(Constants.STATUS_CLOSED);
      Tools.ActiveUser.PendingCart.Status = Constants.STATUS_DISPATCHED;
      Tools.ActiveUser.PendingCart.Save();
      Tools.ActiveUser.PendingCart = null;
      Tools.ActiveUser.Save();
    }

    public void LoadCartOrderItems()
    {
      if (_cartOrderItemsLoaded)
        return;

      string qry = "SELECT * FROM [Shopping].[CartOrderRel] WHERE [CartID] = " + this.CartID;
      DataAccess.DataAccess.ReadObjectData(this.CartOrderItems, qry);

      _cartOrderItemsLoaded = true;
    }

    public void LoadOrderItems()
    {
      if (_orderItemsLoaded)
        return;

      string qry = string.Empty;
      foreach (var cartOrder in CartOrderItems)
        qry += string.Format("{0}{1}", qry.Length < 1 ? string.Empty : ",", cartOrder.OrderID);

      if (qry.Length > 0)
      {
        qry = string.Format("SELECT * FROM [Shopping].[Order] WHERE [OrderID] IN ({0})", qry);
        DataAccess.DataAccess.ReadObjectData(this.OrderItems, qry);
      }

      _orderItemsLoaded = true;
    }
    #endregion //Public Methods

    #region Private Methods
    public void SetStatus(char status)
    {
      foreach (var order in OrderItems)
      {
        order.DispatchDate = DateTime.Now;
        order.Status = status;
      }
      OrderItems.Save();
    }

    private void SetStatus(char status, int[] ids)
    {
      foreach (var order in OrderItems)
      {
        if (!ids.Contains(order.OrderID))
          continue;
        order.DispatchDate = DateTime.Now;
        order.Status = status;
      }
      OrderItems.Save();
    }
    #endregion //Private Methods

    #region Events
    public override void OnPropertyChangedEvent(object sender, FrameWork.QCEventArguments.PropertyChangedEventArgs e)
    {
      var cart = sender as Cart;
      if (cart == null || cart.Status == Constants.STATUS_CLOSED) 
        return;

      if (e.PropertyName == COL_STATUS && cart.Status == Constants.STATUS_ACTIVE)
      {
        if (Tools.ActiveUser != null)
          Tools.ActiveUser.ActiveCart = cart;
      }
      else if (e.PropertyName == COL_CARTID)
      {
        LoadCartOrderItems();
        LoadOrderItems();
      }
      base.OnPropertyChangedEvent(sender, e);
    }
    #endregion //Events
  }
}
