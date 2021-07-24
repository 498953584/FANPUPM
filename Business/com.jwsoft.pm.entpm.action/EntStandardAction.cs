namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class EntStandardAction
    {
        public static int GetMaxId()
        {
            string sqlString = "";
            sqlString = "select ISNULL(max(MainKey),0)+1 as ss from Prj_EnterpriseTechnologyCriterion";
            return (int) publicDbOpClass.ExecuteScalar(sqlString);
        }

        public static int GetStandardCount(string classId)
        {
            return (int) publicDbOpClass.ExecuteScalar(" select count(1) from Prj_EnterpriseTechnologyCriterion where ClassID='" + classId + "'");
        }

        public static DataTable GetStandardList(string classId)
        {
            string str = "";
            if (classId.ToString() != "")
            {
                str = str + "(ClassID ='" + classId + "')";
            }
            return publicDbOpClass.GetPageData(str + " order by len(technologycriterionid),technologycriterionid ", "Prj_EnterpriseTechnologyCriterion");
        }

        public static DataTable GetStandardSingle(string Id)
        {
            return publicDbOpClass.DataTableQuary(" select * from Prj_EnterpriseTechnologyCriterion where MainKey='" + Id + "'");
        }

        public static int StandardAdd(EntStandardInfo objInfo)
        {
            string str = "";
            str = " insert into Prj_EnterpriseTechnologyCriterion (ClassID,TechnologyCriterionID,TechnologyName,TechnologyClassify,TechnologyPromulgateTime,Remark,EnterGuid) values('";
            string str2 = str;
            string str3 = str2 + objInfo.ClassID.ToString() + "','" + objInfo.TechnologyCriterionID + "','" + objInfo.TechnologyName + "','" + objInfo.TechnologyClassify.ToString() + "','";
            return publicDbOpClass.ExecSqlString(str3 + objInfo.TechnologyPromulgateTime + "','" + objInfo.Remark + "','" + objInfo.EnterGuid + "')");
        }

        public static int StandDel(string Id)
        {
            string str = "";
            str = " begin";
            return publicDbOpClass.ExecSqlString(((str + " delete from XPM_Basic_AnnexList where RecordCode ='" + Id + "' and ModuleID='1721'") + " delete from Prj_EnterpriseTechnologyCriterion where MainKey='" + Id + "'") + " end");
        }

        public static int StandUpd(EntStandardInfo objInfo, string Id)
        {
            string str2 = " Begin ";
            string str3 = str2 + " update Prj_EnterpriseTechnologyCriterion set TechnologyCriterionID='" + objInfo.TechnologyCriterionID + "',TechnologyName='" + objInfo.TechnologyName;
            string str4 = str3 + "',TechnologyClassify='" + objInfo.TechnologyClassify.ToString() + "',TechnologyPromulgateTime='" + objInfo.TechnologyPromulgateTime;
            string str5 = str4 + "',Remark='" + objInfo.Remark + "' where MainKey ='" + Id + "'";
            return publicDbOpClass.ExecSqlString((str5 + " update Prj_TechnologyCriterion set TechnologyCriterionID='" + objInfo.TechnologyCriterionID + "',TechnologyClassify='" + objInfo.TechnologyClassify + "',TechnologyName='" + objInfo.TechnologyName + "',TechnologyPromulgateTime='" + objInfo.TechnologyPromulgateTime + "' where MainKey=" + Id) + " End");
        }
    }
}

