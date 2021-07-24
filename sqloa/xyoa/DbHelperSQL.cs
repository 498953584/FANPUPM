using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace xyoa
{
    /// <summary>
    /// 数据访问抽象基础类 
    /// Copyright (C) 2004-2008 By zhengyong
    /// </summary>
    public abstract class DbHelperSQL
    { 
        public DbHelperSQL()
        {

        }
        ////自己解密数据库设置字符串
        //protected static string DecryptDBStr(string Text, string sKey)
        //{
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    int len;
        //    len = Text.Length / 2;
        //    byte[] inputByteArray = new byte[len];
        //    int x, i;
        //    for (x = 0; x < len; x++)
        //    {
        //        i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
        //        inputByteArray[x] = (byte)i;
        //    }
        //    des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        //    des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    return Encoding.Default.GetString(ms.ToArray());
        //} 
        //定义连接字符串。
        protected static string ConnectionString = ConfigurationManager.AppSettings["connstr"].ToString();
        protected static SqlConnection Connection;
        //定义数据库的打开和关闭方法
        protected static void Open()
        {
            if (Connection == null)
            {
                Connection = new SqlConnection(ConnectionString);
            }
            if (Connection.State.Equals(ConnectionState.Closed))
            {
                Connection.Open();
            }
        }
        protected static void Close()
        {
            if (Connection != null)
            {
                Connection.Close();
            }
        }
        //绑定到GridView
        public static void BindGridView(string SqlString, GridView MyGvData)
        {
            MyGvData.DataSource = GetDataSet(SqlString);
            MyGvData.DataBind();
        }
        //绑定到DropDownList，设定Text和value显示
        public static void BindDropDownList2(string SqlString, DropDownList MyDDL, string TextStr, string ValueStr)
        {
            DataSet MyDT =GetDataSet(SqlString);
            for(int i=0;i<MyDT.Tables[0].Rows.Count;i++)
            {
                ListItem MyItem = new ListItem();
                MyItem.Text = MyDT.Tables[0].Rows[i][TextStr].ToString();
                MyItem.Value = MyDT.Tables[0].Rows[i][ValueStr].ToString();
                MyDDL.Items.Add(MyItem);
            }            
        }
        //绑定到DropDownList，设定Text和value显示
        public static void BindDropDownList(string SqlString, DropDownList MyDDL, string TextStr, string ValueStr)
        {            
            MyDDL.Items.Clear();
            DataSet MyDT = GetDataSet(SqlString);
            for (int i = 0; i < MyDT.Tables[0].Rows.Count; i++)
            {
                ListItem MyItem = new ListItem();
                MyItem.Text = MyDT.Tables[0].Rows[i][TextStr].ToString();
                MyItem.Value = MyDT.Tables[0].Rows[i][ValueStr].ToString();
                MyDDL.Items.Add(MyItem);
            } 
        }
        //绑定到DropDownList，设定Text和value显示
        public static void BindItemList(string SqlString, ListBox MyDDL, string TextStr, string ValueStr)
        {            
            MyDDL.Items.Clear();
            DataSet MyDT = GetDataSet(SqlString);
            for (int i = 0; i < MyDT.Tables[0].Rows.Count; i++)
            {
                ListItem MyItem = new ListItem();
                MyItem.Text = MyDT.Tables[0].Rows[i][TextStr].ToString();
                MyItem.Value = MyDT.Tables[0].Rows[i][ValueStr].ToString();
                MyDDL.Items.Add(MyItem);
            } 
        }
        //绑定到DropDownList，设定Text和value显示
        public static void BindDropDownListAddEmpty(string SqlString, DropDownList MyDDL, string TextStr, string ValueStr)
        {            
            MyDDL.Items.Clear();
            ListItem MyItem1 = new ListItem();
            MyItem1.Text = "";
            MyItem1.Value = "0";
            MyDDL.Items.Add(MyItem1);
            DataSet MyDT = GetDataSet(SqlString);
            for (int i = 0; i < MyDT.Tables[0].Rows.Count; i++)
            {
                ListItem MyItem = new ListItem();
                MyItem.Text = MyDT.Tables[0].Rows[i][TextStr].ToString();
                MyItem.Value = MyDT.Tables[0].Rows[i][ValueStr].ToString();
                MyDDL.Items.Add(MyItem);
            } 
        }
        //返回一个用 | 分隔的字符串
        public static string GetStringList(string SqlString)
        {
            string ReturnStr = string.Empty;
            DataSet MyDT = GetDataSet(SqlString);
            for (int i = 0; i < MyDT.Tables[0].Rows.Count; i++)
            {
                if (ReturnStr.Length > 0)
                {
                    ReturnStr = ReturnStr + "|" + MyDT.Tables[0].Rows[i][0].ToString();
                }
                else
                {
                    ReturnStr = MyDT.Tables[0].Rows[i][0].ToString();
                }
            }            
            return ReturnStr;
        }
        //返回当前最大的列值
        public static int GetMaxID(string FieldName, string TableName)
        {
            int MyReturn = 0;
            DataSet MyDT = GetDataSet("select max(" + FieldName + ") from " + TableName);
            if (MyDT.Tables[0].Rows.Count>0)
            {
                MyReturn = int.Parse(MyDT.Tables[0].Rows[0][0].ToString());
            }            
            return MyReturn;
        }
        //判断用Sql查询的数据是否存在,true表示存在，False表示不存在
        public static bool Exists(string strSql)
        {
            object obj = DbHelperSQL.GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = DbHelperSQL.GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //返回SqlDataReader数据集,使用完后记得关闭SqlDataReader
        //public static SqlDataReader GetDataReader(string SqlString)
        //{
        //    try
        //    {
        //        Open();
        //        SqlCommand cmd = new SqlCommand(SqlString, Connection);
        //        return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {                
        //        //throw new Exception(ex.Message);
        //        return null; 
        //    }
        //}
        // 公有方法，获取数据，返回一个DataSet。    
        public static DataSet GetDataSet(string SqlString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(SqlString, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        try
                        {
                            da.Fill(ds, "ds");
                            cmd.Parameters.Clear();
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            //throw new Exception(ex.Message);
                        }
                        connection.Close();
                        return ds;
                    }
                }                
            }
        }
        // 公有方法，获取数据，返回一个DataTable。    
        public static DataTable GetDataTable(string SqlString)
        {
            DataSet dataset = GetDataSet(SqlString);
            return dataset.Tables[0];
        }
        // 公有方法，获取数据，返回首行首列。    
        public static string GetSHSL(string SqlString)
        {
            DataSet dataset = GetDataSet(SqlString);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                return Convert.ToString(dataset.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return "";
            }
        }
        // 公有方法，获取数据，返回首行首列的INT值。    
        public static int GetSHSLInt(string SqlString)
        {
            DataSet dataset = GetDataSet(SqlString);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dataset.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        // 公有方法，获取数据，返回一个DataRow。
        public static DataRow GetDataRow(string SqlString)
        {
            DataSet dataset = GetDataSet(SqlString);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                return dataset.Tables[0].Rows[0];
            }
            else
            {
                return null;
            }
        }        
        // 公有方法，执行Sql语句。对Update、Insert、Delete为影响到的行数，其他情况为-1
        public static int ExecuteSQL(String SqlString, Hashtable MyHashTb)
        {
            int count = -1;
            SqlConnection connectiontemp = new SqlConnection(ConnectionString);
            connectiontemp.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(SqlString, connectiontemp);
                foreach (DictionaryEntry item in MyHashTb)
                {
                    string[] CanShu = item.Key.ToString().Split('|');
                    if (CanShu[1].ToString().Trim() == "string")
                    {
                        cmd.Parameters.Add(CanShu[0], SqlDbType.VarChar);
                    }
                    else if (CanShu[1].ToString().Trim() == "int")
                    {
                        cmd.Parameters.Add(CanShu[0], SqlDbType.Int);
                    }
                    else if (CanShu[1].ToString().Trim() == "text")
                    {
                        cmd.Parameters.Add(CanShu[0], SqlDbType.Text);
                    }
                    else if (CanShu[1].ToString().Trim() == "datetime")
                    {
                        cmd.Parameters.Add(CanShu[0], SqlDbType.DateTime);
                    }
                    else
                    {
                        cmd.Parameters.Add(CanShu[0], SqlDbType.VarChar);
                    }
                    cmd.Parameters[CanShu[0]].Value = item.Value.ToString();
                }
                count = cmd.ExecuteNonQuery();
            }
            catch
            {
                count = -1;
            }
            finally
            {
                connectiontemp.Close();
            }
            return count;
        }
        // 公有方法，执行Sql语句。对Update、Insert、Delete为影响到的行数，其他情况为-1
        public static int ExecuteSQL(String SqlString)
        {
            int count = -1;
            SqlConnection connectionTemp = new SqlConnection(ConnectionString);
            connectionTemp.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(SqlString, connectionTemp);
                count = cmd.ExecuteNonQuery();
            }
            catch
            {
                count = -1;
            }
            finally
            {
                connectionTemp.Close();
            }
            return count;
        }
        // 公有方法，执行一组Sql语句。返回是否成功,采用事务管理，发现异常时回滚数据
        public static bool ExecuteSQL(string[] SqlStrings)
        {
            bool success = true;
            SqlConnection connectionTemp = new SqlConnection(ConnectionString);
            connectionTemp.Open();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = Connection.BeginTransaction();
            cmd.Connection = connectionTemp;
            cmd.Transaction = trans;
            try
            {
                foreach (string str in SqlStrings)
                {
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                success = false;
                trans.Rollback();
            }
            finally
            {
                connectionTemp.Close();
            }
            return success;
        }        
        // 执行一条计算查询结果语句，返回查询结果（object）。        
        public static object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            connection.Close();
                            return null;
                        }
                        else
                        {
                            connection.Close();
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        return null;
                        //throw e;
                    }
                }
            }
        }
        public static object GetSingle(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            connection.Close();
                            return null;
                        }
                        else
                        {
                            connection.Close();
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        //throw e;
                        return null;
                    }
                }
            }
        }
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            connection.Close();
                            return null;
                        }
                        else
                        {
                            connection.Close();
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        //throw e;
                        return null;
                    }
                }
            }
        }
        // 执行SQL语句，返回影响的记录数
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        connection.Close();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        //throw e;
                        return 0;
                    }
                }
            }
        }
        //执行查询语句，返回DataSet
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();

                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        //throw new Exception(ex.Message);
                    }
                    connection.Close();
                    return ds;
                }
            }
        }
        //判断GridView里面被选中的ID
        public static string CheckCbx(GridView GVData, string CheckBoxName, string LabID)
        {
            string str = "";
            for (int i = 0; i < GVData.Rows.Count; i++)
            {
                GridViewRow row = GVData.Rows[i];
                CheckBox Chk = (CheckBox)row.FindControl(CheckBoxName);
                Label LabVis = (Label)row.FindControl(LabID);
                if (Chk.Checked == true)
                {
                    if (str == "")
                    {
                        str = LabVis.Text.ToString();
                    }
                    else
                    {
                        str = str + "," + LabVis.Text.ToString();
                    }
                }
            }
            return str;
        }
        //在RowDataBound事件时使用
        public static void GridViewRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E4F4FF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
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