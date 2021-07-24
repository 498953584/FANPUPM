namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class EquShipMonthReportService : Repository<EquShipMonthReport>
    {
        public EquShipMonthReport GetById(string id)
        {
            return (from r in this
                where r.MonthId.Equals(id)
                select r).FirstOrDefault<EquShipMonthReport>();
        }
    }
}

