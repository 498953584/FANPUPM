namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class SASalaryItemService : Repository<SASalaryItem>
    {
        public SASalaryItem GetById(string id)
        {
            return (from si in this
                where si.Id == id
                select si).FirstOrDefault<SASalaryItem>();
        }
    }
}

