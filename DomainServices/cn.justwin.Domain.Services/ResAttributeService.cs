namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class ResAttributeService : Repository<ResAttribute>
    {
        public ResAttribute GetById(string id)
        {
            return (from a in this
                where a.AttributeId == id
                select a).FirstOrDefault<ResAttribute>();
        }
    }
}

