namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class ResResourceTypeService : Repository<ResResourceType>
    {
        public ResResourceType GetById(string id)
        {
            return (from t in this
                where t.ResourceTypeId == id
                select t).FirstOrDefault<ResResourceType>();
        }
    }
}

