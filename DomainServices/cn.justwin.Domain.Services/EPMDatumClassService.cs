namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class EPMDatumClassService : Repository<EPMDatumClass>
    {
        public EPMDatumClass GetById(int typeId)
        {
            return (from c in this
                where c.TypeId == typeId
                select c).FirstOrDefault<EPMDatumClass>();
        }
    }
}

