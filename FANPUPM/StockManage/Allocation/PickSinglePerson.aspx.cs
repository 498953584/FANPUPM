using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class PickSinglePerson : BasePage, IRequiresSessionState
	{

		private void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack && base.Request.QueryString["txt"] != null && base.Request.QueryString["hid"] != null)
			{
				this.Hdntxt.Value = base.Request.QueryString["txt"];
				this.Hdnhid.Value = base.Request.QueryString["hid"];
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new EventHandler(this.Page_Load);
		}
	}


