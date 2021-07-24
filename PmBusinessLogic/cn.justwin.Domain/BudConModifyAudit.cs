namespace cn.justwin.Domain
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using PmBusinessLogic;
    using System;
    using System.Collections.Generic;

    public class BudConModifyAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            BudConModifyService service = new BudConModifyService();
            BudContractTaskService service2 = new BudContractTaskService();
            BudConModify byId = service.GetById(key.ToString());
            service2.DeleteYearMonthByPrj(byId.PrjId);
            IList<cn.justwin.Domain.Entities.BudContractTask> byProject = service2.GetByProject(byId.PrjId, 0x3e7);
            IList<int> years = service2.GetYears(byId.PrjId);
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

        public void RefuseEvent(object primarykey)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
        }
    }
}

