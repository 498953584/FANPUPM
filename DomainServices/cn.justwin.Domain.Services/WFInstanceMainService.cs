namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WFInstanceMainService : Repository<WFInstanceMain>
    {
        public WFInstanceMain GetById(int id)
        {
            return (from m in this
                where m.ID == id
                select m).FirstOrDefault<WFInstanceMain>();
        }

        public IList<WFInstanceMain> GetByInstanceCode(object code)
        {
            Guid guid = new Guid(code.ToString());
            return (from m in this
                where m.InstanceCode.Value == guid
                orderby m.ID
                select m).ToList<WFInstanceMain>();
        }

        public bool IsReSubmit(string insCode, string businessCode)
        {
            WFBusinessCodeService service = new WFBusinessCodeService();
            WFBusinessCode code = (from c in service
                where c.BusinessCode == businessCode
                select c).FirstOrDefault<WFBusinessCode>();
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2} = '{3}'", new object[] { code.StateField, code.LinkTable, code.PrimaryField, insCode });
            IList<object> list = base.ExcuteSql(sql);
            if ((list == null) || (list.Count == 0))
            {
                throw new Exception("提交错误");
            }
            string str2 = list[0].ToString();
            if ((!(str2 == "0") && !(str2 == "1")) && !(str2 == "-2"))
            {
                return false;
            }
            return true;
        }

        public void UpdateBusinessData(string insCode, string businessCode, int flowSate)
        {
            WFBusinessCodeService service = new WFBusinessCodeService();
            WFBusinessCode code = (from c in service
                where c.BusinessCode == businessCode
                select c).FirstOrDefault<WFBusinessCode>();
            string sql = string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = '{4}'", new object[] { code.LinkTable, code.StateField, flowSate, code.PrimaryField, insCode });
            base.ExcuteSql(sql);
        }
    }
}

