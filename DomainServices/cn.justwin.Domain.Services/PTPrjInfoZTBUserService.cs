namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PTPrjInfoZTBUserService : Repository<PTPrjInfoZTBUser>
    {
        public void Delete(string prjGuid)
        {
            string sql = string.Format("DELETE FROM PT_PrjInfo_ZTB_User WHERE PrjGuid = '{0}'", prjGuid);
            base.ExcuteSql(sql);
        }

        public bool Exists(PTPrjInfoZTBUser tu)
        {
            return ((from u in this
                where (u.PrjGuid == tu.PrjGuid) && (u.UserCode == tu.UserCode)
                select u).Count<PTPrjInfoZTBUser>() > 0);
        }

        public IList<string> GetPrjByUserCode(string userCode)
        {
            return (from u in this
                where u.UserCode == userCode
                select u.PrjGuid.ToUpper()).Distinct<string>().ToList<string>();
        }

        public IList<string> GetUser(string prjGuid)
        {
            return (from u in this
                where u.PrjGuid == prjGuid
                select u.UserCode).Distinct<string>().ToList<string>();
        }

        public List<string> GetUserCodesByPrjID(string prjId)
        {
            return (from p in this
                where p.PrjGuid == prjId
                select p.UserCode).Distinct<string>().ToList<string>();
        }

        public string GetUserNamesByGuid(string Guid)
        {
            string sql = "SELECT STUFF((SELECT '、'+[v_xm] FROM PT_PrjInfo_ZTB_User \r\n                               LEFT JOIN PT_yhmc ON PT_yhmc.v_yhdm=PT_PrjInfo_ZTB_User.UserCode  WHERE PrjGuid=ZU.PrjGuid FOR XML PATH('')),1,1,'')\r\n                               FROM PT_PrjInfo_ZTB_User AS ZU\r\n                               WHERE PrjGuid= '" + Guid + "' GROUP BY [PrjGuid]";
            IList<object> list = base.ExcuteSql(sql);
            if (list.Count > 0)
            {
                return list[0].ToString();
            }
            return "";
        }
    }
}

