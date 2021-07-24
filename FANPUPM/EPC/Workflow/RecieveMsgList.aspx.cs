using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ModuleSet_Workflow_RecieveMsgList : NBasePage, IRequiresSessionState
{
	protected RecieveMsgAction rma
	{
		get
		{
			return new RecieveMsgAction();
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.btnViewWF.Attributes["onclick"] = "OpenViewWF();";
			this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF();";
			if (this.Session["yhdm"] != null)
			{
				this.BindClass(this.Session["yhdm"].ToString());
			}
			this.BindView(this.Getwhere());
		}
	}
	public string Getwhere()
	{
		string text = string.Empty;
		if (!string.IsNullOrEmpty(this.DDLBusinessClass.SelectedValue))
		{
			text = text + " AND WF_Class.BusinessCode = ''" + this.DDLBusinessClass.SelectedValue + "'' ";
		}
		if (!string.IsNullOrEmpty(this.txtTemplateName.Text.Trim()))
		{
			text = text + " AND TemplateName like ''%'+'" + this.GetSafeString(this.txtTemplateName.Text.Trim()) + "'+'%''";
		}
		if (!string.IsNullOrEmpty(this.txtContent.Text.Trim()))
		{
			text = text + " AND LookUrl like ''%'+'" + this.GetSafeString(this.txtContent.Text.Trim()) + "'+'%''";
		}
		if (!string.IsNullOrEmpty(this.txtRecieveDate.Text.Trim()))
		{
			text = text + " AND datediff(dd,RecieveDate,''" + this.txtRecieveDate.Text.Trim() + "'')=0 ";
		}
		if (!string.IsNullOrEmpty(this.hidsend.Value))
		{
			text = text + " AND  charindex(''" + this.hidsend.Value + "'',RecieveUserCode)>0";
		}
		if (this.hidtolder.Value != "")
		{
			text = text + " AND ToldUserCodes LIKE ''%" + this.hidtolder.Value + "%'' ";
		}
		if (!string.IsNullOrEmpty(this.hidOrganizer.Value))
		{
			text = text + " AND charindex(''" + this.hidOrganizer.Value + "'',Organiger)>0 ";
		}
		return text;
	}
	protected void BindClass(string usercode)
	{
		this.DDLBusinessClass.Items.Clear();
		this.DDLBusinessClass.Items.Add(new ListItem("所有类别", ""));
		this.DDLBusinessClass.Items.Add(new ListItem("自定义流程", "999"));
		string spName = "\r\n\t\t\t--DECLARE @userCode varchar(8)\r\n\t\t\t--SET @userCode = '00000000'\r\n\t\t\tSELECT bcls.*, bcode.C_MKDM, mk.I_XH\r\n\t\t\tFROM WF_Business_Class bcls\r\n\t\t\tLEFT JOIN WF_BusinessCode bcode ON bcls.BusinessCode = bcode.BusinessCode\r\n\t\t\tJOIN PT_MK mk ON bcode.C_MKDM = mk.C_MKDM\r\n\t\t\tWHERE bcls.BusinessCode IN (\r\n\t\t\t\tSELECT BusinessCode FROM [WF_Instance_Main] WHERE Organiger = @userCode\r\n\t\t\t) AND bcls.BusinessCode!='999'\r\n\t\t\tORDER BY mk.I_XH, bcls.BusinessCode\r\n\t\t";
		SqlParameter[] commandParameters = new SqlParameter[]
		{
			new SqlParameter("@userCode", usercode)
		};
		DataTable dataTable = publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, commandParameters);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			this.DDLBusinessClass.Items.Add(new ListItem(dataTable.Rows[i]["businessclassname"].ToString(), dataTable.Rows[i]["businesscode"].ToString()));
		}
	}
	protected void DDLBusinessClass_SelectedIndexChanged(object sender, System.EventArgs e)
	{
	}
	protected void BindView(string where)
	{
		int num = (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1;
		int num2 = this.AspNetPager1.PageSize * this.AspNetPager1.CurrentPageIndex;
		string sqlString = string.Format(" DECLARE @sql NVARCHAR(MAX)\r\n                                          SET @sql=''; \r\n                                          SELECT @sql=@sql+'SELECT '''+  BusinessCode  +''' AS BusinessCode,\r\n                                          '+PrimaryField+' AS InstanceId,'+ StateField+\r\n                                          ' AS FlowState FROM '+LinkTable +' UNION ALL ' \r\n                                          FROM WF_BusinessCode AS wfCode\r\n                                          JOIN sys.tables AS tables ON tables.name=wfCode.LinkTable ;\r\n                                          SET @sql=LEFT(@sql,LEN(@sql)-10);\r\n                              DECLARE @execSql NVARCHAR(MAX)\r\n                                          SET @execSql=N'\r\n                            WITH BB AS(\r\n                                --根据审核记录表和审核主表，获取参与的审核人\r\n                                SELECT *,(SELECT yh.v_xm+'','' FROM WF_Instance\r\n                            JOIN pt_yhmc AS yh \r\n\t                            ON Operator=yh.v_yhdm\r\n\t                            WHERE ID=WFMain.ID\r\n\t                            FOR XML PATH(''''))AS OperatNames --参与审核人\r\n\t                            FROM ( SELECT * FROM  WF_Instance_Main \r\n\t                            WHERE ID IN(SELECT MAX(ID)FROM WF_Instance_Main \r\n\t                            GROUP BY InstanceCode))AS WFMain\r\n                            ),WF_Class AS(\r\n                                --获取流程类型名，流程模板名\r\n\t                            SELECT BB.*,WF_Template.TemplateName,\r\n\t                            Class.BusinessClassName,WFCode.BusinessName,WFCode.DoWithUrl FROM BB \r\n                            JOIN \r\n\t                            WF_Template ON BB.TemplateID=WF_Template.TemplateID \r\n                            JOIN \r\n\t                            WF_Business_Class AS Class ON Class.BusinessCode=BB.BusinessCode\r\n\t                            AND Class.BusinessClass=BB.BusinessClass \r\n                            JOIN WF_BusinessCode AS WFCode ON WFCode.BusinessCode=BB.BusinessCode \r\n                            ),CC AS(\r\n                               ---根据被告知人员表获取被告知人\r\n\t                            SELECT Msg.*,(SELECT yh.v_yhdm+'','' FROM WF_RecieveMsg \r\n                            JOIN \r\n\t                            pt_yhmc AS yh ON  WF_RecieveMsg.v_yhdm=yh.v_yhdm\r\n\t                            WHERE InstanceCode= Msg.InstanceCode\r\n\t                            FOR XML PATH(''''))AS ToldUserCodes --被告知人编码\r\n\t                            ,(SELECT yh.v_xm+'','' FROM WF_RecieveMsg \r\n                            JOIN \r\n\t                            pt_yhmc AS yh ON  WF_RecieveMsg.v_yhdm=yh.v_yhdm\r\n\t                            WHERE InstanceCode= Msg.InstanceCode\r\n\t                            FOR XML PATH(''''))AS ToldUserNames --被告知人姓名\r\n\t                            FROM(SELECT * FROM WF_RecieveMsg \r\n\t                            WHERE RecordID IN (SELECT MAX(RecordID) FROM WF_RecieveMsg\r\n\t                            GROUP BY InstanceCode )) AS Msg\r\n                            ),WF_Instan_Msg AS(\r\n\t                            SELECT ROW_NUMBER() OVER(ORDER BY RecieveDate DESC) AS Num, CC.*,\r\n\t                            WF_Class.ID,WF_Class.BusinessCode,WF_Class.BusinessClass,\r\n\t                            WF_Class.TemplateID,WF_Class.Organiger,WF_Class.StartTime,WF_Class.OperatNames,\r\n\t                            WF_Class.TemplateName,WF_Class.BusinessClassName,\r\n\t                            WF_Class.BusinessName,WF_Class.DoWithUrl FROM WF_Class \r\n                            JOIN \r\n\t                            CC ON WF_Class.InstanceCode=CC.InstanceCode\r\n                            JOIN ('+@sql+')AS T1 ON T1.InstanceId=CC.InstanceCode \r\n\t                        WHERE 1=1 AND ToldUserCodes LIKE ''%{0}%'' {1}\r\n\t                        --AND LookUrl LIKE ''%收入合同%''  --告知内容\r\n\t                        --AND TemplateName LIKE ''%管理员%'' --模板名称\r\n\t                        --AND WF_Class.BusinessCode=999  --流程类型\r\n\t                        --AND Organiger = ''00000000''\r\n                            --AND RecieveDate>=''2013-2-26''\r\n                            ) SELECT * FROM WF_Instan_Msg WHERE Num BETWEEN {2} AND {3} ';\r\n                        EXEC(@execSql) ", new object[]
		{
			this.Session["yhdm"].ToString(),
			where,
			num,
			num2
		});
		this.AspNetPager1.RecordCount = this.GetWFInstanMsg(where);
		DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.GVRecieveMsg.DataSource = dataSource;
		this.GVRecieveMsg.DataBind();
	}
	protected void GVRecieveMsg_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"ClickRow('",
				dataRowView["InstanceCode"].ToString(),
				"','",
				dataRowView["BusinessCode"].ToString(),
				"','",
				dataRowView["BusinessClass"].ToString(),
				"')"
			});
			string text = FlowAuditAction.AuditConter((System.Guid)dataRowView["InstanceCode"], dataRowView["BusinessCode"].ToString(), dataRowView["businessclass"].ToString());
			new userManageDb();
			HyperLink hyperLink = (HyperLink)e.Row.Cells[7].FindControl("HLConter");
			if (string.IsNullOrEmpty(text))
			{
				text = "点击查看";
			}
			if (text.Length > 21)
			{
				text = text.Substring(0, 20) + "...";
			}
			hyperLink.Text = text;
			hyperLink.NavigateUrl = "#";
			hyperLink.Target = "_self";
			hyperLink.Attributes["onclick"] = string.Concat(new object[]
			{
				"toolbox_oncommand('",
				dataRowView["DoWithUrl"],
				"?showAudit=1&showname=1&ic=",
				dataRowView["InstanceCode"].ToString(),
				"','审核内容查看')"
			});
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindView(this.Getwhere());
	}
	protected void btnMoreSearch_Click(object sender, System.EventArgs e)
	{
		this.BindView(this.Getwhere());
	}
	protected void GVRecieveMsg_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVRecieveMsg.PageIndex = e.NewPageIndex;
		this.BindView(this.Getwhere());
	}
	protected new string GetUserName(string usercode)
	{
		return PageHelper.QueryUser(this, usercode);
	}
	protected int GetWFInstanMsg(string where)
	{
		int result = 0;
		string sqlString = string.Format("DECLARE @sql NVARCHAR(MAX)\r\n                                          SET @sql=''; \r\n                                          SELECT @sql=@sql+'SELECT '''+  BusinessCode  +''' AS BusinessCode,\r\n                                          '+PrimaryField+' AS InstanceId,'+ StateField+\r\n                                          ' AS FlowState FROM '+LinkTable +' UNION ALL ' \r\n                                          FROM WF_BusinessCode AS wfCode\r\n                                          JOIN sys.tables AS tables ON tables.name=wfCode.LinkTable ;\r\n                                          SET @sql=LEFT(@sql,LEN(@sql)-10);\r\n                              DECLARE @execSql NVARCHAR(MAX)\r\n                                          SET @execSql=N'\r\n                            WITH BB AS(\r\n                                --根据审核记录表和审核主表，获取参与的审核人\r\n                                SELECT *,(SELECT yh.v_xm+'','' FROM WF_Instance\r\n                            JOIN pt_yhmc AS yh \r\n\t                            ON Operator=yh.v_yhdm\r\n\t                            WHERE ID=WFMain.ID\r\n\t                            FOR XML PATH(''''))AS OperatNames --参与审核人\r\n\t                            FROM ( SELECT * FROM  WF_Instance_Main \r\n\t                            WHERE ID IN(SELECT MAX(ID)FROM WF_Instance_Main \r\n\t                            GROUP BY InstanceCode))AS WFMain\r\n                            ),WF_Class AS(\r\n                                --获取流程类型名，流程模板名\r\n\t                            SELECT BB.*,WF_Template.TemplateName,\r\n\t                            Class.BusinessClassName,WFCode.BusinessName,WFCode.DoWithUrl FROM BB \r\n                            JOIN \r\n\t                            WF_Template ON BB.TemplateID=WF_Template.TemplateID \r\n                            JOIN \r\n\t                            WF_Business_Class AS Class ON Class.BusinessCode=BB.BusinessCode\r\n\t                            AND Class.BusinessClass=BB.BusinessClass \r\n                            JOIN WF_BusinessCode AS WFCode ON WFCode.BusinessCode=BB.BusinessCode \r\n                            ),CC AS(\r\n                               ---根据被告知人员表获取被告知人\r\n\t                            SELECT Msg.*,(SELECT yh.v_yhdm+'','' FROM WF_RecieveMsg \r\n                            JOIN \r\n\t                            pt_yhmc AS yh ON  WF_RecieveMsg.v_yhdm=yh.v_yhdm\r\n\t                            WHERE InstanceCode= Msg.InstanceCode\r\n\t                            FOR XML PATH(''''))AS ToldUserCodes --被告知人编码\r\n\t                            ,(SELECT yh.v_xm+'','' FROM WF_RecieveMsg \r\n                            JOIN \r\n\t                            pt_yhmc AS yh ON  WF_RecieveMsg.v_yhdm=yh.v_yhdm\r\n\t                            WHERE InstanceCode= Msg.InstanceCode\r\n\t                            FOR XML PATH(''''))AS ToldUserNames --被告知人姓名\r\n\t                            FROM(SELECT * FROM WF_RecieveMsg \r\n\t                            WHERE RecordID IN (SELECT MAX(RecordID) FROM WF_RecieveMsg\r\n\t                            GROUP BY InstanceCode )) AS Msg\r\n                            ),WF_Instan_Msg AS(\r\n\t                            SELECT ROW_NUMBER() OVER(ORDER BY RecieveDate DESC) AS Num, CC.*,\r\n\t                            WF_Class.ID,WF_Class.BusinessCode,WF_Class.BusinessClass,\r\n\t                            WF_Class.TemplateID,WF_Class.Organiger,WF_Class.StartTime,WF_Class.OperatNames,\r\n\t                            WF_Class.TemplateName,WF_Class.BusinessClassName,\r\n\t                            WF_Class.BusinessName,WF_Class.DoWithUrl FROM WF_Class \r\n                            JOIN \r\n\t                            CC ON WF_Class.InstanceCode=CC.InstanceCode\r\n                            JOIN ('+@sql+')AS T1 ON T1.InstanceId=CC.InstanceCode \r\n\t                        WHERE 1=1 AND ToldUserCodes LIKE ''%{0}%'' {1}\r\n\t                        --AND LookUrl LIKE ''%收入合同%''  --告知内容\r\n\t                        --AND TemplateName LIKE ''%管理员%'' --模板名称\r\n\t                        --AND WF_Class.BusinessCode=999  --流程类型\r\n\t                        --AND Organiger = ''00000000''\r\n                            --AND RecieveDate>=''2013-2-26''\r\n                            ) SELECT COUNT(*) FROM WF_Instan_Msg  ';\r\n                        EXEC(@execSql)", this.Session["yhdm"].ToString(), where);
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			result = System.Convert.ToInt32(dataTable.Rows[0][0]);
		}
		return result;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindView(this.Getwhere());
	}
	private string GetSafeString(string str)
	{
		str = str.Replace("'", " ");
		str = str.Replace("%", "[%]");
		return str;
	}
}


