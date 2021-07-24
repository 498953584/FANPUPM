namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BasicCodeListService : Repository<BasicCodeList>
    {
        public IList<BasicCodeList> GetByType(string type)
        {
            return (from c in this
                where c.TypeCode == type
                orderby c.ItemCode
                select c).ToList<BasicCodeList>();
        }

        public string GetNameByTypeAndCode(string typeCode, int itemCode)
        {
            return (from p in this
                where (p.TypeCode == typeCode) && (p.ItemCode == itemCode)
                select p).FirstOrDefault<BasicCodeList>().ItemName;
        }
    }
}

