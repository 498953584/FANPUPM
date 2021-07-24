namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayoutPayment
    {
        public void Add(PayoutPaymentModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Payout_Payment(");
            builder.Append("ID,PaymentCode,ContractID,PaymentMoney,PaymentPerson,PaymentDate,Annex,FlowState,Notes,InputPerson,InputDate,ContainPending,PayType,CapitalNumber,MonthPlanUID,Beneficiary,Bank,Account)");
            builder.Append(" values (");
            builder.Append("@ID,@PaymentCode,@ContractID,@PaymentMoney,@PaymentPerson,@PaymentDate,@Annex,@FlowState,@Notes,@InputPerson,@InputDate,@ContainPending,@PayType,@CapitalNumber,@MonthPlanUID,@Beneficiary,@Bank,@Account)");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@ID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@PaymentCode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@PaymentMoney", SqlDbType.Decimal, 9),
                new SqlParameter("@PaymentPerson", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@PaymentDate", 4),
                new SqlParameter("@Annex", SqlDbType.NVarChar, 0x100),
                new SqlParameter("@FlowState", SqlDbType.Int, 4),
                new SqlParameter("@Notes", 12),
                new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@InputDate", 4),
                new SqlParameter("@ContainPending", 2),
                new SqlParameter("@PayType", 0x10),
                new SqlParameter("@CapitalNumber", SqlDbType.NVarChar, 0x3e8),
                new SqlParameter("@MonthPlanUID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@Beneficiary", SqlDbType.NVarChar, 200),
                new SqlParameter("@Bank", SqlDbType.NVarChar, 200),
                new SqlParameter("@Account", SqlDbType.NVarChar, 200)
            };
            list[0].Value = model.ID;
            list[1].Value = model.PaymentCode;
            list[2].Value = model.ContractID;
            list[3].Value = model.PaymentMoney;
            list[4].Value = model.PaymentPerson;
            list[5].Value = model.PaymentDate;
            list[6].Value = model.Annex;
            list[7].Value = model.FlowState;
            list[8].Value = model.Notes;
            list[9].Value = model.InputPerson;
            list[10].Value = model.InputDate;
            list[11].Value = model.ContainPending;
            list[12].Value = model.Paytype;
            list[13].Value = model.Capitalnumber;
            if (model.MonthPlanUID != null)
            {
                list[14].Value = model.MonthPlanUID;
            }
            else
            {
                list[14].Value = DBNull.Value;
            }
            list[15].Value = model.Beneficiary;
            list[0x10].Value = model.Bank;
            list[0x11].Value = model.Account;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), list.ToArray());
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), list.ToArray());
            }
        }

        public void Delete(List<string> paymentIds)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in paymentIds)
                    {
                        this.Delete(str, trans);
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw new Exception("删除失败");
                }
            }
        }

        public void Delete(string ID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Payout_Payment ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public bool Exists(string paymentId)
        {
            string cmdText = "SELECT COUNT(1) FROM Con_Payout_Payment WHERE ID = @ID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = paymentId;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters)) > 0);
        }

        public string GetCurrentMonthUID(string contractId)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT UID FROM Fund_Plan_MonthMain Main ");
            builder.AppendLine("LEFT JOIN Fund_Plan_MonthDetail Detail ON Main.MonthPlanId=Detail.MonthPlanId ");
            builder.AppendLine("WHERE PlanType='payout' AND DATEDIFF(MONTH,PlanDate,GETDATE())=0 AND Main.FlowState=1 ");
            builder.AppendFormat("AND ContractId='{0}' ", contractId).AppendLine();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                if (reader.Read())
                {
                    str = reader["UID"].ToString();
                }
            }
            return str;
        }

        public DataTable GetFundPlan(string contractId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT top 2 UID,PlanDate,PlanMoney,ContractId,UsedMoney, ");
            builder.AppendLine("(PlanMoney-UsedMoney) UsableMoney,Remark FROM   ");
            builder.AppendLine("(SELECT UID,PlanDate,PlanMoney,Detail.ContractId,ISNULL(PaymentMoney,0) UsedMoney,Detail.Remark FROM Fund_Plan_MonthMain Main   ");
            builder.AppendLine("LEFT JOIN Fund_Plan_MonthDetail Detail ON Main.MonthPlanID=Detail.MonthPlanID   ");
            builder.AppendLine("LEFT JOIN (SELECT SUM(PaymentMoney) PaymentMoney,MonthPlanUID FROM Con_Payout_Contract Cont INNER JOIN Con_Payout_Payment   ");
            builder.AppendLine("Payment ON Cont.ContractId=Payment.ContractId WHERE Payment.FlowState='1' GROUP BY MonthPlanUID) Cont ON UID=MonthPlanUID  ");
            builder.AppendFormat("WHERE PlanType='payout' and Detail.ContractId='{0}' AND Main.FlowState='1') FundPlan  ", contractId).AppendLine();
            builder.AppendLine("ORDER BY PlanDate DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public List<string> GetFundPlanByMonthPlanUID(string monthPlanUID)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT UID,PlanDate,PlanMoney,ContractId, UsedMoney,(PlanMoney-UsedMoney) UsableMoney,Remark FROM  ");
            builder.AppendLine("(SELECT UID,PlanDate,ISNULL(PlanMoney,0) PlanMoney,Detail.ContractId,ISNULL(PaymentMoney,0) UsedMoney,Detail.Remark FROM Fund_Plan_MonthDetail Detail INNER JOIN  Fund_Plan_MonthMain Main ON Detail.MonthPlanID= Main.MonthPlanID ");
            builder.AppendLine("LEFT JOIN (SELECT SUM(PaymentMoney) PaymentMoney,MonthPlanUID FROM Con_Payout_Contract Cont INNER JOIN Con_Payout_Payment  ");
            builder.AppendLine("Payment ON Cont.ContractId=Payment.ContractId WHERE Payment.FlowState='1' GROUP BY MonthPlanUID) Cont ");
            builder.AppendFormat("ON UID=MonthPlanUID WHERE  CAST(UID AS NVARCHAR(1000))='{0}' ) FundPlan ", monthPlanUID);
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

        public List<PayoutPaymentModel> GetList()
        {
            string cmdText = string.Format("SELECT (SELECT ISNULL(SUM(PayOutMoney),0.00) \r\n                            FROM Fund_Prj_Accoun_Payout\r\n                            WHERE Con_Payout_Payment.id=Fund_Prj_Accoun_Payout.RpGuid  \r\n                            AND Fund_Prj_Accoun_Payout.floestate=1) AS pidcont,\r\n                            Con_Payout_Payment.*,ContractName,UserCodes,Con_Payout_Contract.PrjGuid \r\n                            FROM Con_Payout_Payment \r\n                            LEFT JOIN Con_Payout_Contract \r\n                            ON Con_Payout_Contract.ContractID = Con_Payout_Payment.ContractID \r\n                            LEFT JOIN (SELECT *,''+IncomeAlarmDays+''+getdate() AS IncomeTime,\r\n                            ''+PayoutAlarmDays+''+getdate() AS PayoutTime FROM Con_Config_Contract) AS Config\r\n                            ON Config.ContractId=Con_Payout_Payment.ContractID\r\n                            WHERE Con_Payout_Payment.PaymentDate <PayoutTime AND Con_Payout_Payment.FlowState = 1 \r\n                            AND Con_Payout_Contract.IsArchived = '0' ORDER BY Con_Payout_Payment.InputDate ", new object[0]);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, null))
            {
                return this.GetList(reader);
            }
        }

        private List<PayoutPaymentModel> GetList(IDataReader dr)
        {
            List<PayoutPaymentModel> list = new List<PayoutPaymentModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<PayoutPaymentModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select ");
            builder.Append("(select isnull(sum(PayOutMoney),0.00) from Fund_Prj_Accoun_Payout where  Con_Payout_Payment.id=Fund_Prj_Accoun_Payout.RpGuid  and Fund_Prj_Accoun_Payout.floestate=1) as pidcont,");
            builder.Append(" Con_Payout_Payment.*,ContractName,UserCodes,Con_Payout_Contract.PrjGuid ");
            builder.Append("FROM Con_Payout_Payment ");
            builder.Append("LEFT JOIN Con_Payout_Contract ON Con_Payout_Contract.ContractID = Con_Payout_Payment.ContractID ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append(" WHERE ").Append(strWhere);
            }
            builder.Append(" ORDER BY Con_Payout_Payment.InputDate ");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private PayoutPaymentModel GetModel(IDataReader dr)
        {
            PayoutPaymentModel model = new PayoutPaymentModel();
            if (dr.FieldCount > 0)
            {
                model.ID = DBHelper.GetString(dr["ID"]);
                model.PaymentCode = DBHelper.GetString(dr["PaymentCode"]);
                model.ContractID = DBHelper.GetString(dr["ContractID"]);
                model.ContractName = DBHelper.GetString(dr["ContractName"]);
                model.PaymentMoney = new decimal?(DBHelper.GetDecimal(dr["PaymentMoney"]));
                model.PaymentPerson = DBHelper.GetString(dr["PaymentPerson"]);
                model.PaymentDate = DBHelper.GetDateTimeNullable(dr["PaymentDate"]);
                model.Annex = DBHelper.GetString(dr["Annex"]);
                model.FlowState = new int?(DBHelper.GetInt(dr["FlowState"]));
                model.Notes = DBHelper.GetString(dr["Notes"]);
                model.InputPerson = DBHelper.GetString(dr["InputPerson"]);
                model.InputDate = new DateTime?(DBHelper.GetDateTime(dr["InputDate"]));
                model.UserCodes = DBHelper.GetString(dr["UserCodes"]);
                model.ContainPending = DBHelper.GetBool(dr["ContainPending"]);
                model.PrjGuid = DBHelper.GetString(dr["PrjGuid"]);
                model.Pidcont = new decimal?(DBHelper.GetDecimal(dr["PidCont"]));
                object obj1 = dr["PayType"];
                model.Paytype = (dr["PayType"].ToString() != "") ? int.Parse(dr["PayType"].ToString()) : -1;
                model.Capitalnumber = DBHelper.GetString(dr["CapitalNumber"]);
                model.MonthPlanUID = DBHelper.GetString(dr["MonthPlanUID"]);
                model.Beneficiary = DBHelper.GetString(dr["Beneficiary"]);
                model.Bank = DBHelper.GetString(dr["Bank"]);
                model.Account = DBHelper.GetString(dr["Account"]);
            }
            return model;
        }

        public PayoutPaymentModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select TOP(1)");
            builder.Append("(select isnull(sum(PayOutMoney),0.00) from Fund_Prj_Accoun_Payout where  Con_Payout_Payment.id=Fund_Prj_Accoun_Payout.RpGuid  and Fund_Prj_Accoun_Payout.floestate=1) as pidcont,");
            builder.Append("  Con_Payout_Payment.*,ContractName,UserCodes,PrjGuid ");
            builder.Append("FROM Con_Payout_Payment ");
            builder.Append("JOIN Con_Payout_Contract ON Con_Payout_Contract.ContractID = Con_Payout_Payment.ContractID ");
            builder.Append("WHERE ID = @ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public DataTable GetReportData(string strWhere, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append("SELECT c.ContractID,ContractCode,ContractName,BName,ModifiedMoney,c.conState,Convert(NVARCHAR(10),SignDate,121) AS SignDate,TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) as PrjName, ").AppendLine();
            builder.Append("ISNULL(SUM(PaymentMoney),0) AS PaymentTotal, ISNULL(SUM(PaymentMoney)/NULLIF(ModifiedMoney,0),0) AS Rate, YH.v_xm AS SignPersonName, ").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("\t   WHEN 1 THEN c.ContractID ").AppendLine();
            builder.Append("\t   WHEN 0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append("END AS Version,").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("     WHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("     WHEN 0 THEN (SELECT c2.InputDate FROM Con_Payout_Contract c1").AppendLine();
            builder.Append("                  JOIN Con_Payout_Contract c2 ON c1.MainContractID = c1.ContractID").AppendLine();
            builder.Append("                  WHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("                 )").AppendLine();
            builder.Append("END AS Date,").AppendLine();
            builder.Append("IsMainContract,c.UserCodes,ISNULL(CorpName,BName) AS CorpName,PrjType.CodeName  ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Payment ON Con_Payout_Payment.ContractID = c.ContractID ").AppendLine();
            builder.Append("AND Con_Payout_Payment.FlowState = '1' ").AppendLine();
            builder.Append("LEFT JOIN Con_ContractType ON Con_ContractType.TypeID = c.TypeID  ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo ON PT_PrjInfo.PrjGuid = c.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB on c.PrjGuid = PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append("LEFT JOIN PT_yhmc YH ON YH.v_yhdm = c.SignPerson").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))  ").AppendLine();
            builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
            builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
            builder.Append(" WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
            builder.Append(" FROM PT_PrjInfo_Kind as PrjInfo_PrjKind GROUP by[PrjGuid] ) PrjType ON c.PrjGuid = PrjType.PrjGuid ").AppendLine();
            builder.Append("WHERE c.FlowState = '1' ").AppendLine();
            builder.Append("AND c.PrjGuid != '' ").AppendLine();
            builder.Append("AND c.UserCodes  LIKE '%" + userCode + "%'").AppendLine();
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" AND ").Append(strWhere).AppendLine();
            }
            builder.Append(" GROUP BY c.ContractID,ContractCode,ContractName,BName,ModifiedMoney,c.conState,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName, ").AppendLine();
            builder.Append("IsMainContract,MainContractID,c.InputDate,c.UserCodes,CorpName,PrjType.CodeName, YH.v_xm ").AppendLine();
            builder.Append(") Y \n");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetReportDataSource(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append("SELECT c.ContractID,ContractCode,ContractName,BName,ModifiedMoney,c.conState,Convert(NVARCHAR(10),SignDate,121) AS SignDate,TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) as PrjName, ").AppendLine();
            builder.Append("ISNULL(SUM(PaymentMoney),0) AS PaymentTotal, ISNULL(SUM(PaymentMoney)/NULLIF(ModifiedMoney,0),0) AS Rate, YH.v_xm AS SignPersonName, ").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("\t   WHEN 1 THEN c.ContractID ").AppendLine();
            builder.Append("\t   WHEN 0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append("END AS Version,").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("     WHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("     WHEN 0 THEN (SELECT c2.InputDate FROM Con_Payout_Contract c1").AppendLine();
            builder.Append("                  JOIN Con_Payout_Contract c2 ON c1.MainContractID = c1.ContractID").AppendLine();
            builder.Append("                  WHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("                 )").AppendLine();
            builder.Append("END AS Date,").AppendLine();
            builder.Append("IsMainContract,c.UserCodes,ISNULL(CorpName,BName) AS CorpName,PrjType.CodeName  ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Payment ON Con_Payout_Payment.ContractID = c.ContractID ").AppendLine();
            builder.Append("AND Con_Payout_Payment.FlowState = '1' ").AppendLine();
            builder.Append("LEFT JOIN Con_ContractType ON Con_ContractType.TypeID = c.TypeID  ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo ON PT_PrjInfo.PrjGuid = c.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN PT_yhmc YH ON YH.v_yhdm = c.SignPerson").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB on c.PrjGuid = PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))  ").AppendLine();
            builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
            builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
            builder.Append(" WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
            builder.Append(" FROM PT_PrjInfo_Kind as PrjInfo_PrjKind GROUP by[PrjGuid] ) PrjType ON c.PrjGuid = PrjType.PrjGuid ").AppendLine();
            builder.Append("WHERE c.FlowState = '1' ").AppendLine();
            builder.Append("AND c.PrjGuid != '' ").AppendLine();
            builder.Append("AND c.UserCodes  LIKE '%" + userCode + "%'").AppendLine();
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" AND ").Append(strWhere).AppendLine();
            }
            builder.Append(" GROUP BY c.ContractID,ContractCode,ContractName,BName,ModifiedMoney,c.conState,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName, ").AppendLine();
            builder.Append("IsMainContract,MainContractID,c.InputDate,c.UserCodes,CorpName,PrjType.CodeName, YH.v_xm ").AppendLine();
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            Log4netHelper.Debug(builder.ToString(), "");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public bool IsExists(string PaymentCode, string ContractID)
        {
            string cmdText = "SELECT COUNT(1) FROM Con_Payout_Payment WHERE PaymentCode=@PaymentCode AND ContractID=@ContractID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PaymentCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = PaymentCode;
            commandParameters[1].Value = ContractID;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters)) > 0);
        }

        public void Update(PayoutPaymentModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Payout_Payment set ");
            builder.Append("PaymentCode=@PaymentCode,");
            builder.Append("ContractID=@ContractID,");
            builder.Append("PaymentMoney=@PaymentMoney,");
            builder.Append("PaymentPerson=@PaymentPerson,");
            builder.Append("PaymentDate=@PaymentDate,");
            builder.Append("Annex=@Annex,");
            builder.Append("FlowState=@FlowState,");
            builder.Append("Notes=@Notes,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate,");
            builder.Append("ContainPending=@ContainPending,");
            builder.Append("PayType=@PayType,");
            builder.Append("CapitalNumber=@CapitalNumber,");
            builder.Append("MonthPlanUID=@MonthPlanUID,");
            builder.Append("Beneficiary=@Beneficiary,");
            builder.Append("Bank=@Bank,");
            builder.Append("Account=@Account ");
            builder.Append(" where ID=@ID ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@ID", SqlDbType.NVarChar,1),
                new SqlParameter("@PaymentCode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@PaymentMoney", SqlDbType.Decimal, 9),
                new SqlParameter("@PaymentPerson", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@PaymentDate", 4),
                new SqlParameter("@Annex", SqlDbType.NVarChar, 0x100),
                new SqlParameter("@FlowState", SqlDbType.Int, 4),
                new SqlParameter("@Notes", SqlDbType.NVarChar),
                new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@InputDate", 4),
                new SqlParameter("@ContainPending", 2),
                new SqlParameter("@PayType", 0x10),
                new SqlParameter("@CapitalNumber", SqlDbType.NVarChar, 0x3e8),
                new SqlParameter("@MonthPlanUID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@Beneficiary", SqlDbType.NVarChar, 200),
                new SqlParameter("@Bank", SqlDbType.NVarChar, 200),
                new SqlParameter("@Account", SqlDbType.NVarChar, 200)
            };
            list[0].Value = model.ID;
            list[1].Value = model.PaymentCode;
            list[2].Value = model.ContractID;
            list[3].Value = model.PaymentMoney;
            list[4].Value = model.PaymentPerson;
            list[5].Value = model.PaymentDate;
            list[6].Value = model.Annex;
            list[7].Value = model.FlowState;
            list[8].Value = model.Notes;
            list[9].Value = model.InputPerson;
            list[10].Value = model.InputDate;
            list[11].Value = model.ContainPending;
            list[12].Value = model.Paytype;
            list[13].Value = model.Capitalnumber;
            if (model.MonthPlanUID != null)
            {
                list[14].Value = model.MonthPlanUID;
            }
            else
            {
                list[14].Value = DBNull.Value;
            }
            list[15].Value = model.Beneficiary;
            list[0x10].Value = model.Bank;
            list[0x11].Value = model.Account;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), list.ToArray());
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), list.ToArray());
            }
        }
    }
}

