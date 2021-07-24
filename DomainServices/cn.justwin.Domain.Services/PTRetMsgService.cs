namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTRetMsgService : Repository<PTRetMsg>
    {
        public PTRetMsg GetById(string id)
        {
            return (from sr in this
                where sr.RetMsgId == id
                select sr).FirstOrDefault<PTRetMsg>();
        }
    }
}

