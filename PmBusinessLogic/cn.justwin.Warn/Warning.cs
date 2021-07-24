namespace cn.justwin.Warn
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Warning
    {
        public static void AddWarning(string title, string content, string userCode, string dbTable, string dbColumn, string key, string uri)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("INSERT INTO PT_Warning(WarningTitle,WarningContent,UserCode,RelationsTable,RelationsColumn, ");
            builder.AppendFormat(" RelationsKey,URI,IsValid) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','1') ", new object[] { title, content, userCode, dbTable, dbColumn, key, uri });
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public static IList<string> GetKeysByTable(string tableName)
        {
            IList<string> list = new List<string>();
            string str = string.Format("SELECT * FROM PT_Warning WHERE RelationsTable='{0}'", tableName);
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, str.ToString()).Rows)
            {
                list.Add(row["RelationsKey"].ToString());
            }
            return list;
        }

        public static DataTable GetWarningList(string usercode, string title, string content, int pageNo, int pagesize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" \r\n\t\t\t\tSELECT * FROM (\r\n\t\t\t\t\tSELECT ROW_NUMBER() OVER (ORDER BY InputDate DESC) AS Num,* \r\n\t\t\t\tFROM PT_Warning  WHERE UserCode='{0}' AND IsOpened = 0 ", usercode);
            if (!string.IsNullOrEmpty(title))
            {
                builder.AppendFormat(" and WarningTitle like '%{0}%' ", title);
            }
            if (!string.IsNullOrEmpty(content))
            {
                builder.AppendFormat(" and WarningContent like '%{0}%' ", content);
            }
            int num = ((pageNo - 1) * pagesize) + 1;
            int num2 = pageNo * pagesize;
            builder.AppendFormat(" ) AS T WHERE T.Num BETWEEN {0} AND {1} ", num, num2);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static int GetWarningListCount(string usercode, string title, string content)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT COUNT(*) FROM PT_Warning WHERE UserCode='{0}' AND IsOpened = 0 ", usercode);
            if (!string.IsNullOrEmpty(title))
            {
                builder.AppendFormat(" and WarningTitle like '%{0}%' ", title);
            }
            if (!string.IsNullOrEmpty(content))
            {
                builder.AppendFormat(" and WarningContent like '%{0}%' ", content);
            }
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()).ToString());
        }

        public static void MendingWarning()
        {
            if (DateTime.Now.Hour == 0)
            {
                string cmdText = "EXECUTE uspProgressWarning";
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, cmdText);
            }
        }

        public static void Remove(string key, string userCode)
        {
            string cmdText = string.Format("UPDATE PT_Warning SET IsValid =0 \r\n\t\t\t\t\t\t\t\t\t\t\tWHERE RelationsKey = '{0}' AND UserCode = '{1}' ", key, userCode);
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText);
        }

        public static void UpdateValid(string warningId)
        {
            string cmdText = "UPDATE PT_Warning SET IsValid =0 WHERE WarningId=@warningId";
            SqlParameter parameter = new SqlParameter("@warningId", warningId);
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public static void UpdateValid(string relationsKey, string allocationType)
        {
            string cmdText = "UPDATE PT_Warning SET IsValid =0 WHERE WarningTitle=@title AND RelationsKey=@relationsKey ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar, 200), new SqlParameter("@relationsKey", SqlDbType.NVarChar, 200) };
            if (allocationType == "out")
            {
                commandParameters[0].Value = "调出提醒";
            }
            else
            {
                commandParameters[0].Value = "接收提醒";
            }
            commandParameters[1].Value = relationsKey;
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
    }
}

