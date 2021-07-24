using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class DeptTree : BasePage, IRequiresSessionState
	{
		private int _iSysID;

		protected void Page_Load(object sender, EventArgs e)
		{
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this.TViewDept_Create(this._iSysID);
		}
		private void TViewDept_Create(int iSysID)
		{
			this.TViewDept.Nodes.Clear();
			DeptManageDb deptManageDb = new DeptManageDb();
			DataTable allDepartment = deptManageDb.GetAllDepartment();
			this.TViewDept_Generate(this.TViewDept.Nodes, allDepartment, "0", iSysID);
		}
		private void TViewDept_Generate(TreeNodeCollection preNodes, DataTable deptDTable, string isjCode, int iSysID)
		{
			DataRow[] array = deptDTable.Select("i_sjdm=" + isjCode);
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				TreeNode treeNode = new TreeNode();
				treeNode.NodeData = dataRow["i_bmdm"].ToString();
				treeNode.Text = dataRow["v_bmmc"].ToString();
				if (dataRow["i_bmdm"].ToString().Trim() != "1")
				{
					treeNode.NavigateUrl = "userlist.aspx?deptid=" + dataRow["i_bmdm"].ToString();
					treeNode.Target = "fraHuman";
				}
				preNodes.Add(treeNode);
				this.TViewDept_Generate(treeNode.Nodes, deptDTable, dataRow["i_bmdm"].ToString(), iSysID);
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


