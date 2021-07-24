using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class EntStandardTree : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		private TreeNode Node = new TreeNode();
		private DataTable dt = new DataTable();
		private string type = "";
		private string PrjCode = "";
		private string Levels = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.ViewState["TYPE"] = base.Request.QueryString["type"].ToString();
				this.type = this.ViewState["TYPE"].ToString();
				if (base.Request.QueryString["PrjCode"].ToString().Trim() != "")
				{
					this.ViewState["PRJCODE"] = base.Request.QueryString["PrjCode"].ToString();
				}
				else
				{
					this.ViewState["PRJCODE"] = "";
				}
				if (base.Request.QueryString["Levels"].ToString().Trim() != "")
				{
					this.ViewState["LEVELS"] = base.Request.QueryString["Levels"].ToString();
				}
				else
				{
					this.ViewState["LEVELS"] = "";
				}
				this.PrjCode = this.ViewState["PRJCODE"].ToString();
				this.Levels = this.ViewState["LEVELS"].ToString();
				ProjectFilesUpAction projectFilesUpAction = new ProjectFilesUpAction();
				this.Node.NodeData = "608cd3a8-5040-47ef-ba42-b5a4b9dfebe4";
				this.Node.Text = "企业技术标准";
				this.Node.Target = "Project";
				if (this.ViewState["TYPE"].ToString() == "2")
				{
					this.Node.NavigateUrl = "ScienceInnovateList.aspx?tcode=" + this.Node.NodeData;
				}
				this.TreeView1.Nodes.Add(this.Node);
				int special = 1;
				this.dt = projectFilesUpAction.FilesClassList(special);
				this.CreateFilesClassTree(this.Node, "608cd3a8-5040-47ef-ba42-b5a4b9dfebe4");
				return;
			}
			this.type = this.ViewState["TYPE"].ToString();
			this.PrjCode = this.ViewState["PRJCODE"].ToString();
			this.Levels = this.ViewState["LEVELS"].ToString();
		}
		private void CreateFilesClassTree(TreeNode Nodes, string parentCode)
		{
			DataRow[] array = this.dt.Select("ParentClassID ='" + parentCode + "'");
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.NodeData = array[i]["ClassID"].ToString();
					treeNode.Text = array[i]["ClassName"].ToString();
					treeNode.Target = "Project";
					if (this.ViewState["TYPE"].ToString() == "2")
					{
						treeNode.NavigateUrl = "ScienceInnovateList.aspx?tcode=" + treeNode.NodeData;
					}
					else
					{
						treeNode.NavigateUrl = string.Concat(new string[]
						{
							"EntStandardQuery.aspx?FilesClassID=",
							treeNode.NodeData,
							"&type=",
							this.type,
							"&PrjCode=",
							this.PrjCode,
							"&Levels=",
							this.Levels
						});
					}
					Nodes.Nodes.Add(treeNode);
					this.CreateFilesClassTree(treeNode, array[i]["ClassID"].ToString());
				}
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


