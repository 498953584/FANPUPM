namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTDBSJService : Repository<PTDBSJ>
    {
        public PTDBSJ GetById(int id)
        {
            return (from d in this
                where d.I_DBSJ_ID == id
                select d).FirstOrDefault<PTDBSJ>();
        }
    }
}

