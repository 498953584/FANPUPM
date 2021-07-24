namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BasicCode
    {
        public static IList<BasicCode> GetByType(string typeCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from c in entities.Basic_CodeList
                    where c.TypeCode == typeCode
                    select new BasicCode { TypeCode = c.TypeCode, ItemCode = c.ItemCode, ItemName = c.ItemName }).ToList<BasicCode>();
            }
        }

        public static BasicCode GetByTypeAndItem(string typeCode, int itemCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from c in entities.Basic_CodeList
                    where (c.TypeCode == typeCode) && (c.ItemCode == itemCode)
                    select new BasicCode { TypeCode = c.TypeCode, ItemCode = c.ItemCode, ItemName = c.ItemName }).FirstOrDefault<BasicCode>();
            }
        }

        public void Update(string itemName)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                (from c in entities.Basic_CodeList
                    where (c.TypeCode == this.TypeCode) && (c.ItemCode == this.ItemCode)
                    select c).FirstOrDefault<Basic_CodeList>().ItemName = itemName;
                entities.SaveChanges();
            }
        }

        public int ItemCode { get; set; }

        public string ItemName { get; set; }

        public string TypeCode { get; set; }
    }
}

