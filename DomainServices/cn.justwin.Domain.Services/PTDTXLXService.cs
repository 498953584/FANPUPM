namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTDTXLXService : Repository<PTDTXLX>
    {
        public PTDTXLX GetById(string id)
        {
            return (from l in this
                where l.V_LXBM == id
                select l).FirstOrDefault<PTDTXLX>();
        }
    }
}

