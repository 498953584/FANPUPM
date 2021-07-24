using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_DeptTree : BasePage, IRequiresSessionState
{

	public PTDUTYAction hrAction
	{
		get
		{
			return new PTDUTYAction();
		}
	}
	protected int Type
	{
		get
		{
			return Convert.ToInt32(this.ViewState["Type"]);
		}
		set
		{
			this.ViewState["Type"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["t"] != null)
		{
			this.Type = Convert.ToInt32(base.Request["t"]);
			this.Tree_Bind();
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
				treeNode.Target = "frmPage";
				switch (this.Type)
				{
				case 1:
					treeNode.NavigateUrl = "PersonnelSalaryList.aspx?vb=" + dataRow["v_bmbm"].ToString();
					break;
				case 2:
					treeNode.NavigateUrl = "SalaryIPIFrame.aspx?vb=" + dataRow["v_bmbm"].ToString();
					break;
				}
				this.TVCorp.Nodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
	private void Bind_SubTree(TreeNode ftn, string strBMDM)
	{
		DataTable dataTable = new DataTable();
		dataTable = this.hrAction.GetDepartmentTree(strBMDM);
		if (dataTable.Rows.Count > 0)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Target = "frmPage";
				switch (this.Type)
				{
				case 1:
					treeNode.NavigateUrl = "PersonnelSalaryList.aspx?vb=" + dataRow["v_bmbm"].ToString();
					break;
				case 2:
					treeNode.NavigateUrl = "SalaryIPIFrame.aspx?vb=" + dataRow["v_bmbm"].ToString();
					break;
				}
				ftn.ChildNodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
}


