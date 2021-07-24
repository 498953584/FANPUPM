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
public partial class ProgressManagement_Track_LagAnalysis : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.CurrentPageIndex = -1;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindAnalysis();
		}
	}
	protected void BindAnalysis()
	{
		string prjName = this.txtPrjName.Text.Trim();
		string userCode = base.UserCode;
		int lagAnalysisCount = Progress.GetLagAnalysisCount(prjName, userCode);
		this.AspNetPager1.RecordCount = lagAnalysisCount;
		new DataTable();
		this.rptAnalysis.DataSource = Progress.GetLagAnalysis(prjName, userCode, 1, this.AspNetPager1.PageSize);
		this.rptAnalysis.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindAnalysis();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.BindAnalysis();
	}
}


