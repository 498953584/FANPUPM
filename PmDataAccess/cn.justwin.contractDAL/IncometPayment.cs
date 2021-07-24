namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class IncometPayment
    {
        public int Add(IncometPaymentModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Incomet_Payment(");
            if (model.MonthPlanUID != null)
            {
                builder.Append("ID,ContractID,CllectionCode,CllectionTime,CllectionUser,InputPerson,CllectionPrice,Annex,Remark,InputDate,MonthPlanUID)");
            }
            else
            {
                builder.Append("ID,ContractID,CllectionCode,CllectionTime,CllectionUser,InputPerson,CllectionPrice,Annex,Remark,InputDate)");
            }
            builder.Append(" values (");
            if (model.MonthPlanUID != null)
            {
                builder.Append("@ID,@ContractID,@CllectionCode,@CllectionTime,@CllectionUser,@InputPerson,@CllectionPrice,@Annex,@Remark,@InputDate,@MonthPlanUID)");
            }
            else
            {
                builder.Append("@ID,@ContractID,@CllectionCode,@CllectionTime,@CllectionUser,@InputPerson,@CllectionPrice,@Annex,@Remark,@InputDate)");
            }
            List<SqlParameter> list = new List<SqlParameter>();
            if (model.MonthPlanUID != null)
            {
                list.Add(new SqlParameter("@ID", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionCode", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionTime", SqlDbType.DateTime));
                list.Add(new SqlParameter("@CllectionUser", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionPrice", SqlDbType.Decimal, 9));
                list.Add(new SqlParameter("@Annex", SqlDbType.NVarChar, 200));
                list.Add(new SqlParameter("@Remark", SqlDbType.NVarChar));
                list.Add(new SqlParameter("@InputDate", SqlDbType.DateTime));
                list.Add(new SqlParameter("@MonthPlanUID", SqlDbType.NVarChar, 0x40));
                list[0].Value = model.ID;
                list[1].Value = model.ContractID;
                list[2].Value = model.CllectionCode;
                list[3].Value = model.CllectionTime;
                list[4].Value = model.CllectionUser;
                list[5].Value = model.InputPerson;
                list[6].Value = model.CllectionPrice;
                list[7].Value = model.Annex;
                list[8].Value = model.Remark;
                list[9].Value = model.InputDate;
                list[10].Value = model.MonthPlanUID;
            }
            else
            {
                list.Add(new SqlParameter("@ID", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionCode", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionTime", SqlDbType.DateTime));
                list.Add(new SqlParameter("@CllectionUser", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionPrice", SqlDbType.Decimal, 9));
                list.Add(new SqlParameter("@Annex", SqlDbType.NVarChar, 200));
                list.Add(new SqlParameter("@Remark", SqlDbType.NVarChar));
                list.Add(new SqlParameter("@InputDate", SqlDbType.DateTime));
                list[0].Value = model.ID;
                list[1].Value = model.ContractID;
                list[2].Value = model.CllectionCode;
                list[3].Value = model.CllectionTime;
                list[4].Value = model.CllectionUser;
                list[5].Value = model.InputPerson;
                list[6].Value = model.CllectionPrice;
                list[7].Value = model.Annex;
                list[8].Value = model.Remark;
                list[9].Value = model.InputDate;
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Incomet_Payment ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public string GetCurrentMonthUID(string contractId, bool IsExamineApprove)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            if (!IsExamineApprove)
            {
                builder.AppendLine("SELECT UID FROM Fund_Plan_MonthMain Main ");
                builder.AppendLine("LEFT JOIN Fund_Plan_MonthDetail Detail ON Main.MonthPlanId=Detail.MonthPlanId  ");
                builder.AppendLine("WHERE PlanType='income' AND DATEDIFF(MONTH,PlanDate,GETDATE())=0 AND Main.FlowState=1  ");
                builder.AppendFormat("AND ContractId='{0}' ", contractId).AppendLine();
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                if (reader.Read())
                {
                    str = reader["UID"].ToString();
                }
            }
            return str;
        }

        public DataTable GetFundPlan(string contractId, bool IsExamineApprove)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            if (!IsExamineApprove)
            {
                builder.AppendLine("SELECT top 2 UID,PlanDate,PlanMoney,ContractId,UsedMoney,(PlanMoney-UsedMoney) UsableMoney,Remark FROM ");
                builder.AppendLine("(SELECT UID,PlanDate,PlanMoney,Detail.ContractId,ISNULL(cllectionPrice,0) UsedMoney,Detail.Remark FROM Fund_Plan_MonthMain Main ");
                builder.AppendLine(" LEFT JOIN Fund_Plan_MonthDetail Detail ON Main.MonthPlanID=Detail.MonthPlanID ");
                builder.AppendLine(" LEFT JOIN ( ");
                builder.AppendLine(" SELECT SUM(cllectionPrice) cllectionPrice,MonthPlanUID FROM Con_Incomet_Contract Cont INNER JOIN Con_Incomet_Payment ");
                builder.AppendLine(" Payment ON Cont.ContractId=Payment.ContractId GROUP BY MonthPlanUID) Cont ON UID=MonthPlanUID  ");
                builder.AppendFormat(" WHERE PlanType='income' and Detail.ContractId='{0}' AND Main.FlowState='1') FundPlan ", contractId).AppendLine();
                builder.AppendLine(" ORDER BY PlanDate DESC  ");
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public List<string> GetFundPlanByMonthPlanUID(string monthPlanUID, bool IsExamineApprove)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            if (!IsExamineApprove)
            {
                builder.AppendLine("SELECT UID,PlanDate,PlanMoney,ContractId,UsedMoney,(PlanMoney-UsedMoney) UsableMoney,Remark FROM   ");
                builder.AppendLine("(SELECT UID,PlanDate,ISNULL(PlanMoney,0) PlanMoney,Detail.ContractId,ISNULL(cllectionPrice,0) UsedMoney,Detail.Remark FROM Fund_Plan_MonthDetail Detail INNER JOIN  Fund_Plan_MonthMain Main ON Detail.MonthPlanID= Main.MonthPlanID  ");
                builder.AppendLine(" LEFT JOIN ( ");
                builder.AppendLine(" SELECT SUM(cllectionPrice) cllectionPrice,MonthPlanUID FROM Con_Incomet_Contract Cont INNER JOIN Con_Incomet_Payment ");
                builder.AppendLine(" Payment ON Cont.ContractId=Payment.ContractId GROUP BY MonthPlanUID) Cont ON UID=MonthPlanUID ");
                builder.AppendFormat("WHERE  CAST(UID AS NVARCHAR(1000))='{0}' ) FundPlan ", monthPlanUID).AppendLine();
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                if (!reader.Read())
                {
                    return list;
                }
                list.Add(reader["UID"].ToString());
                if (reader["PlanDate"] != null)
                {
                    list.Add(Convert.ToDateTime(reader["PlanDate"]).ToString("yyyy年MM月"));
                }
                else
                {
                    list.Add("");
                }
                list.Add(Convert.ToDecimal(reader["PlanMoney"]).ToString("0.000"));
                list.Add(reader["ContractId"].ToString());
                list.Add(Convert.ToDecimal(reader["UsedMoney"]).ToString("0.000"));
                list.Add(Convert.ToDecimal(reader["UsableMoney"]).ToString("0.000"));
                list.Add(reader["Remark"].ToString());
            }
            return list;
        }

        public List<IncometPaymentModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ContractID,CllectionCode,CllectionTime,CllectionUser,InputPerson,CllectionPrice,Annex,Remark,InputDate,state,MonthPlanUID ");
            builder.Append(" FROM Con_Incomet_Payment ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<IncometPaymentModel> list = new List<IncometPaymentModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<IncometPaymentModel> GetListByWhere(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM Con_Incomet_Contract AS ConInCon\r\n                             RIGHT JOIN Con_Incomet_Payment AS ConInPay ON ConInCon.contractId=ConInPay.contractId \r\n                             LEFT JOIN (SELECT *,''+IncomeAlarmDays+''+getdate() AS IncomeTime \r\n                             FROM Con_Config_Contract) AS Config ON Config.ContractId=ConInPay.ContractId\r\n                             WHERE ConInPay.CllectionTime<IncomeTime AND ConInCon.IsArchived='0' ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<IncometPaymentModel> list = new List<IncometPaymentModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<IncometPaymentModel> GetListByWhere(string strWhere, string prj, ref List<string> dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *,");
            builder.Append(" (select sum(inmoney) from fund_prj_accoun_income where prjguid='" + prj + "') as cinmoney ");
            builder.Append(" from con_incomet_contract as p1");
            builder.Append(" right join dbo.con_incomet_payment as p2 on p1.contractid=p2.contractid ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<IncometPaymentModel> list = new List<IncometPaymentModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    dt.Add(reader["Cinmoney"].ToString());
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public IncometPaymentModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ContractID,CllectionCode,CllectionTime,CllectionUser,InputPerson,CllectionPrice,Annex,Remark,InputDate,state,MonthPlanUID from Con_Incomet_Payment ");
            builder.Append(" where ID=@ID ");
            IncometPaymentModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public IncometPaymentModel ReaderBind(IDataReader dataReader)
        {
            IncometPaymentModel model = new IncometPaymentModel {
                ID = dataReader["ID"].ToString(),
                ContractID = dataReader["ContractID"].ToString(),
                CllectionCode = dataReader["CllectionCode"].ToString()
            };
            object obj2 = dataReader["CllectionTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.CllectionTime = new DateTime?((DateTime) obj2);
            }
            model.CllectionUser = dataReader["CllectionUser"].ToString();
            model.InputPerson = dataReader["InputPerson"].ToString();
            obj2 = dataReader["CllectionPrice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.CllectionPrice = new decimal?((decimal) obj2);
            }
            model.Annex = dataReader["Annex"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            obj2 = dataReader["InputDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.InputDate = new DateTime?((DateTime) obj2);
            }
            obj2 = dataReader["state"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.State = (int) dataReader["state"];
            }
            model.MonthPlanUID = DBHelper.GetString(dataReader["MonthPlanUID"]);
            return model;
        }

        public int Update(IncometPaymentModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Incomet_Payment set ");
            builder.Append("ContractID=@ContractID,");
            builder.Append("CllectionCode=@CllectionCode,");
            builder.Append("CllectionTime=@CllectionTime,");
            builder.Append("CllectionUser=@CllectionUser,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("CllectionPrice=@CllectionPrice,");
            builder.Append("Annex=@Annex,");
            builder.Append("Remark=@Remark,");
            builder.Append("InputDate=@InputDate");
            if (model.MonthPlanUID != null)
            {
                builder.Append(",MonthPlanUID=@MonthPlanUID ");
            }
            builder.Append(" where ID=@ID ");
            List<SqlParameter> list = new List<SqlParameter>();
            if (model.MonthPlanUID != null)
            {
                list.Add(new SqlParameter("@ID", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionCode", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionTime", SqlDbType.DateTime));
                list.Add(new SqlParameter("@CllectionUser", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionPrice", SqlDbType.Decimal, 9));
                list.Add(new SqlParameter("@Annex", SqlDbType.NVarChar, 200));
                list.Add(new SqlParameter("@Remark", SqlDbType.NVarChar));
                list.Add(new SqlParameter("@InputDate", SqlDbType.DateTime));
                list.Add(new SqlParameter("@MonthPlanUID", SqlDbType.NVarChar, 0x40));
                list[0].Value = model.ID;
                list[1].Value = model.ContractID;
                list[2].Value = model.CllectionCode;
                list[3].Value = model.CllectionTime;
                list[4].Value = model.CllectionUser;
                list[5].Value = model.InputPerson;
                list[6].Value = model.CllectionPrice;
                list[7].Value = model.Annex;
                list[8].Value = model.Remark;
                list[9].Value = model.InputDate;
                list[10].Value = model.MonthPlanUID;
            }
            else
            {
                list.Add(new SqlParameter("@ID", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionCode", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionTime", SqlDbType.DateTime));
                list.Add(new SqlParameter("@CllectionUser", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40));
                list.Add(new SqlParameter("@CllectionPrice", SqlDbType.Decimal, 9));
                list.Add(new SqlParameter("@Annex", SqlDbType.NVarChar, 200));
                list.Add(new SqlParameter("@Remark", SqlDbType.NVarChar));
                list.Add(new SqlParameter("@InputDate", SqlDbType.DateTime));
                list[0].Value = model.ID;
                list[1].Value = model.ContractID;
                list[2].Value = model.CllectionCode;
                list[3].Value = model.CllectionTime;
                list[4].Value = model.CllectionUser;
                list[5].Value = model.InputPerson;
                list[6].Value = model.CllectionPrice;
                list[7].Value = model.Annex;
                list[8].Value = model.Remark;
                list[9].Value = model.InputDate;
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public int UpdateState(SqlTransaction trans, string id)
        {
            string cmdText = "update Con_Incomet_Payment set state=1 where id=@id";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = id;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
        }
    }
}

