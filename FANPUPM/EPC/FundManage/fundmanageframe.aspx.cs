using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
	public partial class FundManageFrame : Page, IRequiresSessionState
	{
		public string strUrl = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pt"] != null)
			{
				this.strUrl = "FundManageTab.aspx?pt=" + base.Request["pt"].ToString() + "&Type=" + base.Request.QueryString["Type"].ToString();
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


