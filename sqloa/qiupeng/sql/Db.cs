using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace qiupeng.sql
{
	public class Db
	{
		public static string strConn = "";
		public static DataView Config;
		public static DataView Board;
		public static string ConnectionString
		{
			get
			{
				Db db = new Db();
				return db.DbPath();
			}
		}
		public string DbPath()
		{
			Db.strConn = "" + ConfigurationManager.AppSettings["connstr"].ToString() + "";
			return Db.strConn;
		}
		public SqlDataReader GetList(string Sql)
		{
			SqlConnection sqlConnection = new SqlConnection(Db.ConnectionString);
			SqlCommand sqlCommand = new SqlCommand(Sql, sqlConnection);
			sqlConnection.Open();
			SqlDataReader result = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
			sqlCommand.Dispose();
			return result;
		}
		public DataView GetGrid(string Sql, string Tb)
		{
			SqlConnection selectConnection = new SqlConnection(Db.ConnectionString);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Sql, selectConnection);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet, Tb);
			DataView defaultView = dataSet.Tables[Tb].DefaultView;
			sqlDataAdapter.Dispose();
			return defaultView;
		}
		public DataView GetGrid_Pages(string Sql, string Sort)
		{
			DataTable table = new DataTable();
			SqlConnection sqlConnection = new SqlConnection(Db.ConnectionString);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Sql, sqlConnection);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet, "MyDt");
			table = dataSet.Tables["MyDt"];
			DataView dataView = new DataView(table);
			dataView.Sort = Sort + " Desc";
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
			return dataView;
		}
		public DataView GetGrid_Pages_not(string Sql)
		{
			DataTable table = new DataTable();
			SqlConnection sqlConnection = new SqlConnection(Db.ConnectionString);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Sql, sqlConnection);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet, "MyDt");
			table = dataSet.Tables["MyDt"];
			DataView result = new DataView(table);
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
			return result;
		}
		public int GetCount(string Sql)
		{
			SqlConnection sqlConnection = new SqlConnection(Db.ConnectionString);
			SqlCommand sqlCommand = new SqlCommand(Sql, sqlConnection);
			sqlConnection.Open();
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			int result = 0;
			while (sqlDataReader.Read())
			{
				result = sqlDataReader.GetInt32(0);
			}
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
			return result;
		}
		public int ExeSql(string Sql)
		{
			SqlConnection sqlConnection = new SqlConnection(Db.ConnectionString);
			SqlCommand sqlCommand = new SqlCommand(Sql, sqlConnection);
			int result;
			try
			{
				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlConnection.Close();
				result = 1;
			}
			catch
			{
				sqlCommand.Dispose();
				sqlConnection.Close();
				result = 0;
			}
			return result;
		}
        public class DataTableConvertJson
        {

            #region dataTable转换成Json格式
            /// <summary>  
            /// dataTable转换成Json格式  
            /// </summary>  
            /// <param name="dt"></param>  
            /// <returns></returns>  
            public static string DataTable2Json(DataTable dt)
            {
                if (dt.Rows.Count != 0)
                {
                    StringBuilder jsonBuilder = new StringBuilder();
                    // jsonBuilder.Append("{");
                    ////jsonBuilder.Append(dt.TableName);
                    //jsonBuilder.Append("\":[");
                    jsonBuilder.Append("{\"Rows\":");


                    jsonBuilder.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        jsonBuilder.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            jsonBuilder.Append("\"");

                            jsonBuilder.Append(dt.Columns[j].ColumnName);
                            jsonBuilder.Append("\":\"");
                            jsonBuilder.Append(dt.Rows[i][j].ToString());
                            jsonBuilder.Append("\",");
                        }
                        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                        jsonBuilder.Append("},");
                    }
                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]");
                    //  jsonBuilder.Append("}");
                    jsonBuilder.Append(",\"Total\":");
                    jsonBuilder.Append(dt.Rows.Count + "}");
                    return jsonBuilder.ToString();
                }
                else
                {
                    return null;
                }
            }

            #endregion dataTable转换成Json格式
            #region DataSet转换成Json格式
            /// <summary>  
            /// DataSet转换成Json格式  
            /// </summary>  
            /// <param name="ds">DataSet</param> 
            /// <returns></returns>  
            public static string DatasetToJson(DataSet ds)
            {
                StringBuilder json = new StringBuilder();

                foreach (DataTable dt in ds.Tables)
                {
                    json.Append("{\"");
                    json.Append(dt.TableName);
                    json.Append("\":");
                    json.Append(DataTable2Json(dt));
                    json.Append("}");
                } return json.ToString();
            }
            #endregion

            /// <summary>
            /// Msdn
            /// </summary>
            /// <param name="jsonName"></param>
            /// <param name="dt"></param>
            /// <returns></returns>
            public static string DataTableToJson(string jsonName, DataTable dt)
            {
                StringBuilder Json = new StringBuilder();
                Json.Append("{\"" + jsonName + "\":[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]}");
                return Json.ToString();
            }
        }
	}
}
