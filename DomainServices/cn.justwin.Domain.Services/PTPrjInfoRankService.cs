namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PTPrjInfoRankService : Repository<PTPrjInfoRank>
    {
        public PTPrjInfoRank GetById(string id)
        {
            return (from r in this
                where r.RankId == id
                select r).FirstOrDefault<PTPrjInfoRank>();
        }

        public IList<PTPrjInfoRank> GetByPrjGuid(Guid guid)
        {
            return (from r in this
                where r.PrjGuid == guid
                select r).ToList<PTPrjInfoRank>();
        }
    }
}

