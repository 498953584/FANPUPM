using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class Broker : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string text = base.Request.QueryString["go"].ToString();
			string a;
			if ((a = text) != null)
			{
				if (!(a == "1"))
				{
					return;
				}
				this.fraAnnex.Attributes["src"] = "Annex.aspx";
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


