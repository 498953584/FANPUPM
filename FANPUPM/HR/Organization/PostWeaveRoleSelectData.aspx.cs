using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_PostWeaveRoleSelectData : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RoleTypeCode"].ToString(),
				"','",
				dataRowView["RoleTypeName"].ToString(),
				"');"
			});
		}
	}
}


