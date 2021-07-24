namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudContractConsTaskService : Repository<BudContractConsTask>
    {
        public void DeleteByReport(string rptId)
        {
            string sql = string.Format("DELETE FROM Bud_ContractConsTask WHERE RptId = '{0}';", rptId);
            base.ExcuteSql(sql);
        }

        public void DeleteByTaskId(List<string> taskIds)
        {
            if (taskIds.Count > 0)
            {
                foreach (string str in taskIds)
                {
                    string sql = string.Format("DELETE FROM Bud_ContractConsTask WHERE TaskId = '{0}';", str);
                    base.ExcuteSql(sql);
                }
            }
        }

        public BudContractConsTask GetById(string id)
        {
            return (from t in this
                where t.ConsTaskId == id
                select t).FirstOrDefault<BudContractConsTask>();
        }

        public BudContractConsTask GetByRptIdAndTaskId(string rptId, string TaskId)
        {
            return (from ct in this
                where (ct.RptId == rptId) && (ct.TaskId == TaskId)
                select ct).FirstOrDefault<BudContractConsTask>();
        }

        public BudContractConsTask GetByTaskId(string rptId, string taskId)
        {
            return (from t in this
                where (t.RptId == rptId) && (t.TaskId == taskId)
                select t).FirstOrDefault<BudContractConsTask>();
        }
    }
}

