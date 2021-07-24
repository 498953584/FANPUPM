using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
	public partial class TechnologyFinishFrame : Page, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
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


