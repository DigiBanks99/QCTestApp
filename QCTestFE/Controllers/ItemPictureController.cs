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
  public class ItemPictureController : ApiController
  {
    // GET api/<controller>
    public ItemPictureList Get()
    {
      return QCTestApp.FrameWork.Tools.ItemPicturesCache;
    }

    // GET api/<controller>/5
    public string Get(int id)
    {
      var pic = QCTestApp.FrameWork.Tools.ItemPicturesCache.GetItemPictureByKey(id);
      return "data:image/jpg;base64," + Convert.ToBase64String(pic.Picture);
    }

    // POST api/<controller>
    public void Post([FromBody]string value)
    {
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