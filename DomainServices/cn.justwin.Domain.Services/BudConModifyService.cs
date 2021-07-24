namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudConModifyService : Repository<BudConModify>
    {
        public void Delete(List<string> ids)
        {
            string str = string.Empty;
            foreach (string str2 in ids)
            {
                str = str + "'" + str2 + "',";
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.Length - 1);
            }
            string sql = string.Format("DELETE Bud_ConModify WHERE ModifyId IN ({0}) ", str);
            base.ExcuteSql(sql);
        }

        public BudConModify GetByConInModifyID(string conInModifyID)
        {
            return (from cm in this
                where cm.ConInModifyID == conInModifyID
                select cm).FirstOrDefault<BudConModify>();
        }

        public BudConModify GetById(string id)
        {
            return (from cm in this
                where cm.ModifyId == id
                select cm).FirstOrDefault<BudConModify>();
        }
    }
}

