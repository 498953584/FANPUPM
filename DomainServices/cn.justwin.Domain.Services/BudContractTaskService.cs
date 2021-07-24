namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [NHibernateContext, ServiceContract, AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed), ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class BudContractTaskService : Repository<BudContractTask>
    {
        public void AddRalationBudgetAndContract(string prjGuid, string ContractId, string orderNumber)
        {
            string sql = string.Format("UPDATE Bud_ContractTask SET ContractID='{1}'  \r\n                                            WHERE PrjId = '{0}' AND contractID IS NULL AND orderNumber LIKE '{2}%' ", prjGuid, ContractId, orderNumber);
            base.ExcuteSql(sql);
            sql = string.Empty;
            sql = string.Format("update Bud_ConModifyTask set ContractId='{1}'\r\n                                    where ModifyTaskID in\r\n                                    (\r\n                                        SELECT ModifyTaskId \r\n                                            FROM Bud_ConModifyTask conModifyTask\r\n                                            JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                                            WHERE PrjId='{0}' AND ModifyType=0\r\n                                            AND FlowState=1 AND ContractID IS NULL\r\n                                            AND OrderNumber like '{2}%'  \r\n                                    )", prjGuid, ContractId, orderNumber);
            base.ExcuteSql(sql);
        }

        private void DeleConModify(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_ConModify WHERE PrjId = '{0}';", prjId);
            base.ExcuteSql(sql);
        }

        public void DeleteByProject(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_ContractTask WHERE PrjId = '{0}';", prjId);
            base.ExcuteSql(sql);
        }

        private void DeleteVersions(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_ContractTaskAudit WHERE PrjId = '{0}';", prjId);
            base.ExcuteSql(sql);
        }

        public void DeleteYearMonthByPrj(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_ContractTask WHERE PrjId = '{0}' AND TaskType IN ('Y','M');", prjId);
            base.ExcuteSql(sql);
        }

        public void DelRalationBudgetAndContract(string prjGuid, string ContractID)
        {
            string sql = string.Format("UPDATE Bud_ContractTask SET ContractID=NULL  \r\n                                            WHERE PrjId = '{0}' AND contractID='{1}'", prjGuid, ContractID);
            base.ExcuteSql(sql);
            sql = string.Empty;
            sql = string.Format("update Bud_ConModifyTask set ContractId=NULL\r\n                                    where ModifyTaskID in\r\n                                    (\r\n                                        SELECT ModifyTaskId \r\n                                            FROM Bud_ConModifyTask conModifyTask\r\n                                            JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                                            WHERE PrjId='{0}' AND ModifyType=0\r\n                                            AND FlowState=1 AND ContractID='{1}' \r\n                                    )", prjGuid, ContractID);
            base.ExcuteSql(sql);
        }

        [OperationContract, WebGet(UriTemplate="/ConTask/{id}", ResponseFormat=WebMessageFormat.Json)]
        public BudContractTask GetById(string id)
        {
            return (from t in this
                where t.TaskId == id
                select t).FirstOrDefault<BudContractTask>();
        }

        public IList<BudContractTask> GetByProject(string prjId, int level = 0x3e7)
        {
            int orderNumberLen = level * 3;
            return (from t in this
                where ((t.PrjId == prjId) && (t.TaskType == string.Empty)) && (t.OrderNumber.Length <= orderNumberLen)
                orderby t.OrderNumber
                select t).ToList<BudContractTask>();
        }

        public IList<BudContractTask> GetChild(string id)
        {
            return (from t in this
                where t.ParentId == id
                orderby t.OrderNumber
                select t).ToList<BudContractTask>();
        }

        public IList<int> GetMonths(IList<BudContractTask> yearTasks, int year)
        {
            int start = 1;
            int month = 12;
            DateTime maxValue = DateTime.MaxValue;
            DateTime minValue = DateTime.MinValue;
            foreach (BudContractTask task in (from t in yearTasks
                where t.OrderNumber.Length == 3
                select t).ToList<BudContractTask>())
            {
                if (task.StartDate.Value < maxValue)
                {
                    maxValue = task.StartDate.Value;
                }
                if (task.EndDate.Value > minValue)
                {
                    minValue = task.EndDate.Value;
                }
            }
            if (maxValue.Year == year)
            {
                start = maxValue.Month;
            }
            if (minValue.Year == year)
            {
                month = minValue.Month;
            }
            try
            {
                return Enumerable.Range(start, (month - start) + 1).ToList<int>();
            }
            catch
            {
                return null;
            }
        }

        [WebGet(UriTemplate="/GetMonths/{prjId}/{year}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public IList<int> GetMonths(string prjId, string year)
        {
            int num = Convert.ToInt32(year);
            IList<BudContractTask> yearTasks = this.GetYearTask(prjId, num, 0x3e7);
            return this.GetMonths(yearTasks, num);
        }

        public IList<BudContractTask> GetMonthTasks(IList<BudContractTask> yearTasks, int year, int month)
        {
            List<BudContractTask> list = new List<BudContractTask>();
            int length = 0;
            DateTime startDate = DateTime.Now;
            if (month == 12)
            {
                startDate = new DateTime(year, month, 0x1f);
            }
            else
            {
                startDate = new DateTime(year, month + 1, 1).AddDays(-1.0);
            }
            DateTime endDate = new DateTime(year, month, 1);
            foreach (BudContractTask task in yearTasks)
            {
                if (!task.StartDate.HasValue)
                {
                    return list;
                }
                if (!task.EndDate.HasValue)
                {
                    return list;
                }
                if (!task.ConstructionPeriod.HasValue)
                {
                    return list;
                }
                if (task.OrderNumber.Length > length)
                {
                    length = task.OrderNumber.Length;
                }
            }
            if (length != 0)
            {
                int num2 = length / 3;
                Func<BudContractTask, bool> predicate = null;
                for (int level = 1; level <= num2; level++)
                {
                    List<BudContractTask> list2 = new List<BudContractTask>();
                    if (predicate == null)
                    {
                        predicate = t => ((t.OrderNumber.Length == (level * 3)) && (t.StartDate.Value <= startDate)) && (t.EndDate.Value >= endDate);
                    }
                    list2 = yearTasks.Where<BudContractTask>(predicate).ToList<BudContractTask>();
                    for (int i = 0; i < list2.Count; i++)
                    {
                        DateTime time2;
                        string str = "M" + year + month.ToString().PadLeft(2, '0');
                        BudContractTask task2 = list2[i];
                        task2.TaskId = str + task2.TaskId.Substring(7);
                        if (!string.IsNullOrEmpty(task2.ParentId))
                        {
                            task2.ParentId = str + task2.ParentId.Substring(7);
                        }
                        int num4 = task2.ConstructionPeriod.Value;
                        DateTime time = new DateTime(year, month, 1);
                        if (time < task2.StartDate.Value)
                        {
                            time = task2.StartDate.Value;
                        }
                        if (month == 12)
                        {
                            time2 = new DateTime(year, month, 0x1f);
                        }
                        else
                        {
                            time2 = new DateTime(year, month + 1, 1).AddDays(-1.0);
                        }
                        if (time2 > task2.EndDate.Value)
                        {
                            time2 = task2.EndDate.Value;
                        }
                        TimeSpan span = (TimeSpan) (time2 - time);
                        task2.ConstructionPeriod = new int?(span.Days + 1);
                        task2.Quantity = (task2.Quantity * task2.ConstructionPeriod.Value) / num4;
                        decimal? total = task2.Total;
                        decimal num5 = task2.ConstructionPeriod.Value;
                        decimal? nullable13 = total.HasValue ? new decimal?(total.GetValueOrDefault() * num5) : null;
                        decimal num6 = num4;
                        task2.Total = nullable13.HasValue ? new decimal?(nullable13.GetValueOrDefault() / num6) : null;
                        task2.TaskType = "M";
                        list.Add((BudContractTask) task2.Clone());
                    }
                }
            }
            return list;
        }

        public IList<BudContractTask> GetMonthTasks(string prjId, int year, int month, int level = 0x3e7)
        {
            string yearMonth = year + month.ToString().PadLeft(2, '0');
            return (from t in this
                where (((t.PrjId == prjId) && (t.TaskType == "M")) && (t.OrderNumber.Length <= (level * 3))) && (t.TaskId.Substring(1, 6) == yearMonth)
                orderby t.OrderNumber
                select t).ToList<BudContractTask>();
        }

        public int GetSubCount(string id)
        {
            BudConModifyTaskService service = new BudConModifyTaskService();
            int num = (from t in this
                where t.ParentId == id
                select t).Count<BudContractTask>();
            int num2 = (from t in service
                where (t.ParentId == id) && (t.ModifyType == 0)
                select t).Count<BudConModifyTask>();
            return (num + num2);
        }

        public BudContractTask GetTaskById(string id)
        {
            BudContractTask task = (from t in this
                where t.TaskId == id
                select t).FirstOrDefault<BudContractTask>();
            if (task == null)
            {
                BudConModifyTaskService service = new BudConModifyTaskService();
                BudConModifyTask task2 = (from r in service
                    where r.TaskId == id
                    select r).FirstOrDefault<BudConModifyTask>();
                if (task2 != null)
                {
                    BudConModify byId = new BudConModifyService().GetById(task2.ModifyId);
                    task = new BudContractTask {
                        TaskId = task2.TaskId,
                        ParentId = task2.ParentId,
                        OrderNumber = task2.OrderNumber,
                        PrjId = byId.PrjId,
                        TaskCode = task2.ModifyTaskCode,
                        TaskName = task2.ModifyTaskContent,
                        Unit = task2.Unit,
                        Quantity = task2.Quantity,
                        UnitPrice = new decimal?(task2.UnitPrice),
                        Total = new decimal?(task2.Total),
                        StartDate = task2.StartDate,
                        EndDate = task2.EndDate,
                        ConstructionPeriod = task2.ConstructionPeriod,
                        Note = task2.Note,
                        InputDate = DateTime.Now,
                        InputUser = byId.InputUser,
                        TaskType = string.Empty,
                        IsValid = true
                    };
                }
                return task;
            }
            BudConModifyTaskService service3 = new BudConModifyTaskService();
            BudConModifyTask task3 = (from r in service3
                where r.TaskId == id
                select r).FirstOrDefault<BudConModifyTask>();
            if (task3 != null)
            {
                task.StartDate = task3.StartDate;
                task.EndDate = task3.EndDate;
                decimal? total = task.Total;
                decimal num = task3.Total;
                task.Total = total.HasValue ? new decimal?(total.GetValueOrDefault() + num) : null;
                task.Quantity += task3.Quantity;
            }
            return task;
        }

        public IList<int> GetYears(IList<BudContractTask> tasks)
        {
            int start = 0x270f;
            int year = 0;
            foreach (BudContractTask task in (from t in tasks
                where t.OrderNumber.Length == 3
                select t).ToList<BudContractTask>())
            {
                if (task.StartDate.HasValue && task.EndDate.HasValue)
                {
                    if (task.StartDate.HasValue && (task.StartDate.Value.Year < start))
                    {
                        start = task.StartDate.Value.Year;
                    }
                    if (task.EndDate.HasValue && (task.EndDate.Value.Year > year))
                    {
                        year = task.EndDate.Value.Year;
                    }
                }
            }
            try
            {
                return Enumerable.Range(start, (year - start) + 1).ToList<int>();
            }
            catch
            {
                return null;
            }
        }

        [OperationContract, WebGet(UriTemplate="/GetYears/{prjId}", ResponseFormat=WebMessageFormat.Json)]
        public IList<int> GetYears(string prjId)
        {
            IList<BudContractTask> byProject = this.GetByProject(prjId, 0x3e7);
            return this.GetYears(byProject);
        }

        public IList<BudContractTask> GetYearTask(IList<BudContractTask> tasks, int year)
        {
            List<BudContractTask> list = new List<BudContractTask>();
            int length = 0;
            foreach (BudContractTask task in tasks)
            {
                if (!task.StartDate.HasValue)
                {
                    return list;
                }
                if (!task.EndDate.HasValue)
                {
                    return list;
                }
                if (!task.ConstructionPeriod.HasValue)
                {
                    return list;
                }
                if (task.OrderNumber.Length > length)
                {
                    length = task.OrderNumber.Length;
                }
            }
            int num2 = length / 3;
            for (int i = 1; i <= num2; i++)
            {
                List<BudContractTask> source = new List<BudContractTask>();
                foreach (BudContractTask task2 in tasks)
                {
                    if (((task2.OrderNumber.Length == (i * 3)) && (task2.StartDate.Value.Year <= year)) && (task2.EndDate.Value.Year >= year))
                    {
                        source.Add(task2);
                    }
                }
                for (int j = 0; j < source.Count<BudContractTask>(); j++)
                {
                    string str = "Y" + year;
                    BudContractTask task3 = source[j];
                    task3.TaskId = str + task3.TaskId.Substring(5);
                    if (!string.IsNullOrEmpty(task3.ParentId))
                    {
                        task3.ParentId = str + task3.ParentId.Substring(5);
                    }
                    int num5 = task3.ConstructionPeriod.Value;
                    DateTime time = (task3.StartDate.Value.Year == year) ? task3.StartDate.Value : new DateTime(year, 1, 1);
                    DateTime time2 = (task3.EndDate.Value.Year == year) ? task3.EndDate.Value : new DateTime(year, 12, 0x1f);
                    TimeSpan span = (TimeSpan) (time2 - time);
                    int num6 = span.Days + 1;
                    if (task3.ConstructionPeriod.Value > num6)
                    {
                        task3.ConstructionPeriod = new int?(num6);
                    }
                    task3.Quantity = (task3.Quantity * task3.ConstructionPeriod.Value) / num5;
                    decimal? total = task3.Total;
                    decimal num7 = task3.ConstructionPeriod.Value;
                    decimal? nullable16 = total.HasValue ? new decimal?(total.GetValueOrDefault() * num7) : null;
                    decimal num8 = num5;
                    task3.Total = nullable16.HasValue ? new decimal?(nullable16.GetValueOrDefault() / num8) : null;
                    task3.TaskType = "Y";
                    list.Add((BudContractTask) task3.Clone());
                }
            }
            return (from t in list
                orderby t.OrderNumber
                select t).ToList<BudContractTask>();
        }

        public IList<BudContractTask> GetYearTask(string prjId, int year, int level = 0x3e7)
        {
            return (from t in this
                where (((t.PrjId == prjId) && (t.TaskType == "Y")) && (t.TaskId.Substring(1, 4) == year.ToString())) && (t.OrderNumber.Length <= (level * 3))
                orderby t.OrderNumber
                select t).ToList<BudContractTask>();
        }

        public void SuperDel(string prjId)
        {
            if ((from task in this
                where (task.PrjId == prjId) && (task.TaskType == "")
                select task).ToList<BudContractTask>().Count == 0)
            {
                this.DeleConModify(prjId);
                this.DeleteVersions(prjId);
            }
        }
    }
}

