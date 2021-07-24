namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class WFTemplateService : Repository<WFTemplate>
    {
        public WFTemplate GetById(int id)
        {
            return (from t in this
                where t.TemplateID == id
                select t).FirstOrDefault<WFTemplate>();
        }
    }
}

