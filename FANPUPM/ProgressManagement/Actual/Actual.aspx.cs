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
public partial class ProgressManagement_Actual_Actual : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindDrop();
		this.BindTree();
		this.BindPlans();
		this.AspNetPager1.PageSize = NBasePage.pagesize;
	}
	public void BindDrop()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindDlistYears(this.dropYear, this.Session["PmSet"], base.UserCode, base.Request["year"]);
	}
	protected void dropYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindTree();
	}
	protected void BindTree()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindTreeNodes(this.trvwBudget, this.Session["PmSet"], base.UserCode, base.Request["prjId"], this.dropYear.SelectedItem.Text, this.dropYear.SelectedValue);
	}
	protected void trvwBudget_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.BindPlans();
	}
	protected void BindPlans()
	{
		string userCode = base.UserCode;
		string selectedValue = this.trvwBudget.SelectedValue;
		DataTable dataSource = null;
		if (!string.IsNullOrEmpty(selectedValue))
		{
			int latestPlansCount = Progress.GetLatestPlansCount(selectedValue, userCode);
			this.AspNetPager1.RecordCount = latestPlansCount;
			dataSource = Progress.GetLatestPlans(selectedValue, userCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		}
		this.gvwPlans.DataSource = dataSource;
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


