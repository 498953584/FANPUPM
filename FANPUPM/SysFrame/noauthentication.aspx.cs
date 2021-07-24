using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
	public partial class NoAuthentication : Page, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsStartupScriptRegistered("NoAuthentication"))
			{
				this.Page.RegisterStartupScript("NoAuthentication", "<script language='javascript'>alert('对不起，产品没有授权，您现在无法使用！请联系产品供应商');window.top.close();</script>");
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


