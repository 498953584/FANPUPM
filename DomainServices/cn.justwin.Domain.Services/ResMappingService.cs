namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ResMappingService : Repository<ResMapping>
    {
        public void Delete(IList<string> idLst)
        {
            foreach (string str in idLst)
            {
                ResMapping byId = this.GetById(str);
                if (byId != null)
                {
                    base.Delete(byId);
                }
            }
        }

        public void DeleteFromParentId(string parentId)
        {
            string sql = string.Format("DELETE FROM Res_Mapping WHERE ParentId = '{0}'", parentId);
            base.ExcuteSql(sql);
        }

        public ResMapping GetById(string id)
        {
            return (from m in this
                where m.Id == id
                select m).First<ResMapping>();
        }
    }
}

