namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BudTemplateType
    {
        private BudTemplateType()
        {
        }

        public static void Add(BudTemplateType budTempType)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TemplateType type = new Bud_TemplateType {
                    TypeId = budTempType.Id,
                    TypeCode = budTempType.Code,
                    TypeName = budTempType.Name,
                    InputUser = budTempType.InputUser
                };
                if (budTempType.InputDate.HasValue)
                {
                    type.InputDate = Convert.ToDateTime(budTempType.InputDate);
                }
                entities.AddToBud_TemplateType(type);
                entities.SaveChanges();
            }
        }

        public static BudTemplateType Create(string id, string code, string name, string inputUser, DateTime? inputDate)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("模板类型Id", "模板类型Id不能为空");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("模板类型编码", "模板类型编码不能为空");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("模板类型名称", "模板类型名称不能为空");
            }
            return new BudTemplateType { Id = id, Code = code, Name = name, InputUser = inputUser, InputDate = inputDate };
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

        private static void Delete(string id, pm2Entities context)
        {
            Bud_TemplateType entity = (from m in context.Bud_TemplateType
                where m.TypeId == id
                select m).FirstOrDefault<Bud_TemplateType>();
            if (entity == null)
            {
                throw new Exception("找不到需要删除的模板类型");
            }
            if (context.Bud_Template.Count<Bud_Template>(c => (c.Bud_TemplateType.TypeId == id)) > 0)
            {
                throw new Exception("当前模板类型不是最底层的模板类型");
            }
            context.DeleteObject(entity);
        }

        private static void Delete(List<string> idList, pm2Entities context)
        {
            bool flag = true;
            List<Bud_TemplateType> list = GetBud_TemplateType(idList, context);
            using (List<Bud_TemplateType>.Enumerator enumerator = list.GetEnumerator())
            {
                Bud_TemplateType bud_TemplateType;
                while (enumerator.MoveNext())
                {
                    bud_TemplateType = enumerator.Current;
                    if (context.Bud_Template.Count<Bud_Template>(c => (c.Bud_TemplateType.TypeId == bud_TemplateType.TypeId)) > 0)
                    {
                        flag = false;
                        goto Label_00ED;
                    }
                }
            }
        Label_00ED:
            if (!flag)
            {
                throw new Exception("当前模板类型不是最底层的模板类型");
            }
            foreach (Bud_TemplateType type in list)
            {
                context.DeleteObject(type);
            }
        }

        public static IList<BudTemplateType> GetAll()
        {
            IList<BudTemplateType> list = new List<BudTemplateType>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from tt in entities.Bud_TemplateType select new BudTemplateType { Id = tt.TypeId, Code = tt.TypeCode, Name = tt.TypeName, InputUser = tt.InputUser, InputDate = tt.InputDate }).ToList<BudTemplateType>();
            }
        }

        public static List<Bud_TemplateType> GetBud_TemplateType(List<string> idList, pm2Entities contxt)
        {
            List<Bud_TemplateType> list = new List<Bud_TemplateType>();
            using (List<string>.Enumerator enumerator = idList.GetEnumerator())
            {
                string id;
                while (enumerator.MoveNext())
                {
                    id = enumerator.Current;
                    Bud_TemplateType item = contxt.Bud_TemplateType.FirstOrDefault<Bud_TemplateType>(c => c.TypeId == id);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static BudTemplateType GetById(string id)
        {
            BudTemplateType type = new BudTemplateType();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from tt in entities.Bud_TemplateType
                    where tt.TypeId == id
                    select new BudTemplateType { Id = tt.TypeId, Code = tt.TypeCode, Name = tt.TypeName, InputUser = tt.InputUser, InputDate = tt.InputDate }).FirstOrDefault<BudTemplateType>();
            }
        }

        public static IList<BudTemplateType> GetByName(string name)
        {
            IList<BudTemplateType> list = new List<BudTemplateType>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from tempType in entities.Bud_TemplateType
                    where tempType.TypeName == name
                    select new BudTemplateType { Id = tempType.TypeId, Code = tempType.TypeCode, Name = tempType.TypeName, InputUser = tempType.InputUser, InputDate = tempType.InputDate }).ToList<BudTemplateType>();
            }
        }

        public static IList<BudTemplateType> Select(int pagesize, int pageIndex)
        {
            IList<BudTemplateType> list = new List<BudTemplateType>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from tt in entities.Bud_TemplateType
                    orderby tt.InputDate descending
                    select new BudTemplateType { Id = tt.TypeId, Code = tt.TypeCode, Name = tt.TypeName, InputUser = tt.InputUser, InputDate = tt.InputDate }).Skip<BudTemplateType>((pageIndex * pagesize)).Take<BudTemplateType>(pagesize).ToList<BudTemplateType>();
            }
        }

        public static void Update(BudTemplateType budTempType)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TemplateType type = (from m in entities.Bud_TemplateType
                    where m.TypeId == budTempType.Id
                    select m).FirstOrDefault<Bud_TemplateType>();
                type.TypeCode = budTempType.Code;
                type.TypeName = budTempType.Name;
                entities.SaveChanges();
            }
        }

        public string Code { get; set; }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public string Name { get; set; }
    }
}

