namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTPrjMemberService : Repository<PTPrjMember>
    {
        public PTPrjMember GetById(string prjMemberId)
        {
            Guid prjmemberID = new Guid(prjMemberId);
            return (from p in this
                where p.PrjMemberId == prjmemberID
                select p).FirstOrDefault<PTPrjMember>();
        }
    }
}

