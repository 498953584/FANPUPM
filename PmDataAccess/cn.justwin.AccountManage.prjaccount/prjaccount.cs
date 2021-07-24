namespace cn.justwin.AccountManage.prjaccount
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class prjaccount
    {
        public int Add(SqlTransaction trans, prjaccountModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into fund_Prjaccount(");
            builder.Append("prjNum,accountNum,acountName,authorizer,creatData,isActivity,remark,createMan,contractNum)");
            builder.Append(" values (");
            builder.Append("@prjNum,@accountNum,@acountName,@authorizer,@creatData,@isActivity,@remark,@createMan,@contractNum)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjNum", SqlDbType.UniqueIdentifier, 9), new SqlParameter("@accountNum", SqlDbType.VarChar, 50), new SqlParameter("@acountName", SqlDbType.VarChar, 50), new SqlParameter("@authorizer", SqlDbType.VarChar, 0x1388), new SqlParameter("@creatData", SqlDbType.DateTime), new SqlParameter("@isActivity", SqlDbType.Int, 4), new SqlParameter("@remark", SqlDbType.VarChar, 0x7d0), new SqlParameter("@createMan", SqlDbType.VarChar, 50), new SqlParameter("@contractNum", SqlDbType.VarChar, 0x3e8) };
            commandParameters[0].Value = model.prjNum;
            commandParameters[1].Value = model.accountNum;
            commandParameters[2].Value = model.acountName;
            commandParameters[3].Value = model.authorizer;
            commandParameters[4].Value = model.creatData;
            commandParameters[5].Value = model.isActivity;
            commandParameters[6].Value = model.remark;
            commandParameters[7].Value = model.createMan;
            commandParameters[8].Value = model.contractNum;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string accountid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_Prjaccount ");
            builder.Append(" where accountid=@accountid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@accountid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = accountid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteList(string accountidlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_Prjaccount ");
            builder.Append(" where accountid in (" + accountidlist + ")  ");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public int Exists(string accountId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from fund_Prjaccount");
            builder.Append(" where accountID=@accountID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@accountID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = accountId;
            SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString);
            DataTable table = SqlHelper.ExecuteQuery(conn, CommandType.Text, builder.ToString(), commandParameters).Tables[0];
            if (table.Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public List<prjaccountModel> GetAccountList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT c.*,TypeName,PrjName,X1.CodeName AS BalanceModeName,X2.CodeName as PayModeName, ").AppendLine();
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
            builder.Append("END AS Date,").AppendLine();
            builder.Append("fund.*").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("right join dbo.fund_Prjaccount as fund on c.ContractID=fund.contractNum").AppendLine();
            builder.Append("JOIN Con_ContractType ON c.TypeID = Con_ContractType.TypeID ").AppendLine();
            builder.Append("JOIN PT_PrjInfo ON c.PrjGuid = PT_PrjInfo.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X1 ON c.BalanceMode = X1.NoteID ").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X2 ON c.PayMode = X2.NoteID ").AppendLine();
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" WHERE ").Append(strWhere).AppendLine();
            }
            builder.Append(" ORDER BY Date DESC, Version ASC").AppendLine();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetprjModel(reader);
            }
        }

        public string GetAuthorizer(string userCodes)
        {
            if (userCodes[0] == ',')
            {
                userCodes = userCodes.Remove(0, 1);
            }
            DataTable table = DBHelper.GetTable("select v_xm from dbo.PT_yhmc where v_yhdm in (" + userCodes + ")");
            string str2 = "";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                str2 = str2 + table.Rows[i][0].ToString() + ",";
            }
            return str2;
        }

        private List<PayoutContractModel> GetConList(IDataReader dr)
        {
            new PayoutContract();
            List<PayoutContractModel> list = new List<PayoutContractModel>();
            while (dr.Read())
            {
                list.Add(this.GetConModel(dr));
            }
            return list;
        }

        public List<PayoutContractModel> GetConList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT c.*,TypeName,PrjName,X1.CodeName AS BalanceModeName,X2.CodeName as PayModeName, ").AppendLine();
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
            builder.Append("END AS Date").AppendLine();
            builder.Append("FROM Con_Payout_Contract c").AppendLine();
            builder.Append("JOIN Con_ContractType ON c.TypeID = Con_ContractType.TypeID ").AppendLine();
            builder.Append("JOIN PT_PrjInfo ON c.PrjGuid = PT_PrjInfo.PrjGuid ").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X1 ON c.BalanceMode = X1.NoteID ").AppendLine();
            builder.Append("LEFT JOIN XPM_Basic_CodeList AS X2 ON c.PayMode = X2.NoteID ").AppendLine();
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains("Con_Payout_Contract"))
                {
                    strWhere = strWhere.Replace("Con_Payout_Contract", "c");
                }
                builder.Append(" WHERE ").Append(strWhere).AppendLine();
            }
            builder.Append(" and c.ContractID not in (select contractNum from fund_Prjaccount)").AppendLine();
            builder.Append(" ORDER BY Date DESC, Version ASC").AppendLine();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetConList(reader);
            }
        }

        private PayoutContractModel GetConModel(IDataReader dr)
        {
            return new PayoutContractModel { 
                ContractID = DBHelper.GetString(dr["ContractID"]), ContractCode = DBHelper.GetString(dr["ContractCode"]), ContractName = DBHelper.GetString(dr["ContractName"]), TypeID = DBHelper.GetString(dr["TypeID"]), TypeName = DBHelper.GetString(dr["TypeName"]), IsMainContract = DBHelper.GetBool(dr["IsMainContract"]), MainContractID = DBHelper.GetString(dr["MainContractID"]), AName = DBHelper.GetString(dr["AName"]), BName = DBHelper.GetString(dr["BName"]), ContractMoney = new decimal?(DBHelper.GetDecimal(dr["ContractMoney"])), ModifiedMoney = new decimal?(DBHelper.GetDecimal(dr["ModifiedMoney"])), PrepayMoney = new decimal?(DBHelper.GetDecimal(dr["PrepayMoney"])), MainItem = DBHelper.GetString(dr["MainItem"]), PaymentCondition = DBHelper.GetString(dr["PaymentCondition"]), SignDate = DBHelper.GetDateTimeNullable(dr["SignDate"]), StartDate = DBHelper.GetDateTimeNullable(dr["StartDate"]), 
                EndDate = DBHelper.GetDateTimeNullable(dr["EndDate"]), BalanceMode = DBHelper.GetString(dr["BalanceMode"]), BalanceModeName = DBHelper.GetString(dr["BalanceModeName"]), PayMode = DBHelper.GetString(dr["PayMode"]), PayModeName = DBHelper.GetString(dr["PayModeName"]), Address = DBHelper.GetString(dr["Address"]), Annex = DBHelper.GetString(dr["Annex"]), FlowState = new int?(DBHelper.GetInt(dr["FlowState"])), IsArchived = DBHelper.GetBool(dr["IsArchived"]), ArchiveDate = DBHelper.GetDateTimeNullable(dr["ArchiveDate"]), PrjGuid = DBHelper.GetString(dr["PrjGuid"]), PrjName = DBHelper.GetString(dr["PrjName"]), InContractID = DBHelper.GetString(dr["InContractID"]), Notes = DBHelper.GetString(dr["Notes"]), InputPerson = DBHelper.GetString(dr["InputPerson"]), InputDate = new DateTime?(DBHelper.GetDateTime(dr["InputDate"])), 
                UserCodes = DBHelper.GetString(dr["UserCodes"])
             };
        }

        public DataTable GetContractList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("");
            return DBHelper.GetTable(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,prjNum,accountNum,accountid,acountName,authorizer,creatData,isActivity,activeData,activeMan,remark,createMan,contractNum");
            builder.Append(" FROM fund_Prjaccount ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DBHelper.GetTable(builder.ToString());
        }

        private prjaccountModel GetModel(IDataReader dr)
        {
            PayoutContractModel model = new PayoutContractModel {
                ContractID = DBHelper.GetString(dr["ContractID"]),
                ContractCode = DBHelper.GetString(dr["ContractCode"]),
                ContractName = DBHelper.GetString(dr["ContractName"]),
                TypeID = DBHelper.GetString(dr["TypeID"]),
                TypeName = DBHelper.GetString(dr["TypeName"]),
                IsMainContract = DBHelper.GetBool(dr["IsMainContract"]),
                MainContractID = DBHelper.GetString(dr["MainContractID"]),
                AName = DBHelper.GetString(dr["AName"]),
                BName = DBHelper.GetString(dr["BName"]),
                ContractMoney = new decimal?(DBHelper.GetDecimal(dr["ContractMoney"])),
                ModifiedMoney = new decimal?(DBHelper.GetDecimal(dr["ModifiedMoney"])),
                PrepayMoney = new decimal?(DBHelper.GetDecimal(dr["PrepayMoney"])),
                MainItem = DBHelper.GetString(dr["MainItem"]),
                PaymentCondition = DBHelper.GetString(dr["PaymentCondition"]),
                SignDate = DBHelper.GetDateTimeNullable(dr["SignDate"]),
                StartDate = DBHelper.GetDateTimeNullable(dr["StartDate"]),
                EndDate = DBHelper.GetDateTimeNullable(dr["EndDate"]),
                BalanceMode = DBHelper.GetString(dr["BalanceMode"]),
                BalanceModeName = DBHelper.GetString(dr["BalanceModeName"]),
                PayMode = DBHelper.GetString(dr["PayMode"]),
                PayModeName = DBHelper.GetString(dr["PayModeName"]),
                Address = DBHelper.GetString(dr["Address"]),
                Annex = DBHelper.GetString(dr["Annex"]),
                FlowState = new int?(DBHelper.GetInt(dr["FlowState"])),
                IsArchived = DBHelper.GetBool(dr["IsArchived"]),
                ArchiveDate = DBHelper.GetDateTimeNullable(dr["ArchiveDate"]),
                PrjGuid = DBHelper.GetString(dr["PrjGuid"]),
                PrjName = DBHelper.GetString(dr["PrjName"]),
                InContractID = DBHelper.GetString(dr["InContractID"]),
                Notes = DBHelper.GetString(dr["Notes"]),
                InputPerson = DBHelper.GetString(dr["InputPerson"]),
                InputDate = new DateTime?(DBHelper.GetDateTime(dr["InputDate"])),
                UserCodes = DBHelper.GetString(dr["UserCodes"])
            };
            return new prjaccountModel { countractModel = model, id = DBHelper.GetInt(dr["id"]), prjNum = DBHelper.GetGuid(dr["prjnum"]), accountNum = DBHelper.GetString(dr["accountnum"]), acountName = DBHelper.GetString(dr["acountName"]), authorizer = DBHelper.GetString(dr["authorizer"]), creatData = new DateTime?(DBHelper.GetDateTime(dr["creatdata"])), isActivity = new int?(DBHelper.GetInt(dr["isactivity"])), activeData = new DateTime?(DBHelper.GetDateTime(dr["activedata"])), activeMan = DBHelper.GetString(dr["activeman"]), remark = DBHelper.GetString(dr["remark"]), createMan = DBHelper.GetString(dr["createman"]), contractNum = DBHelper.GetString(dr["contractnum"]), accountID = DBHelper.GetGuid(dr["accountID"]) };
        }

        public prjaccountModel GetModel(string accountid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,accountid,prjNum,accountNum,acountName,authorizer,creatData,isActivity,activeData,activeMan,remark,createMan,contractNum,isnullify from fund_Prjaccount ");
            builder.Append(" where accountid=@accountid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@accountid", SqlDbType.VarChar, 0x24) };
            commandParameters[0].Value = accountid;
            prjaccountModel model = new prjaccountModel();
            SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString);
            DataSet set = SqlHelper.ExecuteQuery(conn, CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["id"].ToString() != "")
            {
                model.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
            }
            if (set.Tables[0].Rows[0]["prjNum"].ToString() != "")
            {
                model.prjNum = new Guid(set.Tables[0].Rows[0]["prjNum"].ToString());
            }
            model.accountNum = set.Tables[0].Rows[0]["accountNum"].ToString();
            model.acountName = set.Tables[0].Rows[0]["acountName"].ToString();
            model.authorizer = set.Tables[0].Rows[0]["authorizer"].ToString();
            if (set.Tables[0].Rows[0]["creatData"].ToString() != "")
            {
                model.creatData = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["creatData"].ToString()));
            }
            if (set.Tables[0].Rows[0]["isActivity"].ToString() != "")
            {
                model.isActivity = new int?(int.Parse(set.Tables[0].Rows[0]["isActivity"].ToString()));
            }
            if (set.Tables[0].Rows[0]["activeData"].ToString() != "")
            {
                model.activeData = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["activeData"].ToString()));
            }
            model.activeMan = set.Tables[0].Rows[0]["activeMan"].ToString();
            model.remark = set.Tables[0].Rows[0]["remark"].ToString();
            model.createMan = set.Tables[0].Rows[0]["createMan"].ToString();
            if (set.Tables[0].Rows[0]["contractNum"].ToString() != "")
            {
                model.contractNum = set.Tables[0].Rows[0]["contractNum"].ToString();
            }
            if (set.Tables[0].Rows[0]["accountid"].ToString() != "")
            {
                model.contractNum = set.Tables[0].Rows[0]["accountid"].ToString();
            }
            if (set.Tables[0].Rows[0]["isnullify"].ToString() != "")
            {
                model.isnullify = set.Tables[0].Rows[0]["isnullify"].ToString();
            }
            return model;
        }

        public string GetprjLimits(string contractNum)
        {
            DataTable table = DBHelper.GetTable("select podepom from PT_PrjInfo where prjGuid in (select prjGuid from  Con_Payout_Contract where ContractID='" + contractNum + "')");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "";
        }

        private List<prjaccountModel> GetprjModel(IDataReader dr)
        {
            List<prjaccountModel> list = new List<prjaccountModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public int upActivityDal(SqlTransaction trans, prjaccountModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_Prjaccount set ");
            builder.Append("isActivity=@isActivity,");
            builder.Append("activeData=@activeData,");
            builder.Append("activeMan=@activeMan");
            builder.Append(" where accountid=@accountid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@isActivity", SqlDbType.Int, 4), new SqlParameter("@activeData", SqlDbType.DateTime), new SqlParameter("@activeMan", SqlDbType.VarChar, 50), new SqlParameter("@accountid", SqlDbType.UniqueIdentifier, 0x24) };
            commandParameters[0].Value = model.isActivity;
            commandParameters[1].Value = model.activeData;
            commandParameters[2].Value = model.activeMan;
            commandParameters[3].Value = model.accountID;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Update(prjaccountModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_Prjaccount set ");
            builder.Append("acountName=@acountName,");
            builder.Append("authorizer=@authorizer,");
            builder.Append("remark=@remark,");
            builder.Append("accountNum=@accountNum");
            builder.Append(" where accountid=@accountid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@acountName", SqlDbType.VarChar, 50), new SqlParameter("@authorizer", SqlDbType.VarChar, 0x1388), new SqlParameter("@remark", SqlDbType.VarChar, 0x7d0), new SqlParameter("@accountNum", SqlDbType.VarChar, 0x3e8), new SqlParameter("@accountid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = model.acountName;
            commandParameters[1].Value = model.authorizer;
            commandParameters[2].Value = model.remark;
            commandParameters[3].Value = model.accountNum;
            commandParameters[4].Value = model.accountID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

