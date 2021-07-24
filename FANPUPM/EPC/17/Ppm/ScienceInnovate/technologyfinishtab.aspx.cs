using ASP;
using com.jwsoft.common.baseclass;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TechnologyFinishTab : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["PrjCode"] == null)
			{
				this.Session["PRJCODE"] = "";
				return;
			}
			this.Session["PRJCODE"] = base.Request.QueryString["PrjCode"].ToString();
			if (base.Request.QueryString["Levels"].ToString().Trim() != "")
			{
				this.Session["MEASURELEVELS"] = base.Request.QueryString["Levels"].ToString();
				return;
			}
			this.Session["MEASURELEVELS"] = "";
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


