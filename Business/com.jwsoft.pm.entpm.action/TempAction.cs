namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;

    public class TempAction
    {
        public static bool ClearTempResource(Guid operateSession, string userCode)
        {
            string format = "";
            format = "delete from EPM_Res_TempResource where SessionCode = '{0}' and userCode = '{1}'";
            return publicDbOpClass.NonQuerySqlString(string.Format(format, operateSession, userCode));
        }

        private string ContactResourceCode(ArrayList resourceCodeList)
        {
            string str = "";
            int count = resourceCodeList.Count;
            for (int i = 0; i < count; i++)
            {
                if (resourceCodeList[i].ToString().Trim() != "")
                {
                    if (i == (count - 1))
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, "'", resourceCodeList[i], "'" });
                    }
                    else
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "'", resourceCodeList[i], "'," });
                    }
                }
            }
            return str;
        }

        public int DelSelectedResource(Guid operateSession, string userCode, ArrayList resourceCodeList)
        {
            string str = this.ContactResourceCode(resourceCodeList);
            if (str == "")
            {
                return 1;
            }
            string str2 = "";
            return publicDbOpClass.ExecSqlString(string.Format((str2 + " begin " + " update EPM_Res_TempResource set State=0 where (SessionCode='{0}')and(UserCode='{1}')and(ResourceCode in ({2}))and(State=1) ") + " delete from EPM_Res_TempResource where (SessionCode='{0}')and(UserCode='{1}')and(ResourceCode in ({2}))and(State=2) " + " end ", operateSession.ToString(), userCode, str));
        }

        public DataTable dtTempResourceList(Guid operateSession, string userCode)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from EPM_Res_TempResource where (SessionCode = '", operateSession, "') and (userCode = '", userCode, "') and (State>=1) " })))
            {
                return table;
            }
        }

        private ResourceBasicInfo GetResourceBasicInfoFromDataRow(DataRow dr)
        {
            ResourceBasicInfo info = new ResourceBasicInfo {
                ResourceCode = dr["ResourceCode"].ToString(),
                ResourceName = dr["ResourceName"].ToString(),
                Specification = dr["Specification"].ToString()
            };
            if (dr["UnitID"] != DBNull.Value)
            {
                info.UnitID = (int) dr["UnitID"];
            }
            if (dr["UnitName"] != DBNull.Value)
            {
                info.UnitName = dr["UnitName"].ToString();
            }
            if (dr["PriceItemID"] != DBNull.Value)
            {
                info.PriceItemID = (int) dr["PriceItemID"];
            }
            if (dr["PriceItemName"] != DBNull.Value)
            {
                info.PriceItemName = dr["PriceItemName"].ToString();
            }
            if (dr["Price"] != DBNull.Value)
            {
                info.Price = (decimal) dr["Price"];
            }
            if (dr["CategoryCode"] != DBNull.Value)
            {
                info.CategoryCode = dr["CategoryCode"].ToString();
            }
            if (dr["CategoryName"] != DBNull.Value)
            {
                info.CategoryName = dr["CategoryName"].ToString();
            }
            return info;
        }

        public ResourceBasicCollection QueryResourceBasicList(string categoryCode)
        {
            ResourceBasicCollection basics = new ResourceBasicCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_V_Res_ResourceBasic where CategoryCode = '" + categoryCode + "'"))
            {
                if (table.Rows.Count <= 0)
                {
                    return basics;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    basics.Add(this.GetResourceBasicInfoFromDataRow(table.Rows[i]));
                }
            }
            return basics;
        }

        public ResourceBasicCollection QueryValidTempResourceList(Guid operateSession, string userCode)
        {
            ResourceBasicCollection basics = new ResourceBasicCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from EPM_Res_TempResource where (SessionCode = '", operateSession, "') and (userCode = '", userCode, "') and (State>=1)" })))
            {
                if (table.Rows.Count <= 0)
                {
                    return basics;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    basics.Add(this.GetResourceBasicInfoFromDataRow(table.Rows[i]));
                }
            }
            return basics;
        }

        public int UndoModifyTempResource(Guid operateSession, string userCode)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(string.Format((str + " begin " + " update EPM_Res_TempResource set state = 1 where (SessionCode='{0}') and (UserCode='{1}') and (State=0) ") + " delete from EPM_Res_TempResource where (SessionCode = '{0}') and (userCode = '{1}') and (State=2) " + " end ", operateSession.ToString(), userCode));
        }
    }
}

