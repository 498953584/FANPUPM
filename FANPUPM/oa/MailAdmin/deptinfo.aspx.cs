using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DeptInfo : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		private int _iSysID;


		public PTDUTYAction hrAction
		{
			get
			{
				return new PTDUTYAction();
			}
		}
		protected string HT
		{
			get
			{
				return this.ViewState["HT"].ToString();
			}
			set
			{
				this.ViewState["HT"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				try
				{
					this._iSysID = int.Parse(base.Request.QueryString["sysid"].ToString());
				}
				catch
				{
				}
				if (this._iSysID == 0)
				{
					SysManageDb sysManageDb = new SysManageDb();
					this._iSysID = sysManageDb.GetDefault();
				}
				this.HT = base.Request["HT"].ToString();
				this.Tree_Bind();
			}
		}
		private void TViewDept_Create(int iSysID)
		{
			this.TViewDept.Nodes.Clear();
			DeptManageDb deptManageDb = new DeptManageDb();
			DataSet allDepartmentSet = deptManageDb.GetAllDepartmentSet();
			DataTable arg_29_0 = allDepartmentSet.Tables[0];
			this.Tree_Bind();
			string hT;
			if ((hT = this.HT) != null)
			{
				if (hT == "1")
				{
					this.Page.RegisterClientScriptBlock("gourl", "<SCRIPT language=\"JavaScript\">parent.FraHuman.location.href='human.aspx?sysid=" + iSysID.ToString() + "&deptid=0';</SCRIPT>");
					return;
				}
				if (hT == "2")
				{
					this.Page.RegisterClientScriptBlock("gourl", "<SCRIPT language=\"JavaScript\">parent.FraHuman.location.href='human2.aspx?sysid=" + iSysID.ToString() + "&deptid=0';</SCRIPT>");
					return;
				}
				if (!(hT == "3"))
				{
					return;
				}
				this.Page.RegisterClientScriptBlock("gourl", "<SCRIPT language=\"JavaScript\">parent.FraHuman.location.href='human3.aspx?sysid=" + iSysID.ToString() + "&deptid=0';</SCRIPT>");
			}
		}
		private void TViewDept_Generate(Microsoft.Web.UI.WebControls.TreeNodeCollection preNodes, DataTable deptDTable, string isjCode, int iSysID)
		{
			DataRow[] array = deptDTable.Select("i_sjdm=" + isjCode);
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				Microsoft.Web.UI.WebControls.TreeNode treeNode = new Microsoft.Web.UI.WebControls.TreeNode();
				treeNode.NodeData = dataRow["i_bmdm"].ToString();
				treeNode.Text = dataRow["v_bmmc"].ToString();
				string hT;
				if (dataRow["i_bmdm"].ToString().Trim() != "1" && (hT = this.HT) != null)
				{
					if (!(hT == "1"))
					{
						if (!(hT == "2"))
						{
							if (hT == "3")
							{
								treeNode.NavigateUrl = "human3.aspx?sysid=" + this._iSysID.ToString() + "&deptid=" + dataRow["i_bmdm"].ToString();
								treeNode.Target = "FraHuman";
							}
						}
						else
						{
							treeNode.NavigateUrl = "human2.aspx?sysid=" + this._iSysID.ToString() + "&deptid=" + dataRow["i_bmdm"].ToString();
							treeNode.Target = "FraHuman";
						}
					}
					else
					{
						treeNode.NavigateUrl = "human.aspx?sysid=" + this._iSysID.ToString() + "&deptid=" + dataRow["i_bmdm"].ToString();
						treeNode.Target = "FraHuman";
					}
				}
				preNodes.Add(treeNode);
				this.TViewDept_Generate(treeNode.Nodes, deptDTable, dataRow["i_bmdm"].ToString(), iSysID);
			}
		}
		private void Tree_Bind()
		{
			DataTable departmentTree = this.hrAction.GetDepartmentTree("0");
			if (departmentTree.Rows.Count > 0)
			{
				foreach (DataRow dataRow in departmentTree.Rows)
				{
					System.Web.UI.WebControls.TreeNode treeNode = new System.Web.UI.WebControls.TreeNode();
					treeNode.Text = dataRow["V_BMMC"].ToString();
					treeNode.Value = dataRow["i_bmdm"].ToString();
					this.TViewDept.Nodes.Add(treeNode);
					this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
				}
			}
			this.TViewDept.Nodes[0].Select();
		}
		private void Bind_SubTree(System.Web.UI.WebControls.TreeNode ftn, string strBMDM)
		{
			DataTable departmentTree = this.hrAction.GetDepartmentTree(strBMDM);
			if (departmentTree.Rows.Count > 0)
			{
				foreach (DataRow dataRow in departmentTree.Rows)
				{
					System.Web.UI.WebControls.TreeNode treeNode = new System.Web.UI.WebControls.TreeNode();
					treeNode.Text = dataRow["V_BMMC"].ToString();
					treeNode.Value = dataRow["i_bmdm"].ToString();
					ftn.ChildNodes.Add(treeNode);
					this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
				}
			}
		}
		protected void TViewDept_SelectedNodeChanged(object sender, EventArgs e)
		{
			string hT;
			if ((hT = this.HT) != null)
			{
				if (hT == "1")
				{
					this.Page.RegisterClientScriptBlock("gourl", string.Concat(new string[]
					{
						"<SCRIPT language=\"JavaScript\">parent.FraHuman.location.href='human.aspx?sysid=",
						this._iSysID.ToString(),
						"&deptid=",
						this.TViewDept.SelectedValue,
						"';</SCRIPT>"
					}));
					return;
				}
				if (hT == "2")
				{
					this.Page.RegisterClientScriptBlock("gourl", string.Concat(new string[]
					{
						"<SCRIPT language=\"JavaScript\">parent.FraHuman.location.href='human2.aspx?sysid=",
						this._iSysID.ToString(),
						"&deptid=",
						this.TViewDept.SelectedValue,
						"';</SCRIPT>"
					}));
					return;
				}
				if (!(hT == "3"))
				{
					return;
				}
				this.Page.RegisterClientScriptBlock("gourl", string.Concat(new string[]
				{
					"<SCRIPT language=\"JavaScript\">parent.FraHuman.location.href='human3.aspx?sysid=",
					this._iSysID.ToString(),
					"&deptid=",
					this.TViewDept.SelectedValue,
					"';</SCRIPT>"
				}));
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


