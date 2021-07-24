namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public class TechnologyStandardAction
    {
        public static bool AuditTechnologyCriterion(DataTable ArrData)
        {
            string str = " Begin ";
            foreach (DataRow row in ArrData.Rows)
            {
                string str2 = str;
                str = str2 + " Update Prj_TechnologyCriterion set IsAudited=" + row[1].ToString() + " where MainID = " + row[0].ToString();
            }
            return publicDbOpClass.NonQuerySqlString(str + " End ");
        }

        public static int GetMaxId()
        {
            string sqlString = "";
            sqlString = "select ISNULL(max(MainID),0)+1 from Prj_TechnologyCriterion";
            return (int) publicDbOpClass.ExecuteScalar(sqlString);
        }

        public static int GetStandardCount(string strPrjCode)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_TechnologyCriterion where PrjCode ='" + strPrjCode + "'");
        }

        public static DataTable GetStandardList(string strPrjCode)
        {
            string sqlWhere = "";
            sqlWhere = "(1=1)";
            if (strPrjCode.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (PrjCode ='" + strPrjCode + "')";
            }
            return publicDbOpClass.GetPageData(sqlWhere, "Prj_TechnologyCriterion", "MainID desc");
        }

        public static DataTable GetStandardList(string strPrjCode, int pageSize, int pageIndex, out int PageCount)
        {
            string strSql = "select * from Prj_TechnologyCriterion where IsAudited=1";
            PageCount = 1;
            if (strPrjCode.ToString() != "")
            {
                strSql = strSql + " and (PrjCode ='" + strPrjCode + "')";
            }
            strSql = strSql + " order by len(TechnologyCriterionID),TechnologyCriterionID";
            return publicDbOpClass.GetRecordFromPage(ref PageCount, strSql, "MainID", pageSize, pageIndex);
        }

        public static DataTable GetStandardOfSingle(string Id)
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_TechnologyCriterion where MainID='" + Id + "'");
        }

        public static int TechnologyAdd(TechnologyStandardInfo objInfo)
        {
            string str = "";
            str = "insert into Prj_TechnologyCriterion (MainID,PrjCode,TechnologyCriterionID,TechnologyClassify,";
            object obj2 = str + "TechnologyName,TechnologyPromulgateTime,Remark,TechGuid) values('";
            string str2 = string.Concat(new object[] { obj2, objInfo.MainId, "','", objInfo.PrjCode, "','", objInfo.TechnologyCriterionID, "','", objInfo.TechnologyClassify, "','" });
            return publicDbOpClass.ExecSqlString(str2 + objInfo.TechnologyName + "','" + objInfo.TechnologyPromulgateTime + "','" + objInfo.Remark + "','" + objInfo.TechGuid + "')");
        }

        public static int TechnologyDel(string strId)
        {
            string str = "";
            str = "begin";
            return publicDbOpClass.ExecSqlString(((str + " delete from XPM_Basic_AnnexList where ModuleID='1725' and RecordCode='" + strId + "'") + " delete from Prj_TechnologyCriterion where MainID='" + strId + "'") + " end");
        }

        public static int TechnologyUpd(TechnologyStandardInfo objInfo)
        {
            string str2 = "update Prj_TechnologyCriterion set TechnologyCriterionID='" + objInfo.TechnologyCriterionID + "',TechnologyClassify='";
            object obj2 = str2 + objInfo.TechnologyClassify + "',TechnologyName='" + objInfo.TechnologyName + "',TechnologyPromulgateTime='";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, objInfo.TechnologyPromulgateTime, "',Remark='", objInfo.Remark, "' where MainID='", objInfo.MainId, "'" }));
        }

        public static int TechStandardSelect(TechnologyStandardCollectin objColl)
        {
            string str = "";
            str = " begin ";
            foreach (TechnologyStandardInfo info in objColl)
            {
                str = str + " insert into Prj_TechnologyCriterion (MainID,PrjCode,TechnologyCriterionID,TechnologyClassify,";
                str = str + "TechnologyName,TechnologyPromulgateTime,Remark,MainKey) values('";
                object obj2 = str;
                str = string.Concat(new object[] { obj2, info.MainId, "','", info.PrjCode, "','", info.TechnologyCriterionID, "','", info.TechnologyClassify, "','" });
                string str2 = str;
                str = str2 + info.TechnologyName + "','" + info.TechnologyPromulgateTime + "','" + info.Remark + "'," + info.TechnologyCriterion.ToString() + ")";
            }
            return publicDbOpClass.ExecSqlString(str + " end ");
        }
    }
}

