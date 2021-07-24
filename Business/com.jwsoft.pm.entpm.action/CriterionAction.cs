namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class CriterionAction
    {
        public static int ADD(CriterionModel CM)
        {
            object obj2 = string.Concat(new object[] { " insert EPM_Datum_Criterion values ('", CM.CriterionCode, "','", CM.CriterionName, "', " });
            string str2 = string.Concat(new object[] { obj2, " '", CM.PublishYhdm, "','", CM.PublishDate, "','", CM.Publisher, "', " });
            return publicDbOpClass.ExecSqlString(str2 + " '" + CM.Remark + "','" + CM.Flage + "')");
        }

        public static int Del(Guid Code, string ID)
        {
            string str = " delete from EPM_Datum_Criterion";
            object obj2 = str;
            object obj3 = string.Concat(new object[] { obj2, " where CriterionCode = '", Code, "'" }) + " delete from XPM_Basic_AnnexList";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, " where RecordCode = '", Code, "' and ModuleID = '", ID, "'" }));
        }

        public void DelFiles(string Code, string ID)
        {
            string str2 = " select replace(FilePath,'','/') as FilePath from XPM_Basic_AnnexList ";
            DataTable table = publicDbOpClass.DataTableQuary(str2 + " where RecordCode = '" + Code + "' and ModuleId = '" + ID + "'");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                QualitySafetyFileUp.DelFile(table.Rows[0][0].ToString());
            }
        }

        private static CriterionModel GetCriterionInfoFromDataRow(DataRow dr)
        {
            return new CriterionModel { CriterionCode = new Guid(dr["CriterionCode"].ToString()), CriterionName = dr["CriterionName"].ToString(), PublishYhdm = dr["PublishYhdm"].ToString(), Publisher = dr["Publisher"].ToString(), PublishDate = (DateTime) dr["PublishDate"], Remark = dr["Remark"].ToString(), Flage = dr["DatumClass"].ToString() };
        }

        public static DataTable GetPageData(string Flage, string DDLLookup, string TxtLookup)
        {
            return publicDbOpClass.GetPageData(GetSqlWhereOverload(Flage, DDLLookup, TxtLookup), "EPM_Datum_Criterion", "PublishDate desc");
        }

        public static CriterionModel GetSingle(Guid Code)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary(" select * from EPM_Datum_Criterion where CriterionCode = '" + Code + "'"))
            {
                return GetCriterionInfoFromDataRow(table.Rows[0]);
            }
        }

        public static string GetSqlWhere(string Flage, string DDLLookup, string TxtLookup)
        {
            string str = " DatumClass = '" + Flage + "'";
            string str2 = DDLLookup;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    if (str2 != "3")
                    {
                        return str;
                    }
                    return (str + " and Remark like '%" + TxtLookup + "%'");
                }
            }
            else
            {
                return (str + " and CriterionName like '%" + TxtLookup + "%'");
            }
            return (str + " and Publisher IN (select i_bmdm from pt_d_bm where V_BMMC LIKE '%" + TxtLookup + "%') ");
        }

        public static string GetSqlWhereOverload(string Flage, string DDLLookup, string TxtLookup)
        {
            string str = " DatumClass in(" + Flage + ")";
            string str2 = DDLLookup;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    if (str2 != "3")
                    {
                        return str;
                    }
                    return (str + " and Remark like '%" + TxtLookup + "%'");
                }
            }
            else
            {
                return (str + " and CriterionName like '%" + TxtLookup + "%'");
            }
            return (str + " and Publisher IN (select i_bmdm from pt_d_bm where V_BMMC LIKE '%" + TxtLookup + "%') ");
        }

        public static int Update(CriterionModel CM)
        {
            string str2 = " update EPM_Datum_Criterion set CriterionName = '" + CM.CriterionName + "',";
            object obj2 = (str2 + " Publisher = '" + CM.Publisher + "',Remark = '" + CM.Remark + "',") + "DatumClass = " + CM.Flage;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " where CriterionCode = '", CM.CriterionCode, "'" }));
        }
    }
}

