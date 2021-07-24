using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_WFOvertimeCountDetails : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		bool arg_16_0 = base.IsPostBack;
	}
	protected void GVInstance_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[1].Text = userManageDb.GetUserName(dataRowView["Operator"].ToString());
			TableCell expr_99 = e.Row.Cells[3];
			expr_99.Text += "小时";
			if (dataRowView["Sing"].ToString() == "1" && dataRowView["ArriveTime"] != System.DBNull.Value)
			{
				e.Row.Cells[6].Text = this.OvertimeCalculation(System.Convert.ToDateTime(dataRowView["ArriveTime"]), System.Convert.ToDateTime(dataRowView["AuditDate"]), System.Convert.ToInt32(dataRowView["During"]));
			}
		}
	}
	protected string OvertimeCalculation(System.DateTime ArriveTimes, System.DateTime AuditDate, int During)
	{
		System.TimeSpan timeSpan = AuditDate.Subtract(ArriveTimes);
		int num = timeSpan.Days * 24 + timeSpan.Hours - During;
		string result;
		if (num > 0)
		{
			result = string.Concat(new object[]
			{
				"<font color=\"red\">超时 ",
				num.ToString(),
				" 小时",
				timeSpan.Minutes,
				"分</font>"
			});
		}
		else
		{
			result = "未超时";
		}
		return result;
	}
}


