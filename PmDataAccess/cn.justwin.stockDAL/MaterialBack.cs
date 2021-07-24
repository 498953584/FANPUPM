namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MaterialBack
    {
        private MaterialBackModel model = new MaterialBackModel();

        public int add(MaterialBackModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into Sm_Refunding(rid,rcode,procode,tcode,flowstate,isin,person,intime,annx,explain) values(@rid,@rcode,@procode,@tcode,@flowstate,@isin,@person,@intime,@annx,@explain)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rid", SqlDbType.NVarChar, 50), new SqlParameter("@rcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@procode", SqlDbType.NVarChar, 0x40), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@isin", SqlDbType.Bit, 1), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime, 8), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800) };
            commandParameters[0].Value = model.Rid;
            commandParameters[1].Value = model.Rcode;
            commandParameters[2].Value = model.Procode;
            commandParameters[3].Value = model.Tcode;
            commandParameters[4].Value = model.FlowState;
            commandParameters[5].Value = model.IsIn;
            commandParameters[6].Value = model.Person;
            commandParameters[7].Value = model.InTime;
            commandParameters[8].Value = model.Annx;
            commandParameters[9].Value = model.Explain;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string rcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Refunding ");
            builder.Append(" where rcode=@rcode ");
            builder.Append("delete from Sm_Back_Stock ");
            builder.Append(" where scode=@rcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rcode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = rcode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int ExcuteSql(string sql)
        {
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public DataTable getMaterialListBySql(string strWhere)
        {
            string cmdText = "select ResourceCode,ResourceName,Price,UnitName,Specification from EPM_V_Res_ResourceBasic ";
            cmdText = cmdText + strWhere;
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public MaterialBackModel getSmRefoundRow(string rcode)
        {
            StringBuilder builder = new StringBuilder();
            MaterialBackModel model = new MaterialBackModel();
            builder.Append(" select * from Sm_Refunding where rcode=@rcode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rcode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = rcode;
            SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters);
            while (reader.Read())
            {
                model.Rid = reader.GetString(0);
                model.Rcode = reader.GetString(1);
                model.Procode = reader.GetString(2);
                model.Tcode = reader.GetString(3);
                model.FlowState = reader.GetInt32(4);
                model.IsIn = reader.GetBoolean(5);
                model.Person = reader.GetString(6);
                model.InTime = reader.GetDateTime(7);
                model.Annx = reader.GetString(8);
                model.Explain = reader.GetString(9);
            }
            return model;
        }

        public DataTable getTableSmRefoundByPro(string procode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from Sm_Refunding where procode=@procode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@procode", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = procode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable Search(string sql)
        {
            string cmdText = "select * from Sm_Refunding where 1=1";
            cmdText = cmdText + sql;
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public int Update(MaterialBackModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Refunding set ");
            builder.Append("rid=@rid,");
            builder.Append("rcode=@rcode,");
            builder.Append("procode=@procode,");
            builder.Append("tcode=@tcode,");
            builder.Append("flowstate=@flowstate,");
            builder.Append("isin=@isin,");
            builder.Append("person=@person,");
            builder.Append("intime=@intime");
            builder.Append("annx=@annx,");
            builder.Append("explain=@explain");
            builder.Append(" where rcode=@rcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rid", SqlDbType.NVarChar, 50), new SqlParameter("@rcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@procode", SqlDbType.NVarChar, 0x40), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@isin", SqlDbType.Bit, 1), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime, 8), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800) };
            commandParameters[0].Value = model.Rid;
            commandParameters[1].Value = model.Rcode;
            commandParameters[2].Value = model.Procode;
            commandParameters[3].Value = model.Tcode;
            commandParameters[4].Value = model.FlowState;
            commandParameters[5].Value = model.IsIn;
            commandParameters[6].Value = model.Person;
            commandParameters[7].Value = model.InTime;
            commandParameters[8].Value = model.Annx;
            commandParameters[9].Value = model.Explain;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

