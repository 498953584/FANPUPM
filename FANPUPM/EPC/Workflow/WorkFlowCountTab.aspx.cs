using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_WorkFlowCountTab : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
	}
	protected void GVWorkTemp_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[2].Text = userManageDb.GetUserName(dataRowView["Operater"].ToString());
			if (e.Row.Cells[2].Text.ToString() == "")
			{
				e.Row.Cells[2].Text = "在流程中确定";
			}
		}
	}
}


