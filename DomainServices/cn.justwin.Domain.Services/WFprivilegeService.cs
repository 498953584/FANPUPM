namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WFprivilegeService : Repository<WFprivilege>
    {
        public void Delete(string busiClass)
        {
            string sql = string.Format("DELETE FROM wf_privilege WHERE businessClass = '{0}'", busiClass);
            base.ExcuteSql(sql);
        }

        public bool Exists(WFprivilege priv)
        {
            return ((from p in this
                where (p.businessClass == priv.businessClass) && (p.userlist == priv.userlist)
                select p).Count<WFprivilege>() > 0);
        }

        public WFprivilege GetById(string id)
        {
            int _id = Convert.ToInt32(id);
            return (from p in this
                where p.wp_id == _id
                select p).FirstOrDefault<WFprivilege>();
        }

        public IList<string> GetUser(string busiClass)
        {
            return (from p in this
                where p.businessClass == busiClass
                select p.userlist).ToList<string>();
        }
    }
}

