using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public class DBUtil
{
    private static DbConnection conn;
    public static string connectionString = ConfigurationManager.ConnectionStrings["Sql"].ToString();
    private static string dbType = "SqlServer";

    public static void BeginConn()
    {
        getConn();
    }

    private static ArrayList DataTable2ArrayList(DataTable data)
    {
        ArrayList list = new ArrayList();
        for (int i = 0; i < data.Rows.Count; i++)
        {
            DataRow row = data.Rows[i];
            Hashtable hashtable = new Hashtable();
            for (int j = 0; j < data.Columns.Count; j++)
            {
                object obj2 = row[j];
                if (obj2.GetType() == typeof(DBNull))
                {
                    obj2 = null;
                }
                hashtable[data.Columns[j].ColumnName] = obj2;
            }
            list.Add(hashtable);
        }
        return list;
    }

    public static void EndConn()
    {
        if (conn != null)
        {
            conn.Close();
            conn = null;
        }
    }

    public static void Execute(string sql)
    {
        Execute(sql, null);
    }

    public static void Execute(string sql, Hashtable args)
    {
        bool flag = conn != null;
        DbConnection connection = getConn();
        if (dbType != "MySql")
        {
            if (dbType == "Oracle")
            {
                OleDbCommand cmd = new OleDbCommand(sql, (OleDbConnection) connection);
                if (args != null)
                {
                    SetArgs(sql, args, cmd);
                }
                cmd.ExecuteNonQuery();
            }
            else if (dbType == "SqlServer")
            {
                SqlCommand command2 = new SqlCommand(sql, (SqlConnection) connection);
                if (args != null)
                {
                    SetArgs(sql, args, command2);
                }
                command2.ExecuteNonQuery();
            }
        }
        if (!flag)
        {
            EndConn();
        }
    }

    private static DbConnection getConn()
    {
        if (conn == null)
        {
            if (dbType != "MySql")
            {
                if (dbType == "Oracle")
                {
                    conn = new OleDbConnection(connectionString);
                }
                else if (dbType == "SqlServer")
                {
                    conn = new SqlConnection(connectionString);
                }
            }
            conn.Open();
        }
        return conn;
    }

    public static ArrayList Select(string sql)
    {
        return Select(sql, null);
    }

    public static ArrayList Select(string sql, Hashtable args)
    {
        DataTable dataTable = new DataTable();
        bool flag = conn != null;
        DbConnection connection = getConn();
        if (dbType != "MySql")
        {
            if (dbType == "Oracle")
            {
                OleDbCommand cmd = new OleDbCommand(sql, (OleDbConnection) connection);
                if (args != null)
                {
                    SetArgs(sql, args, cmd);
                }
                new OleDbDataAdapter(sql, connectionString).Fill(dataTable);
            }
            else if (dbType == "SqlServer")
            {
                SqlCommand command2 = new SqlCommand(sql, (SqlConnection) connection);
                if (args != null)
                {
                    SetArgs(sql, args, command2);
                }
                new SqlDataAdapter(sql, connectionString).Fill(dataTable);
            }
        }
        if (!flag)
        {
            EndConn();
        }
        return DataTable2ArrayList(dataTable);
    }

    private static void SetArgs(string sql, Hashtable args, IDbCommand cmd)
    {
        if (dbType != "MySql")
        {
            if (dbType == "Oracle")
            {
                MatchCollection matchs = Regex.Matches(sql, @"@\w+");
                int num = 1;
                foreach (Match match in matchs)
                {
                    string oldValue = match.Value;
                    string name = "@P" + num++;
                    sql = sql.Replace(oldValue, "?");
                    object obj2 = args[oldValue];
                    if (obj2 == null)
                    {
                        obj2 = args[oldValue.Substring(1)];
                    }
                    cmd.Parameters.Add(new OleDbParameter(name, obj2));
                }
                cmd.CommandText = sql;
            }
            else if (dbType == "SqlServer")
            {
                foreach (Match match2 in Regex.Matches(sql, @"@\w+"))
                {
                    string strA = match2.Value;
                    object obj3 = args[strA];
                    if (obj3 == null)
                    {
                        obj3 = args[strA.Substring(1)];
                    }
                    if (obj3 == null)
                    {
                        obj3 = DBNull.Value;
                    }
                    if ((string.Compare(strA, "@Critical2", true) == 0) && (args["Critical2"] == null))
                    {
                        obj3 = args["Critical"];
                    }
                    cmd.Parameters.Add(new SqlParameter(strA, obj3));
                }
                cmd.CommandText = sql;
            }
        }
    }
}

