using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class WebForm8 : BasePage, IRequiresSessionState
	{
		public string strSkinPath = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this.strSkinPath = "skin" + this.Session["SkinID"].ToString();
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


