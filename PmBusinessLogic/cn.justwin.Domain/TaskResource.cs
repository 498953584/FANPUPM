namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class TaskResource
    {
        public static void AddResource(TaskResource taskResource)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                int? nullable = (from m in entities.Bud_Task
                    where m.PrjId == taskResource.PrjGuid
                    select m.Version).Max<int?>();
                if (!nullable.HasValue)
                {
                    nullable = 1;
                }
                Bud_TaskResource resource = new Bud_TaskResource {
                    TaskResourceId = taskResource.TaskReourceId,
                    Bud_Task = null,
                    Res_Resource = (from r in entities.Res_Resource
                        where r.ResourceId == taskResource.Resource.Id
                        select r).FirstOrDefault<Res_Resource>(),
                    ResourceQuantity = new decimal?(taskResource.Quantity),
                    ResourcePrice = new decimal?(taskResource.Price),
                    InputUser = taskResource.InputUser,
                    InputDate = taskResource.InputDate.Value,
                    PrjGuid = taskResource.PrjGuid,
                    Versions = nullable,
                    LossCoefficient = taskResource.LossCoefficient
                };
                entities.AddToBud_TaskResource(resource);
                entities.SaveChanges();
            }
        }

        public static void AddResource(List<TaskResource> resList, string prjId, int version)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                DelRes(entities, prjId, version);
                using (List<TaskResource>.Enumerator enumerator = resList.GetEnumerator())
                {
                    TaskResource taskResource;
                    while (enumerator.MoveNext())
                    {
                        taskResource = enumerator.Current;
                        Bud_TaskResource resource = new Bud_TaskResource {
                            TaskResourceId = Guid.NewGuid().ToString(),
                            Bud_Task = null,
                            Res_Resource = (from r in entities.Res_Resource
                                where r.ResourceId == taskResource.Resource.Id
                                select r).FirstOrDefault<Res_Resource>(),
                            ResourceQuantity = new decimal?(taskResource.Quantity),
                            ResourcePrice = new decimal?(taskResource.Price),
                            InputUser = taskResource.InputUser,
                            InputDate = taskResource.InputDate.Value,
                            PrjGuid = prjId,
                            Versions = new int?(version),
                            LossCoefficient = taskResource.LossCoefficient
                        };
                        entities.AddToBud_TaskResource(resource);
                    }
                }
                entities.SaveChanges();
            }
        }

        private static void DeleteResource(List<string> taskResIds, pm2Entities context)
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

        public static void DelRes(string prjId, int version)
        {
            try
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    DeleteResource((from m in entities.Bud_TaskResource
                        where (m.PrjGuid == prjId) && (m.Versions == version)
                        select m.TaskResourceId).ToList<string>(), entities);
                    entities.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("找不到要删除的资源！");
            }
        }

        private static void DelRes(pm2Entities context, string prjId, int version)
        {
            try
            {
                DeleteResource((from m in context.Bud_TaskResource
                    where (m.PrjGuid == prjId) && (m.Versions == version)
                    select m.TaskResourceId).ToList<string>(), context);
            }
            catch (Exception)
            {
                throw new Exception("找不到要删除的资源！");
            }
        }

        public static decimal? GetRepeatResQuantity(string taskId, string resId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_TaskResource
                    where (m.Res_Resource.ResourceId == resId) && (m.Bud_Task.TaskId == taskId)
                    select m.ResourceQuantity).FirstOrDefault<decimal?>();
            }
        }

        public static List<TaskResource> GetResByPrjVersion(string prjId, int version)
        {
            List<TaskResource> list = new List<TaskResource>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var type in (from m in entities.Bud_TaskResource
                    where (m.PrjGuid == prjId) && (m.Versions == version)
                    select new { TaskResourceId = m.TaskResourceId, TaskId = m.Bud_Task.TaskId, ResourceId = m.Res_Resource.ResourceId, ResourceQuantity = m.ResourceQuantity, ResourcePrice = m.ResourcePrice, PrjGuid = m.PrjGuid, Versions = m.Versions, InputDate = m.InputDate, InputUser = m.InputUser }).ToList())
                {
                    TaskResource item = new TaskResource {
                        TaskReourceId = type.TaskResourceId,
                        TaskId = type.TaskId,
                        Resource = cn.justwin.Domain.Resource.GetById(type.ResourceId),
                        Price = !type.ResourcePrice.HasValue ? 0M : type.ResourcePrice.Value,
                        Quantity = !type.ResourceQuantity.HasValue ? 0M : type.ResourceQuantity.Value,
                        PrjGuid = type.PrjGuid,
                        Versions = !type.Versions.HasValue ? 1 : type.Versions.Value,
                        InputDate = new DateTime?(type.InputDate),
                        InputUser = type.InputUser
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static decimal? GetResQuantity(string prjId, string resId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_TaskResource
                    where (m.Res_Resource.ResourceId == resId) && (m.PrjGuid == prjId)
                    select m.ResourceQuantity).FirstOrDefault<decimal?>();
            }
        }

        public static void UpdateResQuantity(string taskId, string resId, decimal quantity)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TaskResource resource = (from m in entities.Bud_TaskResource
                    where (m.Res_Resource.ResourceId == resId) && (m.Bud_Task.TaskId == taskId)
                    select m).FirstOrDefault<Bud_TaskResource>();
                if (resource != null)
                {
                    resource.ResourceQuantity = new decimal?(quantity);
                    entities.SaveChanges();
                }
            }
        }

        public static void UpdateResQuantityByPrjId(string prjId, string resId, decimal quantity)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                int? maxVersion = (from m in entities.Bud_Task
                    where m.PrjId == prjId
                    select m.Version).Max<int?>();
                if (!maxVersion.HasValue)
                {
                    maxVersion = 1;
                }
                Bud_TaskResource resource = (from m in entities.Bud_TaskResource
                    where ((m.Res_Resource.ResourceId == resId) && (m.PrjGuid == prjId)) && (m.Versions == maxVersion.Value)
                    select m).FirstOrDefault<Bud_TaskResource>();
                if (resource != null)
                {
                    resource.ResourceQuantity = new decimal?(quantity);
                    entities.SaveChanges();
                }
            }
        }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public decimal? LossCoefficient { get; set; }

        public decimal Price { get; set; }

        public string PrjGuid { get; set; }

        public decimal Quantity { get; set; }

        public cn.justwin.Domain.Resource Resource { get; set; }

        public string TaskId { get; set; }

        public string TaskReourceId { get; set; }

        public int Versions { get; set; }
    }
}

