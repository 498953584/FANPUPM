namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class XPMBasicContactCorpService : Repository<XPMBasicContactCorp>
    {
        public IList<XPMBasicContactCorp> GetByCorpTypeId(int corpTypeId)
        {
            return (from c in this
                where ((c.CorpTypeID == corpTypeId) && (c.IsValid == true)) && (c.IsVisible == true)
                orderby c.CorpID
                select c).ToList<XPMBasicContactCorp>();
        }

        public XPMBasicContactCorp GetById(int id)
        {
            return (from c in this
                where c.CorpID == id
                select c).FirstOrDefault<XPMBasicContactCorp>();
        }
    }
}

