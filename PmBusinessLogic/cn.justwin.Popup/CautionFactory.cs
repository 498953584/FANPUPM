namespace cn.justwin.Popup
{
    using cn.justwin.BLL;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public class CautionFactory
    {
        private static List<Caution> CreateBulletions(string userCode)
        {
            List<Caution> list = new List<Caution>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM v_bulletin_list").AppendLine();
            builder.Append("WHERE DTM_EXPRIESDATE > DATEADD(DAY, -1, GETDATE())").AppendLine();
            builder.Append("\tAND AUDITSTATE = 1").AppendLine();
            builder.Append("\tAND NOT EXISTS ( ").AppendLine();
            builder.Append("\t\tSELECT * FROM PopupRecord ").AppendLine();
            builder.Append("\t\tWHERE PopupId = CAST(I_BULLETINID AS nvarchar(50)) AND UserCode = @userCode").AppendLine();
            builder.Append("\t)").AppendLine();
            builder.Append("ORDER BY DTM_RELEASETIME DESC").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode) };
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows)
            {
                Bulletin item = new Bulletin {
                    Id = row["I_BULLETINID"].ToString(),
                    Module = PopupParam.Bulletin,
                    Title = row["V_TITLE"].ToString(),
                    Content = StringUtility.StripTagsCharArray(row["V_CONTENT"].ToString()),
                    HandleUrl = PopupParam.BulletinHandleUrl
                };
                list.Add(item);
            }
            return list;
        }

        public static List<Caution> CreateCautions(string userCode)
        {
            List<Caution> first = new List<Caution>();
            PopupSetting setting = new PopupSetting();
            EnumerableRowCollection<string> source = setting.GetSetting(userCode).AsEnumerable().Select<DataRow, string>(m => m.Field<string>("Module"));
            if (source.Contains<string>(PopupParam.Bulletin))
            {
                first = first.Union<Caution>(CreateBulletions(userCode)).ToList<Caution>();
            }
            if (source.Contains<string>(PopupParam.CompanyNews))
            {
                first = first.Union<Caution>(CreateCompanyNews(userCode)).ToList<Caution>();
            }
            if (source.Contains<string>(PopupParam.Workflow))
            {
                first = first.Union<Caution>(CreateWorkflow(userCode)).ToList<Caution>();
            }
            if (source.Contains<string>(PopupParam.Schedule))
            {
                first = first.Union<Caution>(CreateSchedule(userCode)).ToList<Caution>();
            }
            if (source.Contains<string>(PopupParam.Mail))
            {
                first = first.Union<Caution>(CreateMail(userCode)).ToList<Caution>();
            }
            if (source.Contains<string>(PopupParam.Warning))
            {
                first = first.Union<Caution>(CreateWaring(userCode)).ToList<Caution>();
            }
            return first;
        }

        private static List<Caution> CreateCompanyNews(string userCode)
        {
            List<Caution> list = new List<Caution>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Institutions").AppendLine();
            builder.Append("WHERE status = 1 AND isvalid = 1").AppendLine();
            builder.Append("\tAND NOT EXISTS ( ").AppendLine();
            builder.Append("\t\tSELECT * FROM PopupRecord ").AppendLine();
            builder.Append("\t\tWHERE PopupId = CAST(InsCode AS nvarchar(50))  AND UserCode = @userCode").AppendLine();
            builder.Append("\t)").AppendLine();
            builder.Append("ORDER BY writedate DESC").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode) };
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows)
            {
                CompanyNews item = new CompanyNews {
                    Id = row["InsCode"].ToString(),
                    Module = PopupParam.CompanyNews,
                    Title = row["InsName"].ToString(),
                    Content = StringUtility.StripTagsCharArray(row["InsContent"].ToString()),
                    HandleUrl = PopupParam.CompanyNewsHandleUrl
                };
                list.Add(item);
            }
            return list;
        }

        private static List<Caution> CreateMail(string userCode)
        {
            List<Caution> list = new List<Caution>();
            string cmdText = "\r\n\t\t\t\tSELECT * FROM OA_Mail\r\n\t\t\t\tWHERE MailTo = @userCode\r\n\t\t\t\t\tAND MailType = 'I'\r\n\t\t\t\t\tAND IsReaded = '0'\r\n\t\t\t\t\tAND NOT EXISTS (\r\n\t\t\t\t\t\tSELECT * FROM PopupRecord \r\n\t\t\t\t\t\tWHERE Module = 'Mail' AND UserCode = @userCode\r\n\t\t\t\t\t\t\tAND PopupId = OA_Mail.MailId\r\n\t\t\t\t\t)\r\n\t\t\t\tORDER BY InputDate\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode) };
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters).Rows)
            {
                Mail item = new Mail {
                    Id = row["MailId"].ToString(),
                    Module = PopupParam.Mail,
                    Title = "新邮件",
                    Content = row["MailName"].ToString(),
                    HandleUrl = PopupParam.MailHandleUrl
                };
                list.Add(item);
            }
            return list;
        }

        private static List<Caution> CreateSchedule(string userCode)
        {
            List<Caution> list = new List<Caution>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT [I_DBSJ_ID], [I_XGID], [V_LXBM], [V_YHDM], ").AppendLine();
            builder.Append("\tconvert(varchar(10),DTM_DBSJ,20) as DTM_DBSJ, ").AppendLine();
            builder.Append("\t[C_OpenFlag], [V_DBLJ], [V_TPLJ], [V_Content] ").AppendLine();
            builder.Append("FROM [PT_DBSJ] ").AppendLine();
            builder.Append("where [V_YHDM] = @V_YHDM ").AppendLine();
            builder.Append("\tand datediff(ss,DTM_DBSJ,getdate())>0 ").AppendLine();
            builder.Append("\tand v_dblj != '' ").AppendLine();
            builder.Append("\tAND NOT EXISTS ( ").AppendLine();
            builder.Append("\t\tSELECT * FROM PopupRecord ").AppendLine();
            builder.Append("        WHERE PopupId = CAST(I_DBSJ_ID AS nvarchar(50))").AppendLine();
            builder.Append("\t\tAND UserCode = @V_YHDM").AppendLine();
            builder.Append("\t)").AppendLine();
            builder.Append("ORDER BY [DTM_DBSJ] desc").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@V_YHDM", userCode) };
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows)
            {
                Schedule item = new Schedule {
                    Id = row["I_DBSJ_ID"].ToString(),
                    Module = PopupParam.Schedule,
                    Title = "代办工作",
                    Content = row["V_Content"].ToString(),
                    HandleUrl = row["V_DBLJ"].ToString()
                };
                list.Add(item);
            }
            return list;
        }

        private static List<Caution> CreateWaring(string userCode)
        {
            List<Caution> list = new List<Caution>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT WarningId, WarningTitle AS Title, WarningContent AS Content, URI, \n");
            builder.Append("\tWarningContent AS Content, URI \n");
            builder.Append("FROM PT_Warning \n");
            builder.Append("WHERE IsValid = '1' \n");
            builder.Append("\tAND UserCode = @userCode \n");
            builder.Append("\tAND NOT EXISTS ( \n");
            builder.Append("\t\tSELECT * FROM PopupRecord \n");
            builder.Append("\t\tWHERE PopupId = CAST(WarningId AS nvarchar(50)) \n");
            builder.Append("\t\tAND UserCode = @userCode \n");
            builder.Append("\t)");
            builder.Append("ORDER BY WarningId \n");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode) };
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows)
            {
                Warning item = new Warning {
                    Id = row["WarningId"].ToString(),
                    Module = PopupParam.Warning,
                    Title = row["Title"].ToString(),
                    Content = row["Content"].ToString(),
                    HandleUrl = row["URI"].ToString()
                };
                list.Add(item);
            }
            return list;
        }

        private static List<Caution> CreateWorkflow(string userCode)
        {
            List<Caution> list = new List<Caution>();
            string cmdText = "\r\n                SELECT a.BusinessCode, DATEDIFF(hh, b.OutOfTime, GETDATE()) AS cs,\r\n                    a.BusinessClass,CONVERT (varchar(10), a.StartTime, 20) as rq,\r\n                    (SELECT BusinessClassName FROM WF_Business_Class AS d\r\n\t                 WHERE (BusinessCode = a.BusinessCode)\r\n\t                 AND (BusinessClass = a.BusinessClass)) AS BusinessClassName,\r\n                    b.NoteID, b.IsAllPass, a.TemplateID,\r\n                    (SELECT TemplateName FROM WF_Template AS c\r\n                     WHERE (TemplateID = a.TemplateID)) AS TemplateName,\r\n                    b.NodeID, b.NodeName,\r\n                    (SELECT v_xm FROM PT_yhmc WHERE (v_yhdm = a.Organiger)) AS organigerName,\r\n                    a.StartTime, a.InstanceCode,\r\n                    dbo.GetBusinessName(a.BusinessCode) AS BusinessName,\r\n\t                b.ArriveTime ,b.During\r\n                FROM WF_Instance_Main AS a\r\n                INNER JOIN WF_Instance AS b ON a.ID = b.ID\r\n                WHERE (((b.Sing = 0)\r\n\t\t                AND (b.Operator = @operator)\r\n\t                )\r\n\t                OR ((b.Sing = 0)\r\n\t\t                AND (dbo.GetCommissionMan(a.TemplateID, b.Operator) = @operator)\r\n\t                ))\r\n\t                AND NOT EXISTS (\r\n\t\t                SELECT * FROM PopupRecord \r\n\t\t                WHERE PopupId = CAST(a.InstanceCode AS nvarchar(50)) AND UserCode = @operator\r\n\t                )\r\n                order by a.StartTime desc\r\n            ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@operator", userCode) };
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters).Rows)
            {
                Workflow item = new Workflow {
                    Id = row["InstanceCode"].ToString(),
                    Module = PopupParam.Workflow,
                    Title = "流程审核",
                    Content = row["BusinessClassName"].ToString(),
                    HandleUrl = PopupParam.WorkflowHandleUrl,
                    NoteId = row["NoteID"].ToString(),
                    NodeID = row["NodeID"].ToString(),
                    IsAllPass = row["IsAllPass"].ToString(),
                    BusinessClass = row["BusinessClass"].ToString(),
                    BusinessCode = row["BusinessCode"].ToString()
                };
                list.Add(item);
            }
            return list;
        }

        public static void SetPopupRecord(List<Caution> cautions, string userCode)
        {
            foreach (Caution caution in cautions)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("INSERT INTO PopupRecord(UserCode, Module, PopupId)").AppendLine();
                builder.Append("\tVALUES(@userCode, @module, @popupId)").AppendLine();
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@module", caution.Module), new SqlParameter("@popupId", caution.Id) };
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
        }
    }
}

