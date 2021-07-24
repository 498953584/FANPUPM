namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayoutModify
    {
        public void Add(PayoutModifyModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Payout_Modify(");
            builder.Append("ModifyID,ModifyCode,ContractID,ModifyDate,ModifyMoney,Reason,Annex,FlowState,Notes,InputPerson,InputDate,ModifyPerson)");
            builder.Append(" values (");
            builder.Append("@ModifyID,@ModifyCode,@ContractID,@ModifyDate,@ModifyMoney,@Reason,@Annex,@FlowState,@Notes,@InputPerson,@InputDate,@ModifyPerson)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModifyID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ModifyCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ModifyDate", SqlDbType.DateTime), new SqlParameter("@ModifyMoney", SqlDbType.Decimal, 9), new SqlParameter("@Reason", SqlDbType.NVarChar), new SqlParameter("@Annex", SqlDbType.NVarChar, 0x100), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@ModifyPerson", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.ModifyID;
            commandParameters[1].Value = model.ModifyCode;
            commandParameters[2].Value = model.ContractID;
            commandParameters[3].Value = model.ModifyDate;
            commandParameters[4].Value = model.ModifyMoney;
            commandParameters[5].Value = model.Reason;
            commandParameters[6].Value = model.Annex;
            commandParameters[7].Value = model.FlowState;
            commandParameters[8].Value = model.Notes;
            commandParameters[9].Value = model.InputPerson;
            commandParameters[10].Value = model.InputDate;
            commandParameters[11].Value = model.ModifyPerson;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public void Delete(List<string> modifyIds)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in modifyIds)
                    {
                        this.Delete(str, trans);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new Exception("删除失败");
                }
            }
        }

        public void Delete(string ModifyID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Payout_Modify ");
            builder.Append(" where ModifyID=@ModifyID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModifyID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ModifyID;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public bool Exists(string ModifyCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Con_Payout_Modify");
            builder.Append(" where ModifyCode=@ModifyCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModifyCode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = ModifyCode;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        private List<PayoutModifyModel> GetList(IDataReader dr)
        {
            List<PayoutModifyModel> list = new List<PayoutModifyModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<PayoutModifyModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Con_Payout_Modify.*,ContractName, UserCodes,Con_Payout_Contract.PrjGuid ");
            builder.Append("FROM Con_Payout_Modify ");
            builder.Append("JOIN Con_Payout_Contract ON Con_Payout_Modify.ContractID = Con_Payout_Contract.ContractID ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append(" WHERE ").Append(strWhere);
            }
            builder.Append(" ORDER BY Con_Payout_Modify.InputDate DESC");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private PayoutModifyModel GetModel(IDataReader dr)
        {
            return new PayoutModifyModel { ModifyID = DBHelper.GetString(dr["ModifyID"]), ModifyCode = DBHelper.GetString(dr["ModifyCode"]), ContractID = DBHelper.GetString(dr["ContractID"]), ContractName = DBHelper.GetString(dr["ContractName"]), ModifyDate = DBHelper.GetDateTimeNullable(dr["ModifyDate"]), ModifyMoney = new decimal?(DBHelper.GetDecimal(dr["ModifyMoney"])), ModifyPerson = DBHelper.GetString(dr["ModifyPerson"]), Reason = DBHelper.GetString(dr["Reason"]), Annex = DBHelper.GetString(dr["Annex"]), FlowState = new int?(DBHelper.GetInt(dr["FlowState"])), Notes = DBHelper.GetString(dr["Notes"]), InputPerson = DBHelper.GetString(dr["InputPerson"]), InputDate = new DateTime?(DBHelper.GetDateTime(dr["InputDate"])), UserCodes = DBHelper.GetString(dr["UserCodes"]), PrjGuid = DBHelper.GetString(dr["PrjGuid"]) };
        }

        public PayoutModifyModel GetModel(string ModifyID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP(1) Con_Payout_Modify.*,ContractName,UserCodes,PrjGuid ");
            builder.Append("FROM Con_Payout_Modify ");
            builder.Append("JOIN Con_Payout_Contract ON Con_Payout_Modify.ContractID = Con_Payout_Contract.ContractID ");
            builder.Append("WHERE ModifyID = @ModifyID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModifyID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ModifyID;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public bool IsExists(string ModifyCode, string ContractId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select  count(1) from Con_Payout_Modify  where ModifyCode=@ModifyCode and ContractID=@ContractID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModifyCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = ModifyCode;
            commandParameters[1].Value = ContractId;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public void Update(PayoutModifyModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Payout_Modify set ");
            builder.Append("ModifyCode=@ModifyCode,");
            builder.Append("ContractID=@ContractID,");
            builder.Append("ModifyDate=@ModifyDate,");
            builder.Append("ModifyMoney=@ModifyMoney,");
            builder.Append("Reason=@Reason,");
            builder.Append("Annex=@Annex,");
            builder.Append("FlowState=@FlowState,");
            builder.Append("Notes=@Notes,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate,");
            builder.Append("ModifyPerson=@ModifyPerson");
            builder.Append(" where ModifyID=@ModifyID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModifyID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ModifyCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ModifyDate", SqlDbType.DateTime), new SqlParameter("@ModifyMoney", SqlDbType.Decimal, 9), new SqlParameter("@Reason", SqlDbType.NVarChar), new SqlParameter("@Annex", SqlDbType.NVarChar, 0x100), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@ModifyPerson", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.ModifyID;
            commandParameters[1].Value = model.ModifyCode;
            commandParameters[2].Value = model.ContractID;
            commandParameters[3].Value = model.ModifyDate;
            commandParameters[4].Value = model.ModifyMoney;
            commandParameters[5].Value = model.Reason;
            commandParameters[6].Value = model.Annex;
            commandParameters[7].Value = model.FlowState;
            commandParameters[8].Value = model.Notes;
            commandParameters[9].Value = model.InputPerson;
            commandParameters[10].Value = model.InputDate;
            commandParameters[11].Value = model.ModifyPerson;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }
    }
}

