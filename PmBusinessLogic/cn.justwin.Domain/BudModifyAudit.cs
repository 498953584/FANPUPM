namespace cn.justwin.Domain
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.Web;
    using PmBusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudModifyAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            BudModifyService service = new BudModifyService();
            BudTaskService service2 = new BudTaskService();
            BudModifyTaskService service3 = new BudModifyTaskService();
            BudTaskResourceService service4 = new BudTaskResourceService();
            ConPayoutModifyService service5 = new ConPayoutModifyService();
            string modifyId = key.ToString();
            BudModify budmodify = (from r in service
                where r.ModifyId == modifyId
                select r).FirstOrDefault<BudModify>();
            ConPayoutModify item = (from r in service5
                where r.BudModifyId == modifyId
                select r).FirstOrDefault<ConPayoutModify>();
            if (item != null)
            {
                item.FlowState = 1;
                service5.Update(item);
            }
            using (List<BudModifyTask>.Enumerator enumerator = (from p in service3
                where p.ModifyId == budmodify.ModifyId
                select p).ToList<BudModifyTask>().GetEnumerator())
            {
                BudModifyTask taskx;
                while (enumerator.MoveNext())
                {
                    taskx = enumerator.Current;
                    cn.justwin.Domain.Entities.BudTask task = (from p in service2
                                                               where p.TaskId == taskx.TaskId
                        select p).FirstOrDefault<cn.justwin.Domain.Entities.BudTask>();
                    if ((task != null) && (budmodify.Flowstate == 1))
                    {
                        if (taskx.ModifyType == 0)
                        {
                            task.IsValid = true;
                            service2.Update(task);
                            service3.UpdateTotal2(taskx.ModifyTaskId);
                        }
                        if ((task.ModifyId != task.ModifyId) || (taskx.ModifyType == 1))
                        {
                            task.ModifyId = task.ModifyId;
                            if (!task.Total2.HasValue)
                            {
                                task.Total2 = 0;
                            }
                            if (!task.Quantity.HasValue)
                            {
                                task.Quantity = 0;
                            }
                            task.Total2 = new decimal?(Convert.ToDecimal(task.Total2) + Convert.ToDecimal(task.Total2));
                            task.Quantity = new decimal?(Convert.ToDecimal(task.Quantity) + Convert.ToDecimal(task.Quantity));
                            if (task.Quantity != 0M)
                            {
                                task.UnitPrice = task.Total2 / task.Quantity;
                            }
                            service2.Update(task);
                            service3.UpdateTotal2(taskx.ModifyTaskId);
                        }
                    }
                }
            }
            if (ConfigHelper.Get("BudgetRequireDiff") != "0")
            {
                string str2 = ConfigHelper.Get("IsWBSRelevance");
                BudModify byId = service.GetById(key.ToString());
                service2.DeleteYearMonthByPrj(byId.PrjId);
                IList<cn.justwin.Domain.Entities.BudTask> byProject = service2.GetByProject(byId.PrjId, 0x3e7);
                IList<int> years = service2.GetYears(byId.PrjId);
                if (years != null)
                {
                    foreach (int num in years)
                    {
                        IList<cn.justwin.Domain.Entities.BudTask> yearTask = service2.GetYearTask(byProject, num);
                        foreach (cn.justwin.Domain.Entities.BudTask task2 in yearTask)
                        {
                            service2.Add(task2);
                        }
                        foreach (int num2 in service2.GetMonths(yearTask, num))
                        {
                            foreach (cn.justwin.Domain.Entities.BudTask task3 in service2.GetMonthTasks(yearTask, num, num2))
                            {
                                service2.Add(task3);
                            }
                        }
                    }
                    if (str2 == "1")
                    {
                        service4.AddTaskResource(byId.PrjId);
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

