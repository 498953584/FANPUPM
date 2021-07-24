using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Calendar_CalendarList : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			e.Row.Cells[3].Text = ((e.Row.Cells[3].Text == "1") ? "是" : "否");
			if (e.Row.Cells[2].Text.Length > 50)
			{
				e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 50) + "...";
			}
		}
	}
	protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}
}


