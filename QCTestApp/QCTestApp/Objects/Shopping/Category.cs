using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class Category : Base
  {
    public const string COL_CATEGORYID = "CategoryID";
    public const string COL_CODE = "Code";
    public const string COL_CATEGORYNAME = "CategoryName";

    public Category()
    { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 3;
      base.Initialize();
    }

    public override string GetObjectName()
    {
      return "Category";
    }

    public override string GetSchemaName()
    {
      return "Shopping";
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_CATEGORYID;
      _columnList[1] = COL_CODE;
      _columnList[2] = COL_CATEGORYNAME;
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_CATEGORYID: return CategoryID;
        case COL_CODE: return Code;
        case COL_CATEGORYNAME: return CategoryName;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_CATEGORYID: CategoryID = Convert.ToInt32(value); break;
        case COL_CODE: Code = Convert.ToString(value); break;
        case COL_CATEGORYNAME: CategoryName = Convert.ToString(value); break;
      }
      base.SetValue(propertyName, value);
    }
    #endregion //Overrides

    #region Properties
    private int _categoryID;
    public int CategoryID
    {
      get { return _categoryID; }
      set { SetProperty<int>(COL_CATEGORYID, ref _categoryID, value); }
    }

    private string _code;
    public string Code
    {
      get { return _code; }
      set { SetProperty<string>(COL_CODE, ref _code, value); }
    }

    private string _categoryName;
    public string CategoryName
    {
      get { return _categoryName; }
      set { SetProperty<string>(COL_CATEGORYNAME, ref _categoryName, value); }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      _categoryID = reader.GetInt32(0);
      _code = reader.GetString(1);
      _categoryName = reader.GetString(2);
    }
    #endregion //Data Access
  }
}
