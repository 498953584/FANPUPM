namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BasicAreaService : Repository<BasicArea>
    {
        public BasicArea GetAreaByID(string areaId)
        {
            Guid areaID = new Guid(areaId);
            return (from p in this
                where p.Id == areaID
                select p).FirstOrDefault<BasicArea>();
        }
    }
}

