using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_ApplicationAudit : BasePage, IRequiresSessionState
{
	private ApplicationAction aa
	{
		get
		{
			return new ApplicationAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.BtnView.Attributes["onclick"] = "OpenLock();";
			this.btnOk.Attributes["onclick"] = "OpenAudit()";
			if (base.Request["ic"] != null && base.Request["ic"].ToString() != "")
			{
				this.btnOk.Style.Add("display", "none");
				this.SqlApplication.SelectCommand = "SELECT [RecordID], [AuditState], [UserCode], [RecordDate], [LeaveType], [LeaveDays], [BackDate], \r\n                                                    [IsApply], [Reason], [Days], [BeginDate], [IsConfirm], [ConfirmUser],[remark],[auditPerson]  \r\n                                                    FROM [HR_Leave_Application] WHERE RecordID='" + base.Request["ic"].ToString() + "' ORDER BY [AuditState],[RecordDate] ";
				this.GVApplication.DataBind();
			}
		}
	}
	protected void GVApplication_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[1].Text = userManageDb.GetUserName(e.Row.Cells[1].Text);
			e.Row.Attributes["ondblclick"] = "OpenLock();";
			e.Row.ToolTip = "双击查看详细信息";
			string text;
			switch (text = e.Row.Cells[2].Text)
			{
			case "1":
				e.Row.Cells[2].Text = "事假";
				break;
			case "2":
				e.Row.Cells[2].Text = "婚假";
				break;
			case "3":
				e.Row.Cells[2].Text = "年休假";
				break;
			case "4":
				e.Row.Cells[2].Text = "工伤";
				break;
			case "5":
				e.Row.Cells[2].Text = "病假";
				break;
			case "6":
				e.Row.Cells[2].Text = "产假";
				break;
			case "7":
				e.Row.Cells[2].Text = "丧假";
				break;
			}
			int num2 = AduitAction.SetOkState(dataRowView["AuditState"].ToString()).LastIndexOf('#');
			e.Row.Cells[8].Text = AduitAction.SetOkState(dataRowView["AuditState"].ToString()).Substring(0, num2);
			string value = AduitAction.SetOkState(dataRowView["AuditState"].ToString()).Substring(num2);
			e.Row.Cells[8].Style.Add("color", value);
		}
	}
	protected void btnOk_Click(object sender, EventArgs e)
	{
		this.GVApplication.DataBind();
	}
}


