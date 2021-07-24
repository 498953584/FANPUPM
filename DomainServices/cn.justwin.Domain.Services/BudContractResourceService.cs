namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BudContractResourceService : Repository<BudContractResource>
    {
        public BudContractResource GetById(string id)
        {
            return (from r in this
                where r.TaskResourceId == id
                select r).FirstOrDefault<BudContractResource>();
        }
    }
}

