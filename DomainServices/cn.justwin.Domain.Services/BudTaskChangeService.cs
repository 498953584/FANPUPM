namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BudTaskChangeService : Repository<BudTaskChange>
    {
        public BudTaskChange GetById(string id)
        {
            return (from c in this
                where c.TaskChangeId == id
                select c).FirstOrDefault<BudTaskChange>();
        }

        public BudTaskChange GetByPrj(string prjId)
        {
            return (from c in this
                where c.PrjId == prjId
                select c).FirstOrDefault<BudTaskChange>();
        }
    }
}

