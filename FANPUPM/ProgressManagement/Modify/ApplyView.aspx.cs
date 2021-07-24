using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Modify_ApplyView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindPlans();
		}
	}
	protected void BindPlans()
	{
		string verId = base.Request["ic"];
		DataTable modifyInfo = cn.justwin.BLL.ProgressManagement.Version.GetModifyInfo(verId);
		this.gvwPlans.DataSource = modifyInfo;
		this.gvwPlans.DataBind();
	}
	protected void gvwPlans_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		string arg_10_0 = base.Request["modify"];
		cn.justwin.BLL.ProgressManagement.Version byId = cn.justwin.BLL.ProgressManagement.Version.GetById(base.Request["ic"]);
		if (byId != null && e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressVersionId"].ToString();
			if (string.IsNullOrEmpty(byId.ParentVersionId))
			{
				e.Row.Cells[3].Text = string.Empty;
				e.Row.Cells[4].Text = string.Empty;
				e.Row.Cells[5].Text = string.Empty;
			}
		}
	}
}


