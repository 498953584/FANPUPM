namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class WFBusinessCodeService : Repository<WFBusinessCode>
    {
        public WFBusinessCode GetById(string id)
        {
            return (from c in this
                where c.BusinessCode == id
                select c).FirstOrDefault<WFBusinessCode>();
        }
    }
}

