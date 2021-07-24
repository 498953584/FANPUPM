using ASP;
using com.jwsoft.common.baseclass;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DrawCheckUpTabPage : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["PrjCode"] == null)
			{
				this.Session["PRJCODE"] = "";
				this.tabSchedule.Enabled = false;
				return;
			}
			this.Session["PRJCODE"] = base.Request.QueryString["PrjCode"].ToString();
			if (base.Request.QueryString["Levels"].ToString().Trim() != "")
			{
				this.Session["MEASURELEVELS"] = base.Request.QueryString["Levels"].ToString();
				this.lbltitle.Text = "图纸会审查询";
				return;
			}
			this.Session["MEASURELEVELS"] = "";
			this.lbltitle.Text = "图纸会审资料管理";
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


