namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class SASalaryBooksService : Repository<SASalaryBooks>
    {
        public void DelSaMonthNoPayoff(string id)
        {
            string sql = string.Format("\r\n                    DELETE SA_MonthSalary WHERE ID IN\n                    (\n\t                    SELECT MonthSalary.ID FROM SA_MonthSalary MonthSalary\n\t                    LEFT JOIN SA_Payoff Payoff ON MonthSalary.UserCode=Payoff.UserCode \n\t                    AND MonthSalary.year=Payoff.year AND MonthSalary.Month=Payoff.Month\n\t                    INNER JOIN SA_UserSalaryBooks SaUserBooks ON MonthSalary.UserCode=SaUserBooks.UserCode\n\t                    WHERE (IsPayoff IS NULL OR IsPayoff = 0) AND Payoff.BooksId='{0}'\n                    )", id);
            base.ExcuteSql(sql);
        }

        public SASalaryBooks GetById(string id)
        {
            return (from sb in this
                where sb.Id == id
                select sb).FirstOrDefault<SASalaryBooks>();
        }

        public int GetCountSaMonthNoPayoff(string id)
        {
            try
            {
                string sql = string.Format("\r\n                    SELECT COUNT(*) FROM (\n\t                    SELECT MonthSalary.*,Payoff.IsPayoff FROM SA_MonthSalary MonthSalary\n\t                    LEFT JOIN SA_Payoff Payoff ON MonthSalary.UserCode=Payoff.UserCode \n\t                    AND MonthSalary.year=Payoff.year AND MonthSalary.Month=Payoff.Month\n\t                    INNER JOIN SA_UserSalaryBooks SaUserBooks ON MonthSalary.UserCode=SaUserBooks.UserCode\n\t                    WHERE (IsPayoff IS NULL OR IsPayoff = 0) AND BooksId='{0}'\n                    ) SaMonthInfo ", id);
                return Convert.ToInt32(base.ExcuteSql(sql).FirstOrDefault<object>());
            }
            catch
            {
                return 0;
            }
        }
    }
}

