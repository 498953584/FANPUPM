namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BudConsTaskResService : Repository<BudConsTaskRes>
    {
        public BudConsTaskRes GetById(string id)
        {
            return (from r in this
                where r.ConsTaskResId == id
                select r).FirstOrDefault<BudConsTaskRes>();
        }
    }
}

