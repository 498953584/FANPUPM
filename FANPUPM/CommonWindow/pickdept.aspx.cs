using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class PickDept : BasePage, IRequiresSessionState
	{
		protected static DeptManageDb DeptAct
		{
			get
			{
				return new DeptManageDb();
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
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
		}
		private void TVDept_AppendNode(TreeNodeCollection nodes, int parentID)
		{
			DataTable subDepartment = PickDept.DeptAct.GetSubDepartment(parentID);
			int count = subDepartment.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					DataRow dataRow = subDepartment.Rows[i];
					TreeNode treeNode = new TreeNode();
					treeNode.ID = dataRow["i_bmdm"].ToString();
					treeNode.NodeData = "0";
					treeNode.Text = "<span ondblclick=\"selectDept();return false;\">" + dataRow["v_bmmc"].ToString() + "</span>";
					this.TVDept_AppendNode(treeNode.Nodes, System.Convert.ToInt32(treeNode.ID));
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


