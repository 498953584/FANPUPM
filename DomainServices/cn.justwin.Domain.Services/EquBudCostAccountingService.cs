namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EquBudCostAccountingService : Repository<EquBudCostAccounting>
    {
        public EquBudCostAccounting GetById(string budId)
        {
            return (from t in this
                where t.BudId == budId
                select t).FirstOrDefault<EquBudCostAccounting>();
        }

        public IList<EquBudCostAccounting> GetEquBudListByEquId(string equId)
        {
            return (from t in this
                where t.BudId.Equals(equId)
                select t).ToList<EquBudCostAccounting>();
        }
    }
}

