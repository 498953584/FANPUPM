namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudModifyTaskResService : Repository<BudModifyTaskRes>
    {
        public void DelByModifyId(string modifyId)
        {
            string sql = string.Format("DELETE Bud_ModifyTaskRes WHERE ModifyId='{0}'", modifyId);
            base.ExcuteSql(sql);
        }

        public void DelByModifyTaskId(string modifyTaskId)
        {
            string sql = string.Format("DELETE Bud_ModifyTaskRes WHERE ModifyTaskId='{0}'", modifyTaskId);
            base.ExcuteSql(sql);
        }

        public BudModifyTaskRes GetById(string id)
        {
            return (from bmtr in this
                where bmtr.ModifyTaskResId == id
                select bmtr).FirstOrDefault<BudModifyTaskRes>();
        }

        public List<BudModifyTaskRes> GetInTaskRes(string taskId)
        {
            List<BudModifyTaskRes> listModifyTaskRes = new List<BudModifyTaskRes>();
            BudModifyTaskService service = new BudModifyTaskService();
            listModifyTaskRes = (from mts in this
                join mt in service on mts.ModifyTaskId equals mt.ModifyTaskId
                where mt.TaskId == taskId
                select mts).ToList<BudModifyTaskRes>();
            List<string> modifyTasResIds = this.GetModifyTasResIds(listModifyTaskRes);
            List<BudModifyTaskRes> list3 = new List<BudModifyTaskRes>();
            using (List<string>.Enumerator enumerator = modifyTasResIds.GetEnumerator())
            {
                Func<BudModifyTaskRes, bool> predicate = null;
                string id;
                while (enumerator.MoveNext())
                {
                    id = enumerator.Current;
                    BudModifyTaskRes item = new BudModifyTaskRes {
                        ModifyTaskResId = Guid.NewGuid().ToString(),
                        ModifyTaskId = taskId,
                        ModifyId = null,
                        ResourceId = id
                    };
                    decimal num = 0M;
                    decimal num2 = 0M;
                    decimal num3 = 0M;
                    if (predicate == null)
                    {
                        predicate = mtr => mtr.ResourceId == id;
                    }
                    foreach (BudModifyTaskRes res2 in listModifyTaskRes.Where<BudModifyTaskRes>(predicate).ToList<BudModifyTaskRes>())
                    {
                        num += res2.ResourceQuantity;
                        num2 += res2.ResourceQuantity * res2.ResourcePrice;
                    }
                    if (num != 0M)
                    {
                        num3 = num2 / num;
                    }
                    item.ResourcePrice = num3;
                    item.ResourceQuantity = num;
                    list3.Add(item);
                }
            }
            return list3;
        }

        public List<BudModifyTaskRes> GetModifyTaskRes(string modifyTaskId)
        {
            List<BudModifyTaskRes> list = new List<BudModifyTaskRes>();
            list = (from mtr in this
                where mtr.ModifyTaskId == modifyTaskId
                select mtr).ToList<BudModifyTaskRes>();
            List<BudModifyTaskRes> source = this.GetInTaskRes(modifyTaskId);
            using (List<BudModifyTaskRes>.Enumerator enumerator = list.GetEnumerator())
            {
                Func<BudModifyTaskRes, bool> predicate = null;
                BudModifyTaskRes taskRes;
                while (enumerator.MoveNext())
                {
                    taskRes = enumerator.Current;
                    if (predicate == null)
                    {
                        predicate = inTaskRes => inTaskRes.ResourceId == taskRes.ResourceId;
                    }
                    BudModifyTaskRes item = source.Where<BudModifyTaskRes>(predicate).FirstOrDefault<BudModifyTaskRes>();
                    if (item != null)
                    {
                        decimal num = 0M;
                        num = taskRes.ResourceQuantity + item.ResourceQuantity;
                        decimal num2 = (taskRes.ResourcePrice * taskRes.ResourceQuantity) + (item.ResourcePrice * item.ResourceQuantity);
                        decimal num3 = 0M;
                        if (num != 0M)
                        {
                            num3 = num2 / num;
                        }
                        taskRes.ResourceQuantity = num;
                        taskRes.ResourcePrice = num3;
                        source.Remove(item);
                    }
                }
            }
            foreach (BudModifyTaskRes res2 in source)
            {
                BudModifyTaskRes res3 = new BudModifyTaskRes {
                    ModifyTaskResId = res2.ModifyTaskResId,
                    ModifyTaskId = modifyTaskId,
                    ModifyId = null,
                    ResourceId = res2.ResourceId,
                    ResourcePrice = res2.ResourcePrice,
                    ResourceQuantity = res2.ResourceQuantity
                };
                list.Add(res3);
            }
            return list;
        }

        public List<string> GetModifyTasResIds(List<BudModifyTaskRes> listModifyTaskRes)
        {
            List<string> list = new List<string>();
            foreach (BudModifyTaskRes res in listModifyTaskRes)
            {
                string resourceId = res.ResourceId;
                if (!list.Contains(resourceId))
                {
                    list.Add(resourceId);
                }
            }
            return list;
        }
    }
}

