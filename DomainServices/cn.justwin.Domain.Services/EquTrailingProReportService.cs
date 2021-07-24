namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class EquTrailingProReportService : Repository<EquTrailingProReport>
    {
        public EquTrailingProReport GetById(string id)
        {
            return (from t in this
                where t.ProductionId.Equals(id)
                select t).FirstOrDefault<EquTrailingProReport>();
        }
    }
}

