namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WFInstanceService : Repository<WFInstance>
    {
        public WFInstance GetById(int noteId)
        {
            return (from i in this
                where i.NoteID == noteId
                select i).FirstOrDefault<WFInstance>();
        }

        public IList<WFInstance> GetByInstanceId(int insMainId)
        {
            return (from i in this
                where i.ID.Value == insMainId
                select i).ToList<WFInstance>();
        }

        public IList<WFInstance> GetByNodeId(int insMainId, int nodeId)
        {
            return (from i in this
                where (i.ID.Value == insMainId) && (i.NodeID.Value == nodeId)
                select i).ToList<WFInstance>();
        }
    }
}

