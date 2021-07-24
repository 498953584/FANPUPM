namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class InExPlan
    {
        public int Add(SqlTransaction trans, InExPlanModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into fund_InExPlan(");
            builder.Append("ID,IEPNum,IEPname,IEPtype,IEPdate,prjNum,state)");
            builder.Append(" values (");
            builder.Append("@ID,@IEPNum,@IEPname,@IEPtype,@IEPdate,@prjNum,@state)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@IEPNum", SqlDbType.NVarChar, 50), new SqlParameter("@IEPname", SqlDbType.NVarChar, 50), new SqlParameter("@IEPtype", SqlDbType.Int, 4), new SqlParameter("@IEPdate", SqlDbType.DateTime), new SqlParameter("@prjNum", SqlDbType.NVarChar, 0x40), new SqlParameter("@state", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.IEPNum;
            commandParameters[2].Value = model.IEPname;
            commandParameters[3].Value = model.IEPtype;
            commandParameters[4].Value = model.IEPdate;
            commandParameters[5].Value = model.prjNum;
            commandParameters[6].Value = model.state;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_InExPlan ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = ID;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteList(SqlTransaction trans, string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_InExPlan ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return DBHelper.ExecuteNoQuery(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,IEPNum,IEPname,IEPtype,IEPdate,prjNum,state ");
            builder.Append(" FROM fund_InExPlan ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            return DBHelper.GetTable(builder.ToString());
        }

        public InExPlanModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,IEPNum,IEPname,IEPtype,IEPdate,prjNum,state from fund_InExPlan ");
            builder.Append(" where ID=@ID ");
            InExPlanModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public InExPlanModel ReaderBind(IDataReader dataReader)
        {
            InExPlanModel model = new InExPlanModel();
            if (dataReader["ID"].ToString() != "")
            {
                model.ID = dataReader["ID"].ToString();
            }
            model.IEPNum = dataReader["IEPNum"].ToString();
            model.IEPname = dataReader["IEPname"].ToString();
            if (dataReader["IEPtype"].ToString() != "")
            {
                model.IEPtype = new int?(int.Parse(dataReader["IEPtype"].ToString()));
            }
            if (dataReader["IEPdate"].ToString() != "")
            {
                model.IEPdate = new DateTime?(DateTime.Parse(dataReader["IEPdate"].ToString()));
            }
            model.prjNum = dataReader["prjNum"].ToString();
            if (dataReader["state"].ToString() != "")
            {
                model.state = new int?(int.Parse(dataReader["state"].ToString()));
            }
            return model;
        }

        public int Update(SqlTransaction trans, InExPlanModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_InExPlan set ");
            builder.Append("IEPNum=@IEPNum,");
            builder.Append("IEPname=@IEPname,");
            builder.Append("IEPtype=@IEPtype,");
            builder.Append("IEPdate=@IEPdate,");
            builder.Append("prjNum=@prjNum,");
            builder.Append("state=@state");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@IEPNum", SqlDbType.NVarChar, 50), new SqlParameter("@IEPname", SqlDbType.NVarChar, 50), new SqlParameter("@IEPtype", SqlDbType.Int, 4), new SqlParameter("@IEPdate", SqlDbType.DateTime), new SqlParameter("@prjNum", SqlDbType.NVarChar, 0x40), new SqlParameter("@state", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.IEPNum;
            commandParameters[1].Value = model.IEPname;
            commandParameters[2].Value = model.IEPtype;
            commandParameters[3].Value = model.IEPdate;
            commandParameters[4].Value = model.prjNum;
            commandParameters[5].Value = model.state;
            commandParameters[6].Value = model.ID;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

