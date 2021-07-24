namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayoutInvoice
    {
        public void Add(PayoutInvoiceInfo model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Payout_Invoice(");
            builder.Append("InvoiceID,ContractID,InvoiceNo,InvoiceCode,InvoiceType,TaxNo,InvoiceDate,Amount,Transactor,Annex,Payer,Payee,FlowState,InputPerson,InputDate,Notes,Contact,BankCode,OrganizationCode)");
            builder.Append(" values (");
            builder.Append("@InvoiceID,@ContractID,@InvoiceNo,@InvoiceCode,@InvoiceType,@TaxNo,@InvoiceDate,@Amount,@Transactor,@Annex,@Payer,@Payee,@FlowState,@InputPerson,@InputDate,@Notes,@Contact,@BankCode,@OrganizationCode)");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@InvoiceID", model.InvoiceID), new SqlParameter("@ContractID", model.ContractID), new SqlParameter("@InvoiceNo", model.InvoiceNo), new SqlParameter("@InvoiceCode", model.InvoiceCode), new SqlParameter("@InvoiceType", model.InvoiceType), new SqlParameter("@TaxNo", model.TaxNo), new SqlParameter("@InvoiceDate", model.InvoiceDate), new SqlParameter("@Amount", model.Amount), new SqlParameter("@Transactor", model.Transactor), new SqlParameter("@Annex", model.Annex), new SqlParameter("@Payer", model.Payer), new SqlParameter("@Payee", model.Payee), new SqlParameter("@FlowState", model.FlowState), new SqlParameter("@InputPerson", model.InputPerson), new SqlParameter("@InputDate", model.InputDate), new SqlParameter("@Notes", model.Notes), 
                new SqlParameter("@Contact", model.Contact), new SqlParameter("@BankCode", model.BankCode), new SqlParameter("@OrganizationCode", model.OrganizationCode)
             };
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public void Delete(List<string> invoiceIds)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in invoiceIds)
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

        public void Delete(string InvoiceID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Payout_Invoice ");
            builder.Append(" where InvoiceID=@InvoiceID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InvoiceID", InvoiceID) };
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public bool Exists(string invoiceNo)
        {
            string cmdText = string.Format("SELECT COUNT(1) FROM Con_Payout_Invoice WHERE InvoiceNo = @InvoiceNo", new object[0]);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InvoiceNo", invoiceNo) };
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters)) > 0);
        }

        public DataTable GetLedger(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT c.ContractID,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) AS PrjName,ContractCode,ContractName,TypeName,Convert(NVARCHAR(10),SignDate,121) AS SignDate,CodeName,ModifiedMoney,").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(PaymentMoney),0) FROM Con_Payout_Payment WHERE ContractID = c.ContractID) AS PaymentTotal,--付款累计").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(Amount),0) FROM Con_Payout_Invoice WHERE ContractID = c.ContractID) AS InvoiceTotal,--累计已到发票").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(PaymentMoney),0) FROM Con_Payout_Payment WHERE ContractID = c.ContractID) - (SELECT ISNULL(SUM(Amount),0) FROM Con_Payout_Invoice WHERE ContractID = c.ContractID) AS InvoiceDiff,--累计未到票额").AppendLine();
            builder.Append("ISNULL((SELECT SUM(Amount) FROM Con_Payout_Invoice WHERE ContractID = c.ContractID)/ NULLIF((SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID),0),0) AS InvoiceRate,--发票比率").AppendLine();
            builder.Append("BName,Con_Payout_Invoice.Notes,").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("\t   WHEN 1 THEN c.ContractID  ").AppendLine();
            builder.Append("\t   WHEN 0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append("END AS Version,").AppendLine();
            builder.Append("CASE IsMainContract").AppendLine();
            builder.Append("\t\tWHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("\t    WHEN 0 THEN (").AppendLine();
            builder.Append("                 SELECT c2.InputDate FROM Con_Payout_Contract c1").AppendLine();
            builder.Append("\t\t\t\t\tJOIN Con_Payout_Contract c2 ON c1.MainContractID = c2.ContractID").AppendLine();
            builder.Append("\t\t\t\t\tWHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("\t\t\t\t\t)").AppendLine();
            builder.Append("END AS Date,").AppendLine();
            builder.Append("IsMainContract, c.UserCodes,").AppendLine();
            builder.Append("ISNULL(CorpName,BName) AS CorpName ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Payment ON Con_Payout_Payment.ContractID = c.ContractID  ").AppendLine();
            builder.Append("AND Con_Payout_Payment.FlowState = '1'  ").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Invoice ON Con_Payout_Invoice.ContractID = c.ContractID ").AppendLine();
            builder.Append("LEFT JOIN Con_ContractType ON Con_ContractType.TypeID = c.TypeID  ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo ON PT_PrjInfo.PrjGuid = c.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB ON PT_PrjInfo_ZTB.PrjGuid = c.PrjGuid").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_CodeList ON NoteID = c.PayMode").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))").AppendLine();
            builder.Append("WHERE c.FlowState = '1'  ").AppendLine();
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" AND ").Append(strWhere).AppendLine();
            }
            builder.Append("GROUP BY c.ContractID,ContractCode,ContractName,BName,ContractMoney,").AppendLine();
            builder.Append("ModifiedMoney,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName,CodeName,Con_Payout_Invoice.Notes,IsMainContract,").AppendLine();
            builder.Append("MainContractID,c.InputDate,c.UserCodes,c.UserCodes,CorpName ").AppendLine();
            builder.Append("ORDER BY Date DESC, Version ASC  ").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<PayoutInvoiceInfo> GetList(IDataReader dr)
        {
            List<PayoutInvoiceInfo> list = new List<PayoutInvoiceInfo>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<PayoutInvoiceInfo> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Con_Payout_Invoice.*,ContractName,UserCodes ");
            builder.Append("FROM Con_Payout_Invoice ");
            builder.Append("JOIN Con_Payout_Contract ON Con_Payout_Invoice.ContractID = Con_Payout_Contract.ContractID ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append(" WHERE ").Append(strWhere);
            }
            builder.Append(" ORDER BY Con_Payout_Invoice.InputDate DESC");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        public PayoutInvoiceInfo GetModel(IDataReader dr)
        {
            return new PayoutInvoiceInfo { 
                InvoiceID = DBHelper.GetString(dr["InvoiceID"]), ContractID = DBHelper.GetString(dr["ContractID"]), ContractName = DBHelper.GetString(dr["ContractName"]), InvoiceNo = DBHelper.GetString(dr["InvoiceNo"]), InvoiceCode = DBHelper.GetString(dr["InvoiceCode"]), InvoiceType = DBHelper.GetString(dr["InvoiceType"]), TaxNo = DBHelper.GetString(dr["TaxNo"]), InvoiceDate = new DateTime?(DBHelper.GetDateTime(dr["InvoiceDate"])), Amount = new decimal?(DBHelper.GetDecimal(dr["Amount"])), Transactor = DBHelper.GetString(dr["Transactor"]), Annex = DBHelper.GetString(dr["Annex"]), Payer = DBHelper.GetString(dr["Payer"]), Payee = DBHelper.GetString(dr["Payee"]), FlowState = new int?(DBHelper.GetInt(dr["FlowState"])), InputPerson = DBHelper.GetString(dr["InputPerson"]), InputDate = new DateTime?(DBHelper.GetDateTime(dr["InputDate"])), 
                Notes = DBHelper.GetString(dr["Notes"]), Contact = DBHelper.GetString(dr["Contact"]), BankCode = DBHelper.GetString(dr["BankCode"]), UserCodes = DBHelper.GetString(dr["UserCodes"]), OrganizationCode = DBHelper.GetString(dr["OrganizationCode"])
             };
        }

        public PayoutInvoiceInfo GetModel(string InvoiceID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP(1) Con_Payout_Invoice.*,ContractName,UserCodes ");
            builder.Append("FROM Con_Payout_Invoice ");
            builder.Append("JOIN Con_Payout_Contract ON Con_Payout_Invoice.ContractID = Con_Payout_Contract.ContractID ");
            builder.Append("WHERE InvoiceID = @InvoiceID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InvoiceID", InvoiceID) };
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
            builder.Append("SELECT c.ContractID,ContractCode,ContractName,BName,ContractMoney,ModifiedMoney,c.conState,").AppendLine();
            builder.Append("ISNULL(SUM(Amount),0) AS InvoiceTotal,").AppendLine();
            builder.Append("ISNULL(SUM(Amount)/NULLIF((SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID),0),0) AS InvoiceRate,").AppendLine();
            builder.Append("Convert(NVARCHAR(10),SignDate,121) AS SignDate,TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) AS PrjName,YH.v_xm AS SignPersonName, ").AppendLine();
            builder.Append(" CASE IsMainContract  ").AppendLine();
            builder.Append(" \t   WHEN 1 THEN c.ContractID ").AppendLine();
            builder.Append(" \t   WHEN  0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append(" END AS Version,IsMainContract, ").AppendLine();
            builder.Append("CASE IsMainContract").AppendLine();
            builder.Append("\t\tWHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("\t    WHEN 0 THEN (").AppendLine();
            builder.Append("                 SELECT c2.InputDate FROM Con_Payout_Contract c1").AppendLine();
            builder.Append("\t\t\t\t\tJOIN Con_Payout_Contract c2 ON c1.MainContractID = c2.ContractID").AppendLine();
            builder.Append("\t\t\t\t\tWHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("\t\t\t\t\t)").AppendLine();
            builder.Append("END AS Date,").AppendLine();
            builder.Append("c.UserCodes,c.InputDate,ISNULL(CorpName,BName) AS CorpName,PrjType.CodeName   ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Invoice ON Con_Payout_Invoice.ContractID = c.ContractID ").AppendLine();
            builder.Append("LEFT JOIN Con_ContractType ON Con_ContractType.TypeID = c.TypeID ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo ON PT_PrjInfo.PrjGuid = c.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB on c.PrjGuid = PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append("LEFT JOIN PT_yhmc YH ON YH.v_yhdm = c.SignPerson").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))  ").AppendLine();
            builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
            builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
            builder.Append("WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
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
            builder.Append("GROUP BY c.ContractID,MainContractID,ContractCode,ContractName,");
            builder.Append("BName,ContractMoney,ModifiedMoney,c.conState,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName,IsMainContract,c.UserCodes,c.InputDate,CorpName,PrjType.CodeName , YH.v_xm ").AppendLine();
            builder.Append(") Y \n");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetReportDataSource(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append("SELECT c.ContractID,ContractCode,ContractName,BName,ContractMoney,ModifiedMoney,c.conState,").AppendLine();
            builder.Append("ISNULL(SUM(Amount),0) AS InvoiceTotal,").AppendLine();
            builder.Append("ISNULL(SUM(Amount)/NULLIF((SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID),0),0) AS InvoiceRate,").AppendLine();
            builder.Append("Convert(NVARCHAR(10),SignDate,121) AS SignDate,TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) as PrjName,YH.v_xm AS SignPersonName, ").AppendLine();
            builder.Append(" CASE IsMainContract  ").AppendLine();
            builder.Append(" \t   WHEN 1 THEN c.ContractID ").AppendLine();
            builder.Append(" \t   WHEN  0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append(" END AS Version,IsMainContract, ").AppendLine();
            builder.Append("CASE IsMainContract").AppendLine();
            builder.Append("\t\tWHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("\t    WHEN 0 THEN (").AppendLine();
            builder.Append("                 SELECT c2.InputDate FROM Con_Payout_Contract c1").AppendLine();
            builder.Append("\t\t\t\t\tJOIN Con_Payout_Contract c2 ON c1.MainContractID = c2.ContractID").AppendLine();
            builder.Append("\t\t\t\t\tWHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("\t\t\t\t\t)").AppendLine();
            builder.Append("END AS Date,").AppendLine();
            builder.Append("c.UserCodes,c.InputDate,ISNULL(CorpName,BName) AS CorpName,PrjType.CodeName   ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Invoice ON Con_Payout_Invoice.ContractID = c.ContractID ").AppendLine();
            builder.Append("LEFT JOIN Con_ContractType ON Con_ContractType.TypeID = c.TypeID ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo ON PT_PrjInfo.PrjGuid = c.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB on c.PrjGuid = PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append("LEFT JOIN PT_yhmc YH ON YH.v_yhdm = c.SignPerson").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))  ").AppendLine();
            builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
            builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
            builder.Append("WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
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
            builder.Append("GROUP BY c.ContractID,MainContractID,ContractCode,ContractName,");
            builder.Append("BName,ContractMoney,ModifiedMoney,c.conState,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName,IsMainContract,c.UserCodes,c.InputDate,CorpName,PrjType.CodeName, YH.v_xm ").AppendLine();
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public void Update(PayoutInvoiceInfo model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Payout_Invoice set ");
            builder.Append("ContractID=@ContractID,");
            builder.Append("InvoiceNo=@InvoiceNo,");
            builder.Append("InvoiceCode=@InvoiceCode,");
            builder.Append("InvoiceType=@InvoiceType,");
            builder.Append("TaxNo=@TaxNo,");
            builder.Append("InvoiceDate=@InvoiceDate,");
            builder.Append("Amount=@Amount,");
            builder.Append("Transactor=@Transactor,");
            builder.Append("Annex=@Annex,");
            builder.Append("Payer=@Payer,");
            builder.Append("Payee=@Payee,");
            builder.Append("FlowState=@FlowState,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate,");
            builder.Append("Notes=@Notes,");
            builder.Append("Contact=@Contact,");
            builder.Append("BankCode=@BankCode,");
            builder.Append("OrganizationCode=@OrganizationCode");
            builder.Append(" where InvoiceID=@InvoiceID ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@InvoiceID", model.InvoiceID), new SqlParameter("@ContractID", model.ContractID), new SqlParameter("@InvoiceNo", model.InvoiceNo), new SqlParameter("@InvoiceCode", model.InvoiceCode), new SqlParameter("@InvoiceType", model.InvoiceType), new SqlParameter("@TaxNo", model.TaxNo), new SqlParameter("@InvoiceDate", model.InvoiceDate), new SqlParameter("@Amount", model.Amount), new SqlParameter("@Transactor", model.Transactor), new SqlParameter("@Annex", model.Annex), new SqlParameter("@Payer", model.Payer), new SqlParameter("@Payee", model.Payee), new SqlParameter("@FlowState", model.FlowState), new SqlParameter("@InputPerson", model.InputPerson), new SqlParameter("@InputDate", model.InputDate), new SqlParameter("@Notes", model.Notes), 
                new SqlParameter("@Contact", model.Contact), new SqlParameter("@BankCode", model.BankCode), new SqlParameter("@OrganizationCode", model.OrganizationCode)
             };
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

