using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManage_Schema : NBasePage, IRequiresSessionState
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
		this.hfldTreeSelVale.Value = this.tvBudget.SelectedValue;
	}
	protected void tvBudget_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.hfldTreeSelVale.Value = this.tvBudget.SelectedValue;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string text = this.txtStartDate.Text;
		string text2 = this.txtEndDate.Text;
		DateTime? startDate = null;
		DateTime? endDate = null;
		if (!string.IsNullOrEmpty(text))
		{
			startDate = new DateTime?(Convert.ToDateTime(text));
		}
		if (!string.IsNullOrEmpty(text2))
		{
			endDate = new DateTime?(Convert.ToDateTime(text2));
		}
		ProjectInfo.UpdateProjectDate(this.tvBudget.SelectedValue, startDate, endDate);
	}
}


