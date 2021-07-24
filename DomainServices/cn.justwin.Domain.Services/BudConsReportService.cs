namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudConsReportService : Repository<BudConsReport>
    {
        public BudConsReport GetById(string Id)
        {
            return (from p in this
                where p.ConsReportId == Id
                select p).FirstOrDefault<BudConsReport>();
        }

        public List<BudConsReport> LstBudCons()
        {
            return (from p in this
                where p.FlowState == 1
                select p).ToList<BudConsReport>();
        }
    }
}

