namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayoutBalance
    {
        public void Add(PayoutBalanceModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Payout_Balance(");
            builder.Append("BalanceID,BalanceCode,ContractID,BalanceMoney,BalancePerson,BalanceDate,Annex,FlowState,BalanceMode,PayMode,BalanceObject,ContainPending,Notes,InputPerson,InputDate)");
            builder.Append(" values (");
            builder.Append("@BalanceID,@BalanceCode,@ContractID,@BalanceMoney,@BalancePerson,@BalanceDate,@Annex,@FlowState,@BalanceMode,@PayMode,@BalanceObject,@ContainPending,@Notes,@InputPerson,@InputDate)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BalanceID", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceMoney", SqlDbType.Decimal, 9), new SqlParameter("@BalancePerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceDate", SqlDbType.DateTime), new SqlParameter("@Annex", SqlDbType.NVarChar, 0x100), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@BalanceMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceObject", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContainPending", SqlDbType.NVarChar, 0x40), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.BalanceID;
            commandParameters[1].Value = model.BalanceCode;
            commandParameters[2].Value = model.ContractID;
            commandParameters[3].Value = model.BalanceMoney;
            commandParameters[4].Value = model.BalancePerson;
            commandParameters[5].Value = model.BalanceDate;
            commandParameters[6].Value = model.Annex;
            commandParameters[7].Value = model.FlowState;
            commandParameters[8].Value = model.BalanceMode;
            commandParameters[9].Value = model.PayMode;
            commandParameters[10].Value = model.BalanceObject;
            commandParameters[11].Value = model.ContainPending;
            commandParameters[12].Value = model.Notes;
            commandParameters[13].Value = model.InputPerson;
            commandParameters[14].Value = model.InputDate;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public void Delete(List<string> balanceIds)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in balanceIds)
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

        public void Delete(string BalanceID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Payout_Balance ");
            builder.Append(" where BalanceID=@BalanceID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BalanceID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = BalanceID;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public bool Exists(string BalanceCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Con_Payout_Balance");
            builder.Append(" where BalanceCode=@BalanceCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BalanceCode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = BalanceCode;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        private List<PayoutBalanceModel> GetList(IDataReader dr)
        {
            List<PayoutBalanceModel> list = new List<PayoutBalanceModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<PayoutBalanceModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Con_Payout_Balance.*,ContractName,UserCodes,Con_Payout_Contract.PrjGuid ");
            builder.Append("FROM Con_Payout_Balance ");
            builder.Append("JOIN Con_Payout_Contract ON Con_Payout_Balance.ContractID = Con_Payout_Contract.ContractID ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append(" WHERE ").Append(strWhere);
            }
            builder.Append(" ORDER BY Con_Payout_Balance.InputDate DESC ");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private PayoutBalanceModel GetModel(IDataReader dr)
        {
            return new PayoutBalanceModel { 
                BalanceID = DBHelper.GetString(dr["BalanceID"]), BalanceCode = DBHelper.GetString(dr["BalanceCode"]), ContractID = DBHelper.GetString(dr["ContractID"]), ContractName = DBHelper.GetString(dr["ContractName"]), BalanceMoney = new decimal?(DBHelper.GetDecimal(dr["BalanceMoney"])), BalancePerson = DBHelper.GetString(dr["BalancePerson"]), BalanceDate = DBHelper.GetDateTimeNullable(dr["BalanceDate"]), Annex = DBHelper.GetString(dr["Annex"]), FlowState = new int?(DBHelper.GetInt(dr["FlowState"])), BalanceMode = DBHelper.GetString(dr["BalanceMode"]), PayMode = DBHelper.GetString(dr["PayMode"]), BalanceObject = DBHelper.GetString(dr["BalanceObject"]), ContainPending = DBHelper.GetBool(dr["ContainPending"]), Notes = DBHelper.GetString(dr["Notes"]), InputPerson = DBHelper.GetString(dr["InputPerson"]), InputDate = new DateTime?(DBHelper.GetDateTime(dr["InputDate"])), 
                UserCodes = DBHelper.GetString(dr["UserCodes"]), PrjGuid = DBHelper.GetString(dr["PrjGuid"])
             };
        }

        public PayoutBalanceModel GetModel(string BalanceID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP(1) Con_Payout_Balance.*,ContractName,UserCodes,PrjGuid ");
            builder.Append("FROM Con_Payout_Balance ");
            builder.Append("JOIN Con_Payout_Contract ON Con_Payout_Balance.ContractID = Con_Payout_Contract.ContractID ");
            builder.Append("WHERE BalanceID = @BalanceID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BalanceID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = BalanceID;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public DataTable GetReportDataSource(string strWhere, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append("SELECT c.ContractID,ContractCode,ContractName,BName,ModifiedMoney,c.ConState,Convert(NVARCHAR(10),SignDate,121) AS SignDate,TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) as PrjName, YH.v_xm AS SignPersonName,").AppendLine();
            builder.Append("ISNULL(SUM(BalanceMoney),0) AS BalanceTotal, ISNULL(SUM(BalanceMoney)/NULLIF(ModifiedMoney,0),0) AS Rate, ").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("\t   WHEN 1 THEN c.ContractID ").AppendLine();
            builder.Append("\t   WHEN  0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append("END AS Version,").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("     WHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("     WHEN 0 THEN (").AppendLine();
            builder.Append("                  SELECT c2.InputDate FROM Con_Payout_Contract c1").AppendLine();
            builder.Append("                  JOIN Con_Payout_Contract c2 on c1.MainContractID = c2.ContractID").AppendLine();
            builder.Append("                  WHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("                 )").AppendLine();
            builder.Append("END AS Date,").AppendLine();
            builder.Append("IsMainContract,c.UserCodes,ISNULL(CorpName,BName) AS CorpName,PrjType.CodeName ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Balance ON Con_Payout_Balance.ContractID = c.ContractID  ").AppendLine();
            builder.Append("AND Con_Payout_Balance.FlowState = '1'  ").AppendLine();
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
            builder.Append(" GROUP BY c.ContractID,ContractCode,ContractName,BName,ModifiedMoney,c.ConState,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName, ").AppendLine();
            builder.Append("IsMainContract,MainContractID,c.InputDate,c.UserCodes,CorpName,PrjType.CodeName,YH.v_xm  ").AppendLine();
            builder.Append(") Y \n");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetReportDataSource(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append("SELECT c.ContractID,ContractCode,ContractName,BName,ModifiedMoney,c.ConState,Convert(NVARCHAR(10),SignDate,121) AS SignDate,TypeName,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) as PrjName, YH.v_xm AS SignPersonName,").AppendLine();
            builder.Append("ISNULL(SUM(BalanceMoney),0) AS BalanceTotal, ISNULL(SUM(BalanceMoney)/NULLIF(ModifiedMoney,0),0) AS Rate, ").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("\t   WHEN 1 THEN c.ContractID ").AppendLine();
            builder.Append("\t   WHEN  0 THEN MainContractID + c.ContractID ").AppendLine();
            builder.Append("END AS Version,").AppendLine();
            builder.Append("CASE IsMainContract ").AppendLine();
            builder.Append("     WHEN 1 THEN c.InputDate").AppendLine();
            builder.Append("     WHEN 0 THEN (").AppendLine();
            builder.Append("                  SELECT c2.InputDate FROM Con_Payout_Contract c1").AppendLine();
            builder.Append("                  JOIN Con_Payout_Contract c2 on c1.MainContractID = c2.ContractID").AppendLine();
            builder.Append("                  WHERE c1.ContractID = c.ContractID").AppendLine();
            builder.Append("                 )").AppendLine();
            builder.Append("END AS Date,").AppendLine();
            builder.Append("IsMainContract,c.UserCodes,ISNULL(CorpName,BName) AS CorpName,PrjType.CodeName ").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Balance ON Con_Payout_Balance.ContractID = c.ContractID  ").AppendLine();
            builder.Append("AND Con_Payout_Balance.FlowState = '1'  ").AppendLine();
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
            builder.Append(" GROUP BY c.ContractID,ContractCode,ContractName,BName,ModifiedMoney,c.ConState,SignDate,TypeName,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName, ").AppendLine();
            builder.Append("IsMainContract,MainContractID,c.InputDate,c.UserCodes,CorpName,PrjType.CodeName,YH.v_xm ").AppendLine();
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public bool IsExists(string BalanceCode, string ContractID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select count(1) from Con_Payout_Balance where BalanceCode=@BalanceCode and ContractID=@ContractID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BalanceCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = BalanceCode;
            commandParameters[1].Value = ContractID;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public void Update(PayoutBalanceModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Payout_Balance set ");
            builder.Append("BalanceCode=@BalanceCode,");
            builder.Append("ContractID=@ContractID,");
            builder.Append("BalanceMoney=@BalanceMoney,");
            builder.Append("BalancePerson=@BalancePerson,");
            builder.Append("BalanceDate=@BalanceDate,");
            builder.Append("Annex=@Annex,");
            builder.Append("FlowState=@FlowState,");
            builder.Append("BalanceMode=@BalanceMode,");
            builder.Append("PayMode=@PayMode,");
            builder.Append("BalanceObject=@BalanceObject,");
            builder.Append("ContainPending=@ContainPending,");
            builder.Append("Notes=@Notes,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate");
            builder.Append(" where BalanceID=@BalanceID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BalanceID", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceMoney", SqlDbType.Decimal, 9), new SqlParameter("@BalancePerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceDate", SqlDbType.DateTime), new SqlParameter("@Annex", SqlDbType.NVarChar, 0x100), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@BalanceMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@BalanceObject", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContainPending", SqlDbType.NVarChar, 0x40), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.BalanceID;
            commandParameters[1].Value = model.BalanceCode;
            commandParameters[2].Value = model.ContractID;
            commandParameters[3].Value = model.BalanceMoney;
            commandParameters[4].Value = model.BalancePerson;
            commandParameters[5].Value = model.BalanceDate;
            commandParameters[6].Value = model.Annex;
            commandParameters[7].Value = model.FlowState;
            commandParameters[8].Value = model.BalanceMode;
            commandParameters[9].Value = model.PayMode;
            commandParameters[10].Value = model.BalanceObject;
            commandParameters[11].Value = model.ContainPending;
            commandParameters[12].Value = model.Notes;
            commandParameters[13].Value = model.InputPerson;
            commandParameters[14].Value = model.InputDate;
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

