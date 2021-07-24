namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class EquShipOilRecordService : Repository<EquShipOilRecord>
    {
        public EquShipOilRecord GetById(string id)
        {
            return (from t in this
                where t.RecordId.Equals(id)
                select t).FirstOrDefault<EquShipOilRecord>();
        }
    }
}

