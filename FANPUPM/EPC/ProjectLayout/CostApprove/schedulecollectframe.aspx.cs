using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ScheduleCollectFrame : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.UProjectList1.UserCode = base.UserCode;
				this.UProjectList1.TargetFrame = "FraSch";
				if (base.Request.QueryString["op"].ToString() == "ys")
				{
					this.UProjectList1.SubPrjUrl = "../ProjectLayout/CostApprove/CostCbs.aspx";
					return;
				}
				if (base.Request.QueryString["op"].ToString() == "jh")
				{
					this.UProjectList1.SubPrjUrl = "../ProjectLayout/CostApprove/CostCbsPlan.aspx";
				}
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


