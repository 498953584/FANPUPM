namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudConModifyTaskService : Repository<BudConModifyTask>
    {
        public void DelByModifyTaskId(string modifyTaskId)
        {
            string sql = string.Format("DELETE Bud_ConModifyTask WHERE ModifyTaskId='{0}'", modifyTaskId);
            base.ExcuteSql(sql);
        }

        public void DelModifyTask(string modifyId)
        {
            string sql = string.Format("DELETE Bud_ConModifyTask WHERE ModifyId='{0}'", modifyId);
            base.ExcuteSql(sql);
        }

        public BudConModifyTask GetById(string id)
        {
            return (from cmt in this
                where cmt.ModifyTaskId == id
                select cmt).FirstOrDefault<BudConModifyTask>();
        }

        public List<BudConModifyTask> GetListByModifyId(string modifyId)
        {
            return (from ct in this
                where ct.ModifyId == modifyId
                select ct).ToList<BudConModifyTask>();
        }

        public string GetOrderNumber(string prjId, string parentId)
        {
            string str = "001";
            BudConModifyService source = new BudConModifyService();
            if (string.IsNullOrEmpty(parentId))
            {
                string aa=(from mts in this
                    join ms in source.AsQueryable<BudConModify>() on mts.ModifyId equals ms.ModifyId 
                    where ms.PrjId == prjId && mts.TaskId == null
                    select mts.OrderNumber).Max<string>();

                int num;
                int.TryParse(aa, out num);


                string str2 = (num + 1).ToString();
                if (str2.Length == 1)
                {
                    str2 = "00" + str2;
                }
                if (str2.Length == 2)
                {
                    str2 = "0" + str2;
                }
                return str2;
            }
            IQueryable<string> queryable2 = from mts in this
                where mts.ParentId == parentId
                select mts.OrderNumber;
            if (queryable2.Count<string>() > 0)
            {
                string str3 = queryable2.Max<string>();
                string str4 = str3.Substring(0, str3.Length - 3);
                string str6 = (Convert.ToInt32(str3.Substring(str3.Length - 3, 3)) + 1).ToString();
                if (str6.Length == 1)
                {
                    str6 = "00" + str6;
                }
                if (str6.Length == 2)
                {
                    str6 = "0" + str6;
                }
                return (str4 + str6);
            }
            IQueryable<string> queryable3 = from mts in this
                where mts.ModifyTaskId == parentId
                select mts.OrderNumber;
            if (queryable3.Count<string>() > 0)
            {
                str = queryable3.First<string>() + "001";
            }
            return str;
        }
    }
}

