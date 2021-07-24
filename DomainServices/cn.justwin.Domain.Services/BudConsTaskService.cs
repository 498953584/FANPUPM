namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BudConsTaskService : Repository<BudConsTask>
    {
        public BudConsTask GetById(string id)
        {
            return (from t in this
                where t.ConsTaskId == id
                select t).FirstOrDefault<BudConsTask>();
        }
    }
}

