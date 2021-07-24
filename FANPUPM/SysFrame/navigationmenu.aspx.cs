using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class NavigationMenu : BasePage, IRequiresSessionState
	{
		public string strSkinPath = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.strSkinPath = "skin" + this.Session["SkinID"].ToString();
				if (base.Request["id"] != null)
				{
					this.l_title.Text = userManageDb.GetModuleName(base.Request["id"]);
					this.Session["mkdm"] = base.Request["id"].ToString();
					this.JS.Text = "top.frameWorkArea.location.replace('" + base.Server.UrlDecode(base.Request.QueryString["url"].ToString()) + "')";
					return;
				}
				this.l_title.Text = "首页";
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


