namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class EquRepairApplyService : Repository<EquRepairApply>
    {
        public EquRepairApply GetById(string id)
        {
            return (from r in this
                where r.ApplyId.Equals(id)
                select r).FirstOrDefault<EquRepairApply>();
        }
    }
}

