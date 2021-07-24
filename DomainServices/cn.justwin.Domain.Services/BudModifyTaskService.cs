namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudModifyTaskService : Repository<BudModifyTask>
    {
        public void DelByModifyTaskId(string modifyTaskId)
        {
            string sql = string.Format("DELETE Bud_ModifyTask WHERE ModifyTaskId='{0}'", modifyTaskId);
            base.ExcuteSql(sql);
        }

        public void DelModifyTask(string modifyId)
        {
            string sql = string.Format("DELETE Bud_ModifyTask WHERE ModifyId='{0}'", modifyId);
            base.ExcuteSql(sql);
        }

        public BudTask GetBudTask(string taskId)
        {
            BudModifyService service = new BudModifyService();
            BudModifyTask byId = this.GetById(taskId);
            BudTask task2 = new BudTask();
            if (byId != null)
            {
                BudModify modify = service.GetById(byId.ModifyId);
                task2.TaskId = byId.ModifyTaskId;
                task2.ParentId = byId.TaskId;
                task2.OrderNumber = byId.OrderNumber;
                task2.Version = 1;
                task2.PrjId = modify.PrjId;
                task2.TaskCode = byId.ModifyTaskCode;
                task2.TaskName = byId.ModifyTaskContent;
                task2.Unit = byId.Unit;
                task2.Quantity = new decimal?(byId.Quantity);
                task2.UnitPrice = new decimal?(byId.UnitPrice);
                task2.Total = new decimal?(byId.Total);
                task2.StartDate = byId.StartDate;
                task2.EndDate = byId.EndDate;
                task2.ConstructionPeriod = byId.ConstructionPeriod;
                task2.Note = byId.Note;
                task2.InputDate = DateTime.Now;
                task2.InputUser = modify.InputUser;
                task2.TaskType = string.Empty;
                task2.IsValid = true;
            }
            List<BudModifyTask> inModifyTask = this.GetInModifyTask(taskId);
            if (inModifyTask != null)
            {
                foreach (BudModifyTask task3 in inModifyTask)
                {
                    decimal? quantity = task2.Quantity;
                    decimal num3 = task3.Quantity;
                    task2.Quantity = quantity.HasValue ? new decimal?(quantity.GetValueOrDefault() + num3) : null;
                    decimal? total = task2.Total;
                    decimal num4 = task3.Total;
                    task2.Total = total.HasValue ? new decimal?(total.GetValueOrDefault() + num4) : null;
                }
                decimal num = 0M;
                decimal num2 = 0M;
                if (task2.Total.HasValue)
                {
                    num2 = 0M;
                }
                if (task2.Quantity.HasValue && (task2.Quantity.Value != 0M))
                {
                    num = num2 / task2.Quantity.Value;
                }
                task2.UnitPrice = new decimal?(num);
            }
            return task2;
        }

        public BudModifyTask GetById(string id)
        {
            return (from bmt in this
                where bmt.ModifyTaskId == id
                select bmt).FirstOrDefault<BudModifyTask>();
        }

        public List<BudModifyTask> GetByModifyId(string modifyId)
        {
            return (from bmt in this
                where bmt.ModifyId == modifyId
                select bmt).ToList<BudModifyTask>();
        }

        public BudModifyTask GetByTaskId(string taskId)
        {
            return (from bmt in this
                where bmt.TaskId == taskId
                select bmt).FirstOrDefault<BudModifyTask>();
        }

        public List<BudModifyTask> GetInModifyTask(string taskId)
        {
            BudModifyService service = new BudModifyService();
            return (from mt in this
                join m in service on mt.ModifyId equals m.ModifyId into m
                where (mt.TaskId == taskId) && (mt.ModifyType == 1)
                select mt).ToList<BudModifyTask>();
        }

        public string GetOrderNumber(string prjId, string parentId)
        {
            string str = "001";
            BudModifyService source = new BudModifyService();
            if (string.IsNullOrEmpty(parentId))
            {
                int num;
                int.TryParse((from mts in this
                    join ms in source.AsQueryable<BudModify>() on mts.ModifyId equals ms.ModifyId 
                    where (ms.PrjId == prjId) && (mts.TaskId == null)
                    select mts.OrderNumber).Max<string>(), out num);
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
            IQueryable<string> queryable2 = from mts in this
                where mts.ParentId == parentId
                select mts.OrderNumber;
            if (queryable2.Count<string>() > 0)
            {
                string str3 = queryable2.Max<string>();
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
            IQueryable<string> queryable3 = from mts in this
                where mts.ModifyTaskId == parentId
                select mts.OrderNumber;
            if (queryable3.Count<string>() > 0)
            {
                str = queryable3.First<string>() + "001";
            }
            return str;
        }

        public bool IsOrderNumberExists(string prjId, string orderNumber)
        {
            return ((from t in this
                where (t.PrjId2 == prjId) && (t.OrderNumber == orderNumber)
                select t).Count<BudModifyTask>() > 0);
        }

        public void UpdateTotal2(string modifyTaskId)
        {
            try
            {
                BudModifyTask byId = this.GetById(modifyTaskId);
                BudModifyTaskResService service = new BudModifyTaskResService();
                decimal num = 0M;
                List<decimal> list = (from r in service
                    where r.ModifyTaskId == modifyTaskId
                    select r.ResourcePrice * r.ResourceQuantity).ToList<decimal>();
                if (list != null)
                {
                    num = ((IEnumerable<decimal>) list).Sum();
                }
                byId.Total2 = new decimal?(num);
                base.Update(byId);
                new BudTaskService().UpdateTotal2Up(byId.ParentId);
            }
            catch (Exception exception)
            {
                string title = string.Format("BudModifyTaskService.UpdateTotal2({0})", modifyTaskId);
                Log4netHelper.Error(exception, title, "bery");
            }
        }
    }
}

