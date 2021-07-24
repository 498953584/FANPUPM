using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_PostWeaveRoleSelect : BasePage, IRequiresSessionState
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
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.Tree_Bind();
		}
	}
	private void Tree_Bind()
	{
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "岗位名称";
		treeNode.Value = "";
		this.TVDept.Nodes.Add(treeNode);
		DataTable roleTree = this.hrAction.GetRoleTree("ParentCode=''");
		if (roleTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in roleTree.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["RoleTypeName"].ToString();
				treeNode2.Value = dataRow["ParentCode"].ToString();
				treeNode.ChildNodes.Add(treeNode2);
				this.Bind_SubTree(treeNode2, dataRow["RoleTypeCode"].ToString());
			}
		}
	}
	private void Bind_SubTree(TreeNode ftn, string RoleTypeCode)
	{
		DataTable roleTree = this.hrAction.GetRoleTree("ParentCode='" + RoleTypeCode + "' and len(ParentCode)=3");
		if (roleTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in roleTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["RoleTypeName"].ToString();
				treeNode.Value = dataRow["ParentCode"].ToString();
				treeNode.Target = "frmBooks";
				treeNode.NavigateUrl = "PostWeaveRoleSelectData.aspx?cc=" + dataRow["RoleTypeCode"].ToString();
				ftn.ChildNodes.Add(treeNode);
			}
		}
	}
}


