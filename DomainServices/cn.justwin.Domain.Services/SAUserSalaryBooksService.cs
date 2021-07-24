namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class SAUserSalaryBooksService : Repository<SAUserSalaryBooks>
    {
        public SAUserSalaryBooks GetById(string id)
        {
            return (from sub in this
                where sub.Id == id
                select sub).FirstOrDefault<SAUserSalaryBooks>();
        }
    }
}

