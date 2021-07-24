using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
	public partial class ScheduleFrame : Page, IRequiresSessionState
	{

		private void Page_Load(object sender, EventArgs e)
		{
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


