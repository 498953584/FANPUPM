namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class EquRepairPlanService : Repository<EquRepairPlan>
    {
        public EquRepairPlan GetById(string id)
        {
            return (from p in this
                where p.RepairId.Equals(id)
                select p).FirstOrDefault<EquRepairPlan>();
        }
    }
}

