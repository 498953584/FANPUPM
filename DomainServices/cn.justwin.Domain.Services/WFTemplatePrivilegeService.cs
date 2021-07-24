namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class WFTemplatePrivilegeService : Repository<WFTemplatePrivilege>
    {
        public WFTemplatePrivilege GetById(int id)
        {
            return (from p in this
                where p.PrivilegeId == id
                select p).FirstOrDefault<WFTemplatePrivilege>();
        }
    }
}

