namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class BudIndirectDiaryDetailsService : Repository<BudIndirectDiaryDetails>
    {
        public IList<BudIndirectDiaryDetails> GetByDearyId(string diaryId)
        {
            return (from p in this
                where p.InDiaryId == diaryId
                orderby p.No
                select p).ToList<BudIndirectDiaryDetails>();
        }

        public BudIndirectDiaryDetails GetById(string inDiaryDetailsId)
        {
            return (from i in this
                where i.InDiaryDetailsId == inDiaryDetailsId
                select i).FirstOrDefault<BudIndirectDiaryDetails>();
        }

        public BudIndirectDiaryDetails GetDearyId(string diaryId)
        {
            return (from i in this
                where i.InDiaryId == diaryId
                select i).FirstOrDefault<BudIndirectDiaryDetails>();
        }

        public DataTable GetTable(string id)
        {
            string format = "SELECT A.Id,A.Code,A.RptUser,B.Name,B.CBSCode,B.Cost,B.Note \r\n                           FROM Bud_PreReimburseApply  A\r\n                           LEFT JOIN  Bud_PreReimburseApplyDetail B\r\n                           ON A.Id=B.ApplyId\r\n                           WHERE A.Id='{0}'";
            format = string.Format(format, id);
            return base.ExecuteQuery(format, new SqlParameter[0]);
        }
    }
}

