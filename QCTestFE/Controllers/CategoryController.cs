using QCTestApp.DataAccess;
using QCTestApp.FrameWork;
using QCTestApp.Objects.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QCTestFE.Controllers
{
    public class CategoryController : ApiController
    {
      [HttpGet]
      public Info Get()
      {
        Info info = new Info();
        try
        {
          if (Tools.CategoryCache != null)
          {
            info.ObjectList = Tools.CategoryCache;
            return info;
          }

          string qry = "SELECT * FROM [Shopping].[Category]";
          CategoryList catList = new CategoryList();
          DataAccess.ReadObjectData(catList, qry);
          info.ObjectList = catList;
        }
        catch (Exception ex)
        {
          info.Message = ex.Message;
          info.Success = false;
        }
        return info;
      }

      [HttpGet]
      public Info Get(int id)
      {
        Info info = new Info();
        try
        {
          Category cat = Tools.CategoryCache.GetCategoryByKey(id);
          if (cat != null)
          {
            info.Object = cat;
            return info;
          }

          cat = new Category();
          cat.CategoryID = id;
          DataAccess.ReadObjectData(cat);
          info.Object = cat;
        }
        catch (Exception ex)
        {
          info.Message = ex.Message;
          info.Success = false;
        }
        return info;
      }
    }
}
