namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BasicConfigService : Repository<BasicConfig>
    {
        public BasicConfig GetById(string id)
        {
            return (from c in this
                where c.Id == id
                select c).FirstOrDefault<BasicConfig>();
        }

        public BasicConfig GetByName(string name)
        {
            return (from c in this
                where c.ParaName == name
                select c).FirstOrDefault<BasicConfig>();
        }

        public string GetValue(string name)
        {
            return (from c in this
                where c.ParaName == name
                select c.ParaValue).First<string>();
        }
    }
}

