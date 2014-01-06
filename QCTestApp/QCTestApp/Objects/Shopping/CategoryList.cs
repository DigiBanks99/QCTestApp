using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.Objects;

namespace QCTestApp.Objects.Shopping
{
  public class CategoryList : BaseList<Category>
  {
    public override object AddNew()
    {
      Category category = new Category();
      this.Add(category);
      category.Initialize();
      return category;
    }

    public Category GetCategoryByKey(int key)
    {
      return base.GetBaseObjectByKey(key) as Category;
    }
  }
}
