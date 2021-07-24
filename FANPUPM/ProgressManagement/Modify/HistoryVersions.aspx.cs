using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ProgressManagement_Modify_HistoryVersions : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindPlans();
			this.AspNetPager1.PageSize = NBasePage.pagesize;
		}
	}
	protected void BindPlans()
	{
		string progressId = base.Request["progressId"];
		int historyVersionsCount = Progress.GetHistoryVersionsCount(progressId);
		this.AspNetPager1.RecordCount = historyVersionsCount;
		DataTable historyVersions = Progress.GetHistoryVersions(progressId, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwPlans.DataSource = historyVersions;
		this.gvwPlans.DataBind();
	}
	protected void gvwPlans_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressVersionId"].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPlans();
	}
}


