namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTStopMsgService : Repository<PTStopMsg>
    {
        public PTStopMsg GetById(string id)
        {
            return (from sr in this
                where sr.StopMsgId == id
                select sr).FirstOrDefault<PTStopMsg>();
        }
    }
}

