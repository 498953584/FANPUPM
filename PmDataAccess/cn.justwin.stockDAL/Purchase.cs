namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Purchase
    {
        public int Add(SqlTransaction trans, PurchaseModel model)
        {
            if (!string.IsNullOrEmpty(model.Project))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into Sm_Purchase(");
                builder.Append("pid,pcode,contract,flowstate,person,intime,acceptstate,annx,explain,Project,PurchaseType)");
                builder.Append(" values (");
                builder.Append("@pid,@pcode,@contract,@flowstate,@person,@intime,@acceptstate,@annx,@explain,@Project,@PurchaseType)");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@pid", SqlDbType.NVarChar, 50), new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@contract", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@Project", SqlDbType.NVarChar, 0x40), new SqlParameter("@PurchaseType", SqlDbType.Int, 4) };
                parameterArray[0].Value = model.pid;
                parameterArray[1].Value = model.pcode;
                parameterArray[2].Value = model.contract;
                parameterArray[3].Value = model.flowstate;
                parameterArray[4].Value = model.person;
                parameterArray[5].Value = model.intime;
                parameterArray[6].Value = model.acceptstate;
                parameterArray[7].Value = model.annx;
                parameterArray[8].Value = model.explain;
                parameterArray[9].Value = model.Project;
                parameterArray[10].Value = model.PurchaseType;
                return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), parameterArray);
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("insert into Sm_Purchase(");
            builder2.Append("pid,pcode,contract,flowstate,person,intime,acceptstate,annx,explain,PurchaseType)");
            builder2.Append(" values (");
            builder2.Append("@pid,@pcode,@contract,@flowstate,@person,@intime,@acceptstate,@annx,@explain,@PurchaseType)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pid", SqlDbType.NVarChar, 50), new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@contract", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@PurchaseType", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.pid;
            commandParameters[1].Value = model.pcode;
            commandParameters[2].Value = model.contract;
            commandParameters[3].Value = model.flowstate;
            commandParameters[4].Value = model.person;
            commandParameters[5].Value = model.intime;
            commandParameters[6].Value = model.acceptstate;
            commandParameters[7].Value = model.annx;
            commandParameters[8].Value = model.explain;
            commandParameters[9].Value = model.PurchaseType;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder2.ToString(), commandParameters);
        }

        public int AddInContract(SqlTransaction trans, PurchaseModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Purchase(");
            builder.Append("pid,pcode,contract,flowstate,person,intime,acceptstate,annx,explain,Project,IsConPurchase)");
            builder.Append(" values (");
            builder.Append("@pid,@pcode,@contract,@flowstate,@person,@intime,@acceptstate,@annx,@explain,@Project,@IsConPurchase)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pid", SqlDbType.NVarChar, 50), new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@contract", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@Project", SqlDbType.NVarChar, 0x40), new SqlParameter("@IsConPurchase", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.pid;
            commandParameters[1].Value = model.pcode;
            commandParameters[2].Value = model.contract;
            commandParameters[3].Value = model.flowstate;
            commandParameters[4].Value = model.person;
            commandParameters[5].Value = model.intime;
            commandParameters[6].Value = model.acceptstate;
            commandParameters[7].Value = model.annx;
            commandParameters[8].Value = model.explain;
            commandParameters[9].Value = model.Project;
            commandParameters[10].Value = model.IsConPurchase;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(List<string> lstPcode)
        {
            int num = 1;
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in lstPcode)
                    {
                        this.Delete(trans, str);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    num = 0;
                    trans.Rollback();
                }
            }
            return num;
        }

        public int Delete(SqlTransaction trans, string pcode)
        {
            string cmdText = "delete from Sm_Purchase where pcode=@pcode";
            string str2 = "delete from Sm_Purchase_Stock where pscode = @pcode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = pcode;
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str2, commandParameters);
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
            return 1;
        }

        public string GetCodeByGuid(string guid)
        {
            string cmdText = "select pcode from Sm_Purchase where pid = @pid";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = guid;
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public string GetConPurchasePcode(string contractId)
        {
            string cmdText = "DECLARE @ConPurchaseCount NVARCHAR \r\n                            SELECT @ConPurchaseCount=COUNT(*) FROM Sm_Purchase WHERE [Contract]=@contractId\r\n                            AND IsConPurchase=1 AND FlowState=1\r\n                            IF @ConPurchaseCount!=0\r\n                              BEGIN\r\n                                SELECT pcode FROM Sm_Purchase WHERE [Contract]=@contractId\r\n                                AND IsConPurchase=1 AND FlowState=1\r\n                              END\r\n                            ELSE \r\n                              BEGIN \r\n                                SELECT TOP 1 pcode FROM Sm_Purchase WHERE [Contract]=@contractId\r\n                                AND FlowState=1 ORDER BY intime\r\n                              END ";
            SqlParameter parameter = new SqlParameter("@contractId", contractId);
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }));
        }

        public DataTable GetEquTable(string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select pid,pcode,contract,Sm_Purchase.flowstate, person, convert(nvarchar(10), intime,120) as intime,");
            builder.Append("acceptstate, annx, explain,ContractName ");
            builder.Append("from Sm_purchase inner join Con_Payout_Contract on Con_Payout_Contract.ContractID = Sm_Purchase.Contract ");
            builder.Append("where PurchaseType = 1 ");
            if (!string.IsNullOrEmpty(condition))
            {
                builder.Append(condition);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        private List<PurchaseModel> GetList(IDataReader dr)
        {
            List<PurchaseModel> list = new List<PurchaseModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<PurchaseModel> GetList(string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Sm_Purchase.*,ContractName FROM Sm_Purchase").AppendLine();
            builder.Append("LEFT JOIN Con_Payout_Contract ON  Sm_Purchase.contract = Con_Payout_Contract.ContractID").AppendLine();
            if (!string.IsNullOrEmpty(condition))
            {
                builder.Append(" where ").Append(condition).AppendLine();
            }
            builder.Append("order by intime desc").AppendLine();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private PurchaseModel GetModel(IDataReader dr)
        {
            return new PurchaseModel { pid = DBHelper.GetString(dr["pid"]), pcode = DBHelper.GetString(dr["pcode"]), contract = DBHelper.GetString(dr["contract"]), contractName = DBHelper.GetString(dr["ContractName"]), flowstate = DBHelper.GetInt(dr["flowstate"]), person = DBHelper.GetString(dr["person"]), intime = DBHelper.GetDateTime(dr["intime"]), acceptstate = DBHelper.GetInt(dr["acceptstate"]), annx = DBHelper.GetString(dr["annx"]), explain = DBHelper.GetString(dr["explain"]), Project = DBHelper.GetString(dr["Project"]) };
        }

        public PurchaseModel GetModel(string pcode)
        {
            string cmdText = "select Sm_Purchase.*,ContractName from Sm_Purchase \r\n                               left join Con_Payout_Contract on Sm_Purchase.contract = Con_Payout_Contract.ContractID \r\n                               where pcode = @pcode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = pcode;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public PurchaseModel GetModelByContractId(string contractId)
        {
            string cmdText = "select Sm_Purchase.*,ContractName from Sm_Purchase \r\n                               left join Con_Payout_Contract on Sm_Purchase.contract = Con_Payout_Contract.ContractID \r\n                               where contract = @contractId and IsConPurchase='true'";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contractId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = contractId;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public DataTable GetTable(string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select pid, pcode, contract, Sm_Purchase.flowstate, person, convert(nvarchar(10), intime,120) as intime, ");
            builder.Append("acceptstate, annx, explain, Sm_Purchase.Project,ContractName,PrjName  ");
            builder.Append("from Sm_Purchase ");
            builder.Append("left join Con_Payout_Contract on Con_Payout_Contract.ContractID = Sm_Purchase.Contract ");
            builder.Append("left join PT_PrjInfo on Convert(nvarchar(64), PT_PrjInfo.PrjGuid) = Sm_Purchase.Project ");
            builder.Append("WHERE (IsConPurchase='false' or IsConPurchase is null or (IsConPurchase='true' and Sm_Purchase.flowstate=1)) ");
            if (!string.IsNullOrEmpty(condition))
            {
                builder.Append(condition);
            }
            builder.Append(" order by intime desc,pcode DESC");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Update(SqlTransaction trans, PurchaseModel model)
        {
            if (model.Project != "")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update Sm_Purchase set ");
                builder.Append("pid=@pid,");
                builder.Append("contract=@contract,");
                builder.Append("flowstate=@flowstate,");
                builder.Append("person=@person,");
                builder.Append("intime=@intime,");
                builder.Append("acceptstate=@acceptstate,");
                builder.Append("annx=@annx,");
                builder.Append("explain=@explain,");
                builder.Append("Project=@Project");
                builder.Append(" where pcode=@pcode ");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@pid", SqlDbType.NVarChar, 50), new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@contract", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@Project", SqlDbType.NVarChar, 0x40) };
                parameterArray[0].Value = model.pid;
                parameterArray[1].Value = model.pcode;
                parameterArray[2].Value = model.contract;
                parameterArray[3].Value = model.flowstate;
                parameterArray[4].Value = model.person;
                parameterArray[5].Value = model.intime;
                parameterArray[6].Value = model.acceptstate;
                parameterArray[7].Value = model.annx;
                parameterArray[8].Value = model.explain;
                parameterArray[9].Value = model.Project;
                return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), parameterArray);
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Sm_Purchase set ");
            builder2.Append("pid=@pid,");
            builder2.Append("contract=@contract,");
            builder2.Append("flowstate=@flowstate,");
            builder2.Append("person=@person,");
            builder2.Append("intime=@intime,");
            builder2.Append("acceptstate=@acceptstate,");
            builder2.Append("annx=@annx,");
            builder2.Append("explain=@explain ");
            builder2.Append(" where pcode=@pcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pid", SqlDbType.NVarChar, 50), new SqlParameter("@pcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@contract", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800) };
            commandParameters[0].Value = model.pid;
            commandParameters[1].Value = model.pcode;
            commandParameters[2].Value = model.contract;
            commandParameters[3].Value = model.flowstate;
            commandParameters[4].Value = model.person;
            commandParameters[5].Value = model.intime;
            commandParameters[6].Value = model.acceptstate;
            commandParameters[7].Value = model.annx;
            commandParameters[8].Value = model.explain;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder2.ToString(), commandParameters);
        }
    }
}

