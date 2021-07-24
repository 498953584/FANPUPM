namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BasicCodeTypeService : Repository<BasicCodeType>
    {
        public BasicCodeType GetById(string code)
        {
            return (from t in this
                where t.TypeCode == code
                select t).FirstOrDefault<BasicCodeType>();
        }
    }
}

