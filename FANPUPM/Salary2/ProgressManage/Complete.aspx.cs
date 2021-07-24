using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManage_Complete : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
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
		this.hfldTreeSelVale.Value = this.tvBudget.SelectedValue;
	}
	public void BindDrop()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindDlistYears(this.ddlYear, this.Session["PmSet"], base.UserCode, base.Request["year"]);
	}
	protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.BindTree();
	}
	protected void BindTree()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindTreeNodes(this.tvBudget, this.Session["PmSet"], base.UserCode, base.Request["prjId"], this.ddlYear.SelectedItem.Text, this.ddlYear.SelectedValue);
		TreeNode arg_64_0 = this.tvBudget.Nodes[0];
	}
	protected void tvBudget_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.hfldTreeSelVale.Value = this.tvBudget.SelectedValue;
	}
}


