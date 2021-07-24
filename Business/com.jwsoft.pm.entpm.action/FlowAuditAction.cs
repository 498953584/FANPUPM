namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class FlowAuditAction
    {
        public static bool AddInstance(Hashtable instanceInfo)
        {
            return publicDbOpClass.Insert("[WF_Instance]", instanceInfo);
        }

        public static bool AddMessage(Hashtable messageInfo)
        {
            return publicDbOpClass.Insert("[WF_Message]", messageInfo);
        }

        public static bool AddSecondUser(string mainUser, string secondUser, DateTime dtBegin, DateTime dtEnd)
        {
            return publicDbOpClass.NonQuerySqlString(" insert into wf_seconduser values('" + mainUser + "','" + secondUser + "','" + dtBegin.ToShortDateString() + "','" + dtEnd.ToShortDateString() + "')");
        }

        public static string AuditConter(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            string str = "select LinkTable,PrimaryField from WF_BusinessCode where BusinessCode =(";
            object obj2 = str + " select top 1 BusinessCode from wf_instance_main ";
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where BusinessCode='", BusinessCode, "' and BusinessClass = '", BusinessClass, "'and InstanceCode = '", instanceCode, "')" }));
            if (table.Rows.Count > 0)
            {
                DataTable table2 = new DataTable();
                try
                {
                    table2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select Remark from ", table.Rows[0]["LinkTable"].ToString(), " where ", table.Rows[0]["PrimaryField"].ToString(), " = '", instanceCode, "'" }));
                }
                catch
                {
                    return "";
                }
                if (table2.Rows.Count > 0)
                {
                    return table2.Rows[0]["Remark"].ToString();
                }
            }
            return "";
        }

        public static string AuditState(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            string str = "";
            string str2 = "select LinkTable,PrimaryField,StateField from WF_BusinessCode where BusinessCode =(";
            object obj2 = str2 + " select top 1 BusinessCode from wf_instance_main ";
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where BusinessCode='", BusinessCode, "' and BusinessClass = '", BusinessClass, "'and InstanceCode = '", instanceCode, "')" }));
            if (table.Rows.Count > 0)
            {
                DataTable table2 = new DataTable();
                try
                {
                    table2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select ", table.Rows[0]["StateField"].ToString(), " from ", table.Rows[0]["LinkTable"].ToString(), "  where  ", table.Rows[0]["PrimaryField"].ToString(), "  =  '", instanceCode, "'" }));
                }
                catch
                {
                    str = "";
                }
                if (table2.Rows.Count > 0)
                {
                    return table2.Rows[0][0].ToString();
                }
                return "";
            }
            return "";
        }

        public static DataTable AuditYhmcDT(int TemplateID)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, "select top 1 a.Operater as v_yhdm ,(select v_xm from pt_yhmc where v_yhdm = a.Operater) as v_xm from WF_TemplateNode a where a.TemplateID =", TemplateID, "order by NodeID asc" }));
        }

        public static bool BeginFlow(string moduleId, Guid instanceCode, int templateId, string projectCode, string userCode)
        {
            return false;
        }

        public static string BeginFlow(string businessCode, string businessClass, Guid recordID, string projectCode, string userCode)
        {
            string str = "";
            str = JudgeInterface(businessCode, businessClass, recordID, projectCode, userCode);
            if (!(str == ""))
            {
                return str;
            }
            if (ExistsTemplate(businessCode))
            {
                int[] numArray = new int[3];
                if (!IsTemplateNode(GetTemplateNode(businessCode, businessClass, userCode)[0]))
                {
                    str = "请先设置当前模块的审核流程";
                }
                return str;
            }
            return "请先设置当前模块的审核流程";
        }

        public static string BeginFlow(string businessCode, string businessClass, Guid recordID, string projectCode, string userCode, int templateId)
        {
            string str = "";
            str = JudgeInterface(businessCode, businessClass, recordID, projectCode, userCode, templateId);
            if (!(str == ""))
            {
                return str;
            }
            if (IsTemplateNode(templateId))
            {
                int[] numArray = new int[3];
                numArray = GetTemplateNode(businessCode, businessClass, userCode, templateId);
                if (BeginFlow(businessClass, businessCode, recordID, templateId, numArray[1], numArray[2], projectCode, userCode))
                {
                    str = "工作流程已成功启动";
                }
                return str;
            }
            return "尚未定义流程，请与系统管理员联系";
        }

        public static bool BeginFlow(string businessClass, string businessCode, Guid instanceCode, int templateId, int nodeid, int offsetorder, string projectCode, string userCode)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@businessclass", businessClass), new SqlParameter("@businesscode", businessCode), new SqlParameter("@instancecode", instanceCode), new SqlParameter("@templateid", templateId), new SqlParameter("@nodeid", nodeid), new SqlParameter("@offsetorder", offsetorder), new SqlParameter("@projectcode", projectCode), new SqlParameter("@userCode", userCode) };
            bool flag = true;
            try
            {
                publicDbOpClass.ExecuteProcedure("p_wf_beginflow", commandParameters);
            }
            catch (SqlException)
            {
                flag = false;
            }
            return flag;
        }

        public static bool CheckAuditPwd(string UserCode, string Password)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "md5");
            if (publicDbOpClass.ExecuteScalar("select count(*) from pt_login where AuditPwd = '" + str + "'and v_yhdm = '" + UserCode + "' ").ToString() == "0")
            {
                return false;
            }
            return true;
        }

        public static int DealAudit(Guid instanceCode, string BusinessCode, string BusinessClass, int type)
        {
            int num = 0;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select ID from WF_Instance_Main  where InstanceCode='", instanceCode, "' and businessclass='", BusinessClass, "' and businesscode='", BusinessCode, "' ORDER BY StartTime DESC " }));
            bool flag = false;
            if (table.Rows.Count > 1)
            {
                flag = true;
            }
            string str = table.Rows[0][0].ToString();
            DataTable table2 = publicDbOpClass.DataTableQuary("select Sing  from WF_Instance where ID='" + str + "' and TheOrder=(select MIN(TheOrder) from WF_Instance where ID='" + str + "')");
            if (table2.Rows.Count > 0)
            {
                bool flag2 = false;
                for (int i = 0; i < table2.Rows.Count; i++)
                {
                    if (table2.Rows[i][0].ToString() != "0")
                    {
                        flag2 = true;
                        break;
                    }
                }
                if (!flag2 && (type == -1))
                {
                    object obj2 = "delete from wf_instance where [ID] ='" + str + "'";
                    string sqlString = string.Concat(new object[] { obj2, " delete from WF_Instance_Main where instancecode='", instanceCode, "' and businessclass='", BusinessClass, "' and businesscode='", BusinessCode, "' and ID='", str, "'" });
                    if (!flag)
                    {
                        DataTable table3 = publicDbOpClass.DataTableQuary("select linktable,primaryfield,statefield from WF_BusinessCode where businesscode='" + BusinessCode + "'");
                        object obj3 = sqlString;
                        sqlString = string.Concat(new object[] { obj3, " update ", table3.Rows[0]["linktable"], " set ", table3.Rows[0]["statefield"], "=", type, " where ", table3.Rows[0]["primaryfield"], "='", instanceCode, "'" });
                    }
                    else
                    {
                        DataTable table4 = publicDbOpClass.DataTableQuary("select linktable,primaryfield,statefield from WF_BusinessCode where businesscode='" + BusinessCode + "'");
                        object obj4 = sqlString;
                        sqlString = string.Concat(new object[] { obj4, " update ", table4.Rows[0]["linktable"], " set ", table4.Rows[0]["statefield"], "=-3  where ", table4.Rows[0]["primaryfield"], "='", instanceCode, "'" });
                    }
                    num = publicDbOpClass.ExecSqlString(sqlString);
                }
                if (type == -3)
                {
                    publicDbOpClass.ExecSqlString(string.Concat(new object[] { "update wf_instance set AuditResult=-3 , sing=1 where [ID] in(select [ID] from WF_Instance_Main where instancecode='", instanceCode, "' and businessclass='", BusinessClass, "' and businesscode='", BusinessCode, "') and sing=0" }));
                    DataTable table5 = publicDbOpClass.DataTableQuary("select linktable,primaryfield,statefield from WF_BusinessCode where businesscode='" + BusinessCode + "'");
                    num = publicDbOpClass.ExecSqlString(string.Concat(new object[] { " update ", table5.Rows[0]["linktable"], " set ", table5.Rows[0]["statefield"], "=", type, " where ", table5.Rows[0]["primaryfield"], "='", instanceCode, "'" }));
                }
            }
            return num;
        }

        protected static void DelReplaceOperator(Guid instanceCode, string userCode)
        {
            string str = "";
            string str2 = userCode;
            object obj2 = str + "select * from wf_instance where ID = ( ";
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " select ID from WF_Instance_Main where InstanceCode = '", instanceCode, "')" }) + " order by NoteID asc");
            string str3 = " begin";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["Operator"].ToString() == str2)
                {
                    str3 = str3 + " delete wf_instance where NoteID = " + Convert.ToInt32(table.Rows[i]["NoteID"]);
                }
            }
            for (int j = 0; j < table.Rows.Count; j++)
            {
                str2 = table.Rows[j]["Operator"].ToString();
                for (int k = j; k < table.Rows.Count; k++)
                {
                    if ((table.Rows[k]["Operator"].ToString() == str2) && (j != k))
                    {
                        str3 = str3 + " delete wf_instance where NoteID = " + Convert.ToInt32(table.Rows[j]["NoteID"]);
                    }
                }
            }
            publicDbOpClass.ExecSqlString(str3 + " end");
        }

        public static bool DelSecondUser(string mainUser)
        {
            return publicDbOpClass.NonQuerySqlString(" delete from wf_seconduser where mainuser='" + mainUser + "'");
        }

        public static bool display_FlowChart(HtmlTable tbFlowChart, Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            DataTable table = QueryFlowStatus(instanceCode, BusinessCode, BusinessClass);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    HtmlTableRow row2 = new HtmlTableRow();
                    DataRow row = table.Rows[i];
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        HtmlTableCell cell = new HtmlTableCell();
                        int length = 0;
                        string str = "";
                        string text = "";
                        string str3 = "";
                        string str4 = "";
                        if (j > 0)
                        {
                            if (!(row.ItemArray[j].ToString() != "") || (row.ItemArray[j] == null))
                            {
                                goto Label_0284;
                            }
                            cell.Attributes["style"] = "cursor:hand";
                            cell.Attributes["width"] = "45";
                            cell.Attributes["height"] = "45";
                            cell.Attributes["wrap"] = "true";
                            str = row.ItemArray[j].ToString();
                            length = str.IndexOf(";");
                            if (length <= 0)
                            {
                                goto Label_0259;
                            }
                            str3 = str.Substring(0, length);
                            str4 = str.Substring(length + 1, (str.Length - length) - 1);
                            text = "<div align=\"center\"><img src=\"img/image" + str3 + ".gif\" width=\"45\" height=\"20\"><br><font size=\"-1\">" + str4 + "</font></div>";
                            cell.Attributes["onclick"] = "changebkcolor(this);";
                            string str5 = str3;
                            if (str5 != null)
                            {
                                if (!(str5 == "-2"))
                                {
                                    if (str5 == "-1")
                                    {
                                        goto Label_01F6;
                                    }
                                    if (str5 == "0")
                                    {
                                        goto Label_0211;
                                    }
                                    if (str5 == "1")
                                    {
                                        goto Label_0229;
                                    }
                                    if (str5 == "2")
                                    {
                                        goto Label_0241;
                                    }
                                }
                                else
                                {
                                    cell.Attributes["tag"] = "已超时";
                                }
                            }
                        }
                        goto Label_02A1;
                    Label_01F6:
                        cell.Attributes["tag"] = "未到达";
                        goto Label_02A1;
                    Label_0211:
                        cell.Attributes["tag"] = "到达未审核";
                        goto Label_02A1;
                    Label_0229:
                        cell.Attributes["tag"] = "已审核";
                        goto Label_02A1;
                    Label_0241:
                        cell.Attributes["tag"] = "未处理已通过";
                        goto Label_02A1;
                    Label_0259:
                        cell.Attributes["style"] = "cursor:default";
                        text = "<img src=\"img/img" + str + ".gif\" width=\"45\" height=\"45\">";
                        goto Label_02A1;
                    Label_0284:
                        cell.Attributes["style"] = "cursor:default";
                        text = "";
                    Label_02A1:
                        cell.Controls.Add(new LiteralControl(text));
                        row2.Cells.Add(cell);
                    }
                    tbFlowChart.Rows.Add(row2);
                }
            }
            return true;
        }

        public static string DoWithUrl(string BusinessCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from WF_BusinessCode where BusinessCode='" + BusinessCode + "'");
            if (table.Rows.Count <= 0)
            {
                return "about:blank";
            }
            string str2 = table.Rows[0]["DoWithUrl"].ToString();
            if (str2.IndexOf('?') != -1)
            {
                return (str2 + "&");
            }
            return (str2 + "?");
        }

        protected static bool ExistsTemplate(string businessCode)
        {
            return (publicDbOpClass.DataTableQuary("select * from wf_template where BusinessCode = '" + businessCode + "'").Rows.Count > 0);
        }

        public static bool FirstNodeIsSelected(int TempleteID)
        {
            if (publicDbOpClass.ExecuteScalar("SELECT count(*) FROM WF_TemplateNode WHERE (TemplateID = " + TempleteID + ") and IsSelReceiver = 1 and FrontNode = 0").ToString() == "0")
            {
                return false;
            }
            return true;
        }

        public static string[] GetAllFront(int noteid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select [id],theorder from WF_Instance where noteid=" + noteid);
            table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from WF_Instance where [id]=", table.Rows[0]["id"], "  and theorder <", table.Rows[0]["theOrder"] }));
            string[] strArray = new string[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                strArray[i] = table.Rows[i]["Operator"].ToString();
            }
            return strArray;
        }

        public static DataTable GetAuditInfo(string InstanceCode)
        {
            return publicDbOpClass.DataTableQuary(string.Format("\r\n                SELECT AuditDate,AuditInfo,AuditNameImagePath FROM WF_Instance_Main AS T1\r\n                JOIN WF_Instance AS T2 ON T1.ID=T2.ID\r\n                JOIN pt_login AS T3 ON T2.Operator=T3.v_yhdm\r\n                WHERE InstanceCode='{0}' AND T2.ID=(SELECT MAX( id) FROM wf_instance_main WHERE InstanceCode = '{0}')\r\n                ORDER BY TheOrder DESC", InstanceCode));
        }

        public static string GetAuditorType(int Noteid)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from WF_TemplateNode  where NodeID  in (select NodeID from wf_instance where [id] in(select [Id] from wf_instance where Noteid=", Noteid, ") and (TheOrder=(select TheOrder+1.0 from wf_instance where Noteid=", Noteid, ") or TheOrder=(select TheOrder+0.5 from wf_instance where Noteid=", Noteid, "))) " }));
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["AuditorType"].ToString();
            }
            return "1";
        }

        public static DataTable GetAuditUser(int InstanceID)
        {
            DataTable table = publicDbOpClass.DataTableQuary(" select theOrder,IsInsertedFrontNode ,ID,Sing from wf_instance where NoteID  =  " + InstanceID);
            double d = Convert.ToDouble(table.Rows[0]["theOrder"]);
            if (table.Rows[0]["IsInsertedFrontNode"].ToString() == "1")
            {
                d -= 0.5;
            }
            else if (table.Rows[0]["Sing"].ToString() == "1")
            {
                d += 0.5;
            }
            else
            {
                d++;
                d = Math.Floor(d);
            }
            object obj2 = (" select * ,(SELECT BusinessClassName FROM WF_Business_Class AS d " + " WHERE (BusinessCode = a.BusinessCode) " + " AND (BusinessClass = a.BusinessClass)) AS BusinessClassName ") + " from  WF_Instance_Main a inner join  wf_instance b " + " on a.ID = b.ID ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where b.ID  = '", table.Rows[0]["ID"].ToString(), "' and TheOrder = ", d }));
        }

        public static string GetBusinessClassName(string businesscode, string businessclass)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select businessclassname from WF_Business_Class where businesscode='" + businesscode + "' and businessclass='" + businessclass + "'");
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0]["businessclassName"].ToString();
            }
            return str2;
        }

        public static string GetBusinessCode(Guid InstanceCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from WF_Instance_Main where InstanceCode ='" + InstanceCode + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["BusinessCode"].ToString();
            }
            return "";
        }

        public static DataTable GetbusinesscodeTable(int noteid)
        {
            string str = "select primaryfield,linktable,StateField from wf_businesscode inner join WF_Instance_Main on wf_businesscode.businesscode=WF_Instance_Main.businesscode";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where WF_Instance_Main.id in (select [id] from WF_Instance where noteid=", noteid, ")" }));
        }

        public static int GetCount(string Operator)
        {
            int index = 0;
            int count = 0;
            index = Operator.IndexOf(",");
            if (index > 0)
            {
                while (index > 0)
                {
                    count++;
                    Operator = Operator.Substring(index + 1, (Operator.Length - index) - 1);
                    index = Operator.IndexOf(",");
                }
                count++;
                return count;
            }
            if (Operator.Length == 8)
            {
                return 1;
            }
            DataTable table = FlowTemplateAction.QueryRoleUsers(Convert.ToInt32(Operator));
            if (table.Rows.Count > 0)
            {
                count = table.Rows.Count;
            }
            return count;
        }

        public static string GetCurrentAuditorType(int Noteid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT *  FROM  WF_TemplateNode  where NodeID  in( SELECT NodeID FROM WF_Instance WHERE NoteID=" + Noteid + ") ");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["AuditorType"].ToString();
            }
            return "";
        }

        public static string GetDueMode(int NodeId)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select DueMode from WF_TemplateNode where nodeid=" + NodeId);
            string str2 = "2";
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0]["DueMode"].ToString();
            }
            return str2;
        }

        public static DataTable GetDuring(int InstanceID)
        {
            return publicDbOpClass.DataTableQuary(" select theOrder,IsInsertedFrontNode ,ID,During,ArriveTime from wf_instance where NoteID  =  " + InstanceID);
        }

        public static DataTable GetFistDt(int TempleteId)
        {
            return publicDbOpClass.DataTableQuary("select top 1 NodeID,AuditorType,Operater,IsSelReceiver, DepCode from dbo.WF_TemplateNode where TemplateID=" + TempleteId + " AND FrontNode='0' order by NodeID asc");
        }

        public static DataTable GetInstanceMain(int noteid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select [id] from wf_instance where noteid=" + noteid);
            return publicDbOpClass.DataTableQuary("select * from wf_instance_main where [id]=" + table.Rows[0]["id"]);
        }

        public static DataTable GetLastAuditUser(int InstanceID)
        {
            string str = "";
            double num = 0.0;
            DataTable table = publicDbOpClass.DataTableQuary("" + " select theOrder,IsInsertedFrontNode ,ID from wf_instance where NoteID  =  " + InstanceID);
            if (table.Rows.Count > 0)
            {
                if (table.Rows[0]["IsInsertedFrontNode"].ToString() == "1")
                {
                    num = Convert.ToDouble(table.Rows[0]["theOrder"]) - 0.5;
                    if (publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from wf_instance where ID = '", table.Rows[0]["ID"].ToString(), "' and TheOrder = ", num })).Rows.Count <= 0)
                    {
                        num -= 0.5;
                    }
                }
                else
                {
                    num = Convert.ToDouble(table.Rows[0]["theOrder"]);
                    if ((num > Convert.ToInt32((double) (num - 0.5))) && (num < Convert.ToInt32(num)))
                    {
                        num -= 0.5;
                    }
                    else
                    {
                        num--;
                    }
                }
            }
            object obj2 = ((str + " select * ,(SELECT BusinessClassName FROM WF_Business_Class AS d ") + " WHERE (BusinessCode = a.BusinessCode) " + " AND (BusinessClass = a.BusinessClass)) AS BusinessClassName ") + " from  WF_Instance_Main a inner join  wf_instance b " + " on a.ID = b.ID ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where b.ID  = '", table.Rows[0]["ID"].ToString(), "' and Sing = 1 and TheOrder = ", num }));
        }

        public static string GetMaxSing(int noteid)
        {
            DataTable table = publicDbOpClass.DataTableQueryNoEncryptDog("select [id] from WF_Instance where noteid=" + noteid);
            table = publicDbOpClass.DataTableQueryNoEncryptDog(string.Concat(new object[] { "select sing from WF_Instance where [id]=", table.Rows[0]["id"], "  and theorder =(select max(theorder) from wf_instance where [id]=", table.Rows[0]["id"], ")" }));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if ((table.Rows[i][0].ToString() == "0") || (table.Rows[i][0].ToString() == "-1"))
                {
                    return "0";
                }
            }
            return "1";
        }

        public static string GetMsgRecievers(string Operator, int nodeId)
        {
            string str = Operator;
            DataTable table = QuerySecondUser(Operator, nodeId);
            if (table.Rows.Count > 0)
            {
                str = str + "," + table.Rows[0]["SecondUser"].ToString();
            }
            return str;
        }

        public static string GetNextAuditDep(int noteId)
        {
            string str = string.Empty;
            string spName = "\r\n\t\t\t\t--declare @NoteId nvarchar(200)\r\n\t\t\t\t--set @NoteId = '7525'\r\n\t\t\t\tselect t.DepCode\r\n\t\t\t\tfrom wf_instance w\r\n\t\t\t\tleft join WF_TemplateNode t on w.NodeId = t.NodeId\r\n\t\t\t\twhere w.Id in (select [Id] from wf_instance where Noteid=@NoteId) \r\n\t\t\t\t\tand ( \r\n\t\t\t\t\t\tTheOrder=(select TheOrder+1.0 from wf_instance where Noteid=@NoteId) \r\n\t\t\t\t\t\tor TheOrder=(select TheOrder+0.5 from wf_instance where Noteid=@NoteId)\r\n\t\t\t\t\t)\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NoteId", noteId) };
            DataTable table = publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, commandParameters);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0][0].ToString();
            }
            return str;
        }

        public static bool GetNextOperator(int Noteid)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select operator from wf_instance where [id] in(select [Id] from wf_instance where Noteid=", Noteid, ") and ( TheOrder=(select TheOrder+1.0 from wf_instance where Noteid=", Noteid, ") or TheOrder=(select TheOrder+0.5 from wf_instance where Noteid=", Noteid, ") )" }));
            bool flag = true;
            if ((table.Rows.Count > 0) && string.IsNullOrEmpty(table.Rows[0]["Operator"].ToString()))
            {
                flag = false;
            }
            return flag;
        }

        public static int GetNodeCount(int templateId, int nodeId, int xh)
        {
            DataTable table = FlowTemplateAction.QueryOffsetNodeFirst(nodeId.ToString(), templateId);
            if (table.Rows.Count == 1)
            {
                xh++;
                int num = Convert.ToInt32(table.Rows[0]["NodeID"].ToString());
                GetNodeCount(templateId, num, xh);
                return xh;
            }
            int count = table.Rows.Count;
            return xh;
        }

        public static string[] GetOffsetNodeFirst(int templateId, string businessClass, Guid recordID)
        {
            string[] strArray = new string[3];
            if (FlowChartAction.isExistKeyNode(templateId))
            {
                int posPositionNode = FlowChartAction.GetPosPositionNode(0, templateId);
                if (posPositionNode == 1)
                {
                    int nodeID = 0;
                    int orderNumber = 0;
                    foreach (DataRow row in FlowTemplateAction.QueryNodeList(templateId).Rows)
                    {
                        orderNumber++;
                        nodeID = Convert.ToInt32(row["NodeID"].ToString());
                        if (FlowChartAction.GetPosPositionNode(nodeID, templateId) > 1)
                        {
                            return SelectOffSet(templateId, nodeID.ToString(), orderNumber, businessClass, recordID);
                        }
                    }
                    return strArray;
                }
                if (posPositionNode > 1)
                {
                    strArray = SelectOffSet(templateId, "0", 0, businessClass, recordID);
                }
                return strArray;
            }
            return SelectOffSet(templateId, "0", 0, businessClass, recordID);
        }

        public static string GetProject(string businesscode, Guid instrancecode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from WF_BusinessCode Where BusinessCode='" + businesscode + "'");
            try
            {
                return publicDbOpClass.DataTableQuary("select   " + table.Rows[0]["ProjectField"].ToString() + "   from   " + table.Rows[0]["LinkTable"].ToString() + "  where   " + table.Rows[0]["PrimaryField"].ToString() + "  = '" + instrancecode.ToString() + "' ").Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }
        }

        public static DataTable GetRole(string rowId)
        {
            return publicDbOpClass.DataTableQuary("select RoleName from WF_Role where RoleID='" + rowId + "'");
        }

        public static string GetStartDateTime(Guid InstanceCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from WF_Instance_Main where InstanceCode ='" + InstanceCode + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["StartTime"].ToString();
            }
            return "";
        }

        public static string GetStartFlowUserCode(string instanceCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar(string.Format("SELECT top(1) Organiger FROM WF_Instance_Main \r\n                WHERE InstanceCode='{0}'\r\n                ORDER BY StartTime DESC", instanceCode));
            return ((obj2 == null) ? string.Empty : obj2.ToString());
        }

        public static string GetTemplateCode(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from WF_Template  where TemplateID in (select TemplateID from wf_instance_main where  BusinessCode='", BusinessCode, "' and BusinessClass = '", BusinessClass, "'and InstanceCode = '", instanceCode, "') " }));
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["TemplateCode"].ToString();
            }
            DataTable table2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from wf_instance_main where  BusinessCode='", BusinessCode, "' and BusinessClass = '", BusinessClass, "'and InstanceCode = '", instanceCode, "'" }));
            if (table2.Rows.Count > 0)
            {
                string str4 = str;
                str = str4 + "－" + Convert.ToDateTime(table2.Rows[0]["StartTime"]).ToString("yyyy-MM-dd").Replace("-", "") + "－" + table2.Rows[0]["ID"].ToString();
            }
            return str;
        }

        public static int[] GetTemplateNode(string businessCode, string businessClass, string userCode)
        {
            int[] numArray = new int[3];
            DataTable table = FlowTemplateAction.QueryTemplateByClass(businessClass, businessCode, userCode);
            if (table.Rows.Count == 1)
            {
                numArray[0] = Convert.ToInt32(table.Rows[0]["TemplateID"].ToString());
                object obj2 = publicDbOpClass.ExecuteScalar(" select Min(NodeId) as nodeId from wf_TemplateNode where TemplateID = " + numArray[0].ToString());
                numArray[1] = Convert.ToInt32(obj2);
                numArray[2] = 1;
            }
            return numArray;
        }

        public static int[] GetTemplateNode(string businessCode, string businessClass, string userCode, int templateId)
        {
            int[] numArray = new int[3];
            numArray[0] = templateId;
            object obj2 = publicDbOpClass.ExecuteScalar(" select Min(NodeId) as nodeId from wf_TemplateNode where TemplateID = " + numArray[0].ToString());
            numArray[1] = Convert.ToInt32(obj2);
            numArray[2] = 1;
            return numArray;
        }

        public static string GetUsers(string users)
        {
            if (!(users != ""))
            {
                return "";
            }
            string str = "";
            string str2 = "";
            foreach (string str3 in users.Split(new char[] { ',' }))
            {
                if (str3 != "")
                {
                    if (str2 == "")
                    {
                        str2 = "'" + str3 + "'";
                    }
                    else
                    {
                        str2 = str2 + ",'" + str3 + "'";
                    }
                }
            }
            DataTable table = publicDbOpClass.DataTableQuary("select v_xm from pt_yhmc where v_yhdm in (" + str2 + ")");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                str = str + table.Rows[i][0].ToString() + ",";
            }
            if (str.Length > 0)
            {
                return str.Substring(0, str.Length - 1);
            }
            return null;
        }

        public static bool GetUsersis(string users)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            if (users != "")
            {
                string str = "";
                string[] strArray = users.TrimEnd(new char[] { ',' }).Split(new char[] { ',' });
                foreach (string str2 in strArray)
                {
                    if (str2 != "")
                    {
                        if (str == "")
                        {
                            str = "'" + str2 + "'";
                        }
                        else
                        {
                            str = str + ",'" + str2 + "'";
                        }
                    }
                }
                builder.Append("SELECT * FROM pt_yhmc WHERE v_yhdm in (" + str + ")");
                table = publicDbOpClass.DataTableQuary(builder.ToString());
                if ((table.Rows.Count == 0) || (table.Rows.Count != strArray.Length))
                {
                    return false;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["State"].ToString() == "2")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static DataTable ImageListPath(string yhdm)
        {
            return publicDbOpClass.DataTableQuary("(select * from pt_login where V_YHDM='" + yhdm + "') ");
        }

        public static bool InsertBeginOffset(int id, int xh, int templateId, int nodeId)
        {
            string frontNode = "0";
            Hashtable[] hashtableArray = new Hashtable[xh];
            for (int i = 0; i < xh; i++)
            {
                hashtableArray[i] = new Hashtable();
                if (i == (xh - 1))
                {
                    DataRow row = FlowTemplateAction.QueryNodeList(templateId, nodeId).Rows[0];
                    hashtableArray[i].Add("ID", id.ToString());
                    hashtableArray[i].Add("NodeID", nodeId.ToString());
                    hashtableArray[i].Add("NodeName", row["NodeName"].ToString());
                    hashtableArray[i].Add("Operator", SqlStringConstructor.GetQuotedString(row["Operater"].ToString()));
                    int num2 = i + 1;
                    hashtableArray[i].Add("TheOrder", num2.ToString());
                    hashtableArray[i].Add("IsSendMsg", SqlStringConstructor.GetQuotedString(row["IsSendMsg"].ToString()));
                    hashtableArray[i].Add("IsAllPass", SqlStringConstructor.GetQuotedString(row["IsAllPass"].ToString()));
                    hashtableArray[i].Add("During", row["During"].ToString());
                }
                else
                {
                    DataRow row2 = FlowTemplateAction.QueryOffsetNodeFirst(frontNode, templateId).Rows[0];
                    hashtableArray[i].Add("ID", id.ToString());
                    hashtableArray[i].Add("NodeID", row2["NodeID"].ToString());
                    hashtableArray[i].Add("NodeName", row2["NodeName"].ToString());
                    hashtableArray[i].Add("Operator", SqlStringConstructor.GetQuotedString(row2["Operater"].ToString()));
                    int num3 = i + 1;
                    hashtableArray[i].Add("TheOrder", num3.ToString());
                    hashtableArray[i].Add("IsSendMsg", SqlStringConstructor.GetQuotedString(row2["IsSendMsg"].ToString()));
                    hashtableArray[i].Add("IsAllPass", SqlStringConstructor.GetQuotedString(row2["IsAllPass"].ToString()));
                    hashtableArray[i].Add("During", row2["During"].ToString());
                    frontNode = row2["NodeID"].ToString();
                }
                if (i == 0)
                {
                    hashtableArray[i].Add("Sing", "0");
                }
                else
                {
                    hashtableArray[i].Add("Sing", "-1");
                }
                if (!FlowTemplateAction.AddInstance(hashtableArray[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool InsertInstance(int id, int xh, int templateId, int nodeId)
        {
            if (!InsertBeginOffset(id, xh, templateId, nodeId))
            {
                return false;
            }
            string frontNode = nodeId.ToString();
            int num = GetNodeCount(templateId, nodeId, xh);
            Hashtable[] hashtableArray = new Hashtable[num - xh];
            for (int i = 0; i < (num - xh); i++)
            {
                DataRow row = FlowTemplateAction.QueryOffsetNodeFirst(frontNode, templateId).Rows[0];
                hashtableArray[i].Add("ID", id.ToString());
                hashtableArray[i].Add("NodeID", row["NodeID"].ToString());
                hashtableArray[i].Add("NodeName", row["NodeName"].ToString());
                hashtableArray[i].Add("Operator", SqlStringConstructor.GetQuotedString(row["Operater"].ToString()));
                int num3 = (i + xh) + 1;
                hashtableArray[i].Add("TheOrder", num3.ToString());
                hashtableArray[i].Add("IsSendMsg", SqlStringConstructor.GetQuotedString(row["IsSendMsg"].ToString()));
                hashtableArray[i].Add("IsAllPass", SqlStringConstructor.GetQuotedString(row["IsAllPass"].ToString()));
                hashtableArray[i].Add("During", row["During"].ToString());
                hashtableArray[i].Add("Sing", "-1");
                frontNode = row["NodeID"].ToString();
                if (!FlowTemplateAction.AddInstance(hashtableArray[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static void InsertNode(int instanceId, string userCode, string insertType, string isAllPass, string insertAuditor, bool auditResult, string auditInfo, string AuditRemark)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", instanceId), new SqlParameter("@userCode", userCode), new SqlParameter("@insertType", insertType), new SqlParameter("@isallpass", isAllPass), new SqlParameter("@InsertAuditor", insertAuditor), new SqlParameter("@auditResult", auditResult), new SqlParameter("@auditInfo", auditInfo), new SqlParameter("@AuditRemark", AuditRemark) };
            publicDbOpClass.ExecuteProcedure("p_wf_insertnode", commandParameters);
        }

        public static int InsertOperator(int Noteid, string Operator)
        {
            string sqlString = string.Concat(new object[] { "Update wf_instance set Operator='", Operator, "' where [id] in(select [Id] from wf_instance where Noteid=", Noteid, ") and TheOrder=(select TheOrder+1.0 from wf_instance where Noteid=", Noteid, ")" });
            return publicDbOpClass.ExecSqlString(sqlString);
        }

        public static int InsertOperator(string businessclass, string businesscode, Guid instancecode, string Operator)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { " exec p_wf_IsSelReceiverflow '", businessclass, "','", businesscode, "','", instancecode, "','", Operator, "'" }));
        }

        public static bool IsAuditOver(int InstanceID)
        {
            string sqlString = " select isnull(Max(TheOrder),0) from wf_instance  where ID =(select ID from wf_instance \twhere NoteID  = " + InstanceID + " )";
            decimal num = 0M;
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if (table.Rows.Count > 0)
            {
                num = Convert.ToDecimal(table.Rows[0][0]);
            }
            DataTable table2 = publicDbOpClass.DataTableQuary(" select TheOrder from wf_instance\twhere NoteID  = " + InstanceID + " ");
            decimal num2 = 0M;
            if (table2.Rows.Count > 0)
            {
                num2 = Convert.ToDecimal(table2.Rows[0][0]);
            }
            return (num == num2);
        }

        private static bool IsTemplateNode(int TemplateID)
        {
            return (publicDbOpClass.DataTableQuary(" select * from wf_TemplateNode where TemplateID = " + TemplateID).Rows.Count > 0);
        }

        public static string JudgeInterface(string businessCode, string businessClass, Guid recordID, string projectCode, string userCode)
        {
            int count = 0;
            int templateID = 0;
            string str = "";
            string[] strArray = new string[3];
            DataTable table = FlowTemplateAction.QueryTemplateByClass(businessClass, businessCode, userCode);
            count = table.Rows.Count;
            if (count == 1)
            {
                templateID = Convert.ToInt32(table.Rows[0]["TemplateID"].ToString());
                int posPositionNode = FlowChartAction.GetPosPositionNode(0, templateID);
                if (posPositionNode > 1)
                {
                    strArray = SelectOffSet(templateID, "0", 0, businessClass, recordID);
                    if (strArray[2].ToString() == "0")
                    {
                        return "";
                    }
                    return ("/EPC/Workflow/StartWf.aspx?code=" + businessCode + "&class=" + businessClass.ToString() + "&record=" + recordID.ToString() + "&mbs=" + count.ToString() + "&lx=" + posPositionNode.ToString() + "&node=" + strArray[0].ToString() + "&project=" + projectCode.ToString());
                }
                switch (posPositionNode)
                {
                    case 1:
                    {
                        DataTable table2 = FlowTemplateAction.QueryOffsetNodeFirst("0", templateID);
                        string str2 = table2.Rows[0]["IsSelReceiver"].ToString();
                        int num4 = Convert.ToInt32(table2.Rows[0]["NodeID"].ToString());
                        if (str2 == "0")
                        {
                            return "";
                        }
                        return ("/EPC/Workflow/StartWf.aspx?code=" + businessCode + "&class=" + businessClass.ToString() + "&record=" + recordID.ToString() + "&mbs=" + count.ToString() + "&lx=" + posPositionNode.ToString() + "&node=" + num4.ToString() + "&project=" + projectCode.ToString());
                    }
                    case 0:
                        str = "none";
                        break;
                }
                return str;
            }
            if (count > 1)
            {
                str = "/EPC/Workflow/StartWf.aspx?code=" + businessCode + "&class=" + businessClass.ToString() + "&record=" + recordID.ToString() + "&mbs=" + count.ToString() + "&project=" + projectCode.ToString();
            }
            return str;
        }

        public static string JudgeInterface(string businessCode, string businessClass, Guid recordID, string projectCode, string userCode, int templateId)
        {
            int num = 1;
            string str = "";
            string[] strArray = new string[3];
            int posPositionNode = FlowChartAction.GetPosPositionNode(0, templateId);
            if (posPositionNode > 1)
            {
                strArray = SelectOffSet(templateId, "0", 0, businessClass, recordID);
                if (strArray[2].ToString() == "0")
                {
                    return "";
                }
                return string.Concat(new object[] { "../../oa/UserDefineFlow/MoFlowStartWf.aspx?code=", businessCode, "&class=", businessClass.ToString(), "&record=", recordID.ToString(), "&mbs=", num.ToString(), "&lx=", posPositionNode.ToString(), "&node=", strArray[0].ToString(), "&project=", projectCode.ToString(), "&it=", templateId });
            }
            if (posPositionNode != 1)
            {
                return str;
            }
            DataTable table = FlowTemplateAction.QueryOffsetNodeFirst("0", templateId);
            string str2 = table.Rows[0]["IsSelReceiver"].ToString();
            int num3 = Convert.ToInt32(table.Rows[0]["NodeID"].ToString());
            if (str2 == "0")
            {
                return "";
            }
            return string.Concat(new object[] { "../../oa/UserDefineFlow/MoFlowStartWf.aspx?code=", businessCode, "&class=", businessClass.ToString(), "&record=", recordID.ToString(), "&mbs=", num.ToString(), "&lx=", posPositionNode.ToString(), "&node=", num3.ToString(), "&project=", projectCode.ToString(), "&it=", templateId });
        }

        public static DataTable OrganigerCode(int InstanceID)
        {
            string str = " select Organiger,InstanceCode,(SELECT BusinessClassName FROM WF_Business_Class where BusinessCode = a.BusinessCode and BusinessClass = a.BusinessClass) as BusinessClassName from wf_instance_main a where ID = ";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " ( select ID from wf_instance \twhere NoteID  = ", InstanceID, " )" }));
        }

        public static string OrganigerName(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            string str = "select (select v_xm from pt_yhmc where v_yhdm = Organiger)as OrganigerName from wf_instance_main ";
            object obj2 = str;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, "where BusinessCode='", BusinessCode, "' and BusinessClass = '", BusinessClass, "'and InstanceCode = '", instanceCode, "'" }));
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["OrganigerName"].ToString();
            }
            return "";
        }

        public static DataTable OutTimeAudit()
        {
            string sqlString = "select * from WF_Instance where DATEDIFF(hh, OutOfTime, GETDATE())>0 and sing=0";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static void ProcessFlow(int instanceId, string isAllPass, string userCode, bool auditResult, string auditInfo)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", instanceId), new SqlParameter("@isallpass", isAllPass), new SqlParameter("@userCode", userCode), new SqlParameter("@auditResult", auditResult), new SqlParameter("@auditInfo", auditInfo) };
            publicDbOpClass.ExecuteProcedure("p_wf_processflow", commandParameters);
        }

        public static void ProcessFlow(int instanceId, string isAllPass, string userCode, int auditResult, string auditInfo)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", instanceId), new SqlParameter("@isallpass", isAllPass), new SqlParameter("@userCode", userCode), new SqlParameter("@auditResult", auditResult), new SqlParameter("@auditInfo", auditInfo) };
            publicDbOpClass.ExecuteProcedure("p_wf_processflow", commandParameters);
        }

        public static DataTable QueryAuditPassStatus(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n                    SELECT NodeName,AuditInfo,AuditDate,yh.v_xm AS nodeUserName,lg.AuditNameImagePath AS e_signature,\r\n                        CASE yh.i_bmdm\r\n\t                       WHEN 7 THEN '1'\r\n\t                       ELSE '0'\r\n\t                    END AS fawubu  --是否是法务部\r\n                    FROM wf_instance AS wf\r\n                    JOIN pt_yhmc AS yh ON wf.Operator=yh.v_yhdm\r\n                    JOIN pt_login AS lg ON wf.Operator=lg.v_yhdm\r\n                    WHERE wf.ID=(SELECT MAX( id) FROM wf_instance_main WHERE BusinessCode='{0}' \r\n\t                    AND BusinessClass = '{1}' AND InstanceCode = '{2}') \r\n                    AND wf.AuditResult='1'\r\n                    ORDER BY wf.theOrder DESC", BusinessCode, BusinessClass, instanceCode);
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static object QueryAuditStatus(Guid instanceCode, int nodeId, decimal theOrder)
        {
            return publicDbOpClass.ExecuteScalar("select a.sing from wf_instance a, wf_instance_main b where a.id = b.id and b.instanceCode ='" + instanceCode.ToString() + "' and a.nodeID =" + nodeId.ToString() + " and a.theOrder =" + theOrder.ToString());
        }

        public static DataTable QueryAuditStatus(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            string spName = "\r\n\t\t\t\t--DECLARE @IC nvarchar(200), \r\n\t\t\t\t--\t\t@BusinessCode nvarchar(200), \r\n\t\t\t\t--\t\t@BusinessClass nvarchar(200);\r\n\t\t\t\t--SET @IC = '9fe45799-b6d2-4246-8e7b-268c1cb3caa8'\r\n\t\t\t\t--SET @BusinessCode  = '103'\r\n\t\t\t\t--SET @BusinessClass  = '001';\r\n\t\t\t\tSELECT i.*, y.v_xm AS auditor, (\r\n\t\t\t\t\t\tCASE WHEN AuditResult='1' THEN '通过' WHEN AuditResult='-2' THEN '驳回' \r\n\t\t\t\t\t\tWHEN AuditResult='-3' THEN '重报' ELSE '' END\r\n\t\t\t\t\t) AS Result, \r\n\t\t\t\t\t(\r\n\t\t\t\t\t\tCASE WHEN sing = -1 THEN '未到达' WHEN sing=0 THEN '到达未审核' \r\n\t\t\t\t\t\tWHEN sing='1' THEN '已审核' WHEN sing='2' THEN '未处理已通过' ELSE '已超时' END\r\n\t\t\t\t\t) AS Status \r\n\t\t\t\tFROM WF_Instance i\r\n\t\t\t\tJOIN WF_Instance_Main im ON im.ID = i.ID \r\n\t\t\t\tLEFT JOIN PT_Yhmc y ON y.v_yhdm = i.Operator\r\n\t\t\t\tWHERE im.InstanceCode = @IC\r\n\t\t\t\t\tAND im.BusinessCode=@BusinessCode \r\n\t\t\t\t\tAND im.BusinessClass = @BusinessClass\r\n\t\t\t\t\tAND NoteID NOT IN (\t\t\t\t\t\t\t\t--过滤到不是最后一次提交的，没有到达的节点\r\n\t\t\t\t\t\tSELECT NoteID FROM WF_Instance\r\n\t\t\t\t\t\tWHERE ID <> (\t\t\t\r\n\t\t\t\t\t\t\tSELECT MAX(ID) FROM  wf_instance_main \r\n\t\t\t\t\t\t\twhere BusinessCode=@BusinessCode and BusinessClass = @BusinessClass and InstanceCode = @IC \r\n\t\t\t\t\t\t) AND sing =-1 \r\n\t\t\t\t\t)\r\n                ORDER BY ID, TheOrder\r\n\t\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@IC", instanceCode), new SqlParameter("@BusinessCode", BusinessCode), new SqlParameter("@BusinessClass", BusinessClass) };
            return publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, commandParameters);
        }

        public static DataTable QueryFlow(Guid instanceCode, string userCode)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@instancecode", instanceCode), new SqlParameter("@userCode", userCode) };
            return publicDbOpClass.ExecuteDataTable("p_wf_QueryFlow", commandParameters);
        }

        public static DataTable QueryFlowStatus(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@instancecode", instanceCode), new SqlParameter("@BusinessCode", BusinessCode), new SqlParameter("@BusinessClass", BusinessClass) };
            return publicDbOpClass.ExecuteDataTable("p_wf_queryflowchart", commandParameters);
        }

        public static object QueryInstanceID(Guid instanceCode)
        {
            return publicDbOpClass.ExecuteScalar(" select distinct ID from WF_Instance_Main where InstanceCode = '" + instanceCode.ToString() + "'");
        }

        public static DataTable QueryInstanceOperator(Guid instanceCode, int nodeId, decimal theOrder)
        {
            return publicDbOpClass.DataTableQuary("select a.Operator,a.IsAllPass,b.id from wf_instance a, wf_instance_main b where a.id = b.id and b.instanceCode ='" + instanceCode.ToString() + "' and a.nodeID =" + nodeId.ToString() + " and a.theOrder =" + theOrder.ToString());
        }

        public static string QueryRocord(int moduleId, string userCode, int isSelf)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@moduleid", moduleId), new SqlParameter("@userCode", userCode), new SqlParameter("@isSelf", isSelf) };
            DataTable table = publicDbOpClass.ExecuteDataTable("p_wf_queryrecord", commandParameters);
            string str = "";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i != (table.Rows.Count - 1))
                {
                    str = str + "'" + table.Rows[i]["instancecode"].ToString() + "',";
                }
                else
                {
                    str = str + "'" + table.Rows[i]["instancecode"].ToString() + "'";
                }
            }
            return str;
        }

        public static DataTable QuerySecondUser(string userCode, int nodeId)
        {
            return publicDbOpClass.DataTableQuary("select * from WF_Agent where MainUser='" + userCode + "' and NodeID=" + nodeId.ToString());
        }

        public static bool SelectNextOperate(int noteid, string operates, int operatetype, string userCode, string projectCode)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@noteid", noteid), new SqlParameter("@operates", operates), new SqlParameter("@operatetype", operatetype), new SqlParameter("@userCode", userCode), new SqlParameter("@projectCode", projectCode) };
            bool flag = true;
            try
            {
                publicDbOpClass.ExecuteProcedure("p_wf_SelNextOperateflow", commandParameters);
            }
            catch (SqlException)
            {
                flag = false;
            }
            return flag;
        }

        public static string[] SelectOffSet(int templateId, string frontNode, int orderNumber, string businessClass, Guid recordID)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string[] strArray = new string[3];
            DataTable table = FlowTemplateAction.QueryBusiness(businessClass, templateId);
            if (table.Rows.Count == 1)
            {
                str = table.Rows[0]["LinkTable"].ToString();
                str2 = table.Rows[0]["PrimaryField"].ToString();
                str3 = "select count(*) from " + str.ToString() + " where " + str2.ToString() + "= '" + recordID.ToString() + "' ";
                DataTable table2 = FlowTemplateAction.QueryOffsetNodeFirst(frontNode, templateId);
                orderNumber++;
                foreach (DataRow row in table2.Rows)
                {
                    DataTable table3 = FlowTemplateAction.QueryNodeConditionInfo(Convert.ToInt32(row["NodeID"].ToString()));
                    string str5 = "";
                    string sqlString = str3;
                    if (table3.Rows.Count >= 1)
                    {
                        foreach (DataRow row2 in table3.Rows)
                        {
                            if (row2["AndOr"].ToString() == "1")
                            {
                                str4 = str4 + " and ";
                            }
                            else if (row2["AndOr"].ToString() == "0")
                            {
                                str4 = str4 + " or ";
                            }
                            string str7 = row2["ConditionType"].ToString();
                            if (str7 != null)
                            {
                                if (!(str7 == "1"))
                                {
                                    if (str7 == "2")
                                    {
                                        goto Label_0219;
                                    }
                                    if (str7 == "3")
                                    {
                                        goto Label_0222;
                                    }
                                    if (str7 == "4")
                                    {
                                        goto Label_022B;
                                    }
                                    if (str7 == "5")
                                    {
                                        goto Label_0234;
                                    }
                                }
                                else
                                {
                                    str5 = "<=";
                                }
                            }
                            goto Label_023B;
                        Label_0219:
                            str5 = "<";
                            goto Label_023B;
                        Label_0222:
                            str5 = "=";
                            goto Label_023B;
                        Label_022B:
                            str5 = ">";
                            goto Label_023B;
                        Label_0234:
                            str5 = ">=";
                        Label_023B:
                            if (row2["FieldType"].ToString() == "datetime")
                            {
                                string str8 = str4;
                                str4 = str8 + "datediff(day," + row2["ConditionField"].ToString() + ",getdate())" + str5 + row2["ConditionValue"].ToString();
                            }
                            else
                            {
                                str4 = str4 + row2["ConditionField"].ToString() + str5 + row2["ConditionValue"].ToString();
                            }
                        }
                    }
                    if (str4.Length > 0)
                    {
                        sqlString = sqlString + " and (" + str4 + ")";
                    }
                    if (Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString)) > 0)
                    {
                        strArray[0] = row["NodeID"].ToString();
                        strArray[1] = orderNumber.ToString();
                        strArray[2] = row["IsSelReceiver"].ToString();
                        return strArray;
                    }
                    str4 = "";
                }
            }
            return strArray;
        }

        public static string SelectOrganiger(string where)
        {
            string str = "";
            string sqlString = "select instancecode,(select v_xm from pt_yhmc where v_yhdm = Organiger)as OrganigerName from wf_instance_main where 1=1";
            if (where != "")
            {
                sqlString = sqlString + where;
            }
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if (table.Rows.Count <= 0)
            {
                return str;
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i < (table.Rows.Count - 1))
                {
                    str = str + table.Rows[i]["instancecode"].ToString() + ",";
                }
                else
                {
                    str = str + table.Rows[i]["instancecode"].ToString();
                }
            }
            return table.Rows[0]["instancecode"].ToString();
        }

        public static bool SendMsg(Guid instansCode, int nodeId, decimal theOrder)
        {
            DataTable table = QueryInstanceOperator(instansCode, nodeId, theOrder);
            if (table.Rows.Count > 0)
            {
                string @operator = table.Rows[0]["Operator"].ToString();
                string pStr = table.Rows[0]["IsAllPass"].ToString();
                Hashtable[] hashtableArray = new Hashtable[GetCount(@operator)];
                int length = 0;
                int index = 0;
                string msgRecievers = "";
                length = @operator.IndexOf(",");
                if (length > 0)
                {
                    while (length > 0)
                    {
                        hashtableArray[index] = new Hashtable();
                        msgRecievers = GetMsgRecievers(@operator.Substring(0, length), nodeId);
                        hashtableArray[index].Add("InstanceMainID", table.Rows[0]["Id"].ToString());
                        hashtableArray[index].Add("NodeID", nodeId.ToString());
                        hashtableArray[index].Add("TheOrder", theOrder.ToString());
                        hashtableArray[index].Add("IsAllPass", SqlStringConstructor.GetQuotedString(pStr));
                        hashtableArray[index].Add("MsgRecievers", SqlStringConstructor.GetQuotedString(msgRecievers));
                        if (!AddMessage(hashtableArray[index]))
                        {
                            return false;
                        }
                        @operator = @operator.Substring(length + 1, (@operator.Length - length) - 1);
                        length = @operator.IndexOf(",");
                        index++;
                    }
                    hashtableArray[index] = new Hashtable();
                    msgRecievers = GetMsgRecievers(@operator, nodeId);
                    hashtableArray[index].Add("InstanceMainID", table.Rows[0]["Id"].ToString());
                    hashtableArray[index].Add("NodeID", nodeId.ToString());
                    hashtableArray[index].Add("TheOrder", theOrder.ToString());
                    hashtableArray[index].Add("IsAllPass", SqlStringConstructor.GetQuotedString(pStr));
                    hashtableArray[index].Add("MsgRecievers", SqlStringConstructor.GetQuotedString(msgRecievers));
                    if (!AddMessage(hashtableArray[index]))
                    {
                        return false;
                    }
                }
                else if (@operator.Length == 8)
                {
                    hashtableArray[0] = new Hashtable();
                    msgRecievers = GetMsgRecievers(@operator, nodeId);
                    hashtableArray[0].Add("InstanceMainID", table.Rows[0]["Id"].ToString());
                    hashtableArray[0].Add("NodeID", nodeId.ToString());
                    hashtableArray[0].Add("TheOrder", theOrder.ToString());
                    hashtableArray[0].Add("IsAllPass", SqlStringConstructor.GetQuotedString(pStr));
                    hashtableArray[0].Add("MsgRecievers", SqlStringConstructor.GetQuotedString(msgRecievers));
                    if (!AddMessage(hashtableArray[0]))
                    {
                        return false;
                    }
                }
                else
                {
                    DataTable table2 = FlowTemplateAction.QueryRoleUsers(Convert.ToInt32(@operator));
                    if (table2.Rows.Count > 0)
                    {
                        for (int i = 0; i < table2.Rows.Count; i++)
                        {
                            hashtableArray[i] = new Hashtable();
                            msgRecievers = GetMsgRecievers(@operator, nodeId);
                            hashtableArray[i].Add("InstanceMainID", table.Rows[0]["Id"].ToString());
                            hashtableArray[i].Add("NodeID", nodeId.ToString());
                            hashtableArray[i].Add("TheOrder", theOrder.ToString());
                            hashtableArray[i].Add("IsAllPass", SqlStringConstructor.GetQuotedString(pStr));
                            hashtableArray[i].Add("MsgRecievers", SqlStringConstructor.GetQuotedString(msgRecievers));
                            if (!AddMessage(hashtableArray[i]))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public static string StartTime(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            string str = "select * from wf_instance_main ";
            object obj2 = str;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, "where BusinessCode='", BusinessCode, "' and BusinessClass = '", BusinessClass, "'and InstanceCode = '", instanceCode, "'" }));
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["StartTime"].ToString();
            }
            return "";
        }

        public static bool StartWorkFlow(string businessCode, string businessClass, int templateId, Guid recordId, string organiger, int nodeID, int xh)
        {
            string pStr = DateTime.Now.ToString();
            Hashtable instanceMainInfo = new Hashtable();
            instanceMainInfo.Add("BusinessCode", SqlStringConstructor.GetQuotedString(businessCode));
            instanceMainInfo.Add("BusinessClass", SqlStringConstructor.GetQuotedString(businessClass));
            instanceMainInfo.Add("TemplateID", templateId.ToString());
            instanceMainInfo.Add("InstanceCode", SqlStringConstructor.GetQuotedString(recordId.ToString()));
            instanceMainInfo.Add("Organiger", SqlStringConstructor.GetQuotedString(organiger));
            instanceMainInfo.Add("StartTime", SqlStringConstructor.GetQuotedString(pStr));
            if (!InsertInstance(Convert.ToInt32(FlowTemplateAction.AddInstanceMain(instanceMainInfo)), xh, templateId, nodeID))
            {
                return false;
            }
            DataTable table = FlowTemplateAction.QueryBusiness(businessClass, templateId);
            if (table.Rows.Count == 1)
            {
                string str2 = table.Rows[0]["LinkTable"].ToString();
                string str3 = table.Rows[0]["PrimaryField"].ToString();
                string str4 = table.Rows[0]["StateField"].ToString();
                if (publicDbOpClass.ExecuteSQL(" update " + str2 + " set " + str4 + " '0' where " + str3 + " = '" + recordId.ToString() + "'") == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static string strTempLateName(int TemplateID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from WF_Template  where TemplateID = " + TemplateID);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["TemplateName"].ToString();
            }
            return "";
        }

        public static string strTempLateRemark(int TemplateID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from WF_Template  where TemplateID = " + TemplateID);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["Remark"].ToString();
            }
            return "";
        }

        public static DataTable SuperordinateDutyYH(string yhdm)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(((str + " select * from pt_yhmc where I_DUTYID =(") + " select SuperordinateDuty from pt_yhmc where v_yhdm = '" + yhdm + "') ") + " and c_sfyx = 'y'");
        }

        public static string TemplateName(Guid instanceCode, string BusinessCode, string BusinessClass)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from WF_Template  where TemplateID in (select TemplateID from wf_instance_main where BusinessCode='", BusinessCode, "' and BusinessClass = '", BusinessClass, "'and InstanceCode = '", instanceCode, "') " }));
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["TemplateName"].ToString();
            }
            return "";
        }

        public static DataTable TempNodesList(string yhdm, Guid instanceCode)
        {
            object obj2 = (("select * from WF_TemplateNode where " + " NodeID in ( " + " select NodeID from wf_instance  ") + "  where Operator = '" + yhdm + "' and id in (") + " select id from wf_instance_main ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where InstanceCode = '", instanceCode, "'" }) + ") " + ")");
        }

        public static bool UpdateNode(string operater, int templateId)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj2, " UPDATE WF_TemplateNode SET operater='", operater, "'  WHERE  IsSelReceiver = 1 AND FrontNode = 0 AND TemplateID= ", templateId }));
        }

        public static bool UpdSecondUser(string mainUser, string secondUser, DateTime dtBegin, DateTime dtEnd)
        {
            return publicDbOpClass.NonQuerySqlString(" update wf_seconduser set SecondUser='" + secondUser + "',BeginDate='" + dtBegin.ToShortDateString() + "',EndDate='" + dtEnd.ToShortDateString() + "' where mainuser='" + mainUser + "'");
        }
    }
}

