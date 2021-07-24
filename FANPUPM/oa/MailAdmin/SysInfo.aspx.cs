using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class SysInfo : BasePage, IRequiresSessionState
	{
		private int _iSysID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.TViewSystem_Create();
			}
			this.SetDefault(this.TViewSystem.Nodes);
		}
		private bool SetDefault(TreeNodeCollection tnp)
		{
			bool result = false;
			foreach (TreeNode treeNode in tnp)
			{
				if (treeNode.NodeData == this._iSysID.ToString())
				{
					this.TViewSystem.SelectedNodeIndex = treeNode.GetNodeIndex();
					result = true;
					break;
				}
				if (this.SetDefault(treeNode.Nodes))
				{
					break;
				}
			}
			return result;
		}
		private void TViewSystem_Create()
		{
			SysManageDb sysManageDb = new SysManageDb();
			DataTable dtSystem = sysManageDb.QueryAllSys();
			this.TViewSystem_Generate(this.TViewSystem.Nodes, dtSystem, "0");
		}
		private void TViewSystem_Generate(TreeNodeCollection preNodes, DataTable dtSystem, string iSysHigher)
		{
			DataRow[] array = dtSystem.Select("i_sysHigher=" + iSysHigher.ToString());
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				TreeNode treeNode = new TreeNode();
				treeNode.NodeData = dataRow["i_SysID"].ToString();
				treeNode.Text = dataRow["v_SysName"].ToString();
				treeNode.NavigateUrl = "DeptInfo.aspx?sysid=" + dataRow["i_SysID"].ToString();
				treeNode.Target = "FraDept";
				preNodes.Add(treeNode);
				if (dataRow["i_IsDefault"].ToString() == "1")
				{
					this._iSysID = int.Parse(dataRow["i_SysID"].ToString());
				}
				this.TViewSystem_Generate(treeNode.Nodes, dtSystem, dataRow["i_SysID"].ToString());
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


