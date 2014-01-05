using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects.Shopping
{
  public class ItemPicture : Base
  {
    public const string COL_ITEMPICTUREID = "ItemPictureID";
    public const string COL_ITEMID = "ItemID";
    public const string COL_PICTURE = "Picture";

    public ItemPicture()
    { }

    #region Overrides
    public override void Initialize()
    {
      _colCount = 3;
      base.Initialize();
    }

    public override string GetObjectName()
    {
      return "ItemPicture";
    }

    public override string GetSchemaName()
    {
      return "Shopping";
    }

    public override void SetColumnNames()
    {
      _columnList[0] = COL_ITEMPICTUREID;
      _columnList[1] = COL_ITEMID;
      _columnList[2] = COL_PICTURE;
    }

    public override object GetValue(string propertyName)
    {
      switch (propertyName)
      {
        case COL_ITEMPICTUREID: return ItemPictureID;
        case COL_ITEMID: return ItemID;
        case COL_PICTURE: return Picture;
      }
      return base.GetValue(propertyName);
    }

    public override void SetValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case COL_ITEMPICTUREID: ItemPictureID = Convert.ToInt32(value); break;
        case COL_ITEMID: ItemID = Convert.ToInt32(value); break;
        case COL_PICTURE: Picture = (byte[])value; break;
      }
      base.SetValue(propertyName, value);
    }
    #endregion //Overrides

    #region Properties
    private int _itemPictureID;
    public int ItemPictureID
    {
      get { return _itemPictureID; }
      set { SetProperty<int>(COL_ITEMPICTUREID, ref _itemPictureID, value); }
    }

    private int _itemID;
    public int ItemID
    {
      get { return _itemID; }
      set { SetProperty<int>(COL_ITEMID, ref _itemID, value); }
    }

    private byte[] _picture;
    public byte[] Picture
    {
      get { return _picture; }
      set { SetProperty<byte[]>(COL_PICTURE, ref _picture, value); }
    }
    #endregion //Properties

    #region Data Access
    public override void SetFieldsReader(System.Data.SqlClient.SqlDataReader reader)
    {
      _itemPictureID = reader.GetInt32(0);
      _itemID = reader.GetInt32(1);
      _picture = (byte[])reader.GetValue(2);
    }
    #endregion //Data Access
  }
}
