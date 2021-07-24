namespace cn.justwin.stockBLL
{
    using cn.justwin.BLL;
    using cn.justwin.BLL.ProgressManagement;
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.PrjManager;
    using cn.justwin.Tender;
    using com.jwsoft.pm.entpm.action;
    using com.jwsoft.pm.entpm.model;
    using PmBusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class WorkFlow
    {
        private static void AuditDefined(int instanceId, string instanceCode, string businessCode, string businessClass, string lastFlowState, string tableName, string columnName)
        {
            string str2 = SelfEventAction.GetTypeName(AppDomain.CurrentDomain.BaseDirectory + "SelfEventInfo.xml", tableName, columnName);
            if (!string.IsNullOrWhiteSpace(str2))
            {
                ISelfEvent event2 = (ISelfEvent) Assembly.Load("PmBusinessLogic").CreateInstance(str2);
                if (event2 != null)
                {
                    string maxSing = FlowAuditAction.GetMaxSing(instanceId);
                    if ((lastFlowState == "1") && (maxSing == "1"))
                    {
                        event2.CommitEvent(instanceCode.ToString());
                    }
                    else if (lastFlowState == "-2")
                    {
                        event2.RefuseEvent(instanceCode.ToString());
                    }
                    else if (lastFlowState == "-3")
                    {
                        event2.RestatedEvent(instanceCode.ToString());
                    }
                }
            }
            if (lastFlowState == "1")
            {
                if (businessCode == "089")
                {
                    TenderInfo.UpdatePrjState(Guid.Parse(instanceCode), 2);
                }
                else if (businessCode == "100")
                {
                    PrjMember.AddLimit(Guid.Parse(instanceCode));
                }
                else if (businessCode == "107")
                {
                    Progress.UpdateLatest(instanceCode.ToString());
                }
                else if (businessCode == "108")
                {
                    cn.justwin.BLL.ProgressManagement.Version.UpdateLatest(instanceCode.ToString());
                }
                MsgOrganiger(instanceId, businessCode, businessClass);
            }
        }

        public static void DealOutTimeAudit()
        {
            string cmdText = "\r\n\t\t\t\tselect I.*,T.DueMode from WF_Instance I\r\n\t\t\t\tLEFT JOIN WF_TemplateNode T ON T.NodeID=I.NodeID  \r\n\t\t\t\twhere DATEDIFF(mi, OutOfTime, GETDATE())>0 and sing=0 and T.DueMode <> '2'\r\n\t\t\t";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    switch (table.Rows[i]["DueMode"].ToString())
                    {
                        case "0":
                            ProcessFlow(Convert.ToInt32(table.Rows[i]["NoteId"].ToString()), table.Rows[i]["IsAllPass"].ToString(), table.Rows[i]["Operator"].ToString(), -2, "超时驳回");
                            break;

                        case "1":
                            ProcessFlow(Convert.ToInt32(table.Rows[i]["NoteId"].ToString()), table.Rows[i]["IsAllPass"].ToString(), table.Rows[i]["Operator"].ToString(), 1, "超时通过");
                            break;

                        case "3":
                            ProcessFlow(Convert.ToInt32(table.Rows[i]["NoteId"].ToString()), table.Rows[i]["IsAllPass"].ToString(), table.Rows[i]["Operator"].ToString(), -3, "超时重报");
                            break;
                    }
                    int instanceId = Convert.ToInt32(table.Rows[i]["NoteId"]);
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("SELECT Id,Main.BusinessCode,BusinessClass,InstanceCode,LinkTable,PrimaryField,StateField FROM WF_Instance_Main Main\r\n                                            INNER JOIN WF_BusinessCode BusinessCode ON Main.BusinessCode=BusinessCode.BusinessCode WHERE ID='{0}'", table.Rows[i]["ID"].ToString());
                    DataTable table2 = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
                    if (table2.Rows.Count == 1)
                    {
                        string businessCode = table2.Rows[0]["BusinessCode"].ToString();
                        string businessClass = table2.Rows[0]["BusinessClass"].ToString();
                        string instanceCode = table2.Rows[0]["InstanceCode"].ToString();
                        string tableName = table2.Rows[0]["LinkTable"].ToString();
                        string str7 = table2.Rows[0]["PrimaryField"].ToString();
                        string columnName = table2.Rows[0]["StateField"].ToString();
                        StringBuilder builder2 = new StringBuilder();
                        builder2.AppendFormat("SELECT {0} FROM {1} where {2}='{3}'", new object[] { columnName, tableName, str7, instanceCode });
                        DataTable table3 = SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString());
                        if (table3.Rows.Count == 1)
                        {
                            string lastFlowState = table3.Rows[0][0].ToString();
                            AuditDefined(instanceId, instanceCode, businessCode, businessClass, lastFlowState, tableName, columnName);
                        }
                    }
                }
            }
        }

        public static void DeleteInvalid()
        {
            try
            {
                WFInstanceMainService source = new WFInstanceMainService();
                WFBusinessCodeService service2 = new WFBusinessCodeService();
                List<WFInstanceMain> list = source.ToList<WFInstanceMain>();
                for (int i = list.Count - 1; i > 0; i--)
                {
                    WFInstanceMain item = list[i];
                    WFBusinessCode byId = service2.GetById(item.BusinessCode);
                    string sql = string.Concat(new object[] { "SELECT * FROM ", byId.LinkTable, " WHERE ", byId.KeyWord, "='", item.InstanceCode, "'" });
                    if (source.ExcuteSql(sql).Count == 0)
                    {
                        source.Delete(item);
                    }
                }
            }
            catch
            {
            }
        }

        private static void getOrganiger(string xgid, string Mes, string jsyhdm, string businessCode, string businessClass)
        {
            com.jwsoft.pm.entpm.model.PTDBSJ model = new com.jwsoft.pm.entpm.model.PTDBSJ {
                V_LXBM = "017",
                I_XGID = xgid,
                DTM_DBSJ = DateTime.Now,
                V_Content = Mes,
                V_DBLJ = "?rid=" + xgid + "&bc=" + businessCode + "&bcl=" + businessClass,
                V_YHDM = jsyhdm
            };
            SendSysMsg(model);
        }

        private static DataTable getTxlxList(string LXBM)
        {
            string cmdText = "select * from PT_D_TXLX where V_LXBM = '" + LXBM + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText);
        }

        public static string GetWFName(string businesscode, string instancecode)
        {
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, " select * from WF_BusinessCode where BusinessCode='" + businesscode + "' ");
            string str = string.Empty;
            try
            {
                DataTable table2 = SqlHelper.ExecuteQuery(CommandType.Text, " select  " + table.Rows[0]["NameField"].ToString() + " from  " + table.Rows[0]["LinkTable"].ToString() + "  where " + table.Rows[0]["PrimaryField"].ToString() + " ='" + instancecode + "' ");
                if (businesscode == "089")
                {
                    if (!string.IsNullOrEmpty(table2.Rows[0][0].ToString()))
                    {
                        str = str + "—" + TenderInfo.GetProjectName(table2.Rows[0][0].ToString());
                    }
                    return str;
                }
                if ((businesscode == "100") || (businesscode == "106"))
                {
                    if (!string.IsNullOrEmpty(table2.Rows[0][0].ToString()))
                    {
                        str = str + "—" + cn.justwin.PrjManager.ProjectInfo.GetProjectName(table2.Rows[0][0].ToString());
                    }
                    return str;
                }
                if (!string.IsNullOrEmpty(table2.Rows[0][0].ToString()))
                {
                    str = str + "—" + table2.Rows[0][0].ToString();
                }
            }
            catch
            {
            }
            return str;
        }

        public static string GetWFState(string businesscode, string instancecode)
        {
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, " select * from WF_BusinessCode where BusinessCode='" + businesscode + "' ");
            string str = string.Empty;
            try
            {
                str = SqlHelper.ExecuteQuery(CommandType.Text, " select  " + table.Rows[0]["StateField"].ToString() + " from  " + table.Rows[0]["LinkTable"].ToString() + "  where " + table.Rows[0]["PrimaryField"].ToString() + " ='" + instancecode + "' ").Rows[0][0].ToString();
            }
            catch
            {
            }
            return str;
        }

        public static DataTable GetWorkFlowList(string wfname, string tempname, string organizername, string starttime, string flowstate, string businesscode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n\t\t\t\tSELECT ROW_NUMBER() OVER (ORDER BY StartTime DESC) AS Num,  ID, BusinessCode, BusinessClass, \r\n\t\t\t\t\tTemplateID, InstanceCode, Organiger, StartTime, TemplateName, OrganigerName, auditor,\r\n\t\t\t\t\tDoWithUrl, BusinessClassName\r\n\t\t\t\tFROM (\r\n\t\t\t\t\tSELECT  RANK() OVER (PARTITION BY InstanceCode ORDER BY StartTime DESC) AS _Rank, \r\n\t\t\t\t\t\tIM.ID, IM.BusinessCode, IM.BusinessClass, IM.TemplateID, IM.InstanceCode, \r\n\t\t\t\t\tIM.Organiger, IM.StartTime ,T.TemplateName, Y.v_xm AS OrganigerName,(\r\n\t\t\t\t\t\tSELECT STUFF((\r\n\t\t\t\t\t\t\tSELECT ','+[v_xm] FROM WF_Instance \r\n\t\t\t\t\t\t\tLEFT JOIN PT_yhmc ON PT_yhmc.v_yhdm=WF_Instance.Operator\r\n\t\t\t\t\t\t\tWHERE ID=I.ID FOR XML PATH ('')),1,1,'')\r\n\t\t\t\t\t\tFROM WF_Instance AS I\r\n\t\t\t\t\t\tWHERE ID=IM.ID\r\n\t\t\t\t\t\tGROUP BY ID ) AS auditor,C.DoWithUrl,BC.BusinessClassName\r\n\t\t\t\t\tFROM WF_Instance_Main IM \r\n\t\t\t\t\tLEFT JOIN WF_Template T ON IM.TemplateID=T.TemplateID\r\n\t\t\t\t\tLEFT JOIN PT_yhmc Y ON Y.v_yhdm=IM.Organiger\r\n\t\t\t\t\tLEFT JOIN WF_BusinessCode C ON C.BusinessCode=IM.BusinessCode\r\n\t\t\t\t\tLEFT JOIN WF_Business_Class BC ON BC.BusinessCode=IM.BusinessCode \r\n\t\t\t\t\t\tAND BC.BusinessClass=IM.BusinessClass\r\n\t\t\t\t) AS Total WHERE _Rank = 1\r\n\t\t\t");
            if (!string.IsNullOrEmpty(tempname))
            {
                builder.AppendFormat(" AND TemplateName LIKE '%{0}%' ", tempname);
            }
            if (!string.IsNullOrEmpty(organizername))
            {
                builder.AppendFormat(" AND OrganigerName LIKE '%{0}%' ", organizername);
            }
            if (!string.IsNullOrEmpty(starttime))
            {
                string str = Convert.ToDateTime(starttime).AddDays(1.0).ToString("yyyy-MM-dd");
                builder.AppendFormat(" AND StartTime >='{0}' AND StartTime < '{1}' ", starttime, str);
            }
            if (!string.IsNullOrEmpty(businesscode))
            {
                builder.AppendFormat(" AND BusinessCode ='{0}' ", businesscode);
            }
            DataTable table = new DataTable();
            table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
            table.Columns.Add("wfname");
            table.Columns.Add("state");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i]["wfname"] = table.Rows[i]["BusinessClassName"].ToString() + GetWFName(table.Rows[i]["BusinessCode"].ToString(), table.Rows[i]["InstanceCode"].ToString());
                table.Rows[i]["state"] = GetWFState(table.Rows[i]["BusinessCode"].ToString(), table.Rows[i]["InstanceCode"].ToString());
            }
            DataTable table2 = new DataTable();
            table2 = table.Clone();
            if (!string.IsNullOrEmpty(wfname))
            {
                foreach (DataRow row in table.Select(" wfname  LIKE '%" + wfname + "%' "))
                {
                    table2.Rows.Add(row.ItemArray);
                }
            }
            if (!string.IsNullOrEmpty(flowstate))
            {
                foreach (DataRow row2 in table.Select(" state ='" + flowstate + "'"))
                {
                    table2.Rows.Add(row2.ItemArray);
                }
            }
            if (string.IsNullOrEmpty(wfname) && string.IsNullOrEmpty(flowstate))
            {
                return table;
            }
            return table2;
        }

        private static void MsgOrganiger(int instanceID, string businessCode, string businessClass)
        {
            string cmdText = " select Organiger,InstanceCode,(SELECT BusinessClassName FROM WF_Business_Class where BusinessCode = a.BusinessCode and BusinessClass = a.BusinessClass) as BusinessClassName from wf_instance_main a where ID = ";
            object obj2 = cmdText;
            cmdText = string.Concat(new object[] { obj2, " ( select ID from wf_instance \twhere NoteID  = ", instanceID, " )" });
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText);
            if (table.Rows.Count > 0)
            {
                getOrganiger(instanceID.ToString(), "您发起" + table.Rows[0]["BusinessClassName"].ToString() + "的流程已审核完结！", table.Rows[0]["Organiger"].ToString(), businessCode, businessClass);
            }
        }

        public static void ProcessFlow(int instanceId, string isAllPass, string userCode, int auditResult, string auditInfo)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", instanceId), new SqlParameter("@isallpass", isAllPass), new SqlParameter("@userCode", userCode), new SqlParameter("@auditResult", auditResult), new SqlParameter("@auditInfo", auditInfo) };
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "p_wf_processflow", commandParameters);
        }

        private static int PTDBSJAdd(com.jwsoft.pm.entpm.model.PTDBSJ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_DBSJ(");
            builder.Append("I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.I_XGID + "',");
            builder.Append("'" + model.V_LXBM + "',");
            builder.Append("'" + model.V_YHDM + "',");
            builder.Append("'" + model.DTM_DBSJ + "',");
            builder.Append("'" + model.V_Content + "',");
            builder.Append("'" + model.V_TPLJ + "',");
            builder.Append("'" + model.V_DBLJ + "',");
            builder.Append("'" + model.C_OpenFlag + "'");
            builder.Append(")");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString());
        }

        private static int SendSysMsg(com.jwsoft.pm.entpm.model.PTDBSJ model)
        {
            DataTable table = getTxlxList(model.V_LXBM);
            if (table.Rows.Count > 0)
            {
                model.V_TPLJ = table.Rows[0]["V_TPLJ"].ToString();
                if (model.V_LXBM != "014")
                {
                    model.V_DBLJ = table.Rows[0]["V_DBLJ"].ToString() + model.V_DBLJ;
                }
                model.C_OpenFlag = table.Rows[0]["C_OpenFlag"].ToString();
            }
            return PTDBSJAdd(model);
        }
    }
}

