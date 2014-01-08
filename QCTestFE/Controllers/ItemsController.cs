using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QCTestApp.DataAccess;
using QCTestApp.Objects;
using QCTestApp.Objects.Shopping;
using QCTestApp.FrameWork;

namespace QCTestFE.Controllers
{
  public class ItemsController : ApiController
  {
    // GET api/<controller>
    [HttpGet]
    public Info Get()
    {
      Info info = new Info();
      try
      {
        if (Tools.ItemCache != null)
        {
          info.ObjectList = Tools.ItemCache;
          return info;
        }

        ItemList items = new ItemList();
        string qry = "SELECT * FROM [Shopping].[Item]";
        DataAccess.ReadObjectData(items, qry);
        info.ObjectList = items;
      }
      catch (Exception ex)
      {
        info.Message = ex.Message;
        info.Success = false;
      }
      return info;
    }

    // GET api/<controller>/5
    [HttpGet]
    public Info Get(int id)
    {
      Info info = new Info();
      try
      {
        Item item = Tools.ItemCache.GetItemByKey(id);
        if (item != null)
        {
          info.Object = item;
          return info;
        }

        item = new Item();
        item.ItemID = id;
        DataAccess.ReadObjectData(item);
        info.Object = item;
      }
      catch (Exception ex)
      {
        info.Message = ex.Message;
        info.Success = false;
      }
      return info;
    }

    [HttpGet]
    public Info Get(string categoryName)
    {
      Info info = new Info();
      try
      {
        ItemList itemList = new ItemList();
        
        foreach (var item in itemList)
        {
          if (Tools.CategoryCache.GetCategoryByKey(item.CategoryID).CategoryName != categoryName)
            continue;

          itemList.Add(item);
        }

        if (itemList != null)
        {
          info.ObjectList = itemList;
          return info;
        }

        string qry = string.Format("SELECT * FROM [Shopping].[Item] WHERE [CategoryID] = '{0}'", Tools.CategoryCache.GetCategoryByName(categoryName));
        DataAccess.ReadObjectData(itemList, qry);
        info.ObjectList = itemList;
      }
      catch (Exception ex)
      {
        info.Message = ex.Message;
        info.Success = false;
      }
      return info;
    }

    // POST api/<controller>
    [HttpPost]
    public Info Post(Item value)
    {
      Info info = new Info();
      try
      {
        DataAccess.ReadObjectData(value);
        value.AddToCart(1);
      }
      catch (Exception ex)
      {
        info.Message = ex.Message;
        info.Success = false;
      }
      return info;
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }
  }
}