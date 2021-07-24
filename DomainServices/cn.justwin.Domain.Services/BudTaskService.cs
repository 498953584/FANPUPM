namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [NHibernateContext, ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall), ServiceContract, AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class BudTaskService : Repository<BudTask>
    {
        private void DeleModify(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_Modify WHERE PrjId = '{0}';", prjId);
            base.ExcuteSql(sql);
        }

        public void DeleteByProject(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_Task WHERE PrjId = '{0}';", prjId);
            base.ExcuteSql(sql);
        }

        private void DeleteVersions(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_TaskChange WHERE PrjId = '{0}';", prjId);
            base.ExcuteSql(sql);
        }

        public void DeleteYearMonthByPrj(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_Task WHERE PrjId = '{0}' AND TaskType IN ('Y','M');", prjId);
            base.ExcuteSql(sql);
        }

        public DataTable GetAllTable(string prjId)
        {
            string format = "\r\n                        WITH cteType AS\r\n                        (\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                                        FROM XPM_Basic_CodeList AS cl\r\n\t                                        INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                                        WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                        )\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber)AS No,dbo.fn_GetCount(TaskId) AS SubCount,cteType.CodeName AS TypeName,\r\n                        ISNULL(Total2/NULLIF(Quantity,0.0),0.0) AS UnitPrice ,\r\n                        TaskId,ParentId,OrderNumber,PrjId,TaskCode,TaskName,Unit,Quantity,StartDate,EndDate,Note,IsValid,\r\n                        ContractId,ISNULL(Total2,0.0)AS Total2,ModifyType,ModifyId,Modified,ConstructionPeriod,FeatureDescription,ISNULL(Total,0.0) AS Total\r\n                        FROM Bud_Task Task\r\n                        LEFT JOIN cteType \r\n                        ON LEN(OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                        WHERE PrjId='{0}' AND IsValid=1 AND TaskType=''";
            format = string.Format(format, prjId);
            return base.ExecuteQuery(format, new SqlParameter[0]);
        }

        public BudTask GetById(string taskId)
        {
            return (from t in this
                where (t.TaskId == taskId) && (t.IsValid == true)
                select t).FirstOrDefault<BudTask>();
        }

        public IList<BudTask> GetByProject(string prjId, int level = 0x3e7)
        {
            int orderNumberLen = level * 3;
            List<BudTask> source = (from t in this
                where ((t.PrjId == prjId) && (t.TaskType == string.Empty)) && (t.OrderNumber.Length <= orderNumberLen)
                orderby t.OrderNumber
                select t).ToList<BudTask>();
            BudModifyTaskService service = new BudModifyTaskService();
            BudModifyService service2 = new BudModifyService();
            foreach (BudModifyTask task in (from mt in service
                join m in service2 on mt.ModifyId equals m.ModifyId 
                where ((m.PrjId == prjId) && (m.Flowstate == 1)) && (mt.ModifyType == 0)
                select mt).ToList<BudModifyTask>())
            {
                BudModify byId = service2.GetById(task.ModifyId);
                BudTask item = new BudTask {
                    TaskId = task.ModifyTaskId,
                    ParentId = task.TaskId,
                    OrderNumber = task.OrderNumber,
                    Version = 1,
                    PrjId = prjId,
                    TaskCode = task.ModifyTaskCode,
                    TaskName = task.ModifyTaskContent,
                    Unit = task.Unit,
                    Quantity = new decimal?(task.Quantity),
                    UnitPrice = new decimal?(task.UnitPrice),
                    Total = new decimal?(task.Total),
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    ConstructionPeriod = task.ConstructionPeriod,
                    Note = task.Note,
                    InputDate = DateTime.Now,
                    InputUser = byId.InputUser,
                    TaskType = string.Empty,
                    IsValid = true
                };
                source.Add(item);
            }
            using (List<BudModifyTask>.Enumerator enumerator2 = (from mt in service
                join m in service2 on mt.ModifyId equals m.ModifyId 
                where ((m.PrjId == prjId) && (m.Flowstate == 1)) && (mt.ModifyType == 1)
                select mt).ToList<BudModifyTask>().GetEnumerator())
            {
                Func<BudTask, bool> predicate = null;
                BudModifyTask modifyTask;
                while (enumerator2.MoveNext())
                {
                    modifyTask = enumerator2.Current;
                    if (predicate == null)
                    {
                        predicate = t => t.TaskId == modifyTask.TaskId;
                    }
                    BudTask task3 = source.Where<BudTask>(predicate).FirstOrDefault<BudTask>();
                    if (task3 != null)
                    {
                        decimal? quantity = task3.Quantity;
                        decimal num = modifyTask.Quantity;
                        task3.Quantity = quantity.HasValue ? new decimal?(quantity.GetValueOrDefault() + num) : null;
                        decimal? total = task3.Total;
                        decimal num2 = modifyTask.Total;
                        task3.Total = total.HasValue ? new decimal?(total.GetValueOrDefault() + num2) : null;
                    }
                }
            }
            return source;
        }

        [WebGet(UriTemplate="/GetMonths/{prjId}/{year}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public IList<int> GetMonths(string prjId, string year)
        {
            int num = Convert.ToInt32(year);
            IList<BudTask> yearTasks = this.GetYearTask(prjId, num, 0x3e7);
            return this.GetMonths(yearTasks, num);
        }

        public IList<int> GetMonths(IList<BudTask> yearTasks, int year)
        {
            int start = 1;
            int month = 12;
            DateTime maxValue = DateTime.MaxValue;
            DateTime minValue = DateTime.MinValue;
            foreach (BudTask task in (from t in yearTasks
                where t.OrderNumber.Length == 3
                select t).ToList<BudTask>())
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

        public IList<BudTask> GetMonthTasks(IList<BudTask> yearTasks, int year, int month)
        {
            List<BudTask> list = new List<BudTask>();
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
            foreach (BudTask task in yearTasks)
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
                Func<BudTask, bool> predicate = null;
                for (int level = 1; level <= num2; level++)
                {
                    List<BudTask> list2 = new List<BudTask>();
                    if (predicate == null)
                    {
                        predicate = t => ((t.OrderNumber.Length == (level * 3)) && (t.StartDate.Value <= startDate)) && (t.EndDate.Value >= endDate);
                    }
                    list2 = yearTasks.Where<BudTask>(predicate).ToList<BudTask>();
                    for (int i = 0; i < list2.Count; i++)
                    {
                        DateTime time2;
                        string str = "M" + year + month.ToString().PadLeft(2, '0');
                        BudTask task2 = list2[i];
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
                        decimal? quantity = task2.Quantity;
                        decimal num5 = task2.ConstructionPeriod.Value;
                        decimal? nullable12 = quantity.HasValue ? new decimal?(quantity.GetValueOrDefault() * num5) : null;
                        decimal num6 = num4;
                        task2.Quantity = nullable12.HasValue ? new decimal?(nullable12.GetValueOrDefault() / num6) : null;
                        decimal? total = task2.Total;
                        decimal num7 = task2.ConstructionPeriod.Value;
                        decimal? nullable17 = total.HasValue ? new decimal?(total.GetValueOrDefault() * num7) : null;
                        decimal num8 = num4;
                        task2.Total = nullable17.HasValue ? new decimal?(nullable17.GetValueOrDefault() / num8) : null;
                        task2.TaskType = "M";
                        list.Add((BudTask) task2.Clone());
                    }
                }
            }
            return list;
        }

        public IList<BudTask> GetMonthTasks(string prjId, int year, int month, int level = 0x3e7)
        {
            string yearMonth = year + month.ToString().PadLeft(2, '0');
            return (from t in this
                where (((t.PrjId == prjId) && (t.TaskType == "M")) && (t.OrderNumber.Length <= (level * 3))) && (t.TaskId.Substring(1, 6) == yearMonth)
                orderby t.OrderNumber
                select t).ToList<BudTask>();
        }

        public string GetNextChildOrderNumber(string taskId)
        {
            string str = "001";
            BudModifyTaskService service = new BudModifyTaskService();
            BudTask byId = this.GetById(taskId);
            string str2 = (from t in this
                where t.ParentId == taskId
                orderby t.OrderNumber descending
                select t.OrderNumber).FirstOrDefault<string>();
            if (!string.IsNullOrEmpty(str2))
            {
                int num = Convert.ToInt32(str2.Substring(str2.Length - 3)) + 1;
                str = num.ToString().PadLeft(3, '0');
            }
            while (service.IsOrderNumberExists(byId.PrjId, byId.OrderNumber + str))
            {
                int num2 = Convert.ToInt32(str) + 1;
                str = num2.ToString().PadLeft(3, '0');
            }
            return (byId.OrderNumber + str);
        }

        public DataTable GetTable(string prjId)
        {
            string format = "\r\n                        WITH cteType AS\r\n                        (\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                                        FROM XPM_Basic_CodeList AS cl\r\n\t                                        INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                                        WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                        )\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber)AS No,dbo.fn_GetCount(TaskId) AS SubCount,cteType.CodeName AS TypeName,\r\n                        ISNULL(Total2/NULLIF(Quantity,0.0),0.0) AS UnitPrice ,\r\n                        TaskId,ParentId,OrderNumber,PrjId,TaskCode,TaskName,Unit,Quantity,StartDate,EndDate,Note,IsValid,\r\n                        ContractId,ISNULL(Total2,0.0)AS Total2,ModifyType,ModifyId,Modified,ConstructionPeriod,FeatureDescription,ISNULL(Total,0.0) AS Total\r\n                        FROM Bud_Task Task\r\n                        LEFT JOIN cteType \r\n                        ON LEN(OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                        WHERE PrjId='{0}' AND IsValid=1 AND LEN(OrderNumber)<=6 AND TaskType=''";
            format = string.Format(format, prjId);
            return base.ExecuteQuery(format, new SqlParameter[0]);
        }

        public BudTask GetTaskById(string taskId)
        {
            BudTask task = (from t in this
                where t.TaskId == taskId
                select t).FirstOrDefault<BudTask>();
            if (task == null)
            {
                BudModifyTask byTaskId = new BudModifyTaskService().GetByTaskId(taskId);
                if (byTaskId != null)
                {
                    BudModify byId = new BudModifyService().GetById(byTaskId.ModifyId);
                    task = new BudTask {
                        TaskId = byTaskId.ModifyTaskId,
                        ParentId = byTaskId.TaskId,
                        OrderNumber = byTaskId.OrderNumber,
                        Version = 1,
                        PrjId = byId.PrjId,
                        TaskCode = byTaskId.ModifyTaskCode,
                        TaskName = byTaskId.ModifyTaskContent,
                        Unit = byTaskId.Unit,
                        Quantity = new decimal?(byTaskId.Quantity),
                        UnitPrice = new decimal?(byTaskId.UnitPrice),
                        Total = new decimal?(byTaskId.Total),
                        StartDate = byTaskId.StartDate,
                        EndDate = byTaskId.EndDate,
                        ConstructionPeriod = byTaskId.ConstructionPeriod,
                        Note = byTaskId.Note,
                        InputDate = DateTime.Now,
                        InputUser = byId.InputUser,
                        TaskType = string.Empty,
                        IsValid = true,
                        Total2 = byTaskId.Total2
                    };
                }
            }
            return task;
        }

        public DataTable GetTaskChild(string taskId, string isWBSRelevance)
        {
            string format = "WITH cteType AS\r\n                        (\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                                    FROM XPM_Basic_CodeList AS cl\r\n\t                                    INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                                    WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                        )\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber)AS No,dbo.fn_GetCount(TaskId) AS SubCount,cteType.CodeName AS TypeName,\r\n                        ISNULL(Total2/NULLIF(Quantity,0.0),0.0) AS UnitPrice ,\r\n                        TaskId,ParentId,OrderNumber,PrjId,TaskCode,TaskName,Unit,Quantity,StartDate,EndDate,Note,IsValid,\r\n                        ContractId,ISNULL(Total2,0.0)AS Total2,ModifyType,ModifyId,Modified,ConstructionPeriod,FeatureDescription,ISNULL(Total,0.0) AS Total\r\n                        FROM Bud_Task Task\r\n                        LEFT JOIN cteType \r\n                        ON LEN(OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                        WHERE ParentId='{0}'AND IsValid=1";
            format = string.Format(format, taskId);
            return base.ExecuteQuery(format, new SqlParameter[0]);
        }

        public IList<int> GetYears(IList<BudTask> budTaskList)
        {
            int start = 0x270f;
            int year = 0;
            foreach (BudTask task in (from t in budTaskList
                where t.OrderNumber.Length == 3
                select t).ToList<BudTask>())
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

        [WebGet(UriTemplate="/GetYears/{prjId}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public IList<int> GetYears(string prjId)
        {
            IList<BudTask> byProject = this.GetByProject(prjId, 0x3e7);
            return this.GetYears(byProject);
        }

        public IList<BudTask> GetYearTask(IList<BudTask> taskList, int year)
        {
            List<BudTask> list = new List<BudTask>();
            int length = 0;
            foreach (BudTask task in taskList)
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
                int thisOrderNoLen = i * 3;
                List<BudTask> list2 = (from t in taskList
                    where ((t.OrderNumber.Length == thisOrderNoLen) && (t.StartDate.Value.Year <= year)) && (t.EndDate.Value.Year >= year)
                    orderby t.OrderNumber
                    select t).ToList<BudTask>();
                for (int j = 0; j < list2.Count; j++)
                {
                    string str = "Y" + year;
                    BudTask task2 = list2[j];
                    task2.TaskId = str + task2.TaskId.Substring(5);
                    if (!string.IsNullOrEmpty(task2.ParentId))
                    {
                        task2.ParentId = str + task2.ParentId.Substring(5);
                    }
                    int num5 = task2.ConstructionPeriod.Value;
                    DateTime time = (task2.StartDate.Value.Year == year) ? task2.StartDate.Value : new DateTime(year, 1, 1);
                    DateTime time2 = (task2.EndDate.Value.Year == year) ? task2.EndDate.Value : new DateTime(year, 12, 0x1f);
                    TimeSpan span = (TimeSpan) (time2 - time);
                    int num6 = span.Days + 1;
                    if (task2.ConstructionPeriod.Value > num6)
                    {
                        task2.ConstructionPeriod = new int?(num6);
                    }
                    decimal? quantity = task2.Quantity;
                    decimal num7 = task2.ConstructionPeriod.Value;
                    decimal? nullable13 = quantity.HasValue ? new decimal?(quantity.GetValueOrDefault() * num7) : null;
                    decimal num8 = num5;
                    task2.Quantity = nullable13.HasValue ? new decimal?(nullable13.GetValueOrDefault() / num8) : null;
                    decimal? total = task2.Total;
                    decimal num9 = task2.ConstructionPeriod.Value;
                    decimal? nullable18 = total.HasValue ? new decimal?(total.GetValueOrDefault() * num9) : null;
                    decimal num10 = num5;
                    task2.Total = nullable18.HasValue ? new decimal?(nullable18.GetValueOrDefault() / num10) : null;
                    task2.TaskType = "Y";
                    list.Add((BudTask) task2.Clone());
                }
            }
            return (from t in list
                orderby t.OrderNumber
                select t).ToList<BudTask>();
        }

        public IList<BudTask> GetYearTask(string prjId, int year, int level = 0x3e7)
        {
            return (from t in this
                where (((t.PrjId == prjId) && (t.TaskType == "Y")) && (t.TaskId.Substring(1, 4) == year.ToString())) && (t.OrderNumber.Length <= (level * 3))
                orderby t.OrderNumber
                select t).ToList<BudTask>();
        }

        public void SuperDel(string prjId)
        {
            string sql = string.Format("DELETE FROM Bud_Task WHERE PrjId = '{0}'", prjId);
            base.ExcuteSql(sql);
            this.DeleModify(prjId);
            this.DeleteVersions(prjId);
        }

        public void UpdateTotal2(string taskId)
        {
            try
            {
                BudTask byId = this.GetById(taskId);
                BudTaskResourceService service = new BudTaskResourceService();
                new BudModifyTaskService();
                decimal? nullable = ((IQueryable<decimal?>) (from r in service
                    where (r.TaskId == taskId) && (r.ResourceId != null)
                    select (r.ResourcePrice * r.ResourceQuantity) * r.LossCoefficient)).Sum();
                byId.Total2 = nullable;
                base.Update(byId);
                this.UpdateTotal2Up(byId.ParentId);
            }
            catch (Exception exception)
            {
                string title = string.Format("BudTaskService.UpdateTotal2({0})", taskId);
                Log4netHelper.Error(exception, title, "bery");
            }
        }

        public void UpdateTotal2Up(string parentId)
        {
            try
            {
                new BudModifyService();
                new BudModifyTaskService();
                while (!string.IsNullOrEmpty(parentId))
                {
                    BudTask byId = this.GetById(parentId);
                    if (byId == null)
                    {
                        return;
                    }
                    decimal? nullable = ((IQueryable<decimal?>) (from t in this
                        where t.ParentId == parentId
                        select t.Total2)).Sum();
                    if (!nullable.HasValue)
                    {
                        nullable = 0M;
                    }
                    byId.Total2 = nullable;
                    base.Update(byId);
                    parentId = byId.ParentId;
                }
            }
            catch (Exception exception)
            {
                string title = string.Format("BudTaskService.UpdateTotal2Up({0})", parentId);
                Log4netHelper.Error(exception, title, "bery");
            }
        }
    }
}

