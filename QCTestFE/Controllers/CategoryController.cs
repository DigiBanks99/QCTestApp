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
      public CategoryList Get()
      {
        if (Tools.CategoryCache != null)
          return Tools.CategoryCache;

        string qry = "SELECT * FROM [Shopping].[Category]";
        CategoryList catList = new CategoryList();
        DataAccess.ReadObjectData(catList, qry);
        return catList;
      }

      [HttpGet]
      public Category Get(int id)
      {
        Category cat = Tools.CategoryCache.GetCategoryByKey(id);
        if (cat != null)
          return cat;

        cat = new Category();
        cat.CategoryID = id;
        DataAccess.ReadObjectData(cat);
        return cat;
      }
    }
}
