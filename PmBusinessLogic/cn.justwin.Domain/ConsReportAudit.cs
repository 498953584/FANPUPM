namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using PmBusinessLogic;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ConsReportAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                this.UpdateConsTask(trans, key.ToString());
                this.UpdateConsTaskRes(trans, key.ToString());
                trans.Commit();
            }
        }

        private void DelConsReport(SqlTransaction trans, string consReportId)
        {
            string cmdText = string.Format("DELETE Bud_ConsReport WHERE ConsReportId='{0}'", consReportId);
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, null);
        }

        private void DelConsTask(SqlTransaction trans, string consReportId)
        {
            string cmdText = string.Format("DELETE Bud_ConsTask WHERE ConsReportId='{0}'", consReportId);
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, null);
        }

        private void DelConsTaskRes(SqlTransaction trans, string consReportId)
        {
            string cmdText = "\r\n            DELETE Bud_ConsTaskRes FROM Bud_ConsTaskRes ConsTaskRes INNER JOIN Bud_ConsTask \r\n            ON ConsTaskRes.ConsTaskId=Bud_ConsTask.ConsTaskId \r\n            WHERE  ConsReportId=@consReportId\r\n            ";
            SqlParameter parameter = new SqlParameter("@consReportId", SqlDbType.NVarChar, 500) {
                Value = consReportId
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public void RefuseEvent(object primarykey)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
            if (BudContractTaskAudit.GetModelById(key.ToString()) != null)
            {
                using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    this.DelConsTask(trans, key.ToString());
                    this.DelConsTaskRes(trans, key.ToString());
                    this.DelConsReport(trans, key.ToString());
                    trans.Commit();
                }
            }
        }

        private void UpdateConsTask(SqlTransaction trans, string consReportId)
        {
            string cmdText = string.Format("UPDATE Bud_ConsTask SET AccountingQuantity=CompleteQuantity WHERE ConsReportId='{0}'", consReportId);
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, null);
        }

        public void UpdateConsTaskRes(SqlTransaction trans, string consReportId)
        {
            string cmdText = "\r\n            WITH cteConsRes AS\r\n            (\r\n\t           SELECT ConsTaskRes.*,ResourceType,dbo.GetTopResourceType(ResourceType) TopResourceTypeId \r\n\t           FROM Bud_ConsTaskRes ConsTaskRes INNER JOIN Bud_ConsTask\r\n\t           ON ConsTaskRes.ConsTaskId=Bud_ConsTask.ConsTaskId\r\n\t           INNER JOIN Res_Resource Resource on ConsTaskRes.ResourceId=Resource.ResourceId\r\n               WHERE  ConsReportId=@consReportId\r\n            ),cteConsResCost AS\r\n            (\r\n\t           SELECT cteConsRes.*,Bud_CostAccounting.CBSCode CostCBSCode FROM cteConsRes LEFT JOIN Bud_CostAccounting\r\n\t           ON cteConsRes.TopResourceTypeId=Bud_CostAccounting.ResourceType\r\n            )\r\n            UPDATE Bud_ConsTaskRes SET CBSCode = cteConsResCost.CostCBSCode,AccountingQuantity=Bud_ConsTaskRes.Quantity\r\n            FROM Bud_ConsTaskRes JOIN cteConsResCost ON cteConsResCost.ConsTaskResId = Bud_ConsTaskRes.ConsTaskResId \r\n            ";
            SqlParameter parameter = new SqlParameter("@consReportId", SqlDbType.NVarChar, 500) {
                Value = consReportId
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }
    }
}

