namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ResTemplate
    {
        private ResTemplate()
        {
        }

        public static void Add(ResTemplate restemplate)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Template template = bindEntity(restemplate, true);
                entities.AddToRes_Template(template);
                entities.SaveChanges();
            }
        }

        private static Res_Template bindEntity(ResTemplate restemplate, bool isAdd)
        {
            Res_Template template = new Res_Template();
            if (isAdd)
            {
                template.TemplateId = restemplate.Id;
                template.TemplateName = restemplate.Name;
                template.StartRowIndex = restemplate.StartRowIndex;
                template.InputDate = restemplate.InputDate;
                template.InputUser = restemplate.InputUser;
            }
            template.IsValid = restemplate.IsValid;
            return template;
        }

        public static ResTemplate Create(string id, string name, int rowIndex, bool isValid, string user, DateTime? date)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("模板Id", "模板Id不能为空");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("模板名称", "模板名称不能为空");
            }
            return new ResTemplate { Id = id, Name = name, IsValid = new bool?(isValid), StartRowIndex = new int?(rowIndex), InputDate = date, InputUser = user };
        }

        public static void Delete(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Template entity = (from m in entities.Res_Template
                    where m.TemplateId == id
                    select m).FirstOrDefault<Res_Template>();
                if (entity == null)
                {
                    throw new Exception("找不到需要删除的模板");
                }
                entities.DeleteObject(entity);
                entities.SaveChanges();
            }
        }

        public static IList<ResTemplate> GetAll()
        {
            List<ResTemplate> list = new List<ResTemplate>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Res_Template template in (from m in entities.Res_Template
                    orderby m.InputDate descending
                    select m).ToList<Res_Template>())
                {
                    ResTemplate item = new ResTemplate {
                        Id = template.TemplateId,
                        Name = template.TemplateName,
                        IsValid = template.IsValid,
                        StartRowIndex = template.StartRowIndex,
                        InputDate = template.InputDate,
                        InputUser = template.InputUser
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static ResTemplate GetById(string id)
        {
            ResTemplate template = null;
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Template template2 = (from m in entities.Res_Template
                    where m.TemplateId == id
                    select m).FirstOrDefault<Res_Template>();
                if (template2 != null)
                {
                    template = new ResTemplate {
                        Id = template2.TemplateId,
                        Name = template2.TemplateName,
                        IsValid = template2.IsValid,
                        InputDate = template2.InputDate,
                        InputUser = template2.InputUser,
                        StartRowIndex = template2.StartRowIndex
                    };
                }
            }
            return template;
        }

        public static bool isExistTemplate(string templateName)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Res_Template
                    where m.TemplateName == templateName
                    select m.TemplateId).ToList<string>().Count > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static void Update(ResTemplate restemplate)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Res_Template
                    where m.TemplateId == restemplate.Id
                    select m).FirstOrDefault<Res_Template>() == null)
                {
                    throw new Exception("找不到需要修改的模板");
                }
                Res_Template template = bindEntity(restemplate, false);
                entities.SaveChanges();
            }
        }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public bool? IsValid { get; set; }

        public string Name { get; set; }

        public int? StartRowIndex { get; set; }
    }
}

