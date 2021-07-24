namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PrivRoleService : Repository<PrivRole>
    {
        public PrivRole GetById(string id)
        {
            return (from r in this
                where r.Id == id
                select r).FirstOrDefault<PrivRole>();
        }

        public int GetNextNo()
        {
            if (this.Count<PrivRole>() == 0)
            {
                return 1;
            }
            return ((from r in this
                orderby r.No descending
                select r).First<PrivRole>().No + 1);
        }
    }
}

