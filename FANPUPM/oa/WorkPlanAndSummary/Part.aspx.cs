using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_Part : NBasePage, IRequiresSessionState
{
	public PTDUTYAction hrAction
	{
		get
		{
			return new PTDUTYAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdnurl.Value = base.Request.QueryString["FromUrl"];
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
				this.TVCorp.Nodes.Add(treeNode);
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
	protected void TVCorp_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.hdnDeptID.Value = this.TVCorp.SelectedValue.ToString();
		this.hdntext.Value = this.TVCorp.SelectedNode.Text;
	}
}


