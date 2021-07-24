using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class FlowRole : BasePage, IRequiresSessionState
	{
		private string rid = string.Empty;
		public PTDUTYAction hrAction
		{
			get
			{
				return new PTDUTYAction();
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.hfldSelectedVal.Value = base.Request.QueryString["val"];
				this.TVRole_Create();
				this.KeepTreeViewState();
			}
		}
		private void KeepTreeViewState()
		{
			foreach (TreeNode treeNode in this.TVRole.Nodes)
			{
				if (treeNode.Value == this.hfldSelectedVal.Value)
				{
					treeNode.Selected = true;
					this.InfoList.Attributes["src"] = "RoleList.aspx?rt=" + this.hfldSelectedVal.Value;
					break;
				}
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request["ri"]))
			{
				this.rid = base.Request["ri"];
			}
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void TVRole_Create()
		{
			DataTable dataTable = FlowRoleAction.QueryAllRole();
			for (int i = 1; i < 4; i++)
			{
				TreeNode treeNode = new TreeNode();
				string text = "";
				switch (i)
				{
				case 1:
					text = "项目相关";
					break;
				case 2:
					text = "群组";
					break;
				case 3:
					text = "部门相关";
					break;
				}
				treeNode.Text = text;
				treeNode.Value = i.ToString();
				this.TVRole.Nodes.Add(treeNode);
				DataRow[] array = dataTable.Select("RoleType=" + i.ToString());
				if (array.Length > 0)
				{
					for (int j = 0; j < array.Length; j++)
					{
						DataRow dataRow = array[j];
						TreeNode treeNode2 = new TreeNode();
						treeNode2.Text = dataRow["RoleName"].ToString();
						treeNode2.Value = i.ToString() + j;
						if (i == 1)
						{
							treeNode2.Value = "RoleProjectList.aspx?ri=" + dataRow["RoleID"].ToString();
						}
						else
						{
							if (i == 2)
							{
								treeNode2.Value = "RoleUser.aspx?ri=" + dataRow["RoleID"].ToString();
							}
							else
							{
								treeNode2.Value = "RoleCorpUser.aspx?ri=" + dataRow["RoleID"].ToString();
							}
						}
						treeNode.ChildNodes.Add(treeNode2);
					}
				}
			}
		}
		protected void TVRole_SelectedNodeChanged(object sender, System.EventArgs e)
		{
			string selectedValue = this.TVRole.SelectedValue;
			if (selectedValue == "1" || selectedValue == "2" || selectedValue == "3")
			{
				this.hfldSelectedVal.Value = selectedValue;
				this.KeepTreeViewState();
				this.InfoList.Attributes["src"] = "/EPC/Workflow/RoleList.aspx?rt=" + selectedValue;
				return;
			}
			this.InfoList.Attributes["src"] = selectedValue;
		}
	}


