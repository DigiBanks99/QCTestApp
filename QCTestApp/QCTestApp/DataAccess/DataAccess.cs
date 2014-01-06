using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using QCTestApp.Objects;

namespace QCTestApp.DataAccess
{
  public static class DataAccess
  {
    public static SqlConnection _connection;

    public static SqlDataReader GetDataReader(string query)
    {
      try
      {
        OpenConnection();
        SqlCommand cmd = new SqlCommand(query, _connection);
        cmd.CommandType = System.Data.CommandType.Text;
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return null;
      }
    }

    public static void ReadObjectData(IBase obj)
    {
      try
      {
        string qry = string.Format("SELECT * FROM [{0}].[{1}] WHERE [{1}ID] IN ({2})", obj.GetSchemaName(), obj.GetObjectName(), obj.Identity);
        var reader = GetDataReader(qry);
        reader.Read();
        obj.SetFieldsReader(reader);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        CloseConnection();
      }
    }

    public static void ReadObjectData(IBaseList listObj, string qeury)
    {
      try
      {
        if (qeury.Length == 0)
          return;

        var reader = GetDataReader(qeury);
        while (reader.Read())
        {
          IBase obj = listObj.AddNew() as IBase;
          obj.SetFieldsReader(reader);
        }
        CloseConnection();
        listObj.LoadChildren();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        CloseConnection();
      }
    }

    private static object UpdateDatabase(SqlCommand cmd)
    {
      try
      {
        OpenConnection();
        cmd.Connection = _connection;
        cmd.CommandType = System.Data.CommandType.Text;
        return cmd.ExecuteScalar();
      }
      catch (Exception ex)
      {
        System.Console.WriteLine(ex.Message);
        return -1;
      }
    }

    public static int InsertDB(QCTestApp.Objects.Base obj)
    {
      StringBuilder sb = new StringBuilder(),
        sbValues = new StringBuilder();
      SqlCommand cmd = new SqlCommand();
      sb.Append(string.Format("INSERT INTO [{0}].[{1}]\n", obj.GetSchemaName(), obj.GetObjectName()));
      sb.Append("(");
      for (int i = 1; i < obj.ColCount; i++)
      {
        sb.Append(string.Format("{0} [{1}]", i < 2 ? string.Empty : ",", obj.ColumnList[i]));
        sbValues.Append(string.Format("{0} @{1}", i < 2 ? string.Empty : ",", obj.ColumnList[i]));
        var value = obj.GetValue(obj.ColumnList[i]);
        if (value == null)
          value = DBNull.Value;
        cmd.Parameters.Add(new SqlParameter(string.Format("@{0}", obj.ColumnList[i]), value));
      }
      sb.Append(" )\n");
      sb.Append("VALUES (");
      sb.Append(sbValues.ToString());
      sb.Append("); SELECT @@IDENTITY");
      cmd.CommandText = sb.ToString();
      var key = UpdateDatabase(cmd);
      var res = GetScalarRes<int?>(key);
      if (res.HasValue)
        obj.Identity = res.Value;

      return obj.Identity;
    }

    public static int UpdateDB(QCTestApp.Objects.Base obj)
    {
      StringBuilder sb = new StringBuilder();
      SqlCommand cmd = new SqlCommand();
      sb.Append(string.Format("UPDATE [{0}].[{1}]\n", obj.GetSchemaName(), obj.GetObjectName()));
      sb.Append("SET ");
      for (int i = 1; i < obj.ColCount; i++)
      {
        sb.Append(string.Format("{0} [{1}] = @{1}", i < 2 ? string.Empty : ",", obj.ColumnList[i]));
        cmd.Parameters.Add(new SqlParameter(string.Format("@{0}", obj.ColumnList[i]), obj.GetValue(obj.ColumnList[i])));
      }
      sb.Append(" WHERE [" + obj.ColumnList[0] + "] = @" + obj.ColumnList[0] + "0;");
      cmd.Parameters.AddWithValue(string.Format("@{0}0", obj.ColumnList[0]), obj.GetValue(obj.ColumnList[0]));
      cmd.CommandText = sb.ToString();
      var key = UpdateDatabase(cmd);
      return obj.Identity;
    }

    public static void DeleteDB(QCTestApp.Objects.Base obj)
    {
      SqlCommand cmd = new SqlCommand();
      string qry = string.Format("DELETE FROM [{0}].[{1}] WHERE [{1}ID] = @{1}ID", obj.GetSchemaName(), obj.GetObjectName());
      cmd.Parameters.AddWithValue(string.Format("@{0}ID", obj.GetObjectName()), obj.Identity);
      cmd.CommandText = qry;
      UpdateDatabase(cmd);
    }

    public static bool OpenConnection()
    {
      try
      {
        if (_connection == null)  
          _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["QCTestDB"].ConnectionString);

        switch (_connection.State)
        {
          case ConnectionState.Broken:
          case ConnectionState.Connecting:
          case ConnectionState.Executing:
          case ConnectionState.Fetching:
          case ConnectionState.Open: 
            break;
          case ConnectionState.Closed:
            _connection.Open();
            break;
        }
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
    }

    public static bool CloseConnection()
    {
      try
      {
        if (_connection == null) return true;

        switch (_connection.State)
        {
          case ConnectionState.Broken:
          case ConnectionState.Connecting:
          case ConnectionState.Executing:
          case ConnectionState.Fetching:
          case ConnectionState.Closed:
            break;
          case ConnectionState.Open:
            _connection.Close();
            break;
        }
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
    }

    public static P GetScalarRes<P>(object result)
    {
      if (result == null || result == System.DBNull.Value)
        return default(P);

      Type t = typeof(P);
      Type u = Nullable.GetUnderlyingType(t);

      return (P)Convert.ChangeType(result, u ?? t);
    }
  }
}
