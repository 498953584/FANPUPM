namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class CheckAction
    {
        public static bool CheckInfoOp(CheckInfo objinfo, string OpType)
        {
            string sqlString = "";
            if (OpType == "Insert")
            {
                sqlString = string.Concat(new object[] { 
                    "Insert into Prj_ItemInspect(PrjCode,ExamineUnit,AcceptCheckUnit,AcceptCheckContent,AcceptCheckGist,AcceptCheckSort,AcceptCheckAnswerForPerson,AcceptCheckDate,AcceptCheckResult,CompleteApplyContent,Remark,Flags,requestfinishtime,planfinishtime,factfinishtime,factresult,prjplan,CheckResult,CertifiResult,checkResults,filesType,mark,rectifyMarkID) values('", objinfo.PrjCode, "','", objinfo.ExamineUnit, "','", objinfo.AcceptCheckUnit, "','", objinfo.AcceptCheckContent, "','", objinfo.AcceptCheckGist, "',", objinfo.AcceptCheckSort, ",'", objinfo.AcceptCheckAnswerForPerson, "','", objinfo.AcceptCheckDate, 
                    "','", objinfo.AcceptCheckResult, "','", objinfo.CompleteApplyContent, "','", objinfo.Remark, "',", objinfo.Flags, ",'", objinfo.requestfinishtime, "','", objinfo.planfinishtime, "','", objinfo.factfinishtime, "','", objinfo.factresult, 
                    "','", objinfo.prjplan, "',", objinfo.CheckResult.ToString(), ",", objinfo.CertifiResult.ToString(), ",", objinfo.checkResults.ToString(), ",", objinfo.FilesType.ToString(), ",", objinfo.Mark.ToString(), ",'", objinfo.RectifyMarkID, "')"
                 });
            }
            else if (OpType == "Update")
            {
                sqlString = string.Concat(new object[] { 
                    "update Prj_ItemInspect set ExamineUnit='", objinfo.ExamineUnit, "',AcceptCheckUnit='", objinfo.AcceptCheckUnit, "',AcceptCheckContent='", objinfo.AcceptCheckContent, "',AcceptCheckGist='", objinfo.AcceptCheckGist, "',AcceptCheckSort=", objinfo.AcceptCheckSort, ",AcceptCheckAnswerForPerson='", objinfo.AcceptCheckAnswerForPerson, "',AcceptCheckDate='", objinfo.AcceptCheckDate, "',CompleteApplyContent='", objinfo.CompleteApplyContent, 
                    "',AcceptCheckResult='", objinfo.AcceptCheckResult, "',Remark='", objinfo.Remark, "',requestfinishtime='", objinfo.requestfinishtime, "',planfinishtime='", objinfo.planfinishtime, "',factfinishtime='", objinfo.factfinishtime, "',factresult='", objinfo.factresult, "',prjplan='", objinfo.prjplan, "',CheckResult=", objinfo.CheckResult.ToString(), 
                    ",CertifiResult = ", objinfo.CertifiResult.ToString(), ",IsRectified=", objinfo.IsRectified ? "1" : "0", ",checkResults =", objinfo.checkResults.ToString(), ",filesType =", objinfo.FilesType.ToString(), ",mark =", objinfo.Mark.ToString(), ",rectifyMarkID ='", objinfo.RectifyMarkID.ToString(), "'  where ID=", objinfo.ID
                 });
            }
            else if (OpType == "Sp")
            {
                sqlString = string.Concat(new object[] { "update Prj_ItemInspect set ExamineApprovePerson='", objinfo.ExamineApprovePerson, "',ExamineApproveResult=", objinfo.ExamineApproveResult, ",ExamineApproveDate='", objinfo.ExamineApproveDate, "',ExamineApproveIdea='", objinfo.ExamineApproveIdea, "' where ID=", objinfo.ID });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool Delete(string pk)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Prj_ItemInspect where ID='" + pk + "'");
        }

        public static DataTable GetCheckCollections(string strwhere)
        {
            return publicDbOpClass.GetPageData(strwhere, "Prj_v_ItemInspect", "id desc");
        }

        public static int GetCheckCount(string strwhere)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_ItemInspect where " + strwhere);
        }

        public static CheckInfo GetCheckInfo(string ID)
        {
            CheckInfo info = new CheckInfo();
            DataTable table = publicDbOpClass.DataTableQuary("select * from Prj_ItemInspect where ID=" + ID);
            info.AcceptCheckAnswerForPerson = table.Rows[0]["AcceptCheckAnswerForPerson"].ToString();
            info.AcceptCheckContent = table.Rows[0]["AcceptCheckContent"].ToString();
            info.AcceptCheckDate = DateTime.Parse(table.Rows[0]["AcceptCheckDate"].ToString());
            info.AcceptCheckGist = table.Rows[0]["AcceptCheckGist"].ToString();
            info.AcceptCheckResult = table.Rows[0]["AcceptCheckResult"].ToString();
            info.AcceptCheckSort = int.Parse(table.Rows[0]["AcceptCheckSort"].ToString());
            info.AcceptCheckUnit = table.Rows[0]["AcceptCheckUnit"].ToString();
            info.CompleteApplyContent = table.Rows[0]["CompleteApplyContent"].ToString();
            if (table.Rows[0]["ExamineApproveDate"].ToString() != "")
            {
                info.ExamineApproveDate = DateTime.Parse(table.Rows[0]["ExamineApproveDate"].ToString());
            }
            info.ExamineApproveIdea = table.Rows[0]["ExamineApproveIdea"].ToString();
            info.ExamineApprovePerson = table.Rows[0]["ExamineApprovePerson"].ToString();
            info.ExamineApproveResult = int.Parse(table.Rows[0]["ExamineApproveResult"].ToString());
            info.ExamineUnit = table.Rows[0]["ExamineUnit"].ToString();
            info.ID = int.Parse(table.Rows[0]["ID"].ToString());
            info.PrjCode = table.Rows[0]["PrjCode"].ToString();
            info.Remark = table.Rows[0]["Remark"].ToString();
            if (table.Rows[0]["requestfinishtime"].ToString() != "")
            {
                info.requestfinishtime = DateTime.Parse(table.Rows[0]["requestfinishtime"].ToString());
            }
            if (table.Rows[0]["planfinishtime"].ToString() != "")
            {
                info.planfinishtime = DateTime.Parse(table.Rows[0]["planfinishtime"].ToString());
            }
            if (table.Rows[0]["factfinishtime"].ToString() != "")
            {
                info.factfinishtime = DateTime.Parse(table.Rows[0]["factfinishtime"].ToString());
            }
            info.factresult = table.Rows[0]["factresult"].ToString();
            info.prjplan = table.Rows[0]["prjplan"].ToString();
            if (table.Rows[0]["CertifiResult"] != DBNull.Value)
            {
                info.CertifiResult = int.Parse(table.Rows[0]["CertifiResult"].ToString());
            }
            if (table.Rows[0]["checkResults"] != DBNull.Value)
            {
                info.checkResults = int.Parse(table.Rows[0]["checkResults"].ToString());
            }
            if (table.Rows[0]["CheckResult"] != DBNull.Value)
            {
                info.CheckResult = int.Parse(Convert.ToBoolean(table.Rows[0]["CheckResult"].ToString()) ? "1" : "0");
            }
            if (table.Rows[0]["Mark"] != DBNull.Value)
            {
                info.Mark = int.Parse(table.Rows[0]["Mark"].ToString());
            }
            if (table.Rows[0]["RectifyMarkID"] != DBNull.Value)
            {
                info.RectifyMarkID = table.Rows[0]["RectifyMarkID"].ToString();
            }
            if (table.Rows[0]["FilesType"] != DBNull.Value)
            {
                info.FilesType = int.Parse(table.Rows[0]["FilesType"].ToString());
            }
            return info;
        }

        public static void UpdateThisSate(int ids)
        {
            publicDbOpClass.NonQuerySqlString(((string.Empty + "UPDATE  Prj_ItemInspect SET CheckResult= 0" + " , IsRectified=0") + " , CertifiResult=0" + " , CheckResults=0 ") + "  where ID=" + ids);
        }
    }
}

