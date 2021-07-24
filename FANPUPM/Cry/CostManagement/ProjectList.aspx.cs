using ASP;
using com.jwsoft.pm.web;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ProjectList : PageBase, IRequiresSessionState
	{

		private void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.UProjectList1.UserCode = base.UserCode;
				this.UProjectList1.SubPrjUrl = "ProfitView.aspx";
				this.UProjectList1.TargetFrame = "fraSchedule";
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


