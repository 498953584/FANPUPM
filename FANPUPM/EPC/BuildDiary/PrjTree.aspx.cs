using ASP;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
	public partial class PrjTree : Page, IRequiresSessionState
	{
		public string businessType = "";
		public string opType = "";

		private void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["businessType"] == null || !(base.Request["businessType"].ToString().Trim() != ""))
				{
					this.js.Text = "alert('业务类型不可以为空!');";
					return;
				}
				this.businessType = base.Request["businessType"].ToString().Trim();
				if (base.Request["businessType"] != null)
				{
					this.opType = base.Request["opType"].ToString().Trim();
				}
				this.UProjectList1.UserCode = this.Session["yhdm"].ToString();
				string key;
				switch (key = this.businessType)
				{
				case "PMDept":
					this.UProjectList1.SubPrjUrl = "/OPM/PM/PMOrg/PMDept.aspx?opType=" + this.opType;
					goto IL_2BF;
				case "PMDeptView":
					this.UProjectList1.SubPrjUrl = "/OPM/PM/PMOrg/PMDeptView.aspx?opType=" + this.opType;
					goto IL_2BF;
				case "PMUserInfo":
					this.UProjectList1.SubPrjUrl = "/OPM/PM/PMOrg/PMUserInfo.aspx?opType=" + this.opType;
					goto IL_2BF;
				case "PrjStageList":
					this.UProjectList1.SubPrjUrl = "/OPM/Prj_Stage/PrjStageList.aspx?opType=" + this.opType;
					goto IL_2BF;
				case "PrjStageType":
					this.UProjectList1.SubPrjUrl = "/OPM/Prj_Stage/PrjStageType.aspx?opType=" + this.opType;
					goto IL_2BF;
				case "PrjStageInfo":
					this.UProjectList1.SubPrjUrl = "/OPM/Prj_Stage/PrjStageInfo.aspx?opType=" + this.opType;
					goto IL_2BF;
				case "BuildDiaryList":
					this.UProjectList1.SubPrjUrl = "/EPC/BuildDiary/BuildDiaryList.aspx?opType=" + this.opType;
					goto IL_2BF;
				case "budget_est":
					this.UProjectList1.SubPrjUrl = "/OPM/InvestCtrl/Budget/Budget_Estimate.aspx?bu_Type=0&opType=" + this.opType;
					goto IL_2BF;
				case "budget_app":
					this.UProjectList1.SubPrjUrl = "/OPM/InvestCtrl/Budget/Budget_Estimate.aspx?bu_Type=1&opType=" + this.opType;
					goto IL_2BF;
				}
				this.UProjectList1.SubPrjUrl = "";
				IL_2BF:
				this.UProjectList1.TargetFrame = "fraRight";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new EventHandler(this.Page_Load);
		}
	}


