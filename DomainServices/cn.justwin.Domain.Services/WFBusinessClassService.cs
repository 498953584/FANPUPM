namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class WFBusinessClassService : Repository<WFBusinessClass>
    {
        public WFBusinessClass GetById(string id)
        {
            return (from c in this
                where c.Id == id
                select c).FirstOrDefault<WFBusinessClass>();
        }
    }
}

