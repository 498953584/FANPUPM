using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DeptInfo : BasePage, IRequiresSessionState
	{
		private int _iSysID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.TViewDept_Create();
		}
		private void TViewDept_Create()
		{
			this.TViewDept.Nodes.Clear();
			DeptManageDb deptManageDb = new DeptManageDb();
			DataSet allDepartmentSet = deptManageDb.GetAllDepartmentSet();
			DataTable deptDTable = allDepartmentSet.Tables[0];
			this.TViewDept_Generate(this.TViewDept.Nodes, deptDTable, "0");
			this.Page.RegisterClientScriptBlock("gourl", "<SCRIPT language=\"JavaScript\">parent.FraHuman.location.href='human.aspx?deptid=0';</SCRIPT>");
		}
		private void TViewDept_Generate(TreeNodeCollection preNodes, DataTable deptDTable, string isjCode)
		{
			DataRow[] array = deptDTable.Select("i_sjdm=" + isjCode);
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				TreeNode treeNode = new TreeNode();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Text = dataRow["v_bmmc"].ToString();
				if (dataRow["i_bmdm"].ToString().Trim() != "1")
				{
					treeNode.NavigateUrl = "human.aspx?deptid=" + dataRow["i_bmdm"].ToString();
					treeNode.Target = "FraHuman";
				}
				preNodes.Add(treeNode);
				this.TViewDept_Generate(treeNode.ChildNodes, deptDTable, dataRow["i_bmdm"].ToString());
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


