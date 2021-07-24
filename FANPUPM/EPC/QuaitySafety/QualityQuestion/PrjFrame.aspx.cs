using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
	public partial class PrjFrame : Page, IRequiresSessionState
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new System.EventHandler(this.Page_Load);
		}
	}


