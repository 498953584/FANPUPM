namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class PaymentTarget
    {
        private void Add(SqlTransaction trans, PaymentTargetModel model)
        {
            string cmdText = "INSERT INTO Con_Payout_PaymentTarget VALUES(@TargetId,@PaymentId,@ContTargetId,@PaymentAmount,@InputUser,@InputDate)";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TargetId", model.Id), new SqlParameter("@PaymentId", model.PaymentId), new SqlParameter("@ContTargetId", model.ConTargetId), new SqlParameter("@PaymentAmount", model.PaymentAmount), new SqlParameter("@InputUser", model.InputUser), new SqlParameter("@InputDate", model.InputDate) };
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
            }
        }

        public void Add(List<PaymentTargetModel> lstPaymentTarget, string paymentId)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    this.Del(trans, paymentId);
                    foreach (PaymentTargetModel model in lstPaymentTarget)
                    {
                        this.Add(trans, model);
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw new Exception("保存失败");
                }
            }
        }

        private void Del(SqlTransaction trans, string paymentId)
        {
            string cmdText = "DELETE Con_Payout_PaymentTarget WHERE paymentId = @paymentId";
            SqlParameter parameter = new SqlParameter("@paymentId", paymentId);
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, new SqlParameter[] { parameter });
            }
        }

        public DataTable GetConTarget(string contractId, string isWBSRelevance)
        {
            DataTable table = new DataTable();
            string cmdText = "\r\n            --DECLARE @ContractId NVARCHAR(64);\r\n            --SET @ContractId='28446917-31dd-4c37-906f-fe40f758afe6';\r\n            SELECT TargetId,TaskName,TaskCode,conTarget.ContractId,conTarget.TaskId,SignAmount,Total BudTotal FROM Con_Payout_Target conTarget ";
            if (isWBSRelevance == "1")
            {
                cmdText = cmdText + "\r\n\t\t\t\tINNER JOIN (\r\n\t\t\t\t\tSELECT TaskId,ParentId,OrderNumber,Version,PrjId,TaskCode,TaskName,Unit,\r\n\t\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,Note,IsValid,InputUser,InputDate,\r\n\t\t\t\t\tdbo.fn_GetTotal(TaskId) AS Total,Modified,ConstructionPeriod FROM Bud_Task \r\n\t\t\t\t\tUNION \r\n\t\t\t\t\tSELECT ModifyTaskId AS TaskId,NULL ParentId,OrderNumber,null Version,NULL PrjId,\r\n\t\t\t\t\tModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit, \r\n\t\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,modifyTask.Note,NULL IsVaild,NULL InputUser,\r\n\t\t\t\t\tNULL InputDate,dbo.fn_GetModifyTotal(ModifyTaskId) Total,NULL Modified,NULL ConstructionPeriod \r\n\t\t\t\t\tFROM Bud_ModifyTask modifyTask \r\n\t\t\t\t\tWHERE ModifyType=0 \r\n\t\t\t\t) budTask ON conTarget.TaskId=budTask.TaskId ";
            }
            else
            {
                cmdText = cmdText + "\r\n\t\t\t\tINNER JOIN (\r\n\t\t\t\t\tSELECT TaskId,ParentId,OrderNumber,Version,PrjId,TaskCode,TaskName,Unit,\r\n\t\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,Note,IsValid,InputUser,InputDate\r\n\t\t\t\t\t,ISNULL(Total,0) Total,Modified,ConstructionPeriod FROM Bud_Task \r\n\t\t\t\t\tUNION \r\n\t\t\t\t\tSELECT ModifyTaskId AS TaskId,NULL ParentId,OrderNumber,null Version,NULL PrjId,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit, \r\n\t\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,modifyTask.Note,NULL IsVaild,NULL InputUser,NULL InputDate,Total,NULL Modified,NULL ConstructionPeriod \r\n\t\t\t\t\tFROM Bud_ModifyTask modifyTask \r\n\t\t\t\t\tWHERE ModifyType=0 \r\n\t\t\t\t) budTask ON conTarget.TaskId=budTask.TaskId ";
            }
            cmdText = cmdText + "WHERE ContractId=@ContractId ";
            SqlParameter parameter = new SqlParameter("@contractId", contractId);
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
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

        public DataTable GetPaymentTarget(List<string> lstTargetId, string contractId, string paymentId, string isWBSRelevance)
        {
            DataTable table = new DataTable();
            if (!string.IsNullOrEmpty(paymentId))
            {
                if (lstTargetId == null)
                {
                    lstTargetId = new List<string>();
                }
                lstTargetId = this.GetPaymentTargetId(paymentId);
            }
            string cmdText = "\r\n                --DECLARE @ContractId NVARCHAR(64),@paymentId NVARCHAR(64);\r\n                --SET @ContractId='28446917-31dd-4c37-906f-fe40f758afe6';\r\n                --SET @paymentId='';\r\n                WITH ContTargetInfo AS \r\n                (\r\n\t                --合同签订金额\r\n\t                SELECT TargetId,TaskName,TaskCode,conTarget.ContractId,conTarget.TaskId,SignAmount,Total BudTotal FROM Con_Payout_Target conTarget ";
            if (isWBSRelevance == "1")
            {
                cmdText = cmdText + "\r\n\t\t\t\tINNER JOIN (\r\n\t\t\t\t\tSELECT TaskId,ParentId,OrderNumber,Version,PrjId,TaskCode,TaskName,Unit,\r\n\t\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,Note,IsValid,InputUser,InputDate,\r\n\t\t\t\t\tdbo.fn_GetTotal(TaskId) AS Total,Modified,ConstructionPeriod FROM Bud_Task \r\n\t\t\t\t\tUNION \r\n\t\t\t\t\tSELECT ModifyTaskId AS TaskId,NULL ParentId,OrderNumber,null Version,NULL PrjId,\r\n\t\t\t\t\tModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit, \r\n\t\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,modifyTask.Note,NULL IsVaild,NULL InputUser,\r\n\t\t\t\t\tNULL InputDate,dbo.fn_GetModifyTotal(ModifyTaskId) Total,NULL Modified,NULL ConstructionPeriod \r\n\t\t\t\t\tFROM Bud_ModifyTask modifyTask \r\n\t\t\t\t\tWHERE ModifyType=0 \r\n\t\t\t\t) budTask ON conTarget.TaskId=budTask.TaskId ";
            }
            else
            {
                cmdText = cmdText + "\r\n\t\t\t\tINNER JOIN (\r\n\t\t\t\t\tSELECT TaskId,ParentId,OrderNumber,Version,PrjId,TaskCode,TaskName,Unit,\r\n\t\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,Note,IsValid,InputUser,InputDate\r\n\t\t\t\t\t,ISNULL(Total,0) Total,Modified,ConstructionPeriod FROM Bud_Task \r\n\t\t\t\t\tUNION \r\n\t\t\t\t\tSELECT ModifyTaskId AS TaskId,NULL ParentId,OrderNumber,null Version,NULL PrjId,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit, \r\n\t\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,modifyTask.Note,NULL IsVaild,NULL InputUser,NULL InputDate,Total,NULL Modified,NULL ConstructionPeriod \r\n\t\t\t\t\tFROM Bud_ModifyTask modifyTask \r\n\t\t\t\t\tWHERE ModifyType=0 \r\n\t\t\t\t) budTask ON conTarget.TaskId=budTask.TaskId ";
            }
            cmdText = cmdText + string.Format("  WHERE TargetId IN ({0})", this.GetFormat(lstTargetId)) + " ),PaymentedTarget AS\r\n                (\r\n\t                --合同已支付支付控制指标\r\n\t                SELECT ConTargetId,SUM(ISNULL(PaymentAmount,0)) PaymentedAmount FROM Con_Payout_Payment payoutPayment \r\n\t                INNER JOIN Con_Payout_PaymentTarget paymentTarget ON Id=PaymentId\r\n\t                WHERE ContractId=@ContractId AND flowState=1 GROUP BY ConTargetId\r\n                )\r\n                SELECT ContTargetInfo.TargetId,TaskName,TaskCode,SignAmount,BudTotal,ISNULL(PaymentedAmount,0) PaymentedAmount,\r\n                ISNULL(PaymentAmount,0) CurrentPaymentAmount,ISNULL(ISNULL(PaymentedAmount,0)/NULLIF(SignAmount,0),0) Rate\r\n                FROM ContTargetInfo \r\n                LEFT JOIN PaymentedTarget ON ContTargetInfo.TargetId=PaymentedTarget.ConTargetId\r\n                LEFT JOIN Con_Payout_PaymentTarget ON ContTargetInfo.TargetId=Con_Payout_PaymentTarget.ConTargetId AND paymentId=@PaymentId\r\n            ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ContractId", contractId), new SqlParameter("@paymentId", paymentId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        private List<string> GetPaymentTargetId(string paymentId)
        {
            List<string> list = new List<string>();
            string str = "SELECT ConTargetId FROM Con_Payout_PaymentTarget WHERE paymentId=@paymentId ";
            SqlParameter parameter = new SqlParameter("@paymentId", paymentId);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, str.ToString(), new SqlParameter[] { parameter }))
            {
                while (reader.Read())
                {
                    list.Add(reader["ConTargetId"].ToString());
                }
            }
            return list;
        }

        public List<string> GetTargetIdsByConTargetId(string conTargetId)
        {
            List<string> list = new List<string>();
            string str = "SELECT TargetId FROM Con_Payout_PaymentTarget WHERE ConTargetId=@ConTargetId ";
            SqlParameter parameter = new SqlParameter("@ConTargetId", conTargetId);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, str.ToString(), new SqlParameter[] { parameter }))
            {
                while (reader.Read())
                {
                    list.Add(reader["TargetId"].ToString());
                }
            }
            return list;
        }
    }
}

