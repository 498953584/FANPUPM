namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTPrjInfoEngineeringTypeService : Repository<PTPrjInfoEngineeringType>
    {
        public PTPrjInfoEngineeringType GetById(string id)
        {
            return (from t in this
                where t.ID == id
                select t).FirstOrDefault<PTPrjInfoEngineeringType>();
        }
    }
}

