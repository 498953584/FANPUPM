namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class BudTemplateItem
    {
        private static string priceTypeId = ConfigHelper.Get("BudgetPriceType");
        private IList<TaskResource> resources;
        private string typeName;

        private BudTemplateItem()
        {
        }

        public static void Add(BudTemplateItem budTemplateItem)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TemplateItem item = new Bud_TemplateItem {
                    TemplateItemId = budTemplateItem.Id,
                    ParentId = budTemplateItem.ParentId,
                    Bud_Template = entities.Bud_Template.FirstOrDefault<Bud_Template>(c => c.TemplateId == budTemplateItem.BudTemplate.Id),
                    ItemCode = budTemplateItem.Code,
                    ItemName = budTemplateItem.Name,
                    OrderNumber = budTemplateItem.OrderNumber,
                    Unit = budTemplateItem.Unit,
                    Quantity = budTemplateItem.Quantity,
                    UnitPrice = budTemplateItem.UnitPrice,
                    Note = budTemplateItem.Note,
                    FeatureDescription = budTemplateItem.FeatureDescription
                };
                entities.AddToBud_TemplateItem(item);
                entities.SaveChanges();
            }
        }

        public static void AddResource(BudTemplateItem budTempItem)
        {
            if ((budTempItem.Resources != null) && (budTempItem.Resources.Count > 0))
            {
                IList<TaskResource> resources = new List<TaskResource>();
                resources = budTempItem.Resources;
                using (pm2Entities entities = new pm2Entities())
                {
                    DeleteResource(budTempItem, entities);
                    using (IEnumerator<TaskResource> enumerator = resources.GetEnumerator())
                    {
                        TaskResource tempResource;
                        while (enumerator.MoveNext())
                        {
                            tempResource = enumerator.Current;
                            Bud_TemplateResource resource = new Bud_TemplateResource {
                                TemplateResourceId = Guid.NewGuid().ToString(),
                                Bud_TemplateItem = (from t in entities.Bud_TemplateItem
                                    where t.TemplateItemId == budTempItem.Id
                                    select t).FirstOrDefault<Bud_TemplateItem>(),
                                Res_Resource = (from r in entities.Res_Resource
                                    where r.ResourceId == tempResource.Resource.Id
                                    select r).FirstOrDefault<Res_Resource>(),
                                ResourceQuantity = new decimal?(tempResource.Quantity),
                                ResourcePrice = new decimal?(tempResource.Price),
                                InputUser = tempResource.InputUser,
                                LossCoefficient = tempResource.LossCoefficient
                            };
                            if (tempResource.InputDate.HasValue)
                            {
                                resource.InputDate = Convert.ToDateTime(tempResource.InputDate);
                            }
                            entities.AddToBud_TemplateResource(resource);
                        }
                    }
                    entities.SaveChanges();
                    return;
                }
            }
            using (pm2Entities entities2 = new pm2Entities())
            {
                DeleteResource(budTempItem, entities2);
                entities2.SaveChanges();
            }
        }

        public void AddResource(Resource resource, decimal quantity, decimal price, string inputUser, DateTime inputDate, string action, decimal lossCoefficient)
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
            TaskResource item = new TaskResource {
                Resource = resource,
                Quantity = quantity,
                Price = price,
                InputUser = inputUser,
                LossCoefficient = new decimal?(lossCoefficient),
                InputDate = new DateTime?(Convert.ToDateTime(inputDate))
            };
            this.Resources.Add(item);
        }

        public static BudTemplateItem Create(string id, string parentId, cn.justwin.Domain.BudTemplate budTemplate, string code, string name, string unit, decimal quantity, decimal? unitPrice, string note, string description)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("模板项ID", "模板项ID不能为空");
            }
            if (parentId == "")
            {
                parentId = null;
            }
            return new BudTemplateItem { Id = id, ParentId = parentId, BudTemplate = budTemplate, Code = code, Name = name, OrderNumber = GetNextOrderNumber(budTemplate.Id, parentId), Unit = unit, Quantity = quantity, UnitPrice = unitPrice, Note = note, FeatureDescription = description };
        }

        public static void Delete(string templateId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Delete((from m in entities.Bud_TemplateItem
                    where m.Bud_Template.TemplateId == templateId
                    select m.TemplateItemId).ToList<string>());
            }
        }

        public static void Delete(List<string> idList)
        {
            try
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
                    entities.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("当前模板项不是最底层的模板项");
            }
        }

        private static void Delete(string id, pm2Entities context)
        {
            Bud_TemplateItem entity = context.Bud_TemplateItem.FirstOrDefault<Bud_TemplateItem>(c => c.TemplateItemId == id);
            if (entity == null)
            {
                throw new ApplicationException("找不到需要删除的模板项");
            }
            if (context.Bud_TemplateItem.Count<Bud_TemplateItem>(c => (c.ParentId == id)) > 0)
            {
                throw new ApplicationException("当前模板项不是最底层的模板项");
            }
            DeleteResourceByItemId(id);
            context.DeleteObject(entity);
        }

        private static void Delete(List<string> idList, pm2Entities context)
        {
            bool flag = true;
            IList<Bud_TemplateItem> list = GetBud_TemplateItemList(idList, context);
            foreach (Bud_TemplateItem item in list)
            {
                if (!IsAllowDele(idList, item.TemplateItemId))
                {
                    flag = false;
                    break;
                }
            }
            if (!flag)
            {
                throw new ApplicationException("当前模板项不是最底层的模板项");
            }
            foreach (Bud_TemplateItem item2 in list)
            {
                DeleteResourceByItemId(item2.TemplateItemId);
                context.DeleteObject(item2);
            }
        }

        public static void DeleteResource(List<string> tempResIds)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (tempResIds.Count == 1)
                {
                    DeleteResource(tempResIds[0], entities);
                }
                else
                {
                    DeleteResource(tempResIds, entities);
                }
                entities.SaveChanges();
            }
        }

        private static void DeleteResource(BudTemplateItem budTempItem, pm2Entities context)
        {
            budTempItem = GetById(budTempItem.Id, budTempItem.BudTemplate.Id);
            using (IEnumerator<TaskResource> enumerator = budTempItem.Resources.GetEnumerator())
            {
                TaskResource tempResource;
                while (enumerator.MoveNext())
                {
                    tempResource = enumerator.Current;
                    Bud_TemplateResource entity = (from tr in context.Bud_TemplateResource
                        where (tr.Res_Resource.ResourceId == tempResource.Resource.Id) && (tr.Bud_TemplateItem.TemplateItemId == budTempItem.Id)
                        select tr).FirstOrDefault<Bud_TemplateResource>();
                    if (entity != null)
                    {
                        context.DeleteObject(entity);
                    }
                }
            }
        }

        private static void DeleteResource(string tempResId, pm2Entities context)
        {
            Bud_TemplateResource entity = context.Bud_TemplateResource.FirstOrDefault<Bud_TemplateResource>(c => c.TemplateResourceId == tempResId);
            if (entity == null)
            {
                throw new Exception("找不到要删除的资源！");
            }
            context.DeleteObject(entity);
        }

        private static void DeleteResource(List<string> tempResIds, pm2Entities context)
        {
            try
            {
                using (List<string>.Enumerator enumerator = tempResIds.GetEnumerator())
                {
                    string tempResId;
                    while (enumerator.MoveNext())
                    {
                        tempResId = enumerator.Current;
                        Bud_TemplateResource entity = context.Bud_TemplateResource.FirstOrDefault<Bud_TemplateResource>(c => c.TemplateResourceId == tempResId);
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

        private static void DeleteResourceByItemId(string itemId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                DeleteResource((from tr in entities.Bud_TemplateResource
                    where tr.Bud_TemplateItem.TemplateItemId == itemId
                    select tr.TemplateResourceId).ToList<string>());
            }
        }

        public static IList<BudTemplateItem> GetAll()
        {
            IList<BudTemplateItem> list = new List<BudTemplateItem>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var typed in (from bti in entities.Bud_TemplateItem select new { TemplateItemId = bti.TemplateItemId, ItemName = bti.ItemName, ItemCode = bti.ItemCode, Unit = bti.Unit, Note = bti.Note, OrderNumber = bti.OrderNumber, UnitPrice = bti.UnitPrice, Quantity = bti.Quantity, TemplateId = bti.Bud_Template.TemplateId, parentId = bti.ParentId }).ToList())
                {
                    BudTemplateItem item = new BudTemplateItem {
                        Id = typed.TemplateItemId,
                        ParentId = typed.parentId,
                        BudTemplate = cn.justwin.Domain.BudTemplate.GetById(typed.TemplateId),
                        Code = typed.ItemCode,
                        Name = typed.ItemName,
                        OrderNumber = typed.OrderNumber,
                        Unit = typed.Unit,
                        Quantity = typed.Quantity,
                        Note = typed.Note
                    };
                    decimal total = 0M;
                    GetQuantityTotal(item.Id, item.BudTemplate.Id, ref total);
                    item.Total = new decimal?(decimal.Round(total, 3));
                    if (item.Quantity == 0M)
                    {
                        item.UnitPrice = new decimal?(decimal.Round(0M, 3));
                    }
                    else
                    {
                        item.UnitPrice = new decimal?(decimal.Round(total / item.Quantity, 3));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public static IList<BudTemplateItem> GetAll(string templateId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from bti in entities.Bud_TemplateItem
                    where bti.Bud_Template.TemplateId == templateId
                    orderby bti.OrderNumber
                    select new BudTemplateItem { Id = bti.TemplateItemId, typeName = bti.Bud_Template.TemplateName, Code = bti.ItemCode, Name = bti.ItemName, OrderNumber = bti.OrderNumber }).ToList<BudTemplateItem>();
            }
        }

        public static IList<Bud_TemplateItem> GetBud_TemplateItemList(List<string> idList, pm2Entities context)
        {
            IList<Bud_TemplateItem> list = new List<Bud_TemplateItem>();
            using (List<string>.Enumerator enumerator = idList.GetEnumerator())
            {
                string id;
                while (enumerator.MoveNext())
                {
                    id = enumerator.Current;
                    Bud_TemplateItem item = context.Bud_TemplateItem.FirstOrDefault<Bud_TemplateItem>(c => c.TemplateItemId == id);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static IList<BudTemplateItem> GetByCode(string code, string templateId)
        {
            IList<BudTemplateItem> list = new List<BudTemplateItem>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var typee in (from bti in entities.Bud_TemplateItem
                    where (bti.ItemCode == code) && (bti.Bud_Template.TemplateId == templateId)
                    select new { id = bti.TemplateItemId, parentId = bti.ParentId, tempId = bti.Bud_Template.TemplateId, code = bti.ItemCode, name = bti.ItemName, orederNum = bti.OrderNumber, unit = bti.Unit, quntity = bti.Quantity, unitPrice = bti.UnitPrice, note = bti.Note }).ToList())
                {
                    BudTemplateItem item = new BudTemplateItem {
                        Id = typee.id,
                        ParentId = typee.parentId,
                        BudTemplate = cn.justwin.Domain.BudTemplate.GetById(typee.tempId),
                        Code = typee.code,
                        Name = typee.name,
                        OrderNumber = typee.orederNum,
                        Unit = typee.unit,
                        Quantity = typee.quntity,
                        UnitPrice = typee.unitPrice,
                        Note = typee.note
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static BudTemplateItem GetById(string id, string templateId)
        {
            BudTemplateItem item = new BudTemplateItem();
            using (pm2Entities entities = new pm2Entities())
            {
                var typec = (from bti in entities.Bud_TemplateItem
                    where (bti.TemplateItemId == id) && (bti.Bud_Template.TemplateId == templateId)
                    select new { id = bti.TemplateItemId, parentId = bti.ParentId, tempId = bti.Bud_Template.TemplateId, code = bti.ItemCode, name = bti.ItemName, orederNum = bti.OrderNumber, unit = bti.Unit, quntity = bti.Quantity, unitPrice = bti.UnitPrice, note = bti.Note, featureDescription = bti.FeatureDescription }).FirstOrDefault();
                if (typec != null)
                {
                    item.Id = id;
                    item.ParentId = typec.parentId;
                    item.BudTemplate = cn.justwin.Domain.BudTemplate.GetById(typec.tempId);
                    item.Code = typec.code;
                    item.Name = typec.name;
                    item.OrderNumber = typec.orederNum;
                    item.Unit = typec.unit;
                    item.Quantity = typec.quntity;
                    item.UnitPrice = typec.unitPrice;
                    item.Note = typec.note;
                    item.FeatureDescription = typec.featureDescription;
                    return item;
                }
                return null;
            }
        }

        private static IList<BudTemplateItem> GetByParentId(string parentId)
        {
            IList<BudTemplateItem> list = new List<BudTemplateItem>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from bti in entities.Bud_TemplateItem
                    where bti.ParentId == parentId
                    select new BudTemplateItem { Id = bti.TemplateItemId }).ToList<BudTemplateItem>();
            }
        }

        public static string GetByTemplateId(string templateId)
        {
            string str = "";
            using (pm2Entities entities = new pm2Entities())
            {
                var source = from bti in entities.Bud_TemplateItem
                    where (bti.Bud_Template.TemplateId == templateId) && (bti.ParentId == null)
                    orderby bti.OrderNumber
                    select new { OrderNumber = bti.OrderNumber };
                if (source != null)
                {
                    str = source.Max(bti => bti.OrderNumber);
                }
            }
            return str;
        }

        public static DataTable GetChangeTaskNo(string TemplateId, string compareId, string selectedId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n\t\t\t\tSELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber ASC) AS No,TemplateItemId \r\n\t\t\t\tFROM Bud_TemplateItem WHERE TemplateId = @TemplateId) AS T1 \r\n\t\t\t\tWHERE TemplateItemId =@comId OR TemplateItemId=@selId");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TemplateId", TemplateId), new SqlParameter("@comId", compareId), new SqlParameter("@selId", selectedId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int GetCount(string tempItemId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return entities.Bud_TemplateResource.Count<Bud_TemplateResource>(c => (c.Bud_TemplateItem.TemplateItemId == tempItemId));
            }
        }

        public static int GetItemsCount(string templateId)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                throw new ArgumentNullException("模板Id", "不能为空!");
            }
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_TemplateItem
                    where m.Bud_Template.TemplateId == templateId
                    select m.TemplateItemId).ToList<string>().Count<string>();
            }
        }

        public static int GetMaxLayer()
        {
            int num = 0;
            using (pm2Entities entities = new pm2Entities())
            {
                string str = (from ti in entities.Bud_TemplateItem
                    orderby ti.OrderNumber.Length descending
                    select ti.OrderNumber).FirstOrDefault<string>();
                if (str.Length != 0)
                {
                    num = str.Length / 3;
                }
            }
            return num;
        }

        private static string GetNewOrderNumber(string start, string oldOrderNubmer, int length)
        {
            return (start + oldOrderNubmer.Remove(0, length));
        }

        public static string GetNextOrderNumber(string tempId, string id)
        {
            string str = "";
            if (string.IsNullOrEmpty(id))
            {
                int num;
                int.TryParse(GetByTemplateId(tempId), out num);
                string str3 = (num + 1).ToString();
                if (str3.Length == 1)
                {
                    str = "00" + str3;
                }
                if (str3.Length == 2)
                {
                    str = "0" + str3;
                }
                return str;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                List<Bud_TemplateItem> source = (from m in entities.Bud_TemplateItem
                    where m.ParentId == id
                    select m).ToList<Bud_TemplateItem>();
                if (source.Count == 0)
                {
                    return ((from m in entities.Bud_TemplateItem
                        where m.TemplateItemId == id
                        select m.OrderNumber).FirstOrDefault<string>() + "001");
                }
                string str5 = source.Max<Bud_TemplateItem, string>(m => m.OrderNumber);
                string str6 = str5.Substring(0, str5.Length - 3);
                string str8 = (Convert.ToInt32(str5.Substring(str5.Length - 3, 3)) + 1).ToString();
                if (str8.Length == 1)
                {
                    str8 = "00" + str8;
                }
                if (str8.Length == 2)
                {
                    str8 = "0" + str8;
                }
                return (str6 + str8);
            }
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
                        string str = (from m in entities.Bud_TemplateItem
                            where m.TemplateItemId == id
                            select m.OrderNumber).FirstOrDefault<string>();
                        if (string.IsNullOrEmpty(str))
                        {
                            throw new Exception("要保存为模板的任务节点已不存在，请重新选择!!!");
                        }
                        list.Add(str);
                    }
                }
            }
            return list;
        }

        private static void GetQuantityTotal(string itemId, string tempId, ref decimal total)
        {
            if (IsLeafNode(itemId))
            {
                GetTaskQuantityTotal(itemId, tempId, ref total);
            }
            else
            {
                IList<BudTemplateItem> byParentId = GetByParentId(itemId);
                if (byParentId.Count > 0)
                {
                    foreach (BudTemplateItem item in byParentId)
                    {
                        GetQuantityTotal(item.Id, tempId, ref total);
                    }
                }
            }
        }

        public static DataTable GetResourcesByItemId(string itemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TemplateResourceId as Id,Bud_TemplateResource.ResourceId,ResourceCode,ResourceName,Res_Unit.[Name] as Unit,Res_Resource.Specification,ResourcePrice as price, ISNULL(LossCoefficient,1.00) LossCoefficient,");
            builder.Append("Res_Resource.Brand,ModelNumber,TechnicalParameter, ");
            builder.Append("ResourceQuantity as number from Bud_TemplateResource INNER JOIN Res_Resource ON Bud_TemplateResource.ResourceId=Res_Resource.ResourceId LEFT JOIN Res_Unit ON Unit=UnitId where TemplateItemId=@TemplateId");
            SqlParameter parameter = new SqlParameter("@TemplateId", SqlDbType.NVarChar, 0x40) {
                Value = itemId
            };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static IList<TaskResource> GetResourcesByTempItemId(string tempId)
        {
            List<TaskResource> list = new List<TaskResource>();
            using (pm2Entities entities = new pm2Entities())
            {
                var list2 = (from m in entities.Bud_TemplateResource
                    where m.Bud_TemplateItem.TemplateItemId == tempId
                    select new { ResourceQuantity = m.ResourceQuantity, ResourcePrice = m.ResourcePrice, ResourceId = m.Res_Resource.ResourceId }).ToList();
                if (list2.Count <= 0)
                {
                    return list;
                }
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
            return list;
        }

        private static void GetTaskQuantityTotal(string itemId, string tempId, ref decimal total)
        {
            if (GetById(itemId, tempId) != null)
            {
                IList<TaskResource> resourcesByTempItemId = GetResourcesByTempItemId(itemId);
                if (resourcesByTempItemId.Count > 0)
                {
                    foreach (TaskResource resource in resourcesByTempItemId)
                    {
                        decimal quantity = resource.Quantity;
                        decimal price = resource.Price;
                        total += quantity * price;
                    }
                }
            }
        }

        public static List<string> GetTemplateItemIds(string templateId)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_TemplateItem
                    where m.Bud_Template.TemplateId == templateId
                    select m.TemplateItemId).ToList<string>();
            }
        }

        private static bool IsAllowDele(List<string> idList, string currentId)
        {
            IList<BudTemplateItem> list = new List<BudTemplateItem>();
            foreach (BudTemplateItem item in GetByParentId(currentId))
            {
                if (!idList.Contains(item.Id))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsLeafNode(string id)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_TemplateItem
                    where m.ParentId == id
                    select m).FirstOrDefault<Bud_TemplateItem>() == null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static void SaveTemplate(List<string> lsttaskIds, string templateId, string saveNodeId, string inputUser)
        {
            if (!string.IsNullOrEmpty(saveNodeId) && (((GetById(saveNodeId, templateId) == null) ? 0 : 1) == 0))
            {
                throw new Exception("你选择的根节点不存在,请刷新查看!");
            }
            bool flag2 = true;
            List<string> orderNumberList = new List<string>();
            orderNumberList = BudTask.GetOrderNumberById(lsttaskIds);
            if (lsttaskIds.Count > 1)
            {
                flag2 = BudTask.IsStructured(orderNumberList);
            }
            List<BudTask> list2 = null;
            int length = 0;
            string start = string.Empty;
            if (flag2)
            {
                list2 = BudTask.ChangeId(lsttaskIds);
                start = GetNextOrderNumber(templateId, saveNodeId);
                length = orderNumberList.Min<string>((System.Func<string, int>) (m => m.Length));
            }
            int num2 = 0;
            foreach (string str2 in lsttaskIds)
            {
                BudTask task = null;
                if (flag2)
                {
                    task = list2[num2];
                }
                else
                {
                    task = BudTask.GetById(str2);
                    task.Id = Guid.NewGuid().ToString();
                }
                string id = task.Id;
                string parentId = string.Empty;
                if (flag2)
                {
                    parentId = task.ParentId;
                }
                else
                {
                    parentId = saveNodeId;
                }
                cn.justwin.Domain.BudTemplate byId = cn.justwin.Domain.BudTemplate.GetById(templateId);
                BudTemplateItem budTemplateItem = Create(id, parentId, byId, task.Code, task.Name, task.Unit, task.Quantity, task.UnitPrice, task.Note, "");
                if (flag2)
                {
                    if (task.OrderNumber.Length == length)
                    {
                        start = GetNextOrderNumber(templateId, saveNodeId);
                        budTemplateItem.ParentId = (saveNodeId == "") ? null : saveNodeId;
                    }
                    budTemplateItem.OrderNumber = GetNewOrderNumber(start, task.OrderNumber, length);
                }
                Add(budTemplateItem);
                List<TaskResource> list3 = BudTask.GetResourcesByTaskId(str2).ToList<TaskResource>();
                if (list3.Count > 0)
                {
                    foreach (TaskResource resource in list3)
                    {
                        budTemplateItem.AddResource(resource.Resource, budTemplateItem.Quantity, resource.Price, inputUser, DateTime.Now, "add", 0M);
                        AddResource(budTemplateItem);
                    }
                }
                num2++;
            }
        }

        public static IList<BudTemplateItem> Select(string templateId)
        {
            List<BudTemplateItem> list = new List<BudTemplateItem>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select tempItem.TemplateItemId,tempItem.ParentId,tempItem.ItemCode,tempItem.ItemName,tempItem.OrderNumber,");
            builder.Append("tempItem.Unit,ISNULL(tempItem.Quantity,0.000) AS Quantity,tempItem.Note,ISNULL(tempRes.Total,0.000) AS Total from Bud_TemplateItem as tempItem ");
            builder.Append("left join (select TemplateItemId,sum(ResourceQuantity*ResourcePrice) total from Bud_TemplateResource ");
            builder.Append("where templateItemId in (select TemplateItemId from Bud_TemplateItem ");
            builder.Append("where templateId=@templateId) group by TemplateItemId) as tempRes on ");
            builder.Append("tempItem.TemplateItemId=tempRes.TemplateItemId where tempItem.TemplateId=@templateId ");
            builder.Append("order by OrderNumber desc");
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@templateId", templateId) }))
            {
                while (reader.Read())
                {
                    BudTemplateItem item = new BudTemplateItem {
                        Id = reader["TemplateItemId"].ToString(),
                        ParentId = reader["ParentId"].ToString(),
                        Code = reader["ItemCode"].ToString(),
                        Name = reader["ItemName"].ToString(),
                        OrderNumber = reader["OrderNumber"].ToString(),
                        Unit = reader["Unit"].ToString(),
                        Quantity = decimal.Round(Convert.ToDecimal(reader["Quantity"]), 3),
                        Note = reader["Note"].ToString()
                    };
                    if (IsLeafNode(item.Id))
                    {
                        item.Total = new decimal?(decimal.Round(Convert.ToDecimal(reader["Total"]), 3));
                    }
                    else
                    {
                        decimal num = decimal.Round(0M, 3);
                        foreach (BudTemplateItem item2 in list)
                        {
                            if (item2.ParentId == item.Id)
                            {
                                num += Convert.ToDecimal(item2.Total);
                            }
                        }
                        item.Total = new decimal?(num);
                    }
                    if (item.Quantity == 0M)
                    {
                        item.UnitPrice = new decimal?(decimal.Round(0M, 3));
                    }
                    else
                    {
                        item.UnitPrice = new decimal?(decimal.Round(Convert.ToDecimal(item.Total) / item.Quantity, 3));
                    }
                    list.Add(item);
                }
            }
            list.Reverse();
            return list;
        }

        public static DataTable showMaterialListForAdd(string IdList, string tempId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TemplateResourceId as Id,Res_Resource.ResourceId,ResourceCode,ResourceName,[Name] AS Unit,Specification, ISNULL(PriceValue,0.000) AS Price, ");
            builder.Append(" Res_Resource.Brand,ModelNumber,TechnicalParameter, ISNULL(LossCoefficient,1.00) LossCoefficient,");
            builder.Append(" ISNULL(resourceQuantity,0.000) AS number FROM Res_Resource ");
            builder.Append("LEFT JOIN Bud_TemplateResource ON Res_Resource.ResourceId=Bud_TemplateResource.ResourceId and templateItemId=@tempId ");
            builder.Append("LEFT JOIN Res_Unit ON UnitId = Unit ");
            builder.Append("LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId AND PriceTypeId = @priceType ");
            builder.Append("LEFT JOIN Res_PriceType ON Res_PriceType.PriceTypeId = Res_Price.PriceTypeId ");
            builder.Append("WHERE Res_Resource.ResourceId IN( ");
            builder.Append(IdList);
            builder.Append(")");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@priceType", SqlDbType.NVarChar, 0x40), new SqlParameter("@tempId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = priceTypeId;
            commandParameters[1].Value = tempId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void Update(BudTemplateItem budTemplateItem)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TemplateItem item = entities.Bud_TemplateItem.FirstOrDefault<Bud_TemplateItem>(c => c.TemplateItemId == budTemplateItem.Id);
                if (item == null)
                {
                    throw new ApplicationException("找不到需要修改的模板项");
                }
                item.ParentId = budTemplateItem.ParentId;
                item.Bud_Template = entities.Bud_Template.FirstOrDefault<Bud_Template>(c => c.TemplateId == budTemplateItem.BudTemplate.Id);
                item.ItemCode = budTemplateItem.Code;
                item.ItemName = budTemplateItem.Name;
                item.OrderNumber = budTemplateItem.OrderNumber;
                item.Unit = budTemplateItem.Unit;
                item.Quantity = budTemplateItem.Quantity;
                item.UnitPrice = budTemplateItem.UnitPrice;
                item.Note = budTemplateItem.Note;
                item.FeatureDescription = budTemplateItem.FeatureDescription;
                entities.SaveChanges();
            }
        }

        public static void UpdateChangeTask(string templeteItemId, string replaceOrderNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\nWITH Children AS\r\n(\r\nSELECT * FROM Bud_TemplateItem WHERE TemplateItemId=@templeteItemId\r\nUNION ALL\r\nSELECT TempItems.* FROM Bud_TemplateItem AS TempItems\r\nJOIN Children ON TempItems.ParentId=Children.TemplateItemId\r\n)\r\nUPDATE Bud_TemplateItem SET OrderNumber=@repOrderNumber+RIGHT(OrderNumber,(LEN(OrderNumber)-LEN(@repOrderNumber)))\r\nWHERE TemplateItemId IN( SELECT TemplateItemId FROM Children)\r\n");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@templeteItemId", templeteItemId), new SqlParameter("@repOrderNumber", replaceOrderNumber) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public cn.justwin.Domain.BudTemplate BudTemplate { get; set; }

        public string Code { get; set; }

        public string FeatureDescription { get; set; }

        public string Id { get; set; }

        public int Layer
        {
            get
            {
                return (this.OrderNumber.Length / 3);
            }
        }

        public string Name { get; set; }

        public string Note { get; set; }

        public string OrderNumber { get; set; }

        public string ParentId { get; set; }

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
                        foreach (var type in from r in entities.Bud_TemplateResource
                            where r.Bud_TemplateItem.TemplateItemId == this.Id
                            select new { ResourceId = r.Res_Resource.ResourceId, ResourceQuantity = r.ResourceQuantity, ResourcePrice = r.ResourcePrice, InputUser = r.InputUser, InputDate = r.InputDate })
                        {
                            TaskResource item = new TaskResource {
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

        public decimal? Total { get; set; }

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
    }
}

