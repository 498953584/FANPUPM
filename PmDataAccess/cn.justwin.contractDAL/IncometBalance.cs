namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class IncometBalance
    {
        public int Add(IncometBalanceModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Incomet_Balance(");
            builder.Append("ID,ContractID,ClearingNumber,ClearingTime,BalanceMode,PayMode,BalanceObject,ClearingUser,ClearingPrice,Annex,Remark,InputPerson,InputDate)");
            builder.Append(" values (");
            builder.Append("@ID,@ContractID,@ClearingNumber,@ClearingTime,@BalanceMode,@PayMode,@BalanceObject,@ClearingUser,@ClearingPrice,@Annex,@Remark,@InputPerson,@InputDate)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ClearingNumber", SqlDbType.NVarChar, 0x40), new SqlParameter("@ClearingTime", SqlDbType.DateTime), new SqlParameter("@BalanceMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceObject", SqlDbType.NVarChar, 0x40), new SqlParameter("@ClearingUser", SqlDbType.NVarChar, 0x40), new SqlParameter("@ClearingPrice", SqlDbType.Decimal, 9), new SqlParameter("@Annex", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.ClearingNumber;
            commandParameters[3].Value = model.ClearingTime;
            commandParameters[4].Value = model.BalanceMode;
            commandParameters[5].Value = model.PayMode;
            commandParameters[6].Value = model.BalanceObject;
            commandParameters[7].Value = model.ClearingUser;
            commandParameters[8].Value = model.ClearingPrice;
            commandParameters[9].Value = model.Annex;
            commandParameters[10].Value = model.Remark;
            commandParameters[11].Value = model.InputPerson;
            commandParameters[12].Value = model.InputDate;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Incomet_Balance ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<IncometBalanceModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ContractID,ClearingNumber,ClearingTime,BalanceMode,PayMode,BalanceObject,ClearingUser,ClearingPrice,Annex,Remark,InputPerson,InputDate ");
            builder.Append(" FROM Con_Incomet_Balance ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<IncometBalanceModel> list = new List<IncometBalanceModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public IncometBalanceModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,ContractID,ClearingNumber,ClearingTime,BalanceMode,PayMode,BalanceObject,ClearingUser,ClearingPrice,Annex,Remark,InputPerson,InputDate from Con_Incomet_Balance ");
            builder.Append(" where ID=@ID ");
            IncometBalanceModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public IncometBalanceModel ReaderBind(IDataReader dataReader)
        {
            IncometBalanceModel model = new IncometBalanceModel {
                ID = dataReader["ID"].ToString(),
                ContractID = dataReader["ContractID"].ToString(),
                ClearingNumber = dataReader["ClearingNumber"].ToString()
            };
            object obj2 = dataReader["ClearingTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ClearingTime = new DateTime?((DateTime) obj2);
            }
            model.BalanceMode = dataReader["BalanceMode"].ToString();
            model.PayMode = dataReader["PayMode"].ToString();
            model.BalanceObject = dataReader["BalanceObject"].ToString();
            model.ClearingUser = dataReader["ClearingUser"].ToString();
            obj2 = dataReader["ClearingPrice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ClearingPrice = new decimal?((decimal) obj2);
            }
            model.Annex = dataReader["Annex"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            model.InputPerson = dataReader["InputPerson"].ToString();
            obj2 = dataReader["InputDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.InputDate = new DateTime?((DateTime) obj2);
            }
            return model;
        }

        public int Update(IncometBalanceModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Incomet_Balance set ");
            builder.Append("ContractID=@ContractID,");
            builder.Append("ClearingNumber=@ClearingNumber,");
            builder.Append("ClearingTime=@ClearingTime,");
            builder.Append("BalanceMode=@BalanceMode,");
            builder.Append("PayMode=@PayMode,");
            builder.Append("BalanceObject=@BalanceObject,");
            builder.Append("ClearingUser=@ClearingUser,");
            builder.Append("ClearingPrice=@ClearingPrice,");
            builder.Append("Annex=@Annex,");
            builder.Append("Remark=@Remark,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ClearingNumber", SqlDbType.NVarChar, 0x40), new SqlParameter("@ClearingTime", SqlDbType.DateTime), new SqlParameter("@BalanceMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceObject", SqlDbType.NVarChar, 0x40), new SqlParameter("@ClearingUser", SqlDbType.NVarChar, 0x40), new SqlParameter("@ClearingPrice", SqlDbType.Decimal, 9), new SqlParameter("@Annex", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.ClearingNumber;
            commandParameters[3].Value = model.ClearingTime;
            commandParameters[4].Value = model.BalanceMode;
            commandParameters[5].Value = model.PayMode;
            commandParameters[6].Value = model.BalanceObject;
            commandParameters[7].Value = model.ClearingUser;
            commandParameters[8].Value = model.ClearingPrice;
            commandParameters[9].Value = model.Annex;
            commandParameters[10].Value = model.Remark;
            commandParameters[11].Value = model.InputPerson;
            commandParameters[12].Value = model.InputDate;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

