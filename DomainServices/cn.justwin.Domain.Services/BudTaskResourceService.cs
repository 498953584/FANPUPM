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

    public class BudTaskResourceService : Repository<BudTaskResource>
    {
        private BudTaskService tSer = new BudTaskService();

        public void AddTaskResource(string prjId)
        {
            IList<BudTask> byProject = this.tSer.GetByProject(prjId, 0x3e7);
            int version = 1;
            if (byProject != null)
            {
                version = byProject[0].Version.Value;
            }
            List<BudTaskResource> allRes = this.GetAllRes(prjId, version);
            List<BudTaskResource> list3 = this.GetTaskRes(allRes);
            if (list3 != null)
            {
                using (List<BudTaskResource>.Enumerator enumerator = list3.GetEnumerator())
                {
                    Func<BudTask, bool> predicate = null;
                    Func<BudTaskResource, bool> func2 = null;
                    BudTaskResource taskRes;
                    while (enumerator.MoveNext())
                    {
                        taskRes = enumerator.Current;
                        if (predicate == null)
                        {
                            predicate = t => t.TaskId == taskRes.TaskId;
                        }
                        BudTask task = byProject.Where<BudTask>(predicate).FirstOrDefault<BudTask>();
                        if (task != null)
                        {
                            string mainTaskId = task.TaskId;
                            mainTaskId = mainTaskId.Substring(7, mainTaskId.Length - 7);
                            foreach (BudTask task2 in (from t in this.tSer
                                where t.TaskId.Contains(mainTaskId) && (t.TaskType.Trim() != "")
                                select t).ToList<BudTask>())
                            {
                                decimal num2 = 0M;
                                if (task2.Quantity.HasValue && (task.Quantity != 0M))
                                {
                                    num2 = task2.Quantity.Value / task.Quantity.Value;
                                }
                                if (func2 == null)
                                {
                                    func2 = tr => (tr.TaskId == taskRes.TaskId) && (tr.ResourceId == taskRes.ResourceId);
                                }
                                List<BudTaskResource> list5 = allRes.Where<BudTaskResource>(func2).ToList<BudTaskResource>();
                                decimal num3 = 0M;
                                decimal num4 = 0M;
                                decimal num5 = 0M;
                                foreach (BudTaskResource resource in list5)
                                {
                                    if ((resource.ResourceQuantity != 0M) && (resource.ResourcePrice != 0M))
                                    {
                                        num3 += resource.ResourceQuantity.Value * resource.ResourcePrice.Value;
                                        num4 += resource.ResourceQuantity.Value;
                                    }
                                }
                                BudTaskResource item = new BudTaskResource {
                                    TaskResourceId = Guid.NewGuid().ToString(),
                                    TaskId = task2.TaskId,
                                    ResourceId = taskRes.ResourceId
                                };
                                num4 *= num2;
                                item.ResourceQuantity = new decimal?(num4);
                                if (num4 != 0M)
                                {
                                    num5 = (num3 * num2) / num4;
                                }
                                item.ResourcePrice = new decimal?(num5);
                                item.PrjGuid = taskRes.PrjGuid;
                                item.Versions = taskRes.Versions;
                                item.InputUser = taskRes.InputUser;
                                item.InputDate = taskRes.InputDate;
                                base.Add(item);
                            }
                        }
                    }
                }
            }
        }

        private List<BudTaskResource> GetAllRes(string prjId, int version)
        {
            List<BudTaskResource> list = (from taskRes in this
                join task in this.tSer on taskRes.TaskId equals task.TaskId 
                where (task.PrjId == prjId) && (task.TaskType == "")
                select taskRes).ToList<BudTaskResource>();
            BudModifyTaskResService service = new BudModifyTaskResService();
            BudModifyTaskService service2 = new BudModifyTaskService();
            BudModifyService service3 = new BudModifyService();
            using (List<BudModifyTaskRes>.Enumerator enumerator = (from mtr in service
                join mt in service2 on mtr.ModifyTaskId equals mt.ModifyTaskId 
                join m in service3 on mt.ModifyId equals m.ModifyId 
                where (m.PrjId == prjId) && (m.Flowstate == 1)
                select mtr).ToList<BudModifyTaskRes>().GetEnumerator())
            {
                BudModifyTaskRes modifyTaskRes;
                while (enumerator.MoveNext())
                {
                    modifyTaskRes = enumerator.Current;
                    BudModify byId = service3.GetById(modifyTaskRes.ModifyId);
                    BudModifyTask task = (from mts in service2
                        where mts.ModifyTaskId == modifyTaskRes.ModifyTaskId
                        select mts).FirstOrDefault<BudModifyTask>();
                    BudTaskResource item = new BudTaskResource {
                        TaskResourceId = modifyTaskRes.ModifyTaskResId
                    };
                    if (task.ModifyType == 0)
                    {
                        item.TaskId = modifyTaskRes.ModifyTaskId;
                    }
                    else
                    {
                        item.TaskId = task.TaskId;
                    }
                    item.ResourceId = modifyTaskRes.ResourceId;
                    item.ResourceQuantity = new decimal?(modifyTaskRes.ResourceQuantity);
                    item.ResourcePrice = new decimal?(modifyTaskRes.ResourcePrice);
                    item.InputDate = DateTime.Now;
                    item.InputUser = byId.InputUser;
                    item.PrjGuid = prjId;
                    item.Versions = new int?(version);
                    list.Add(item);
                }
            }
            return list;
        }

        public BudTaskResource GetById(string id)
        {
            return (from ts in this
                where ts.TaskResourceId == id
                select ts).FirstOrDefault<BudTaskResource>();
        }

        public DataTable GetResPriceDifference(string prjId, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetResPriceDifferenceCount(prjId);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            List<SqlParameter> list = new List<SqlParameter>();
            string str = ConfigHelper.Get("BudgetPriceType");
            string cmdText = "\r\n                    --DECLARE @PrjId NVARCHAR(64),@priceTypeId NVARCHAR(64),@pageSize int,@pageIndex int;\r\n                    --SET @PrjId = '1f37a43d-b2d4-4f4f-9477-fc5fc9633edd';\r\n                    --SET @priceTypeId = '192340F1-08E3-4B32-B65D-75E785062D05';\r\n                    --SET @pageSize = 15;\r\n                    --SET @pageIndex = 1;\r\n                    SELECT TOP(@pageSize) * FROM \r\n                    (\r\n\t                    SELECT ROW_NUMBER() OVER(ORDER BY ResourceCode) Num,ResourceCode,ResourceName,\r\n\t                    Name AS UnIt,Specification,ResourceQuantity,ResourcePrice,PriceValue AS BudGetPrice,\r\n\t                    (ResourcePrice - PriceValue ) AS PriceDiff\r\n\t                    FROM Bud_TaskResource\r\n\t                    JOIN Res_Resource ON Bud_TaskResource.ResourceId = Res_Resource.ResourceId\r\n\t                    JOIN Res_Unit ON Res_Resource.Unit = Res_Unit.UnitId\r\n\t                    JOIN Res_Price ON Res_Resource.ResourceId = Res_Price.ResourceId\r\n\t                    WHERE PrjGuid = @prjId AND PriceTypeId = @priceTypeId\r\n                    )tab WHERE Num>@pageSize*(@pageIndex-1)";
            list.Add(new SqlParameter("@prjId", prjId));
            list.Add(new SqlParameter("@priceTypeId", str));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public int GetResPriceDifferenceCount(string prjId)
        {
            int num = 0;
            string str = ConfigHelper.Get("BudgetPriceType");
            List<SqlParameter> list = new List<SqlParameter>();
            string cmdText = "\r\n                    --DECLARE @prjId NVARCHAR(64),@priceTypeId NVARCHAR(64),@pageSize int,@pageNo int;\r\n                    --SET @prjId = '1f37a43d-b2d4-4f4f-9477-fc5fc9633edd';\r\n                    SELECT COUNT(*) FROM\r\n                    (\r\n\t                    SELECT DISTINCT ResourceCode,ResourceName,Name AS UnIt,Specification,ResourceQuantity,\r\n\t                    ResourcePrice,PriceValue AS BudGetPrice,(ResourcePrice - PriceValue ) AS PriceDiff\r\n\t                    FROM Bud_TaskResource\r\n\t                    JOIN Res_Resource ON Bud_TaskResource.ResourceId = Res_Resource.ResourceId\r\n\t                    JOIN Res_Unit ON Res_Resource.Unit = Res_Unit.UnitId\r\n\t                    JOIN Res_Price ON Res_Resource.ResourceId = Res_Price.ResourceId\r\n\t                    WHERE PrjGuid = @prjId AND PriceTypeId = @priceTypeId\r\n                    )tab";
            list.Add(new SqlParameter("@prjId", prjId));
            list.Add(new SqlParameter("@priceTypeId", str));
            DataTable table = base.ExecuteQuery(cmdText, list.ToArray());
            if (table != null)
            {
                num = Convert.ToInt32(table.Rows[0][0]);
            }
            return num;
        }

        private List<BudTaskResource> GetTaskRes(List<BudTaskResource> listTaskRes)
        {
            List<BudTaskResource> list = new List<BudTaskResource>();
            foreach (BudTaskResource resource in listTaskRes)
            {
                string resId = resource.ResourceId;
                string taskId = resource.TaskId;
                if ((from tr in list
                    where (tr.ResourceId == resId) && (tr.TaskId == taskId)
                    select tr).ToList<BudTaskResource>().Count == 0)
                {
                    list.Add(resource);
                }
            }
            return list;
        }
    }
}

