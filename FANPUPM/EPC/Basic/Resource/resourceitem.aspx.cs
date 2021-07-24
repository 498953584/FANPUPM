using ASP;
using cn.justwin.stockBLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ResourceItem : PageBase, IRequiresSessionState
	{
		public ResourceTypeManage manage = new ResourceTypeManage();
		protected ResourceCategoryAction ResCategoryAct
		{
			get
			{
				return new ResourceCategoryAction();
			}
		}
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
		protected ResourceStyle PassResourceStyle
		{
			get
			{
				object obj = this.ViewState["PASSRESOURCESTYLE"];
				if (obj != null)
				{
					return (ResourceStyle)obj;
				}
				return ResourceStyle.Unknown;
			}
			set
			{
				this.ViewState["PASSRESOURCESTYLE"] = value;
			}
		}
		public string CategoryParentCode
		{
			get
			{
				object obj = this.ViewState["CATEGORYPARENTCODE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CATEGORYPARENTCODE"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["cc"] == null || base.Request["cc"].ToString() == "")
				{
					return;
				}
				this.CategoryParentCode = base.Request["cc"];
				this.DgdCategory_Bind(this.CategoryParentCode);
			}
		}
		private void DgdCategory_Bind(string categoryCode)
		{
			this.DgdCategory.DataSource = this.manage.getChildList(categoryCode);
			this.DgdCategory.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DgdCategory.ItemDataBound += new DataGridItemEventHandler(this.DgdCategory_ItemDataBound);
		}
		private void DgdCategory_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);dbClickResRow('",
					dataRowView["ResourceTypeCode"].ToString(),
					"','",
					dataRowView["ResourceTypeName"].ToString(),
					"','",
					dataRowView["ResourceCode"].ToString(),
					"','",
					dataRowView["ResourceName"].ToString(),
					"','",
					dataRowView["Specification"].ToString(),
					"')"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				break;
			}
			}
			e.Item.Cells[0].Attributes["style"] = "display:none";
		}
	}


