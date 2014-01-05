using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class Order : Base
  {
    public const string COL_ORDERID = "OrderID";
    public const string COL_ITEMID = "ItemID";
    public const string COL_QUANTITY = "Quantity";
    public const string COL_PRICEPERUNIT = "PricePerUnit";
    public const string COL_TOTAL = "Total";
    public const string COL_STATUS = "Status";
    public const string COL_ORDERDATE = "OrderDate";
    public const string COL_DISPATCHDATE = "DispatchDate";
    public const string COL_COMPLETIONDATE = "CompletionDate";

    public Order()
    { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 9;
      base.Initialize();
    }

    public override string GetObjectName()
    {
      return "Order";
    }

    public override string GetSchemaName()
    {
      return "Shopping";
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_ORDERID;
      _columnList[1] = COL_ITEMID;
      _columnList[2] = COL_QUANTITY;
      _columnList[3] = COL_PRICEPERUNIT;
      _columnList[4] = COL_TOTAL;
      _columnList[5] = COL_STATUS;
      _columnList[6] = COL_ORDERDATE;
      _columnList[7] = COL_DISPATCHDATE;
      _columnList[8] = COL_COMPLETIONDATE;
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_ORDERID: return OrderID;
        case COL_ITEMID: return ItemID;
        case COL_QUANTITY: return Quantity;
        case COL_PRICEPERUNIT: return PricePerUnit;
        case COL_TOTAL: return Total;
        case COL_STATUS: return Status;
        case COL_ORDERDATE: return OrderDate;
        case COL_DISPATCHDATE: return DispatchDate;
        case COL_COMPLETIONDATE: return CompletionDate;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_ORDERID: OrderID = Convert.ToInt32(value); break;
        case COL_ITEMID: ItemID = Convert.ToInt32(value); break;
        case COL_QUANTITY: Quantity = Convert.ToInt32(value); break;
        case COL_PRICEPERUNIT: PricePerUnit = Convert.ToDecimal(value); break;
        case COL_TOTAL: Total = Convert.ToDecimal(value); break;
        case COL_STATUS: Status = Convert.ToChar(value); break;
        case COL_ORDERDATE: OrderDate = Convert.ToDateTime(value); break;
        case COL_DISPATCHDATE: DispatchDate = Convert.ToDateTime(value); break;
        case COL_COMPLETIONDATE: CompletionDate = Convert.ToDateTime(value); break;
      }
      base.SetValue(propertyName, value);
    }
    #endregion //Overrides

    #region Properties
    private int _orderID;
    public int OrderID
    {
      get { return _orderID; }
      set { SetProperty<int>(COL_ORDERID, ref _orderID, value); }
    }

    private int _itemID;
    public int ItemID
    {
      get { return _itemID; }
      set { SetProperty<int>(COL_ITEMID, ref _itemID, value); }
    }

    private int _quantity;
    public int Quantity
    {
      get { return _quantity; }
      set { SetProperty<int>(COL_QUANTITY, ref _quantity, value); }
    }

    private decimal _pricePerUnit;
    public decimal PricePerUnit
    {
      get { return _pricePerUnit; }
      set { SetProperty<decimal>(COL_PRICEPERUNIT, ref _pricePerUnit, value); }
    }

    private decimal _total;
    public decimal Total
    {
      get { return _total; }
      set { SetProperty<decimal>(COL_TOTAL, ref _total, value); }
    }

    private char _status;
    public char Status
    {
      get { return _status; }
      set { SetProperty<char>(COL_STATUS, ref _status, value); }
    }

    private DateTime _orderDate;
    public DateTime OrderDate
    {
      get { return _orderDate; }
      set { SetProperty<DateTime>(COL_ORDERDATE, ref _orderDate, value); }
    }

    private DateTime? _dispatchDate;
    public DateTime? DispatchDate
    {
      get { return _dispatchDate; }
      set { SetProperty<DateTime?>(COL_DISPATCHDATE, ref _dispatchDate, value); }
    }

    private DateTime? _completionDate;
    public DateTime? CompletionDate
    {
      get { return _completionDate; }
      set { SetProperty<DateTime?>(COL_COMPLETIONDATE, ref _completionDate, value); }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      _orderID = reader.GetInt32(0);
      _itemID = reader.GetInt32(1);
      _quantity = reader.GetInt32(2);
      _pricePerUnit = reader.GetDecimal(3);
      _total = reader.GetDecimal(4);
      _status = reader.GetString(5).ToCharArray()[0];
      _orderDate = reader.GetDateTime(6);
      _dispatchDate = reader.GetDateTime(7);
      _completionDate = reader.GetDateTime(8);
    }
    #endregion //Data Access

    #region Public Methods
    public decimal CalcTotal()
    {
      return PricePerUnit * Quantity;
    }
    #endregion //Public Methods

    #region Private Methods
    private void UpdateCalcTotal()
    {
      Total = CalcTotal();
    }
    #endregion //Private Methods

    #region Events
    public override void OnPropertyChangedEvent(object sender, FrameWork.QCEventArguments.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == COL_QUANTITY || e.PropertyName == COL_PRICEPERUNIT)
        UpdateCalcTotal();
      base.OnPropertyChangedEvent(sender, e);
    }
    #endregion //Events
  }
}
