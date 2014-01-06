using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCTestApp.Objects
{
  #region Interface
  public interface IBaseList
  {
    IBase GetBaseObjectByKey(int key);
    object AddNew();
    void LoadChildren();
  }
  #endregion //Interface

  #region Class
  public abstract class BaseList<T> : BindingList<T>, IBaseList
  {
    protected List<IBase> _baseList = null;

    public BaseList()
    {
      Initialize();
    }

    public void Initialize()
    {
      _baseList = new List<IBase>();
    }

    public IBase GetBaseObjectByKey(int key)
    {
      foreach (IBase obj in this)
      {
        if (obj.Identity == key)
          return obj;
      }
      //if (_baseList == null || _baseList.Count < 1)
      //  return null;

      //foreach (IBase baseObject in _baseList)
      //{
      //  if (baseObject.Identity != key)
      //    continue;
      //  return baseObject;
      //}

      return null;
    }

    public void Save()
    {
      foreach (IBase obj in this)
      {
        obj.Save();
      }
    }

    public abstract object AddNew();

    public virtual void LoadChildren() 
    { 
      foreach (IBase obj in this)
      {
        obj.LoadChildren();
      }
    }
  }
  #endregion //Class
}
