namespace cn.justwin.DAL
{
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;

    public abstract class DB2SqlHelper
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["sql"].ToString();
        public static readonly string db2ConStr = ConfigurationManager.ConnectionStrings["db2Sql"].ToString();

        protected DB2SqlHelper()
        {
        }

        public static int db2ExecuteNonQuery(CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection connection = new OleDbConnection(db2ConStr))
            {
                db2PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return num;
            }
        }

        public static void db2PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, CommandType cmdType, string cmdText, OleDbParameter[] cmdParms)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception exception)
                    {
                        Log4netHelper.Error(exception, "db2error", "db2Openerror");
                    }
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
                    foreach (OleDbParameter parameter in cmdParms)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
            }
            catch (Exception exception2)
            {
                Log4netHelper.Error(exception2, "db2error", "db2Commanderror");
            }
        }

        public static void db2Update()
        {
            string cmdText = " UPDATE ZJIANWEN SET FLAG=1 WHERE FLAG=0 ";
            db2ExecuteNonQuery(CommandType.Text, cmdText, null);
        }

        public static OleDbDataReader ExecuteReader(CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbDataReader reader2;
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(db2ConStr);
            try
            {
                db2PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch (Exception exception)
            {
                conn.Close();
                Log4netHelper.Error(exception, "db2error", "db2Readerror");
                throw;
            }
            return reader2;
        }

        public static void ImportSmTreasury()
        {
            try
            {
                string cmdText = " INSERT INTO Sm_Treasury_Stock(tsid,scode,tcode,sprice,snumber,isfirst,corp,incode,intime,intype,[Type])\r\n                               SELECT  LOWER(newid()),Scode,Tcode,Sprice,Snumber,0,'','',InputDate,0,'O' FROM Sap_JWSmStock \r\n                               WHERE Flag=0 ";
                if (SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null) > 0)
                {
                    UpdateSmStock();
                }
            }
            catch (Exception exception)
            {
                Log4netHelper.Error(exception, "JwError", "JwError");
            }
        }

        public static bool IsExist(string serialNumber)
        {
            bool flag = false;
            string cmdText = string.Format("SELECT COUNT(*) FROM MiddleTable WHERE SerialNumber='{0}'", serialNumber);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null);
            if (obj2 != null)
            {
                flag = Convert.ToInt32(obj2) <= 0;
            }
            return flag;
        }

        public static void MiddleTableImport()
        {
            string cmdText = "SELECT * FROM ZJIANWEN WHERE FLAG=0";
            string str2 = " INSERT INTO MiddleTable VALUES(@PrjCode,@ContractCode,@SerialNumber,\r\n                           @Belnr,@MoneyAmount,@Tag,@Hkont,@Flag,@Buzet,@F_date,@Txt)";
            try
            {
                OleDbDataReader reader = ExecuteReader(CommandType.Text, cmdText, null);
                while (reader.Read())
                {
                    string serialNumber = DBHelper.GetString(reader["SERIAL"]);
                    decimal @decimal = DBHelper.GetDecimal(reader["MONEY"]);
                    if (IsExist(serialNumber))
                    {
                        SqlHelper.ExecuteNonQuery(CommandType.Text, str2, new List<SqlParameter> { new SqlParameter("@PrjCode", DBHelper.GetString(reader["PROJNO"])), new SqlParameter("@ContractCode", DBHelper.GetString(reader["HETONG"])), new SqlParameter("@SerialNumber", serialNumber), new SqlParameter("@Belnr", DBHelper.GetString(reader["BELNR"])), new SqlParameter("@MoneyAmount", DBHelper.GetDecimal(reader["MONEY"])), new SqlParameter("@Tag", DBHelper.GetString(reader["TAG"])), new SqlParameter("@Hkont", DBHelper.GetString(reader["HKONT"])), new SqlParameter("@Flag", DBHelper.GetBool(reader["FLAG"])), new SqlParameter("@Buzet", DBHelper.GetInt(reader["BUZEI"])), new SqlParameter("@F_date", DBHelper.GetDateTimeNullable(reader["F_DATE"])), new SqlParameter("@Txt", DBHelper.GetString(reader["WENBEN"])) }.ToArray());
                    }
                    else
                    {
                        string str4 = string.Format(" UPDATE MiddleTable SET MoneyAmount= '{0}' WHERE SerialNumber='{1}'", @decimal, serialNumber);
                        SqlHelper.ExecuteNonQuery(CommandType.Text, str4, null);
                    }
                }
                reader.Close();
                reader.Dispose();
                db2Update();
            }
            catch (Exception exception)
            {
                Log4netHelper.Error(exception, "db2error", "db2error");
            }
        }

        public static void SapSmStockImport()
        {
            string cmdText = " SELECT * FROM ZJIANWEN_KC WHERE FLAG=0";
            string str2 = "  INSERT INTO Sap_JWSmStock VALUES(@PrjCode,@Tcode,@Scode,\r\n                           @Snumber,@Sprice,@InputDate,@Flag )";
            try
            {
                OleDbDataReader reader = ExecuteReader(CommandType.Text, cmdText, null);
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    SqlHelper.ExecuteNonQuery(CommandType.Text, str2, new List<SqlParameter> { new SqlParameter("@PrjCode", DBHelper.GetString(reader["PROJNO"])), new SqlParameter("@Tcode", DBHelper.GetString(reader["LGORT"])), new SqlParameter("@Scode", DBHelper.GetString(reader["MATNR"])), new SqlParameter("@Snumber", DBHelper.GetString(reader["MENGE"])), new SqlParameter("@Sprice", DBHelper.GetDecimal(reader["MONEY"])), new SqlParameter("@InputDate", DBHelper.GetDateTimeNullable(reader["F_DATE"])), new SqlParameter("@Flag", DBHelper.GetBool(reader["FLAG"])) }.ToArray());
                }
                reader.Close();
                reader.Dispose();
                SapUpdate();
            }
            catch (Exception exception)
            {
                Log4netHelper.Error(exception, "SapError", "SapError");
            }
        }

        public static void SapUpdate()
        {
            string cmdText = " UPDATE ZJIANWEN_KC SET FLAG=1 WHERE FLAG=0 ";
            db2ExecuteNonQuery(CommandType.Text, cmdText, null);
        }

        public static void UpdateSmStock()
        {
            string cmdText = " UPDATE Sap_JWSmStock SET Flag=1 WHERE Flag=0 ";
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
        }
    }
}

