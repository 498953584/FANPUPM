using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Salary2_DepartmentFrame : NBasePage, IRequiresSessionState
{
	private PTDUTYAction hrAction = new PTDUTYAction();
	private string path = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["path"]))
		{
			this.path = base.Request["path"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Tree_Bind();
			if (this.path == "UserSaBooks")
			{
				this.InfoList.Attributes.Add("src", "UserSalaryBooks.aspx?department=" + this.tvDepartment.SelectedValue.Trim());
				return;
			}
			if (this.path == "SaMonthSalary")
			{
				this.InfoList.Attributes.Add("src", "SaMonthSalary.aspx?department=" + this.tvDepartment.SelectedValue.Trim());
				return;
			}
			if (this.path == "PayoffSalary")
			{
				this.InfoList.Attributes.Add("src", "PayoffSalary.aspx?department=" + this.tvDepartment.SelectedValue.Trim());
				return;
			}
			if (this.path == "SaMonthReport")
			{
				this.InfoList.Attributes.Add("src", "SalaryMonthReport.aspx?department=" + this.tvDepartment.SelectedValue.Trim());
			}
		}
	}
	protected void tvDepartment_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		if (this.path == "UserSaBooks")
		{
			this.InfoList.Attributes.Add("src", "UserSalaryBooks.aspx?department=" + this.tvDepartment.SelectedValue.Trim());
			return;
		}
		if (this.path == "SaMonthSalary")
		{
			this.InfoList.Attributes.Add("src", "SaMonthSalary.aspx?department=" + this.tvDepartment.SelectedValue.Trim());
			return;
		}
		if (this.path == "PayoffSalary")
		{
			this.InfoList.Attributes.Add("src", "PayoffSalary.aspx?department=" + this.tvDepartment.SelectedValue.Trim());
			return;
		}
		if (this.path == "SaMonthReport")
		{
			this.InfoList.Attributes.Add("src", "SalaryMonthReport.aspx?department=" + this.tvDepartment.SelectedValue.Trim());
		}
	}
	private void Tree_Bind()
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree("0");
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Selected = true;
				this.tvDepartment.Nodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
	private void Bind_SubTree(TreeNode ftn, string strBMDM)
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree(strBMDM);
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				ftn.ChildNodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
}


