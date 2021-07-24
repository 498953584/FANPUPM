namespace cn.justwin.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;

    /// <summary>
    /// Excel辅助类
    /// </summary>
    public class ExcelUtility
    {
        private string fullName;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fullName">文件名称</param>
        public ExcelUtility(string fullName)
        {
            this.fullName = fullName;
        }

        /// <summary>
        /// 创建连接字符串
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString()
        {
            return ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.fullName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"");
        }

        /// <summary>
        /// 获取行数
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>行数</returns>
        public int GetCount(string tableName)
        {
            return this.GetData(tableName).Rows.Count;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public DataTable GetData(string tableName)
        {
            using (OleDbConnection connection = new OleDbConnection(this.CreateConnectionString()))
            {
                DataTable dataTable = new DataTable();
                OleDbCommand selectCommand = connection.CreateCommand();
                selectCommand.CommandText = "SELECT * FROM [" + tableName + "]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand))
                {
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
        }

        /// <summary>
        /// 返回数据库的列与Excel的对应关系
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static IDictionary<string, int> GetRelation(string[] arr)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 2; i < arr.Length; i++)
            {
                if (!string.IsNullOrEmpty(arr[i]) && (string.Compare(arr[i], "Invalid", true) != 0))
                {
                    dictionary.Add(arr[i], i - 2);
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 获取所有表名称
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public DataTable GetSchema()
        {
            using (OleDbConnection connection = new OleDbConnection(this.CreateConnectionString()))
            {
                DataTable table = new DataTable();
                connection.Open();
                return connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            }
        }

        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                this.fullName = value;
            }
        }
    }
}

