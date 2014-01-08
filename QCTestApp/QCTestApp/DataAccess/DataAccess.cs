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
    public static SqlDataReader GetDataReader(string query, SqlConnection connection)
    {
      try
      {
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.CommandType = System.Data.CommandType.Text;
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
      }
    }

    public static void ReadObjectData(IBase obj)
    {
      SqlConnection connection = null;
      try
      {
        connection = OpenConnection();
        string qry = string.Format("SELECT * FROM [{0}].[{1}] WHERE [{1}ID] IN ({2})", obj.GetSchemaName(), obj.GetObjectName(), obj.Identity);
        var reader = GetDataReader(qry, connection);
        reader.Read();
        obj.SetFieldsReader(reader);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection(connection);
      }
    }

    public static void ReadObjectData(IBaseList listObj, string qeury)
    {
      SqlConnection connection = null;
      try
      {
        connection = OpenConnection();
        if (qeury.Length == 0)
          return;

        var reader = GetDataReader(qeury, connection);
        while (reader.Read())
        {
          IBase obj = listObj.AddNew() as IBase;
          obj.SetFieldsReader(reader);
        }
        CloseConnection(connection);
        listObj.LoadChildren();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection(connection);
      }
    }

    private static object UpdateDatabase(SqlCommand cmd, SqlConnection connection)
    {
      try
      {
        cmd.Connection = connection;
        cmd.CommandType = System.Data.CommandType.Text;
        return cmd.ExecuteScalar();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
      }
    }

    public static int InsertDB(QCTestApp.Objects.Base obj)
    {
      SqlConnection connection = null;
      try
      {
        connection = OpenConnection();
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
        var key = UpdateDatabase(cmd, connection);
        var res = GetScalarRes<int?>(key);
        if (res.HasValue)
          obj.Identity = res.Value;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection(connection);
      }

      return obj.Identity;
    }

    public static int UpdateDB(QCTestApp.Objects.Base obj)
    {
      SqlConnection connection = null;
      try
      {
        connection = OpenConnection();
        StringBuilder sb = new StringBuilder();
        SqlCommand cmd = new SqlCommand();
        sb.Append(string.Format("UPDATE [{0}].[{1}]\n", obj.GetSchemaName(), obj.GetObjectName()));
        sb.Append("SET ");
        for (int i = 1; i < obj.ColCount; i++)
        {
          sb.Append(string.Format("{0} [{1}] = @{1}", i < 2 ? string.Empty : ",", obj.ColumnList[i]));
          var value = obj.GetValue(obj.ColumnList[i]);
          if (value == null)
            value = DBNull.Value;
          cmd.Parameters.Add(new SqlParameter(string.Format("@{0}", obj.ColumnList[i]), value));
        }
        sb.Append(" WHERE [" + obj.ColumnList[0] + "] = @" + obj.ColumnList[0] + "0;");
        cmd.Parameters.AddWithValue(string.Format("@{0}0", obj.ColumnList[0]), obj.GetValue(obj.ColumnList[0]));
        cmd.CommandText = sb.ToString();
        var key = UpdateDatabase(cmd, connection);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection(connection);
      }
      return obj.Identity;
    }

    public static void DeleteDB(QCTestApp.Objects.Base obj)
    {
      SqlConnection connection = null;
      try
      {
        connection = OpenConnection();
        SqlCommand cmd = new SqlCommand();
        string qry = string.Format("DELETE FROM [{0}].[{1}] WHERE [{1}ID] = @{1}ID", obj.GetSchemaName(), obj.GetObjectName());
        cmd.Parameters.AddWithValue(string.Format("@{0}ID", obj.GetObjectName()), obj.Identity);
        cmd.CommandText = qry;
        UpdateDatabase(cmd, connection);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection(connection);
      }
    }

    public static SqlConnection OpenConnection()
    {
      try
      {          
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["QCTestDB"].ConnectionString);

        switch (connection.State)
        {
          case ConnectionState.Broken:
          case ConnectionState.Connecting:
          case ConnectionState.Executing:
          case ConnectionState.Fetching:
          case ConnectionState.Open:
            CloseConnection(connection);
            break;
          case ConnectionState.Closed:
            connection.Open();
            break;
        }
        return connection;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
      }
    }

    public static bool CloseConnection(SqlConnection connection)
    {
      try
      {
        if (connection == null) return true;

        switch (connection.State)
        {
          case ConnectionState.Broken:
          case ConnectionState.Connecting:
          case ConnectionState.Executing:
          case ConnectionState.Fetching:
          case ConnectionState.Closed:
            break;
          case ConnectionState.Open:
            connection.Close();
            break;
        }
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw ex;
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
