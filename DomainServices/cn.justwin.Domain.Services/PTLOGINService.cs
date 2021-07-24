namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTLOGINService : Repository<PTLOGIN>
    {
        public PTLOGIN GetById(string id)
        {
            return (from p in this
                where p.V_DLID == id
                select p).FirstOrDefault<PTLOGIN>();
        }

        public PTLOGIN GetByUserCode(string code)
        {
            return (from p in this
                where p.V_YHDM == code
                select p).FirstOrDefault<PTLOGIN>();
        }
    }
}

