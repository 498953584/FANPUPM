namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;

    public class BudPreReimburseModifyDetailService : Repository<BudPreReimburseModifyDetail>
    {
        public DataTable GetTable(string id)
        {
            string format = "SELECT A.Id,A.Code,A.RptUser,B.Name,B.CBSCode,B.Cost,B.Note \r\n                           FROM Bud_PreReimburseApply  A\r\n                           LEFT JOIN  Bud_PreReimburseApplyDetail B\r\n                           ON A.Id=B.ApplyId\r\n                           WHERE A.Id='{0}'";
            format = string.Format(format, id);
            return base.ExecuteQuery(format);
        }
    }
}

