namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConBalanceItemService : Repository<ConBalanceItem>
    {
        public void DeleteByBalanceId(string balanceId)
        {
            base.ExcuteSql("DELETE Con_BalanceItem WHERE BalanceId='" + balanceId + "'");
        }

        public List<ConBalanceItem> GetListByBalanceId(string balanceId)
        {
            return (from cb in this
                where cb.BalanceId == balanceId
                select cb).ToList<ConBalanceItem>();
        }
    }
}

