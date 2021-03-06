namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class EquGrapProReportService : Repository<EquGrapProReport>
    {
        public EquGrapProReport GetById(string id)
        {
            return (from u in this
                where u.ProductionId == id
                select u).FirstOrDefault<EquGrapProReport>();
        }
    }
}

