using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class Boardroom : BasePage, IRequiresSessionState
	{

		public DeptManageDb dmd
		{
			get
			{
				return new DeptManageDb();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.OrgManageTree();
			}
		}
		protected void TvCorp_Created()
		{
			DataTable dataTable = ConferenceManage.QueryCorpCode();
			TreeNode treeNode = new TreeNode();
			treeNode.Text = "集团公司";
			treeNode.NavigateUrl = "";
			treeNode.Target = "BoardroomList";
			this.TVCorp.Nodes.Add(treeNode);
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["corpName"].ToString();
				treeNode2.NavigateUrl = "BoardroomList.aspx?code=" + dataRow["corpCode"].ToString();
				treeNode2.Target = "BoardroomList";
				treeNode.Nodes.Add(treeNode2);
			}
		}
		protected void OrgManageTree()
		{
			DataTable depTree = this.dmd.GetDepTree();
			DataTable depTree2 = this.dmd.GetDepTree1();
			TreeNode treeNode = new TreeNode();
			treeNode.Text = depTree.Rows[0]["V_BMMC"].ToString();
			treeNode.NavigateUrl = "BoardroomList.aspx?code=" + depTree.Rows[0]["i_bmdm"].ToString();
			treeNode.Target = "BoardroomList";
			this.TVCorp.Nodes.Add(treeNode);
			foreach (DataRow dataRow in depTree2.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["V_BMMC"].ToString();
				treeNode2.NavigateUrl = "BoardroomList.aspx?code=" + dataRow["i_bmdm"].ToString();
				treeNode2.Target = "BoardroomList";
				treeNode.Nodes.Add(treeNode2);
			}
		}
	}


