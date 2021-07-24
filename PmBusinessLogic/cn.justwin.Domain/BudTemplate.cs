namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BudTemplate
    {
        private BudTemplate()
        {
        }

        public static void Add(BudTemplate budTemplate)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_Template entity = new Bud_Template {
                    TemplateId = budTemplate.Id,
                    TemplateCode = budTemplate.Code,
                    TemplateName = budTemplate.Name,
                    InputUser = budTemplate.InputUser
                };
                if (budTemplate.InputDate.HasValue)
                {
                    entity.InputDate = Convert.ToDateTime(budTemplate.InputDate);
                }
                entities.Bud_TemplateType.FirstOrDefault<Bud_TemplateType>(c => (c.TypeId == budTemplate.BudTempType.Id)).Bud_Template.Add(entity);
                entities.AddToBud_Template(entity);
                entities.SaveChanges();
            }
        }

        public static BudTemplate Create(string id, string code, string name, string inputUser, DateTime? inputDate, BudTemplateType budTempType)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("模板Id", "模板Id不能为空");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("模板编码", "模板编码不能为空");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("模板名称", "模板名称不能为空");
            }
            return new BudTemplate { Id = id, Code = code, Name = name, InputUser = inputUser, InputDate = inputDate, BudTempType = budTempType };
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
                entities.SaveChanges();
            }
        }

        public static void Delete(string id, pm2Entities context)
        {
            Bud_Template entity = context.Bud_Template.FirstOrDefault<Bud_Template>(c => c.TemplateId == id);
            if (entity == null)
            {
                throw new Exception("找不到需要删除的模板");
            }
            BudTemplateItem.Delete(id);
            context.DeleteObject(entity);
        }

        public static void Delete(List<string> idList, pm2Entities context)
        {
            foreach (string str in idList)
            {
                Delete(str, context);
            }
        }

        public static IList<BudTemplate> GetAll(string templateTypeId)
        {
            
            IList<BudTemplate> list = new List<BudTemplate>();
            using (pm2Entities entities = new pm2Entities())
            {
                var source = (from bt in entities.Bud_Template
                    orderby bt.InputDate descending
                    select new { TemplateId = bt.TemplateId, TemplateCode = bt.TemplateCode, TemplateName = bt.TemplateName, InputUser = bt.InputUser, InputDate = bt.InputDate, TypeId = bt.Bud_TemplateType.TypeId }).ToList();
                if (!string.IsNullOrEmpty(templateTypeId))
                {
                    source = source.Where(m => m.TypeId == templateTypeId).ToList();
                }
                foreach (var typea in source)
                {
                    BudTemplate item = new BudTemplate {
                        Id = typea.TemplateId,
                        Code = typea.TemplateCode,
                        Name = typea.TemplateName,
                        InputUser = typea.InputUser,
                        InputDate = new DateTime?(typea.InputDate),
                        BudTempType = BudTemplateType.GetById(typea.TypeId)
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<Bud_Template> GetBud_Template(List<string> idList, pm2Entities contxt)
        {
            List<Bud_Template> list = new List<Bud_Template>();
            using (List<string>.Enumerator enumerator = idList.GetEnumerator())
            {
                string id;
                while (enumerator.MoveNext())
                {
                    id = enumerator.Current;
                    Bud_Template item = contxt.Bud_Template.FirstOrDefault<Bud_Template>(c => c.TemplateId == id);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static BudTemplate GetById(string id)
        {
            BudTemplate template = new BudTemplate();
            using (pm2Entities entities = new pm2Entities())
            {
                var typea = (from bt in entities.Bud_Template
                    where bt.TemplateId == id
                    select new { TemplateId = bt.TemplateId, TemplateCode = bt.TemplateCode, TemplateName = bt.TemplateName, InputUser = bt.InputUser, InputDate = bt.InputDate, TypeId = bt.Bud_TemplateType.TypeId }).FirstOrDefault();
                if (typea != null)
                {
                    template.Id = id;
                    template.Code = typea.TemplateCode;
                    template.Name = typea.TemplateName;
                    template.InputUser = typea.InputUser;
                    template.InputDate = new DateTime?(typea.InputDate);
                    BudTemplateType byId = BudTemplateType.GetById(typea.TypeId);
                    template.BudTempType = byId;
                    return template;
                }
                return null;
            }
        }

        public static IList<BudTemplate> GetByName(string name)
        {
            IList<BudTemplate> list = new List<BudTemplate>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var typea in (from bt in entities.Bud_Template
                    where bt.TemplateName == name
                    select new { TemplateId = bt.TemplateId, TemplateCode = bt.TemplateCode, TemplateName = bt.TemplateName, InputUser = bt.InputUser, InputDate = bt.InputDate, TypeId = bt.Bud_TemplateType.TypeId }).ToList())
                {
                    BudTemplate item = new BudTemplate {
                        Id = typea.TemplateId,
                        Code = typea.TemplateCode,
                        Name = typea.TemplateName,
                        InputUser = typea.InputUser,
                        InputDate = new DateTime?(typea.InputDate),
                        BudTempType = BudTemplateType.GetById(typea.TypeId)
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static DataTable GetChildrenTemplateItem(string itemId)
        {
            string cmdText = "\r\n--DECLARE @ItemId nvarchar(200)\r\n--SET @ItemId = '0bfb677f-78e1-48e3-97a0-0408928286e8'\r\nDECLARE @TemplateId nvarchar(200)\r\nSELECT @TemplateId = TemplateId FROM Bud_TemplateItem WHERE TemplateItemId = @ItemId;\r\nWITH cteTemlate AS\r\n(\r\n\tSELECT t.TemplateItemId, t.OrderNumber, t.ParentId,\r\n\t\tSUM(tr.ResourceQuantity * tr.ResourcePrice) AS Total\r\n\tFROM Bud_TemplateItem AS t\r\n\tLEFT JOIN Bud_TemplateResource AS tr ON t.TemplateItemId = tr.TemplateItemId\r\n\tWHERE t.TemplateId = @TemplateId\r\n\tGROUP BY t.TemplateItemId, t.OrderNumber, t.ParentId\r\n), cteTemplate2 AS\r\n(\r\n\tSELECT t2.TemplateItemId, t2.OrderNumber, t2.ParentId, t2.ItemCode, \r\n\t\tt2.ItemName, t2.Unit, t2.Quantity, t2.Note,\r\n\t\t( SELECT SUM(cteTemlate.Total) \r\n\t\t\tFROM cteTemlate\r\n\t\t\tWHERE cteTemlate.OrderNumber LIKE t2.OrderNumber + '%'\r\n\t\t) AS Total, --小计（汇总）\r\n\t\t( SELECT COUNT(1) \r\n\t\t\tFROM Bud_TemplateItem AS t3 \r\n\t\t\tWHERE t3.ParentId = t2.TemplateItemId\r\n\t\t) AS SubCount --子节点的数量\r\n\tFROM Bud_TemplateItem AS t2\r\n\tINNER JOIN cteTemlate ON t2.TemplateItemId = cteTemlate.TemplateItemId\r\n),  cteType AS\r\n(\r\n\tSELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\tFROM XPM_Basic_CodeList AS cl\r\n\tINNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\tWHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n)\r\nSELECT * FROM\r\n(\r\n\tSELECT ROW_NUMBER() OVER(ORDER BY cteTemplate2.OrderNumber) AS No, cteTemplate2.ParentId, cteTemplate2.TemplateItemId,\r\n\t\tcteTemplate2.OrderNumber, cteTemplate2.ItemCode, cteTemplate2.ItemName, cteTemplate2.Unit,\r\n\t\tcteTemplate2.Quantity, cteTemplate2.Note, ISNULL(cteTemplate2.Total, 0) AS Total, cteTemplate2.SubCount,\r\n\t\tISNULL(cteTemplate2.Total / NULLIF(cteTemplate2.Quantity, 0), 0) AS UnitPrice,\r\n\t\tcteType.CodeNo, cteType.CodeName AS TypeName\r\n\tFROM cteTemplate2\r\n\tLEFT JOIN cteType ON LEN(cteTemplate2.OrderNumber) / 3 = cteType.CodeNo\r\n) AS a\r\nWHERE ParentId = @ItemId\r\nORDER BY OrderNumber ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ItemId", itemId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetTemplateItem(string templateId)
        {
            string cmdText = "\r\n--DECLARE @TemplateId nvarchar(200) \r\n--SET @TemplateId = '866fc03b-c4fd-4d22-ac6c-0e05382770a1';\r\nWITH cteTemlate AS\r\n(\r\n\tSELECT t.TemplateItemId, t.OrderNumber, t.ParentId,\r\n\t\tSUM(tr.ResourceQuantity * tr.ResourcePrice) AS Total,t.FeatureDescription\r\n\tFROM Bud_TemplateItem AS t\r\n\tLEFT JOIN Bud_TemplateResource AS tr ON t.TemplateItemId = tr.TemplateItemId\r\n\tWHERE t.TemplateId = @TemplateId\r\n\tGROUP BY t.TemplateItemId, t.OrderNumber, t.ParentId,t.FeatureDescription\r\n), cteTemplate2 AS\r\n(\r\n\tSELECT t2.TemplateItemId, t2.OrderNumber, t2.ParentId,\r\n\t\tt2.ItemCode, t2.ItemName, t2.Unit, t2.Quantity, t2.Note,t2.FeatureDescription,\r\n\t\t( SELECT SUM(cteTemlate.Total) \r\n\t\t\tFROM cteTemlate\r\n\t\t\tWHERE cteTemlate.OrderNumber LIKE t2.OrderNumber + '%'\r\n\t\t) AS Total, --小计（汇总）\r\n\t\t( SELECT COUNT(1) \r\n\t\t\tFROM Bud_TemplateItem AS t3 \r\n\t\t\tWHERE t3.ParentId = t2.TemplateItemId\r\n\t\t) AS SubCount --子节点的数量\r\n\tFROM Bud_TemplateItem AS t2\r\n\tINNER JOIN cteTemlate ON t2.TemplateItemId = cteTemlate.TemplateItemId\r\n),  cteType AS\r\n(\r\n\tSELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\tFROM XPM_Basic_CodeList AS cl\r\n\tINNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\tWHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n)\r\nSELECT * FROM\r\n(\r\n\tSELECT ROW_NUMBER() OVER(ORDER BY cteTemplate2.OrderNumber) AS No, \r\n\t\tcteTemplate2.ParentId, cteTemplate2.TemplateItemId,\r\n\t\tcteTemplate2.OrderNumber, cteTemplate2.ItemCode, cteTemplate2.ItemName,cteTemplate2.FeatureDescription, cteTemplate2.Unit,cteTemplate2.Quantity, cteTemplate2.Note, ISNULL(cteTemplate2.Total, 0) AS Total,cteTemplate2.SubCount,\r\n\t\tISNULL(cteTemplate2.Total / NULLIF(cteTemplate2.Quantity, 0), 0) AS UnitPrice,cteType.CodeNo, cteType.CodeName AS TypeName\r\n\tFROM cteTemplate2\r\n\tLEFT JOIN cteType ON LEN(cteTemplate2.OrderNumber) / 3 = CASE  (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n) AS a\r\nWHERE LEN(OrderNumber)/3 <= 2--CodeNo <= 2\r\nORDER BY OrderNumber ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TemplateId", templateId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static IList<BudTemplate> Select(string tempTypeId)
        {
            IList<BudTemplate> list = new List<BudTemplate>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var typeb in (from temp in entities.Bud_Template
                    where temp.Bud_TemplateType.TypeId == tempTypeId
                    orderby temp.InputDate descending
                    select new { TemplateId = temp.TemplateId, TemplateName = temp.TemplateName, TypeId = temp.Bud_TemplateType.TypeId }).ToList())
                {
                    BudTemplate item = new BudTemplate {
                        Id = typeb.TemplateId,
                        Name = typeb.TemplateName,
                        BudTempType = BudTemplateType.GetById(typeb.TypeId)
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static IList<BudTemplate> Select(int pagesize, int pageIndex, string tempTypeId)
        {
            IList<BudTemplate> list = new List<BudTemplate>();
            using (pm2Entities entities = new pm2Entities())
            {
                var queryable = (from temp in entities.Bud_Template
                    where temp.Bud_TemplateType.TypeId == tempTypeId
                    orderby temp.InputDate descending
                    select new { TemplateId = temp.TemplateId, TemplateName = temp.TemplateName, TypeId = temp.Bud_TemplateType.TypeId }).Skip((pageIndex * pagesize)).Take(pagesize);
                if ((tempTypeId == "") || (tempTypeId == "0"))
                {
                    queryable = (from temp in entities.Bud_Template
                        orderby temp.InputDate descending
                        select new { TemplateId = temp.TemplateId, TemplateName = temp.TemplateName, TypeId = temp.Bud_TemplateType.TypeId }).Skip((pageIndex * pagesize)).Take(pagesize);
                }
                foreach (var typeb in queryable)
                {
                    BudTemplate item = new BudTemplate {
                        Id = typeb.TemplateId,
                        Name = typeb.TemplateName,
                        BudTempType = BudTemplateType.GetById(typeb.TypeId)
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static void Update(BudTemplate budTemplate)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_Template template = entities.Bud_Template.FirstOrDefault<Bud_Template>(c => c.TemplateId == budTemplate.Id);
                template.TemplateCode = budTemplate.Code;
                template.TemplateName = budTemplate.Name;
                template.Bud_TemplateType = entities.Bud_TemplateType.FirstOrDefault<Bud_TemplateType>(c => c.TypeId == budTemplate.BudTempType.Id);
                entities.SaveChanges();
            }
        }

        public BudTemplateType BudTempType { get; set; }

        public string Code { get; set; }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public string Name { get; set; }
    }
}

