namespace com.jwsoft.pm.data
{
    using com.jwsoft.common.data;
    using com.jwsoft.common.EncryptDog;
    using com.jwsoft.pm.entpm.Model;
    using com.jwsoft.sysManage.PageOrder;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    public class publicDbOpClass
    {
        public static int Add(ConstructionLogModel mdl)
        {
            object obj2 = (((((((((("" + "insert into pm_Construction_Log(" + "logID,ProjectId,code,part,attendance,temperature,amweather,pmweather,operations,thisDate,daycontent,design,acceptance,beton,datum,product,situation,remark") + ")" + " values (") + "'" + mdl.logID + "',") + mdl.ProjectId + ",") + "'" + mdl.code + "',") + "'" + mdl.part + "',") + mdl.attendance + ",") + "'" + mdl.temperature + "',") + "'" + mdl.amweather + "',") + "'" + mdl.pmweather + "',") + "'" + mdl.operations + "',";
            return ExecSqlString((((((((((string.Concat(new object[] { obj2, "'", mdl.thisDate, "'," }) + "'" + mdl.daycontent + "',") + "'" + mdl.design + "',") + "'" + mdl.acceptance + "',") + "'" + mdl.beton + "',") + "'" + mdl.datum + "',") + "'" + mdl.product + "',") + "'" + mdl.situation + "',") + "'" + mdl.remark + "'") + ")").ToString());
        }

        public static int Add(ConstructionLogModel mdl, string prjectId)
        {
            object obj2 = (((((((((("" + "insert into pm_Construction_Log(" + "logID,ProjectId,code,part,attendance,temperature,amweather,pmweather,operations,thisDate,daycontent,design,acceptance,beton,datum,product,situation,remark") + ")" + " values (") + "'" + mdl.logID + "',") + "'" + prjectId.Trim() + "',") + "'" + mdl.code + "',") + "'" + mdl.part + "',") + mdl.attendance + ",") + "'" + mdl.temperature + "',") + "'" + mdl.amweather + "',") + "'" + mdl.pmweather + "',") + "'" + mdl.operations + "',";
            return ExecSqlString((((((((((string.Concat(new object[] { obj2, "'", mdl.thisDate, "'," }) + "'" + mdl.daycontent + "',") + "'" + mdl.design + "',") + "'" + mdl.acceptance + "',") + "'" + mdl.beton + "',") + "'" + mdl.datum + "',") + "'" + mdl.product + "',") + "'" + mdl.situation + "',") + "'" + mdl.remark + "'") + ")").ToString());
        }

        public static DataSet DataSetQuary(string SqlString)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            try
            {
                adapter.SelectCommand = new SqlCommand(SqlString, connection);
                adapter.Fill(dataSet);
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
            }
            return dataSet;
        }

        public static DataTable DataTableQuary(string SqlString)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            DataTable dataTable = new DataTable();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            try
            {
                new SqlDataAdapter { SelectCommand = new SqlCommand(SqlString, connection) }.Fill(dataTable);
            }
            catch (SqlException exception)
            {
                connection.Close();
                throw exception;
            }
            return dataTable;
        }

        public static DataTable DataTableQueryNoEncryptDog(string sql)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            try
            {
                new SqlDataAdapter { SelectCommand = new SqlCommand(sql, connection) }.Fill(dataTable);
            }
            catch (SqlException exception)
            {
                connection.Close();
                throw exception;
            }
            return dataTable;
        }

        public static int Delete(string logID)
        {
            string str = "";
            return ExecSqlString(((str + "delete pm_Construction_Log ") + " where logID='" + logID + "'").ToString());
        }

        public static int ExecSqlString(string SqlString)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            bool flag = false;
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = new SqlCommand {
                Connection = connection,
                Transaction = transaction,
                CommandText = SqlString
            };
            int number = 0;
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
                flag = true;
            }
            catch (SqlException exception)
            {
                transaction.Rollback();
                flag = false;
                number = exception.Number;
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
            if (flag)
            {
                return 1;
            }
            return number;
        }

        public static DataSet ExecuteDataSet(string spName, SqlParameter[] commandParameters)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            try
            {
                command.Connection = connection;
                command.CommandText = spName;
                command.CommandType = CommandType.StoredProcedure;
                if (commandParameters != null)
                {
                    foreach (SqlParameter parameter in commandParameters)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
                command.Parameters.Clear();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                adapter.Dispose();
                command.Dispose();
                connection.Close();
            }
            return dataSet;
        }

        public static DataTable ExecuteDataTable(string spName, SqlParameter[] commandParameters)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            try
            {
                command.Connection = connection;
                command.CommandText = spName;
                command.CommandType = CommandType.StoredProcedure;
                if (commandParameters != null)
                {
                    foreach (SqlParameter parameter in commandParameters)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
                command.Parameters.Clear();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                adapter.Dispose();
                command.Dispose();
                connection.Close();
            }
            return dataTable;
        }

        public static DataTable ExecuteDataTable(CommandType cmdType, string spName, SqlParameter[] commandParameters)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            try
            {
                command.Connection = connection;
                command.CommandText = spName;
                command.CommandType = cmdType;
                if (commandParameters != null)
                {
                    foreach (SqlParameter parameter in commandParameters)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
                command.Parameters.Clear();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                adapter.Dispose();
                command.Dispose();
                connection.Close();
            }
            return dataTable;
        }

        public static int ExecuteNonQuery(CommandType commandType, string spName, SqlParameter[] commandParameters)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand {
                Connection = connection,
                CommandText = spName,
                CommandType = commandType
            };
            int num = 0;
            if (commandParameters != null)
            {
                foreach (SqlParameter parameter in commandParameters)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                num = command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            return num;
        }

        public static void ExecuteProcedure(string spName)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand {
                Connection = connection,
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("执行存储过程出错，请检查数据库连接和相关参数！");
            }
            finally
            {
                connection.Close();
            }
        }

        public static void ExecuteProcedure(string spName, SqlParameter[] commandParameters)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand {
                Connection = connection,
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };
            if (commandParameters != null)
            {
                foreach (SqlParameter parameter in commandParameters)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
        }

        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlDataReader reader2;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new Conn().SqlConnectionSystem();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch
            {
                conn.Close();
                throw;
            }
            return reader2;
        }

        public static object ExecuteScalar(string SqlString)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            object obj2 = new object();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand(SqlString) {
                Connection = connection
            };
            try
            {
                obj2 = command.ExecuteScalar();
            }
            catch (SqlException exception)
            {
                obj2 = DBNull.Value;
                throw exception;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
            return obj2;
        }

        public static object ExecuteScalar(string spName, SqlParameter[] commandParameters)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            object obj2 = new object();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand {
                Connection = connection,
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };
            if (commandParameters != null)
            {
                foreach (SqlParameter parameter in commandParameters)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                obj2 = command.ExecuteScalar();
                command.Parameters.Clear();
            }
            catch (SqlException exception)
            {
                obj2 = DBNull.Value;
                throw exception;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
            return obj2;
        }

        public static bool ExecuteSQL(string[] sqlString)
        {
            bool flag = true;
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand();
            SqlTransaction transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            try
            {
                foreach (string str in sqlString)
                {
                    if (str.Trim().Length != 0)
                    {
                        command.CommandText = str;
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
            catch
            {
                flag = false;
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }

        public static int ExecuteSQL(string sqlString)
        {
            int num = -1;
            SqlConnection connection = new Conn().SqlConnectionSystem();
            try
            {
                num = new SqlCommand(sqlString, connection).ExecuteNonQuery();
            }
            catch
            {
                num = -1;
            }
            finally
            {
                connection.Close();
            }
            return num;
        }

        public static int Exist(string code, string PrjCode)
        {
            string str = "";
            string str2 = str + "select count(*) from pm_Construction_Log where code='";
            return int.Parse(ExecuteScalar((str2 + code + "' and ProjectId='" + PrjCode.Trim() + "' ").ToString()).ToString());
        }

        public static void FillDataSet(DataSet data, string SqlString, string TableName)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlConnection connection = new Conn().SqlConnectionSystem();
            try
            {
                adapter.SelectCommand = new SqlCommand(SqlString, connection);
                adapter.Fill(data, TableName);
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
            }
        }

        public static DataTable GetList(string pmId)
        {
            string str = "";
            return DataTableQuary((((str + "select * from pm_Construction_Log ") + "where" + pmId) + "order by thisDate desc").ToString());
        }

        public static ConstructionLogModel GetModel(string logID)
        {
            string str = "";
            str = (str + "select * from pm_Construction_Log ") + " where logID='" + logID + "'";
            ConstructionLogModel model = new ConstructionLogModel();
            DataSet set = DataSetQuary(str.ToString());
            model.logID = logID;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ProjectId"].ToString() != "")
            {
                model.ProjectId = int.Parse(set.Tables[0].Rows[0]["ProjectId"].ToString());
            }
            model.code = set.Tables[0].Rows[0]["code"].ToString();
            model.part = set.Tables[0].Rows[0]["part"].ToString();
            if (set.Tables[0].Rows[0]["attendance"].ToString() != "")
            {
                model.attendance = int.Parse(set.Tables[0].Rows[0]["attendance"].ToString());
            }
            model.temperature = set.Tables[0].Rows[0]["temperature"].ToString();
            model.amweather = set.Tables[0].Rows[0]["amweather"].ToString();
            model.pmweather = set.Tables[0].Rows[0]["pmweather"].ToString();
            model.operations = set.Tables[0].Rows[0]["operations"].ToString();
            if (set.Tables[0].Rows[0]["thisDate"].ToString() != "")
            {
                model.thisDate = DateTime.Parse(set.Tables[0].Rows[0]["thisDate"].ToString());
            }
            model.daycontent = set.Tables[0].Rows[0]["daycontent"].ToString();
            model.design = set.Tables[0].Rows[0]["design"].ToString();
            model.acceptance = set.Tables[0].Rows[0]["acceptance"].ToString();
            model.beton = set.Tables[0].Rows[0]["beton"].ToString();
            model.datum = set.Tables[0].Rows[0]["datum"].ToString();
            model.product = set.Tables[0].Rows[0]["product"].ToString();
            model.situation = set.Tables[0].Rows[0]["situation"].ToString();
            model.remark = set.Tables[0].Rows[0]["remark"].ToString();
            return model;
        }

        public static DataTable GetPageData(string SqlWhere, string TableName)
        {
            if (SqlWhere.Trim() != "")
            {
                SqlWhere = " where " + SqlWhere;
            }
            return DataTableQuary("select * from " + TableName + SqlWhere);
        }

        public static DataTable GetPageData(string SqlWhere, string TableName, string strsort)
        {
            if (SqlWhere.Trim() != "")
            {
                SqlWhere = " where " + SqlWhere;
            }
            if (strsort.Trim() != "")
            {
                SqlWhere = SqlWhere + " ORDER BY   " + strsort;
            }
            return DataTableQuary("select * from " + TableName + SqlWhere);
        }

        public static DataTable GetQuery(string code, string part, string operations, string pmid, string workdate)
        {
            string str = "";
            str = str + "select * from pm_Construction_Log where 1=1 ";
            string str2 = "";
            if (code.Trim() != "")
            {
                str2 = str2 + " and  code like '%" + code + "%'";
            }
            if ((part.Trim() != "") && (str2 == ""))
            {
                str2 = str2 + " and  part like '%" + part + "%'";
            }
            else if ((part.Trim() != "") && (str2 != ""))
            {
                str2 = str2 + " and  part like '%" + part + "%'";
            }
            if ((operations.Trim() != "") && (str2 == ""))
            {
                str2 = str2 + " and operations like '%" + operations + "%'";
            }
            else if ((operations.Trim() != "") && (str2 != ""))
            {
                str2 = str2 + " and operations like '%" + operations + "%'";
            }
            if (str2 == "")
            {
                str2 = str2 + " and projectid ='" + pmid + "'";
            }
            else
            {
                str2 = str2 + " and projectid ='" + pmid + "'";
            }
            if (!string.IsNullOrEmpty(workdate))
            {
                object obj2 = str2;
                str2 = string.Concat(new object[] { obj2, " and thisDate='", Convert.ToDateTime(workdate), "'" });
            }
            return DataTableQuary((str + str2 + " order by thisDate desc").ToString());
        }

        public static int GetRecordCount(string strTableName, string strWhere)
        {
            int num;
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tblName", strTableName), new SqlParameter("@fldName", ""), new SqlParameter("@PageSize", 1), new SqlParameter("@PageIndex", 1), new SqlParameter("@IsCount", 1), new SqlParameter("@OrderType", ""), new SqlParameter("@strWhere", strWhere) };
            try
            {
                num = Convert.ToInt32(ExecuteDataTable("P_GetRecordFromPage", commandParameters).Rows[0][0].ToString());
            }
            catch
            {
                throw new Exception("查询数据库出错，请检查参数信息！");
            }
            return num;
        }

        public static DataTable GetRecordFromPage(string strTableName, PageOrderInfo onePageOrderObject, int iPageSize, int iPageIndex, string strWhere)
        {
            DataTable table;
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tblName", strTableName), new SqlParameter("@orderstring", onePageOrderObject.GetOrderString()), new SqlParameter("@PageSize", iPageSize), new SqlParameter("@PageIndex", iPageIndex), new SqlParameter("@IsCount", SqlDbType.BigInt), new SqlParameter("@OrderType", onePageOrderObject.GetMainFiledOrderType()), new SqlParameter("@strWhere", strWhere), new SqlParameter("@mainfldName", onePageOrderObject.GetMainOrderField()) };
            try
            {
                table = ExecuteDataTable("P_GetRecordFromPageMutiOrderTest", commandParameters);
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return table;
        }

        public static DataTable GetRecordFromPage(ref int pgCount, string strSql, string pkName, int pgSize, int pgIndex)
        {
            DataTable table2;
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            strSql = Regex.Replace(strSql, @"\s+", " ");
            SqlParameter parameter = new SqlParameter("@pgCount", (int) pgCount) {
                DbType = DbType.Int32,
                Direction = ParameterDirection.InputOutput
            };
            SqlParameter[] commandParameters = new SqlParameter[] { parameter, new SqlParameter("@SQL", strSql), new SqlParameter("@pkName", pkName), new SqlParameter("@pgSize", pgSize), new SqlParameter("@pgIdx", pgIndex) };
            try
            {
                DataTable table = ExecuteDataTable("Prj_sp_DataPager", commandParameters);
                pgCount = (int) parameter.Value;
                table2 = table;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetRecordFromPage(string strTableName, string strFieldName, int iPageSize, int iPageIndex, int iOrderType, string strWhere)
        {
            DataTable table;
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            int num = 0;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tblName", strTableName), new SqlParameter("@fldName", strFieldName), new SqlParameter("@PageSize", iPageSize), new SqlParameter("@PageIndex", iPageIndex), new SqlParameter("@IsCount", num), new SqlParameter("@OrderType", iOrderType), new SqlParameter("@strWhere", strWhere) };
            try
            {
                table = ExecuteDataTable("P_GetRecordFromPage", commandParameters);
            }
            catch
            {
                throw new Exception("查询数据库出错，请检查参数信息！");
            }
            return table;
        }

        public static bool Insert(string TableName, Hashtable Cols)
        {
            int num = 0;
            if (Cols.Count <= 0)
            {
                return true;
            }
            string str = "(";
            string str2 = "Values(";
            foreach (DictionaryEntry entry in Cols)
            {
                if (num != 0)
                {
                    str = str + ",";
                    str2 = str2 + ",";
                }
                str = str + "[" + entry.Key.ToString() + "]";
                str2 = str2 + entry.Value.ToString();
                num++;
            }
            str = str + ")";
            str2 = str2 + ")";
            string str3 = "Insert Into " + TableName + str + str2;
            return ExecuteSQL(new string[] { str3 });
        }

        public object Insert(string TableName, Hashtable Cols, string select)
        {
            int num = 0;
            if (Cols.Count <= 0)
            {
                return true;
            }
            string str = "(";
            string str2 = "Values(";
            foreach (DictionaryEntry entry in Cols)
            {
                if (num != 0)
                {
                    str = str + ",";
                    str2 = str2 + ",";
                }
                str = str + "[" + entry.Key.ToString() + "]";
                str2 = str2 + entry.Value.ToString();
                num++;
            }
            str = str + ")";
            str2 = str2 + ")";
            return ExecuteScalar("Insert Into " + TableName + str + str2 + select);
        }

        public static bool NonQuerySqlString(string SqlString)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            bool flag = false;
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = new SqlCommand {
                Connection = connection,
                Transaction = transaction,
                CommandText = SqlString
            };
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
                flag = true;
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                flag = false;
                throw exception;
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
            return flag;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public static string QuaryMaxid(string tablename, string fieldname)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            Conn conn = new Conn();
            string cmdText = "";
            string str2 = "";
            cmdText = "select max(" + fieldname + ") as maxid from " + tablename;
            SqlConnection connection = conn.SqlConnectionSystem();
            SqlCommand command = new SqlCommand(cmdText) {
                Connection = connection
            };
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            str2 = reader["maxid"].ToString().Trim();
            reader.Close();
            command.Dispose();
            connection.Close();
            if (str2.Length == 0)
            {
                str2 = "0";
            }
            return str2;
        }

        public static string QuaryMaxid(string tablename, string fieldname, string where)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            Conn conn = new Conn();
            string cmdText = "";
            string str2 = "";
            cmdText = "select max(" + fieldname + ") as maxid from " + tablename + where;
            SqlConnection connection = conn.SqlConnectionSystem();
            SqlCommand command = new SqlCommand(cmdText) {
                Connection = connection
            };
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            str2 = reader["maxid"].ToString().Trim();
            reader.Close();
            command.Dispose();
            connection.Close();
            if (str2.Length == 0)
            {
                str2 = "0";
            }
            return str2;
        }

        public static DataRow QueryDataRow(string sqlString)
        {
            new com.jwsoft.common.EncryptDog.EncryptDog().IsAuthorization();
            DataSet set = DataSetQuary(sqlString);
            set.CaseSensitive = false;
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0];
            }
            return null;
        }

        public static int Update(ConstructionLogModel mdl)
        {
            object obj2 = (("" + "update pm_Construction_Log set ") + "code='" + mdl.code + "',") + "part='" + mdl.part + "',";
            object obj3 = (((string.Concat(new object[] { obj2, "attendance=", mdl.attendance, "," }) + "temperature='" + mdl.temperature + "',") + "amweather='" + mdl.amweather + "',") + "pmweather='" + mdl.pmweather + "',") + "operations='" + mdl.operations + "',";
            return ExecSqlString((((((((((string.Concat(new object[] { obj3, "thisDate='", mdl.thisDate, "'," }) + "daycontent='" + mdl.daycontent + "',") + "design='" + mdl.design + "',") + "acceptance='" + mdl.acceptance + "',") + "beton='" + mdl.beton + "',") + "datum='" + mdl.datum + "',") + "product='" + mdl.product + "',") + "situation='" + mdl.situation + "',") + "remark='" + mdl.remark + "'") + " where logID='" + mdl.logID + "'").ToString());
        }

        public static bool Update(string TableName, Hashtable Cols, string Where)
        {
            int num = 0;
            if (Cols.Count <= 0)
            {
                return true;
            }
            string str = "";
            foreach (DictionaryEntry entry in Cols)
            {
                if (num != 0)
                {
                    str = str + ",";
                }
                str = str + "[" + entry.Key.ToString() + "]";
                str = str + "=";
                str = str + entry.Value.ToString();
                num++;
            }
            str = str + " ";
            string str2 = "Update " + TableName + " set " + str + Where;
            return ExecuteSQL(new string[] { str2 });
        }
    }
}

