using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ProgressManagement_Plan_PlanRatify : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	public OAWFApplyItemAction hrAction
	{
		get
		{
			return new OAWFApplyItemAction();
		}
	}

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindPlans();
		this.AspNetPager1.PageSize = NBasePage.pagesize;
	}
	protected void BindPlans()
	{
		string name = this.txtVerName.Text.Trim();
		string version = this.txtVerCode.Text.Trim();
		string organigerName = this.txtOrgName.Text.Trim();
		bool? sing = null;
		if (this.dropFlow.SelectedValue != "")
		{
			sing = new bool?(!(this.dropFlow.SelectedValue == "0"));
		}
		DataTable dataSource = null;
		if (!string.IsNullOrEmpty(this.prjId))
		{
			string userCode = base.UserCode;
			int wFCount = Progress.GetWFCount(userCode, this.prjId, name, version, sing, organigerName);
			this.AspNetPager1.RecordCount = wFCount;
			dataSource = Progress.GetWF(userCode, this.prjId, name, version, sing, organigerName, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		}
		this.gvwPlans.DataSource = dataSource;
		this.gvwPlans.DataBind();
	}
	protected void gvwPlans_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			string text = dataRowView["InstanceCode"].ToString();
			e.Row.Attributes["id"] = text;
			string text2 = dataRowView["NoteID"].ToString();
			string text3 = dataRowView["NodeID"].ToString();
			string text4 = dataRowView["IsAllPass"].ToString();
			string text5 = dataRowView["BusinessCode"].ToString();
			string text6 = dataRowView["BusinessClass"].ToString();
			string a = dataRowView["Sing"].ToString();
			System.Convert.ToInt32(dataRowView["During"]);
			DataTable dataTable = FlowAuditAction.QueryAuditStatus(new System.Guid(dataRowView["instanceCode"].ToString()), dataRowView["BusinessCode"].ToString(), dataRowView["businessclass"].ToString());
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (i < dataTable.Rows.Count - 1)
				{
					TableCell expr_12B = e.Row.Cells[6];
					expr_12B.Text = expr_12B.Text + dataTable.Rows[i]["auditor"].ToString() + ",";
				}
				else
				{
					TableCell expr_170 = e.Row.Cells[6];
					expr_170.Text += dataTable.Rows[i]["auditor"].ToString();
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
			linkButton.ToolTip = linkButton.Text;
			string str = string.Empty;
			string text7 = string.Empty;
			string text8 = string.Empty;
			if (d > 0m && a == "0")
			{
				str = "超时";
				linkButton.Text = "[<font color=\"red\">" + str + "</font>]" + StringUtility.GetStr(linkButton.Text, 25);
			}
			if (a == "0")
			{
				e.Row.Cells[3].Text = "待审核";
				text7 = string.Concat(new string[]
				{
					"/EPC/Workflow/AuditFrame.aspx?ic=",
					text,
					"&id=",
					text2,
					"&pass=",
					text4,
					"&nid=",
					text3,
					"&bc=",
					text5,
					"&bcl=",
					text6
				});
				text8 = "流程审核";
			}
			else
			{
				e.Row.Cells[3].Text = "<span style='color:#008B45;'>已审核</span>";
				text7 = "/ProgressManagement/Plan/RatifyView.aspx?ic=" + text;
				text8 = "流程审核记录";
			}
			e.Row.Attributes["procedureURL"] = string.Concat(new string[]
			{
				"/epc/Workflow/AuditViewPrint.aspx?ic=",
				text,
				"&bc=",
				text5,
				"&bcl=",
				text6
			});
			e.Row.Attributes["flowStateURL"] = string.Concat(new string[]
			{
				"/epc/Workflow/AuditViewWF.aspx?ic=",
				text,
				"&bc=",
				text5,
				"&bcl=",
				text6
			});
			linkButton.Attributes["class"] = "firstpage link tooltip";
			linkButton.Attributes["onclick"] = string.Concat(new string[]
			{
				"top.ui._flowclass = window; toolbox_oncommand('",
				text7,
				"','",
				text8,
				"')"
			});
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPlans();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindPlans();
	}
}


