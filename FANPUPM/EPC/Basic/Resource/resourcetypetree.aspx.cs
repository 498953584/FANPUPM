using ASP;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ResourceTypeTree : PageBase, IRequiresSessionState
	{
		public ResourceTypeManage manage = new ResourceTypeManage();

		public Guid VersionCode
		{
			get
			{
				object obj = this.ViewState["VERSIONCODE"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["VERSIONCODE"] = value;
			}
		}
		protected ResourceCategoryAction ResCategoryAct
		{
			get
			{
				return new ResourceCategoryAction();
			}
		}
		protected ResourceStyle PassResourceStyle
		{
			get
			{
				object obj = this.ViewState["PASSRESOURCESTYLE"];
				if (obj != null)
				{
					return (ResourceStyle)obj;
				}
				return ResourceStyle.Material;
			}
			set
			{
				this.ViewState["PASSRESOURCESTYLE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.VersionCode = new Guid("896431D1-F875-47EC-8164-CED63F6E65F2");
				this.Data_Bind();
			}
		}
		private void Data_Bind()
		{
			ResourceType firstNodes = this.manage.getFirstNodes("6A1A7050-1F92-4291-B932-43E1DFCE6E92");
			TreeNode treeNode = new TreeNode();
			treeNode.ID = firstNodes.ResourceTypeId;
			treeNode.NodeData = "1";
			treeNode.Text = firstNodes.ResourceTypeName;
			treeNode.NavigateUrl = "ResourceItem.aspx?cc=" + firstNodes.ResourceTypeId;
			treeNode.Target = "fitem";
			this.TVResource.Nodes.Add(treeNode);
			this.TVResource_AppendNode(treeNode.Nodes, firstNodes.ResourceTypeId);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.TVResource.Expand += new ClickEventHandler(this.TVResource_Expand);
		}
		private void TVResource_AppendNode(TreeNodeCollection nodes, string parentid)
		{
			List<ResourceType> childNodes = this.manage.getChildNodes(parentid);
			int count = childNodes.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					ResourceType resourceType = new ResourceType();
					resourceType = childNodes[i];
					TreeNode treeNode = new TreeNode();
					treeNode.ID = resourceType.ResourceTypeId;
					treeNode.NodeData = "0";
					treeNode.Text = resourceType.ResourceTypeName;
					treeNode.NavigateUrl = "ResourceItem.aspx?cc=" + resourceType.ResourceTypeId;
					treeNode.Target = "fitem";
					if (this.manage.ISHaveChildNodes(resourceType.ResourceTypeId))
					{
						TreeNode treeNode2 = new TreeNode();
						treeNode2.Text = "Load...";
						treeNode.Nodes.Add(treeNode2);
					}
					nodes.Add(treeNode);
				}
			}
		}
		private void TVResource_Expand(object sender, TreeViewClickEventArgs e)
		{
			TreeNode nodeFromIndex = this.TVResource.GetNodeFromIndex(e.Node.ToString());
			nodeFromIndex.Nodes.Clear();
			this.TVResource_AppendNode(nodeFromIndex.Nodes, nodeFromIndex.ID);
		}
	}


