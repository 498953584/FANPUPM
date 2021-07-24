namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudModifyService : Repository<BudModify>
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
            string sql = string.Format("DELETE Bud_Modify WHERE ModifyId IN ({0}) ", str);
            base.ExcuteSql(sql);
        }

        public BudModify GetById(string id)
        {
            return (from bm in this
                where bm.ModifyId == id
                select bm).FirstOrDefault<BudModify>();
        }
    }
}

