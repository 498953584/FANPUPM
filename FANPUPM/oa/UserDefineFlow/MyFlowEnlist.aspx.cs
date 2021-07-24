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
public partial class oa_UserDefineFlow_MyFlowEnlist : NBasePage, IRequiresSessionState
{

	public OAWFApplyItemAction hrAction
	{
		get
		{
			return new OAWFApplyItemAction();
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.btnViewWF.Attributes["onclick"] = "OpenViewWF();return false;";
			this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF();return false;";
			string text = base.Request["para"];
			if (this.Session["yhdm"] != null)
			{
				this.BindClass(this.Session["yhdm"].ToString());
			}
			string text2 = "";
			if (text != null)
			{
				text2 += "  and sing =1 ";
			}
			text2 += this.Getwhere();
			this.BindView(text2);
		}
	}
	protected void BindView(string where)
	{
		int num = (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1;
		int num2 = this.AspNetPager1.CurrentPageIndex * this.AspNetPager1.PageSize;
		this.AspNetPager1.RecordCount = this.GetWFEnlistCount(where);
		string sqlString = string.Format("DECLARE @sql NVARCHAR(MAX)\r\n                                      SET @sql=''; \r\n                                      SELECT @sql=@sql+'SELECT '''+  BusinessCode  +''' AS BusinessCode,\r\n                                      '+PrimaryField+' AS InstanceId,'+ StateField+\r\n                                      ' AS FlowState FROM '+LinkTable +' UNION ALL ' \r\n                                      FROM WF_BusinessCode AS wfCode\r\n                                      JOIN sys.tables AS tables ON tables.name=wfCode.LinkTable ;\r\n                                      SET @sql=LEFT(@sql,LEN(@sql)-10);\r\n                                      DECLARE @execSql NVARCHAR(MAX)\r\n                                      SET @execSql=N'\r\n                                      WITH AA AS(\r\n                      SELECT ROW_NUMBER() OVER(ORDER BY AuditDate DESC ) AS Num,* FROM \r\n\t                  (\r\n\t\t                  SELECT distinct T.TemplateName,BC.BusinessClassName,I.NodeName,IM.Organiger,Y.v_xm AS organigerName ,I.Sing,\r\n                         (case when sing = -1 then ''未到达'' when sing=0 then ''到达未审核'' \r\n                         when sing=''1'' then ''已审核'' when sing=''2'' then ''未处理已通过'' else ''已超时'' end) as Status,\r\n                         I.AuditDate,B.DoWithUrl,IM.InstanceCode,IM.BusinessCode,IM.BusinessClass,I.ID,I.NoteID,\r\n                         (SELECT yh.v_xm +'','' FROM WF_Instance \r\n                         JOIN pt_yhmc AS yh ON Operator=yh.v_yhdm\r\n                         WHERE ID=IM.ID\t\r\n                         ORDER BY TheOrder ASC\r\n                         FOR XML PATH(''''))AS OperatorPerson --参与人\r\n                         FROM WF_Instance AS I\r\n                         LEFT JOIN ( SELECT * FROM WF_Instance_Main WHERE ID IN\r\n                         ( SELECT MAX(ID) FROM WF_Instance_Main GROUP BY InstanceCode)) AS IM ON I.ID=IM.ID\r\n                         LEFT JOIN WF_Template AS T ON IM.TemplateID=T.TemplateID\r\n                         LEFT JOIN WF_Business_Class AS BC ON IM.BusinessCode=BC.BusinessCode AND IM.BusinessClass=BC.BusinessClass\r\n                         LEFT JOIN WF_BusinessCode AS B ON IM.BusinessCode=B.BusinessCode\r\n                         JOIN ('+@sql+') AS T1 ON T1.InstanceId=IM.InstanceCode \r\n                         LEFT JOIN PT_yhmc AS Y ON IM.Organiger=Y.v_yhdm  WHERE I.Operator=''{0}'' AND Sing <> -1 {1}\r\n                         -- AND  IM.BusinessCode=''002''  AND T.TemplateName LIKE ''%公告%''\r\n                         -- AND Sing=''1'' AND datediff(dd,AuditDate,''2013-01-06'')=0 \r\n                         --AND I.ID in (select [ID] from wf_instance where charindex(Operator,''00000000'')>0)\r\n                    ) tab\r\n                         ) SELECT * FROM AA WHERE Num BETWEEN {2} AND {3} ';\r\n                        EXEC (@execSql)", new object[]
		{
			this.Session["yhdm"].ToString(),
			where,
			num,
			num2
		});
		DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.GVEnlist.DataSource = dataSource;
		this.GVEnlist.DataBind();
	}
	protected void BindClass(string usercode)
	{
		this.DDLBusinessClass.Items.Clear();
		this.DDLBusinessClass.Items.Add(new ListItem("所有类别", ""));
		this.DDLBusinessClass.Items.Add(new ListItem("自定义流程", "999"));
		string spName = "\r\n\t\t\t--DECLARE @userCode varchar(8)\r\n\t\t\t--SET @userCode = '00000000'\r\n\t\t\tSELECT bcls.*, bcode.C_MKDM, mk.I_XH\r\n\t\t\tFROM WF_Business_Class bcls\r\n\t\t\tLEFT JOIN WF_BusinessCode bcode ON bcls.BusinessCode = bcode.BusinessCode\r\n\t\t\tJOIN PT_MK mk ON bcode.C_MKDM = mk.C_MKDM\r\n\t\t\tWHERE bcls.BusinessCode IN (\r\n\t\t\t\tSELECT BusinessCode FROM [WF_Instance_Main] WHERE [ID] in(\r\n\t\t\t\t\tSELECT DISTINCT [ID] FROM [WF_Instance] WHERE Operator = @userCode\r\n\t\t\t\t)\r\n\t\t\t) AND bcls.BusinessCode!='999'\r\n\t\t\tORDER BY mk.I_XH, bcls.BusinessCode\r\n\t\t";
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
	public string Getwhere()
	{
		string text = "";
		if (!string.IsNullOrEmpty(this.DDLBusinessClass.SelectedValue))
		{
			text = text + " AND IM.BusinessCode= ''" + this.DDLBusinessClass.SelectedValue + "'' ";
		}
		if (!string.IsNullOrEmpty(this.txtTemplateName.Text.Trim()))
		{
			text = text + " AND T.TemplateName LIKE ''%'+'" + this.GetSafeString(this.txtTemplateName.Text.Trim()) + "'+'%''";
		}
		if (!string.IsNullOrEmpty(this.DBDate.Text))
		{
			text = text + " and datediff(dd,AuditDate,''" + this.DBDate.Text + "'')=0 ";
		}
		if (!string.IsNullOrEmpty(this.hidorganizer.Value))
		{
			text = text + " and Organiger like ''%" + this.hidorganizer.Value + "%''";
		}
		if (!string.IsNullOrEmpty(this.hidjoiner.Value))
		{
			text = text + " and I.ID in (select [ID] from wf_instance where charindex(Operator,''" + this.hidjoiner.Value + "'')>0)";
		}
		if (!string.IsNullOrEmpty(this.DDLSing.SelectedValue))
		{
			text = text + " AND Sing  = " + this.DDLSing.SelectedValue;
		}
		return text;
	}
	protected void GVEnlist_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			string text = dataRowView["BusinessCode"].ToString();
			string text2 = dataRowView["BusinessClass"].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["InstanceCode"].ToString(),
				"','",
				text,
				"','",
				text2,
				"');"
			});
			HyperLink hyperLink = (HyperLink)e.Row.Cells[8].Controls[1];
			hyperLink.Text = FlowAuditAction.AuditConter((System.Guid)dataRowView["InstanceCode"], text, text2);
			if (hyperLink.Text == "")
			{
				hyperLink.Text = "点击查看";
			}
			if (hyperLink.Text.Length > 15)
			{
				hyperLink.ToolTip = hyperLink.Text;
				hyperLink.Text = hyperLink.Text.Substring(0, 15) + "...";
			}
			string text3 = dataRowView["DoWithUrl"].ToString();
			if (text3.IndexOf('?') != -1)
			{
				text3 = text3 + "&showAudit=1&showname=1&ic=" + dataRowView["InstanceCode"].ToString();
			}
			else
			{
				text3 = text3 + "?showAudit=1&showname=1&ic=" + dataRowView["InstanceCode"].ToString();
			}
			hyperLink.Attributes["onclick"] = "toolbox_oncommand('" + text3 + "','审核内容查看')";
			hyperLink.NavigateUrl = "#";
			hyperLink.Target = "_self";
		}
	}
	protected void DDLSing_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindView(this.Getwhere());
	}
	protected void btnMoreSearch_Click(object sender, System.EventArgs e)
	{
		this.BindView(this.Getwhere());
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindView(this.Getwhere());
	}
	protected void GVEnlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVEnlist.PageIndex = e.NewPageIndex;
		this.BindView(this.Getwhere());
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindView(this.Getwhere());
	}
	protected int GetWFEnlistCount(string where)
	{
		string sqlString = string.Format("DECLARE @sql NVARCHAR(MAX)\r\n                                      SET @sql=''; \r\n                                      SELECT @sql=@sql+'SELECT '''+  BusinessCode  +''' AS BusinessCode,\r\n                                      '+PrimaryField+' AS InstanceId,'+ StateField+\r\n                                      ' AS FlowState FROM '+LinkTable +' UNION ALL ' \r\n                                      FROM WF_BusinessCode AS wfCode\r\n                                      JOIN sys.tables AS tables ON tables.name=wfCode.LinkTable ;\r\n                                      SET @sql=LEFT(@sql,LEN(@sql)-10);\r\n                                      DECLARE @execSql NVARCHAR(MAX)\r\n                                      SET @execSql=N'\r\n                                      WITH AA AS(\r\n                            SELECT ROW_NUMBER() OVER(ORDER BY AuditDate DESC ) AS Num,* FROM \r\n\t                        (\r\n\t\t                         SELECT distinct \r\n                                 T.TemplateName,BC.BusinessClassName,I.NodeName,IM.Organiger,Y.v_xm AS organigerName ,I.Sing,\r\n                                 (case when sing = -1 then ''未到达'' when sing=0 then ''到达未审核'' \r\n                                 when sing=''1'' then ''已审核'' when sing=''2'' then ''未处理已通过'' else ''已超时'' end) as Status,\r\n                                 I.AuditDate,B.DoWithUrl,IM.InstanceCode,IM.BusinessCode,IM.BusinessClass,I.ID,I.NoteID,\r\n                                 (SELECT yh.v_xm +'','' FROM WF_Instance \r\n                                 JOIN pt_yhmc AS yh ON Operator=yh.v_yhdm\r\n                                 WHERE ID=IM.ID\t\r\n                                 ORDER BY TheOrder ASC\r\n                                 FOR XML PATH(''''))AS OperatorPerson --参与人\r\n                                 FROM WF_Instance AS I\r\n                                 LEFT JOIN ( SELECT * FROM WF_Instance_Main WHERE ID IN\r\n                                 ( SELECT MAX(ID) FROM WF_Instance_Main GROUP BY InstanceCode)) AS IM ON I.ID=IM.ID\r\n                                 LEFT JOIN WF_Template AS T ON IM.TemplateID=T.TemplateID\r\n                                 LEFT JOIN WF_Business_Class AS BC ON IM.BusinessCode=BC.BusinessCode AND IM.BusinessClass=BC.BusinessClass\r\n                                 LEFT JOIN WF_BusinessCode AS B ON IM.BusinessCode=B.BusinessCode\r\n                                 JOIN ('+@sql+') AS T1 ON T1.InstanceId=IM.InstanceCode \r\n                                 LEFT JOIN PT_yhmc AS Y ON IM.Organiger=Y.v_yhdm  WHERE I.Operator=''{0}'' AND Sing <> -1 {1}\r\n                                 -- AND  IM.BusinessCode=''002''  AND T.TemplateName LIKE ''%公告%''\r\n                                 -- AND Sing=''1'' AND datediff(dd,AuditDate,''2013-01-06'')=0 \r\n                                 --AND I.ID in (select [ID] from wf_instance where charindex(Operator,''00000000'')>0)\r\n                            ) tab\r\n                         ) SELECT COUNT(*) FROM AA ';\r\n                        EXEC (@execSql)", this.Session["yhdm"].ToString(), where);
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		int result = 0;
		if (dataTable.Rows.Count > 0)
		{
			result = System.Convert.ToInt32(dataTable.Rows[0][0]);
		}
		return result;
	}
	private string GetSafeString(string str)
	{
		str = str.Replace("'", " ");
		str = str.Replace("%", "[%]");
		return str;
	}
}


