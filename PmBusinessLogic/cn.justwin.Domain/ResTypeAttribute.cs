namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ResTypeAttribute
    {
        private ResTypeAttribute()
        {
        }

        public static void Add(ResTypeAttribute typeAttr)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Attribute attribute = new Res_Attribute {
                    AttributeId = typeAttr.Id,
                    AttributeName = typeAttr.Name,
                    InputUser = typeAttr.InputUser,
                    InputDate = typeAttr.InputDate
                };
                Res_ResourceType type = (from rt in entities.Res_ResourceType
                    where rt.ResourceTypeId == typeAttr.ResourceType.Id
                    select rt).FirstOrDefault<Res_ResourceType>();
                if (type == null)
                {
                    throw new Exception("不能确定其所属类别");
                }
                attribute.Res_ResourceType = type;
                entities.SaveChanges();
            }
        }

        public static ResTypeAttribute Create(string id, ResType resType, string name, string inputUser, DateTime? date)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("类别属性Id", "类别属性Id不能为空");
            }
            if (resType == null)
            {
                throw new ArgumentNullException("资源类型", "资源类型不能为空");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("类别名称", "类别名称不能为空");
            }
            return new ResTypeAttribute { Id = id, Name = name, ResourceType = resType, InputUser = inputUser, InputDate = date };
        }

        public static void Delete(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Attribute entity = (from attr in entities.Res_Attribute
                    where attr.AttributeId == id
                    select attr).FirstOrDefault<Res_Attribute>();
                if (entity == null)
                {
                    throw new Exception("找不到需要删除的类别属性");
                }
                entities.DeleteObject(entity);
                entities.SaveChanges();
            }
        }

        public static IList<ResTypeAttribute> GetAll()
        {
            IList<ResTypeAttribute> list = new List<ResTypeAttribute>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var type in (from a in entities.Res_Attribute select new { AttributeId = a.AttributeId, AttributeName = a.AttributeName, InputUser = a.InputUser, InputDate = a.InputDate, ResourceTypeId = a.Res_ResourceType.ResourceTypeId }).ToList())
                {
                    ResTypeAttribute item = new ResTypeAttribute {
                        Id = type.AttributeId,
                        Name = type.AttributeName,
                        InputUser = type.InputUser,
                        ResourceType = ResType.GetById(type.ResourceTypeId)
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static ResTypeAttribute GetByAttributeName(string attributeName)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_Attribute
                    where m.AttributeName == attributeName
                    select new ResTypeAttribute { Id = m.AttributeId, Name = m.AttributeName, InputDate = m.InputDate, InputUser = m.InputUser }).FirstOrDefault<ResTypeAttribute>();
            }
        }

        public static ResTypeAttribute GetById(string id)
        {
            ResTypeAttribute attribute = new ResTypeAttribute();
            using (pm2Entities entities = new pm2Entities())
            {
                var type = (from a in entities.Res_Attribute
                    where a.AttributeId == id
                    select new { AttributeId = a.AttributeId, AttributeName = a.AttributeName, InputUser = a.InputUser, InputDate = a.InputDate, ResourceTypeId = a.Res_ResourceType.ResourceTypeId }).FirstOrDefault();
                if (type != null)
                {
                    attribute.Id = id;
                    attribute.Name = type.AttributeName;
                    attribute.InputUser = type.InputUser;
                    attribute.InputDate = type.InputDate;
                    ResType byId = ResType.GetById(type.ResourceTypeId);
                    attribute.ResourceType = byId;
                    return attribute;
                }
                return null;
            }
        }

        public static void Update(ResTypeAttribute typeAttr)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Attribute attribute = (from attr in entities.Res_Attribute
                    where attr.AttributeId == typeAttr.Id
                    select attr).FirstOrDefault<Res_Attribute>();
                if (attribute == null)
                {
                    throw new Exception("找不到需要修改的类别属性");
                }
                attribute.AttributeName = typeAttr.Name;
                entities.SaveChanges();
            }
        }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public string Name { get; set; }

        public ResType ResourceType { get; set; }
    }
}

