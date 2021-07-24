namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayoutContract
    {
        private cn.justwin.stockDAL.Purchase Purchase = new cn.justwin.stockDAL.Purchase();
        private cn.justwin.stockDAL.PurchaseStock PurchaseStock = new cn.justwin.stockDAL.PurchaseStock();

        public void Add(PayoutContractModel model, SqlTransaction trans)
        {
            string cmdText = "\r\n\t\t\t\tinsert into Con_Payout_Contract (\r\n\t\t\t\t\tContractID,ContractCode,ContractName,TypeID,IsMainContract,MainContractID,AName,BName,ContractMoney,\r\n\t\t\t\t\tModifiedMoney,PrepayMoney,MainItem,PaymentCondition,SignDate,StartDate,EndDate,BalanceMode,PayMode,\r\n\t\t\t\t\tAddress,Annex,FlowState,IsArchived,ArchiveDate,PrjGuid,InContractID,Notes,InputPerson,InputDate,\r\n\t\t\t\t\tUserCodes,fictitious,CapitalNumber,financeNumber,financeProject,SignPerson\r\n\t\t\t\t)\r\n\t\t\t\tvalues (\r\n\t\t\t\t\t@ContractID,@ContractCode,@ContractName,@TypeID,@IsMainContract,@MainContractID,@AName,@BName,@ContractMoney,\r\n\t\t\t\t\t@ModifiedMoney,@PrepayMoney,@MainItem,@PaymentCondition,@SignDate,@StartDate,@EndDate,@BalanceMode,@PayMode,\r\n\t\t\t\t\t@Address,@Annex,@FlowState,@IsArchived,@ArchiveDate,@PrjGuid,@InContractID,@Notes,@InputPerson,@InputDate,\r\n\t\t\t\t\t@UserCodes,@fictitious,@CapitalNumber,@financeNumber,@financeProject,@signPerson\r\n\t\t\t\t)\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractName", SqlDbType.NVarChar, 0x80), new SqlParameter("@TypeID", SqlDbType.NVarChar, 0x40), new SqlParameter("@IsMainContract", SqlDbType.Bit, 1), new SqlParameter("@MainContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@AName", SqlDbType.NVarChar, 80), new SqlParameter("@BName", SqlDbType.NVarChar, 0x400), new SqlParameter("@ContractMoney", SqlDbType.Decimal, 9), new SqlParameter("@ModifiedMoney", SqlDbType.Decimal, 9), new SqlParameter("@PrepayMoney", SqlDbType.Decimal, 9), new SqlParameter("@MainItem", SqlDbType.NVarChar, 0x400), new SqlParameter("@PaymentCondition", SqlDbType.NVarChar, 0x400), new SqlParameter("@SignDate", SqlDbType.DateTime), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), 
                new SqlParameter("@BalanceMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@Address", SqlDbType.NVarChar, 0x100), new SqlParameter("@Annex", SqlDbType.NVarChar, 0x100), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@IsArchived", SqlDbType.Bit, 1), new SqlParameter("@ArchiveDate", SqlDbType.DateTime), new SqlParameter("@PrjGuid", SqlDbType.NVarChar, 0x40), new SqlParameter("@InContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@UserCodes", SqlDbType.NVarChar), new SqlParameter("@fictitious", SqlDbType.Int), new SqlParameter("@CapitalNumber", SqlDbType.NVarChar, 200), new SqlParameter("@financeNumber", SqlDbType.NVarChar, 50), 
                new SqlParameter("@financeProject", SqlDbType.NVarChar, 50), new SqlParameter("@signPerson", SqlDbType.VarChar, 8)
             };
            commandParameters[0].Value = model.ContractID;
            commandParameters[1].Value = model.ContractCode;
            commandParameters[2].Value = model.ContractName;
            commandParameters[3].Value = model.TypeID;
            commandParameters[4].Value = model.IsMainContract;
            if (model.MainContractID != null)
            {
                commandParameters[5].Value = model.MainContractID;
            }
            else
            {
                commandParameters[5].Value = DBNull.Value;
            }
            commandParameters[6].Value = model.AName;
            commandParameters[7].Value = model.BName;
            if (model.ContractMoney.HasValue)
            {
                commandParameters[8].Value = model.ContractMoney;
            }
            else
            {
                commandParameters[8].Value = DBNull.Value;
            }
            if (model.ModifiedMoney.HasValue)
            {
                commandParameters[9].Value = model.ModifiedMoney;
            }
            else
            {
                commandParameters[9].Value = DBNull.Value;
            }
            if (model.PrepayMoney.HasValue)
            {
                commandParameters[10].Value = model.PrepayMoney;
            }
            else
            {
                commandParameters[10].Value = DBNull.Value;
            }
            commandParameters[11].Value = model.MainItem;
            commandParameters[12].Value = model.PaymentCondition;
            if (model.SignDate.HasValue)
            {
                commandParameters[13].Value = model.SignDate;
            }
            else
            {
                commandParameters[13].Value = DBNull.Value;
            }
            if (model.StartDate.HasValue)
            {
                commandParameters[14].Value = model.StartDate;
            }
            else
            {
                commandParameters[14].Value = DBNull.Value;
            }
            if (model.EndDate.HasValue)
            {
                commandParameters[15].Value = model.EndDate;
            }
            else
            {
                commandParameters[15].Value = DBNull.Value;
            }
            if (model.BalanceMode != null)
            {
                commandParameters[0x10].Value = model.BalanceMode;
            }
            else
            {
                commandParameters[0x10].Value = DBNull.Value;
            }
            if (model.PayMode != null)
            {
                commandParameters[0x11].Value = model.PayMode;
            }
            else
            {
                commandParameters[0x11].Value = DBNull.Value;
            }
            commandParameters[0x12].Value = model.Address;
            if (model.Annex != null)
            {
                commandParameters[0x13].Value = model.Annex;
            }
            else
            {
                commandParameters[0x13].Value = DBNull.Value;
            }
            commandParameters[20].Value = model.FlowState;
            commandParameters[0x15].Value = model.IsArchived;
            if (model.ArchiveDate.HasValue)
            {
                commandParameters[0x16].Value = model.ArchiveDate;
            }
            else
            {
                commandParameters[0x16].Value = DBNull.Value;
            }
            commandParameters[0x17].Value = model.PrjGuid;
            commandParameters[0x18].Value = model.InContractID;
            commandParameters[0x19].Value = model.Notes;
            commandParameters[0x1a].Value = model.InputPerson;
            commandParameters[0x1b].Value = model.InputDate;
            if (model.UserCodes != null)
            {
                commandParameters[0x1c].Value = model.UserCodes;
            }
            else
            {
                commandParameters[0x1c].Value = DBNull.Value;
            }
            commandParameters[0x1d].Value = model.Fictitious;
            commandParameters[30].Value = model.CapitalNumber;
            commandParameters[0x1f].Value = model.FinanceNumber;
            commandParameters[0x20].Value = model.FinanceProject;
            commandParameters[0x21].Value = model.SignPerson;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
            }
        }

        public void Delete(List<string> contractIds)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in contractIds)
                    {
                        PurchaseModel modelByContractId = new PurchaseModel();
                        modelByContractId = this.Purchase.GetModelByContractId(str);
                        if (modelByContractId != null)
                        {
                            List<string> lstPcode = new List<string> {
                                modelByContractId.pcode
                            };
                            this.Purchase.Delete(lstPcode);
                        }
                        this.Delete(str, trans);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new Exception("删除失败！");
                }
            }
        }

        public void Delete(string ContractID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Payout_Contract ");
            builder.Append(" where ContractID=@ContractID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = ContractID;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public bool Exists(string contractCode)
        {
            string cmdText = "SELECT COUNT(1) FROM Con_Payout_Contract WHERE ContractCode = @ContractCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ContractCode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = contractCode;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters)) > 0);
        }

        private List<PayoutContractModel> GetList(IDataReader dr)
        {
            List<PayoutContractModel> list = new List<PayoutContractModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<PayoutContractModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT c.*,ISNULL(TypeName,'') TypeName,PrjName,X1.CodeName AS BalanceModeName,X2.CodeName as PayModeName, ").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("\tWHEN '1' THEN ContractID ").AppendLine();
            builder.Append("\tWHEN '0' THEN MainContractID + ContractID ").AppendLine();
            builder.Append("END AS Version,").AppendLine();
            builder.Append("CASE IsMainContract").AppendLine();
            builder.Append("\tWHEN '1' THEN c.InputDate").AppendLine();
            builder.Append("\tWHEN '0' THEN (").AppendLine();
            builder.Append("\t\t\t\t\tSELECT c2.InputDate FROM Con_Payout_Contract c1").AppendLine();
            builder.Append("\t\t\t\t\tJOIN Con_Payout_Contract c2 ON c1.MainContractID = c2.ContractID").AppendLine();
            builder.Append("\t\t\t\t\tWHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("\t\t\t\t\t)").AppendLine();
            builder.Append("END AS Date,conState,ISNULL(CorpName,null) AS CorpName ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_ContractType ON c.TypeID = Con_ContractType.TypeID ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo ON c.PrjGuid = PT_PrjInfo.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X1 ON c.BalanceMode = X1.NoteID ").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X2 ON c.PayMode = X2.NoteID ").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024)) ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" WHERE ").Append(strWhere).AppendLine();
                builder.Append(" AND c.PrjGuid!=''");
            }
            else
            {
                builder.Append(" WHERE c.PrjGuid!=''");
            }
            builder.Append(" ORDER BY Date DESC, Version ASC").AppendLine();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        public List<PayoutContractModel> GetList(string strWhere, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT c.*,ISNULL(TypeName,'') TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName ) as PrjName, \n");
            builder.Append("\tX1.CodeName AS BalanceModeName,X2.CodeName as PayModeName, YH.v_xm AS SignPersonName, \n");
            builder.Append("CASE IsMainContract  \n");
            builder.Append("\tWHEN '1' THEN ContractID  \n");
            builder.Append("\tWHEN '0' THEN MainContractID + ContractID  \n");
            builder.Append("END AS Version, \n");
            builder.Append("CASE IsMainContract \n");
            builder.Append("\tWHEN '1' THEN c.InputDate \n");
            builder.Append("\tWHEN '0' THEN ( \n");
            builder.Append("\t\t\t\t\tSELECT c2.InputDate FROM Con_Payout_Contract c1 \n");
            builder.Append("\t\t\t\t\tJOIN Con_Payout_Contract c2 ON c1.MainContractID = c2.ContractID \n");
            builder.Append("\t\t\t\t\tWHERE c1.ContractID = c.ContractID \n");
            builder.Append("\t\t\t\t\t) \n");
            builder.Append("END AS Date,conState,ISNULL(CorpName,null) AS CorpName  \n");
            builder.Append("FROM Con_Payout_Contract c \n");
            builder.Append("LEFT JOIN Con_ContractType ON c.TypeID = Con_ContractType.TypeID  \n");
            builder.Append("LEFT JOIN PT_PrjInfo ON c.PrjGuid = PT_PrjInfo.PrjGuid  \n");
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB on c.PrjGuid = PT_PrjInfo_ZTB.PrjGuid \n");
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X1 ON c.BalanceMode = X1.NoteID  \n");
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X2 ON c.PayMode = X2.NoteID  \n");
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))  \n");
            builder.Append("LEFT JOIN PT_yhmc YH ON YH.v_yhdm = c.SignPerson \n");
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" WHERE ").Append(strWhere).AppendLine();
                builder.Append(" AND c.PrjGuid!=''");
            }
            else
            {
                builder.Append(" WHERE c.PrjGuid!=''");
            }
            builder.Append(" AND c.UserCodes LIKE '%" + userCode + "%'");
            builder.Append(" ORDER BY Date DESC, Version ASC").AppendLine();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        public DataTable GetList(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append("SELECT c.*,ISNULL(TypeName,'') TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName ) as PrjName, \n");
            builder.Append("\tX1.CodeName AS BalanceModeName,X2.CodeName as PayModeName,YH.v_xm AS SignPersonName, \n");
            builder.Append("CASE IsMainContract \n");
            builder.Append("\tWHEN '1' THEN ContractID \n");
            builder.Append("\tWHEN '0' THEN MainContractID + ContractID \n");
            builder.Append("END AS Version, \n");
            builder.Append("CASE IsMainContract \n");
            builder.Append("\tWHEN '1' THEN c.InputDate \n");
            builder.Append("\tWHEN '0' THEN ( \n");
            builder.Append("\t\t\t\t\tSELECT c2.InputDate FROM Con_Payout_Contract c1 \n");
            builder.Append("\t\t\t\t\tJOIN Con_Payout_Contract c2 ON c1.MainContractID = c2.ContractID \n");
            builder.Append("\t\t\t\t\tWHERE c1.ContractID = c.ContractID \n");
            builder.Append("\t\t\t\t\t) \n");
            builder.Append("END AS Date,conState AS state,ISNULL(CorpName,null) AS CorpName  \n");
            builder.Append("FROM Con_Payout_Contract c \n");
            builder.Append("LEFT JOIN Con_ContractType ON c.TypeID = Con_ContractType.TypeID  \n");
            builder.Append("LEFT JOIN PT_PrjInfo ON c.PrjGuid = PT_PrjInfo.PrjGuid  \n");
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB on c.PrjGuid = PT_PrjInfo_ZTB.PrjGuid \n");
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X1 ON c.BalanceMode = X1.NoteID  \n");
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X2 ON c.PayMode = X2.NoteID  \n");
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))  \n");
            builder.Append("LEFT JOIN PT_yhmc YH ON YH.v_yhdm = c.SignPerson \n");
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" WHERE ").Append(strWhere).AppendLine();
                builder.Append(" AND c.PrjGuid!=''");
            }
            else
            {
                builder.Append(" WHERE c.PrjGuid!=''");
            }
            builder.Append(" AND c.UserCodes LIKE '%" + userCode + "%'");
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList_New(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT cpc.ContractID ,cpc.ContractCode ,cpc.ContractName ,cpc.BName  ");
            builder.Append(" ,CONVERT(DECIMAL(18,3),cpc.ModifiedMoney) AS ModifiedMoney  ");
            builder.Append(",ct.TypeName ,CONVERT(VARCHAR(100) ,cpc.InputDate ,23) AS SignDate  ");
            builder.Append(" ,CONVERT(DECIMAL(18,3),SUM(PaymentMoney)) AS PaymentMoney ");
            builder.Append(",CONVERT(DECIMAL(18,3),SUM(BalanceMoney)) AS BalanceMoney ");
            builder.Append(" FROM   Con_Payout_Contract cpc ");
            builder.Append(" LEFT JOIN Con_ContractType ct  ON  cpc.TypeID = ct.TypeID ");
            builder.Append(" LEFT JOIN PT_PrjInfo ON  cpc.PrjGuid = PT_PrjInfo.PrjGuid ");
            builder.Append(" LEFT JOIN XPM_Basic_CodeList AS X1 ON  cpc.BalanceMode = X1.NoteID ");
            builder.Append(" LEFT JOIN XPM_Basic_CodeList AS X2 ON  cpc.PayMode = X2.NoteID ");
            builder.Append(" LEFT JOIN XPM_Basic_ContactCorp ON  BName = CAST(CorpId AS NVARCHAR(1024)) ");
            builder.Append(" LEFT JOIN Con_Payout_Balance cpb ON  cpb.ContractID = cpc.ContractID ");
            builder.Append(" LEFT JOIN Con_Payout_Payment cpp ON  cpp.ContractID = cpc.ContractID ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append("WHERE ");
                builder.Append(strWhere).Append(" ");
                builder.Append("AND cpc.flowstate = 1 ");
                builder.Append(" ");
                builder.Append(" AND cpc.PrjGuid!=''");
            }
            else
            {
                builder.Append(" WHERE cpc.PrjGuid!=''");
            }
            builder.Append("GROUP BY cpc.ContractID,cpc.ContractCode ,cpc.ContractName,cpc.BName ");
            builder.Append(",cpc.ModifiedMoney,ct.TypeName , cpc.InputDate ");
            builder.Append("ORDER BY CONVERT(VARCHAR(100) ,cpc.InputDate ,23) DESC ");
            builder.Append(" ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        private PayoutContractModel GetModel(IDataReader dr)
        {
            PayoutContractModel model = new PayoutContractModel {
                ContractID = DBHelper.GetString(dr["ContractID"]),
                ContractCode = DBHelper.GetString(dr["ContractCode"]),
                ContractName = DBHelper.GetString(dr["ContractName"])
            };
            if (dr["TypeID"] != null)
            {
                model.TypeID = DBHelper.GetString(dr["TypeID"]);
            }
            else
            {
                model.TypeID = "";
            }
            model.TypeName = DBHelper.GetString(dr["TypeName"]);
            model.IsMainContract = DBHelper.GetBool(dr["IsMainContract"]);
            model.MainContractID = DBHelper.GetString(dr["MainContractID"]);
            model.AName = DBHelper.GetString(dr["AName"]);
            model.BName = DBHelper.GetString(dr["BName"]);
            model.ContractMoney = new decimal?(DBHelper.GetDecimal(dr["ContractMoney"]));
            model.ModifiedMoney = new decimal?(DBHelper.GetDecimal(dr["ModifiedMoney"]));
            model.PrepayMoney = new decimal?(DBHelper.GetDecimal(dr["PrepayMoney"]));
            model.MainItem = DBHelper.GetString(dr["MainItem"]);
            model.PaymentCondition = DBHelper.GetString(dr["PaymentCondition"]);
            model.SignDate = DBHelper.GetDateTimeNullable(dr["SignDate"]);
            model.StartDate = DBHelper.GetDateTimeNullable(dr["StartDate"]);
            model.EndDate = DBHelper.GetDateTimeNullable(dr["EndDate"]);
            model.BalanceMode = DBHelper.GetString(dr["BalanceMode"]);
            model.BalanceModeName = DBHelper.GetString(dr["BalanceModeName"]);
            model.PayMode = DBHelper.GetString(dr["PayMode"]);
            model.PayModeName = DBHelper.GetString(dr["PayModeName"]);
            model.Address = DBHelper.GetString(dr["Address"]);
            model.Annex = DBHelper.GetString(dr["Annex"]);
            model.FlowState = new int?(DBHelper.GetInt(dr["FlowState"]));
            model.IsArchived = DBHelper.GetBool(dr["IsArchived"]);
            model.ArchiveDate = DBHelper.GetDateTimeNullable(dr["ArchiveDate"]);
            model.PrjGuid = DBHelper.GetString(dr["PrjGuid"]);
            model.PrjName = DBHelper.GetString(dr["PrjName"]);
            model.InContractID = DBHelper.GetString(dr["InContractID"]);
            model.Notes = DBHelper.GetString(dr["Notes"]);
            model.InputPerson = DBHelper.GetString(dr["InputPerson"]);
            model.InputDate = new DateTime?(DBHelper.GetDateTime(dr["InputDate"]));
            model.UserCodes = DBHelper.GetString(dr["UserCodes"]);
            model.Fictitious = DBHelper.GetInt(dr["Fictitious"]);
            model.ConState = DBHelper.GetInt(dr["conState"]);
            model.CapitalNumber = DBHelper.GetString(dr["CapitalNumber"]);
            model.FinanceNumber = DBHelper.GetString(dr["financeNumber"]);
            model.FinanceProject = DBHelper.GetString(dr["financeProject"]);
            model.CorpName = DBHelper.GetString(dr["CorpName"]);
            model.SignPerson = DBHelper.GetString(dr["SignPerson"]);
            return model;
        }

        public PayoutContractModel GetModel(string ContractID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Con_Payout_Contract.*,ISNULL(TypeName,'') TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) AS PrjName,X1.CodeName AS BalanceModeName,X2.CodeName as PayModeName,conState ");
            builder.Append(",ISNULL(CorpName,null) AS CorpName FROM Con_Payout_Contract ");
            builder.Append("LEFT JOIN Con_ContractType ON Con_Payout_Contract.TypeID = Con_ContractType.TypeID ");
            builder.Append("LEFT JOIN PT_PrjInfo ON Con_Payout_Contract.PrjGuid = PT_PrjInfo.PrjGuid ");
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB ON Con_Payout_Contract.PrjGuid =PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X1 ON Con_Payout_Contract.BalanceMode = X1.NoteID ");
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X2 ON Con_Payout_Contract.PayMode = X2.NoteID ");
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024)) ");
            builder.Append(" WHERE ContractID=@ContractID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = ContractID;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public DataTable GetModifyTaskDetail(string prjId, string sWhere, int pageSize, int pageIndex)
        {
            string cmdText = string.Format(string.Concat(new object[] { "SELECT TOP ", pageSize, " *  FROM(\r\n                           SELECT ROW_NUMBER() OVER(ORDER BY CT.OrderNumber)AS NUM, CT.TaskCode,CT.TaskId,CT.ModifyType,CT.TaskName,CT.Unit,CT.UnitPrice,\r\n                            CT.oQty,CT.CQuantity,CT.CTotal,CT.Quantity,\r\n                            CT.CTotal-(CT.oTotal2+ISNULL(SUM(BTS.ResourcePrice*BTS.ResourceQuantity),0.0))AS Total2,\r\n                            CT.oTotal2+ISNULL(SUM(BTS.ResourcePrice*BTS.ResourceQuantity),0.0)AS oTotal2 FROM \r\n                            (\r\n                            SELECT T.TaskCode,T.TaskId,T.ModifyType,T.TaskName,T.Unit,T.OrderNumber,\r\n                            ISNULL((ISNULL(T.Total2,0.0)/NULLIF(T.Quantity,0.0)),0.0) AS UnitPrice,\r\n                            (T.Quantity-SUM(M.Quantity))AS oQty,  --变更前工程量\r\n                            T.Quantity AS CQuantity ,        --变更后工程量\r\n                            ISNULL(T.Total,0.0)AS oTotal2,   --变更前小计\r\n                            ISNULL(T.Total2,0.0) AS CTotal,              --变更后小计\r\n                            SUM(M.Quantity)AS Quantity                         \r\n                            FROM  Bud_Task T\r\n                            LEFT JOIN \r\n                            Bud_ModifyTask M\r\n                            ON  T.TaskId=M.TaskId         \r\n                            WHERE T.PrjId='{0}' AND T.ModifyId!='' ", sWhere, "\r\n                            GROUP BY T.OrderNumber,T.TaskCode,T.TaskId,T.ModifyType,T.TaskName,T.Unit,T.Total2,T.Quantity,T.Total,M.Quantity\r\n                            ) CT\r\n                            LEFT JOIN  Bud_TaskResource BTS \r\n                            ON CT.TaskId=BTS.TaskId\r\n                            GROUP BY CT.TaskCode,CT.TaskId,CT.ModifyType,CT.TaskName,CT.Unit,CT.UnitPrice,\r\n                            CT.oQty,CT.CQuantity,CT.CTotal,CT.oTotal2,CT.OrderNumber,CT.Quantity\r\n                            ) Task WHERE NUM>", pageSize * (pageIndex - 1) }), prjId);
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public DataTable GetMonthStatistics(string prjId, string beginTime, string endTime, string sWhere)
        {
            string cmdText = "--DECLARE @prjId NVARCHAR(200)\r\n                            --DECLARE @sDate NVARCHAR(200)\r\n                            --DECLARE @eDate NVARCHAR(200)\r\n                            --SET @prjId='c6124819-a067-4b5a-b7ff-290e40743621';\r\n                            --SET @sDate='2014/6/1';\r\n                            --SET @eDate='2014/6/30';\r\n                            WITH Task AS\r\n                            (\r\n                            SELECT CT.NUM,CT.TaskCode,CT.TaskId,CT.ModifyType,CT.TaskName,CT.Unit,CT.OrderNumber,CT.UnitPrice,\r\n                            CT.SubCount,CT.Quantity,CT.mtQty,CT.mTotal2,\r\n                            CT.Total+ISNULL(SUM(BTS.ResourcePrice*BTS.ResourceQuantity),0.0)AS Total  FROM\r\n                            (\r\n                            SELECT  ROW_NUMBER() OVER(ORDER BY T.OrderNumber)AS NUM, T.TaskCode,T.TaskId,T.ModifyType,T.TaskName,T.Unit,T.OrderNumber,\r\n                            ISNULL((ISNULL(T.Total2,0.0)/NULLIF(T.Quantity,0.0)),0.0) AS UnitPrice,\r\n                            dbo.fn_GetCount(T.TaskId) AS SubCount,\r\n                            (T.Quantity-SUM(ISNULL(M.Quantity,0.0)))AS Quantity,  --变更前工程量\r\n                            T.Quantity AS mtQty ,        --变更后工程量\r\n                            ISNULL(T.Total,0.0) AS Total,   --变更前小计\r\n                            ISNULL(T.Total2,0.0) AS mTotal2              --变更后小计\r\n                            FROM  Bud_Task T\r\n                            LEFT JOIN \r\n                            Bud_ModifyTask M\r\n                            ON  T.TaskId=M.TaskId\r\n                            WHERE T.PrjId=@prjId \r\n                            GROUP BY T.OrderNumber,T.TaskCode,T.TaskId,T.ModifyType,T.TaskName,T.Unit,T.Total2,T.Quantity,T.Total,M.Quantity,T.OrderNumber\r\n                            ) CT\r\n                            LEFT JOIN  Bud_TaskResource BTS \r\n                            ON CT.TaskId=BTS.TaskId\r\n                            GROUP BY CT.NUM,CT.TaskCode,CT.TaskId,CT.ModifyType,CT.TaskName,CT.Unit,CT.OrderNumber,CT.UnitPrice,\r\n                            CT.SubCount,CT.Quantity,CT.mtQty,CT.mTotal2,CT.Total\r\n                            ),\r\n                            MPT AS\r\n                            (\r\n                            SELECT ConsReportId FROM Bud_ConsReport WHERE InputDate>@sDate AND InputDate<@eDate\r\n                            AND PrjId=@prjId AND IsValid=1\r\n                            ),MPI AS\r\n                            (\r\n                            SELECT SUM(CompleteQuantity) AS cQty,TaskId FROM Bud_ConsTask CTK\r\n                            JOIN MPT ON  CTK.ConsReportId=MPT.ConsReportId\r\n                            GROUP BY TaskId\r\n                            ),TPT AS\r\n                            (\r\n                            SELECT ConsReportId FROM Bud_ConsReport WHERE PrjId=@prjId AND IsValid=1\r\n                            ),TPI AS\r\n                            (\r\n                            SELECT SUM(CompleteQuantity) AS tQty,TaskId FROM Bud_ConsTask CTK\r\n                            JOIN TPT ON  CTK.ConsReportId=TPT.ConsReportId\r\n                            GROUP BY TaskId\r\n                            )\r\n                            SELECT Task.NUM,Task.TaskCode,Task.TaskId,Task.ModifyType,Task.TaskName,Task.Unit,Task.UnitPrice,\r\n                            Task.Quantity,Task.mtQty,Task.Total,Task.mTotal2,Task.OrderNumber,Task.SubCount,\r\n                            ISNULL(MPI.cQty,0.0) AS cQty ,                                 --本月完成量\r\n                            ISNULL(ISNULL(MPI.cQty,0.0)*ISNULL(Task.UnitPrice,0.0),0.0) AS cTotal, --本月产值\r\n                            ISNULL(TPI.tQty,0.0) AS tQty,                                  --总完成量\r\n                            ISNULL(ISNULL(TPI.tQty,0.0)*ISNULL(Task.UnitPrice,0.0),0.0) AS tTotal  --总产值\r\n                            FROM Task LEFT JOIN MPI\r\n                            ON Task.TaskId=MPI.TaskId\r\n                            LEFT JOIN TPI\r\n                            ON Task.TaskId=TPI.TaskId WHERE 1=1   " + sWhere;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@sDate", beginTime), new SqlParameter("@eDate", endTime) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetMonthStatisticsTable(string prjId, string beginTime, string endTime, string sWhere)
        {
            string cmdText = "WITH cte_TaskModify AS\r\n                            (\r\n\t                            --查询节点变更的信息\r\n\t                            SELECT T.TaskId, MT.ModifyType\r\n\t\t                            , ISNULL(SUM(MT.Quantity), 0.0) AS mQty\t\t\t\t\t--变更的工程量\r\n                                    , ISNULL(SUM(MT.Total2), 0.0) AS mTotal2\t\t\t\t--变更的小计\r\n\t                            FROM Bud_Task AS T\r\n\t                            LEFT JOIN v_BudModifyTask AS MT ON MT.TaskId = T.TaskId AND MT.FlowState = '1' AND MT.ModifyType = '1'\t--清单内变更 \r\n                                WHERE T.PrjId = @prjId AND T.TaskType = '' \r\n\t                            GROUP BY T.TaskId, MT.ModifyType\r\n                            )\r\n                            --SELECT * FROM cte_TaskModify\r\n\r\n                            SELECT Row_Number() Over(ORDER BY UT.OrderNumber) AS Num, UT.ParentId, UT.TaskId, UT.OrderNumber, UT.TaskCode, UT.TaskName, UT.Unit,UT.ModifyType\r\n\t                            , CONVERT(numeric(18,3),ISNULL(ISNULL(mTotal2 ,0.0)/NULLIF((UT.Quantity + UT.mQty),0.0),0.0)) AS UnitPrice\t\t--单价\t\t\r\n\t                            , UT.Quantity\t\t--工程量\r\n\t                            , UT.Total          --小计\r\n\t                            , UT.Total2\t\t\t\r\n\t                            , UT.Quantity + UT.mQty AS mtQty\t--变更后工程量\r\n\t                            , mTotal2\t--变更后小计\r\n\t                            , UT.cQty\t\t\t\t\t\t\t--本月完成量\r\n\t                            , CONVERT(numeric(18,3),UT.cQty *CONVERT(numeric(18,2),ISNULL(ISNULL(mTotal2 ,0.0)/NULLIF((UT.Quantity + UT.mQty),0.0),0.0))) AS cTotal\t--本月产值\r\n\t                            , UT.tQty\t\t\t\t\t\t\t--累计完成量\r\n\t                            , CONVERT(numeric(18,3),UT.tQty *CONVERT(numeric(18,2),ISNULL(ISNULL(mTotal2 ,0.0)/NULLIF((UT.Quantity + UT.mQty),0.0),0.0))) AS tTotal\t--累计完成产值\r\n                            FROM \r\n                            (\r\n                                SELECT T.ParentId, T.TaskId, T.OrderNumber, T.TaskCode, T.TaskName, T.Unit, T.Quantity,MT.ModifyType\r\n                                    , ROUND(ISNULL(MT.mTotal2 /NULLIF((MT.mQty+T.Quantity),0.0),0.0),3) AS UnitPrice\r\n                                    , ISNULL(T.Total,0.0) AS Total\r\n                                    , ISNULL(T.Total2,0.0) AS Total2\r\n                                    , MT.mQty\t\t\t\t\t--变更的工程量\r\n                                    , dbo.fn_GetTotal(T.TaskId) AS mTotal2\t\t\t\t--变更后的小计\r\n                                    , ISNULL(SUM(CT2.CompleteQuantity), 0.0) AS cQty\t\t--本月完成量\r\n                                    , ISNULL(SUM(CT.CompleteQuantity), 0.0) AS tQty\t\t\t--累计完成量\r\n                                FROM Bud_Task AS T\r\n                                LEFT JOIN cte_TaskModify AS MT ON MT.TaskId = T.TaskId \r\n\t                            LEFT JOIN v_BudConsTask AS CT ON CT.TaskId = T.TaskId AND CT.IsValid = '1' AND CT.FlowState = '1'\r\n                                LEFT JOIN v_BudConsTask AS CT2 ON CT2.TaskId = T.TaskId AND CT2.IsValid = '1' AND CT2.FlowState = '1' \r\n                                    AND CT2.InputDate >= @sDate AND CT2.InputDate < @eDate AND CT.ConsTaskId = CT2.ConsTaskId\r\n                                WHERE T.PrjId = @prjId AND T.TaskType = ''\r\n                               GROUP BY T.ParentId, T.TaskId, T.OrderNumber, T.TaskCode, T.TaskName, T.Unit, T.Quantity, T.UnitPrice, T.Total, T.Total2, MT.ModifyType, MT.mQty, MT.mTotal2\r\n                                UNION\r\n                                SELECT T.ParentId, T.TaskId, T.OrderNumber, T.ModifyTaskCode AS TaskCode, T.ModifyTaskContent AS TaskName, T.Unit,T.ModifyType\r\n                                    ,0.0 AS Quantity\t\t\t\t\t\t--清单外的原工程量记为0\r\n                                    , CONVERT(decimal,ISNULL(T.Total2 /NULLIF(T.Quantity,0.0),0.0)) AS UnitPrice\r\n                                    , 0.0 AS Total\r\n                                    , 0.0 AS Total2\t\t\t\t\t\t\t--清单外的原小计记为0\r\n                                    , T.Quantity AS mQty\t\t\t\t\t--变更的工程量\r\n                                    , dbo.fn_GetModifyTotal(T.ModifyTaskId) AS mTotal2\t\t\t\t--变更后的小计\r\n                                    , ISNULL(SUM(CT2.CompleteQuantity), 0.0) AS cQty\t\t--本月完成量\r\n                                    , ISNULL(SUM(CT.CompleteQuantity), 0.0) AS tQty\t\t--累计完成量\r\n                                FROM v_BudModifyTask AS T\r\n                                LEFT JOIN v_BudConsTask AS CT ON CT.TaskId = T.TaskId AND CT.IsValid = '1' AND CT.FlowState = '1'\r\n                                LEFT JOIN v_BudConsTask AS CT2 ON CT2.TaskId = T.TaskId AND CT2.IsValid = '1' AND CT2.FlowState = '1' \r\n                                    AND CT2.InputDate >= @sDate AND CT2.InputDate < @eDate \r\n                                WHERE T.PrjId2 = @prjId AND T.FlowState = '1' AND T.ModifyType = '0' \r\n                                GROUP BY T.ParentId, T.TaskId, T.OrderNumber, T.ModifyTaskCode, T.ModifyTaskContent, T.Unit, T.Quantity, T.UnitPrice,T.Total,T.Total2,T.ModifyType,T.ModifyTaskId\r\n                            ) UT WHERE 1=1 " + sWhere;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@sDate", beginTime), new SqlParameter("@eDate", endTime) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetPrjTaskList(string prjId, string sWhere, int pageSize, int pageIndex)
        {
            string cmdText = string.Concat(new object[] { "SELECT TOP ", pageSize, " * FROM(\r\n\t\t\t\t\t\t\tSELECT  Row_Number() Over(ORDER BY MT.OrderNumber) AS Num,MT.ModifyTaskId, MT.TaskId, MT.ModifyTaskCode, MT.ModifyTaskContent, MT.Unit, \r\n\t\t\t\t\t\t\t\tMT.UnitPrice, MT.Quantity, MT.Total2, MT.ModifyType, MT.OrderNumber,\r\n\t\t\t\t\t\t\t\tISNULL(T.Quantity, 0.0) AS oQty,\t\t\t--变更前工程量\r\n\t\t\t\t\t\t\t\tISNULL(T.Total2, 0.0) AS oTotal2, --变更前小计\r\n\t\t\t\t\t\t\t\t(ISNULL(T.Quantity,0)+ISNULL(MT.Quantity,0)) AS CQuantity, --变更后工程量小计\r\n\t\t\t\t\t\t\t\t(IsNull(T.Total2,0)+ISNULL(MT.Total2,0)) AS CTotal        --变更后小计\r\n\t\t\t\t\t\t\tFROM Bud_ModifyTask AS MT\r\n\t\t\t\t\t\t\tINNER JOIN Bud_Modify AS M ON MT.ModifyId = M.ModifyId\r\n\t\t\t\t\t\t\tLEFT JOIN Bud_Task AS T ON MT.TaskId = T.TaskId AND MT.ModifyType = '1'\t\t--只有清单内的变更才关联Bud_Task表\r\n\t\t\t\t\t\t\tWHERE M.PrjId = @prjId AND M.FlowState = '1'\r\n\t\t\t\t\t\t\t) A WHERE Num>", pageSize * (pageIndex - 1) });
            SqlParameter parameter = new SqlParameter("@prjId", prjId);
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public int GetPrjTaskListCount(string prjId, string sWhere)
        {
            string cmdText = "SELECT count(ModifyTaskCode)AS NUM FROM\r\n\t\t\t\t\t\t\t(\r\n\t\t\t\t\t\t\tSELECT ModifyTaskCode \r\n\t\t\t\t\t\t\tFROM Bud_ModifyTask AS MT\r\n\t\t\t\t\t\t\tINNER JOIN Bud_Modify AS M ON MT.ModifyId = M.ModifyId\r\n\t\t\t\t\t\t\tLEFT JOIN Bud_Task AS T ON MT.TaskId = T.TaskId AND MT.ModifyType = '1'\t\t\r\n\t\t\t\t\t\t\tWHERE M.PrjId = @prjId AND M.FlowState = '1' " + sWhere + ") T ";
            SqlParameter parameter = new SqlParameter("@prjId", prjId);
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }));
        }

        public DataTable GetReportData(string strWhere, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append("SELECT c.ContractID,ContractCode,ContractName,BName,ContractMoney,ModifiedMoney,c.conState, ").AppendLine();
            builder.Append("\tConvert(NVARCHAR(10),SignDate,121) AS SignDate,TypeName,").AppendLine();
            builder.Append("\tISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) as PrjName, YH.v_xm AS SignPersonName, ").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(ModifyMoney),0) FROM Con_Payout_Modify WHERE ContractID = c.ContractID AND FlowState = 1) AS ModifyTotal,--变更累计").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(BalanceMoney),0) FROM Con_Payout_Balance WHERE ContractID = c.ContractID AND FlowState = 1) AS BalanceTotal,--结算累计").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(PaymentMoney),0) FROM Con_Payout_Payment WHERE ContractID = c.ContractID AND FlowState = 1) AS PaymentTotal,--付款累计").AppendLine();
            builder.Append("ISNULL((SELECT SUM(ModifyMoney) FROM Con_Payout_Modify WHERE ContractID = c.ContractID AND FlowState = 1)/NULLIF(ContractMoney,0),0) AS ModifyRate,--变更比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(BalanceMoney) FROM Con_Payout_Balance WHERE ContractID = c.ContractID AND FlowState = 1)/NULLIF(ModifiedMoney,0),0) AS BalanceRate,--结算比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID AND FlowState = 1)/NULLIF(ModifiedMoney,0),0) AS PaymentRate,--付款比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID AND FlowState = 1)").AppendLine();
            builder.Append("\t/(SELECT SUM(BalanceMoney) FROM Con_Payout_Balance WHERE ContractID = c.ContractID AND FlowState = 1),0) AS PaymentBalanceRate,--付款结算比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(Amount) FROM Con_Payout_Invoice WHERE ContractID = c.ContractID AND FlowState = 1),0) AS InvoiceSum,--发票比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(Amount) FROM Con_Payout_Invoice WHERE ContractID = c.ContractID AND FlowState = 1) ").AppendLine();
            builder.Append("\t/(SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID AND FlowState = 1),0) AS InvoiceRate,--已开发票比率").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("\t   WHEN 1 THEN c.ContractID ").AppendLine();
            builder.Append("\t   WHEN  0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append("END AS Version,").AppendLine();
            builder.Append("CASE IsMainContract").AppendLine();
            builder.Append("\tWHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("\tWHEN 0 THEN (").AppendLine();
            builder.Append("\t\t\t\tSELECT c2.InputDate ").AppendLine();
            builder.Append("\t\t\t\tFROM Con_Payout_Contract c1 ").AppendLine();
            builder.Append("\t\t\t\tJOIN Con_Payout_Contract c2 ON c1.MainContractID = c2.ContractID").AppendLine();
            builder.Append("\t\t\t\tWHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("\t\t\t\t)").AppendLine();
            builder.Append("END AS Date").AppendLine();
            builder.Append(",IsMainContract, c.UserCodes,ISNULL(CorpName,BName) AS CorpName,PrjType.CodeName  ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Modify ON Con_Payout_Modify.ContractID = c.ContractID ").AppendLine();
            builder.Append("AND Con_Payout_Modify.FlowState = '1' ").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Balance ON Con_Payout_Balance.ContractID = c.ContractID ").AppendLine();
            builder.Append("AND Con_Payout_Balance.FlowState = '1' ").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Payment ON Con_Payout_Payment.ContractID = c.ContractID ").AppendLine();
            builder.Append("AND Con_Payout_Payment.FlowState = '1' ").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Invoice ON Con_Payout_Invoice.ContractID = c.ContractID ").AppendLine();
            builder.Append("LEFT JOIN Con_ContractType ON Con_ContractType.TypeID = c.TypeID").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo ON PT_PrjInfo.PrjGuid = c.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB on c.PrjGuid = PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append("\tLEFT JOIN PT_yhmc YH ON YH.v_yhdm = c.SignPerson").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))  ").AppendLine();
            builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
            builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
            builder.Append("WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
            builder.Append(" FROM PT_PrjInfo_Kind as PrjInfo_PrjKind GROUP by[PrjGuid] ) PrjType ON c.PrjGuid = PrjType.PrjGuid ").AppendLine();
            builder.Append("WHERE c.FlowState = '1' ").AppendLine();
            builder.Append(" AND c.PrjGuid!='' ").AppendLine();
            builder.Append("AND c.UserCodes  LIKE '%" + userCode + "%'").AppendLine();
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" AND ").Append(strWhere).AppendLine();
            }
            builder.Append(" GROUP BY c.ContractID,ContractCode,ContractName,BName,ContractMoney,ModifiedMoney,c.conState,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName,").AppendLine();
            builder.Append("IsMainContract,MainContractID,c.InputDate,c.UserCodes,c.UserCodes,CorpName,PrjType.CodeName,YH.v_xm").AppendLine();
            builder.Append(") Y \n");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetReportDataSource(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append("SELECT c.ContractID,ContractCode,ContractName,BName,ContractMoney,ModifiedMoney,c.conState,Convert(NVARCHAR(10),SignDate,121) AS SignDate, ").AppendLine();
            builder.Append("\tTypeName, ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) as PrjName, YH.v_xm AS SignPersonName,");
            builder.Append("(SELECT ISNULL(SUM(ModifyMoney),0) FROM Con_Payout_Modify WHERE ContractID = c.ContractID AND FlowState = 1) AS ModifyTotal,--变更累计").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(BalanceMoney),0) FROM Con_Payout_Balance WHERE ContractID = c.ContractID AND FlowState = 1) AS BalanceTotal,--结算累计").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(PaymentMoney),0) FROM Con_Payout_Payment WHERE ContractID = c.ContractID AND FlowState = 1) AS PaymentTotal,--付款累计").AppendLine();
            builder.Append("ISNULL((SELECT SUM(ModifyMoney) FROM Con_Payout_Modify WHERE ContractID = c.ContractID AND FlowState = 1)/NULLIF(ContractMoney,0),0) AS ModifyRate,--变更比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(BalanceMoney) FROM Con_Payout_Balance WHERE ContractID = c.ContractID AND FlowState = 1)/NULLIF(ModifiedMoney,0),0) AS BalanceRate,--结算比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID AND FlowState = 1)/NULLIF(ModifiedMoney,0),0) AS PaymentRate,--付款比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID AND FlowState = 1)").AppendLine();
            builder.Append("\t/(SELECT SUM(BalanceMoney) FROM Con_Payout_Balance WHERE ContractID = c.ContractID AND FlowState = 1),0) AS PaymentBalanceRate,--付款结算比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(Amount) FROM Con_Payout_Invoice WHERE ContractID = c.ContractID ),0) AS InvoiceSum,--发票比率").AppendLine();
            builder.Append("ISNULL((SELECT SUM(Amount) FROM Con_Payout_Invoice WHERE ContractID = c.ContractID ) ").AppendLine();
            builder.Append("\t/NULLIF((SELECT SUM(PaymentMoney) FROM Con_Payout_Payment WHERE ContractID = c.ContractID ),0),0) AS InvoiceRate,--已开发票比率").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("\t   WHEN 1 THEN c.ContractID ").AppendLine();
            builder.Append("\t   WHEN  0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append("END AS Version,").AppendLine();
            builder.Append("CASE IsMainContract").AppendLine();
            builder.Append("\tWHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("\tWHEN 0 THEN (").AppendLine();
            builder.Append("\t\t\t\tSELECT c2.InputDate ").AppendLine();
            builder.Append("\t\t\t\tFROM Con_Payout_Contract c1 ").AppendLine();
            builder.Append("\t\t\t\tJOIN Con_Payout_Contract c2 ON c1.MainContractID = c2.ContractID").AppendLine();
            builder.Append("\t\t\t\tWHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("\t\t\t\t)").AppendLine();
            builder.Append("END AS Date").AppendLine();
            builder.Append(",IsMainContract, c.UserCodes,ISNULL(CorpName,BName) AS CorpName,PrjType.CodeName  ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Modify ON Con_Payout_Modify.ContractID = c.ContractID ").AppendLine();
            builder.Append("AND Con_Payout_Modify.FlowState = '1' ").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Balance ON Con_Payout_Balance.ContractID = c.ContractID ").AppendLine();
            builder.Append("AND Con_Payout_Balance.FlowState = '1' ").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Payment ON Con_Payout_Payment.ContractID = c.ContractID ").AppendLine();
            builder.Append("AND Con_Payout_Payment.FlowState = '1' ").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Invoice ON Con_Payout_Invoice.ContractID = c.ContractID ").AppendLine();
            builder.Append("LEFT JOIN Con_ContractType ON Con_ContractType.TypeID = c.TypeID").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo ON PT_PrjInfo.PrjGuid = c.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN PT_PrjInfo_ZTB on c.PrjGuid = PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append("\tLEFT JOIN PT_yhmc YH ON YH.v_yhdm = c.SignPerson").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))  ").AppendLine();
            builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
            builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
            builder.Append("WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
            builder.Append(" FROM PT_PrjInfo_Kind as PrjInfo_PrjKind GROUP by[PrjGuid] ) PrjType ON c.PrjGuid = PrjType.PrjGuid ").AppendLine();
            builder.Append("WHERE c.FlowState = '1' ").AppendLine();
            builder.Append(" AND c.PrjGuid!='' ").AppendLine();
            builder.Append("AND c.UserCodes  LIKE '%" + userCode + "%'").AppendLine();
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" AND ").Append(strWhere).AppendLine();
            }
            builder.Append(" GROUP BY c.ContractID,ContractCode,ContractName,BName,ContractMoney,ModifiedMoney,c.conState,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName,").AppendLine();
            builder.Append("IsMainContract,MainContractID,c.InputDate,c.UserCodes,c.UserCodes,CorpName,PrjType.CodeName,YH.v_xm").AppendLine();
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public bool IsReferenced(string contractID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM ");
            builder.Append("( ");
            builder.Append("SELECT ContractID FROM Con_Payout_Modify WHERE ContractID = @ContractID ");
            builder.Append("UNION ALL ");
            builder.Append("SELECT ContractID FROM Con_Payout_Balance WHERE ContractID = @ContractID ");
            builder.Append("UNION ALL ");
            builder.Append("SELECT ContractID FROM Con_Payout_Payment WHERE ContractID = @ContractID ");
            builder.Append(") AS A ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = contractID;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public void Update(PayoutContractModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Payout_Contract set ");
            builder.Append("ContractCode=@ContractCode,");
            builder.Append("ContractName=@ContractName,");
            builder.Append("TypeID=@TypeID,");
            builder.Append("IsMainContract=@IsMainContract,");
            builder.Append("MainContractID=@MainContractID,");
            builder.Append("AName=@AName,");
            builder.Append("BName=@BName,");
            builder.Append("ContractMoney=@ContractMoney,");
            builder.Append("ModifiedMoney=@ModifiedMoney,");
            builder.Append("PrepayMoney=@PrepayMoney,");
            builder.Append("MainItem=@MainItem,");
            builder.Append("PaymentCondition=@PaymentCondition,");
            builder.Append("SignDate=@SignDate,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("BalanceMode=@BalanceMode,");
            builder.Append("PayMode=@PayMode,");
            builder.Append("Address=@Address,");
            builder.Append("Annex=@Annex,");
            builder.Append("FlowState=@FlowState,");
            builder.Append("IsArchived=@IsArchived,");
            builder.Append("ArchiveDate=@ArchiveDate,");
            builder.Append("PrjGuid=@PrjGuid,");
            builder.Append("InContractID=@InContractID,");
            builder.Append("Notes=@Notes,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate,");
            builder.Append("UserCodes=@UserCodes,");
            builder.Append("fictitious=@fictitious,");
            builder.Append("conState=@conState ,");
            builder.Append("CapitalNumber=@CapitalNumber,");
            builder.Append("financeNumber=@financeNumber,");
            builder.Append("financeProject=@financeProject, ");
            builder.Append("SignPerson=@signPerson ");
            builder.Append(" where ContractID=@ContractID ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractName", SqlDbType.NVarChar, 0x80), new SqlParameter("@TypeID", SqlDbType.NVarChar, 0x40), new SqlParameter("@IsMainContract", SqlDbType.Bit, 1), new SqlParameter("@MainContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@AName", SqlDbType.NVarChar, 80), new SqlParameter("@BName", SqlDbType.NVarChar, 0x400), new SqlParameter("@ContractMoney", SqlDbType.Decimal, 9), new SqlParameter("@ModifiedMoney", SqlDbType.Decimal, 9), new SqlParameter("@PrepayMoney", SqlDbType.Decimal, 9), new SqlParameter("@MainItem", SqlDbType.NVarChar, 0x400), new SqlParameter("@PaymentCondition", SqlDbType.NVarChar, 0x400), new SqlParameter("@SignDate", SqlDbType.DateTime), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), 
                new SqlParameter("@BalanceMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@Address", SqlDbType.NVarChar, 0x100), new SqlParameter("@Annex", SqlDbType.NVarChar, 0x100), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@IsArchived", SqlDbType.Bit, 1), new SqlParameter("@ArchiveDate", SqlDbType.DateTime), new SqlParameter("@PrjGuid", SqlDbType.NVarChar, 0x40), new SqlParameter("@InContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@UserCodes", SqlDbType.NVarChar), new SqlParameter("@fictitious", SqlDbType.Int), new SqlParameter("@conState", SqlDbType.Int), new SqlParameter("@CapitalNumber", SqlDbType.NVarChar, 200), 
                new SqlParameter("@financeNumber", SqlDbType.NVarChar, 50), new SqlParameter("@financeProject", SqlDbType.NVarChar, 50), new SqlParameter("@signPerson", SqlDbType.VarChar, 8)
             };
            commandParameters[0].Value = model.ContractID;
            commandParameters[1].Value = model.ContractCode;
            commandParameters[2].Value = model.ContractName;
            commandParameters[3].Value = model.TypeID;
            commandParameters[4].Value = model.IsMainContract;
            if (model.MainContractID != null)
            {
                commandParameters[5].Value = model.MainContractID;
            }
            else
            {
                commandParameters[5].Value = DBNull.Value;
            }
            commandParameters[6].Value = model.AName;
            commandParameters[7].Value = model.BName;
            if (model.ContractMoney.HasValue)
            {
                commandParameters[8].Value = model.ContractMoney;
            }
            else
            {
                commandParameters[8].Value = DBNull.Value;
            }
            if (model.ModifiedMoney.HasValue)
            {
                commandParameters[9].Value = model.ModifiedMoney;
            }
            else
            {
                commandParameters[9].Value = DBNull.Value;
            }
            if (model.PrepayMoney.HasValue)
            {
                commandParameters[10].Value = model.PrepayMoney;
            }
            else
            {
                commandParameters[10].Value = DBNull.Value;
            }
            commandParameters[11].Value = model.MainItem;
            commandParameters[12].Value = model.PaymentCondition;
            if (model.SignDate.HasValue)
            {
                commandParameters[13].Value = model.SignDate;
            }
            else
            {
                commandParameters[13].Value = DBNull.Value;
            }
            if (model.StartDate.HasValue)
            {
                commandParameters[14].Value = model.StartDate;
            }
            else
            {
                commandParameters[14].Value = DBNull.Value;
            }
            if (model.EndDate.HasValue)
            {
                commandParameters[15].Value = model.EndDate;
            }
            else
            {
                commandParameters[15].Value = DBNull.Value;
            }
            if (model.BalanceMode != null)
            {
                commandParameters[0x10].Value = model.BalanceMode;
            }
            else
            {
                commandParameters[0x10].Value = DBNull.Value;
            }
            if (model.PayMode != null)
            {
                commandParameters[0x11].Value = model.PayMode;
            }
            else
            {
                commandParameters[0x11].Value = DBNull.Value;
            }
            commandParameters[0x12].Value = model.Address;
            if (model.Annex != null)
            {
                commandParameters[0x13].Value = model.Annex;
            }
            else
            {
                commandParameters[0x13].Value = DBNull.Value;
            }
            commandParameters[20].Value = model.FlowState;
            commandParameters[0x15].Value = model.IsArchived;
            if (model.ArchiveDate.HasValue)
            {
                commandParameters[0x16].Value = model.ArchiveDate;
            }
            else
            {
                commandParameters[0x16].Value = DBNull.Value;
            }
            commandParameters[0x17].Value = model.PrjGuid;
            commandParameters[0x18].Value = model.InContractID;
            commandParameters[0x19].Value = model.Notes;
            commandParameters[0x1a].Value = model.InputPerson;
            commandParameters[0x1b].Value = model.InputDate;
            if (model.UserCodes != null)
            {
                commandParameters[0x1c].Value = model.UserCodes;
            }
            else
            {
                commandParameters[0x1c].Value = DBNull.Value;
            }
            commandParameters[0x1d].Value = model.Fictitious;
            commandParameters[30].Value = model.ConState;
            commandParameters[0x1f].Value = model.CapitalNumber;
            commandParameters[0x20].Value = model.FinanceNumber;
            commandParameters[0x21].Value = model.FinanceProject;
            commandParameters[0x22].Value = model.SignPerson;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public void UpdateState(string id, int state)
        {
            string cmdText = string.Format("UPDATE Con_Payout_Contract SET conState = '{0}' WHERE ContractID = '{1}'", state, id);
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }
    }
}

