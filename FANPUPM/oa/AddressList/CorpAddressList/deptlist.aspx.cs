using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DeptList : NBasePage, IRequiresSessionState
	{
		private string strTarget;
		private string strNavigationURL;
		private bool blnEnabledLink;
		private AddressListDb myAddress;

		public PTDUTYAction hrAction
		{
			get
			{
				return new PTDUTYAction();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
			if (base.Request.QueryString["flag"].ToString() == "Manage")
			{
				this.strNavigationURL = "LinkmanList.aspx";
				return;
			}
			if (base.Request.QueryString["flag"].ToString() == "Search")
			{
				this.strNavigationURL = "LinkmanSearch.aspx";
			}
		}
		private void InitializeComponent()
		{
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			this.strTarget = "LinkmanList";
			this.blnEnabledLink = true;
			this.myAddress = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				DeptManageDb deptManageDb = new DeptManageDb();
				deptManageDb.GetTopDepartmentSet();
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
					this.tvDept.Nodes.Add(treeNode);
					this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
				}
			}
			this.tvDept.Nodes[0].Select();
			base.RegisterScript(string.Concat(new string[]
			{
				"setVal('",
				this.strNavigationURL,
				"?code=",
				this.tvDept.SelectedValue.Trim(),
				"')"
			}));
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
		protected void tvDept_SelectedNodeChanged(object sender, EventArgs e)
		{
			if (this.blnEnabledLink)
			{
				base.RegisterScript(string.Concat(new string[]
				{
					"setVal('",
					this.strNavigationURL,
					"?code=",
					this.tvDept.SelectedValue.Trim(),
					"')"
				}));
			}
		}
	}


