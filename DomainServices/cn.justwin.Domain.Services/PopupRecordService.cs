namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PopupRecordService : Repository<PopupRecord>
    {
        public PopupRecord GetById(int id)
        {
            return (from r in this
                where r.Id == id
                select r).FirstOrDefault<PopupRecord>();
        }
    }
}

