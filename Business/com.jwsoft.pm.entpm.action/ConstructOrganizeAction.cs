namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ConstructOrganizeAction
    {
        public static int Add(ConstructOrganizeInfo objInfo)
        {
            string str2 = (((((" insert into Prj_TechnologyConstructOrganize(Id,FillUnit,PrjCode,WeavePerson,WeaveTime,Maindescripe,Remark,TCO_Name,FlowGuid) values ('" + objInfo.Id + "','") + objInfo.FillUnit + "','") + objInfo.PrjCode + "','") + objInfo.WeavePerson + "','") + objInfo.WeaveTime + "','") + objInfo.Manidescripe + "','";
            return publicDbOpClass.ExecSqlString((str2 + objInfo.Remark + "','" + objInfo.TCO_Name + "','") + objInfo.FlowGuid + "')");
        }

        public static int Del(string id)
        {
            string str = "";
            if (((int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_TechnologyConstructOrganize where (PPMAuditResult='0' or EntAuditResult='0') and (Id='" + id + "')")) > 0)
            {
                return 2;
            }
            str = "begin";
            return publicDbOpClass.ExecSqlString(((str + " delete from XPM_Basic_AnnexList where RecordCode ='" + id + "' and ModuleID='1720'") + "delete from Prj_TechnologyConstructOrganize where Id='" + id + "'") + " end");
        }

        public static int EntCancel(string id)
        {
            return publicDbOpClass.ExecSqlString("update  Prj_TechnologyConstructOrganize set EntAuditResult='1' where Id='" + id + "'");
        }

        public static int GetContructCount(string strPrjCode)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_V_ScienceInnovate where PrjCode ='" + strPrjCode + "'");
        }

        public static DataTable GetContructList(string strPrjCode)
        {
            string sqlWhere = "";
            sqlWhere = "(1=1)";
            if (strPrjCode.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (PrjCode ='" + strPrjCode + "')";
            }
            return publicDbOpClass.GetPageData(sqlWhere, "Prj_V_ScienceInnovate", "Id desc");
        }

        public static int GetEntContructCount(string strPrjCode)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_V_ScienceInnovate where PrjCode ='" + strPrjCode + "' and PPMAuditResult=0");
        }

        public static DataTable GetEntContructList(string strPrjCode)
        {
            string sqlWhere = "";
            sqlWhere = "(1=1) and PPMAuditResult=0";
            if (strPrjCode.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (PrjCode ='" + strPrjCode + "')";
            }
            return publicDbOpClass.GetPageData(sqlWhere, "Prj_V_ScienceInnovate", "Id desc");
        }

        public static int GetMaxId()
        {
            string sqlString = "";
            sqlString = "select ISNULL(max(Id),0)+1 from Prj_TechnologyConstructOrganize";
            return (int) publicDbOpClass.ExecuteScalar(sqlString);
        }

        public static DataTable GetSingleConOrgInfo(string strId)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from Prj_V_ScienceInnovate where Id ='" + strId + "'");
            new DataTable();
            return table;
        }

        public static int PPMCancel(string id)
        {
            return publicDbOpClass.ExecSqlString("update  Prj_TechnologyConstructOrganize set PPMAuditResult='1' where Id='" + id + "'");
        }

        public static int SetEntAudit(ConstructOrganizeInfo objInfo)
        {
            object obj2 = "update Prj_TechnologyConstructOrganize set EntAuditPerson='" + objInfo.EntAuditPerson + "',EntAuditTime='" + objInfo.EntAuditTime.ToShortDateString() + "',EntAuditResult='" + objInfo.EntAuditResult;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "',EntAuditIdea='", objInfo.EntAuditIdea, "',EntAuditRemark='", objInfo.EntAuditRemark, "' where Id='", objInfo.Id, "'" }));
        }

        public static int SetPPMAudit(ConstructOrganizeInfo objInfo)
        {
            object obj2 = "update Prj_TechnologyConstructOrganize set PPMAuditPerson='" + objInfo.PPMAuditPerson + "',PPMAuditTime='" + objInfo.PPMAuditTime.ToShortDateString() + "',PPMAuditResult='" + objInfo.PPMAuditResult;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "',PPMAuditIdea='", objInfo.PPMAuditIdea, "',PPMAuditRemark='", objInfo.PPMAuditRemark, "' where Id='", objInfo.Id, "'" }));
        }

        public static int Update(ConstructOrganizeInfo objInfo)
        {
            object obj2 = " update Prj_TechnologyConstructOrganize set TCO_Name='" + objInfo.TCO_Name + "',FillUnit='";
            string str2 = string.Concat(new object[] { obj2, objInfo.FillUnit, "',WeavePerson='", objInfo.WeavePerson, "',WeaveTime='", objInfo.WeaveTime, "',Maindescripe='" });
            return publicDbOpClass.ExecSqlString(str2 + objInfo.Manidescripe + "',Remark='" + objInfo.Remark + "' where Id='" + objInfo.Id.ToString() + "'");
        }
    }
}

