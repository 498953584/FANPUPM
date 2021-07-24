using cn.justwin.contractBLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace cn.justwin.Domain
{
    public class BudTask
    {
        // Fields
        private static string priceTypeId = ConfigurationManager.AppSettings["BudgetPriceType"];
        private IList<TaskResource> resources;
        private string typeName;

        // Methods
        private BudTask()
        {
        }

        public static void Add(BudTask task, bool isLock)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_Task task2 = new Bud_Task
                {
                    TaskId = task.Id,
                    TaskCode = task.Code,
                    TaskName = task.Name,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    Quantity = task.Quantity,
                    Unit = task.Unit,
                    UnitPrice = task.UnitPrice,
                    Total = task.Total,
                    Note = task.Note,
                    InputDate = task.InputDate,
                    InputUser = task.InputUser,
                    IsValid = new bool?(task.IsValid),
                    PrjId = task.Prj,
                    ParentId = (task.ParentId == "") ? null : task.ParentId,
                    Version = new int?(task.Version),
                    OrderNumber = task.OrderNumber,
                    Total2 = new decimal?(task.Total2)
                };
                if (isLock)
                {
                    task2.Modified = "1";
                }
                entities.AddToBud_Task(task2);
                entities.SaveChanges();
            }
        }

        public static void AddResource(BudTask budTask)
        {
            if ((budTask.Resources != null) && (budTask.Resources.Count > 0))
            {
                IList<TaskResource> resources = new List<TaskResource>();
                resources = budTask.Resources;
                using (pm2Entities entities = new pm2Entities())
                {
                    DeleteResource(budTask, entities);
                    using (IEnumerator<TaskResource> enumerator = resources.GetEnumerator())
                    {
                        TaskResource taskResource;
                        while (enumerator.MoveNext())
                        {
                            taskResource = enumerator.Current;
                            Bud_TaskResource resource = new Bud_TaskResource
                            {
                                TaskResourceId = Guid.NewGuid().ToString(),
                                Bud_Task = (from t in entities.Bud_Task
                                            where t.TaskId == budTask.Id
                                            select t).FirstOrDefault<Bud_Task>(),
                                Res_Resource = (from r in entities.Res_Resource
                                                where r.ResourceId == taskResource.Resource.Id
                                                select r).FirstOrDefault<Res_Resource>(),
                                ResourceQuantity = new decimal?(taskResource.Quantity),
                                ResourcePrice = new decimal?(taskResource.Price),
                                InputUser = budTask.InputUser,
                                InputDate = budTask.InputDate,
                                PrjGuid = budTask.Prj,
                                Versions = new int?(budTask.Version),
                                LossCoefficient = taskResource.LossCoefficient
                            };
                            entities.AddToBud_TaskResource(resource);
                        }
                    }
                    entities.SaveChanges();
                    return;
                }
            }
            DeleteReourceByTaskId(budTask.Id);
        }

        public static void AddResource(string taskId, TaskResource taskResource)
        {
            if ((taskResource != null) && !string.IsNullOrEmpty(taskId))
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_Task task = (from m in entities.Bud_Task
                                     where m.TaskId == taskId
                                     select m).FirstOrDefault<Bud_Task>();
                    Res_Resource resource = (from m in entities.Res_Resource
                                             where m.ResourceId == taskResource.Resource.Id
                                             select m).FirstOrDefault<Res_Resource>();
                    if ((task != null) && (resource != null))
                    {
                        Bud_TaskResource resource3 = new Bud_TaskResource
                        {
                            Bud_Task = task,
                            TaskResourceId = Guid.NewGuid().ToString()
                        };
                        DateTime? inputDate = taskResource.InputDate;
                        resource3.InputDate = inputDate.HasValue ? inputDate.GetValueOrDefault() : DateTime.Now;
                        resource3.InputUser = taskResource.InputUser;
                        resource3.Res_Resource = resource;
                        resource3.ResourcePrice = new decimal?(taskResource.Price);
                        resource3.ResourceQuantity = new decimal?(taskResource.Quantity);
                        Bud_TaskResource resource2 = resource3;
                        if ((from m in entities.Bud_TaskResource
                             where (m.Res_Resource.ResourceId == taskResource.Resource.Id) && (m.Bud_Task.TaskId == taskId)
                             select m.TaskResourceId).Count<string>() == 0)
                        {
                            entities.AddToBud_TaskResource(resource2);
                            entities.SaveChanges();
                        }
                    }
                }
            }
        }

        public void AddResource(Resource resource, decimal quantity, decimal price, decimal lossCoefficient, string action)
        {
            if (this.Resources == null)
            {
                this.Resources = new List<TaskResource>();
            }
            foreach (TaskResource resource2 in this.Resources)
            {
                if (resource.Id == resource2.Resource.Id)
                {
                    if (action != "add")
                    {
                        resource2.Quantity = quantity;
                        resource2.Price = price;
                        resource2.LossCoefficient = new decimal?(lossCoefficient);
                    }
                    return;
                }
            }
            TaskResource item = new TaskResource
            {
                Resource = resource,
                Quantity = quantity,
                Price = price,
                LossCoefficient = new decimal?(lossCoefficient)
            };
            this.Resources.Add(item);
        }

        public static List<BudTask> ChangeId(List<string> lstIds)
        {
            List<BudTask> list = new List<BudTask>();
            BudModifyTaskService service = new BudModifyTaskService();
            cn.justwin.Domain.Entities.BudTask budTask = new cn.justwin.Domain.Entities.BudTask();
            foreach (string str in lstIds)
            {
                BudTask byId = GetById(str);
                if (byId == null)
                {
                    budTask = service.GetBudTask(str);
                    byId = Create(budTask.TaskId, budTask.ParentId, null, budTask.PrjId, budTask.TaskCode, budTask.TaskName, budTask.Unit, budTask.Quantity.Value, budTask.StartDate, budTask.EndDate, budTask.IsValid.Value, budTask.Note, budTask.InputUser, budTask.InputDate, budTask.UnitPrice, budTask.Total);
                }
                else
                {
                    List<BudModifyTask> inModifyTask = service.GetInModifyTask(str);
                    if (inModifyTask != null)
                    {
                        foreach (BudModifyTask task3 in inModifyTask)
                        {
                            byId.Quantity += task3.Quantity;
                            decimal? total = byId.Total;
                            decimal num3 = task3.Total;
                            byId.Total = total.HasValue ? new decimal?(total.GetValueOrDefault() + num3) : null;
                        }
                        decimal num = 0M;
                        decimal num2 = 0M;
                        if (byId.Total.HasValue)
                        {
                            num2 = 0M;
                        }
                        if (byId.Quantity != 0M)
                        {
                            num = num2 / byId.Quantity;
                        }
                        byId.UnitPrice = new decimal?(num);
                    }
                }
                list.Add(byId);
            }
            foreach (string str2 in lstIds)
            {
                string str3 = Guid.NewGuid().ToString();
                foreach (BudTask task4 in list)
                {
                    if (task4.Id == str2)
                    {
                        task4.Id = str3;
                    }
                    if (task4.ParentId == str2)
                    {
                        task4.ParentId = str3;
                    }
                }
            }
            return list;
        }

        public static bool CheckChilds(string id)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_Task
                     where m.ParentId == id
                     select m).FirstOrDefault<Bud_Task>() != null)
                {
                    return true;
                }
                BudModifyService service = new BudModifyService();
                BudModifyTaskService service2 = new BudModifyTaskService();
                if ((from mt in service2
                     join m in service on mt.ModifyId equals m.ModifyId
                     where ((m.Flowstate == 1) && (mt.ModifyType == 0)) && (mt.ParentId == id)
                     select mt).FirstOrDefault<BudModifyTask>() != null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static bool CheckCode(string code, string prjId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_Task
                     where (m.TaskCode == code) && (m.PrjId == prjId)
                     select m).FirstOrDefault<Bud_Task>() != null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static bool CheckDel(List<string> lstIds, string prjId)
        {
            bool flag = true;
            if (lstIds.Count == 1)
            {
                flag = !CheckChilds(lstIds[0]);
            }
            if (lstIds.Count > 1)
            {
                bool flag2 = false;
                foreach (string str in lstIds)
                {
                    bool flag3 = CheckChilds(str);
                    if (flag3)
                    {
                        flag2 = flag3;
                        break;
                    }
                }
                if (flag2)
                {
                    List<string> orderNumberById = GetOrderNumberById(lstIds);
                    flag = IsStructured(orderNumberById);
                    if (flag)
                    {
                        flag = IsCheckAllLeadNodes(orderNumberById, prjId);
                    }
                }
            }
            return flag;
        }

        public static BudTask Create(string Id, string ParentTaskId, BudTaskType taskType, string Prj, string Code, string Name, string Unit, decimal quantity, DateTime? StartDate, DateTime? EndDate, bool IsValid, string Note, string InputUser, DateTime InputDate, decimal? unitPrice, decimal? total)
        {
            BudTask task = null;
            if (string.IsNullOrEmpty(Id))
            {
                throw new ArgumentNullException("任务节点Id", "任务节点Id不能为空");
            }
            if (string.IsNullOrEmpty(Prj))
            {
                throw new ArgumentNullException("所属项目", "所属项目不能为空");
            }
            string orderNumber = GetOrderNumber(Prj, ParentTaskId);
            task = new BudTask
            {
                Id = Id,
                InputDate = InputDate,
                InputUser = InputUser,
                IsValid = IsValid,
                ParentId = ParentTaskId,
                Name = Name,
                Code = Code,
                StartDate = StartDate,
                EndDate = EndDate,
                Unit = Unit,
                Quantity = quantity,
                Note = Note,
                Prj = Prj,
                OrderNumber = orderNumber,
                UnitPrice = unitPrice,
                Total = total,
                Total2 = 0.0M
            };
            task.Version = 1;
            return task;
        }

        public static void Delete(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_Task entity = (from m in entities.Bud_Task
                                   where m.TaskId == id
                                   select m).FirstOrDefault<Bud_Task>();
                if (entity == null)
                {
                    string cmdText = "\r\n                        --DECLARE @ModifyTaskId nvarchar(200);\r\n                        --SET @ModifyTaskId = '3509b659-d739-48f3-b3fe-79e76edcf50f'\r\n                        SELECT PrjId,OrderNumber,ModifyTaskCode AS TaskCode\r\n                        FROM Bud_ModifyTask modifyTask\r\n                        JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n                        WHERE ModifyTaskId = @ModifyTaskId";
                    SqlParameter parameter = new SqlParameter("@ModifyTaskId", SqlDbType.NVarChar, 200)
                    {
                        Value = id
                    };
                    DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("找不到需要删除的任务节点");
                    }
                    DataRow row = table.Rows[0];
                    ConstructTask.DelByTaskId(id);
                    new BudModifyTaskService().DelByModifyTaskId(id);
                    new BudModifyTaskResService().DelByModifyTaskId(id);
                    string str2 = string.Format("DELETE FROM Bud_Task WHERE PrjId = '{0}' AND OrderNumber = '{1}'", row["PrjId"], row["OrderNumber"]);
                    SqlHelper.ExecuteNonQuery(CommandType.Text, str2, new SqlParameter[0]);
                }
                else
                {
                    ConstructTask.DelByTaskId(id);
                    entities.DeleteObject(entity);
                    entities.SaveChanges();
                    string str3 = string.Format("DELETE FROM Bud_Task WHERE PrjId = '{0}' AND OrderNumber = '{1}'", entity.PrjId, entity.OrderNumber);
                    SqlHelper.ExecuteNonQuery(CommandType.Text, str3, new SqlParameter[0]);
                }
            }
        }

        public static void DeleteByPrjId(string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> ids = (from m in entities.Bud_Task
                                    where m.PrjId == prjId
                                    select m.TaskId).ToList<string>();
                new PayoutTarget().DelByTaskId(ids);
                foreach (string str in ids)
                {
                    if (!str.Contains("Y") && !str.Contains("M"))
                    {
                        DeleteReourceByTaskId(str);
                        Delete(str);
                    }
                }
            }
        }

        public static void DeleteReourceByTaskId(string taskId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                DeleteResource((from m in entities.Bud_TaskResource
                                where m.Bud_Task.TaskId == taskId
                                select m.TaskResourceId).ToList<string>());
            }
        }

        public static void DeleteResource(List<string> taskResIds)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (taskResIds.Count == 1)
                {
                    DeleteResource(taskResIds[0], entities);
                }
                else
                {
                    DeleteResource(taskResIds, entities);
                }
                entities.SaveChanges();
            }
        }

        private static void DeleteResource(BudTask budTask, pm2Entities context)
        {
            budTask = GetById(budTask.Id);
            using (IEnumerator<TaskResource> enumerator = budTask.Resources.GetEnumerator())
            {
                TaskResource taskResource;
                while (enumerator.MoveNext())
                {
                    taskResource = enumerator.Current;
                    Bud_TaskResource entity = (from tr in context.Bud_TaskResource
                                               where (tr.Res_Resource.ResourceId == taskResource.Resource.Id) && (tr.Bud_Task.TaskId == budTask.Id)
                                               select tr).FirstOrDefault<Bud_TaskResource>();
                    if (entity != null)
                    {
                        context.DeleteObject(entity);
                    }
                }
            }
        }

        public static void DeleteResource(List<string> taskResIds, pm2Entities context)
        {
            try
            {
                using (List<string>.Enumerator enumerator = taskResIds.GetEnumerator())
                {
                    string taskResId;
                    while (enumerator.MoveNext())
                    {
                        taskResId = enumerator.Current;
                        Bud_TaskResource entity = context.Bud_TaskResource.FirstOrDefault<Bud_TaskResource>(c => c.TaskResourceId == taskResId);
                        if (entity == null)
                        {
                            throw new Exception("找不到要删除的资源！");
                        }
                        context.DeleteObject(entity);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("找不到要删除的资源！");
            }
        }

        public static void DeleteResource(string taskResId, pm2Entities context)
        {
            Bud_TaskResource entity = context.Bud_TaskResource.FirstOrDefault<Bud_TaskResource>(c => c.TaskResourceId == taskResId);
            if (entity == null)
            {
                throw new Exception("找不到要删除的资源！");
            }
            context.DeleteObject(entity);
        }

        public static IList<BudTask> GetAll(string priceTypeId, string prjId, string isWBSRelevance, string taskType, string year, string month)
        {
            List<BudTask> list = new List<BudTask>();
            StringBuilder builder = new StringBuilder();
            if (isWBSRelevance == "1")
            {
                builder.AppendLine("--DECLARE @prjId nvarchar(50),@taskType NVARCHAR(50)");
                builder.AppendLine("--DECLARE @year char(4)");
                builder.AppendLine("--DECLARE @month char(2)");
                builder.AppendLine("--set @prjId='b3d250d1-5dcc-4b87-981d-eea8e40a3dca';");
                builder.AppendLine("--SET @taskType='M';");
                builder.AppendLine("--SET @year = '2013';");
                builder.AppendLine("--SET @month = '03';");
                builder.AppendLine("select task.TaskId,task.ParentId,task.OrderNumber,task.TaskCode,task.TaskName,task.Unit,ISNULL(task.Quantity,0.000) as Quantity, ");
                builder.AppendLine("task.StartDate,task.EndDate,task.Note,ISNULL(taskRes.Total,0.000) as Total from Bud_Task as task left join");
                if (priceTypeId == "")
                {
                    builder.AppendLine("(select TaskId,sum(ResourceQuantity*ResourcePrice) as Total from Bud_TaskResource where TaskId in (select TaskId from ");
                    builder.AppendLine("Bud_Task where PrjId=@prjId) group by TaskId)");
                }
                else
                {
                    builder.AppendLine("(select TaskId,sum(ResourceQuantity*price.PriceValue) as Total from Bud_TaskResource as taskRes ");
                    builder.AppendLine("left join (select ResourceId,PriceValue from res_price where PriceTypeId=@priceTypeId and ");
                    builder.AppendLine("ResourceId in (select ResourceId from Bud_TaskResource where TaskId in (select TaskId from ");
                    builder.AppendLine("Bud_Task where PrjId=@prjId))) as price ");
                    builder.AppendLine("on taskRes.ResourceId=price.ResourceId where taskRes.TaskId in (select TaskId from Bud_Task where ");
                    builder.AppendLine("PrjId=@prjId) group by TaskId)");
                }
                builder.AppendLine("as taskRes on task.TaskId=taskRes.TaskId where task.PrjId=@prjId AND TaskType = @taskType");
                builder.AppendLine("AND SUBSTRING(task.TaskID, 2, LEN(@year + @month)) = @year + @month");
                builder.AppendLine("order by task.OrderNumber desc");
            }
            else
            {
                builder.Append("\r\n                    --declare @prjId nvarchar(50),@taskType NVARCHAR(50)\r\n                    --DECLARE @year char(4)\r\n                    --DECLARE @month char(2)\r\n                    --set @prjId='b3d250d1-5dcc-4b87-981d-eea8e40a3dca';\r\n                    --SET @taskType='';\r\n                    --SET @year = '';\r\n                    --SET @month = '';\r\n                    SELECT TaskId,ParentId,OrderNumber,TaskCode,TaskName,Unit,StartDate,EndDate,\r\n                    ISNULL(Quantity,0.000) as Quantity,ISNULL(UnitPrice,0.000) as UnitPrice,\r\n                    ISNULL(Total,0.000) as Total,Note FROM Bud_Task\r\n                    WHERE PrjId=@prjId AND TaskType = @taskType\r\n                    AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month\r\n                    order by OrderNumber desc\r\n                ");
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", SqlDbType.NVarChar, 500), new SqlParameter("@priceTypeId", SqlDbType.NVarChar, 500), new SqlParameter("@taskType", SqlDbType.Char, 1), new SqlParameter("@year", SqlDbType.Char, 4), new SqlParameter("@month", SqlDbType.Char, 2) };
            commandParameters[0].Value = prjId;
            commandParameters[1].Value = priceTypeId;
            commandParameters[2].Value = taskType;
            commandParameters[3].Value = year;
            commandParameters[4].Value = month;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    BudTask item = new BudTask
                    {
                        Id = reader["TaskId"].ToString(),
                        ParentId = reader["ParentId"].ToString(),
                        OrderNumber = reader["OrderNumber"].ToString(),
                        Code = reader["TaskCode"].ToString(),
                        Name = reader["TaskName"].ToString(),
                        Unit = reader["Unit"].ToString(),
                        Quantity = decimal.Round(Convert.ToDecimal(reader["Quantity"]), 3)
                    };
                    if ((reader["StartDate"] != null) && !string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        item.StartDate = new DateTime?(Convert.ToDateTime(reader["StartDate"]));
                    }
                    if ((reader["EndDate"] != null) && !string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        item.EndDate = new DateTime?(Convert.ToDateTime(reader["EndDate"]));
                    }
                    if (isWBSRelevance == "1")
                    {
                        if (CheckChilds(item.Id))
                        {
                            decimal num = decimal.Round(0M, 3);
                            foreach (BudTask task2 in list)
                            {
                                if (task2.ParentId == item.Id)
                                {
                                    num += Convert.ToDecimal(task2.Total);
                                }
                            }
                            item.Total = new decimal?(num);
                        }
                        else
                        {
                            item.Total = new decimal?(decimal.Round(Convert.ToDecimal(reader["Total"]), 3));
                        }
                        if (item.Quantity == 0M)
                        {
                            item.UnitPrice = new decimal?(decimal.Round(0M, 3));
                        }
                        else
                        {
                            item.UnitPrice = new decimal?(decimal.Round(Convert.ToDecimal(item.Total) / item.Quantity, 3));
                        }
                    }
                    else
                    {
                        if (reader["UnitPrice"] != null)
                        {
                            item.UnitPrice = new decimal?(decimal.Round(Convert.ToDecimal(reader["UnitPrice"]), 3));
                        }
                        else
                        {
                            item.UnitPrice = new decimal?(decimal.Round(Convert.ToDecimal("0.000"), 3));
                        }
                        if (reader["Total"] != null)
                        {
                            item.Total = new decimal?(decimal.Round(Convert.ToDecimal(reader["Total"]), 3));
                        }
                        else
                        {
                            item.Total = new decimal?(decimal.Round(Convert.ToDecimal("0.000"), 3));
                        }
                    }
                    item.Note = reader["Note"].ToString();
                    list.Add(item);
                }
            }
            list.Reverse();
            return list;
        }

        public static IList<BudTask> GetAllByPrjId(string prjId)
        {
            List<BudTask> list = new List<BudTask>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_Task
                        where m.PrjId == prjId
                        orderby m.OrderNumber
                        select new BudTask { Id = m.TaskId, Code = m.TaskCode, Name = m.TaskName, Unit = m.Unit, OrderNumber = m.OrderNumber }).ToList<BudTask>();
            }
        }

        public static IList<BudTask> GetAllTaskId(string prjId)
        {
            List<BudTask> list = new List<BudTask>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_Task
                        orderby m.OrderNumber
                        where m.PrjId == prjId
                        select new BudTask { Id = m.TaskId, ParentId = m.ParentId }).ToList<BudTask>();
            }
        }

        public static DataTable GetBudGetOutPut(string prjId, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = GetBudGetOutPutCount(prjId);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                    --DECLARE @PrjId nvarchar(200), @Version int,@taskType NVARCHAR(50)\r\n                    --DECLARE @year char(4)\r\n                    --DECLARE @month char(2)\r\n                    --DECLARE @pageNo int\r\n                    --DECLARE @pageSize int;\r\n                    --SET @pageSize = 15;\r\n                    --SET @pageNo = 1;\r\n                    --SET @PrjId = '6c091bba-914a-41ce-b4a5-b4803dae26c3';\r\n                    --SET @Version = 1;\r\n                    --SET @taskType='';\r\n                    --SET @year = '';\r\n                    --SET @month = '';\r\n                    WITH cteTask AS --目标成本清单\r\n                    (\r\n                    SELECT Bud_Task.TaskId,ResourceId,OrderNumber, TaskCode, \r\n                    TaskName,FeatureDescription, Unit, Quantity, StartDate, EndDate,ConstructionPeriod,Note,\r\n                    Modified \r\n                    FROM Bud_Task\r\n                    LEFT JOIN Bud_TaskResource ON Bud_Task.TaskId = Bud_TaskResource.TaskId\r\n                    WHERE PrjId = @PrjId AND TaskType = @taskType\r\n                    ),budModifyInfo AS --目标成本变更\r\n                    (\r\n                    SELECT modifyTask.ModifyTaskId AS TaskId,ResourceId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,\r\n                    FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified \r\n                    FROM Bud_ModifyTask modifyTask\r\n                    JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n                    LEFT JOIN Bud_ModifyTaskRes ON modifyTask.ModifyTaskId = Bud_ModifyTaskRes.ModifyTaskId\r\n                    WHERE PrjId=@PrjId AND ModifyType=0 AND FlowState=1     \r\n                    ),cteType AS --项目类型\r\n                    (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                    FROM XPM_Basic_CodeList AS cl\r\n                    INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                    WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                    ),cteTask2 AS\r\n                    (\r\n                    SELECT * FROM \r\n                    (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,newTask.TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n                    Unit,(newTask.Quantity+ISNULL(inBudModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note, Modified,\r\n                    newTask.Total Total,SubCount  FROM \r\n                    (\r\n                    SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n                    Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n                    dbo.fn_GetTotal(TaskId) AS Total, dbo.fn_GetCount(TaskId) AS SubCount\r\n                    FROM cteTask  \r\n                    UNION\r\n                    SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n                    Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n                    dbo.fn_GetModifyTotal(TaskId) AS Total, dbo.fn_GetCount(TaskId) AS SubCount\r\n                    FROM budModifyInfo \r\n                    )newTask LEFT JOIN \r\n                    (\r\n                    --清单内的变更金额\r\n                    SELECT modifyTask.TaskId,SUM(modifyTask.Quantity) Quantity from Bud_ModifyTask modifyTask\r\n                    JOIN \r\n                    (\r\n                        SELECT Bud_Modify.* FROM Bud_Modify WHERE PrjId=@PrjId\r\n                    ) budModify ON modifyTask.ModifyId=budModify.ModifyId \r\n                    where budModify.FlowState=1 AND modifyType=1  group by modifyTask.TaskId\r\n                    ) inBudModifyTask ON newTask.TaskId=inBudModifyTask.TaskId\r\n                    ) tab \r\n                    ),cteBudTask AS --目标成本\r\n                    (\r\n                    SELECT No,TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,cteType.CodeName TypeName,\r\n                    Unit,Quantity,Total,ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice\r\n                    FROM cteTask2\r\n                    LEFT JOIN cteType ON \r\n                    LEN(cteTask2.OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                    ),cteContactTask AS --合同预算清单\r\n                    (\r\n                    SELECT ct.TaskId, ct.OrderNumber, ct.TaskCode, ct.TaskName, ct.Unit, ct.Quantity, \r\n                    ct.StartDate, ct.EndDate, ct.Note, ct.Total, ct.UnitPrice, ct.ConstructionPeriod,\r\n                    ct.FeatureDescription,ISNULL(ct.MainMaterial,0) MainMaterial,ISNULL(ct.SubMaterial,0) AS   SubMaterial,ISNULL(ct.Labor,0) Labor,\r\n                    dbo.fn_GetContractTaskCount(TaskId) AS SubCount\r\n                    FROM Bud_ContractTask ct\r\n                    WHERE PrjId = @PrjId AND TaskType = @taskType\r\n                    AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month\r\n                    ),outBudConModifyInfo AS --合同预算变更\r\n                    (\r\n                    SELECT ModifyTaskId AS TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit,\r\n                    Quantity,StartDate,EndDate,conModifyTask.Note,Total,ISNULL(UnitPrice,0)      UnitPrice,ConstructionPeriod,FeatureDescription,ISNULL(MainMaterial,0) MainMaterial,ISNULL(SubMaterial,0)   SubMaterial,ISNULL(Labor,0) Labor,\r\n                    dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount\r\n                    FROM Bud_ConModifyTask conModifyTask\r\n                    JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                    WHERE PrjId=@PrjId AND ModifyType=0\r\n                    AND FlowState=1 \r\n                    ),cteConTask AS --合同预算\r\n                    (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,* FROM \r\n                    (\r\n                    SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,Unit,\r\n                    (newTask.Quantity+ISNULL(inBudConModifyTask.Quantity,0)) Quantity,\r\n                    StartDate,EndDate,ConstructionPeriod,Note,UnitPrice,ISNULL(FeatureDescription,'') FeatureDescription,\r\n                    (newTask.MainMaterial+ISNULL(inBudConModifyTask.MainMaterial,0)) MainMaterial,\r\n                    (newTask.SubMaterial+ISNULL(inBudConModifyTask.SubMaterial,0)) SubMaterial,\r\n                    (newTask.Labor+ISNULL(inBudConModifyTask.Labor,0)) Labor,\r\n                    (newTask.Total+ISNULL(inBudConModifyTask.Total,0)) Total,SubCount  FROM \r\n                    (\r\n                    SELECT * FROM cteContactTask\r\n                    UNION\r\n                    SELECT * FROM outBudConModifyInfo\r\n                    )newTask LEFT JOIN \r\n                    (\r\n                    --清单内的变更金额\r\n                    SELECT conModifyTask.TaskId,SUM(conModifyTask.Quantity) Quantity,SUM(conModifyTask.Total) Total,\r\n                    SUM(conModifyTask.MainMaterial) MainMaterial,SUM(conModifyTask.SubMaterial) SubMaterial,\r\n                    SUM(conModifyTask.Labor) Labor from Bud_ConModifyTask conModifyTask\r\n                    JOIN \r\n                    (\r\n                        SELECT Bud_ConModify.* FROM Bud_ConModify WHERE PrjId=@PrjId\r\n                    ) budConModify ON conModifyTask.ModifyId=budConModify.ModifyId \r\n                    where budConModify.FlowState=1 AND modifyType=1 \r\n                    group by conModifyTask.TaskId\r\n                    ) inBudConModifyTask ON newTask.TaskId=inBudConModifyTask.TaskId\r\n                    ) tab \r\n                    ),cteContractTask AS\r\n                    (\r\n                    SELECT OrderNumber,UnitPrice,Total FROM cteConTask\r\n                    ),cteBudConTask AS\r\n(\r\n                    SELECT cteBudTask.No,TaskId,cteBudTask.OrderNumber,TaskCode,TaskName,FeatureDescription,TypeName,\r\n                    Unit,Quantity,cteBudTask.Total BudTotal,cteBudTask.UnitPrice BudUnitPrice,cteContractTask.UnitPrice ConUnitPrice,\r\n                    cteContractTask.Total ConTotal,(cteContractTask.Total - cteBudTask.Total) Profit,");
            builder.Append("\r\n                    'ProfitRate' = CASE\r\n                    WHEN cteContractTask.Total = 0 THEN 0 ELSE (cteContractTask.Total - cteBudTask.Total)/cteContractTask.Total END\r\n                    FROM cteBudTask JOIN cteContractTask ON cteBudTask.OrderNumber = cteContractTask.OrderNumber \r\n                   ) \r\nSELECT TOP(@pageSize) No,TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,TypeName,Unit,Quantity,\r\nConUnitPrice,ConTotal,BudTotal,BudUnitPrice,Profit,ProfitRate\r\nFROM\r\n(\r\n\tSELECT cteBudConTask.No,TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,TypeName,Unit,Quantity,BudTotal,\r\n\tBudUnitPrice,ConUnitPrice,ConTotal,Profit,SUBSTRING(CONVERT(VARCHAR(20),(ProfitRate*100)),1,4)+'%' AS ProfitRate\r\n\tFROM cteBudConTask \r\n)tab WHERE No > (@pageNo - 1 ) * @pageSize ORDER BY OrderNumber");
            List<SqlParameter> list = new List<SqlParameter> {
            new SqlParameter("@PrjId", prjId),
            new SqlParameter("@Version", 1),
            new SqlParameter("@taskType", ""),
            new SqlParameter("@year", ""),
            new SqlParameter("@month", ""),
            new SqlParameter("@pageSize", pageSize),
            new SqlParameter("@pageNo", pageIndex)
        };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public static int GetBudGetOutPutCount(string prjId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                    --DECLARE @PrjId nvarchar(200), @Version int,@taskType NVARCHAR(50)\r\n                    --DECLARE @year char(4)\r\n                    --DECLARE @month char(2);\r\n                    --SET @PrjId = '6c091bba-914a-41ce-b4a5-b4803dae26c3';\r\n                    --SET @Version = 1;\r\n                    --SET @taskType='';\r\n                    --SET @year = '';\r\n                    --SET @month = '';\r\n                    WITH cteTask AS --目标成本清单\r\n                    (\r\n                    SELECT Bud_Task.TaskId,ResourceId,OrderNumber, TaskCode, \r\n                    TaskName,FeatureDescription, Unit, Quantity, StartDate, EndDate,ConstructionPeriod,Note,\r\n                    Modified \r\n                    FROM Bud_Task\r\n                    LEFT JOIN Bud_TaskResource ON Bud_Task.TaskId = Bud_TaskResource.TaskId\r\n                    WHERE PrjId = @PrjId AND TaskType = @taskType\r\n                    ),budModifyInfo AS --目标成本变更\r\n                    (\r\n                    SELECT modifyTask.ModifyTaskId AS TaskId,ResourceId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,\r\n                    FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified \r\n                    FROM Bud_ModifyTask modifyTask\r\n                    JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n                    LEFT JOIN Bud_ModifyTaskRes ON modifyTask.ModifyTaskId = Bud_ModifyTaskRes.ModifyTaskId\r\n                    WHERE PrjId=@PrjId AND ModifyType=0 AND FlowState=1     \r\n                    ),cteType AS --项目类型\r\n                    (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                    FROM XPM_Basic_CodeList AS cl\r\n                    INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                    WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                    ),cteTask2 AS\r\n                    (\r\n                    SELECT * FROM \r\n                    (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,newTask.TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n                    Unit,(newTask.Quantity+ISNULL(inBudModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note, Modified,\r\n                    newTask.Total Total,SubCount  FROM \r\n                    (\r\n                    SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n                    Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n                    dbo.fn_GetTotal(TaskId) AS Total, dbo.fn_GetCount(TaskId) AS SubCount\r\n                    FROM cteTask  \r\n                    UNION\r\n                    SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n                    Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n                    dbo.fn_GetModifyTotal(TaskId) AS Total, dbo.fn_GetCount(TaskId) AS SubCount\r\n                    FROM budModifyInfo \r\n                    )newTask LEFT JOIN \r\n                    (\r\n                    --清单内的变更金额\r\n                    SELECT modifyTask.TaskId,SUM(modifyTask.Quantity) Quantity from Bud_ModifyTask modifyTask\r\n                    JOIN \r\n                    (\r\n                        SELECT Bud_Modify.* FROM Bud_Modify WHERE PrjId=@PrjId\r\n                    ) budModify ON modifyTask.ModifyId=budModify.ModifyId \r\n                    where budModify.FlowState=1 AND modifyType=1  group by modifyTask.TaskId\r\n                    ) inBudModifyTask ON newTask.TaskId=inBudModifyTask.TaskId\r\n                    ) tab \r\n                    ),cteBudTask AS --目标成本\r\n                    (\r\n                    SELECT No,TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,cteType.CodeName TypeName,\r\n                    Unit,Quantity,Total,ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice\r\n                    FROM cteTask2\r\n                    LEFT JOIN cteType ON \r\n                    LEN(cteTask2.OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                    ),cteContactTask AS --合同预算清单\r\n                    (\r\n                    SELECT ct.TaskId, ct.OrderNumber, ct.TaskCode, ct.TaskName, ct.Unit, ct.Quantity, \r\n                    ct.StartDate, ct.EndDate, ct.Note, ct.Total, ct.UnitPrice, ct.ConstructionPeriod,\r\n                    ct.FeatureDescription,ISNULL(ct.MainMaterial,0) MainMaterial,ISNULL(ct.SubMaterial,0) AS   SubMaterial,ISNULL(ct.Labor,0) Labor,\r\n                    dbo.fn_GetContractTaskCount(TaskId) AS SubCount\r\n                    FROM Bud_ContractTask ct\r\n                    WHERE PrjId = @PrjId AND TaskType = @taskType\r\n                    AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month\r\n                    ),outBudConModifyInfo AS --合同预算变更\r\n                    (\r\n                    SELECT ModifyTaskId AS TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit,\r\n                    Quantity,StartDate,EndDate,conModifyTask.Note,Total,ISNULL(UnitPrice,0)      UnitPrice,ConstructionPeriod,FeatureDescription,ISNULL(MainMaterial,0) MainMaterial,ISNULL(SubMaterial,0)   SubMaterial,ISNULL(Labor,0) Labor,\r\n                    dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount\r\n                    FROM Bud_ConModifyTask conModifyTask\r\n                    JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                    WHERE PrjId=@PrjId AND ModifyType=0\r\n                    AND FlowState=1 \r\n                    ),cteConTask AS --合同预算\r\n                    (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,* FROM \r\n                    (\r\n                    SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,Unit,\r\n                    (newTask.Quantity+ISNULL(inBudConModifyTask.Quantity,0)) Quantity,\r\n                    StartDate,EndDate,ConstructionPeriod,Note,UnitPrice,ISNULL(FeatureDescription,'') FeatureDescription,\r\n                    (newTask.MainMaterial+ISNULL(inBudConModifyTask.MainMaterial,0)) MainMaterial,\r\n                    (newTask.SubMaterial+ISNULL(inBudConModifyTask.SubMaterial,0)) SubMaterial,\r\n                    (newTask.Labor+ISNULL(inBudConModifyTask.Labor,0)) Labor,\r\n                    (newTask.Total+ISNULL(inBudConModifyTask.Total,0)) Total,SubCount  FROM \r\n                    (\r\n                    SELECT * FROM cteContactTask\r\n                    UNION\r\n                    SELECT * FROM outBudConModifyInfo\r\n                    )newTask LEFT JOIN \r\n                    (\r\n                    --清单内的变更金额\r\n                    SELECT conModifyTask.TaskId,SUM(conModifyTask.Quantity) Quantity,SUM(conModifyTask.Total) Total,\r\n                    SUM(conModifyTask.MainMaterial) MainMaterial,SUM(conModifyTask.SubMaterial) SubMaterial,\r\n                    SUM(conModifyTask.Labor) Labor from Bud_ConModifyTask conModifyTask\r\n                    JOIN \r\n                    (\r\n                        SELECT Bud_ConModify.* FROM Bud_ConModify WHERE PrjId=@PrjId\r\n                    ) budConModify ON conModifyTask.ModifyId=budConModify.ModifyId \r\n                    where budConModify.FlowState=1 AND modifyType=1 \r\n                    group by conModifyTask.TaskId\r\n                    ) inBudConModifyTask ON newTask.TaskId=inBudConModifyTask.TaskId\r\n                    ) tab \r\n                    ),cteContractTask AS\r\n                    (\r\n                    SELECT OrderNumber,UnitPrice,Total FROM cteConTask\r\n                    ),cteBudConTask AS\r\n                    (\r\n                    SELECT cteBudTask.No,TaskId,cteBudTask.OrderNumber,TaskCode,TaskName,FeatureDescription,TypeName,\r\n                    Unit,Quantity,cteBudTask.Total BudTotal,cteBudTask.UnitPrice BudUnitPrice,cteContractTask.UnitPrice ConUnitPrice,\r\n                    cteContractTask.Total ConTotal,(cteContractTask.Total - cteBudTask.Total) Profit,");
            builder.Append("\r\n                    'ProfitRate' = CASE\r\n                    WHEN cteContractTask.Total = 0 THEN 0 ELSE (cteContractTask.Total - cteBudTask.Total)/cteContractTask.Total END\r\n                    FROM cteBudTask JOIN cteContractTask ON cteBudTask.OrderNumber = cteContractTask.OrderNumber \r\n                    )\r\n                    SELECT cteBudConTask.No,TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,TypeName,Unit,Quantity,BudTotal,\r\n                    BudUnitPrice,ConUnitPrice,ConTotal,Profit,SUBSTRING(CONVERT(VARCHAR(20),(ProfitRate*100)),1,4)+'%' AS ProfitRate\r\n                    FROM cteBudConTask ");
            List<SqlParameter> list = new List<SqlParameter> {
            new SqlParameter("@PrjId", prjId),
            new SqlParameter("@Version", 1),
            new SqlParameter("@taskType", ""),
            new SqlParameter("@year", ""),
            new SqlParameter("@month", "")
        };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray()).Rows.Count;
        }

        public static BudTask GetById(string id)
        {
            BudTask budtask = new BudTask();
            using (pm2Entities context = new pm2Entities())
            {
                budtask = (
                    from m in context.Bud_Task
                    where m.TaskId == id
                    select new BudTask
                    {
                        Id = m.TaskId,
                        Code = m.TaskCode,
                        Name = m.TaskName,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        Unit = m.Unit,
                        UnitPrice = m.UnitPrice,
                        Quantity = m.Quantity,
                        Total = m.Total,
                        Note = m.Note,
                        OrderNumber = m.OrderNumber,
                        ParentId = m.ParentId,
                        Prj = m.PrjId,
                        InputUser = m.InputUser,
                        InputDate = m.InputDate,
                        Version = 1,
                        IsValid = m.IsValid ?? true,
                        Mondified = m.Modified
                    }).FirstOrDefault<BudTask>();
            }
            return budtask;
        }

        public static DataTable GetChangeTaskNo(string prjId, string compareId, string selectedId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n            SELECT * FROM (\r\n                SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber ASC) AS No,TaskId FROM Bud_Task \r\n                WHERE PrjId =@prjId AND Version=1\r\n            ) AS T1 \r\n            WHERE TaskId =@comId OR TaskId=@selId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@comId", compareId), new SqlParameter("@selId", selectedId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static string GetChildNode(string prjId)
        {
            string str = "";
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (string str2 in (from m in entities.Bud_Task
                                         where m.PrjId == prjId
                                         select m.TaskId).ToList<string>())
                {
                    if (!CheckChilds(str2))
                    {
                        str = str + "'" + str2 + "',";
                    }
                }
            }
            if (str != "")
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }

        public static DataTable GetChildrenTaskInfo(string taskId, string isWBSRelevance)
        {
            string cmdText = string.Empty;
            if (isWBSRelevance == "1")
            {
                cmdText = "\r\n                DECLARE @PrjId nvarchar(200), @Version int \r\n                SELECT @PrjId = PrjId FROM Bud_Task WHERE TaskId = @TaskId;\r\n                SELECT @Version = Version FROM Bud_Task WHERE TaskId = @TaskId;\r\n                WITH cteTask AS\r\n                (\r\n\t                SELECT TaskId,ParentId, OrderNumber, TaskCode, \r\n\t                TaskName,FeatureDescription, Unit, Quantity, StartDate,EndDate,ConstructionPeriod, Note,\r\n\t                Modified \r\n\t                FROM Bud_Task\r\n\t                WHERE PrjId = @PrjId \r\n\t                AND VERSION = @Version\r\n                ), cteType AS\r\n                (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                    FROM XPM_Basic_CodeList AS cl\r\n                    INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                    WHERE ct.SignCode = 'taskType' AND ct.IsValid = '1' AND cl.IsValid = '1'\r\n                ),budModifyInfo AS\r\n                (\r\n\t                --针对原预算中的变更任务节点\r\n \t                SELECT ModifyTaskId,TaskId,ParentId,OrderNumber,ModifyTaskCode TaskCode,\r\n\t                ModifyTaskContent TaskName,FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified \r\n\t                FROM Bud_ModifyTask modifyTask\r\n\t                JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                where budModify.PrjId=@PrjId \r\n\t                and ModifyType=0 AND FlowState=1\r\n                ),budModifyInfo2 AS\r\n                (\r\n\t                --针对变更中的变更任务节点\r\n\t                SELECT ModifyTaskId,TaskId,ParentId,OrderNumber,ModifyTaskCode TaskCode,\r\n\t                ModifyTaskContent TaskName,FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified \r\n\t                FROM Bud_ModifyTask modifyTask\r\n\t                JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                JOIN\r\n\t                ( \r\n\t\t                SELECT prjId from Bud_ModifyTask \r\n\t\t                JOIN Bud_Modify ON Bud_ModifyTask.ModifyId=Bud_Modify.ModifyId\r\n\t\t                WHERE ModifyTaskId=@TaskId \r\n\t                ) prj\r\n\t                on budModify.PrjId=prj.prjId \r\n\t                where ModifyType=0 AND FlowState=1\r\n                )\r\n                ,cteTask2 AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,newTask.TaskId,ParentId,OrderNumber,TaskCode,\r\n\t                TaskName,FeatureDescription,Unit,(newTask.Quantity+ISNULL(inBudModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n\t                newTask.Total,SubCount FROM\r\n\t                (\r\n                        SELECT TaskId,ParentId,OrderNumber,TaskCode,TaskName,FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n\t\t                dbo.fn_GetTotal(TaskId) Total,dbo.fn_GetCount(TaskId) AS SubCount FROM cteTask WHERE ParentId = @TaskId \r\n                        UNION\r\n                        SELECT TaskId,ParentId,OrderNumber,TaskCode,TaskName,FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n\t\t                dbo.fn_GetModifyTotal(ModifyTaskId) Total,dbo.fn_GetCount(ModifyTaskId) AS SubCount FROM budModifyInfo WHERE ParentId = @TaskId \r\n                        UNION\r\n                        SELECT TaskId,ParentId,OrderNumber,TaskCode,TaskName,FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n\t\t                dbo.fn_GetModifyTotal(ModifyTaskId) Total,dbo.fn_GetCount(ModifyTaskId) AS SubCount FROM budModifyInfo2\tWHERE ParentId = @TaskId \t\t\r\n                    )newTask LEFT JOIN \r\n\t                (\r\n\t\t                --清单内的变更金额\r\n\t\t                SELECT modifyTask.TaskId,SUM(modifyTask.Quantity) Quantity from Bud_ModifyTask modifyTask\r\n\t\t                JOIN \r\n\t\t                (\r\n\t\t\t                SELECT Bud_Modify.* FROM Bud_Modify WHERE PrjId=@PrjId\r\n\t\t                ) budModify ON modifyTask.ModifyId=budModify.ModifyId \r\n\t\t                where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId\r\n\t                ) inBudModifyTask ON newTask.TaskId=inBudModifyTask.TaskId\r\n                )\r\n                SELECT  * FROM\r\n                ( \r\n                    SELECT ROW_NUMBER() OVER(ORDER BY cteTask2.OrderNumber) AS No, cteTask2.TaskId, cteTask2.ParentId,\r\n                        cteTask2.OrderNumber, cteTask2.TaskCode,cteTask2.TaskName, cteTask2.FeatureDescription,cteTask2.Unit, \r\n                        cteTask2.Quantity, cteTask2.StartDate, cteTask2.EndDate,ConstructionPeriod,cteTask2.Note,cteTask2.Modified,\r\n                        ISNULL(cteTask2.Total, 0) AS Total, cteTask2.SubCount, \r\n                        ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice,\r\n                        cteType.CodeNo,ISNULL(cteType.CodeName,'') AS TypeName\r\n                    FROM cteTask2\r\n                    LEFT JOIN cteType ON LEN(cteTask2.OrderNumber) / 3 = cteType.CodeNo\r\n                ) AS a\r\n                ORDER BY No\r\n            ";
            }
            else
            {
                cmdText = "\r\n                    DECLARE @PrjId nvarchar(200), @Version int;\r\n                    --DECLARE @TaskId nvarchar(200);\r\n                    --set @TaskId='a4dfc4f0-6a04-4cab-b75e-4fe08ebc78d0';\r\n                    SELECT @PrjId = PrjId FROM Bud_Task WHERE TaskId = @TaskId;\r\n                    SELECT @Version = Version FROM Bud_Task WHERE TaskId = @TaskId;\r\n                    WITH cteBudTask AS\r\n                    (\r\n                        SELECT TaskId,ParentId,OrderNumber,TaskCode,TaskName,FeatureDescription,Unit,\r\n                        ISNULL(Quantity,0) Quantity,StartDate, EndDate,ConstructionPeriod,Note,Modified,ISNULL(UnitPrice,0) UnitPrice,\r\n                        ISNULL(Total,0) Total,dbo.fn_GetCount(TaskId) AS SubCount FROM Bud_Task \r\n                        WHERE PrjId=@PrjId AND Version=@Version \r\n                    ),budModifyInfo AS\r\n                    (\r\n\t                    --针对原预算中的变更任务节点\r\n \t                    SELECT TaskId,ParentId,OrderNumber,ModifyTaskCode TaskCode,\r\n\t                    ModifyTaskContent TaskName,FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified,\r\n\t                    ISNULL(UnitPrice,0) UnitPrice,Total,dbo.fn_GetCount(ModifyTaskId) AS SubCount\r\n\t                    FROM Bud_ModifyTask modifyTask\r\n\t                    JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                    where budModify.PrjId=@PrjId \r\n\t                    and ModifyType=0 AND FlowState=1\r\n                    ),budModifyInfo2 AS\r\n                    (\r\n\t                    --针对变更中的变更任务节点\r\n\t                    SELECT TaskId,ParentId,OrderNumber,ModifyTaskCode TaskCode,\r\n\t                    ModifyTaskContent TaskName,FeatureDescription,Unit,Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified,\r\n\t                    ISNULL(UnitPrice,0) UnitPrice,Total,dbo.fn_GetCount(ModifyTaskId) AS SubCount\r\n\t                    FROM Bud_ModifyTask modifyTask\r\n\t                    JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                    JOIN\r\n\t                    ( \r\n\t\t                    SELECT prjId from Bud_ModifyTask \r\n\t\t                    JOIN Bud_Modify ON Bud_ModifyTask.ModifyId=Bud_Modify.ModifyId\r\n\t\t                    WHERE ModifyTaskId=@TaskId \r\n\t                    ) prj\r\n\t                    on budModify.PrjId=prj.prjId \r\n\t                    where ModifyType=0 AND FlowState=1\r\n                    )\r\n                    ,cteTask2 AS\r\n                    (\r\n\t                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,newTask.TaskId,ParentId,OrderNumber,TaskCode,\r\n\t                    TaskName,FeatureDescription,Unit,(newTask.Quantity+ISNULL(inBudModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,UnitPrice,\r\n\t                    (newTask.Total+ISNULL(inBudModifyTask.Total,0)) Total,SubCount FROM\r\n\t                    (\r\n\t\t                    SELECT * FROM cteBudTask WHERE ParentId = @TaskId\r\n\t\t                    UNION\r\n\t\t                    SELECT * FROM budModifyInfo WHERE ParentId = @TaskId\r\n\t\t                    UNION\r\n\t\t                    SELECT * FROM budModifyInfo2 WHERE ParentId = @TaskId\r\n\t                    )newTask LEFT JOIN \r\n\t                    (\r\n\t\t                    --清单内的变更金额\r\n\t\t                    SELECT modifyTask.TaskId,SUM(modifyTask.Quantity) Quantity,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask\r\n\t\t                    JOIN \r\n\t\t                    (\r\n\t\t\t                    SELECT Bud_Modify.* FROM Bud_Modify WHERE PrjId=@PrjId\r\n\t\t                    ) budModify ON modifyTask.ModifyId=budModify.ModifyId \r\n\t\t                    where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId\r\n\t                    ) inBudModifyTask ON newTask.TaskId=inBudModifyTask.TaskId\r\n                    ), cteType AS\r\n                    (\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                        FROM XPM_Basic_CodeList AS cl\r\n                        INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                        WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                    )\r\n                    SELECT No, cteTask2.TaskId,\r\n\t\t                cteTask2.OrderNumber, cteTask2.TaskCode, cteTask2.TaskName, cteTask2.FeatureDescription,cteTask2.Unit, \r\n\t\t                cteTask2.Quantity, cteTask2.StartDate, cteTask2.EndDate,ConstructionPeriod,cteTask2.Note,cteTask2.Modified,\r\n\t\t                ISNULL(cteTask2.Total, 0) AS Total, cteTask2.SubCount, \r\n\t\t                ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice,\r\n                        cteType.CodeNo,ISNULL(cteType.CodeName,'') AS TypeName FROM cteTask2\r\n                    LEFT JOIN cteType ON LEN(cteTask2.OrderNumber) / 3 \r\n                    = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                    WHERE ParentId = @TaskId \r\n                    ORDER BY No\r\n             ";
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TaskId", taskId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public IList<string> GetConsTaskIdList()
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from ct in entities.Bud_ConsTask
                        join cr in entities.Bud_ConsReport on ct.Bud_ConsReport equals cr
                        where (ct.TaskId == this.Id) & (cr.FlowState == 1)
                        select ct.ConsTaskId).ToList<string>();
            }
        }

        public static int GetCount(string taskId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return entities.Bud_TaskResource.Count<Bud_TaskResource>(c => (c.Bud_Task.TaskId == taskId));
            }
        }

        public static IList<BudTask> GetFirstLayerList(string projectId)
        {
            IList<BudTask> list = new List<BudTask>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Bud_Task task in from bt in entities.Bud_Task
                                          where (bt.PrjId == projectId) && (bt.OrderNumber.Length == 3)
                                          orderby bt.OrderNumber
                                          select bt)
                {
                    BudTask item = new BudTask
                    {
                        Id = task.TaskId,
                        Code = task.TaskCode,
                        Name = task.TaskName,
                        OrderNumber = task.OrderNumber
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static IList<BudTask> GetListByParentId(string parentId)
        {
            IList<BudTask> list = new List<BudTask>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Bud_Task task in from bt in entities.Bud_Task
                                          where bt.ParentId == parentId
                                          orderby bt.OrderNumber
                                          select bt)
                {
                    BudTask item = new BudTask
                    {
                        Id = task.TaskId,
                        Code = task.TaskCode,
                        Name = task.TaskName,
                        OrderNumber = task.OrderNumber
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static int GetMaxLayer()
        {
            int num = 0;
            using (pm2Entities entities = new pm2Entities())
            {
                string str = (from t in entities.Bud_Task
                              orderby t.OrderNumber.Length descending
                              select t.OrderNumber).FirstOrDefault<string>();
                if (str.Length != 0)
                {
                    num = str.Length / 3;
                }
            }
            return num;
        }

        public static string GetOrderNumber(string prId, string parentId)
        {
            string str = "001";
            using (pm2Entities entities = new pm2Entities())
            {
                if (string.IsNullOrEmpty(parentId))
                {
                    int num;
                    int.TryParse((from m in entities.Bud_Task
                                  where (m.PrjId == prId) && (m.ParentId == null)
                                  select m.OrderNumber).Max<string>(), out num);
                    string str2 = (num + 1).ToString();
                    if (str2.Length == 1)
                    {
                        str2 = "00" + str2;
                    }
                    if (str2.Length == 2)
                    {
                        str2 = "0" + str2;
                    }
                    return str2;
                }
                IQueryable<string> source = from m in entities.Bud_Task
                                            where m.ParentId == parentId
                                            select m.OrderNumber;
                if (source.Count<string>() > 0)
                {
                    string str3 = source.Max<string>();
                    string str4 = str3.Substring(0, str3.Length - 3);
                    string str6 = (Convert.ToInt32(str3.Substring(str3.Length - 3, 3)) + 1).ToString();
                    if (str6.Length == 1)
                    {
                        str6 = "00" + str6;
                    }
                    if (str6.Length == 2)
                    {
                        str6 = "0" + str6;
                    }
                    return (str4 + str6);
                }
                IQueryable<string> queryable3 = from m in entities.Bud_Task
                                                where m.TaskId == parentId
                                                select m.OrderNumber;
                if (queryable3.Count<string>() > 0)
                {
                    str = queryable3.First<string>() + "001";
                }
            }
            return str;
        }

        public static List<string> GetOrderNumberById(List<string> lst)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<string>.Enumerator enumerator = lst.GetEnumerator())
                {
                    string id;
                    while (enumerator.MoveNext())
                    {
                        id = enumerator.Current;
                        string orderNumber = (from m in entities.Bud_Task
                                              where m.TaskId == id
                                              select m.OrderNumber).FirstOrDefault<string>();
                        if (string.IsNullOrEmpty(orderNumber))
                        {
                            BudModifyTask byId = new BudModifyTaskService().GetById(id);
                            if (byId == null)
                            {
                                throw new Exception("要保存为模板的任务节点已不存在，请重新选择!");
                            }
                            orderNumber = byId.OrderNumber;
                        }
                        list.Add(orderNumber);
                    }
                }
            }
            return list;
        }

        public static void GetQuantityAndTotal(BudTask budTask, pm2Entities context, IQueryable<Bud_Task> bud_TaskList, string priceTypeId, string prjId)
        {
            Func<ResPrice, bool> predicate = null;
            if (!string.IsNullOrEmpty(budTask.OrderNumber))
            {
                decimal d = 0M;
                IQueryable<string> queryable = from t in bud_TaskList
                                               where t.OrderNumber.StartsWith(budTask.OrderNumber) && (t.PrjId == prjId)
                                               select t.TaskId;
                if (queryable != null)
                {
                    using (IEnumerator<string> enumerator = queryable.GetEnumerator())
                    {
                        string id;
                        while (enumerator.MoveNext())
                        {
                            id = enumerator.Current;
                            IQueryable<Bud_TaskResource> queryable2 = from tr in context.Bud_TaskResource
                                                                      where tr.Bud_Task.TaskId == id
                                                                      select tr;
                            if (queryable2 != null)
                            {
                                using (IEnumerator<Bud_TaskResource> enumerator2 = queryable2.GetEnumerator())
                                {
                                    Func<ResPrice, bool> func = null;
                                    Bud_TaskResource tRes;
                                    while (enumerator2.MoveNext())
                                    {
                                        tRes = enumerator2.Current;
                                        if (string.IsNullOrEmpty(priceTypeId))
                                        {
                                            decimal? resourcePrice = tRes.ResourcePrice;
                                            decimal? resourceQuantity = tRes.ResourceQuantity;
                                            d += (resourcePrice.HasValue ? resourcePrice.GetValueOrDefault() : 0M) * (resourceQuantity.HasValue ? resourceQuantity.GetValueOrDefault() : 0M);
                                        }
                                        else
                                        {
                                            if (func == null)
                                            {
                                                func = m => m.ResourceId == tRes.Res_Resource.ResourceId;
                                            }
                                            if (predicate == null)
                                            {
                                                predicate = m => m.PriceTypeId == priceTypeId;
                                            }
                                            ResPrice price = ResPrice.GetAll().Where<ResPrice>(func).Where<ResPrice>(predicate).FirstOrDefault<ResPrice>();
                                            if (price != null)
                                            {
                                                decimal? nullable3 = tRes.ResourceQuantity;
                                                d += price.PriceValue * (nullable3.HasValue ? nullable3.GetValueOrDefault() : 0M);
                                            }
                                        }
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
                if (budTask.Quantity == 0M)
                {
                    budTask.UnitPrice = new decimal?(decimal.Round(0M, 3));
                }
                else
                {
                    budTask.UnitPrice = new decimal?(decimal.Round(d / budTask.Quantity, 3));
                }
                budTask.Total = new decimal?(decimal.Round(d, 3));
            }
        }

        private static void GetQuantityTotal(string taskId, string prjId, string priceType, ref decimal quantity, ref decimal total)
        {
            if (!CheckChilds(taskId))
            {
                GetTaskQuantityTotal(taskId, priceType, ref quantity, ref total);
            }
            else
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    List<string> list = (from m in entities.Bud_Task
                                         where (m.PrjId == prjId) && (m.ParentId == taskId)
                                         orderby m.OrderNumber
                                         select m.TaskId).ToList<string>();
                    if (list.Count > 0)
                    {
                        foreach (string str in list)
                        {
                            GetQuantityTotal(str, prjId, priceType, ref quantity, ref total);
                        }
                    }
                }
            }
        }

        public IList<string> GetReportImgPathList()
        {
            IList<string> list = new List<string>();
            IList<string> consTaskIdList = this.GetConsTaskIdList();
            string str = ConfigurationManager.AppSettings["ConstructReport"];
            foreach (string str2 in consTaskIdList)
            {
                string item = str + "/" + str2;
                list.Add(item);
            }
            return list;
        }

        public static Dictionary<string, decimal> GetResourceQuantity(string prjId)
        {
            Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT ResourceCode,SUM(ResourceQuantity) ResourceQuantity  FROM Bud_TaskResource AS taskRes ");
            builder.AppendLine("JOIN Bud_Task AS task ON taskRes.TaskId=task.TaskId");
            builder.AppendLine("INNER JOIN Res_Resource AS res ON taskRes.ResourceId=res.ResourceId");
            builder.AppendFormat("WHERE PrjId='{0}' GROUP BY ResourceCode", prjId);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                while (reader.Read())
                {
                    string str = reader["ResourceCode"].ToString();
                    decimal num = Convert.ToDecimal(reader["ResourceQuantity"]);
                    if (!string.IsNullOrEmpty(str))
                    {
                        dictionary.Add(str, num);
                    }
                }
            }
            return dictionary;
        }

        public static DataTable GetResourcesByPrjId(string prjId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n\t\t\t--DECLARE @PrjId Nvarchar(50)\r\n\t\t\t--SET @PrjId='29faba45-5a1e-4fda-97bc-7b02cc44f5ab';\r\n\t\t\tWITH cteBudTaskRes AS\r\n\t\t\t(\r\n\t\t\t\t--原预算资源\r\n\t\t\t\tSELECT TaskResourceId as Id,ResourceId,ResourcePrice as price,\r\n\t\t\t\tResourceQuantity as number,ISNULL(LossCoefficient,1.00) LossCoefficient FROM Bud_TaskResource\r\n\t\t\t\tINNER JOIN (\r\n\t\t\t\t\tSELECT PrjId,MAX(Version) Version FROM Bud_TaskChange\r\n\t\t\t\t\tWHERE PrjId=@PrjId\r\n\t\t\t\t\tGROUP BY PrjId\r\n\t\t\t\t) MaxVersion ON Bud_TaskResource.PrjGuid=MaxVersion.PrjId AND Versions=MaxVersion.Version\r\n\t\t\t\tWHERE TaskResourceId NOT IN(\r\n\t\t\t\t\t--不包含拆分的资源\r\n\t\t\t\t\tSELECT TaskResourceId FROM Bud_Task task\r\n\t\t\t\t\tINNER JOIN Bud_TaskResource taskRes ON task.TaskId=taskRes.TaskId\r\n\t\t\t\tWHERE PrjId=@PrjId AND TaskType<>'')\r\n\t\t\t),cteBudModifyRes AS\r\n\t\t\t(\r\n\t\t\t\t--变更资源\r\n\t\t\t\tSELECT ModifyTaskResId AS Id,ResourceId,ResourcePrice as price,\r\n\t\t\t\tResourceQuantity as number,ISNULL(LossCoefficient,1.00) LossCoefficient FROM Bud_Modify budModify\r\n\t\t\t\tINNER JOIN Bud_ModifyTaskRes budModifyRes ON budModify.ModifyId=budModifyRes.ModifyId\r\n\t\t\t\tWHERE PrjId=@PrjId AND FlowState=1\r\n\t\t\t)\r\n\t\t\tSELECT BudRes1.ResourceId,ResourceCode,ResourceName,Res_Unit.[Name] as Unit,Specification,\r\n\t\t\tBrand,ModelNumber,TechnicalParameter,Number,LossCoefficient,\r\n\t\t\tCONVERT(DECIMAL(18,3),ISNULL(Total/NULLIF(Number,0),0)) Price  FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT ResourceId,SUM(Number) Number,SUM(Price*Number) Total,SUM(LossCoefficient) LossCoefficient FROM \r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT * FROM cteBudTaskRes\r\n\t\t\t\t\tUNION \r\n\t\t\t\t\tSELECT * FROM cteBudModifyRes\r\n\t\t\t\t) BudRes GROUP BY ResourceId\r\n\t\t\t) BudRes1\r\n\t\t\tINNER JOIN Res_Resource ON BudRes1.ResourceId=Res_Resource.ResourceId\r\n\t\t\tLEFT JOIN Res_Unit ON Unit=UnitId  ORDER BY ResourceCode\r\n            ");
            SqlParameter parameter = new SqlParameter("@prjId", SqlDbType.NVarChar, 0x40)
            {
                Value = prjId
            };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static DataTable GetResourcesByTask(string taskId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TaskResourceId as Id,Bud_TaskResource.ResourceId,ResourceCode,ResourceName,Res_Unit.[Name] as Unit,Res_Resource.Specification,ResourcePrice as price, ");
            builder.Append("Res_Resource.Brand,ModelNumber,TechnicalParameter, ");
            builder.Append("ResourceQuantity as number,Bud_TaskResource.InputUser,Bud_TaskResource.InputDate,ISNULL(LossCoefficient,1.00) LossCoefficient from ");
            builder.Append("Bud_TaskResource INNER JOIN Res_Resource ON Bud_TaskResource.ResourceId=Res_Resource.ResourceId LEFT JOIN Res_Unit ON Unit=UnitId ");
            builder.Append("where TaskId=@TaskId  ORDER BY ResourceCode ");
            SqlParameter parameter = new SqlParameter("@TaskId", SqlDbType.NVarChar, 0x40)
            {
                Value = taskId
            };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static IList<TaskResource> GetResourcesByTaskId(string taskId)
        {
            List<TaskResource> list = new List<TaskResource>();
            using (pm2Entities entities = new pm2Entities())
            {
                var list2 = (from m in entities.Bud_TaskResource
                             where m.Bud_Task.TaskId == taskId
                             select new { ResourceQuantity = m.ResourceQuantity, ResourcePrice = m.ResourcePrice, ResourceId = m.Res_Resource.ResourceId }).ToList();
                if (list2.Count > 0)
                {
                    foreach (var type in list2)
                    {
                        TaskResource item = new TaskResource();
                        decimal? resourcePrice = type.ResourcePrice;
                        item.Price = resourcePrice.HasValue ? resourcePrice.GetValueOrDefault() : 0M;
                        decimal? resourceQuantity = type.ResourceQuantity;
                        item.Quantity = resourceQuantity.HasValue ? resourceQuantity.GetValueOrDefault() : 0M;
                        item.Resource = Resource.GetById(type.ResourceId);
                        list.Add(item);
                    }
                }
            }
            List<BudModifyTaskRes> inTaskRes = new BudModifyTaskResService().GetInTaskRes(taskId);
            if (list.Count > 0)
            {
                using (List<TaskResource>.Enumerator enumerator2 = list.GetEnumerator())
                {
                    Func<BudModifyTaskRes, bool> predicate = null;
                    TaskResource taskRes;
                    while (enumerator2.MoveNext())
                    {
                        taskRes = enumerator2.Current;
                        if (predicate == null)
                        {
                            predicate = tr => tr.ResourceId == taskRes.Resource.Id;
                        }
                        BudModifyTaskRes res = inTaskRes.Where<BudModifyTaskRes>(predicate).FirstOrDefault<BudModifyTaskRes>();
                        if (res != null)
                        {
                            decimal num = 0M;
                            decimal num2 = 0M;
                            decimal num3 = 0M;
                            num = taskRes.Quantity + res.ResourceQuantity;
                            num3 = (taskRes.Quantity * taskRes.Price) + (res.ResourceQuantity * res.ResourcePrice);
                            if (!num.Equals((decimal)0M))
                            {
                                num2 = num3 / num;
                            }
                            taskRes.Quantity = num;
                            taskRes.Price = num2;
                            inTaskRes.Remove(res);
                        }
                    }
                }
            }
            foreach (BudModifyTaskRes res2 in inTaskRes)
            {
                TaskResource resource2 = new TaskResource
                {
                    Price = res2.ResourcePrice,
                    Quantity = res2.ResourceQuantity,
                    Resource = Resource.GetById(res2.ResourceId)
                };
                list.Add(resource2);
            }
            return list;
        }

        public static string GetResourcesInfoByTaskId(string taskId)
        {
            string str = string.Empty;
            using (pm2Entities entities = new pm2Entities())
            {
                var list = (from tr in entities.Bud_TaskResource
                            where tr.Bud_Task.TaskId == taskId
                            select new { Id = tr.TaskResourceId, ResourceId = tr.Res_Resource.ResourceId, resourceCode = tr.Res_Resource.ResourceCode, resourceName = tr.Res_Resource.ResourceName, unit = tr.Res_Resource.Res_Unit.Name, specification = (tr.Res_Resource.Specification == null) ? "" : tr.Res_Resource.Specification, brand = (tr.Res_Resource.Brand == null) ? "" : tr.Res_Resource.Brand, modelNumber = (tr.Res_Resource.ModelNumber == null) ? "" : tr.Res_Resource.ModelNumber, TechnicalParameter = (tr.Res_Resource.TechnicalParameter == null) ? "" : tr.Res_Resource.TechnicalParameter, price = tr.ResourcePrice, number = tr.ResourceQuantity, lossCoefficient = tr.LossCoefficient }).ToList();
                BudModifyService service = new BudModifyService();
                BudModifyTaskService service2 = new BudModifyTaskService();
                BudModifyTaskResService service3 = new BudModifyTaskResService();
                List<BudModifyTaskRes> first = (from mt in service2
                                                join m in service on mt.ModifyId equals m.ModifyId 
                                                join mtr in service3 on mt.ModifyTaskId equals mtr.ModifyTaskId 
                                                where (mt.TaskId == taskId) && (m.Flowstate == 1)
                                                select mtr).ToList<BudModifyTaskRes>();
                List<BudModifyTaskRes> second = (from mt in service2
                                                 join m in service on mt.ModifyId equals m.ModifyId 
                                                 join mtr in service3 on mt.ModifyTaskId equals mtr.ModifyTaskId 
                                                 where (mt.ModifyTaskId == taskId) && (m.Flowstate == 1)
                                                 select mtr).ToList<BudModifyTaskRes>();
                first = first.Union<BudModifyTaskRes>(second).ToList<BudModifyTaskRes>();
                using (var enumerator = list.GetEnumerator())
                {
                    Func<BudModifyTaskRes, bool> predicate = null;
                    while (enumerator.MoveNext())
                    {
                        decimal? nullable5;
                        decimal num5;
                        decimal? nullable6;
                        var taskResource = enumerator.Current;
                        str = str + taskResource.resourceCode + ",";
                        str = str + taskResource.resourceName + ",";
                        if ((taskResource.unit == "") || (taskResource.unit == null))
                        {
                            str = str + " ,";
                        }
                        else
                        {
                            str = str + taskResource.unit + ",";
                        }
                        str = str + taskResource.specification.Replace(',', '，') + ",";
                        str = str + taskResource.brand.Replace(',', '，') + ",";
                        if (!string.IsNullOrEmpty(taskResource.modelNumber))
                        {
                            str = str + taskResource.modelNumber.Replace(',', '，') + ",";
                        }
                        else
                        {
                            str = str + ",";
                        }
                        str = str + taskResource.TechnicalParameter.Replace(',', '，') + ",";
                        decimal? nullable = 0M;
                        decimal num = 0M;
                        decimal? lossCoefficient = 1M;
                        if (taskResource.lossCoefficient.HasValue)
                        {
                            lossCoefficient = taskResource.lossCoefficient;
                        }
                        if (taskResource.number.HasValue && taskResource.price.HasValue)
                        {
                            num = taskResource.number.Value;
                            num5 = num * taskResource.price.Value;
                            nullable5 = lossCoefficient;
                            nullable = nullable5.HasValue ? new decimal?(num5 * nullable5.GetValueOrDefault()) : ((decimal?)(nullable6 = null));
                        }
                        if (predicate == null)
                        {
                            predicate = listx => listx.ResourceId == taskResource.ResourceId;
                        }
                        foreach (BudModifyTaskRes res in first.Where<BudModifyTaskRes>(predicate).ToList<BudModifyTaskRes>())
                        {
                            if (res != null)
                            {
                                decimal? nullable7;
                                num += res.ResourceQuantity;
                                nullable5 = nullable;
                                num5 = res.ResourcePrice * res.ResourceQuantity;
                                nullable6 = res.LossCoefficient;
                                nullable6 = nullable6.HasValue ? new decimal?(num5 * nullable6.GetValueOrDefault()) : ((decimal?)(nullable7 = null));
                                nullable = (nullable5.HasValue & nullable6.HasValue) ? new decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : ((decimal?)(nullable7 = null));
                                first.Remove(res);
                            }
                        }
                        decimal? price = 0M;
                        if (num != 0M)
                        {
                            price = taskResource.price;
                        }
                        str = str + price.Value.ToString("0.000") + ",";
                        str = str + num + ",";
                        str = str + lossCoefficient + ",";
                        str = str + nullable.Value.ToString("0.000") + "⊙";
                    }
                }
                using (List<string>.Enumerator enumerator3 = service3.GetModifyTasResIds(first).GetEnumerator())
                {
                    Func<BudModifyTaskRes, bool> func2 = null;
                    string resId;
                    while (enumerator3.MoveNext())
                    {
                        resId = enumerator3.Current;
                        Resource byId = Resource.GetById(resId);
                        str = str + byId.Code + ",";
                        str = str + byId.Name + ",";
                        if (byId.ResourceUnit == null)
                        {
                            str = str + " ,";
                        }
                        else if (!string.IsNullOrEmpty(byId.ResourceUnit.Name))
                        {
                            str = str + byId.ResourceUnit.Name + ",";
                        }
                        else
                        {
                            str = str + " ,";
                        }
                        str = str + byId.Specification.Replace(',', '，') + ",";
                        str = str + byId.Brand.Replace(',', '，') + ",";
                        if (!string.IsNullOrEmpty(byId.ModelNumber))
                        {
                            str = str + byId.ModelNumber.Replace(',', '，') + ",";
                        }
                        else
                        {
                            str = str + ",";
                        }
                        str = str + byId.TechnicalParameter.Replace(',', '，') + ",";
                        decimal num2 = 0M;
                        decimal num3 = 0M;
                        decimal? nullable4 = 1M;
                        if (func2 == null)
                        {
                            func2 = listx => listx.ResourceId == resId;
                        }
                        foreach (BudModifyTaskRes res2 in first.Where<BudModifyTaskRes>(func2).ToList<BudModifyTaskRes>())
                        {
                            if (res2 != null)
                            {
                                if (!res2.LossCoefficient.HasValue)
                                {
                                    res2.LossCoefficient = 1.0M;
                                }
                                num3 += res2.ResourceQuantity;
                                num2 += (res2.ResourcePrice * res2.ResourceQuantity) * res2.LossCoefficient.Value;
                                first.Remove(res2);
                            }
                        }
                        decimal num4 = 0M;
                        if (num3 != 0M)
                        {
                            num4 = num2 / num3;
                        }
                        str = str + num4.ToString("0.000") + ",";
                        str = str + num3 + ",";
                        str = str + nullable4 + ",";
                        str = str + num2.ToString("0.000") + "⊙";
                    }
                }
                return str;
            }
        }

        public static DataTable GetSumTotal(IList<BudTask> lstTask, IList<ResType> restypelst, string priceType)
        {
            DateTime.Now.ToLongTimeString();
            DataTable dtrcj = new DataTable();
            DataRow row = dtrcj.NewRow();
            dtrcj.Rows.Add(row);
            foreach (ResType type in restypelst)
            {
                dtrcj.Columns.Add(type.Name, typeof(decimal));
                dtrcj.Rows[0][type.Name] = 0M;
            }
            foreach (BudTask task in lstTask)
            {
                Resource.GetResTypeTotal(task.Id, priceType, dtrcj);
            }
            DateTime.Now.ToLongTimeString();
            return dtrcj;
        }

        public static Hashtable GetSumTotal(string prjId, int version, string priceType, List<CostAccounting> costAccountingList, string isWBSRelevance)
        {
            Hashtable hashtable = new Hashtable();
            DataTable table = new DataTable();
            foreach (CostAccounting accounting in costAccountingList)
            {
                hashtable.Add(accounting.Code, 0M);
            }
            StringBuilder builder = new StringBuilder();
            if (isWBSRelevance == "1")
            {
                builder.AppendLine("select res.ResourceType,Sum(ISNULL(taskRes.Total,0.000)) Total from Res_Resource as res inner join ");
                if (priceType == "")
                {
                    builder.AppendLine("\r\n                        (\r\n\t                        SELECT SUM(ResTotal) AS Total,ResourceId FROM\r\n\t                        ( \r\n\t\t                        SELECT (SUM(Bud_TaskResource.ResourceQuantity)*Bud_TaskResource.ResourcePrice*Bud_TaskResource.LossCoefficient) ResTotal,\r\n\t\t                        Bud_TaskResource.ResourceId\r\n\t\t                        FROM Bud_TaskResource JOIN Bud_Task ON Bud_TaskResource.TaskId=Bud_Task.TaskId\r\n\t\t\t                    WHERE Bud_Task.PrjId=@prjId\r\n\t\t\t                    AND Bud_Task.version=@version and TaskType=''\r\n\t\t                        GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId,Bud_TaskResource.LossCoefficient\r\n\t\t                        UNION ALL\r\n\t\t                        SELECT (SUM(ResourceQuantity)*ResourcePrice*LossCoefficient) ReseTotal,\r\n\t\t                        ResourceId FROM Bud_ModifyTaskRes modifyTaskRes\r\n\t\t                        JOIN Bud_ModifyTask modifyTask ON modifyTaskRes.ModifyTaskId=modifyTask.ModifyTaskId\r\n\t\t                        JOIN Bud_MOdify modify ON modifyTask.modifyId=modify.modifyId\r\n\t\t                        WHERE modify.FlowState='1' AND modify.PrjId=@prjId\r\n\t\t                        GROUP BY ResourcePrice,ResourceId,LossCoefficient\r\n\t                        ) allBudResInfo  GROUP BY ResourceId\r\n                        ) \r\n                        ");
                }
                else
                {
                    builder.AppendLine("(select sum(tr.ResourceQuantity*rp.PriceValue) Total,tr.ResourceId from ");
                    builder.Append("\r\n                        (\r\n\t\t                    SELECT SUM(ResourceQuantity) AS ResourceQuantity,ResourceId FROM\r\n\t\t                    ( \r\n\t\t\t                    SELECT SUM(ResourceQuantity) ResourceQuantity,Bud_TaskResource.ResourceId\r\n\t\t\t                    FROM Bud_TaskResource JOIN Bud_Task ON Bud_TaskResource.TaskId=Bud_Task.TaskId\r\n\t\t\t                    WHERE Bud_Task.PrjId=@prjId\r\n\t\t\t                    AND Bud_Task.version=@version and TaskType=''\r\n\t\t\t                    GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId\r\n\t\t\t                    UNION ALL\r\n\t\t\t                    SELECT SUM(ResourceQuantity) ResourceQuantity,\r\n\t\t\t                    ResourceId FROM Bud_ModifyTaskRes modifyTaskRes\r\n\t\t\t                    JOIN Bud_ModifyTask modifyTask ON modifyTaskRes.ModifyTaskId=modifyTask.ModifyTaskId\r\n\t\t\t                    JOIN Bud_MOdify modify ON modifyTask.modifyId=modify.modifyId\r\n\t\t\t                    WHERE modify.FlowState='1' AND modify.PrjId=@prjId\r\n\t\t\t                    GROUP BY ResourceId\r\n\t\t                    ) allBudResInfo GROUP BY ResourceId\r\n                        )");
                    builder.AppendLine(" as tr left join ");
                    builder.AppendLine("(select ResourceId,PriceValue from res_price where PriceTypeId=@priceTypeId and ResourceId in ");
                    builder.Append("\r\n\t                    (\r\n                            SELECT ResourceId FROM\r\n\t\t\t\t                    ( \r\n\t\t\t\t\t                    SELECT SUM(ResourceQuantity) ResourceQuantity,Bud_TaskResource.ResourceId\r\n\t\t\t\t\t                    FROM Bud_TaskResource JOIN Bud_Task ON Bud_TaskResource.TaskId=Bud_Task.TaskId\r\n\t\t\t                            WHERE Bud_Task.PrjId=@prjId\r\n\t\t\t                            AND Bud_Task.version=@version and TaskType=''\r\n\t\t\t\t\t                    GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId\r\n\t\t\t\t\t                    UNION ALL\r\n\t\t\t\t\t                    SELECT SUM(ResourceQuantity) ResourceQuantity,\r\n\t\t\t\t\t                    ResourceId FROM Bud_ModifyTaskRes modifyTaskRes\r\n\t\t\t\t\t                    JOIN Bud_ModifyTask modifyTask ON modifyTaskRes.ModifyTaskId=modifyTask.ModifyTaskId\r\n\t\t\t\t\t                    JOIN Bud_MOdify modify ON modifyTask.modifyId=modify.modifyId\r\n\t\t\t\t\t                    WHERE modify.FlowState='1' AND modify.PrjId=@prjId\r\n\t\t\t\t\t                    GROUP BY ResourceId\r\n\t\t                    ) allBudResInfo GROUP BY ResourceId\r\n                        )");
                    builder.AppendLine(")as rp on tr.ResourceId=rp.ResourceId group by tr.ResourceId) ");
                }
                builder.AppendLine("as taskRes on res.ResourceId=taskRes.ResourceId group by res.ResourceType ");
            }
            else
            {
                builder.Append("\r\n                    --declare @prjId nvarchar(50),@version int;\r\n                    --set @prjId='be069cf6-b851-4344-8f25-1e90e86864e1';\r\n                    --set @version='1';\r\n                    select res.ResourceType,Sum(ISNULL(taskRes.Total,0.000)) Total \r\n                    from Res_Resource as res \r\n                    inner join (\r\n\t                    select sum(ResourceQuantity*ResourcePrice) Total,ResourceId from Bud_TaskResource \r\n\t                    where PrjGuid=@prjId AND Versions=@version\r\n\t                    group by ResourceId\r\n                    ) as taskRes on res.ResourceId=taskRes.ResourceId group by res.ResourceType\r\n                    ");
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", SqlDbType.NVarChar, 500), new SqlParameter("@version", SqlDbType.Int), new SqlParameter("@priceTypeId", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = prjId;
            commandParameters[1].Value = version;
            commandParameters[2].Value = priceType;
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows)
            {
                ResType byId = ResType.GetById(Resource.GetFirstResourceTypeId(row["ResourceType"].ToString()));
                if (!string.IsNullOrEmpty(byId.CBSCode) && (hashtable[byId.CBSCode] != null))
                {
                    decimal num = Convert.ToDecimal(hashtable[byId.CBSCode]) + Convert.ToDecimal(row["Total"]);
                    hashtable.Remove(byId.CBSCode);
                    hashtable.Add(byId.CBSCode, num);
                }
            }
            return hashtable;
        }

        public static DataTable GetTaskByContract(string contractId)
        {
            string cmdText = string.Format("SELECT TOP(1) TaskId FROM Bud_Task WHERE ContractId = '{0}' ORDER BY OrderNumber", contractId);
            return GetTaskInfoByParent(DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0])));
        }

        public static int GetTaskCount(string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from bt in entities.Bud_Task
                        orderby bt.OrderNumber
                        where bt.PrjId == prjId
                        select bt).Count<Bud_Task>();
            }
        }

        public static DataTable GetTaskInfo(string prjId, int pageSize, int pageIndex)
        {
            DataTable table = new DataTable();
            string cmdText = "\r\n            WITH cteTask AS\r\n            (\r\n\t            SELECT t.TaskId, t.OrderNumber, t.ParentId, \r\n\t\t            SUM(tr.ResourceQuantity * tr.ResourcePrice) AS Total  --本节点资源配置金额\r\n\t            FROM Bud_Task AS t\r\n\t            LEFT JOIN Bud_TaskResource AS tr ON t.TaskId = tr.TaskID\r\n\t            WHERE t.PrjId = @PrjId\r\n\t            GROUP BY t.TaskId, t.OrderNumber, t.ParentId\r\n            ), cteTask2 AS \r\n            (\r\n            SELECT t2.TaskId, t2.OrderNumber, t2.TaskCode,t2.TaskName, t2.Unit, t2.Quantity, t2.StartDate, t2.EndDate,t2.Note,\r\n\t            (SELECT SUM(cteTask.Total) FROM cteTask \r\n\t\t            WHERE cteTask.OrderNumber LIKE t2.OrderNumber + '%'\r\n\t            ) AS Total , --小计（已对子节点进行汇总）\r\n\t            (SELECT COUNT(1) FROM Bud_Task AS t3 \r\n\t\t            WHERE t3.ParentId = t2.TaskId\r\n\t            ) AS SubCount -- 子节点的数量\r\n            FROM Bud_Task AS t2 \r\n            JOIN cteTask ON t2.TaskId = cteTask.TaskId\r\n            )\r\n            SELECT TOP(@PageSize) * FROM\r\n            ( \r\n\t            SELECT ROW_NUMBER() OVER(ORDER BY cteTask2.OrderNumber) AS No, cteTask2.TaskId,\r\n\t\t            cteTask2.OrderNumber, cteTask2.TaskCode,cteTask2.TaskName, cteTask2.Unit, \r\n\t\t            cteTask2.Quantity, cteTask2.StartDate, cteTask2.EndDate, cteTask2.Note,\r\n                    --任务类型\r\n                    TypeName=(SELECT TOP(1) CodeName  FROM XPM_Basic_CodeList AS clist,XPM_Basic_CodeType AS ctype\r\n\t\t\t\t             WHERE clist.TypeId=ctype.TypeId AND SignCode='taskType' AND clist.IsValid='1' AND NoteID NOT IN\r\n\t\t\t\t\t\t            (SELECT TOP(LEN(cteTask2.OrderNumber)/3-1)NoteId FROM XPM_Basic_CodeList AS clist,XPM_Basic_CodeType AS ctype\r\n\t\t\t\t\t\t            WHERE clist.TypeId=ctype.TypeId AND SignCode='taskType' AND clist.IsValid='1')\r\n\t\t\t\t            ),\r\n\t\t            ISNULL(cteTask2.Total, 0) AS Total, cteTask2.SubCount, \r\n\t\t            ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice \r\n\t            FROM cteTask2\r\n            ) AS a\r\n            WHERE No > (@PageIndex - 1) * @PageSize\r\n            ORDER BY OrderNumber";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@PageSize", pageSize), new SqlParameter("@PageIndex", pageIndex) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetTaskInfo(string prjId, string isWBSRelevance, string taskType, string year, string month)
        {
            string cmdText = string.Empty;
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(taskType))
            {
                str2 = " and 1<>1 ";
            }
            if (isWBSRelevance == "1")
            {
                cmdText = "\r\n                --DECLARE @PrjId nvarchar(200),@taskType NVARCHAR(50)\r\n                --DECLARE @year char(4)\r\n                --DECLARE @month char(2)\r\n                --SET @PrjId = 'b3d250d1-5dcc-4b87-981d-eea8e40a3dca';\r\n                --SET @taskType='';\r\n                --SET @year = '';\r\n                --SET @month = '';\r\n                WITH cteTask AS\r\n                (\r\n\t                SELECT TaskId, OrderNumber, TaskCode, \r\n\t                TaskName,FeatureDescription, Unit, Quantity, StartDate, EndDate,ConstructionPeriod,Note,\r\n\t                Modified \r\n\t                FROM Bud_Task\r\n\t                WHERE PrjId = @PrjId \r\n\t                AND TaskType = @taskType\r\n\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month\r\n                ), cteType AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                FROM XPM_Basic_CodeList AS cl\r\n\t                INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                ),budModifyInfo AS\r\n                (\r\n\t                SELECT  ModifyTaskId,TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,FeatureDescription,Unit,\r\n\t                Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified \r\n\t                FROM Bud_ModifyTask modifyTask\r\n\t                JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                WHERE PrjId=@PrjId AND ModifyType=0\r\n\t                AND FlowState=1 ";
                cmdText = (cmdText + str2 + "  ),cteTask2 AS\r\n                (\r\n\t                SELECT * FROM \r\n\t                (\r\n\t\t                SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,newTask.TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n\t\t                Unit,(newTask.Quantity+ISNULL(inBudModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note, Modified,\r\n\t\t                newTask.Total Total,SubCount  FROM \r\n\t\t                (\r\n                            SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n\t\t\t                Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n\t\t\t                dbo.fn_GetTotal(TaskId) AS Total, dbo.fn_GetCount(TaskId) AS SubCount\r\n\t\t\t                FROM cteTask  WHERE Len(OrderNumber)<=6\r\n                            UNION\r\n                            SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n\t\t\t                Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n\t\t\t                dbo.fn_GetModifyTotal(ModifyTaskId) AS Total, dbo.fn_GetCount(ModifyTaskId) AS SubCount\r\n\t\t\t                FROM budModifyInfo WHERE Len(OrderNumber)<=6\r\n\t\t                )newTask LEFT JOIN \r\n\t\t                (\r\n\t\t\t                --清单内的变更金额\r\n\t\t\t                SELECT modifyTask.TaskId,SUM(modifyTask.Quantity) Quantity from Bud_ModifyTask modifyTask\r\n\t\t\t                JOIN \r\n\t\t\t                (\r\n\t\t\t\t                SELECT Bud_Modify.* FROM Bud_Modify WHERE PrjId=@PrjId\r\n\t\t\t                ) budModify ON modifyTask.ModifyId=budModify.ModifyId \r\n\t\t\t                where budModify.FlowState=1 AND modifyType=1 ") + str2 + " group by modifyTask.TaskId\r\n\t\t                ) inBudModifyTask ON newTask.TaskId=inBudModifyTask.TaskId\r\n\t                ) tab WHERE Len(OrderNumber)<=6\r\n                )\r\n                SELECT  * FROM\r\n                ( \r\n\t                SELECT No, cteTask2.TaskId,\r\n\t\t            cteTask2.OrderNumber, cteTask2.TaskCode, cteTask2.TaskName, cteTask2.FeatureDescription,cteTask2.Unit, \r\n\t\t            cteTask2.Quantity, cteTask2.StartDate, cteTask2.EndDate,ConstructionPeriod,cteTask2.Note,cteTask2.Modified,\r\n\t\t            ISNULL(cteTask2.Total, 0) AS Total, cteTask2.SubCount, \r\n\t\t            ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice,\r\n\t\t            cteType.CodeNo,cteType.CodeName AS TypeName\r\n\t                FROM cteTask2\r\n\t\t            LEFT JOIN cteType ON LEN(cteTask2.OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                ) AS a\r\n            ";
            }
            else
            {
                cmdText = "\r\n                --DECLARE @PrjId nvarchar(200), @taskType NVARCHAR(50)\r\n                --DECLARE @year char(4)\r\n                --DECLARE @month char(2)\r\n                --SET @PrjId = 'b3d250d1-5dcc-4b87-981d-eea8e40a3dca';\r\n                --SET @taskType='';\r\n                --SET @year = '';\r\n                --SET @month = '';\r\n                WITH cteBudTask AS          --分部分项\r\n                (\r\n                    SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,Unit,\r\n                    ISNULL(Quantity,0) Quantity,StartDate, EndDate,ConstructionPeriod,Note,Modified,ISNULL(UnitPrice,0) UnitPrice,\r\n                    ISNULL(Total,0) Total,dbo.fn_GetCount(TaskId) AS SubCount FROM Bud_Task \r\n                    WHERE PrjId=@PrjId AND TaskType = @taskType\r\n\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month\r\n                ),outBudModifyInfo AS\r\n                (\r\n\t                SELECT ModifyTaskId,TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,FeatureDescription,Unit,\r\n\t                Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified,ISNULL(UnitPrice,0) UnitPrice,Total,\r\n\t                dbo.fn_GetCount(ModifyTaskId) AS SubCount\r\n\t                FROM Bud_ModifyTask modifyTask\r\n\t                JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                WHERE PrjId=@PrjId AND ModifyType=0\r\n\t                AND FlowState=1 ";
                cmdText = (cmdText + str2 + "\r\n                ),cteTask2 AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,* FROM \r\n\t                (\r\n\t\t                SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n\t\t                Unit,(newTask.Quantity+ISNULL(inBudModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note, Modified,UnitPrice,\r\n\t\t                (newTask.Total+ISNULL(inBudModifyTask.Total,0)) Total,SubCount  FROM \r\n\t\t                (\r\n\t\t\t                SELECT * FROM cteBudTask\r\n\t\t\t                UNION\r\n\t\t\t                SELECT * FROM outBudModifyInfo\r\n\t\t                )newTask LEFT JOIN \r\n\t\t                (\r\n\t\t\t                --清单内的变更金额\r\n\t\t\t                SELECT modifyTask.TaskId,SUM(modifyTask.Quantity) Quantity,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask\r\n\t\t\t                JOIN \r\n\t\t\t                (\r\n\t\t\t\t                SELECT Bud_Modify.* FROM Bud_Modify WHERE PrjId=@PrjId\r\n\t\t\t                ) budModify ON modifyTask.ModifyId=budModify.ModifyId \r\n\t\t\t                where budModify.FlowState=1 AND modifyType=1 ") + str2 + " group by modifyTask.TaskId\r\n\t\t                ) inBudModifyTask ON newTask.TaskId=inBudModifyTask.TaskId\r\n\t                ) tab WHERE Len(OrderNumber)<=6\r\n                ), cteType AS               --任务类型\r\n                (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                    FROM XPM_Basic_CodeList AS cl\r\n                    INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                    WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                )\r\n                SELECT No, cteTask2.TaskId,\r\n\t\t        cteTask2.OrderNumber, cteTask2.TaskCode, cteTask2.TaskName, cteTask2.FeatureDescription,cteTask2.Unit, \r\n\t\t        cteTask2.Quantity, cteTask2.StartDate, cteTask2.EndDate,ConstructionPeriod,cteTask2.Note,cteTask2.Modified,\r\n\t\t        ISNULL(cteTask2.Total, 0) AS Total, cteTask2.SubCount, \r\n\t\t        ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice,\r\n                cteType.CodeNo,ISNULL(cteType.CodeName,'')  AS TypeName FROM cteTask2\r\n                LEFT JOIN cteType ON LEN(cteTask2.OrderNumber) / 3 \r\n                = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n            ";
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@taskType", taskType), new SqlParameter("@year", year), new SqlParameter("@month", month) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetTaskInfo2(string prjId)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId) };
            return SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "usp_GetAllTaskByPrj", commandParameters);
        }

        public static DataTable GetTaskInfo2(string prjId, string isWBSRelevance, string taskType, string year, string month)
        {
            string cmdText = string.Empty;
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(taskType))
            {
                str2 = " and 1<>1 ";
            }
            if (isWBSRelevance == "1")
            {
                cmdText = "\r\n                --DECLARE @PrjId nvarchar(200),@taskType NVARCHAR(50)\r\n                --DECLARE @year char(4)\r\n                --DECLARE @month char(2)\r\n                --SET @PrjId = 'b3d250d1-5dcc-4b87-981d-eea8e40a3dca';\r\n                --SET @taskType='';\r\n                --SET @year = '';\r\n                --SET @month = '';\r\n                WITH cteTask AS\r\n                (\r\n\t                SELECT TaskId, OrderNumber, TaskCode, \r\n\t                TaskName,FeatureDescription, Unit, Quantity, StartDate, EndDate,ConstructionPeriod,Note,\r\n\t                Modified \r\n\t                FROM Bud_Task\r\n\t                WHERE PrjId = @PrjId \r\n\t                AND TaskType = @taskType\r\n\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month\r\n                ), cteType AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                FROM XPM_Basic_CodeList AS cl\r\n\t                INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                ),budModifyInfo AS\r\n                (\r\n\t                SELECT ModifyTaskId,TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,FeatureDescription,Unit,\r\n\t                Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified \r\n\t                FROM Bud_ModifyTask modifyTask\r\n\t                JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                WHERE PrjId=@PrjId AND ModifyType=0\r\n\t                AND FlowState=1 ";
                cmdText = (cmdText + str2 + "  ),cteTask2 AS\r\n                (\r\n\t                SELECT * FROM \r\n\t                (\r\n\t\t                SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,newTask.TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n\t\t                Unit,(newTask.Quantity+ISNULL(inBudModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note, Modified,\r\n\t\t                newTask.Total Total,SubCount  FROM \r\n\t\t                (\r\n                            SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n\t\t\t                Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n\t\t\t                dbo.fn_GetTotal(TaskId) AS Total, dbo.fn_GetCount(TaskId) AS SubCount\r\n\t\t\t                FROM cteTask  \r\n                            UNION\r\n                            SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n\t\t\t                Unit,Quantity,StartDate,EndDate,ConstructionPeriod,Note,Modified,\r\n\t\t\t                dbo.fn_GetModifyTotal(ModifyTaskId) AS Total, dbo.fn_GetCount(ModifyTaskId) AS SubCount\r\n\t\t\t                FROM budModifyInfo \r\n\t\t                )newTask LEFT JOIN \r\n\t\t                (\r\n\t\t\t                --清单内的变更金额\r\n\t\t\t                SELECT modifyTask.TaskId,SUM(modifyTask.Quantity) Quantity from Bud_ModifyTask modifyTask\r\n\t\t\t                JOIN \r\n\t\t\t                (\r\n\t\t\t\t                SELECT Bud_Modify.* FROM Bud_Modify WHERE PrjId=@PrjId\r\n\t\t\t                ) budModify ON modifyTask.ModifyId=budModify.ModifyId \r\n\t\t\t                where budModify.FlowState=1 AND modifyType=1 ") + str2 + " group by modifyTask.TaskId\r\n\t\t                ) inBudModifyTask ON newTask.TaskId=inBudModifyTask.TaskId\r\n\t                ) tab \r\n                )\r\n                SELECT  * FROM\r\n                ( \r\n\t                SELECT No, cteTask2.TaskId,\r\n\t\t            cteTask2.OrderNumber, cteTask2.TaskCode, cteTask2.TaskName, cteTask2.FeatureDescription,cteTask2.Unit, \r\n\t\t            cteTask2.Quantity, cteTask2.StartDate, cteTask2.EndDate,ConstructionPeriod,cteTask2.Note,cteTask2.Modified,\r\n\t\t            ISNULL(cteTask2.Total, 0) AS Total, cteTask2.SubCount, \r\n\t\t            ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice,\r\n\t\t            cteType.CodeNo,cteType.CodeName AS TypeName\r\n\t                FROM cteTask2\r\n\t\t            LEFT JOIN cteType ON LEN(cteTask2.OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                ) AS a\r\n            ";
            }
            else
            {
                cmdText = "\r\n                --DECLARE @PrjId nvarchar(200), @taskType NVARCHAR(50)\r\n                --DECLARE @year char(4)\r\n                --DECLARE @month char(2)\r\n                --SET @PrjId = 'b3d250d1-5dcc-4b87-981d-eea8e40a3dca';\r\n                --SET @taskType='';\r\n                --SET @year = '';\r\n                --SET @month = '';\r\n                WITH cteBudTask AS          --分部分项\r\n                (\r\n                    SELECT TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,Unit,\r\n                    ISNULL(Quantity,0) Quantity,StartDate, EndDate,ConstructionPeriod,Note,Modified,ISNULL(UnitPrice,0) UnitPrice,\r\n                    ISNULL(Total,0) Total,dbo.fn_GetCount(TaskId) AS SubCount FROM Bud_Task \r\n                    WHERE PrjId=@PrjId AND TaskType = @taskType\r\n\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month\r\n                ),outBudModifyInfo AS\r\n                (\r\n\t                SELECT ModifyTaskId,TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,FeatureDescription,Unit,\r\n\t                Quantity,StartDate,EndDate,ConstructionPeriod,modifyTask.Note,NULL Modified,ISNULL(UnitPrice,0) UnitPrice,Total,\r\n\t                dbo.fn_GetCount(ModifyTaskId) AS SubCount\r\n\t                FROM Bud_ModifyTask modifyTask\r\n\t                JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                WHERE PrjId=@PrjId AND ModifyType=0\r\n\t                AND FlowState=1 ";
                cmdText = (cmdText + str2 + "\r\n                ),cteTask2 AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,* FROM \r\n\t                (\r\n\t\t                SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,FeatureDescription,\r\n\t\t                Unit,(newTask.Quantity+ISNULL(inBudModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note, Modified,UnitPrice,\r\n\t\t                (newTask.Total+ISNULL(inBudModifyTask.Total,0)) Total,SubCount  FROM \r\n\t\t                (\r\n\t\t\t                SELECT * FROM cteBudTask\r\n\t\t\t                UNION\r\n\t\t\t                SELECT * FROM outBudModifyInfo\r\n\t\t                )newTask LEFT JOIN \r\n\t\t                (\r\n\t\t\t                --清单内的变更金额\r\n\t\t\t                SELECT modifyTask.TaskId,SUM(modifyTask.Quantity) Quantity,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask\r\n\t\t\t                JOIN \r\n\t\t\t                (\r\n\t\t\t\t                SELECT Bud_Modify.* FROM Bud_Modify WHERE PrjId=@PrjId\r\n\t\t\t                ) budModify ON modifyTask.ModifyId=budModify.ModifyId \r\n\t\t\t                where budModify.FlowState=1 AND modifyType=1 ") + str2 + " group by modifyTask.TaskId\r\n\t\t                ) inBudModifyTask ON newTask.TaskId=inBudModifyTask.TaskId\r\n\t                ) tab \r\n                ), cteType AS               --任务类型\r\n                (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                    FROM XPM_Basic_CodeList AS cl\r\n                    INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                    WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                )\r\n                SELECT No, cteTask2.TaskId,\r\n\t\t        cteTask2.OrderNumber, cteTask2.TaskCode, cteTask2.TaskName, cteTask2.FeatureDescription,cteTask2.Unit, \r\n\t\t        cteTask2.Quantity, cteTask2.StartDate, cteTask2.EndDate,ConstructionPeriod,cteTask2.Note,cteTask2.Modified,\r\n\t\t        ISNULL(cteTask2.Total, 0) AS Total, cteTask2.SubCount, \r\n\t\t        ISNULL(cteTask2.Total / NULLIF(cteTask2.Quantity, 0), 0) AS UnitPrice,\r\n                cteType.CodeNo,ISNULL(cteType.CodeName,'')  AS TypeName FROM cteTask2\r\n                LEFT JOIN cteType ON LEN(cteTask2.OrderNumber) / 3 \r\n                = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n            ";
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@taskType", taskType), new SqlParameter("@year", year), new SqlParameter("@month", month) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetTaskInfoByParent(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                return null;
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@taskId", taskId) };
            return SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "usp_GetAllTaskByParent", commandParameters);
        }

        private static void GetTaskQuantityTotal(string taskId, string priceType, ref decimal quantity, ref decimal total)
        {
            if (GetById(taskId) != null)
            {
                IList<TaskResource> resourcesByTaskId = GetResourcesByTaskId(taskId);
                if (resourcesByTaskId != null)
                {
                    using (IEnumerator<TaskResource> enumerator = resourcesByTaskId.GetEnumerator())
                    {
                        TaskResource tr;
                        while (enumerator.MoveNext())
                        {
                            tr = enumerator.Current;
                            decimal num = tr.Quantity;
                            decimal price = tr.Price;
                            using (pm2Entities entities = new pm2Entities())
                            {
                                if (!string.IsNullOrEmpty(priceType))
                                {
                                    price = (from m in entities.Res_Price
                                             where (m.ResourceId == tr.Resource.Id) && (m.PriceTypeId == priceType)
                                             select m.PriceValue).FirstOrDefault<decimal>();
                                }
                                total += num * price;
                                continue;
                            }
                        }
                    }
                }
            }
        }

        public static DataTable GetTaskRes(string taskIds)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                    --DECLARE @TaskId nvarchar(200)\r\n                    --SET @TaskId = '4e6a5c83-91de-48ba-b301-73fbada527c0';\r\n                    WITH cteResource AS\r\n                    (\r\n\t                    SELECT ResourceId,dbo.GetResourceType(ResourceId) TopResId,ResourceName,Unit\r\n\t                    FROM Res_Resource \r\n                    ),cteTaskRes AS\r\n                    (\r\n\t                    SELECT TaskId,ResourceId,ResourceQuantity,ResourcePrice,LossCoefficient\r\n\t                    FROM Bud_TaskResource \r\n\t                    UNION\r\n\t                    SELECT TaskId,ResourceId,ResourceQuantity,ResourcePrice,LossCoefficient FROM Bud_ModifyTaskRes BTS\r\n                        LEFT JOIN  Bud_ModifyTask BTK\r\n                        ON BTS.ModifyTaskId=BTK.ModifyTaskId\r\n                    )\r\n                    SELECT TaskId,cteResource.ResourceId,ResourceName,ResourceTypeName,Res_Unit.[Name] AS UnitName,\r\n                    convert(decimal(18,2),ResourceQuantity) ResourceQuantity,\r\n                    convert(decimal(18,2),ResourcePrice) ResourcePrice,\r\n                    convert(decimal(18,2),LossCoefficient) LossCoefficient,\r\n                    convert(decimal(18,2),(ResourceQuantity*ResourcePrice*LossCoefficient)) AS ResTotal\r\n                    FROM cteResource \r\n                    JOIN cteTaskRes ON cteResource.ResourceId = cteTaskRes.ResourceId\r\n                    JOIN Res_Unit ON cteResource.Unit = Res_Unit.UnitId\r\n                    JOIN Res_ResourceType ON cteResource.TopResId = Res_ResourceType.ResourceTypeId\r\n                    WHERE TaskId IN(");
            builder.Append(taskIds + ")");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public static IList<int?> GetVersion(string prjId)
        {
            IList<int?> list = new List<int?>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from bt in entities.Bud_Task
                        where bt.PrjId == prjId
                        orderby bt.Version descending
                        select bt.Version).Distinct<int?>().ToList<int?>();
            }
        }

        public static bool IsCheckAllLeadNodes(List<string> lstOrderNumbers, string prjId)
        {
            bool flag = true;
            int num = 0;
            using (pm2Entities entities = new pm2Entities())
            {
                int length = (from m in lstOrderNumbers
                              orderby m.Length
                              select m).First<string>().Length;
                using (List<string>.Enumerator enumerator = (from m in lstOrderNumbers
                                                             where m.Length == length
                                                             select m).Distinct<string>().ToList<string>().GetEnumerator())
                {
                    string orderNumber;
                    while (enumerator.MoveNext())
                    {
                        orderNumber = enumerator.Current;
                        int num2 = (from m in entities.Bud_Task
                                    where m.OrderNumber.StartsWith(orderNumber) && (m.PrjId == prjId)
                                    select m.ParentId).Count<string>();
                        if (num2 == 0)
                        {
                            num++;
                        }
                        else
                        {
                            num += num2;
                        }
                    }
                }
                if (num != lstOrderNumbers.Count)
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static bool IsStructured(IList<string> orderNumberList)
        {
            int num = orderNumberList.Max<string>((Func<string, int>)(n => n.Length));
            Func<string, bool> predicate = null;
            Func<string, bool> func2 = null;
            Func<string, bool> func3 = null;
            for (int len = num; len > 0; len -= 3)
            {
                if (predicate == null)
                {
                    predicate = n => n.Length == len;
                }
                IEnumerable<string> enumerable = orderNumberList.Where<string>(predicate);
                if (func2 == null)
                {
                    func2 = n => n.Length == (len - 3);
                }
                IEnumerable<string> source = orderNumberList.Where<string>(func2);
                if (func3 == null)
                {
                    func3 = n => n.Length < len;
                }
                IEnumerable<string> enumerable3 = orderNumberList.Where<string>(func3);
                foreach (string str in enumerable)
                {
                    string str2 = str.Substring(0, str.Length - 3);
                    if (!source.Contains<string>(str2) && (enumerable3.Count<string>() > 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static DataTable showMaterialListByPrjId(string IdList, string prjId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n            --DECLARE @PrjId NVARCHAR(200),@priceType NVARCHAR(200);\r\n            --SET @PrjId='51df67a6-7a4c-47f1-8b0d-fb6d1da97a9a';\r\n            --set @priceType='192340F1-08E3-4B32-B65D-75E785062D05';\r\n            SELECT TaskResourceId as Id,Res_Resource.ResourceId,ResourceCode,ResourceName,[Name] AS Unit,\r\n            Specification, ISNULL(PriceValue,0.000) AS price, Res_Resource.Brand,ModelNumber,TechnicalParameter,\r\n            0.000 AS number,ISNULL(LossCoefficient,1.00) LossCoefficient FROM Res_Resource \r\n            LEFT JOIN (\r\n\t            select Bud_TaskResource.* from Bud_TaskResource INNER JOIN (\r\n\t\t            SELECT PrjId,MAX(Version) Version FROM Bud_TaskChange\r\n\t\t            WHERE PrjId=@PrjId\r\n\t\t            GROUP BY PrjId\r\n\t            ) MaxVersion ON Bud_TaskResource.PrjGuid=MaxVersion.PrjId AND Versions=MaxVersion.Version\r\n            ) taskResInfo ON Res_Resource.ResourceId=taskResInfo.ResourceId\r\n            LEFT JOIN Res_Unit ON UnitId = Unit \r\n            LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId AND PriceTypeId = @priceType \r\n            LEFT JOIN Res_PriceType ON Res_PriceType.PriceTypeId = Res_Price.PriceTypeId \r\n            WHERE Res_Resource.ResourceId IN(\r\n            ");
            builder.Append(IdList);
            builder.Append(")  ORDER BY ResourceCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@priceType", SqlDbType.NVarChar, 0x40), new SqlParameter("@PrjId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = priceTypeId;
            commandParameters[1].Value = prjId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static DataTable showMaterialListForAdd(string IdList, string TaskId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TaskResourceId as Id,Res_Resource.ResourceId,ResourceCode,ResourceName,[Name] AS Unit,Specification, ISNULL(PriceValue,0.000) AS price, ");
            builder.Append("Res_Resource.Brand,ModelNumber,TechnicalParameter,");
            builder.Append("0.000 AS number FROM Res_Resource ");
            builder.Append("LEFT JOIN Bud_TaskResource ON Res_Resource.ResourceId=Bud_TaskResource.ResourceId and TaskId=@taskId ");
            builder.Append("LEFT JOIN Res_Unit ON UnitId = Unit ");
            builder.Append("LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId AND PriceTypeId = @priceType ");
            builder.Append("LEFT JOIN Res_PriceType ON Res_PriceType.PriceTypeId = Res_Price.PriceTypeId ");
            builder.Append("WHERE Res_Resource.ResourceId IN( ");
            builder.Append(IdList);
            builder.Append(")  ORDER BY ResourceCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@priceType", SqlDbType.NVarChar, 0x40), new SqlParameter("@taskId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = priceTypeId;
            commandParameters[1].Value = TaskId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void Update(BudTask budtask, bool isLock)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_Task task = (from m in entities.Bud_Task
                                 where m.TaskId == budtask.Id
                                 select m).FirstOrDefault<Bud_Task>();
                task.TaskCode = budtask.Code;
                task.TaskName = budtask.Name;
                task.StartDate = budtask.StartDate;
                task.EndDate = budtask.EndDate;
                task.Quantity = budtask.Quantity;
                task.Unit = budtask.Unit;
                task.UnitPrice = budtask.UnitPrice;
                task.Note = budtask.Note;
                task.ParentId = budtask.ParentId;
                task.Total = budtask.Total;
                if (isLock && ((from m in entities.Bud_Task
                                where (((((((((m.TaskId == budtask.Id) && (m.TaskCode == budtask.Code)) && (m.TaskName == budtask.Name)) && (m.StartDate == budtask.StartDate)) && (m.EndDate == budtask.EndDate)) && (m.Unit == budtask.Unit)) && (m.Quantity == budtask.Quantity)) && (m.Note == budtask.Note)) && (m.UnitPrice == budtask.UnitPrice)) && (m.Total == budtask.Total)
                                select m).FirstOrDefault<Bud_Task>() == null))
                {
                    task.Modified = "1";
                }
                entities.SaveChanges();
            }
        }

        public static void UpdateChangeTask(string taskId, string replaceOrderNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n            WITH Children AS(\r\n\t\t\t\tSELECT  TaskId,ParentId,OrderNumber,Version,PrjId,TaskCode,TaskName,Unit,\r\n\t\t\t\tQuantity,UnitPrice,StartDate,EndDate,Note,IsValid,InputUser,InputDate,\r\n\t\t\t\tTotal,Modified,ConstructionPeriod FROM Bud_Task WHERE TaskId=@taskId\r\n\t\t\t\tUNION ALL\r\n\t\t\t\tSELECT Tasks.TaskId,Tasks.ParentId,Tasks.OrderNumber,Tasks.Version,Tasks.PrjId,\r\n\t\t\t\tTasks.TaskCode,Tasks.TaskName,Tasks.Unit,Tasks.Quantity,Tasks.UnitPrice,\r\n\t\t\t\tTasks.StartDate,Tasks.EndDate,Tasks.Note,Tasks.IsValid,Tasks.InputUser,Tasks.InputDate,\r\n\t\t\t\tTasks.Total,Tasks.Modified,Tasks.ConstructionPeriod FROM Bud_Task AS Tasks \r\n\t\t\t\tJOIN Children ON Tasks.ParentId=Children.TaskId\r\n            )\r\n            UPDATE Bud_Task SET OrderNumber=@repOrderNumber+RIGHT(OrderNumber,(LEN(OrderNumber)-LEN(@repOrderNumber)))\r\n            WHERE TaskId IN( SELECT TaskId FROM Children) ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@taskId", taskId), new SqlParameter("@repOrderNumber", replaceOrderNumber) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void UpdateResourcePrice(string prjId, int version, string priceTypeId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> list = (from m in entities.Bud_Task
                                     where (m.PrjId == prjId) && (m.Version == version)
                                     select m.TaskId).ToList<string>();
                if ((list != null) && (list.Count > 0))
                {
                    foreach (string str in list)
                    {
                        UpdateResPrice(str, priceTypeId);
                    }
                }
            }
        }

        private static void UpdateResPrice(string taskId, string priceTypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT ResourceId FROM Bud_TaskResource WHERE TaskId='{0}'", taskId);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                StringBuilder builder2 = new StringBuilder();
                while (reader.Read())
                {
                    StringBuilder builder3 = new StringBuilder();
                    string str = reader["ResourceId"].ToString();
                    builder3.AppendFormat("SELECT PriceValue FROM Res_Price WHERE  ResourceId='{0}' AND PriceTypeId='{1}'", str, priceTypeId);
                    object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder3.ToString(), null);
                    if (obj2 == null)
                    {
                        obj2 = 0M.ToString();
                    }
                    builder2.AppendFormat("UPDATE Bud_TaskResource SET ResourcePrice='{0}' WHERE TaskId='{1}' AND ResourceId='{2}' ", obj2, taskId, str);
                    builder2.AppendLine();
                }
                if (builder2.Length > 0)
                {
                    SqlHelper.ExecuteNonQuery(CommandType.Text, builder2.ToString(), null);
                }
            }
        }

        private static bool UpdateVersion(string prjId, string isWBSRelevance)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> lstIds = new List<string>();
                lstIds = (from m in entities.Bud_Task
                          where m.PrjId == prjId
                          select m.TaskId).ToList<string>();
                if (lstIds.Count <= 0)
                {
                    return flag;
                }
                List<BudTask> list2 = ChangeId(lstIds);
                int num = 0;
                foreach (string str in lstIds)
                {
                    BudTask task = list2[num];
                    task.InputDate = DateTime.Now;
                    task.Version = 1;
                    Add(task, false);
                    if (isWBSRelevance == "1")
                    {
                        foreach (TaskResource resource in GetResourcesByTaskId(str).ToList<TaskResource>())
                        {
                            task.AddResource(resource.Resource, resource.Quantity, resource.Price, resource.LossCoefficient.Value, "add");
                            AddResource(task);
                        }
                    }
                    num++;
                    if (num == list2.Count)
                    {
                        flag = true;
                    }
                }
                if (isWBSRelevance == "0")
                {
                    TaskResource.AddResource(TaskResource.GetResByPrjVersion(prjId, 1), prjId, 1);
                }
            }
            return flag;
        }

        // Properties
        public string Code { get; set; }

        public DateTime? EndDate { get; set; }

        public string Id { get; set; }

        public DateTime InputDate { get; set; }

        public string InputUser { get; set; }

        public bool IsValid { get; set; }

        public int Layer
        {
            get
            {
                return (this.OrderNumber.Length / 3);
            }
        }

        public string Mondified { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public string OrderNumber { get; set; }

        public string ParentId { get; set; }

        public string Prj { get; set; }

        public decimal Quantity { get; set; }

        public IList<TaskResource> Resources
        {
            get
            {
                if (this.resources == null)
                {
                    this.resources = new List<TaskResource>();
                    using (pm2Entities entities = new pm2Entities())
                    {
                        foreach (var type in from r in entities.Bud_TaskResource
                                             where r.Bud_Task.TaskId == this.Id
                                             select new { ResourceId = r.Res_Resource.ResourceId, ResourceQuantity = r.ResourceQuantity, ResourcePrice = r.ResourcePrice, InputUser = r.InputUser, InputDate = r.InputDate })
                        {
                            TaskResource item = new TaskResource
                            {
                                Resource = Resource.GetById(type.ResourceId)
                            };
                            decimal? resourceQuantity = type.ResourceQuantity;
                            item.Quantity = resourceQuantity.HasValue ? resourceQuantity.GetValueOrDefault() : 0M;
                            decimal? resourcePrice = type.ResourcePrice;
                            item.Price = resourcePrice.HasValue ? resourcePrice.GetValueOrDefault() : 0M;
                            item.InputUser = type.InputUser;
                            item.InputDate = new DateTime?(type.InputDate);
                            this.resources.Add(item);
                        }
                    }
                }
                return this.resources;
            }
            set
            {
                this.resources = value;
            }
        }

        public DateTime? StartDate { get; set; }

        public decimal? Total { get; set; }

        public decimal Total2 { get; set; }

        public string TypeName
        {
            get
            {
                if (string.IsNullOrEmpty(this.typeName))
                {
                    BudTaskType byLayer = BudTaskType.GetByLayer(this.Layer);
                    if (byLayer != null)
                    {
                        this.typeName = byLayer.Name;
                    }
                }
                return this.typeName;
            }
        }

        public string Unit { get; set; }

        public decimal? UnitPrice { get; set; }

        public int Version { get; set; }
    }
}

 
