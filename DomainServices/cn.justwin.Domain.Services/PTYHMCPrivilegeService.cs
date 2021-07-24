namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PTYHMCPrivilegeService : Repository<PTYHMCPrivilege>
    {
        public void DeleteByMk(string mk)
        {
            string sql = string.Format("DELETE FROM PT_YHMC_Privilege WHERE C_MKDM = '{0}'", mk);
            base.ExcuteSql(sql);
        }

        public bool Exists(PTYHMCPrivilege yp)
        {
            return ((from y in this
                where (y.C_MKDM == yp.C_MKDM) && (y.V_YHDM == yp.V_YHDM)
                select y).Count<PTYHMCPrivilege>() > 0);
        }

        public PTYHMCPrivilege GetById(string id)
        {
            return (from p in this
                where p.Id == id
                select p).FirstOrDefault<PTYHMCPrivilege>();
        }

        public IList<string> GetUser(string mk)
        {
            return (from y in this
                where y.C_MKDM == mk
                select y.V_YHDM).ToList<string>();
        }
    }
}

