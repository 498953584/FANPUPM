namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using PmBusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class ContractTaskAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            cn.justwin.Domain.Entities.BudContractTaskAudit byId = new BudContractTaskAuditService().GetById(key.ToString());
            BudContractTaskService service2 = new BudContractTaskService();
            IList<cn.justwin.Domain.Entities.BudContractTask> byProject = service2.GetByProject(byId.PrjId, 0x3e7);
            IList<int> years = service2.GetYears(byProject);
            if (years != null)
            {
                foreach (int num in years)
                {
                    IList<cn.justwin.Domain.Entities.BudContractTask> yearTask = service2.GetYearTask(byProject, num);
                    foreach (cn.justwin.Domain.Entities.BudContractTask task in yearTask)
                    {
                        service2.Add(task);
                    }
                    foreach (int num2 in service2.GetMonths(yearTask, num))
                    {
                        foreach (cn.justwin.Domain.Entities.BudContractTask task2 in service2.GetMonthTasks(yearTask, num, num2))
                        {
                            service2.Add(task2);
                        }
                    }
                }
            }
        }

        private void DelContractTask(SqlTransaction trans, string prjId)
        {
            string str = "Delete Bud_ContractTask Where PrjId = @prjId";
            SqlParameter parameter = new SqlParameter("@prjId", SqlDbType.NVarChar, 500) {
                Value = prjId
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
        }

        private void DelContractTaskAudit(SqlTransaction trans, string id)
        {
            string str = "Delete Bud_ContractTaskAudit Where ContractTaskAuditId = @id";
            SqlParameter parameter = new SqlParameter("@id", SqlDbType.NVarChar, 500) {
                Value = id
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
        }

        public void RefuseEvent(object primarykey)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
            cn.justwin.Domain.BudContractTaskAudit modelById = cn.justwin.Domain.BudContractTaskAudit.GetModelById(key.ToString());
            if (modelById != null)
            {
                using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    this.DelContractTask(trans, modelById.PrjId);
                    this.DelContractTaskAudit(trans, key.ToString());
                    trans.Commit();
                }
            }
        }
    }
}

