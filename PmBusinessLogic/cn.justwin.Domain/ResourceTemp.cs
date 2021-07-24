namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ResourceTemp
    {
        private ResourceTemp()
        {
        }

        public static void Add(ResourceTemp resTemp)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Resource resource = (from m in entities.Res_Resource
                    where m.ResourceId == resTemp.ResourceId
                    select m).FirstOrDefault<Res_Resource>();
                Bud_Task task = null;
                if (resTemp.BudTask != null)
                {
                    task = (from m in entities.Bud_Task
                        where m.TaskId == resTemp.BudTask.Id
                        select m).FirstOrDefault<Bud_Task>();
                }
                Res_ResourceTemp temp = new Res_ResourceTemp {
                    Id = Guid.NewGuid().ToString(),
                    Amount = resTemp.Amount,
                    PrjId = resTemp.PrjId,
                    Quantity = resTemp.Quantity,
                    UnitPrice = resTemp.UnitPrice,
                    ResourceCode = resTemp.ResourceCode,
                    ResourceName = resTemp.ResourceName,
                    Bud_Task = task,
                    Res_Resource = resource
                };
                entities.AddToRes_ResourceTemp(temp);
                entities.SaveChanges();
            }
        }

        public static void Clear()
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Res_ResourceTemp temp in entities.Res_ResourceTemp)
                {
                    entities.DeleteObject(temp);
                }
                entities.SaveChanges();
            }
        }

        public static ResourceTemp Create(string resourceId, string taskId, string resourceCode, string resourceName, decimal? unitPrice, decimal? quanity, decimal? amount, string prjId)
        {
            return new ResourceTemp { ResourceId = resourceId, BudTask = cn.justwin.Domain.BudTask.GetById(taskId), ResourceCode = resourceCode, ResourceName = resourceName, UnitPrice = unitPrice, Quantity = quanity, Amount = amount, PrjId = prjId };
        }

        public static void DelByPrjId(string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Res_ResourceTemp temp in (from m in entities.Res_ResourceTemp
                    where m.PrjId == prjId
                    select m).ToList<Res_ResourceTemp>())
                {
                    entities.DeleteObject(temp);
                }
                entities.SaveChanges();
            }
        }

        public static void Delete(ResourceTemp resTemp, string InputUser, string isWBSRelevance)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (isWBSRelevance == "1")
                {
                    Bud_TaskResource resource = (from tr in entities.Bud_TaskResource
                        where (tr.Res_Resource.ResourceId == resTemp.ResourceId) && (tr.Bud_Task.TaskId == resTemp.BudTask.Id)
                        select tr).FirstOrDefault<Bud_TaskResource>();
                    if (resource == null)
                    {
                        Bud_TaskResource resource2 = new Bud_TaskResource {
                            TaskResourceId = Guid.NewGuid().ToString(),
                            Bud_Task = (from t in entities.Bud_Task
                                where t.TaskId == resTemp.BudTask.Id
                                select t).FirstOrDefault<Bud_Task>(),
                            Res_Resource = (from r in entities.Res_Resource
                                where r.ResourceId == resTemp.ResourceId
                                select r).FirstOrDefault<Res_Resource>(),
                            ResourceQuantity = resTemp.Quantity,
                            ResourcePrice = resTemp.UnitPrice,
                            InputUser = InputUser,
                            InputDate = DateTime.Now,
                            PrjGuid = resTemp.PrjId,
                            Versions = 1,
                            LossCoefficient = 1
                        };
                        entities.AddToBud_TaskResource(resource2);
                    }
                    else
                    {
                        resource.ResourceQuantity += resTemp.Quantity;
                    }
                }
                else
                {
                    Bud_TaskResource resource3 = (from tr in entities.Bud_TaskResource
                        where (tr.Res_Resource.ResourceId == resTemp.ResourceId) && (tr.PrjGuid == resTemp.PrjId)
                        select tr).FirstOrDefault<Bud_TaskResource>();
                    if (resource3 == null)
                    {
                        int? nullable = (from m in entities.Bud_Task
                            where m.PrjId == resTemp.PrjId
                            select m.Version).Max<int?>();
                        if (!nullable.HasValue)
                        {
                            nullable = 1;
                        }
                        Bud_TaskResource resource4 = new Bud_TaskResource {
                            TaskResourceId = Guid.NewGuid().ToString(),
                            Bud_Task = null,
                            Res_Resource = (from r in entities.Res_Resource
                                where r.ResourceId == resTemp.ResourceId
                                select r).FirstOrDefault<Res_Resource>(),
                            ResourceQuantity = resTemp.Quantity,
                            ResourcePrice = resTemp.UnitPrice,
                            InputUser = InputUser,
                            InputDate = DateTime.Now,
                            PrjGuid = resTemp.PrjId,
                            Versions = nullable
                        };
                        entities.AddToBud_TaskResource(resource4);
                    }
                    else
                    {
                        resource3.ResourceQuantity += resTemp.Quantity;
                    }
                }
                Res_ResourceTemp entity = (from rt in entities.Res_ResourceTemp
                    where rt.Id == resTemp.Id
                    select rt).FirstOrDefault<Res_ResourceTemp>();
                entities.DeleteObject(entity);
                entities.SaveChanges();
            }
        }

        public static void DelResMapping(List<string> ids)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<string>.Enumerator enumerator = ids.GetEnumerator())
                {
                    string id;
                    while (enumerator.MoveNext())
                    {
                        id = enumerator.Current;
                        Res_ResourceTemp entity = (from m in entities.Res_ResourceTemp
                            where m.Id == id
                            select m).FirstOrDefault<Res_ResourceTemp>();
                        if (entity != null)
                        {
                            entities.DeleteObject(entity);
                        }
                    }
                }
                entities.SaveChanges();
            }
        }

        public static ResourceTemp GetById(string id)
        {
            ResourceTemp temp = new ResourceTemp();
            using (pm2Entities entities = new pm2Entities())
            {
                var typeb = (from rt in entities.Res_ResourceTemp
                    where rt.Id == id
                    select new { id = rt.Id, resId = rt.Res_Resource.ResourceId, taskId = rt.Bud_Task.TaskId, resCode = rt.ResourceCode, resName = rt.ResourceName, unitPrice = rt.UnitPrice, quantity = rt.Quantity, amount = rt.Amount, prjId = rt.PrjId }).FirstOrDefault();
                if (typeb != null)
                {
                    temp.Id = typeb.id;
                    temp.ResourceId = typeb.resId;
                    temp.BudTask = cn.justwin.Domain.BudTask.GetById(typeb.taskId);
                    temp.ResourceCode = typeb.resCode;
                    temp.ResourceName = typeb.resName;
                    temp.UnitPrice = typeb.unitPrice;
                    temp.Quantity = typeb.quantity;
                    temp.Amount = typeb.amount;
                    temp.PrjId = typeb.prjId;
                }
            }
            return temp;
        }

        public static int GetResourceTemCount(string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_ResourceTemp
                    where m.PrjId == prjId
                    select m.Id).Count<string>();
            }
           
        }
        


        public static List<ResourceTemp> GetResourceTempByPrj(string prjId)
        {
            List<ResourceTemp> list = new List<ResourceTemp>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var typec in (from rt in entities.Res_ResourceTemp
                    where rt.PrjId == prjId
                    select new { Id = rt.Id, ResourceId = rt.Res_Resource.ResourceId, TaskId = rt.Bud_Task.TaskId, ResourceCode = rt.ResourceCode, ResourceName = rt.ResourceName, UnitPrice = rt.UnitPrice, Quantity = rt.Quantity, Amount = rt.Amount, PrjId = rt.PrjId }).ToList())
                {
                    ResourceTemp item = new ResourceTemp {
                        Id = typec.Id,
                        ResourceId = typec.ResourceId,
                        BudTask = cn.justwin.Domain.BudTask.GetById(typec.TaskId),
                        ResourceCode = typec.ResourceCode,
                        ResourceName = typec.ResourceName,
                        UnitPrice = typec.UnitPrice,
                        Quantity = typec.Quantity,
                        Amount = typec.Amount,
                        PrjId = typec.PrjId
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<ResourceTemp> GetResourceTempByPrj(string prjId, int pageSize, int pageNo)
        {
            List<ResourceTemp> list = new List<ResourceTemp>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var typec in (from rt in entities.Res_ResourceTemp
                    where rt.PrjId == prjId
                    orderby rt.Id
                    select new { Id = rt.Id, ResourceId = rt.Res_Resource.ResourceId, TaskId = rt.Bud_Task.TaskId, ResourceCode = rt.ResourceCode, ResourceName = rt.ResourceName, UnitPrice = rt.UnitPrice, Quantity = rt.Quantity, Amount = rt.Amount, PrjId = rt.PrjId }).Skip(((pageNo - 1) * pageSize)).Take(pageSize).ToList())
                {
                    ResourceTemp item = new ResourceTemp {
                        Id = typec.Id,
                        ResourceId = typec.ResourceId,
                        BudTask = cn.justwin.Domain.BudTask.GetById(typec.TaskId),
                        ResourceCode = typec.ResourceCode,
                        ResourceName = typec.ResourceName,
                        UnitPrice = typec.UnitPrice,
                        Quantity = typec.Quantity,
                        Amount = typec.Amount,
                        PrjId = typec.PrjId
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static void Update(ResourceTemp resTemp, string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_ResourceTemp temp = entities.Res_ResourceTemp.FirstOrDefault<Res_ResourceTemp>(c => c.Id == id);
                if (temp == null)
                {
                    throw new ApplicationException("找不到需要修改的模板项");
                }
                temp.Res_Resource = entities.Res_Resource.FirstOrDefault<Res_Resource>(c => c.ResourceId == resTemp.ResourceId);
                temp.Bud_Task = entities.Bud_Task.FirstOrDefault<Bud_Task>(c => c.TaskId == resTemp.BudTask.Id);
                entities.SaveChanges();
            }
        }

        public decimal? Amount { get; set; }

        public cn.justwin.Domain.BudTask BudTask { get; set; }

        public string Id { get; set; }

        public string PrjId { get; set; }

        public decimal? Quantity { get; set; }

        public string ResourceCode { get; set; }

        public string ResourceId { get; set; }

        public string ResourceName { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}

