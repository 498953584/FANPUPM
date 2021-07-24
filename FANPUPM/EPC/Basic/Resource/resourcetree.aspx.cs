using ASP;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ResourceTree : PageBase, IRequiresSessionState
	{
		public ResourceTypeManage manage = new ResourceTypeManage();
		private ResourceCategoryAction _ResCategoryAct = new ResourceCategoryAction();
		private static string _parentid;

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
		protected Guid OpSession
		{
			get
			{
				object obj = this.ViewState["OPSESSION"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["OPSESSION"] = value;
			}
		}
		protected static ResourceCategoryAction ResCategoryAct
		{
			get
			{
				return new ResourceCategoryAction();
			}
		}
		public static string Parentid
		{
			get
			{
				return ResourceTree._parentid;
			}
			set
			{
				ResourceTree._parentid = value;
			}
		}
		protected com.jwsoft.pm.entpm.model.ResourceStyle PassResourceStyle
		{
			get
			{
				object obj = this.ViewState["PASSRESOURCESTYLE"];
				if (obj != null)
				{
					return (com.jwsoft.pm.entpm.model.ResourceStyle)obj;
				}
				return com.jwsoft.pm.entpm.model.ResourceStyle.Unknown;
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
				if (base.Request.QueryString["ses"] != null)
				{
					ResourceTree._parentid = base.Request.QueryString["ses"].ToString();
					this.OpSession = new Guid(ResourceTree._parentid);
				}
				ResourceType firstNodes = this.manage.getFirstNodes(ResourceTree._parentid);
				TreeNode treeNode = new TreeNode();
				treeNode.Value = firstNodes.ResourceTypeId;
				treeNode.Text = firstNodes.ResourceTypeName;
				this.TVResource.Nodes.Add(treeNode);
				this.TVResource_AppendNode(treeNode, firstNodes.ResourceTypeId);
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
		private void TVResource_AppendNode(TreeNode rootNode, string parentid)
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
					treeNode.Value = resourceType.ResourceTypeId;
					treeNode.Text = resourceType.ResourceTypeName;
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						"ResourceSelect.aspx?vc=",
						resourceType.ResourceTypeId,
						"&cate=",
						resourceType.ResourceTypeCode,
						"&ses=",
						this.OpSession.ToString(),
						"&t=",
						base.Request["t"]
					});
					treeNode.Target = "fraResource";
					rootNode.ChildNodes.Add(treeNode);
					this.TVResource_AppendNode(treeNode, resourceType.ResourceTypeId);
				}
			}
		}
	}


