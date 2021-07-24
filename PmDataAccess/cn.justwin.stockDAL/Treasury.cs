namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Treasury
    {
        public int Add(TreasuryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Treasury(");
            builder.Append("tid,tcode,tname,ptcode,tflag,taddress,texplain,prjCode,isContrast)");
            builder.Append(" values (");
            builder.Append("@tid,@tcode,@tname,@ptcode,@tflag,@taddress,@texplain,@prjCode,@isContrast)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tid", SqlDbType.NVarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@tname", SqlDbType.NVarChar, 0x200), new SqlParameter("@ptcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@tflag", SqlDbType.NVarChar, 0x40), new SqlParameter("@taddress", SqlDbType.NVarChar, 0x400), new SqlParameter("@texplain", SqlDbType.NVarChar, 0x800), new SqlParameter("@prjCode", SqlDbType.VarChar, 0x1388), new SqlParameter("@isContrast", SqlDbType.Bit) };
            commandParameters[0].Value = model.tid;
            commandParameters[1].Value = model.tcode;
            commandParameters[2].Value = model.tname;
            commandParameters[3].Value = model.ptcode;
            commandParameters[4].Value = model.tflag;
            commandParameters[5].Value = model.taddress;
            commandParameters[6].Value = model.texplain;
            if (string.IsNullOrEmpty(model.PrjCode))
            {
                commandParameters[7].Value = DBNull.Value;
            }
            else
            {
                commandParameters[7].Value = model.PrjCode;
            }
            if (model.IsContrast.HasValue)
            {
                commandParameters[8].Value = model.IsContrast;
            }
            else
            {
                commandParameters[8].Value = DBNull.Value;
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int AddRoot()
        {
            string str = Guid.NewGuid().ToString();
            string cmdText = string.Format("insert into SM_Treasury(tid,tcode,tname) values('{0}','0','仓库名称')", str);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
        }

        public void CancelTotal()
        {
            string cmdText = "UPDATE Sm_Treasury SET tflag = '0' WHERE tflag = '1'";
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public void DelContrast()
        {
            string cmdText = "UPDATE Sm_Treasury SET IsContrast=null WHERE TID=(SELECT TID FROM Sm_Treasury WHERE IsContrast=1)";
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public int Delete(string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Treasury ");
            builder.Append(" where tcode=@tcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tcode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = tcode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string[] arrTcode)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString);
            connection.Open();
            SqlTransaction trans = connection.BeginTransaction();
            string cmdText = string.Empty;
            try
            {
                foreach (string str2 in arrTcode)
                {
                    cmdText = string.Format("delete from Sm_Treasury where tcode={0}", str2);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, null);
                    num++;
                }
                trans.Commit();
            }
            catch
            {
                num = 0;
                trans.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return num;
        }

        public DataTable GetChildData(string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select row_number() over(order by tcode) as rowNumber, tcode, tname, ");
            builder.Append("case tflag ");
            builder.Append("    when '0' then '分库' ");
            builder.Append("    when '1' then '总库' ");
            builder.Append("    else '' ");
            builder.Append("end\tas tflag, ");
            builder.Append("case IsContrast ");
            builder.Append("    when '1' then '对比库' ");
            builder.Append("    else '非对比库' ");
            builder.Append("end\tas IsContrast, ");
            builder.Append("taddress, texplain, prjCode");
            builder.Append(" from SM_Treasury ");
            builder.Append("where ptcode = @ptcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ptcode", SqlDbType.NVarChar, 0x200) };
            commandParameters[0].Value = tcode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int GetChildNodesLength(string tcode)
        {
            string cmdText = "select count(*) from Sm_Treasury where ptcode = @ptcode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ptcode", SqlDbType.NVarChar, 0x200) };
            commandParameters[0].Value = tcode;
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public string GetContrastTreasuryCode()
        {
            string cmdText = "SELECT tcode FROM Sm_Treasury WHERE IsContrast=1";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[0]))
            {
                if (reader.Read())
                {
                    return reader[0].ToString();
                }
                return null;
            }
        }

        public int GetCount()
        {
            string cmdText = "select count(*) from Sm_Treasury";
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null));
        }

        public List<TreasuryModel> GetList()
        {
            string cmdText = "SELECT * FROM Sm_Treasury";
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, null))
            {
                List<TreasuryModel> list = new List<TreasuryModel>();
                while (reader.Read())
                {
                    list.Add(this.GetModel(reader));
                }
                return list;
            }
        }

        public List<TreasuryModel> GetList(string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Sm_Treasury ").Append("where ").Append(condition);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                List<TreasuryModel> list = new List<TreasuryModel>();
                while (reader.Read())
                {
                    list.Add(this.GetModel(reader));
                }
                return list;
            }
        }

        public string GetMaxChildLastThreeCode(string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top(1) right(tcode,3) from Sm_Treasury ");
            builder.Append("where ptcode = @ptcode ");
            builder.Append("order by tcode desc");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ptcode", SqlDbType.NVarChar, 0x200) };
            commandParameters[0].Value = tcode;
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters));
        }

        private TreasuryModel GetModel(IDataReader dr)
        {
            return new TreasuryModel { tid = DBHelper.GetString(dr["tid"]), tcode = DBHelper.GetString(dr["tcode"]), tname = DBHelper.GetString(dr["tname"]), ptcode = DBHelper.GetString(dr["ptcode"]), tflag = DBHelper.GetString(dr["tflag"]), taddress = DBHelper.GetString(dr["taddress"]), texplain = DBHelper.GetString(dr["texplain"]), PrjCode = DBHelper.GetString(dr["prjCode"]), IsContrast = new bool?(DBHelper.GetBool(dr["IsContrast"])) };
        }

        public TreasuryModel GetModel(string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Sm_Treasury ");
            builder.Append(" where tcode=@tcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tcode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = tcode;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public string GetParallel()
        {
            string cmdText = "select tcode from Sm_Treasury where isContrast = '1'";
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null));
        }

        public DataTable GetSameModel(string strName)
        {
            string cmdText = string.Empty;
            cmdText = "SELECT * FROM Sm_Treasury WHERE tname ='" + strName + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public string GetTcodeByPrjcode(string prjCode)
        {
            DataTable treeViewDataSource = this.GetTreeViewDataSource();
            if (treeViewDataSource.Rows.Count > 0)
            {
                DataRow[] rowArray = treeViewDataSource.Select(string.Format("prjCode = '{0}'", prjCode));
                if ((rowArray != null) && (rowArray.Length > 0))
                {
                    return rowArray[0]["tcode"].ToString();
                }
            }
            return string.Empty;
        }

        public string GetTotalCode()
        {
            string cmdText = "select tcode from Sm_Treasury where tflag = '1'";
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null));
        }

        public TreasuryModel GetTotalTreasury()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Sm_Treasury WHERE TFlag=1 ");
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public DataTable GetTreasury(string userCode, bool isLimitPermit)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                    SELECT tid,tcode,tname,ptcode,\r\n                    (\r\n                    SELECT COUNT(v_yhdm) FROM PT_yhmc AS p1 \r\n                    INNER JOIN dbo.Sm_Treasury_Permit AS p2 \r\n                    ON (p2.ptype='Department' AND p2.pcode=p1.i_bmdm)\r\n                    OR (p2.ptype='post' AND p2.pcode=p1.I_DUTYID)\r\n                    OR (p2.ptype='person' AND p2.pcode=p1.v_yhdm)\r\n                    WHERE p2.tcode=T1.tcode ");
            if (isLimitPermit)
            {
                builder.Append(" AND p1.v_yhdm=@userCode ");
            }
            builder.Append("\r\n                    ) AS permit\r\n                    FROM Sm_Treasury AS T1");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetTreeViewDataSource()
        {
            string cmdText = "select tid,tcode,tname,ptcode,prjCode from SM_Treasury";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public TreasuryModel SetTotalTreasuryByMinTcode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" \r\n                BEGIN \r\n\t                DECLARE @TCode nvarchar(50); \r\n\t                SELECT @TCode=MIN(Tcode) FROM Sm_Treasury WHERE TCode<>'0' AND PrjCode IS NULL \r\n\t                UPDATE Sm_Treasury SET TFlag=1 WHERE TCode=@TCode \r\n\t                SELECT * FROM Sm_Treasury WHERE TCode=@TCode \r\n                END \r\n            ");
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public int Update(TreasuryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Treasury set ");
            builder.Append("tid=@tid,");
            builder.Append("tname=@tname,");
            builder.Append("ptcode=@ptcode,");
            builder.Append("tflag=@tflag,");
            builder.Append("taddress=@taddress,");
            builder.Append("texplain=@texplain,");
            builder.Append("prjCode=@prjCode,");
            builder.Append("isContrast=@isContrast");
            builder.Append(" where tcode=@tcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tid", SqlDbType.NVarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@tname", SqlDbType.NVarChar, 0x200), new SqlParameter("@ptcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@tflag", SqlDbType.NVarChar, 0x40), new SqlParameter("@taddress", SqlDbType.NVarChar, 0x400), new SqlParameter("@texplain", SqlDbType.NVarChar, 0x800), new SqlParameter("@prjCode", SqlDbType.VarChar, 0x1388), new SqlParameter("@isContrast", SqlDbType.Bit) };
            commandParameters[0].Value = model.tid;
            commandParameters[1].Value = model.tcode;
            commandParameters[2].Value = model.tname;
            if (string.IsNullOrEmpty(model.ptcode))
            {
                commandParameters[3].Value = DBNull.Value;
            }
            else
            {
                commandParameters[3].Value = model.ptcode;
            }
            commandParameters[4].Value = model.tflag;
            commandParameters[5].Value = model.taddress;
            commandParameters[6].Value = model.texplain;
            if (string.IsNullOrEmpty(model.PrjCode))
            {
                commandParameters[7].Value = DBNull.Value;
            }
            else
            {
                commandParameters[7].Value = model.PrjCode;
            }
            if (model.IsContrast.HasValue)
            {
                commandParameters[8].Value = model.IsContrast;
            }
            else
            {
                commandParameters[8].Value = DBNull.Value;
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

