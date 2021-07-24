using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class PickDuty : BasePage, IRequiresSessionState
	{

		public string CorpCode
		{
			get
			{
				object obj = this.ViewState["CorpCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CorpCode"] = value;
			}
		}
		public int DeptID
		{
			get
			{
				object obj = this.ViewState["DeptID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["DeptID"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				this.CorpCode = this.Session["corpcode"].ToString();
				if (base.Request.QueryString["cc"].ToString() != "")
				{
					this.DeptID = Convert.ToInt32(base.Request.QueryString["cc"].ToString());
				}
				this.TViewDept_Create();
				this.BtnOk.Attributes["onclick"] = "confirmDuty();";
			}
		}
		private void InitTree(System.Web.UI.WebControls.TreeNodeCollection nodes, int parentID)
		{
			DataTable deptmentList = PersonnelAction.getDeptmentList();
			DataRow[] array = deptmentList.Select("i_sjdm =" + parentID.ToString());
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				System.Web.UI.WebControls.TreeNode treeNode = new System.Web.UI.WebControls.TreeNode();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Target = "FraDuty";
				treeNode.NavigateUrl = "DutyList.aspx?=DeptID=" + dataRow["i_bmdm"].ToString();
				nodes.Add(treeNode);
				this.InitTree(treeNode.ChildNodes, Convert.ToInt32(treeNode.Value));
			}
		}
		protected void BtnOk_Click(object sender, EventArgs e)
		{
		}
		private void TViewDept_Create()
		{
			this.TViewDept.Nodes.Clear();
			DeptManageDb deptManageDb = new DeptManageDb();
			DataSet allDepartmentSet = deptManageDb.GetAllDepartmentSet();
			DataTable deptDTable = allDepartmentSet.Tables[0];
			this.TViewDept_Generate(this.TViewDept.Nodes, deptDTable, "0");
		}
		private void TViewDept_Generate(Microsoft.Web.UI.WebControls.TreeNodeCollection preNodes, DataTable deptDTable, string isjCode)
		{
			DataRow[] array = deptDTable.Select("i_sjdm=" + isjCode);
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				Microsoft.Web.UI.WebControls.TreeNode treeNode = new Microsoft.Web.UI.WebControls.TreeNode();
				treeNode.NodeData = dataRow["i_bmdm"].ToString();
				treeNode.Text = dataRow["v_bmmc"].ToString();
				treeNode.Target = "_self";
				treeNode.NavigateUrl = "javascript:clicknode('" + treeNode.NodeData + "');";
				preNodes.Add(treeNode);
				this.TViewDept_Generate(treeNode.Nodes, deptDTable, dataRow["i_bmdm"].ToString());
			}
		}
	}


