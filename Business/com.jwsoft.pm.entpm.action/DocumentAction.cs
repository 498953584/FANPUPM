namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Data;

    public class DocumentAction
    {
        public static bool AddDocClass(Hashtable htClass)
        {
            return publicDbOpClass.Insert("[PT_SingleClasses]", htClass);
        }

        public static bool AddDocTemplate(Hashtable templateInfo)
        {
            return publicDbOpClass.Insert("[OA_Document_Templet]", templateInfo);
        }

        public static bool AddReceiveFileInfo(Hashtable receiveFileInfo)
        {
            return publicDbOpClass.Insert("[OA_Document_ReceiveFile]", receiveFileInfo);
        }

        public static bool AddSendFileInfo(Hashtable sendFileInfo)
        {
            return publicDbOpClass.Insert("[OA_Document_SendFile]", sendFileInfo);
        }

        public static bool DelDocClass(int classId)
        {
            return publicDbOpClass.NonQuerySqlString(" delete PT_SingleClasses where ClassID = " + classId.ToString());
        }

        public static bool DelDocTemplate(int templateId)
        {
            return publicDbOpClass.NonQuerySqlString(" delete OA_Document_Templet where TempletID = " + templateId.ToString());
        }

        public static bool DelReceiveFileInfo(Guid fileId)
        {
            return publicDbOpClass.NonQuerySqlString(" delete OA_Document_ReceiveFile where FileID = '" + fileId.ToString() + "'");
        }

        public static bool DelSendFileInfo(Guid fileId)
        {
            return publicDbOpClass.NonQuerySqlString("delete OA_Document_SendFile where FileID = '" + fileId.ToString() + "'");
        }

        public static DataTable FilterDocumentClass(string filterField)
        {
            return publicDbOpClass.DataTableQuary("Select a.* from PT_SingleClasses a inner join PT_D_SingleClass b  on  a.ClassTypeCode = b.ClassTypeCode where b.filterField = '" + filterField.ToString() + "'");
        }

        public static object QueryClassTypeCode(string filterField)
        {
            return publicDbOpClass.ExecuteScalar(" select ClassTypeCode from PT_D_SingleClass where FilterField = '" + filterField.ToString() + "'");
        }

        public static DataTable QueryCorpCode(string userCode)
        {
            string str = "";
            str = " select a.v_bmbm,a.v_bmmc,a.corpCode,(select corpName from PT_d_CorpCode where corpCode = a.corpCode) as corpName from pt_d_bm a where  a.c_sfyx ='y' ";
            return publicDbOpClass.DataTableQuary(str + " and a.i_bmdm = (select i_bmdm from pt_yhmc where v_yhdm = '" + userCode.ToString() + "')");
        }

        public static DataTable QueryDocTemplateList(int classId)
        {
            return publicDbOpClass.DataTableQuary(" select *,(select v_xm from pt_yhmc where v_yhdm = userCode) as UserName from OA_Document_Templet where ClassID = " + classId.ToString());
        }

        public static DataTable QueryDocumentClass(string classTypeCode)
        {
            return publicDbOpClass.DataTableQuary("Select * from PT_SingleClasses  where ClassTypeCode = '" + classTypeCode.ToString() + "' order by ClassID asc");
        }

        public static DataTable QueryOneDocTemplate(int templateId)
        {
            return publicDbOpClass.DataTableQuary(" select *,(select v_xm from pt_yhmc where v_yhdm = userCode) as UserName from OA_Document_Templet where TempletID = " + templateId.ToString());
        }

        public static DataTable QueryOneDocumentClass(int classId)
        {
            return publicDbOpClass.DataTableQuary(" select * from PT_SingleClasses where classId = " + classId.ToString());
        }

        public static DataTable QueryOneDocumentClasscode(string strClassCode, string strClassTypeCode)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_SingleClasses where ClassCode ='" + strClassCode + "' collate chinese_PRC_CS_AI and ClassTypeCode='" + strClassTypeCode + "'");
        }

        public static DataTable QueryOneDocumentClassname(string strClassName, string strClassTypeCode)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_SingleClasses where ClassName ='" + strClassName + "' collate chinese_PRC_CS_AI and ClassTypeCode='" + strClassTypeCode + "'");
        }

        public static DataTable QueryOneReceiveFile(Guid fileId)
        {
            return publicDbOpClass.DataTableQuary("select * from OA_Document_ReceiveFile where FileID = '" + fileId.ToString() + "'");
        }

        public static DataTable QueryOneSendFile(Guid fileId)
        {
            return publicDbOpClass.DataTableQuary("select *,(select v_xm from pt_yhmc where v_yhdm = usercode) as userName from OA_Document_SendFile where FileID = '" + fileId.ToString() + "'");
        }

        public static DataTable QueryReceiveFileList(string corpCode)
        {
            return publicDbOpClass.DataTableQuary(" select *, (case when ReceiveType='1' then '内部收文' else '外部收文' end) as ReceiveTypeName from OA_Document_ReceiveFile where CorpCode = '" + corpCode.ToString() + "'");
        }

        public static DataTable QuerySendFileList(string corpCode, int auditState, string UserCode)
        {
            string sqlString = "";
            if (auditState == 1)
            {
                sqlString = "select *,(select v_xm from pt_yhmc where v_yhdm = usercode) as userName,'已审核' State from OA_Document_SendFile where CorpCode = '" + corpCode.ToString() + "' and auditState = '1' and UserCode = '" + UserCode + "' order by IssueDate desc";
            }
            else
            {
                sqlString = "select *,(select v_xm from pt_yhmc where v_yhdm = usercode) as userName, ";
                string str2 = sqlString;
                sqlString = str2 + " (case when auditstate = -1 then '未提交' when auditstate = '0' then '审核中' when auditstate = '1' then '已审核' when auditstate = '-2' then '被驳回' end) as State from OA_Document_SendFile where CorpCode = '" + corpCode.ToString() + "' and UserCode = '" + UserCode + "' order by IssueDate desc";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QuerySignRecord(Guid fileID, string types)
        {
            string sqlString = "";
            if (types == "send")
            {
                sqlString = ("select *,(select v_xm from pt_yhmc where v_yhdm = signusercode) as signUserName,(select corpName from pt_d_CorpCode where corpCode = (select corpCode from OA_Document_SendFile where fileID = '" + fileID.ToString() + "')) as corpName ") + " from OA_Document_SignIn a,OA_Document_SendFile b where a.fileID = b.fileID and a.signDate is not null and a.DocumentType = '1' and a.fileID='" + fileID.ToString() + "' ";
            }
            else
            {
                sqlString = " select *,(select v_xm from pt_yhmc where v_yhdm = signusercode) as signUserName,sendCorpName as corpName from  OA_Document_SignIn a,OA_Document_ReceiveFile b ";
                sqlString = sqlString + " where a.fileID = b.fileID and a.signDate is not null and a.DocumentType = '2' and a.fileID='" + fileID.ToString() + "' ";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QuerySignRecordInfo(string userCode)
        {
            string str = " select a.RecordID,a.FileID,b.Title,a.ReceiveDate,a.DocumentType,a.signDate,b.Remark,(select top 1 v_xm from pt_yhmc where v_yhdm = signusercode) as signUserName,(select top 1 corpName from pt_d_CorpCode ";
            return publicDbOpClass.DataTableQuary((((str + " where corpCode = (select top 1 c.corpCode from OA_Document_SendFile c inner join OA_Document_SignIn d on c.fileId = d.fileId where d.signusercode = '" + userCode.ToString() + "')) as corpName ") + " from OA_Document_SignIn a,OA_Document_SendFile b where a.fileID = b.fileID and a.signDate is null and a.DocumentType = '1' and a.SignUserCode = '" + userCode.ToString() + "' union ") + " select a.RecordID,a.FileID,b.Title,a.ReceiveDate,a.DocumentType,a.signDate,b.Remark,(select top 1 v_xm from pt_yhmc where v_yhdm = signusercode) as signUserName,sendCorpName as corpName from  OA_Document_SignIn a,OA_Document_ReceiveFile b ") + " where a.fileID = b.fileID and a.signDate is null and a.DocumentType = '2' and a.SignUserCode = '" + userCode.ToString() + "' ");
        }

        public static bool UpdDocClass(Hashtable htClass, string where)
        {
            return publicDbOpClass.Update("[PT_SingleClasses]", htClass, where);
        }

        public static bool UpdDocTemplate(Hashtable templateInfo, string where)
        {
            return publicDbOpClass.Update("[OA_Document_Templet]", templateInfo, where);
        }

        public static bool UpdReceiveFileInfo(Hashtable receiveFileInfo, string where)
        {
            return publicDbOpClass.Update("[OA_Document_ReceiveFile]", receiveFileInfo, where);
        }

        public static bool UpdSendFileInfo(Hashtable sendFileInfo, string where)
        {
            return publicDbOpClass.Update("[OA_Document_SendFile]", sendFileInfo, where);
        }
    }
}

