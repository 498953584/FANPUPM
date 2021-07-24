namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class IEPInfo
    {
        public int Add(SqlTransaction trans, IEPInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into fund_IEPInfo(");
            builder.Append("ID,infoNum,IEPid,type,Money,conId,addDate,addPeople,remark)");
            builder.Append(" values (");
            builder.Append("@ID,@infoNum,@IEPid,@type,@Money,@conId,@addDate,@addPeople,@remark)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x22), new SqlParameter("@infoNum", SqlDbType.NVarChar, 50), new SqlParameter("@IEPid", SqlDbType.NVarChar, 0x40), new SqlParameter("@type", SqlDbType.Int, 4), new SqlParameter("@Money", SqlDbType.Decimal, 9), new SqlParameter("@conId", SqlDbType.NVarChar, 0x40), new SqlParameter("@addDate", SqlDbType.DateTime), new SqlParameter("@addPeople", SqlDbType.VarChar, 0x40), new SqlParameter("@remark", SqlDbType.VarChar, 100) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.infoNum;
            commandParameters[2].Value = model.IEPid;
            commandParameters[3].Value = model.type;
            commandParameters[4].Value = model.Money;
            commandParameters[5].Value = model.conId;
            commandParameters[6].Value = model.addDate;
            commandParameters[7].Value = model.addPeople;
            commandParameters[8].Value = model.remark;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DelByIEPid(SqlTransaction trans, string IEPid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_IEPInfo ");
            builder.Append(" where IEPid=@IEPid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@IEPid", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = IEPid;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_IEPInfo ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_IEPInfo ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return DBHelper.ExecuteNoQuery(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,infoNum,IEPid,type,Money,conId,addDate,addPeople,remark,v_xm  ");
            builder.Append(" FROM fund_IEPInfo as f inner join PT_yhmc as pt on f.addPeople=pt.v_yhdm ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by addDate desc");
            return DBHelper.GetTable(builder.ToString());
        }

        public IEPInfoModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,infoNum,IEPid,type,Money,conId,addDate,addPeople,remark from fund_IEPInfo ");
            builder.Append(" where ID=@ID ");
            IEPInfoModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public IEPInfoModel ReaderBind(IDataReader dataReader)
        {
            IEPInfoModel model = new IEPInfoModel {
                ID = dataReader["ID"].ToString(),
                infoNum = dataReader["infoNum"].ToString(),
                IEPid = dataReader["IEPid"].ToString()
            };
            if (dataReader["type"].ToString() != "")
            {
                model.type = new int?(int.Parse(dataReader["type"].ToString()));
            }
            if (dataReader["Money"].ToString() != "")
            {
                model.Money = new decimal?(decimal.Parse(dataReader["Money"].ToString()));
            }
            model.conId = dataReader["conId"].ToString();
            if (dataReader["addDate"].ToString() != "")
            {
                model.addDate = new DateTime?(DateTime.Parse(dataReader["addDate"].ToString()));
            }
            model.addPeople = dataReader["addPeople"].ToString();
            model.remark = dataReader["remark"].ToString();
            return model;
        }

        public int Update(SqlTransaction trans, IEPInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_IEPInfo set ");
            builder.Append("infoNum=@infoNum,");
            builder.Append("IEPid=@IEPid,");
            builder.Append("type=@type,");
            builder.Append("Money=@Money,");
            builder.Append("conId=@conId,");
            builder.Append("addDate=@addDate,");
            builder.Append("addPeople=@addPeople,");
            builder.Append("remark=@remark");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@infoNum", SqlDbType.NVarChar, 50), new SqlParameter("@IEPid", SqlDbType.NVarChar, 0x40), new SqlParameter("@type", SqlDbType.Int, 4), new SqlParameter("@Money", SqlDbType.Decimal, 9), new SqlParameter("@conId", SqlDbType.NVarChar, 0x40), new SqlParameter("@addDate", SqlDbType.DateTime), new SqlParameter("@addPeople", SqlDbType.VarChar, 0x40), new SqlParameter("@remark", SqlDbType.VarChar, 100), new SqlParameter("@ID", SqlDbType.NVarChar, 0x22) };
            commandParameters[0].Value = model.infoNum;
            commandParameters[1].Value = model.IEPid;
            commandParameters[2].Value = model.type;
            commandParameters[3].Value = model.Money;
            commandParameters[4].Value = model.conId;
            commandParameters[5].Value = model.addDate;
            commandParameters[6].Value = model.addPeople;
            commandParameters[7].Value = model.remark;
            commandParameters[8].Value = model.ID;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

