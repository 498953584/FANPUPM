using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Warn;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManage_Warn : NBasePage, IRequiresSessionState
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
		this.DisabledPropressValid();
	}
	public void DisabledPropressValid()
	{
		string text = base.Request["id"];
		if (!string.IsNullOrEmpty(text))
		{
			Warning.UpdateValid(text);
		}
	}
	public void BindDrop()
	{
		ProjectTree projectTree = new ProjectTree();
		string text = base.Request["prjId"];
		this.hfldTreeSelVale.Value = text;
		string selectedYearValue = string.Empty;
		if (!string.IsNullOrEmpty(text))
		{
			DataTable projectDate = ProjectInfo.GetProjectDate(text);
			if (projectDate != null && projectDate.Rows.Count > 0)
			{
				object obj = projectDate.Rows[0]["StartDate"];
				string value = (obj == null) ? string.Empty : obj.ToString();
				selectedYearValue = Convert.ToDateTime(value).Year.ToString();
			}
		}
		projectTree.BindDlistYears(this.ddlYear, this.Session["PmSet"], base.UserCode, selectedYearValue);
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
}


