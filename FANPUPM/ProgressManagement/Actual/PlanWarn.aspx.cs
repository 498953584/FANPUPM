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
public partial class ProgressManagement_Actual_PlanWarn : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
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
		string userCode = base.UserCode;
		DataTable dataSource = null;
		if (!string.IsNullOrEmpty(this.prjId))
		{
			int latestPlansCount = Progress.GetLatestPlansCount(this.prjId, userCode);
			this.AspNetPager1.RecordCount = latestPlansCount;
			dataSource = Progress.GetLatestPlans(this.prjId, userCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		}
		this.gvwPlans.DataSource = dataSource;
		this.gvwPlans.DataBind();
	}
	protected void gvwPlans_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressId"].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPlans();
	}
}


