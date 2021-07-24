namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PTPrjInfoKindService : Repository<PTPrjInfoKind>
    {
        public PTPrjInfoKind GetById(string id)
        {
            return (from k in this
                where k.KindId == id
                select k).FirstOrDefault<PTPrjInfoKind>();
        }

        public IList<PTPrjInfoKind> GetByPrjGuid(Guid guid)
        {
            return (from k in this
                where k.PrjGuid == guid
                select k).ToList<PTPrjInfoKind>();
        }
    }
}

