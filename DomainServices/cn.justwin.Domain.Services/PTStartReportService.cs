namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTStartReportService : Repository<PTStartReport>
    {
        public PTStartReport GetById(string id)
        {
            return (from sr in this
                where sr.ReportId == id
                select sr).FirstOrDefault<PTStartReport>();
        }

        public PTStartReport GetByPrjGuid(string prjId)
        {
            return (from sr in this
                where sr.PrjGuid == prjId
                select sr).FirstOrDefault<PTStartReport>();
        }
    }
}

