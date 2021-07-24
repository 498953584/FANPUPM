using ASP;
using com.jwsoft.common.baseclass;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MeasureDataTab : BasePage, IRequiresSessionState
	{
		public static string leaves = "";

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
				MeasureDataTab.leaves = base.Request.QueryString["Levels"].ToString();
				this.Session["MEASURELEVELS"] = base.Request.QueryString["Levels"].ToString();
				this.lblTitle.Text = "资料查询";
				return;
			}
			this.Session["MEASURELEVELS"] = "";
			this.lblTitle.Text = "甲供设备验收资料";
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


