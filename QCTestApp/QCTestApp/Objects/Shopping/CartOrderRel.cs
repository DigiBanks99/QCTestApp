using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class CartOrderRel : Base
  {
    public const string COL_CARTORDERRELID = "CartOrderRelID";
    public const string COL_CARTID = "CartID";
    public const string COL_ORDERID = "OrderID";

    public CartOrderRel() { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 3;
      base.Initialize();
    }

    public override string GetObjectName()
    {
      return "CartOrderRel";
    }

    public override string GetSchemaName()
    {
      return "Shopping";
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_CARTORDERRELID;
      _columnList[1] = COL_CARTID;
      _columnList[2] = COL_ORDERID;
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_CARTORDERRELID: return CartOrderRelID;
        case COL_CARTID: return CartID;
        case COL_ORDERID: return OrderID;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_CARTORDERRELID: CartOrderRelID = Convert.ToInt32(value); break;
        case COL_CARTID: CartID = Convert.ToInt32(value); break;
        case COL_ORDERID: OrderID = Convert.ToInt32(value); break;
      }
      base.SetValue(propertyName, value);
    }
    #endregion //Overrides

    #region Properties
    private int _cartOrderRelID;
    public int CartOrderRelID
    {
      get { return _cartOrderRelID; }
      set { SetProperty<int>(COL_CARTORDERRELID, ref _cartOrderRelID, value); }
    }

    private int _cartID;
    public int CartID
    {
      get { return _cartID; }
      set { SetProperty<int>(COL_CARTID, ref _cartID, value); }
    }

    private int _orderID;
    public int OrderID
    {
      get { return _orderID; }
      set { SetProperty<int>(COL_ORDERID, ref _orderID, value); }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      _cartOrderRelID = reader.GetInt32(0);
      _cartID = reader.GetInt32(1);
      _orderID = reader.GetInt32(2);
    }
    #endregion //Data Access
  }
}
