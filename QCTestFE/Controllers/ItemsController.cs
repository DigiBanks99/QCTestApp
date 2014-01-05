using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QCTestApp.DataAccess;
using QCTestApp.Objects;
using QCTestApp.Objects.Shopping;

namespace QCTestFE.Controllers
{
  public class ItemsController : ApiController
  {
    // GET api/<controller>
    [HttpGet]
    public ItemList Get()
    {
      ItemList items = new ItemList();
      string qry = "SELECT * FROM [Shopping].[Item]";
      DataAccess.ReadObjectData(items, qry);      
      return items;
    }

    // GET api/<controller>/5
    [HttpGet]
    public Item Get(int id)
    {
      Item item = new Item();
      item.ItemID = id;
      DataAccess.ReadObjectData(item);
      return item;
    }

    // POST api/<controller>
    [HttpPost]
    public void Post(Item value)
    {
      DataAccess.ReadObjectData(value);
      value.AddToCart(1);
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