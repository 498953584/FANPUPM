using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class DeptList : BasePage, IRequiresSessionState
	{

		protected static DeptManageDb DeptAct
		{
			get
			{
				return new DeptManageDb();
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TVDept_AppendNode(this.TVDept.Nodes, 0);
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.TVDept.Expand += new ClickEventHandler(this.TVDept_Expand);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void TVDept_AppendNode(TreeNodeCollection nodes, int parentID)
		{
			DataTable subDepartment = DeptList.DeptAct.GetSubDepartment(parentID);
			int count = subDepartment.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					DataRow dataRow = subDepartment.Rows[i];
					TreeNode treeNode = new TreeNode();
					treeNode.ID = dataRow["i_bmdm"].ToString();
					treeNode.NodeData = "0";
					treeNode.Text = dataRow["v_bmmc"].ToString();
					treeNode.NavigateUrl = "HumanSList.aspx?pid=" + dataRow["i_bmdm"].ToString();
					treeNode.Target = "FraHuman";
					if ((int)dataRow["i_xjbm"] > 0)
					{
						TreeNode treeNode2 = new TreeNode();
						treeNode2.Text = "Load...";
						treeNode.Nodes.Add(treeNode2);
					}
					nodes.Add(treeNode);
				}
			}
		}
		private void TVDept_Expand(object sender, TreeViewClickEventArgs e)
		{
			TreeNode nodeFromIndex = this.TVDept.GetNodeFromIndex(e.Node.ToString());
			nodeFromIndex.NodeData = "1";
			if (nodeFromIndex.Nodes.Count == 1)
			{
				nodeFromIndex.Nodes.Remove(nodeFromIndex.Nodes[0]);
				this.TVDept_AppendNode(nodeFromIndex.Nodes, System.Convert.ToInt32(nodeFromIndex.ID));
			}
		}
	}


