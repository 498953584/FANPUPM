namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTRoleService : Repository<PTRole>
    {
        public PTRole GetById(int id)
        {
            return (from r in this
                where r.RoleCode == id
                select r).FirstOrDefault<PTRole>();
        }
    }
}

