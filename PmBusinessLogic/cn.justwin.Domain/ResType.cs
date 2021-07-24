namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ResType
    {
        private string cbsCode;
        private string code;
        private string id;
        private DateTime? inputDate;
        private string inputUser;
        private bool? isValid;
        private string name;
        private string parentId;

        private ResType()
        {
        }

        public static void Add(ResType objResType)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_ResourceType type = new Res_ResourceType {
                    ResourceTypeId = objResType.Id,
                    ResourceTypeCode = objResType.Code,
                    ResourceTypeName = objResType.Name,
                    InputUser = objResType.InputUser,
                    InputDate = objResType.InputDate,
                    IsValid = objResType.IsValid,
                    Res_ResourceType2 = (from rt in entities.Res_ResourceType
                        where rt.ResourceTypeId == objResType.ParentId
                        select rt).FirstOrDefault<Res_ResourceType>()
                };
                entities.AddToRes_ResourceType(type);
                entities.SaveChanges();
            }
        }

        public static ResType Create(string id, string parentId, string code, string name, string inputUser, DateTime? inputDate, bool? isValid)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("资源类型Id", "资源类型Id不能为空");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("资源类型名称", "资源类型名称不能为空");
            }
            return new ResType { Id = id, ParentId = parentId, Code = code, Name = name, InputUser = inputUser, InputDate = inputDate, IsValid = isValid };
        }

        public static void Delete(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                entities.DeleteObject(entities.Res_ResourceType.FirstOrDefault<Res_ResourceType>(c => c.ResourceTypeId == id));
                entities.SaveChanges();
            }
        }

        public static IList<ResType> GetAll()
        {
            IList<ResType> list = new List<ResType>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from rt in entities.Res_ResourceType
                    where rt.Res_ResourceType2.ResourceTypeId == null
                    select new ResType { Id = rt.ResourceTypeId, ParentId = rt.Res_ResourceType2.ResourceTypeId, Name = rt.ResourceTypeName, Code = rt.ResourceTypeCode, InputUser = rt.InputUser, InputDate = rt.InputDate, IsValid = rt.IsValid, CBSCode = rt.CBSCode }).ToList<ResType>();
            }
        }

        public static ResType GetById(string id)
        {
            ResType type = new ResType();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from rt in entities.Res_ResourceType
                    where rt.ResourceTypeId == id
                    select new ResType { Id = rt.ResourceTypeId, Code = rt.ResourceTypeCode, Name = rt.ResourceTypeName, InputUser = rt.InputUser, InputDate = rt.InputDate, IsValid = rt.IsValid, ParentId = rt.Res_ResourceType2.ResourceTypeId, CBSCode = rt.CBSCode }).FirstOrDefault<ResType>();
            }
        }

        public static IList<ResType> GetHumanResMachine()
        {
            IList<ResType> list = new List<ResType>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from rt in entities.Res_ResourceType
                    where rt.Res_ResourceType2.ResourceTypeId == null
                    select new ResType { Id = rt.ResourceTypeId, ParentId = rt.Res_ResourceType2.ResourceTypeId, Name = rt.ResourceTypeName, Code = rt.ResourceTypeCode, InputUser = rt.InputUser, InputDate = rt.InputDate, IsValid = rt.IsValid }).Take<ResType>(3).ToList<ResType>();
            }
        }

        public static bool IsContainsChild(string resourceTypeId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return ((from m in entities.Res_ResourceType
                    where m.Res_ResourceType2.ResourceTypeId == resourceTypeId
                    select m).Count<Res_ResourceType>() > 0);
            }
        }

        public static void Update(ResType resType)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_ResourceType type = entities.Res_ResourceType.FirstOrDefault<Res_ResourceType>(c => c.ResourceTypeId == resType.Id);
                type.ResourceTypeCode = resType.Code;
                type.ResourceTypeName = resType.Name;
                type.IsValid = resType.IsValid;
                type.Res_ResourceType2 = (from rt in entities.Res_ResourceType
                    where rt.ResourceTypeId == resType.ParentId
                    select rt).FirstOrDefault<Res_ResourceType>();
                entities.SaveChanges();
            }
        }

        public static void Update(List<ResType> resTypeList)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<ResType>.Enumerator enumerator = resTypeList.GetEnumerator())
                {
                    ResType resType;
                    while (enumerator.MoveNext())
                    {
                        resType = enumerator.Current;
                        Res_ResourceType type = entities.Res_ResourceType.FirstOrDefault<Res_ResourceType>(c => c.ResourceTypeId == resType.Id);
                        type.ResourceTypeCode = resType.Code;
                        type.ResourceTypeName = resType.Name;
                        type.IsValid = resType.IsValid;
                        type.CBSCode = resType.CBSCode;
                        type.Res_ResourceType2 = (from rt in entities.Res_ResourceType
                            where rt.ResourceTypeId == resType.ParentId
                            select rt).FirstOrDefault<Res_ResourceType>();
                    }
                }
                entities.SaveChanges();
            }
        }

        public string CBSCode
        {
            get
            {
                return this.cbsCode;
            }
            set
            {
                this.cbsCode = value;
            }
        }

        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public DateTime? InputDate
        {
            get
            {
                return this.inputDate;
            }
            set
            {
                this.inputDate = value;
            }
        }

        public string InputUser
        {
            get
            {
                return this.inputUser;
            }
            set
            {
                this.inputUser = value;
            }
        }

        public bool? IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string ParentId
        {
            get
            {
                return this.parentId;
            }
            set
            {
                this.parentId = value;
            }
        }
    }
}

