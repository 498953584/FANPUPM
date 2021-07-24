namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class SAPayoffService : Repository<SAPayoff>
    {
        public void DelSaMonthNoPayoff(string userCode)
        {
            string sql = string.Format("\r\n                    DELETE saMonth FROM SA_MonthSalary saMonth\r\n                    LEFT JOIN SA_Payoff saPayOff ON saMonth.UserCode=saPayOff.UserCode \r\n                    AND saMonth.YEAR=saPayOff.YEAR AND saMonth.Month=saPayOff.Month\r\n                    where saMonth.UserCode='{0}'\r\n                    AND (IsPayoff!=1 OR IsPayoff IS NULL)\r\n                    ", userCode);
            base.ExcuteSql(sql);
        }

        public SAPayoff GetById(string id)
        {
            return (from p in this
                where p.Id == id
                select p).FirstOrDefault<SAPayoff>();
        }
    }
}

