namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class ConPayoutModifyService : Repository<ConPayoutModify>
    {
        public ConPayoutModify GetById(string modifyId)
        {
            return (from m in this
                where m.ModifyID == modifyId
                select m).FirstOrDefault<ConPayoutModify>();
        }
    }
}

