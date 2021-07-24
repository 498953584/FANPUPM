namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class ConstructTask
    {
        private BudModifyService budModifySer = new BudModifyService();
        private BudModifyTaskResService budModifyTaskResSer = new BudModifyTaskResService();
        private BudModifyTaskService budModifyTaskSer = new BudModifyTaskService();
        private IList<ConstructResource> resourceList;

        private ConstructTask()
        {
        }

        public void Add(ConstructTask consTask, string isWBSRelevance)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                (from m in entities.Bud_Task
                    where m.TaskId == consTask.taskId
                    select m).FirstOrDefault<Bud_Task>();
                Bud_ConsReport report = (from m in entities.Bud_ConsReport
                    where m.ConsReportId == consTask.reportId
                    select m).FirstOrDefault<Bud_ConsReport>();
                Bud_ConsTask task = new Bud_ConsTask {
                    TaskId = consTask.taskId,
                    ConsTaskId = consTask.Id,
                    Note = consTask.Note,
                    Bud_ConsReport = report,
                    CompleteQuantity = consTask.CompleteQuantity,
                    WorkContent = consTask.WorkContent
                };
                entities.AddToBud_ConsTask(task);
                entities.SaveChanges();
                if (isWBSRelevance == "1")
                {
                    var list = (from m in entities.Bud_TaskResource
                        where m.Bud_Task.TaskId == consTask.taskId
                        select new { resourceId = m.Res_Resource.ResourceId, resourcePrice = m.ResourcePrice, resourceQuantity = m.ResourceQuantity }).ToList();
                    List<BudModifyTaskRes> first = (from mt in this.budModifyTaskSer
                        join m in this.budModifySer on mt.ModifyId equals m.ModifyId 
                        join mtr in this.budModifyTaskResSer on mt.ModifyTaskId equals mtr.ModifyTaskId 
                        where (mt.TaskId == consTask.taskId) && (m.Flowstate == 1)
                        select mtr).ToList<BudModifyTaskRes>();
                    List<BudModifyTaskRes> second = (from mt in this.budModifyTaskSer
                        join m in this.budModifySer on mt.ModifyId equals m.ModifyId 
                        join mtr in this.budModifyTaskResSer on mt.ModifyTaskId equals mtr.ModifyTaskId 
                        where (mt.ModifyTaskId == consTask.taskId) && (m.Flowstate == 1)
                        select mtr).ToList<BudModifyTaskRes>();
                    first = first.Union<BudModifyTaskRes>(second).ToList<BudModifyTaskRes>();
                    using (var enumerator = list.GetEnumerator())
                    {
                        System.Func<BudModifyTaskRes, bool> predicate = null;
                        while (enumerator.MoveNext())
                        {
                            var resx = enumerator.Current;
                            decimal num = 0M;
                            decimal unitPrice = 0M;
                            decimal num3 = 0M;
                            if (resx.resourceQuantity.HasValue && resx.resourcePrice.HasValue)
                            {
                                num = resx.resourceQuantity.Value;
                                num3 = num * resx.resourcePrice.Value;
                            }
                            if (predicate == null)
                            {
                                predicate = listx => listx.ResourceId == resx.resourceId;
                            }
                            foreach (BudModifyTaskRes res in first.Where<BudModifyTaskRes>(predicate).ToList<BudModifyTaskRes>())
                            {
                                if (res != null)
                                {
                                    num += res.ResourceQuantity;
                                    num3 += res.ResourcePrice * res.ResourceQuantity;
                                    first.Remove(res);
                                }
                            }
                            if (num != 0M)
                            {
                                unitPrice = num3 / num;
                            }
                            ConstructResource consRes = ConstructResource.Create(Guid.NewGuid().ToString(), consTask.Id, resx.resourceId, 0M, unitPrice);
                            consRes.Add(consRes);
                        }
                    }
                    using (List<string>.Enumerator enumerator3 = this.budModifyTaskResSer.GetModifyTasResIds(first).GetEnumerator())
                    {
                        System.Func<BudModifyTaskRes, bool> func2 = null;
                        string resId;
                        while (enumerator3.MoveNext())
                        {
                            resId = enumerator3.Current;
                            decimal num4 = 0M;
                            decimal num5 = 0M;
                            decimal num6 = 0M;
                            if (func2 == null)
                            {
                                func2 = listx => listx.ResourceId == resId;
                            }
                            foreach (BudModifyTaskRes res2 in first.Where<BudModifyTaskRes>(func2).ToList<BudModifyTaskRes>())
                            {
                                if (res2 != null)
                                {
                                    num4 += res2.ResourceQuantity;
                                    num6 += res2.ResourcePrice * res2.ResourceQuantity;
                                    first.Remove(res2);
                                }
                            }
                            if (num4 != 0M)
                            {
                                num5 = num6 / num4;
                            }
                            Resource.GetById(resId);
                            ConstructResource resource2 = ConstructResource.Create(Guid.NewGuid().ToString(), consTask.Id, resId, 0M, num5);
                            resource2.Add(resource2);
                        }
                    }
                }
            }
        }

        public static void AddResource(ConstructTask consTask)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                IList<ConstructResource> resourceList = new List<ConstructResource>();
                resourceList = consTask.ResourceList;
                DelByConsTaskId(consTask.Id, entities);
                using (IEnumerator<ConstructResource> enumerator = resourceList.GetEnumerator())
                {
                    ConstructResource consResource;
                    while (enumerator.MoveNext())
                    {
                        consResource = enumerator.Current;
                        Bud_ConsTaskRes res = new Bud_ConsTaskRes {
                            ConsTaskResId = consResource.Id,
                            Bud_ConsTask = entities.Bud_ConsTask.FirstOrDefault<Bud_ConsTask>(c => c.ConsTaskId == consResource.ConsTaskId),
                            Res_Resource = entities.Res_Resource.FirstOrDefault<Res_Resource>(c => c.ResourceId == consResource.ResourceId),
                            Quantity = consResource.Quantity,
                            UnitPrice = consResource.UnitPrice,
                            AccountingQuantity = new decimal?(consResource.Quantity)
                        };
                        entities.AddToBud_ConsTaskRes(res);
                    }
                }
                entities.SaveChanges();
            }
        }

        public static void AuditConsTask(List<ConstructTask> consTaskList)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<ConstructTask>.Enumerator enumerator = consTaskList.GetEnumerator())
                {
                    ConstructTask consTask;
                    while (enumerator.MoveNext())
                    {
                        consTask = enumerator.Current;
                        Bud_ConsTask task = (from ct in entities.Bud_ConsTask
                            where ct.ConsTaskId == consTask.Id
                            select ct).FirstOrDefault<Bud_ConsTask>();
                        if (task != null)
                        {
                            task.AccountingQuantity = new decimal?(consTask.AccountingQuantity);
                        }
                    }
                }
                entities.SaveChanges();
            }
        }

        public static void Clear(string reportId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_ConsTask entity = (from m in entities.Bud_ConsTask
                    where m.Bud_ConsReport.ConsReportId == reportId
                    select m).FirstOrDefault<Bud_ConsTask>();
                if (entity != null)
                {
                    entities.DeleteObject(entity);
                    entities.SaveChanges();
                }
            }
        }

        public static ConstructTask Create(string id, string reportId, string taskId, decimal completeQuantity, string content, string note, IList<ConstructResource> resourceList)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("Id");
            }
            return new ConstructTask { Id = id, taskId = taskId, reportId = reportId, CompleteQuantity = completeQuantity, WorkContent = content, Note = note, ResourceList = resourceList };
        }

        private static void DelByConsTaskId(string id, pm2Entities context)
        {
            foreach (Bud_ConsTaskRes res in (from c in context.Bud_ConsTaskRes
                where c.Bud_ConsTask.ConsTaskId == id
                select c).ToList<Bud_ConsTaskRes>())
            {
                if (res != null)
                {
                    context.DeleteObject(res);
                }
            }
        }

        public static void DelByTaskId(string taskId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<Bud_ConsTask> list = (from m in entities.Bud_ConsTask
                    where m.TaskId == taskId
                    select m).ToList<Bud_ConsTask>();
                if ((list != null) && (list.Count > 0))
                {
                    foreach (Bud_ConsTask task in list)
                    {
                        entities.DeleteObject(task);
                    }
                    entities.SaveChanges();
                }
            }
        }

        public static void Delete(List<string> idList)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (idList.Count == 1)
                {
                    Delete(idList[0], entities);
                }
                else
                {
                    Delete(idList, entities);
                }
            }
        }

        private static void Delete(string id, pm2Entities context)
        {
            Bud_ConsTask entity = (from m in context.Bud_ConsTask
                where m.ConsTaskId == id
                select m).FirstOrDefault<Bud_ConsTask>();
            if (entity == null)
            {
                throw new Exception("已删除不存在!");
            }
            context.DeleteObject(entity);
            context.SaveChanges();
        }

        private static void Delete(List<string> idList, pm2Entities context)
        {
            using (List<string>.Enumerator enumerator = idList.GetEnumerator())
            {
                string id;
                while (enumerator.MoveNext())
                {
                    id = enumerator.Current;
                    Bud_ConsTask entity = (from m in context.Bud_ConsTask
                        where m.ConsTaskId == id
                        select m).FirstOrDefault<Bud_ConsTask>();
                    if (entity != null)
                    {
                        context.DeleteObject(entity);
                        context.SaveChanges();
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (base.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.Id == ((ConstructTask) obj).Id);
        }

        public static List<ConstructTask> GetAll(string reportId, List<string> taskIds)
        {
            List<ConstructTask> list = new List<ConstructTask>();
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<string>.Enumerator enumerator = taskIds.GetEnumerator())
                {
                    string taskId;
                    while (enumerator.MoveNext())
                    {
                        taskId = enumerator.Current;
                        Bud_ConsTask task = (from m in entities.Bud_ConsTask
                            where (m.Bud_ConsReport.ConsReportId == reportId) && (m.TaskId == taskId)
                            select m).FirstOrDefault<Bud_ConsTask>();
                        decimal num = 0M;
                        try
                        {
                            num = ((IQueryable<decimal>) (from m in entities.Bud_ConsTask
                                where m.TaskId == taskId
                                select m.CompleteQuantity)).Sum();
                        }
                        catch
                        {
                        }
                        ConstructTask item = new ConstructTask();
                        if (task != null)
                        {
                            item.Id = task.ConsTaskId;
                            item.Note = task.Note;
                            item.CompleteQuantity = task.CompleteQuantity;
                            item.WorkContent = task.WorkContent;
                            item.taskId = taskId;
                            item.SurplusQuantity = decimal.Subtract(item.BudTask.Quantity, num);
                            item.reportId = reportId;
                        }
                        else
                        {
                            item.Id = string.Empty;
                            item.Note = string.Empty;
                            item.CompleteQuantity = 0M;
                            item.WorkContent = string.Empty;
                            item.taskId = taskId;
                            item.SurplusQuantity = decimal.Subtract(item.BudTask.Quantity, num);
                            item.reportId = reportId;
                        }
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static DataTable GetAllByTaskIds(string taskIds, string reportId, bool isUpdate, bool isEditAccountingQuantity)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT ConsTaskId,TaskCode,TaskName,ConsReportId,TaskId,Quantity,Unit,SumAccountingQuantity,SurplusQuantity,CompleteQuantity,AccountingQuantity ");
            if (isUpdate)
            {
                builder.AppendLine(",WorkContent,Note FROM ( ");
            }
            else
            {
                builder.AppendLine("FROM ( ");
            }
            builder.AppendLine("SELECT ConsTaskId,TaskCode,TaskName,ConsReportId,cons.TaskId,Quantity,BudTask.Unit,OrderNumber,ISNULL(SumAccountingData.SumAccountingQuantity,0) SumAccountingQuantity, ");
            builder.AppendLine("ISNULL((Quantity-ISNULL(SumAccountingData.SumAccountingQuantity,0)),0) AS SurplusQuantity,CompleteQuantity ");
            if (isEditAccountingQuantity)
            {
                builder.AppendLine(",ISNULL(AccountingQuantity,CompleteQuantity)  AS  AccountingQuantity ");
            }
            else
            {
                builder.AppendLine(",ISNULL(AccountingQuantity,0) AS AccountingQuantity ");
            }
            if (isUpdate)
            {
                builder.AppendLine(",WorkContent,cons.Note ");
            }
            builder.AppendLine();
            builder.AppendLine("FROM Bud_ConsTask AS cons  ");
            builder.AppendLine("JOIN ( ");
            builder.AppendLine("     --变更与原预算结合 ");
            builder.AppendFormat("\t\r\n                               SELECT  CT.TaskId,CT.ParentId,CT.OrderNumber,CT.Version,CT.PrjId,CT.TaskCode,CT.TaskName,CT.Unit,\r\n                                (ISNULL(CT.Quantity,0.0)+ISNULL(MT.Quantity,0.0))as Quantity,CT.UnitPrice,CT.StartDate,CT.EndDate,CT.Note,CT.IsValid,CT.InputUser,CT.InputDate,\r\n                                CT.Total,CT.Modified,CT.ConstructionPeriod  FROM Bud_Task  CT\r\n                                LEFT JOIN (SELECT sum(Quantity)AS Quantity,T.TaskId   from  Bud_ModifyTask T  \r\n                                WHERE T.TaskId IN ({0})\r\n                                GROUP BY T.TaskId  ) MT\r\n                                ON CT. TaskId+'-'+CT.OrderNumber=MT.TaskId\r\n                                WHERE CT.TaskId IN ({0})", taskIds);
            builder.AppendLine();
            builder.AppendLine("\tUNION ");
            builder.AppendLine("\tSELECT TaskId,NULL ParentId,OrderNumber,null Version,NULL PrjId,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit, ");
            builder.AppendLine("\tQuantity,UnitPrice,StartDate,EndDate,modifyTask.Note,NULL IsVaild,NULL InputUser,NULL InputDate,dbo.fn_GetModifyTotal(ModifyTaskId) Total,NULL Modified,NULL ConstructionPeriod ");
            builder.AppendLine("\tFROM Bud_ModifyTask modifyTask ");
            builder.AppendFormat("\tWHERE ModifyType=0 AND TaskId IN ({0})", taskIds);
            builder.AppendLine();
            builder.AppendLine(") BudTask ON BudTask.TaskId = cons.TaskId  ");
            builder.AppendLine("LEFT JOIN (SELECT TaskId,Sum(AccountingQuantity) SumAccountingQuantity FROM Bud_ConsTask  ");
            builder.AppendFormat("LEFT JOIN Bud_ConsReport AS consReport ON Bud_ConsTask.ConsReportId=consReport.ConsReportId WHERE TaskId IN  ({0})", taskIds);
            builder.AppendLine("AND FlowState=1 group by TaskId) SumAccountingData ON cons.TaskId=SumAccountingData.TaskId ");
            builder.AppendLine();
            builder.AppendFormat("WHERE cons.TaskId IN ({0}) AND ConsReportId = '{1}'", taskIds, reportId);
            builder.AppendLine();
            builder.AppendLine("UNION");
            builder.AppendLine();
            builder.AppendLine("SELECT * FROM(");
            builder.AppendLine("SELECT ConsTaskId,TaskCode,TaskName,ConsReportId,BudTask.TaskId,Quantity,BudTask.Unit,OrderNumber,ISNULL(SumAccountingData.SumAccountingQuantity,0) SumAccountingQuantity, ");
            builder.AppendLine("ISNULL(CompleteQuantity,0) AS CompleteQuantity ");
            if (isEditAccountingQuantity)
            {
                builder.AppendLine(",ISNULL(AccountingQuantity,CompleteQuantity) AS  AccountingQuantity");
            }
            else
            {
                builder.AppendLine(",ISNULL(AccountingQuantity,0) AS  AccountingQuantity");
            }
            builder.AppendLine(",ISNULL((Quantity-ISNULL(SumAccountingData.SumAccountingQuantity,0)),0) AS SurplusQuantity");
            if (isUpdate)
            {
                builder.AppendLine(",WorkContent,cons.Note ");
            }
            builder.AppendLine();
            builder.AppendLine("FROM Bud_ConsTask AS cons ");
            builder.AppendLine("RIGHT JOIN ( ");
            builder.AppendLine("   \r\n                               SELECT TaskId,ParentId,OrderNumber,Version,PrjId,TaskCode,TaskName,Unit,\r\n\t                           Quantity,UnitPrice,StartDate,EndDate,Note,IsValid,InputUser,InputDate,\r\n\t                           Total,Modified,ConstructionPeriod  FROM Bud_Task ");
            builder.AppendLine("    UNION ");
            builder.AppendLine("    SELECT TaskId,NULL ParentId,OrderNumber,null Version,NULL PrjId,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit, ");
            builder.AppendLine("    Quantity,UnitPrice,StartDate,EndDate,modifyTask.Note,NULL IsVaild,NULL InputUser,NULL InputDate,dbo.fn_GetModifyTotal(ModifyTaskId) Total,NULL Modified,NULL ConstructionPeriod ");
            builder.AppendLine("    FROM Bud_ModifyTask modifyTask ");
            builder.AppendLine("    WHERE ModifyType=0  ");
            builder.AppendLine(") BudTask ON BudTask.TaskId = cons.TaskId  ");
            builder.AppendLine("LEFT JOIN (SELECT TaskId,Sum(AccountingQuantity) SumAccountingQuantity FROM Bud_ConsTask  ");
            builder.AppendFormat("LEFT JOIN Bud_ConsReport AS consReport ON Bud_ConsTask.ConsReportId=consReport.ConsReportId WHERE TaskId IN  ({0})", taskIds);
            builder.AppendLine("AND FlowState=1 group by TaskId) SumAccountingData ON cons.TaskId=SumAccountingData.TaskId ");
            builder.AppendLine();
            builder.AppendFormat("WHERE BudTask.TaskId IN({0})) AS RESULT WHERE TaskId NOT IN(SELECT TaskId FROM Bud_ConsTask WHERE ConsReportId ='{1}')", taskIds, reportId);
            builder.AppendLine(" )tab1 order by orderNumber ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        private static List<Bud_ConsTask> GetBud_ConsTask(List<string> idList, pm2Entities contxt)
        {
            List<Bud_ConsTask> list = new List<Bud_ConsTask>();
            using (List<string>.Enumerator enumerator = idList.GetEnumerator())
            {
                string id;
                while (enumerator.MoveNext())
                {
                    id = enumerator.Current;
                    Bud_ConsTask item = contxt.Bud_ConsTask.FirstOrDefault<Bud_ConsTask>(c => c.ConsTaskId == id);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static ConstructTask GetById(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from ct in entities.Bud_ConsTask
                    where ct.ConsTaskId == id
                    select new ConstructTask { Id = ct.ConsTaskId, taskId = ct.TaskId, CompleteQuantity = ct.CompleteQuantity, Note = ct.Note, WorkContent = ct.WorkContent }).FirstOrDefault<ConstructTask>();
            }
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool GetIsUsedByTask(string taskId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_ConsTask
                    where m.TaskId == taskId
                    select m).ToList<Bud_ConsTask>().Count > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static List<string> GetTaskIdsByReportId(string reportId)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> list2 = (from m in entities.Bud_ConsTask
                    where m.Bud_ConsReport.ConsReportId == reportId
                    select m.TaskId).ToList<string>();
                if ((list2 == null) || (list2.Count <= 0))
                {
                    return list;
                }
                foreach (string str in list2)
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public static IList<ConstructTask> GetyByReportId(string reportId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from ct in entities.Bud_ConsTask
                    where ct.Bud_ConsReport.ConsReportId == reportId
                    select new ConstructTask { Id = ct.ConsTaskId, taskId = ct.TaskId, CompleteQuantity = ct.CompleteQuantity, Note = ct.Note, WorkContent = ct.WorkContent }).ToList<ConstructTask>();
            }
        }

        public static bool isExist(string consReportId, string taskId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from ct in entities.Bud_ConsTask
                    where (ct.Bud_ConsReport.ConsReportId == consReportId) && (ct.TaskId == taskId)
                    select new ConstructTask { Id = ct.ConsTaskId, taskId = ct.TaskId, CompleteQuantity = ct.CompleteQuantity, Note = ct.Note, WorkContent = ct.WorkContent }).FirstOrDefault<ConstructTask>() != null)
                {
                    flag = true;
                }
                return flag;
            }
        }

        public void SetResQuantityByConsQuantity(decimal quantity)
        {
            cn.justwin.Domain.BudTask byId = cn.justwin.Domain.BudTask.GetById(this.taskId);
            decimal num = 0M;
            if (byId == null)
            {
                BudModifyTask task2 = this.budModifyTaskSer.GetById(this.taskId);
                if ((task2 != null) && (task2.Quantity != 0M))
                {
                    num = quantity / task2.Quantity;
                }
            }
            else if (byId.Quantity != 0M)
            {
                num = quantity / byId.Quantity;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                List<BudModifyTaskRes> first = (from mt in this.budModifyTaskSer
                    join m in this.budModifySer on mt.ModifyId equals m.ModifyId 
                    join mtr in this.budModifyTaskResSer on mt.ModifyTaskId equals mtr.ModifyTaskId 
                    where (mt.TaskId == this.taskId) && (m.Flowstate == 1)
                    select mtr).ToList<BudModifyTaskRes>();
                List<BudModifyTaskRes> second = (from mt in this.budModifyTaskSer
                    join m in this.budModifySer on mt.ModifyId equals m.ModifyId 
                    join mtr in this.budModifyTaskResSer on mt.ModifyTaskId equals mtr.ModifyTaskId 
                    where (mt.ModifyTaskId == this.taskId) && (m.Flowstate == 1)
                    select mtr).ToList<BudModifyTaskRes>();
                first = first.Union<BudModifyTaskRes>(second).ToList<BudModifyTaskRes>();
                if (byId != null)
                {
                    using (IEnumerator<TaskResource> enumerator = byId.Resources.GetEnumerator())
                    {
                        System.Func<BudModifyTaskRes, bool> predicate = null;
                        TaskResource item;
                        while (enumerator.MoveNext())
                        {
                            item = enumerator.Current;
                            decimal num2 = 0M;
                            num2 = item.Quantity;
                            if (predicate == null)
                            {
                                predicate = list => list.ResourceId == item.Resource.Id;
                            }
                            foreach (BudModifyTaskRes res in first.Where<BudModifyTaskRes>(predicate).ToList<BudModifyTaskRes>())
                            {
                                if (item != null)
                                {
                                    num2 += res.ResourceQuantity;
                                    first.Remove(res);
                                }
                            }
                            for (int i = 0; i < this.ResourceList.Count; i++)
                            {
                                string resourceId = this.resourceList[i].ResourceId;
                                if (resourceId == item.Resource.Id)
                                {
                                    Bud_ConsTaskRes res2 = (from ctr in entities.Bud_ConsTaskRes
                                        where (ctr.Res_Resource.ResourceId == resourceId) && (ctr.Bud_ConsTask.ConsTaskId == this.Id)
                                        select ctr).FirstOrDefault<Bud_ConsTaskRes>();
                                    if (res2 != null)
                                    {
                                        if (num > 1M)
                                        {
                                            res2.Quantity = num2;
                                            res2.AccountingQuantity = new decimal?(num2);
                                        }
                                        else
                                        {
                                            res2.Quantity = num * num2;
                                            res2.AccountingQuantity = new decimal?(num * num2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                using (List<string>.Enumerator enumerator3 = this.budModifyTaskResSer.GetModifyTasResIds(first).GetEnumerator())
                {
                    System.Func<BudModifyTaskRes, bool> func2 = null;
                    string resId;
                    while (enumerator3.MoveNext())
                    {
                        resId = enumerator3.Current;
                        decimal num4 = 0M;
                        if (func2 == null)
                        {
                            func2 = list => list.ResourceId == resId;
                        }
                        foreach (BudModifyTaskRes res3 in first.Where<BudModifyTaskRes>(func2).ToList<BudModifyTaskRes>())
                        {
                            num4 += res3.ResourceQuantity;
                            first.Remove(res3);
                        }
                        for (int j = 0; j < this.ResourceList.Count; j++)
                        {
                            string resourceId = this.resourceList[j].ResourceId;
                            if (resourceId == resId)
                            {
                                Bud_ConsTaskRes res4 = (from ctr in entities.Bud_ConsTaskRes
                                    where (ctr.Res_Resource.ResourceId == resourceId) && (ctr.Bud_ConsTask.ConsTaskId == this.Id)
                                    select ctr).FirstOrDefault<Bud_ConsTaskRes>();
                                if (res4 != null)
                                {
                                    if (num > 1M)
                                    {
                                        res4.Quantity = num4;
                                        res4.AccountingQuantity = new decimal?(num4);
                                    }
                                    else
                                    {
                                        res4.Quantity = num * num4;
                                        res4.AccountingQuantity = new decimal?(num * num4);
                                    }
                                }
                            }
                        }
                    }
                }
                entities.SaveChanges();
            }
        }

        public void Update(ConstructTask consTask)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_ConsTask task = (from m in entities.Bud_ConsTask
                    where m.ConsTaskId == consTask.Id
                    select m).FirstOrDefault<Bud_ConsTask>();
                if (task == null)
                {
                    throw new Exception("此施工报量节点不存在无法完成修改!");
                }
                task.WorkContent = consTask.WorkContent;
                task.Note = consTask.Note;
                task.CompleteQuantity = consTask.CompleteQuantity;
                task.AccountingQuantity = new decimal?(consTask.CompleteQuantity);
                entities.SaveChanges();
            }
        }

        public decimal AccountingQuantity { get; set; }

        public cn.justwin.Domain.BudTask BudTask
        {
            get
            {
                return cn.justwin.Domain.BudTask.GetById(this.taskId);
            }
        }

        public decimal CompleteQuantity { get; set; }

        public string Id { get; set; }

        public string Note { get; set; }

        public string reportId { get; set; }

        public IList<ConstructResource> ResourceList
        {
            get
            {
                if (this.resourceList == null)
                {
                    this.resourceList = ConstructResource.GetListByConsTaskId(this.Id);
                }
                return this.resourceList;
            }
            set
            {
                this.resourceList = value;
            }
        }

        private decimal SurplusQuantity { get; set; }

        public string taskId { get; set; }

        public string WorkContent { get; set; }
    }
}

