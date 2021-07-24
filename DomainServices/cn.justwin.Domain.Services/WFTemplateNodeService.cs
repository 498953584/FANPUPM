namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WFTemplateNodeService : Repository<WFTemplateNode>
    {
        public WFTemplateNode GetById(string id)
        {
            return (from t in this
                where t.Id == id
                select t).FirstOrDefault<WFTemplateNode>();
        }

        public WFTemplateNode GetByNodeId(int nodeId)
        {
            return (from t in this
                where t.NodeID == nodeId
                select t).FirstOrDefault<WFTemplateNode>();
        }

        public IList<WFTemplateNode> GetNodes(int templateId)
        {
            return (from t in this
                where t.TemplateID == templateId
                select t).ToList<WFTemplateNode>();
        }
    }
}

