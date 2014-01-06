using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using QCTestApp.FrameWork.QCEventArguments;

namespace QCTestApp.Objects
{
  public interface IBase : IDisposable
  {
    string[] ColumnList { get; }
    int ColCount { get; }
    int Identity { get; set; }
    void Initialize();
    string GetSchemaName();
    string GetObjectName();
    void SetColumnNames();
    object GetValue(string propertyName);
    void SetValue(string propertyName, object value);
    void SetFieldsReader(SqlDataReader reader);
    void Save();
    void LoadChildren();
  }

  public abstract class Base : IBase
  {
    public Base()
    {
      Initialize();
    }

    #region Delegates and Events
    public delegate void PropertyChangedHandler(object sender, PropertyChangedEventArgs e);
    public event PropertyChangedHandler OnPropertyChanged;
    #endregion //Delegates and Events

    #region Properties and Private Members
    protected string[] _columnList;
    protected int _colCount;

    public string[] ColumnList
    {
      get { return _columnList; }
    }

    public int ColCount
    {
      get { return _colCount; }
    }

    public int Identity
    {
      get
      {
        if (_columnList != null && _columnList.Length > 0)
        {
          return Convert.ToInt32(GetValue(_columnList[0]));
        }
        return -1;
      }
      set
      {
        if (_columnList == null)
          _columnList = new string[_colCount];
        SetValue(_columnList[0], value);
      }
    }

    public bool IsNew 
    {
      get
      {
        if (Identity < 1)
          return true;
        return false;
      }
    }
    #endregion //Properties and Private Members

    #region Virtual Methods
    public virtual void Initialize()
    {
      if (_colCount < 0)
        _colCount = 0;
      _columnList = new string[_colCount];
      SetColumnNames();
      OnPropertyChanged += OnPropertyChangedEvent;
    }

    public virtual void OnPropertyChangedEvent(object sender, PropertyChangedEventArgs e) { }

    public virtual object GetValue(string propertyName)
    {
      return null;
    }
    
    public virtual void SetValue(string propertyName, object value)
    {
      OnPropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName, value));
    }

    public virtual void Dispose()
    {
      OnPropertyChanged -= OnPropertyChangedEvent;
      _columnList = null;
    }

    public virtual void UpdateChildren() { }
    public virtual void SaveChildren() { }
    public virtual void LoadChildren() { }
    #endregion //Virtual Methods

    #region Utilities
    public void SetProperty<P>(string propertyName, ref P field, P value)
    {
      if (value == null)
        return;
      if (field != null && field.Equals(value))
        return;
      field = value;
      OnPropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName, value));
    }
    #endregion //Utilities

    #region Abstract Methods
    public abstract string GetSchemaName();
    public abstract string GetObjectName();
    public abstract void SetColumnNames();
    public abstract void SetFieldsReader(SqlDataReader reader);
    #endregion //Abstract Methods

    #region Data Access
    public virtual void Save()
    {
      if (IsNew)
        DataAccess.DataAccess.InsertDB(this);
      else
        DataAccess.DataAccess.UpdateDB(this);
      UpdateChildren();
      SaveChildren();
    }
    #endregion //Data Access
  }
}
