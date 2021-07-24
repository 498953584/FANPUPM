namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class SAMonthSalaryService : Repository<SAMonthSalary>
    {
        public SAMonthSalary GetById(string id)
        {
            return (from sms in this
                where sms.Id == id
                select sms).FirstOrDefault<SAMonthSalary>();
        }
    }
}

