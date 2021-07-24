using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_PigeonholeFileList : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.btnPigeonhole.Attributes["onclick"] = "return openEdit();";
		}
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			string str = ((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + str + "');";
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[2].Text = userManageDb.GetUserName(e.Row.Cells[2].Text);
		}
	}
	protected void btnPigeonhole_Click(object sender, EventArgs e)
	{
		this.GridView1.DataBind();
	}
}


