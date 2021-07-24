using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ResourceTypeItem : BasePage, IRequiresSessionState
	{

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
				if (base.Request["vc"] == null || base.Request["cc"] == null || base.Request["t"] == null)
				{
					return;
				}
				this.VersionCode = new Guid(base.Request["vc"]);
				this.CategoryParentCode = base.Request["cc"];
				this.PassResourceStyle = (ResourceStyle)Enum.Parse(typeof(ResourceStyle), base.Request["t"]);
				this.DgdCategory_Bind(this.VersionCode, this.CategoryParentCode);
			}
		}
		private void DgdCategory_Bind(Guid versionCode, string categoryCode)
		{
			this.DgdCategory.DataSource = this.ResCategoryAct.QuerySubCategoryList(versionCode, categoryCode);
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
				ResourceCategory resourceCategory = (ResourceCategory)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);dbClickResRow('",
					resourceCategory.CategoryCode.ToString(),
					"','",
					resourceCategory.CategoryName.ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				break;
			}
			}
			e.Item.Cells[0].Attributes["style"] = "display:none";
		}
	}


