using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
	public partial class ResourceTypeFrame : PageBase, IRequiresSessionState
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


