namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayendFeedback
    {
        public int Add(PayendFeedbackModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_PayendFeedback(");
            builder.Append("ID,ContractID,FeedbackPerson,FeedbackTime,FeedbackOpinion,Annex,InPerson,InTime,FeedbackState)");
            builder.Append(" values (");
            builder.Append("@ID,@ContractID,@FeedbackPerson,@FeedbackTime,@FeedbackOpinion,@Annex,@InPerson,@InTime,@FeedbackState)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@FeedbackPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@FeedbackTime", SqlDbType.DateTime), new SqlParameter("@FeedbackOpinion", SqlDbType.NVarChar), new SqlParameter("@Annex", SqlDbType.NVarChar, 200), new SqlParameter("@InPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InTime", SqlDbType.DateTime), new SqlParameter("@FeedbackState", SqlDbType.NVarChar, 10) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.FeedbackPerson;
            commandParameters[3].Value = model.FeedbackTime;
            commandParameters[4].Value = model.FeedbackOpinion;
            commandParameters[5].Value = model.Annex;
            commandParameters[6].Value = model.InPerson;
            commandParameters[7].Value = model.InTime;
            commandParameters[8].Value = model.FeedbackState;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_PayendFeedback ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<PayendFeedbackModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ContractID,FeedbackPerson,FeedbackTime,FeedbackOpinion,Annex,InPerson,InTime,FeedbackState ");
            builder.Append(" FROM Con_PayendFeedback ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<PayendFeedbackModel> list = new List<PayendFeedbackModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public PayendFeedbackModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ContractID,FeedbackPerson,FeedbackTime,FeedbackOpinion,Annex,InPerson,InTime,FeedbackState from Con_PayendFeedback ");
            builder.Append(" where ID=@ID ");
            PayendFeedbackModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public PayendFeedbackModel ReaderBind(IDataReader dataReader)
        {
            PayendFeedbackModel model = new PayendFeedbackModel {
                ID = dataReader["ID"].ToString(),
                ContractID = dataReader["ContractID"].ToString(),
                FeedbackPerson = dataReader["FeedbackPerson"].ToString()
            };
            object obj2 = dataReader["FeedbackTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.FeedbackTime = new DateTime?((DateTime) obj2);
            }
            model.FeedbackOpinion = dataReader["FeedbackOpinion"].ToString();
            model.Annex = dataReader["Annex"].ToString();
            model.InPerson = dataReader["InPerson"].ToString();
            obj2 = dataReader["InTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.InTime = new DateTime?((DateTime) obj2);
            }
            model.FeedbackState = dataReader["FeedbackState"].ToString();
            return model;
        }

        public int Update(PayendFeedbackModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_PayendFeedback set ");
            builder.Append("ContractID=@ContractID,");
            builder.Append("FeedbackPerson=@FeedbackPerson,");
            builder.Append("FeedbackTime=@FeedbackTime,");
            builder.Append("FeedbackOpinion=@FeedbackOpinion,");
            builder.Append("Annex=@Annex,");
            builder.Append("InPerson=@InPerson,");
            builder.Append("InTime=@InTime,");
            builder.Append("FeedbackState=@FeedbackState");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@FeedbackPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@FeedbackTime", SqlDbType.DateTime), new SqlParameter("@FeedbackOpinion", SqlDbType.NVarChar), new SqlParameter("@Annex", SqlDbType.NVarChar, 200), new SqlParameter("@InPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InTime", SqlDbType.DateTime), new SqlParameter("@FeedbackState", SqlDbType.NVarChar, 10) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.FeedbackPerson;
            commandParameters[3].Value = model.FeedbackTime;
            commandParameters[4].Value = model.FeedbackOpinion;
            commandParameters[5].Value = model.Annex;
            commandParameters[6].Value = model.InPerson;
            commandParameters[7].Value = model.InTime;
            commandParameters[8].Value = model.FeedbackState;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

