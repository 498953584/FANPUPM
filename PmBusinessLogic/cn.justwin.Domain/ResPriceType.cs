namespace cn.justwin.Domain
{
    using cn.justwin.BLL;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ResPriceType
    {
        private ResPriceType()
        {
        }

        public static void Add(ResPriceType respricetype)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_PriceType type = bindEntity(respricetype, true);
                entities.AddToRes_PriceType(type);
                entities.SaveChanges();
            }
        }

        private static Res_PriceType bindEntity(ResPriceType respricetype, bool isAdd)
        {
            Res_PriceType type = new Res_PriceType {
                PriceTypeName = respricetype.Name,
                Note = respricetype.Note
            };
            if (isAdd)
            {
                type.PriceTypeId = respricetype.Id;
                type.PriceTypeCode = respricetype.Code;
                type.InputUser = respricetype.InputUser;
                type.InputDate = respricetype.InputDate;
                type.UserCodes = JsonHelper.Serialize(respricetype.UserCodes.ToArray<string>());
            }
            return type;
        }

        public static ResPriceType Create(string id, string code, string name, string note, string user, DateTime? date, IList<string> userCodes)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("价格类型Id", "价格类型Id不能为空");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("价格类型编码", "价格类型编码不能为空");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("价格类型名称", "价格类型名称不能为空");
            }
            return new ResPriceType { Id = id, Code = code, Name = name, Note = note, InputUser = user, InputDate = date, UserCodes = userCodes };
        }

        public static void Delete(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_PriceType entity = (from m in entities.Res_PriceType
                    where m.PriceTypeId == id
                    select m).FirstOrDefault<Res_PriceType>();
                if (entity == null)
                {
                    throw new Exception("找不到需要删除的价格类型");
                }
                entities.DeleteObject(entity);
                entities.SaveChanges();
            }
        }

        public static IList<ResPriceType> GetAll()
        {
            IList<ResPriceType> list = new List<ResPriceType>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Res_PriceType type in (from m in entities.Res_PriceType select m).ToList<Res_PriceType>())
                {
                    ResPriceType item = new ResPriceType {
                        Id = type.PriceTypeId,
                        Code = type.PriceTypeCode,
                        Name = type.PriceTypeName,
                        Note = type.Note,
                        InputUser = type.InputUser,
                        InputDate = type.InputDate,
                        UserCodes = JsonHelper.GetListFromJson(type.UserCodes)
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static IList<ResPriceType> GetAll(string userCode)
        {
            List<ResPriceType> list = new List<ResPriceType>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Res_PriceType type in (from pt in entities.Res_PriceType
                    where pt.UserCodes.Contains(userCode)
                    select pt).ToList<Res_PriceType>())
                {
                    ResPriceType item = new ResPriceType {
                        Id = type.PriceTypeId,
                        Code = type.PriceTypeCode,
                        Name = type.PriceTypeName,
                        Note = type.PriceTypeCode,
                        InputUser = type.InputUser,
                        InputDate = type.InputDate,
                        UserCodes = JsonHelper.GetListFromJson(type.UserCodes)
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static ResPriceType GetById(string id)
        {
            ResPriceType type = null;
            using (pm2Entities entities = new pm2Entities())
            {
                Res_PriceType type2 = (from m in entities.Res_PriceType
                    where m.PriceTypeId == id
                    select m).FirstOrDefault<Res_PriceType>();
                if (type2 != null)
                {
                    type = new ResPriceType {
                        Id = type2.PriceTypeId,
                        Code = type2.PriceTypeCode,
                        Name = type2.PriceTypeName,
                        Note = type2.Note,
                        InputUser = type2.InputUser,
                        InputDate = type2.InputDate,
                        UserCodes = JsonHelper.GetListFromJson(type2.UserCodes)
                    };
                }
            }
            return type;
        }

        public static ResPriceType GetByTypeCode(string typeCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_PriceType
                    where m.PriceTypeCode == typeCode
                    select new ResPriceType { Id = m.PriceTypeId, Code = m.PriceTypeCode, Name = m.PriceTypeName, Note = m.Note, InputDate = m.InputDate, InputUser = m.InputUser }).FirstOrDefault<ResPriceType>();
            }
        }

        public static List<string> GetPriceTypeCodes()
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_PriceType select m.PriceTypeCode).ToList<string>();
            }
        }

        public static void Upadate(ResPriceType respricetype)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Res_PriceType
                    where m.PriceTypeId == respricetype.Id
                    select m).FirstOrDefault<Res_PriceType>() == null)
                {
                    throw new Exception("找不到需要更新的价格类型");
                }
                Res_PriceType type = bindEntity(respricetype, false);
                entities.SaveChanges();
            }
        }

        public string Code { get; set; }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public IList<string> UserCodes { get; set; }
    }
}

