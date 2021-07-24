namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayoutTarget
    {
        public void Add(List<PayoutTargetModel> PayoutTargetList)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (PayoutTargetModel model in PayoutTargetList)
                    {
                        this.Add(trans, model);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new Exception("添加控制指标失败");
                }
            }
        }

        private void Add(SqlTransaction trans, PayoutTargetModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Payout_Target(");
            builder.Append("TargetId,ContractId,TaskId,SignAMount)");
            builder.Append(" values (");
            builder.Append("@ID,@ContractId,@TaskId,@SignAMount)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 500), new SqlParameter("@ContractId", SqlDbType.NVarChar, 0x40), new SqlParameter("@TaskId", SqlDbType.NVarChar, 500), new SqlParameter("@SignAMount", SqlDbType.Decimal, 0x12) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.ContractId;
            commandParameters[2].Value = model.TaskId;
            commandParameters[3].Value = model.SignAmount;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public void Add(List<PayoutTargetModel> PayoutTargetList, string contractId)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    this.DeleteByContId(trans, contractId);
                    foreach (PayoutTargetModel model in PayoutTargetList)
                    {
                        this.Add(trans, model);
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

        public void DelByContractId(List<string> ids)
        {
            string format = this.GetFormat(ids);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("DELETE FROM Con_Payout_Target WHERE ContractId IN ({0})", format);
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public void DelByContractId(string contractId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("DELETE FROM Con_Payout_Target WHERE ContractId = '{0}'", contractId);
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public void DelByIds(List<string> ids)
        {
            string format = this.GetFormat(ids);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("DELETE FROM Con_Payout_Target WHERE TargetId IN ({0})", format);
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public void DelByTaskId(List<string> ids)
        {
            string format = this.GetFormat(ids);
            StringBuilder builder = new StringBuilder();
            this.DelPaymentTargetIdByTaskIds(ids);
            builder.AppendFormat("DELETE FROM Con_Payout_Target WHERE TaskId IN ({0})", format);
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        private void DeleteByContId(SqlTransaction trans, string contractId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Payout_Target ");
            builder.Append(" where ContractId=@contractId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contractId", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = contractId;
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        private void DelPaymentTargetIdByTaskIds(List<string> ids)
        {
            string format = this.GetFormat(ids);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n\t\t\tDELETE PaymentTarget FROM Con_Payout_PaymentTarget PaymentTarget\r\n\t\t\tINNER JOIN Con_Payout_Target PayoutTarget  ON PaymentTarget.ConTargetId=PayoutTarget.TargetId\r\n\t\t\tWHERE PayoutTarget.TaskId IN ({0}) \r\n\t\t\t", format);
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetContInfoByTaskId(string taskId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ContractCode,ContractName,ContractMoney,ModifiedMoney, ");
            builder.Append("--(SELECT ISNULL(SUM(ModifyMoney),0) FROM Con_Payout_Modify WHERE ContractID = c.ContractID AND FlowState = '1') AS ModifyTotal,--变更累计").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(BalanceMoney),0) FROM Con_Payout_Balance WHERE ContractID = c.ContractID AND FlowState = '1') AS BalanceTotal,--结算累计").AppendLine();
            builder.Append("(SELECT ISNULL(SUM(PaymentMoney),0) FROM Con_Payout_Payment WHERE ContractID = c.ContractID AND FlowState = '1') AS PaymentTotal --付款累计").AppendLine();
            builder.Append("FROM Con_Payout_Contract c ");
            builder.Append("LEFT JOIN Con_Payout_Modify ON  c.ContractID= Con_Payout_Modify.ContractID ");
            builder.Append("AND Con_Payout_Modify.FlowState = '1' ");
            builder.Append("LEFT JOIN Con_Payout_Balance ON  c.ContractID= Con_Payout_Balance.ContractID ");
            builder.Append("AND Con_Payout_Balance.FlowState = '1' ");
            builder.Append("LEFT JOIN Con_Payout_Payment ON  c.ContractID = Con_Payout_Payment.ContractID ");
            builder.Append("AND Con_Payout_Payment.FlowState = '1' ");
            builder.Append("WHERE c.ContractId in (select ContractId from Con_Payout_Target where TaskId=@taskId) ");
            builder.Append("AND c.FlowState = '1' ");
            builder.Append("GROUP BY c.ContractID,ContractCode,ContractName,ContractMoney,ModifiedMoney");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@taskId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = taskId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        private string GetFormat(List<string> lst)
        {
            string str = string.Empty;
            if (lst.Count > 0)
            {
                foreach (string str2 in lst)
                {
                    str = str + "'" + str2 + "',";
                }
                return str.Substring(0, str.Length - 1);
            }
            return "''";
        }

        public bool GetIsUsedByTaskId(string taskId)
        {
            string cmdText = "SELECT COUNT(*) FROM Con_Payout_Target WHERE TaskId=@TaskId ";
            SqlParameter parameter = new SqlParameter("@TaskId", taskId);
            int num = 0;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[] { parameter }))
            {
                if (reader.Read())
                {
                    num = Convert.ToInt32(reader[0]);
                }
            }
            return (num > 0);
        }

        public List<PayoutTargetModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TargetId,ContractId,TaskId,SignAMount from Con_Payout_Target ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<PayoutTargetModel> list = new List<PayoutTargetModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    PayoutTargetModel item = null;
                    item.Id = reader["TargetId"].ToString();
                    item.ContractId = reader["ContractId"].ToString();
                    item.taskId = reader["TaskId"].ToString();
                    item.SignAmount = new decimal?(Convert.ToDecimal(reader["SignAMount"]));
                    list.Add(item);
                }
            }
            return list;
        }

        public PayoutTargetModel GetModel(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ContractId,TaskId,SignAMount from Con_Payout_Target ");
            builder.Append(" where TargetId=@ID ");
            PayoutTargetModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", id) }))
            {
                if (reader.Read())
                {
                    model.Id = id;
                    model.ContractId = reader["ContractId"].ToString();
                    model.taskId = reader["TaskId"].ToString();
                    model.SignAmount = new decimal?(Convert.ToDecimal(reader["SignAMount"]));
                }
            }
            return model;
        }

        public DataTable GetTarget(string contractId, string taskIds, bool isPendingApprove, string isWBSRelevance)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("WITH cteTotal AS ");
            builder.AppendLine("( ");
            if (isWBSRelevance == "1")
            {
                builder.AppendLine("     SELECT TaskId,dbo.fn_GetTotal(TaskId) AS TotalAmount FROM Bud_Task  ");
                builder.AppendFormat("     WHERE TaskId IN ({0}) ", taskIds).AppendLine();
                builder.AppendLine("     UNION  ");
                builder.AppendLine("     SELECT ModifyTaskId AS TaskId,dbo.fn_GetModifyTotal(ModifyTaskId) Total ");
                builder.AppendLine("     FROM Bud_ModifyTask modifyTask   ");
                builder.AppendFormat("     WHERE ModifyType=0 AND ModifyTaskId IN ({0}) ", taskIds).AppendLine();
            }
            else
            {
                builder.AppendLine(" SELECT TaskId,Total AS TotalAmount FROM Bud_Task WHERE TaskId IN( ");
                builder.AppendLine(taskIds);
                builder.AppendLine(" ) ");
            }
            builder.AppendLine("), SignedInfo AS ");
            builder.AppendLine("(SELECT TaskId,SUM(SignAmount) AS SignedAmount FROM Con_Payout_Target  ");
            builder.AppendLine("JOIN Con_Payout_Contract ON Con_Payout_Contract.ContractId = Con_Payout_Target.ContractId ");
            builder.AppendLine("WHERE Con_Payout_Target.TaskId IN (");
            builder.AppendLine(taskIds);
            builder.AppendLine(")");
            if (isPendingApprove)
            {
                builder.AppendLine("AND Con_Payout_Contract.FlowState IN('0','1') ");
            }
            else
            {
                builder.AppendLine("AND Con_Payout_Contract.FlowState = '1' ");
            }
            if (contractId == "")
            {
                builder.AppendLine("AND Con_Payout_Contract.ContractId !='' ");
            }
            else
            {
                builder.AppendLine("AND Con_Payout_Contract.ContractId !=@contractId ");
            }
            builder.AppendLine(" Group by TaskId");
            builder.AppendLine("), cteSignedAmount AS ");
            builder.AppendLine("( ");
            builder.AppendLine("SELECT TargetId,TaskId,SignAmount AS CurSignAmount FROM Con_Payout_Target ");
            if (contractId == "")
            {
                builder.AppendLine("WHERE ContractId ='' ");
            }
            else
            {
                builder.AppendLine("WHERE ContractId = @contractId ");
            }
            builder.AppendLine("AND TaskId IN(");
            builder.AppendLine(taskIds);
            builder.AppendLine(") ");
            builder.AppendLine(" ) ,budTask AS");
            builder.AppendLine("( ");
            builder.AppendLine("\r\n                               SELECT TaskId,ParentId,OrderNumber,Version,PrjId,TaskCode,TaskName,Unit,\r\n\t                           Quantity,UnitPrice,StartDate,EndDate,Note,IsValid,InputUser,InputDate,\r\n\t                           Total,Modified,ConstructionPeriod FROM Bud_Task ");
            builder.AppendLine("    UNION ");
            builder.AppendLine("    SELECT ModifyTaskId AS TaskId,NULL ParentId,OrderNumber,null Version,NULL PrjId,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit, ");
            builder.AppendLine("    Quantity,UnitPrice,StartDate,EndDate,modifyTask.Note,NULL IsVaild,NULL InputUser,NULL InputDate,dbo.fn_GetModifyTotal(ModifyTaskId) Total,NULL Modified,NULL ConstructionPeriod ");
            builder.AppendLine("    FROM Bud_ModifyTask modifyTask ");
            builder.AppendLine("    WHERE ModifyType=0  ");
            builder.AppendLine(" )");
            builder.AppendLine("SELECT TargetId,budTask.TaskId,TaskName,ISNULL(TotalAmount, 0.000) AS TotalAmount,  --总金额 ").AppendLine();
            builder.AppendLine("ISNULL(SignedAmount, 0.000) AS SignedAmount, --已签订金额 ").AppendLine();
            builder.AppendLine("ISNULL(TotalAmount, 0.000) - ISNULL(SignedAmount, 0.000) AS NotSignAmount, --未签订 ").AppendLine();
            builder.AppendLine("ISNULL(CurSignAmount,0.000) AS CurSignAmount,   --本次签订 ").AppendLine();
            builder.AppendLine("ISNULL(ISNULL(TotalAmount, 0.000) - ISNULL(SignedAmount, 0.000) - CurSignAmount,0.000) AS BalSignAmount  --差额 ").AppendLine();
            builder.AppendLine("FROM budTask ");
            builder.AppendLine("LEFT JOIN cteTotal ON budTask.TaskId = cteTotal.TaskId  ");
            builder.AppendLine("LEFT JOIN cteSignedAmount ON budTask.TaskId = cteSignedAmount.TaskId  ");
            builder.AppendLine("LEFT JOIN SignedInfo ON  budTask.TaskId = SignedInfo.TaskId  ");
            builder.AppendLine("WHERE budTask.TaskId IN( ");
            builder.AppendLine(taskIds);
            builder.AppendLine(") ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ContractId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = contractId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<string> GetTaskIdsByContId(string contractId)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select TaskId from Con_Payout_Target ");
            builder.Append(" where ContractId=@contractId ");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@contractId", contractId) }))
            {
                while (reader.Read())
                {
                    list.Add(reader["TaskId"].ToString());
                }
            }
            return list;
        }

        public int Update(PayoutTargetModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Payout_Target set ");
            builder.Append("ContractId=@ContractId,");
            builder.Append("TaskId=@TaskId,");
            builder.Append("SignAMount=@SignAMount ");
            builder.Append(" where TargetId=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 500), new SqlParameter("@ContractId", SqlDbType.NVarChar, 0x40), new SqlParameter("@TaskId", SqlDbType.NVarChar, 500), new SqlParameter("@SignAMount", SqlDbType.Decimal, 0x12) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.ContractId;
            commandParameters[2].Value = model.TaskId;
            commandParameters[3].Value = model.SignAmount;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

