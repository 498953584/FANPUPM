namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ExperProjectAction
    {
        public static int EntCancel(string id)
        {
            return publicDbOpClass.ExecSqlString("update  Prj_ExpertSchemeManage set EntExamineApproveResult='1' where MainId='" + id + "'");
        }

        public static int ExperAdd(ExpertProjectInfo objInfo)
        {
            string str = "";
            str = " insert into Prj_ExpertSchemeManage (MainId,PrjCode,PrejectName,SchemeName,WeavePeople,WeaveDate,";
            object obj2 = str;
            object obj3 = string.Concat(new object[] { obj2, "FillPeople,FillDate,SchemEbewrite,Remark,ExamineApprove,FlowGuid) values('", objInfo.ManiId, "','", objInfo.PrjCode, "','" });
            object obj4 = string.Concat(new object[] { obj3, objInfo.PrejectName, "','", objInfo.SchemeName, "','", objInfo.WeavePeople, "','", objInfo.WeaveDate, "','", objInfo.FillPeople, "','" });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, objInfo.FillDate, "','", objInfo.SchemEbewrite, "','", objInfo.Remark, "','", objInfo.ExamineApprove, "','", objInfo.FlowGuid, "')" }));
        }

        public static int ExperDel(string Id)
        {
            string str = "";
            if (((int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_ExpertSchemeManage where (PPMExamineApproveResult!='' or EntExamineApproveResult!='') and (MainId='" + Id + "')")) > 0)
            {
                return 2;
            }
            str = "begin";
            return publicDbOpClass.ExecSqlString(((str + " delete from XPM_Basic_AnnexList where ModuleID='1722' and RecordCode='" + Id + "'") + " delete from Prj_ExpertSchemeManage where MainId='" + Id + "'") + " end");
        }

        public static int ExperUpd(ExpertProjectInfo objInfo, string Id)
        {
            object obj2 = "update Prj_ExpertSchemeManage set PrjCode='" + objInfo.PrjCode + "',PrejectName='" + objInfo.PrejectName + "',SchemeName='" + objInfo.SchemeName;
            object obj3 = string.Concat(new object[] { obj2, "',WeavePeople='", objInfo.WeavePeople, "',WeaveDate='", objInfo.WeaveDate, "',FillPeople='", objInfo.FillPeople, "',FillDate='" });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, objInfo.FillDate, "',SchemEbewrite='", objInfo.SchemEbewrite, "',Remark='", objInfo.Remark, "',ExamineApprove='", objInfo.ExamineApprove, "'" }) + " where MainId='" + Id + "'");
        }

        public static DataTable GetEntContructList(string strPrjCode)
        {
            string sqlWhere = "";
            sqlWhere = "(1=1) and PPMExamineApproveResult=0";
            if (strPrjCode.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (PrejectName ='" + strPrjCode + "')";
            }
            return publicDbOpClass.GetPageData(sqlWhere, "Prj_V_ExpertProject", "MainId desc");
        }

        public static int GetEntExpertCount(string strPrjCode)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_V_ExpertProject where PrejectName ='" + strPrjCode + "' and PPMExamineApproveResult=0");
        }

        public static int GetMaxId()
        {
            string sqlString = "";
            sqlString = "select ISNULL(max(MainId),0)+1 from Prj_ExpertSchemeManage";
            return (int) publicDbOpClass.ExecuteScalar(sqlString);
        }

        public static int GetPPMExpertCount(string strPrjCode)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_V_ExpertProject where PrejectName ='" + strPrjCode + "'");
        }

        public static DataTable GetPPMExpertList(string strPrjCode)
        {
            string sqlWhere = "";
            sqlWhere = "(1=1)";
            if (strPrjCode.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (PrejectName ='" + strPrjCode + "')";
            }
            return publicDbOpClass.GetPageData(sqlWhere, "Prj_V_ExpertProject", "MainId desc");
        }

        public static DataTable GetSingleExpertInfo(string strId)
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_V_ExpertProject where MainId ='" + strId + "'");
        }

        public static int PPMCancel(string id)
        {
            return publicDbOpClass.ExecSqlString("update  Prj_ExpertSchemeManage set PPMExamineApproveResult='1' where MainId='" + id + "'");
        }

        public static int SetEntAudit(ExpertProjectInfo objInfo)
        {
            object obj2 = "update Prj_ExpertSchemeManage set EntExamineApprovePeople='" + objInfo.EntExamineApprovePeople + "',EntExamineApproveDate='" + objInfo.EntExamineApproveDate.ToShortDateString() + "',EntExamineApproveResult='" + objInfo.EntExamineApproveResult.ToString();
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "',EntExamineApproveIdea='", objInfo.EntExamineApproveIdea, "',EntRemark='", objInfo.EntRemark, "' where MainId='", objInfo.ManiId, "'" }));
        }

        public static int SetPPMAudit(ExpertProjectInfo objInfo)
        {
            object obj2 = "update Prj_ExpertSchemeManage set PPMExamineApprovePeople='" + objInfo.PPMExamineApprovePeople + "',PPMExamineApproveDate='" + objInfo.PPMExamineApproveDate.ToShortDateString() + "',PPMExamineApproveResult='" + objInfo.PPMExamineApproveResult.ToString();
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "',PPMExamineApproveIdea='", objInfo.PPMExamineApproveIdea, "',PPMRemark='", objInfo.PPMRemark, "' where MainId='", objInfo.ManiId, "'" }));
        }
    }
}

