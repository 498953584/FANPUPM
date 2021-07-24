namespace cn.justwin.Tender
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PrjType
    {
        private PrjType()
        {
        }

        public static void Add(PrjType prjType)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Basic_CodeList list = new Basic_CodeList {
                    TypeCode = prjType.TypeCode,
                    ItemCode = prjType.ItemCode,
                    ItemName = prjType.ItemName
                };
                Basic_CodeType type = (from c in entities.Basic_CodeType
                    where c.TypeCode == prjType.TypeCode
                    select c).FirstOrDefault<Basic_CodeType>();
                if (type != null)
                {
                    list.Basic_CodeType = type;
                }
                entities.AddToBasic_CodeList(list);
                entities.SaveChanges();
            }
        }

        public static PrjType Create(string typeCode, int itemCode, string ItemName)
        {
            return new PrjType { TypeCode = typeCode, ItemCode = itemCode, ItemName = ItemName };
        }

        public static void Delete(string typeCode, List<string> itemCodeList)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (itemCodeList.Count == 1)
                {
                    Delete(typeCode, Convert.ToInt32(itemCodeList[0]), entities);
                }
                else
                {
                    Delete(typeCode, itemCodeList, entities);
                }
                entities.SaveChanges();
            }
        }

        private static void Delete(string typeCode, int itemCode, pm2Entities context)
        {
            Basic_CodeList entity = (from m in context.Basic_CodeList
                where (m.TypeCode == typeCode) && (m.ItemCode == itemCode)
                select m).FirstOrDefault<Basic_CodeList>();
            if (entity == null)
            {
                throw new Exception("没有找到要删除的工程类型");
            }
            if (GetUsedCount(typeCode, itemCode, context) > 0)
            {
                throw new Exception("此工程类型已被项目使用，不能被删除！");
            }
            context.DeleteObject(entity);
        }

        private static void Delete(string typeCode, List<string> itemCodeList, pm2Entities context)
        {
            foreach (string str in itemCodeList)
            {
                Delete(typeCode, Convert.ToInt32(str), context);
            }
        }

        public static PrjType GetByItemCode(string typeCode, int itemCode)
        {
            PrjType type = new PrjType();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Basic_CodeList
                    where (m.TypeCode == typeCode) && (m.ItemCode == itemCode)
                    select new PrjType { TypeCode = m.TypeCode, ItemCode = m.ItemCode, ItemName = m.ItemName }).FirstOrDefault<PrjType>();
            }
        }

        public static List<PrjType> GetByItemName(string typeCode, string itemName)
        {
            List<PrjType> list = new List<PrjType>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Basic_CodeList
                    where (m.TypeCode == typeCode) && (m.ItemName == itemName)
                    select new PrjType { TypeCode = m.TypeCode, ItemCode = m.ItemCode, ItemName = m.ItemName }).ToList<PrjType>();
            }
        }

        public static List<PrjType> GetByType(string typeCode, int pageIndex, int pagesize)
        {
            List<PrjType> list = new List<PrjType>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Basic_CodeList
                    where m.TypeCode == typeCode
                    orderby m.ItemCode
                    select new PrjType { TypeCode = m.TypeCode, ItemCode = m.ItemCode, ItemName = m.ItemName }).Skip<PrjType>((pageIndex * pagesize)).Take<PrjType>(pagesize).ToList<PrjType>();
            }
        }

        public static Dictionary<string, string> GetCodeType()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            using (pm2Entities entities = new pm2Entities())
            {
                List<Basic_CodeType> list = (from m in entities.Basic_CodeType
                    where (m.TypeCode == "ConstructType") || (m.TypeCode == "DesignType")
                    select m).ToList<Basic_CodeType>();
                for (int i = 0; i < list.Count; i++)
                {
                    dictionary.Add(list[i].TypeCode, list[i].TypeName);
                }
            }
            return dictionary;
        }

        public static int GetCountByType(string typeCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Basic_CodeList
                    where m.TypeCode == typeCode
                    select m).Count<Basic_CodeList>();
            }
        }

        public static int GetNewItemCode(string typeCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Basic_CodeList list = (from m in entities.Basic_CodeList
                    where m.TypeCode == typeCode
                    orderby m.ItemCode descending
                    select m).FirstOrDefault<Basic_CodeList>();
                if (list != null)
                {
                    return (list.ItemCode + 1);
                }
                return 1;
            }
        }

        private static int GetUsedCount(string typeCode, int itemCode, pm2Entities context)
        {
            return (from m in context.PT_PrjInfo_EngineeringType
                where (m.EngineeringType == typeCode) && (m.EngineeringSubType == itemCode)
                select m).Count<PT_PrjInfo_EngineeringType>();
        }

        public static void Update(PrjType prjType)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Basic_CodeList list = (from m in entities.Basic_CodeList
                    where (m.ItemCode == prjType.ItemCode) && (m.TypeCode == prjType.TypeCode)
                    select m).FirstOrDefault<Basic_CodeList>();
                if (list == null)
                {
                    throw new Exception("没有找到要修改的工程类型");
                }
                list.ItemName = prjType.ItemName;
                entities.SaveChanges();
            }
        }

        public int ItemCode { get; set; }

        public string ItemName { get; set; }

        public string TypeCode { get; set; }
    }
}

