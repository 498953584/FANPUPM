namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ResTemplateItem
    {
        private ResTemplateItem()
        {
        }

        public static void Add(ResTemplateItem restemplateitem)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_TemplateItem entity = bindEntity(restemplateitem);
                Res_Template template = (from m in entities.Res_Template
                    where m.TemplateId == restemplateitem.Template.Id
                    select m).FirstOrDefault<Res_Template>();
                entity.Res_Template = template;
                template.Res_TemplateItem.Add(entity);
                entities.SaveChanges();
            }
        }

        private static Res_TemplateItem bindEntity(ResTemplateItem restemplateitem)
        {
            return new Res_TemplateItem { ItemId = restemplateitem.ItemId, ExcelColumn = restemplateitem.ExcelColumn, ExcelRealCoumn = restemplateitem.ExcelRealCoumn, DbColumn = restemplateitem.DbColumn };
        }

        public static ResTemplateItem Create(string id, ResTemplate template, string column, string realColumn, string Dbcolumn)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("模板子项Id", "模板子项Id不能为空");
            }
            return new ResTemplateItem { ItemId = id, Template = template, ExcelColumn = column, ExcelRealCoumn = realColumn, DbColumn = Dbcolumn };
        }

        public static void Delete(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_TemplateItem entity = (from m in entities.Res_TemplateItem
                    where m.ItemId == id
                    select m).FirstOrDefault<Res_TemplateItem>();
                if (entity == null)
                {
                    throw new Exception("找不到需要删除的模板");
                }
                entities.DeleteObject(entity);
                entities.SaveChanges();
            }
        }

        public static IList<ResTemplateItem> GetAll()
        {
            List<ResTemplateItem> list = new List<ResTemplateItem>();
            using (pm2Entities entities = new pm2Entities())
            {
                var list2 = (from m in entities.Res_TemplateItem select new { ItemId = m.ItemId, ExcelColumn = m.ExcelColumn, ExcelRealCoumn = m.ExcelRealCoumn, DbColumn = m.DbColumn, TemplateId = m.Res_Template.TemplateId }).ToList();
                if (list2.Count == 0)
                {
                    return list;
                }
                foreach (var type in list2)
                {
                    ResTemplateItem item = new ResTemplateItem {
                        ItemId = type.ItemId,
                        ExcelColumn = type.ExcelColumn,
                        ExcelRealCoumn = type.ExcelRealCoumn,
                        DbColumn = type.DbColumn,
                        Template = ResTemplate.GetById(type.TemplateId)
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<ResTemplateItem> GetAllByTemplateId(string templateId)
        {
            List<ResTemplateItem> list = new List<ResTemplateItem>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_TemplateItem
                    where m.Res_Template.TemplateId == templateId
                    orderby m.DbColumn descending
                    select new ResTemplateItem { ItemId = m.ItemId, ExcelColumn = m.ExcelColumn, ExcelRealCoumn = m.ExcelRealCoumn, DbColumn = m.DbColumn }).ToList<ResTemplateItem>();
            }
        }

        public static ResTemplateItem GetById(string id)
        {
            ResTemplateItem item = new ResTemplateItem();
            using (pm2Entities entities = new pm2Entities())
            {
                var type = (from m in entities.Res_TemplateItem
                    where m.ItemId == id
                    select new { ItemId = m.ItemId, ExcelColumn = m.ExcelColumn, ExcelRealCoumn = m.ExcelRealCoumn, DbColumn = m.DbColumn, TemplateId = m.Res_Template.TemplateId }).FirstOrDefault();
                if (type != null)
                {
                    item.ItemId = type.ItemId;
                    item.ExcelColumn = type.ExcelColumn;
                    item.ExcelRealCoumn = type.ExcelRealCoumn;
                    item.DbColumn = type.DbColumn;
                    item.Template = ResTemplate.GetById(type.TemplateId);
                    return item;
                }
                return null;
            }
        }

        public string DbColumn { get; set; }

        public string ExcelColumn { get; set; }

        public string ExcelRealCoumn { get; set; }

        public string ItemId { get; set; }

        public ResTemplate Template { get; set; }
    }
}

