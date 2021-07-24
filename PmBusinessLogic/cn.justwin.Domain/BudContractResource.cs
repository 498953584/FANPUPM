namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BudContractResource
    {
        private BudContractResource()
        {
        }

        public static void Add(BudContractResource conRes)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_ContractTask task = (from m in entities.Bud_ContractTask
                    where m.TaskId == conRes.TaskId
                    select m).FirstOrDefault<Bud_ContractTask>();
                Res_Resource resource = (from m in entities.Res_Resource
                    where m.ResourceId == conRes.ResourceId
                    select m).FirstOrDefault<Res_Resource>();
                Bud_ContractResource resource2 = new Bud_ContractResource {
                    TaskResourceId = conRes.Id,
                    ResourcePrice = new decimal?(conRes.ResPrice),
                    ResourceQuantity = new decimal?(conRes.ResQuantity),
                    InputDate = conRes.InputDate,
                    InputUser = conRes.InputUser,
                    Bud_ContractTask = task,
                    Res_Resource = resource
                };
                entities.AddToBud_ContractResource(resource2);
                entities.SaveChanges();
            }
        }

        public static BudContractResource Create(string id, string taskId, string resourceId, decimal resPrice, decimal resQuantity, string inputUser, DateTime inputDate)
        {
            return new BudContractResource { Id = id, TaskId = taskId, ResourceId = resourceId, ResPrice = resPrice, ResQuantity = resQuantity, InputDate = inputDate, InputUser = inputUser };
        }

        public static decimal? GetRepeatResQuantity(string resId, string taskId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_ContractResource
                    where (m.Res_Resource.ResourceId == resId) && (m.Bud_ContractTask.TaskId == taskId)
                    select m.ResourceQuantity).FirstOrDefault<decimal?>();
            }
        }

        public static string GetResourcesInfoByTaskId(string taskId)
        {
            string str = string.Empty;
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var type in (from tr in entities.Bud_ContractResource
                    where tr.Bud_ContractTask.TaskId == taskId
                    select new { Id = tr.TaskResourceId, ResourceId = tr.Res_Resource.ResourceId, resourceCode = tr.Res_Resource.ResourceCode, resourceName = tr.Res_Resource.ResourceName, unit = tr.Res_Resource.Res_Unit.Name, specification = tr.Res_Resource.Specification, brand = tr.Res_Resource.Brand, modelNumber = tr.Res_Resource.ModelNumber, TechnicalParameter = tr.Res_Resource.TechnicalParameter, price = tr.ResourcePrice, number = tr.ResourceQuantity }).ToList())
                {
                    str = str + type.resourceCode + ",";
                    str = str + type.resourceName + ",";
                    if ((type.unit == "") || (type.unit == null))
                    {
                        str = str + " ,";
                    }
                    else
                    {
                        str = str + type.unit + ",";
                    }
                    str = str + type.specification + ",";
                    str = str + type.brand + ",";
                    str = str + type.modelNumber + ",";
                    str = str + type.TechnicalParameter + ",";
                    str = str + type.price + ",";
                    str = str + type.number + ",";
                    decimal? nullable4 = type.price * type.number;
                    str = str + decimal.Round(nullable4.HasValue ? nullable4.GetValueOrDefault() : 0M, 3) + "⊙";
                }
                return str;
            }
        }

        public static void UpdateResQuantity(string taskId, string resId, decimal quantity)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_ContractResource resource = (from m in entities.Bud_ContractResource
                    where (m.Res_Resource.ResourceId == resId) && (m.Bud_ContractTask.TaskId == taskId)
                    select m).FirstOrDefault<Bud_ContractResource>();
                if (resource != null)
                {
                    resource.ResourceQuantity = new decimal?(quantity);
                    entities.SaveChanges();
                }
            }
        }

        public string Id { get; set; }

        public DateTime InputDate { get; set; }

        public string InputUser { get; set; }

        public string ResourceId { get; set; }

        public decimal ResPrice { get; set; }

        public decimal ResQuantity { get; set; }

        public string TaskId { get; set; }
    }
}

