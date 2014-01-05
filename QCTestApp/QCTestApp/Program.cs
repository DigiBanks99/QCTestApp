using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCTestApp.Objects.Security;
using QCTestApp.DataAccess;

namespace QCTestApp
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        User myUser = new User();
        myUser.UserID = 1;
        DataAccess.DataAccess.ReadObjectData(myUser);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      Console.ReadKey();
    }
  }
}
