namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BudPreReimburseApplyService : Repository<BudPreReimburseApply>
    {
        public BudPreReimburseApply GetById(string id)
        {
            return (from i in this
                where i.Id == id
                select i).FirstOrDefault<BudPreReimburseApply>();
        }
    }
}

