namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BudPreReimburseModifyService : Repository<BudPreReimburseModify>
    {
        public BudPreReimburseModify GetById(string id)
        {
            return (from i in this
                where i.Id == id
                select i).FirstOrDefault<BudPreReimburseModify>();
        }
    }
}

