using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ScheduleComplete : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UProjectList1.UserCode = base.UserCode;
				if (base.Request.QueryString["SeeType"] != null && base.Request.QueryString["SeeType"] == "1")
				{
					this.UProjectList1.SubPrjUrl = "../ConstructSchedule/ScheduleCompleteQueryInfo.aspx";
				}
				else
				{
					this.UProjectList1.SubPrjUrl = "../ConstructSchedule/ScheduleCompleteQuery.aspx";
				}
				this.UProjectList1.TargetFrame = "InfoList";
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


