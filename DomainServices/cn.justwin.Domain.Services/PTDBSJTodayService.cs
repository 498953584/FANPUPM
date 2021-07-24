namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTDBSJTodayService : Repository<PTDBSJToday>
    {
        public PTDBSJToday GetById(int id)
        {
            return (from d in this
                where d.I_DBSJ_ID == id
                select d).FirstOrDefault<PTDBSJToday>();
        }
    }
}

