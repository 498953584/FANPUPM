namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PrivBusiDataRoleService : Repository<PrivBusiDataRole>
    {
        public void DeleteByRes(string resId)
        {
            string sql = string.Format("DELETE FROM Priv_BusiDataRole WHERE BusiDataId = '{0}' ", resId);
            base.ExcuteSql(sql);
        }

        public bool Exists(PrivBusiDataRole busiData)
        {
            return ((from r in this
                where ((r.TableName == busiData.TableName) && (r.BusiDataId == busiData.BusiDataId)) && (r.RoleId == busiData.RoleId)
                select r).Count<PrivBusiDataRole>() > 0);
        }

        public IList<string> GetBusiData(string userCode, string tableName)
        {
            IList<string> roleByUserCode = new PrivUserRoleService().GetRoleByUserCode(userCode);
            return this.GetBusiData(roleByUserCode, tableName);
        }

        private IList<string> GetBusiData(IList<string> roleList, string tableName)
        {
            return (from r in this
                where (r.TableName == tableName) && roleList.Contains(r.RoleId)
                select r.BusiDataId.ToUpper()).ToList<string>();
        }

        public PrivBusiDataRole GetById(string id)
        {
            return (from r in this
                where r.Id == id
                select r).FirstOrDefault<PrivBusiDataRole>();
        }

        public IList<string> GetRoleId(string busiDataId, string tableName)
        {
            return (from r in this
                where (r.BusiDataId == busiDataId) && (r.TableName == tableName)
                select r.RoleId).ToList<string>();
        }

        public string GetRoleNamesByBusiDataRole(string busiData)
        {
            string sql = "SELECT STUFF((SELECT '、'+[Name] FROM Priv_BusiDataRole \r\n                           LEFT JOIN Priv_Role on  Priv_Role.Id=Priv_BusiDataRole.RoleId \r\n                           WHERE BusiDataId=BDR.BusiDataId FOR XML PATH('')),1,1,'')\r\n                           FROM Priv_BusiDataRole AS BDR \r\n                           WHERE BDR.TableName='PT_PrjInfo_ZTB_Detail'\r\n                           AND BusiDataId='" + busiData + "' GROUP BY [BusiDataId]";
            IList<object> list = base.ExcuteSql(sql);
            if (list.Count > 0)
            {
                return list[0].ToString();
            }
            return "";
        }

        public List<string> GetUserCode(string prjId)
        {
            List<string> list = new List<string>();
            string sql = "select distinct UserCode from Priv_UserRole \r\n                            left join Priv_BusiDataRole  on Priv_UserRole.RoleId=Priv_BusiDataRole.RoleId\r\n                            where  BusiDataId='" + prjId + "' and tableName='PT_PrjInfo_ZTB_Detail'";
            IList<object> list2 = base.ExcuteSql(sql);
            if (list2.Count > 0)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    list.Add(list2[i].ToString());
                }
            }
            return list;
        }
    }
}

