namespace cn.justwin.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Text;

    public abstract class DBHelper
    {
        protected DBHelper()
        {
        }

        public static int DelByStrWhere(string tbName, string strWhere)
        {
            string str = "delete from " + tbName + " " + strWhere;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), new SqlParameter[0]);
        }

        public static int ExecuteNoQuery(string sql)
        {
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, new SqlParameter[0]);
        }

        public static string FilterSpacialString(string str)
        {
            if (str.Contains("*"))
            {
                str = str.Replace("*", "[*]");
            }
            if (str.Contains("%"))
            {
                str = str.Replace("%", "[%]");
            }
            return str;
        }

        public static byte[] GetBinary(object obj)
        {
            if (obj != DBNull.Value)
            {
                return (byte[]) obj;
            }
            return null;
        }

        public static bool GetBool(object obj)
        {
            return ((obj != DBNull.Value) && Convert.ToBoolean(obj));
        }

        public static byte GetByte(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToByte(obj);
            }
            return 0;
        }

        public static string GetCurrentDBName()
        {
            string cmdText = "SELECT db_name(dbid) FROM master.dbo.sysprocesses WHERE spid = @@spid";
            return GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]));
        }

        public static DateTime GetDateTime(object obj)
        {
            DateTime time;
            if ((obj != DBNull.Value) && DateTime.TryParse(obj.ToString(), out time))
            {
                return time;
            }
            return DateTime.MinValue;
        }

        public static DateTime? GetDateTimeNullable(object obj)
        {
            DateTime time;
            if (obj == DBNull.Value)
            {
                return null;
            }
            if (DateTime.TryParse(obj.ToString(), out time))
            {
                return new DateTime?(time);
            }
            return new DateTime?(DateTime.MinValue);
        }

        public static decimal GetDecimal(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToDecimal(obj);
            }
            return 0M;
        }

        public static double GetDouble(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToDouble(obj);
            }
            return 0.0;
        }

        public static float GetFloat(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToSingle(obj);
            }
            return 0f;
        }

        public static Guid GetGuid(object obj)
        {
            if (obj != DBNull.Value)
            {
                return new Guid(obj.ToString());
            }
            return Guid.Empty;
        }

        public static string GetInParameterSql(IList<string> arr)
        {
            StringBuilder builder = new StringBuilder();
            if (arr.Count == 0)
            {
                builder.Append("''");
            }
            else
            {
                builder.Append("'");
                foreach (string str in arr)
                {
                    builder.Append(str).Append("','");
                }
                builder.Remove(builder.Length - 2, 2);
            }
            return builder.ToString();
        }

        public static int GetInt(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        public static short GetInt16(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToInt16(obj);
            }
            return 0;
        }

        public static long GetLong(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToInt64(obj);
            }
            return 0L;
        }

        public static string GetPivotSql(string[] arr)
        {
            StringBuilder builder = new StringBuilder();
            if (arr.Length != 0)
            {
                foreach (string str in arr)
                {
                    builder.Append(str).Append(",");
                }
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        public static string GetQuerySql(string fieldName, string sql)
        {
            string str = string.Empty;
            // TODO 2、字符问题
            string[] strArray = FilterSpacialString(sql).Split(new char[] { ',', '，' });
            if (strArray.Length != 0)
            {
                StringBuilder builder = new StringBuilder("and (");
                foreach (string str2 in strArray)
                {
                    if (!string.IsNullOrEmpty(str2))
                    {
                        builder.AppendFormat("{0} like '%{1}%' or ", fieldName, str2);
                    }
                }
                if (builder.ToString().EndsWith(" or "))
                {
                    str = builder.Remove(builder.Length - 4, 4).Append(")").ToString();
                }
            }
            return str;
        }

        public static DataTable GetRemark(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT c.name,ep.value FROM sys.columns AS c ");
            builder.Append("JOIN sys.tables AS t ON c.object_id = t.object_id ");
            builder.Append("JOIN sys.extended_properties AS ep ON c.column_id = ep.minor_id ");
            builder.Append("WHERE ep.major_id = t.object_id ");
            builder.Append("AND t.name = @tableName");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tableName", tableName) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static sbyte GetSByte(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToSByte(obj);
            }
            return 0;
        }

        public static SqlDateTime GetSqlDateTime(object obj)
        {
            DateTime time;
            if ((obj != DBNull.Value) && DateTime.TryParse(obj.ToString(), out time))
            {
                return time;
            }
            return SqlDateTime.MinValue;
        }

        public static string GetString(object obj)
        {
            if (obj != null)
            {
                return obj.ToString();
            }
            return string.Empty;
        }

        public static DataTable GetTable(string sql)
        {
            return SqlHelper.ExecuteQuery(CommandType.Text, sql, new SqlParameter[0]);
        }

        public static DataTable GetTable(string tbName, string Condition)
        {
            string cmdText = "select * from " + tbName + " " + Condition;
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public static uint GetUInt(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToUInt32(obj);
            }
            return 0;
        }

        public static ushort GetUInt16(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToUInt16(obj);
            }
            return 0;
        }

        public static ulong GetULong(object obj)
        {
            if (obj != DBNull.Value)
            {
                return Convert.ToUInt64(obj);
            }
            return 0L;
        }
    }
}

