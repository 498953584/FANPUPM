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
		public string isFlow = "";

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
				if (base.Request["opType"] != null)
				{
					this.opType = base.Request["opType"].ToString().Trim();
				}
				if (base.Request["isFlow"] != null)
				{
					this.isFlow = base.Request["isFlow"].ToString().Trim();
				}
				this.UProjectList1.UserCode = this.Session["yhdm"].ToString();
				string key;
				switch (key = this.businessType)
				{
				case "PMDept":
					this.UProjectList1.SubPrjUrl = "/OPM/PM/PMOrg/PMDept.aspx?opType=" + this.opType;
					goto IL_C3D;
				case "PMDeptView":
					this.UProjectList1.SubPrjUrl = "/OPM/PM/PMOrg/PMDeptView.aspx?opType=" + this.opType;
					goto IL_C3D;
				case "PMUserInfo":
					this.UProjectList1.SubPrjUrl = "/OPM/PM/PMOrg/PMUserInfo.aspx?opType=" + this.opType;
					goto IL_C3D;
				case "PrjStageList":
					this.UProjectList1.SubPrjUrl = "/OPM/Prj_Stage/PrjStageList.aspx?opType=" + this.opType;
					goto IL_C3D;
				case "PrjStageType":
					this.UProjectList1.SubPrjUrl = "/OPM/Prj_Stage/PrjStageType.aspx?opType=" + this.opType;
					goto IL_C3D;
				case "PrjStageInfo":
					this.UProjectList1.SubPrjUrl = "/OPM/Prj_Stage/PrjStageInfo.aspx?opType=" + this.opType;
					goto IL_C3D;
				case "PrjStageSummary":
					this.UProjectList1.SubPrjUrl = "/OPM/Prj_Stage/PrjStageSummary.aspx?opType=" + this.opType;
					goto IL_C3D;
				case "PhotoSupervise":
					this.UProjectList1.SubPrjUrl = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/PhotosCheckInList2.aspx?opType=" + this.opType + "&pt=1";
					goto IL_C3D;
				case "PhotoSuperviseView":
					this.UProjectList1.SubPrjUrl = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/PhotosCheckInList2.aspx?opType=" + this.opType + "&pt=4";
					goto IL_C3D;
				case "budget_est":
					this.UProjectList1.SubPrjUrl = "/OPM/InvestCtrl/Budget/Budget_Estimate.aspx?bu_Type=0&opType=" + this.opType + "&isFlow=" + this.isFlow;
					goto IL_C3D;
				case "budget_estLock":
					this.UProjectList1.SubPrjUrl = "/OPM/InvestCtrl/Budget/Budget_EstimateLock.aspx?bu_Type=0&opType=" + this.opType + "&isFlow=" + this.isFlow;
					goto IL_C3D;
				case "budget_app":
					this.UProjectList1.SubPrjUrl = "/OPM/InvestCtrl/Budget/Budget_Estimate.aspx?bu_Type=1&opType=" + this.opType + "&isFlow=" + this.isFlow;
					goto IL_C3D;
				case "Estimate":
				{
					string subPrjUrl;
					if (base.Request["isFlow"] != null)
					{
						subPrjUrl = "/OPM/InvestCtrl/Estimate/EstimateView.aspx?bu_Type=0&QX=view&opType=" + this.opType + "&isFlow=";
					}
					else
					{
						subPrjUrl = "/OPM/InvestCtrl/Estimate/EstimateView.aspx?bu_Type=0&QX=view&opType=" + this.opType;
					}
					this.UProjectList1.SubPrjUrl = subPrjUrl;
					goto IL_C3D;
				}
				case "Estimate_edit":
				{
					string subPrjUrl;
					if (base.Request["isFlow"] != null)
					{
						subPrjUrl = "/OPM/InvestCtrl/Estimate/EstimateView.aspx?bu_Type=0&QX=edit&opType=" + this.opType + "&isFlow=";
					}
					else
					{
						subPrjUrl = "/OPM/InvestCtrl/Estimate/EstimateView.aspx?bu_Type=0&QX=edit&opType=" + this.opType;
					}
					this.UProjectList1.SubPrjUrl = subPrjUrl;
					goto IL_C3D;
				}
				case "Completion":
					this.UProjectList1.SubPrjUrl = "/OPM/EPCM/Completion/CompletionList.aspx?opType=" + this.opType;
					goto IL_C3D;
				case "File":
					this.UProjectList1.SubPrjUrl = "/OPM/ComForm/File/ProjectElectronFiles.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "FileSearch":
					this.UProjectList1.SubPrjUrl = "/OPM/ComForm/File/ProjectElectronFilesSearch.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "PutAwayFile":
					this.UProjectList1.SubPrjUrl = "/OPM/E-RecordManage/FileInfoList.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Plan":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Plan/Plan.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "PlanRatify":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Plan/PlanRatify.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Apply":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Modify/Apply.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Ratify":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Modify/Ratify.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "QueryVersion":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Modify/QueryVersion.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "ActualReport":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Actual/ActualReport.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "PlanWarn":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Actual/PlanWarn.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Month":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Track/Month.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Track":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Track/Track.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Milestone":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Track/Milestone.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "AnalysisDetail":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Analysis/AnalysisDetail.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Privilege":
					this.UProjectList1.SubPrjUrl = "/ProgressManagement/Privilege/Privilege.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "ImageProgress":
					this.UProjectList1.SubPrjUrl = "/OPM/Progress_Sum/ImProgressList.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "oneProgress":
					this.UProjectList1.SubPrjUrl = "/OPM/Progress_Sum/ImProgressOneSchedule.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "oneProgress1":
					this.UProjectList1.SubPrjUrl = "/OPM/Progress_Sum/ImProgressOneSchedule.aspx?Type=" + this.opType + "&Action=0";
					goto IL_C3D;
				case "oneProgress2":
					this.UProjectList1.SubPrjUrl = "/OPM/Progress_Sum/ImProgressOneSchedule.aspx?Type=" + this.opType + "&Action=1";
					goto IL_C3D;
				case "oneProgress3":
					this.UProjectList1.SubPrjUrl = "/OPM/Progress_Sum/ImProgressOneSchedule.aspx?Type=" + this.opType + "&Action=2";
					goto IL_C3D;
				case "ImageProgress1":
					this.UProjectList1.SubPrjUrl = "/OPM/Progress_Sum/ImProgressList.aspx?Type=" + this.opType + "&Action=0";
					goto IL_C3D;
				case "ImageProgress2":
					this.UProjectList1.SubPrjUrl = "/OPM/Progress_Sum/ImProgressList.aspx?Type=" + this.opType + "&Action=1";
					goto IL_C3D;
				case "ImageProgress3":
					this.UProjectList1.SubPrjUrl = "/OPM/Progress_Sum/ImProgressList.aspx?Type=" + this.opType + "&Action=2";
					goto IL_C3D;
				case "ImgManage":
					this.UProjectList1.SubPrjUrl = "/OPM/ComForm/BasicCode/DictFrame.aspx?codetype=Img&DictTypeID=212&opType=" + this.opType;
					goto IL_C3D;
				case "ImgMag":
					this.UProjectList1.SubPrjUrl = "/OPM/ComForm/BasicCode/DictFrame.aspx?codetype=Img&DictTypeID=212&opType=" + this.opType + "&Type=1";
					goto IL_C3D;
				case "ImgManageView":
					this.UProjectList1.SubPrjUrl = "/OPM/ComForm/BasicCode/DictFrame.aspx?codetype=Img&DictTypeID=212&opType=" + this.opType;
					goto IL_C3D;
				case "FireManage":
					this.UProjectList1.SubPrjUrl = "/OPM/ComForm/BasicCode/DictFrame.aspx?codetype=Fire&DictTypeID=211&opType=" + this.opType;
					goto IL_C3D;
				case "Maintain":
					this.UProjectList1.SubPrjUrl = "/OPM/OPS/MaintainList.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "MaintainGlance":
					this.UProjectList1.SubPrjUrl = "/OPM/OPS/MaintainGlance.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "MaintainDataList":
					this.UProjectList1.SubPrjUrl = "/OPM/OPS/MaintainData/MaintainDataList.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "PayOutContract":
					this.UProjectList1.SubPrjUrl = "/ContractManage/PayoutContract/PayoutContractList.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Problem":
					this.UProjectList1.SubPrjUrl = "/OPM/EPCM/Problem/ProblemList.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "ProblemGlance":
					this.UProjectList1.SubPrjUrl = "/OPM/EPCM/Problem/ProblemGlance.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "Pro_Sch":
					this.UProjectList1.SubPrjUrl = "/OPM/Info_Summary/Project_Schedule.aspx?Type=" + this.opType;
					goto IL_C3D;
				case "ItemProgList":
					this.UProjectList1.SubPrjUrl = "/EPC/17/PPM/Prog/ItemProgList.aspx?PrjState=0&Levels=0&Type=" + this.opType;
					goto IL_C3D;
				case "Budget":
					this.UProjectList1.SubPrjUrl = "/OPM/FinancialBudget/BudgetPrjInfo.aspx?businessType=Budget&opType=" + this.opType;
					goto IL_C3D;
				case "FinancialBudget":
					this.UProjectList1.SubPrjUrl = "/OPM/FinancialBudget/Budget_List.aspx?businessType=FinancialBudget&opType=" + this.opType;
					goto IL_C3D;
				case "PrjLedger":
					this.UProjectList1.SubPrjUrl = "/OPM/ReportCenter/PrjLedgerList.aspx?businessType=PrjLedger&opType=" + this.opType;
					goto IL_C3D;
				case "PrjMoveBmp":
					this.UProjectList1.SubPrjUrl = "/OPM/PM/PrjInfo/moveBmp2.aspx?PHc=7&Ptype=PrjInfo&opType=" + this.opType;
					goto IL_C3D;
				}
				this.UProjectList1.SubPrjUrl = "";
				IL_C3D:
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


