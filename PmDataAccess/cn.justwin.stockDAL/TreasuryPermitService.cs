namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TreasuryPermitService
    {
        public int AddTreasuryPermit(TreasuryPermit model)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString);
            connection.Open();
            SqlTransaction trans = connection.BeginTransaction();
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("insert into Sm_Treasury_Permit(");
                builder.Append("tpid,tcode,ptype,pcode)");
                builder.Append(" values (");
                builder.Append("@tpid,@tcode,@ptype,@pcode)");
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tpid", SqlDbType.NVarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@ptype", SqlDbType.NVarChar, 0x40), new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40) };
                commandParameters[0].Value = model.tpid;
                commandParameters[1].Value = model.tcode;
                commandParameters[2].Value = model.ptype;
                commandParameters[3].Value = model.pcode;
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                connection.Close();
            }
            connection.Close();
            return num;
        }

        public int DelTreasuryPermitById(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Treasury_Permit ");
            builder.Append(" where tpid=@tpid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tpid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = id;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DelTreasuryPermitByTcode(string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Treasury_Permit ");
            builder.Append(" where tcode=@tcode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200) };
            commandParameters[0].Value = tcode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public IList<TreasuryPermit> GetAllTreasuryPermitByWhere(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select tpid,tcode,ptype,pcode from Sm_Treasury_Permit " + where);
            IList<TreasuryPermit> list = new List<TreasuryPermit>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.GetTreasuryPermitByReader(reader));
                }
            }
            return list;
        }

        public TreasuryPermit GetTreasuryPermitById(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 tpid,tcode,ptype,pcode from Sm_Treasury_Permit ");
            builder.Append(" where tpid=@tpid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tpid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = id;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    return this.GetTreasuryPermitByReader(reader);
                }
            }
            return null;
        }

        private TreasuryPermit GetTreasuryPermitByReader(SqlDataReader reader)
        {
            return new TreasuryPermit { pcode = Convert.ToString(reader["pcode"]), ptype = Convert.ToString(reader["pType"]), tcode = Convert.ToString(reader["tcode"]), tpid = Convert.ToString(reader["tpid"]) };
        }

        public bool IsPermitAccept(string allocCode, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Sm_Allocation").AppendLine();
            builder.Append("WHERE acode = @AllocCode AND InAllocationPerson = @InPerson;").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AllocCode", allocCode), new SqlParameter("@InPerson", userCode) };
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public bool IsPermitBool(string tcode, string yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from PT_yhmc as p1 ");
            builder.Append(" inner join dbo.Sm_Treasury_Permit as p2 ");
            builder.Append(" on (p2.ptype='Department' and p2.pcode=p1.i_bmdm)");
            builder.Append(" or (p2.ptype='post' and p2.pcode=p1.I_DUTYID)");
            builder.Append(" or (p2.ptype='person' and p2.pcode=p1.v_yhdm)");
            builder.Append(" where tcode='" + tcode + "' and p1.v_yhdm='" + yhdm + "'");
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[0])) == 0)
            {
                return false;
            }
            return true;
        }

        public int UpdateTreasuryPermit(TreasuryPermit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Treasury_Permit set ");
            builder.Append("tcode=@tcode,");
            builder.Append("ptype=@ptype,");
            builder.Append("pcode=@pcode");
            builder.Append(" where tpid=@tpid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tpid", SqlDbType.NVarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@ptype", SqlDbType.NVarChar, 0x40), new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.tpid;
            commandParameters[1].Value = model.tcode;
            commandParameters[2].Value = model.ptype;
            commandParameters[3].Value = model.pcode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

