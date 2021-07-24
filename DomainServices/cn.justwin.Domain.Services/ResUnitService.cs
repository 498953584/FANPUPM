namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class ResUnitService : Repository<ResUnit>
    {
        public ResUnit GetById(string id)
        {
            return (from u in this
                where u.UnitId == id
                select u).FirstOrDefault<ResUnit>();
        }
    }
}

