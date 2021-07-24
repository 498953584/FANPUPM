using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SysFrame_PTAuditList : NBasePage, IRequiresSessionState
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
			this.BindView("");
		}
	}
	protected void BindView(string where)
	{
		string text = "SELECT * FROM ( SELECT  a.BusinessCode, DATEDIFF(hh, b.OutOfTime, GETDATE()) AS cs,a.BusinessClass,CONVERT (varchar(10), a.StartTime, 20) as rq, (SELECT BusinessClassName FROM WF_Business_Class AS d WHERE (BusinessCode = a.BusinessCode) AND (BusinessClass = a.BusinessClass)) AS BusinessClassName, b.NoteID, b.IsAllPass, a.TemplateID, (SELECT TemplateName FROM WF_Template AS c WHERE (TemplateID = a.TemplateID)) AS TemplateName, b.NodeID, b.NodeName, (SELECT v_xm FROM PT_yhmc WHERE (v_yhdm = a.Organiger)) AS organigerName, a.StartTime, a.InstanceCode, dbo.GetBusinessName(a.BusinessCode) AS BusinessName ,b.ArriveTime ,b.During FROM WF_Instance_Main AS a INNER JOIN WF_Instance AS b ON a.ID = b.ID WHERE ((b.Sing = 0) AND (b.Operator = '" + this.Session["yhdm"] + "')) ) AS AA ";
		if (text != "")
		{
			text += where;
		}
		text += " order by StartTime desc";
		DataTable dataTable = publicDbOpClass.DataTableQuary(text);
		DataTable dataTable2 = new DataTable();
		dataTable2.Columns.Add("BusinessCode");
		dataTable2.Columns.Add("cs");
		dataTable2.Columns.Add("BusinessClass");
		dataTable2.Columns.Add("rq");
		dataTable2.Columns.Add("BusinessClassName");
		dataTable2.Columns.Add("NoteID");
		dataTable2.Columns.Add("IsAllPass");
		dataTable2.Columns.Add("TemplateID");
		dataTable2.Columns.Add("TemplateName");
		dataTable2.Columns.Add("NodeID");
		dataTable2.Columns.Add("NodeName");
		dataTable2.Columns.Add("organigerName");
		dataTable2.Columns.Add("StartTime");
		dataTable2.Columns.Add("InstanceCode");
		dataTable2.Columns.Add("BusinessName");
		dataTable2.Columns.Add("ArriveTime");
		dataTable2.Columns.Add("During");
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			DataTable dataTable3 = publicDbOpClass.DataTableQuary(" select * from WF_BusinessCode where BusinessCode='" + dataTable.Rows[i][0].ToString() + "' ");
			if (dataTable3.Rows.Count > 0)
			{
				DataTable dataTable4 = publicDbOpClass.DataTableQuary(string.Concat(new string[]
				{
					"SELECT * FROM ",
					dataTable3.Rows[0]["LinkTable"].ToString(),
					" WHERE ",
					dataTable3.Rows[0]["KeyWord"].ToString(),
					"='",
					dataTable.Rows[i]["InstanceCode"].ToString(),
					"'"
				}));
				if (dataTable4.Rows.Count > 0)
				{
					DataRow dataRow = dataTable2.NewRow();
					dataRow["BusinessCode"] = dataTable.Rows[i]["BusinessCode"];
					dataRow["cs"] = dataTable.Rows[i]["cs"];
					dataRow["BusinessClass"] = dataTable.Rows[i]["BusinessClass"];
					dataRow["rq"] = dataTable.Rows[i]["rq"];
					dataRow["BusinessClassName"] = dataTable.Rows[i]["BusinessClassName"];
					dataRow["NoteID"] = dataTable.Rows[i]["NoteID"];
					dataRow["IsAllPass"] = dataTable.Rows[i]["IsAllPass"];
					dataRow["TemplateID"] = dataTable.Rows[i]["TemplateID"];
					dataRow["TemplateName"] = dataTable.Rows[i]["TemplateName"];
					dataRow["NodeID"] = dataTable.Rows[i]["NodeID"];
					dataRow["NodeName"] = dataTable.Rows[i]["NodeName"];
					dataRow["organigerName"] = dataTable.Rows[i]["organigerName"];
					dataRow["StartTime"] = dataTable.Rows[i]["StartTime"];
					dataRow["InstanceCode"] = dataTable.Rows[i]["InstanceCode"];
					dataRow["BusinessName"] = dataTable.Rows[i]["BusinessName"];
					dataRow["ArriveTime"] = dataTable.Rows[i]["ArriveTime"];
					dataRow["During"] = dataTable.Rows[i]["During"];
					dataTable2.Rows.Add(dataRow);
				}
			}
		}
		this.gvAuditingList.DataSource = dataTable2;
		this.gvAuditingList.DataBind();
	}
	protected string GetWhere()
	{
		string text = "WHERE 1=1";
		if (!string.IsNullOrEmpty(this.txtclassname.Text.Trim()))
		{
			text = text + " and BusinessClassName like '%" + this.GetSafeString(this.txtclassname.Text.Trim()) + "%'";
		}
		if (!string.IsNullOrEmpty(this.txtorganizer.Text.Trim()))
		{
			text = text + " AND organigerName like  '%" + this.GetSafeString(this.txtorganizer.Text.Trim()) + "%' ";
		}
		return text;
	}
	protected void gvAuditingList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Cells[0].Text = System.Convert.ToString(e.Row.RowIndex + 1 + this.gvAuditingList.PageIndex * this.gvAuditingList.PageSize);
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			string text = dataRowView["InstanceCode"].ToString();
			string text2 = dataRowView["NoteID"].ToString();
			string text3 = dataRowView["IsAllPass"].ToString();
			string text4 = dataRowView["NodeID"].ToString();
			string text5 = dataRowView["BusinessCode"].ToString();
			string text6 = dataRowView["BusinessClass"].ToString();
			System.Convert.ToInt32(dataRowView["During"]);
			e.Row.Cells[2].Text = FlowAuditAction.OrganigerName(new System.Guid(dataRowView["instanceCode"].ToString()), text5, text6);
			DataTable dataTable = FlowAuditAction.QueryAuditStatus(new System.Guid(dataRowView["instanceCode"].ToString()), dataRowView["BusinessCode"].ToString(), dataRowView["businessclass"].ToString());
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (i < dataTable.Rows.Count - 1)
				{
					TableCell expr_19E = e.Row.Cells[3];
					expr_19E.Text = expr_19E.Text + dataTable.Rows[i]["auditor"].ToString() + ",";
				}
				else
				{
					TableCell expr_1E3 = e.Row.Cells[3];
					expr_1E3.Text += dataTable.Rows[i]["auditor"].ToString();
				}
			}
			decimal d = 0m;
			try
			{
				d = System.Convert.ToDecimal(dataRowView["cs"]);
			}
			catch
			{
			}
			LinkButton linkButton = (LinkButton)e.Row.Cells[1].Controls[0];
			DataTable dataTable2 = publicDbOpClass.DataTableQuary(" select * from WF_BusinessCode where BusinessCode='" + dataRowView["BusinessCode"].ToString() + "' ");
			try
			{
				DataTable dataTable3 = publicDbOpClass.DataTableQuary(string.Concat(new string[]
				{
					" select  ",
					dataTable2.Rows[0]["NameField"].ToString(),
					" from  ",
					dataTable2.Rows[0]["LinkTable"].ToString(),
					"  where ",
					dataTable2.Rows[0]["PrimaryField"].ToString(),
					" ='",
					dataRowView["instanceCode"].ToString(),
					"' "
				}));
				if (dataRowView["BusinessCode"].ToString() == "089")
				{
					if (!string.IsNullOrEmpty(dataTable3.Rows[0][0].ToString()))
					{
						LinkButton expr_389 = linkButton;
						expr_389.Text = expr_389.Text + "—" + TenderInfo.GetProjectName(dataTable3.Rows[0][0].ToString());
					}
				}
				else
				{
					if (dataRowView["BusinessCode"].ToString() == "100" || dataRowView["BusinessCode"].ToString() == "106")
					{
						if (!string.IsNullOrEmpty(dataTable3.Rows[0][0].ToString()))
						{
							LinkButton expr_41C = linkButton;
							expr_41C.Text = expr_41C.Text + "—" + ProjectInfo.GetProjectName(dataTable3.Rows[0][0].ToString());
						}
					}
					else
					{
						if (!string.IsNullOrEmpty(dataTable3.Rows[0][0].ToString()))
						{
							LinkButton expr_471 = linkButton;
							expr_471.Text = expr_471.Text + "—" + dataTable3.Rows[0][0].ToString();
						}
					}
				}
			}
			catch
			{
			}
			linkButton.Attributes["class"] = "firstpage";
			linkButton.Attributes["style"] = " color:blue;";
			if (d > 0m)
			{
				string str = "超时";
				linkButton.Text = "[<font color=\"red\">" + str + "</font>]" + linkButton.Text;
			}
			string str2 = string.Concat(new string[]
			{
				"/EPC/Workflow/AuditFrame.aspx?ic=",
				text,
				"&id=",
				text2,
				"&pass=",
				text3,
				"&nid=",
				text4,
				"&bc=",
				text5,
				"&bcl=",
				text6
			});
			linkButton.Attributes["onclick"] = "toolbox_oncommand('" + str2 + "','审核内容查看')";
		}
	}
	protected void SearchBt_Click(object sender, System.EventArgs e)
	{
		this.BindView(this.GetWhere());
	}
	protected void gvAuditingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvAuditingList.PageIndex = e.NewPageIndex;
		this.BindView(this.GetWhere());
	}
	protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		string script = "javascript:parent.desktop.flowclass = window;toolbox_oncommand('/oa/UserDefineFlow/MyFlowEnlist.aspx?para=yes', '已审核流程');";
		base.ClientScript.RegisterStartupScript(base.GetType(), "", script, true);
		this.DropDownList1.SelectedValue = "1";
	}
	private string GetSafeString(string str)
	{
		str = str.Replace("'", "''");
		str = str.Replace("%", "[%]");
		return str;
	}
}


