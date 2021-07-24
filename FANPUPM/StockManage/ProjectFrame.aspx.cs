using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.EasyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_ProjectFrame : NBasePage, IRequiresSessionState
{
	private string path = string.Empty;
	private string businessType = string.Empty;
	private string opType = string.Empty;
	private IList<string> prjStatList = new List<string>();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["path"]))
		{
			this.path = base.Request["path"];
		}
		if (!string.IsNullOrEmpty(base.Request["businessType"]))
		{
			this.businessType = base.Request["businessType"];
		}
		if (!string.IsNullOrEmpty(base.Request["opType"]))
		{
			this.opType = base.Request["opType"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.SetSubUrl();
			this.SetPrjState();
			this.BindPrjYear();
			this.BindPrjTree();
		}
	}
	protected void dropYear_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.SetPrjState();
		this.BindPrjTree();
	}
	protected void btnQuery_Click(object sender, EventArgs e)
	{
		this.SetPrjState();
		this.BindPrjTree();
	}
	private void SetPrjState()
	{
		if (this.hfldStateType.Value == "1")
		{
			this.prjStatList = Parameters.PrjAvaildState;
		}
		if (this.hfldStateType.Value == "2")
		{
			this.prjStatList = Parameters.PrjAvaildState2;
		}
		if (this.hfldStateType.Value == "3")
		{
			this.prjStatList = Parameters.PrjAvaildState3;
		}
		if (this.hfldStateType.Value == "4")
		{
			this.prjStatList = Parameters.PrjAvaildState4;
		}
		if (this.hfldStateType.Value == "5")
		{
			this.prjStatList = Parameters.PrjAvaildState5;
		}
	}
	private void BindPrjYear()
	{
		ProjectTree2.BindPrjYear(this.dropYear, base.UserCode, this.prjStatList);
		if (this.hfldContainsOrg.Value == "1")
		{
			this.dropYear.Items.Add(new ListItem("组织机构", "zzjg"));
		}
	}
	private void BindPrjTree()
	{
		string showType = this.Session["PmSet"].ToString();
		string text = this.txtPjr.Text.Trim();
		string pname = text;
		string value = string.Empty;
		if (this.dropYear.SelectedValue == "zzjg")
		{
			cn.justwin.Domain.EasyUI.Department department = new cn.justwin.Domain.EasyUI.Department();
			IList<cn.justwin.Domain.EasyUI.Department> department2 = department.GetDepartment(10);
			value = JsonHelper.JsonSerializer<cn.justwin.Domain.EasyUI.Department[]>(department2.ToArray<cn.justwin.Domain.EasyUI.Department>());
		}
		else
		{
			int year = Convert.ToInt32(this.dropYear.SelectedValue);
			IList<Project> onelevelPrj = ProjectTree2.GetOnelevelPrj(year, base.UserCode, showType, this.prjStatList, text, pname);
			value = JsonHelper.JsonSerializer<Project[]>(onelevelPrj.ToArray<Project>());
		}
		this.hfldProjectJson.Value = value;
	}
	private void SetSubUrl()
	{
		if (this.path == "wantPlan")
		{
			this.hfldSubUrl.Value = "/StockManage/basicset/SmWantPlanList.aspx?prjId=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "purchasePlan")
		{
			this.hfldSubUrl.Value = "/StockManage/SmPurchaseplan/SmPurchaseplanList.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "selectWantPlan")
		{
			this.hfldSubUrl.Value = "/StockManage/SmPurchaseplan/SelectByNoteList.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "purchase")
		{
			this.hfldSubUrl.Value = "/StockManage/Purchase/Purchase.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "sendNoteList")
		{
			this.hfldSubUrl.Value = "/StockManage/sendGoods/SendNoteList.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "receiveNoteList")
		{
			this.hfldSubUrl.Value = "/StockManage/receiveGoods/ReceiveNoteList.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "reciveEditList")
		{
			this.hfldSubUrl.Value = "/StockManage/receiveGoods/ReciveEditList.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "delReceiveNote")
		{
			this.hfldSubUrl.Value = "/StockManage/receiveGoods/DelReceiveNote.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "outReserve")
		{
			this.hfldSubUrl.Value = "/StockManage/SmOutReserve/SmOutReserveList.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "qOutReserve")
		{
			this.hfldSubUrl.Value = "/StockManage/SmOutReserve/QOutReserve.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "refundingList")
		{
			this.hfldSubUrl.Value = "/StockManage/Refunding/RefundingList.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "qoRefundingList")
		{
			this.hfldSubUrl.Value = "/StockManage/Refunding/QoRefundingList.aspx?PrjGuid=";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.businessType == "ImgManage")
		{
			this.hfldSubUrl.Value = "/OPM/ComForm/BasicCode/DictFrame.aspx?codetype=Img&opType= + " + this.opType + "&pc=";
			return;
		}
		if (this.businessType == "ImgMag")
		{
			this.hfldSubUrl.Value = "/OPM/ComForm/BasicCode/DictFrame.aspx?codetype=Img&opType= + " + this.opType + "&pc=";
			return;
		}
		if (this.businessType == "ImgManageView")
		{
			this.hfldSubUrl.Value = "/OPM/ComForm/BasicCode/DictFrame.aspx?codetype=Img&opType= + " + this.opType + "&pc=";
			return;
		}
		if (this.path == "contractTask")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/ContractTask.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "contractTaskList")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/ContractList.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "contractTaskQuery")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/ContractTaskQuery.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "contractTaskRpt")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Construct/ContractReport.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "contractTaskModify")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/BudConModifyList.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "budgetPlait")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/BudgetPlaitList.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "budgetModify")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/BudModifyList.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "resDeploy")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/ResourceDeploy.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "resMapping")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/ResourceMapping.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "budgetQuery")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Budget/CheckBudget.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planPlait")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Plan/Plan.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planAduit")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Plan/PlanRatify.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planModifyApply")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Modify/Apply.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planModifyAudit")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Modify/Ratify.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planVersionQuery")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Modify/QueryVersion.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planActualRpt")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Actual/ActualReport.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planWarn")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Actual/PlanWarn.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planMonth")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Track/Month.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planTrack")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Track/Track.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planMilestone")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Track/Milestone.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planChartDetail")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Analysis/ChartDetail.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "planPriv")
		{
			this.hfldSubUrl.Value = "/ProgressManagement/Privilege/Privilege.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "stopMsg")
		{
			this.hfldSubUrl.Value = "/StartStopReturnWork/StopMsgList.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldStateType.Value = "3";
			return;
		}
		if (this.path == "retMsg")
		{
			this.hfldSubUrl.Value = "/StartStopReturnWork/RetMsgList.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldStateType.Value = "3";
			return;
		}
		if (this.path == "queryRpt")
		{
			this.hfldSubUrl.Value = "/StartStopReturnWork/QueryReportList.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldStateType.Value = "4";
			return;
		}
		if (this.path == "prjMemberQuery")
		{
			this.hfldSubUrl.Value = "/PrjManager/PrjTreeMemberQuery.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldStateType.Value = "2";
			return;
		}
		if (this.path == "fundPlanIncome")
		{
			this.hfldSubUrl.Value = "/Fund/MonthPlan/FundMonthPlan.aspx?plantype=income&year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "fundPlanPayout")
		{
			this.hfldSubUrl.Value = "/Fund/MonthPlan/FundMonthPlan.aspx?plantype=payout&year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "indirectBudget")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Cost/IndirectBudget.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldContainsOrg.Value = "1";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "indirectMonthBudget")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Cost/IndirectMonthBudget.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldContainsOrg.Value = "1";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "costBudgetApply")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Cost/CostBudgetApply.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldContainsOrg.Value = "1";
			this.hfldStateType.Value = "5";
		}
		if (this.path == "costBudgetModify")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Cost/CostPreReimburseModify.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldContainsOrg.Value = "1";
			this.hfldStateType.Value = "5";
		}
		if (this.path == "costDiary")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Cost/CostDiary.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldContainsOrg.Value = "1";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "costDiaryQuery")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Cost/CostDiaryQuery.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldContainsOrg.Value = "1";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "costDiaryQueryOneself")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Cost/CostDiaryQueryOneself.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldContainsOrg.Value = "1";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "indirectCostAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/IndirectCostAnalysis.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			this.hfldContainsOrg.Value = "1";
			this.hfldStateType.Value = "5";
			return;
		}
		if (this.path == "constructReport")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Construct/ConstructReport.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "checkConstructReport")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Construct/CheckConstructReport.aspx?ear=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "checkConstructRes")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Construct/CheckConstructRes.aspx?year=" + this.dropYear.SelectedValue + "&prjId=";
			return;
		}
		if (this.path == "rptBudgetDetail")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/BudgetDetail.aspx?prjId=";
			return;
		}
		if (this.path == "rptEvenAnalysisDatail")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/EvenAnalysisDetail.aspx?prjId=";
			return;
		}
		if (this.path == "rptCostRptDetail")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/CostReportDetail.aspx?prjId=";
			return;
		}
		if (this.path == "rptSubPrjCmp")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/SubProjectCompare.aspx?prjId=";
			return;
		}
		if (this.path == "rptSubPrjDffCmp")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/SubProjectDiffCompare.aspx?prjId=";
			return;
		}
		if (this.path == "rptJixieAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/JixieAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptJixieDetail")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/JixieDetail.aspx?prjId=";
			return;
		}
		if (this.path == "rptLaborAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/LaborAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptMajorStuffAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/MajorStuffAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptMajorStuffDiffAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/MajorStuffDiffAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptMinorStuffAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/MinorStuffAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptCBSCost")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/CBSCost.aspx?prjId=";
			return;
		}
		if (this.path == "rptLaborCostAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/LaborCostAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptLaborDetailAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/LaboreDetailAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptStuffDetailAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/StuffDetailAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptMachineDetailAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/MachineDetailAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptLSMDetailAnalysis")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/LSMDetailAnalysis.aspx?prjId=";
			return;
		}
		if (this.path == "rptPrjSummary")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Construct/PrjSummary.aspx?prjId=";
			return;
		}
		if (this.path == "rptBudGetOutPut")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/BudGetOutPutReport.aspx?prjId=";
			return;
		}
		if (this.path == "rptResPriceDiff")
		{
			this.hfldSubUrl.Value = "/BudgetManage/Report/ResPriceDifference.aspx?prjId=";
			return;
		}
		if (this.path == "qsAffairEdit")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/AffairList.aspx?Flag=Q&Type=Edit&TypeId=4&PrjState=0";
			return;
		}
		if (this.path == "qsAffairList")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/AffairList.aspx?Flag=Q&Type=List&TypeId=4&PrjState=0";
			return;
		}
		if (this.path == "qsGoalEdit")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/QualityGoal/GoalList.aspx?Type=Edit&PrjState=0";
			return;
		}
		if (this.path == "qsGoalList")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/QualityGoal/GoalList.aspx?Type=List&PrjState=0";
			return;
		}
		if (this.path == "qsFileUpload")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/FileUpload.aspx?Type=List&PrjState=0&TypeId=6&Flag=Q";
			return;
		}
		if (this.path == "qsPrjSuperviseEditCA2")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=Q&Type=Edit&TypeId=6&CA=2&PrjState=0";
			return;
		}
		if (this.path == "qsPrjSuperviseListCA2")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=Q&Type=List&TypeId=6&CA=2&PrjState=0";
			return;
		}
		if (this.path == "qsPrjSuperviseEditCA1")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=Q&Type=Edit&TypeId=6&CA=1&PrjState=0";
			return;
		}
		if (this.path == "qsPrjSuperviseListCA1")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=Q&Type=List&TypeId=6&CA=1&PrjState=0";
			return;
		}
		if (this.path == "qsPrjSuperviseEditCA3")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=Q&Type=Edit&TypeId=6&CA=3&PrjState=0";
			return;
		}
		if (this.path == "qsPrjSuperviseListCA3")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=Q&Type=List&TypeId=6&CA=3&PrjState=0";
			return;
		}
		if (this.path == "qsHighlightsEditTypeID8")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/QualityHighlights/HighlightsList.aspx?Type=Edit&TypeId=8&PrjState=0";
			return;
		}
		if (this.path == "qsHighLightsListTypeID8")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/QualityHighlights/HighlightsList.aspx?Type=List&TypeId=8&PrjState=0";
			return;
		}
		if (this.path == "qsHighlightsEditTypeID9")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/QualityHighlights/HighlightsList.aspx?Type=Edit&TypeId=9&PrjState=0";
			return;
		}
		if (this.path == "qsHighLightsListTypeID9")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/QualityHighlights/HighlightsList.aspx?Type=List&TypeId=9&PrjState=0";
			return;
		}
		if (this.path == "qsPrjFrameEdit")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/PhotosCheckInList2.aspx?opType=edit&pt=1&PrjGuid=";
			return;
		}
		if (this.path == "qsPrjFrameView")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/PhotosCheckInList2.aspx?opType=edit&pt=4&PrjGuid=";
			return;
		}
		if (this.path == "qssAffairEdit")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/AffairList.aspx?Flag=S&Type=Edit&TypeId=5&PrjState=0";
			return;
		}
		if (this.path == "qssAffairList")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/AffairList.aspx?Flag=S&Type=List&TypeId=5&PrjState=0";
			return;
		}
		if (this.path == "qssPrjSuperivseEdit")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=S&Type=Edit&TypeId=6&CA=6&PrjState=0";
			return;
		}
		if (this.path == "qssPrjSuperivseList")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=S&Type=List&TypeId=6&CA=6&PrjState=0";
			return;
		}
		if (this.path == "qssFileUpload")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/FileUpload.aspx?Type=List&PrjState=0&TypeId=6&Flag=S";
			return;
		}
		if (this.path == "qssMeasureEdit")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/SafetyMeasure/MeasureList.aspx?Type=Edit&PrjState=0";
			return;
		}
		if (this.path == "qssMeasureList")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/SafetyMeasure/MeasureList.aspx?Type=List&PrjState=0";
			return;
		}
		if (this.path == "qssPrjSuperivseEditDitCA4")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=S&Type=Edit&TypeId=6&CA=4&PrjState=0";
			return;
		}
		if (this.path == "qssPrjSuperivseListDitCA4")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=S&Type=List&TypeId=6&CA=4&PrjState=0";
			return;
		}
		if (this.path == "qssPrjSuperivseEditDitCA5")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=S&Type=Edit&TypeId=6&CA=5&PrjState=0";
			return;
		}
		if (this.path == "qssPrjSuperivseListDitCA5")
		{
			this.hfldSubUrl.Value = "/EPC/QuaitySafety/ProjectSupervise.aspx?Flag=S&Type=List&TypeId=6&CA=5&PrjState=0";
			return;
		}
		if (this.path == "siBuildDiaryEdit")
		{
			this.hfldSubUrl.Value = "/EPC/BuildDiary/BuildDiaryList.aspx?businessType=BuildDiaryList&opType=edit&PrjGuid=";
			return;
		}
		if (this.path == "siBuildDiaryView")
		{
			this.hfldSubUrl.Value = "/EPC/BuildDiary/BuildDiaryList.aspx?businessType=BuildDiaryList&opType=view&PrjGuid=";
			return;
		}
		if (this.path == "siConstructionOrg")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ConstructOrganizeQuery.aspx?Type=Edit&PrjState=0&Levels=";
			return;
		}
		if (this.path == "siConstructionOrgView")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ConstructOrganizeQuery.aspx?Type=Edit&PrjState=0&Levels=1";
			return;
		}
		if (this.path == "siExpertPrj")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ExpertProjectQuery.aspx?Type=Edit&PrjState=0";
			return;
		}
		if (this.path == "siExpertPrjView")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ExpertProjectQuery.aspx?Type=Edit&PrjState=0&Levels=1";
			return;
		}
		if (this.path == "siTechnology")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/TechnologyStandardQuery.aspx?Type=Edit&PrjState=0&Levels=";
			return;
		}
		if (this.path == "siTechnologyAudit")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/TechnologyStandardQuery.aspx?Type=Auditing&PrjState=0&Levels=";
			return;
		}
		if (this.path == "siTechnologyView")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/TechnologyStandardQuery.aspx?Type=Edit&PrjState=0&Levels=1";
			return;
		}
		if (this.path == "siPrjLinkEditLevels4")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ProjectLinkQuery.aspx?Type=Edit&PrjState=0&Levels=4&PrjCode=";
			return;
		}
		if (this.path == "siPrjLinkViewLevels4")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ProjectLinkQuery.aspx?Type=View&PrjState=0&Levels=4&PrjCode=";
			return;
		}
		if (this.path == "siTechnologyJd")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/TechnologyJDQuery.aspx?Type=Edit&PrjState=0";
			return;
		}
		if (this.path == "siTechnologyJdView")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/TechnologyJDQuery.aspx?Type=Edit&PrjState=0&Levels=1";
			return;
		}
		if (this.path == "siDrawCheckUp")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/DrawCheckUpTabPage.aspx?Type=Edit&PrjState=0&Levels=&PrjCode=";
			return;
		}
		if (this.path == "siDrawCheckUpView")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/DrawCheckUpTabPage.aspx?Type=Edit&PrjState=0&Levels=1&PrjCode=";
			return;
		}
		if (this.path == "siPrjLinkEditLevels5")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ProjectLinkQuery.aspx?Type=Edit&PrjState=0&Levels=5&PrjCode=";
			return;
		}
		if (this.path == "siPrjLinkViewLevels5")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ProjectLinkQuery.aspx?Type=View&PrjState=0&Levels=5&PrjCode=";
			return;
		}
		if (this.path == "siPrjLinkEditLevels3")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ProjectLinkQuery.aspx?Type=Edit&PrjState=0&Levels=3&PrjCode=";
			return;
		}
		if (this.path == "siPrjLinkViewLevels3")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ProjectLinkQuery.aspx?Type=View&PrjState=0&Levels=3&PrjCode=";
			return;
		}
		if (this.path == "siEngineerConfirmEditLevels7")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/EngineerConfirmList.aspx?Type=Edit&PrjState=0&Levels=7&PrjCode=";
			return;
		}
		if (this.path == "siEngineerConfirmViewLevels7")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/EngineerConfirmList.aspx?Type=View&PrjState=0&Levels=7&PrjCode=";
			return;
		}
		if (this.path == "siEngineerConfirmEditLevels8")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/EngineerConfirmList.aspx?Type=Edit&PrjState=0&Levels=8&PrjCode=";
			return;
		}
		if (this.path == "siEngineerConfirmViewLevels8")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/EngineerConfirmList.aspx?Type=View&PrjState=0&Levels=8&PrjCode=";
			return;
		}
		if (this.path == "siMeasureDataTab")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/MeasureDataTab.aspx?Type=Edit&PrjState=0&Levels=&PrjCode=";
			return;
		}
		if (this.path == "siMeasureDataTabView")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/MeasureDataTab.aspx?Type=Edit&PrjState=0&Levels=1&PrjCode=";
			return;
		}
		if (this.path == "siFinishTab")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/TechnologyFinishTab.aspx?Type=Edit&PrjState=0&Levels=&PrjCode=";
			return;
		}
		if (this.path == "siFinishTabView")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/TechnologyFinishTab.aspx?Type=Edit&PrjState=0&Levels=1&PrjCode=";
			return;
		}
		if (this.path == "siSumEdit")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSum.aspx?Type=Edit&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "siSumList")
		{
			this.hfldSubUrl.Value = "/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSum.aspx?Type=List&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "siPlanEdit")
		{
			this.hfldSubUrl.Value = "/EPC/17/common/ScienceInnovate/ProgressPlanList.aspx?Type=Edit&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "siPlanList")
		{
			this.hfldSubUrl.Value = "/EPC/17/common/ScienceInnovate/ProgressPlanList.aspx?Type=List&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "siPlanAduit")
		{
			this.hfldSubUrl.Value = "/EPC/17/common/ScienceInnovate/ProgressPlanList.aspx?Type=Auditing&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "siPlanView")
		{
			this.hfldSubUrl.Value = "/EPC/17/common/ScienceInnovate/ProgressPlanList.aspx?Type=View&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "siImplEdit")
		{
			this.hfldSubUrl.Value = "/EPC/17/common/ScienceInnovate/ProgressImplementMain.aspx?Type=Edit&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "siImplAudit")
		{
			this.hfldSubUrl.Value = "/EPC/17/common/ScienceInnovate/ProgressImplementMain.aspx?Type=Auditing&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "poPrjElecFileEdit")
		{
			this.hfldSubUrl.Value = "/EPC/ProjectOver/ProjectElectronFiles.aspx?Type=Edit&PrjCode=";
			return;
		}
		if (this.path == "poPrjElecFileList")
		{
			this.hfldSubUrl.Value = "/EPC/ProjectOver/ProjectElectronFilesSearch.aspx?Type=List&PrjCode=";
			return;
		}
		if (this.path == "poProjDoc")
		{
			this.hfldSubUrl.Value = "/EPC/ProjectOver/DocumentList.aspx";
			return;
		}
		if (this.path == "poProjDocSearch")
		{
			this.hfldSubUrl.Value = "/EPC/ProjectOver/DocumentListView.aspx";
			return;
		}
		if (this.path == "poCheckEdit")
		{
			this.hfldSubUrl.Value = "/EPC/17/Entpm/PrjCheck/CheckList.aspx?Type=Edit&PrjState=0&Levels=0&n=1&PrjCode=";
			return;
		}
		if (this.path == "poCheckRectify")
		{
			this.hfldSubUrl.Value = "/EPC/17/Entpm/PrjCheck/CheckList.aspx?Type=Rectify&PrjState=0&Levels=0&n=2&PrjCode=";
			return;
		}
		if (this.path == "poCheckCertify")
		{
			this.hfldSubUrl.Value = "/EPC/17/Entpm/PrjCheck/CheckList.aspx?Type=Certify&PrjState=0&Levels=0&n=3&PrjCode=";
			return;
		}
		if (this.path == "poCheckAudit")
		{
			this.hfldSubUrl.Value = "/EPC/17/Entpm/PrjCheck/CheckList.aspx?Type=ShenHe&PrjState=0&Levels=0&PrjCode=";
			return;
		}
		if (this.path == "poCheckList")
		{
			this.hfldSubUrl.Value = "/EPC/17/Entpm/PrjCheck/CheckList.aspx?Type=List&PrjState=0&Levels=0&PrjCode=";
			return;
		}
		if (this.path == "poSuperviseEdit")
		{
			this.hfldSubUrl.Value = "/EPC/17/Entpm/PrjCheck/SuperviseList.aspx?Type=Edit&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "poSuperviseAudit")
		{
			this.hfldSubUrl.Value = "/EPC/17/Entpm/PrjCheck/SuperviseList.aspx?Type=ShenHe&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "poSuperviseList")
		{
			this.hfldSubUrl.Value = "/EPC/17/Entpm/PrjCheck/SuperviseList.aspx?Type=List&PrjState=0&PrjCode=";
			return;
		}
		if (this.path == "poItemProgEdit")
		{
			this.hfldSubUrl.Value = "/EPC/17/PPM/Prog/ItemProgList.aspx?Type=Edit&PrjState=0&Levels=0&PrjCode=";
			return;
		}
		if (this.path == "poItemProgList")
		{
			this.hfldSubUrl.Value = "/EPC/17/PPM/Prog/ItemProgList.aspx?Type=List&PrjState=0&Levels=0&PrjCode=";
			return;
		}
		if (this.path == "poItemProgView")
		{
			this.hfldSubUrl.Value = "/EPC/17/PPM/Prog/ItemProgList.aspx?Type=View&PrjState=0&Levels=0&PrjCode=";
			return;
		}
		if (this.path == "requirePlanRpt")
		{
			this.hfldSubUrl.Value = "/Equ/Report/RequirePlanReport.aspx";
			return;
		}
		if (this.path == "BudgetCompletedList")
		{
			this.hfldSubUrl.Value = "/ContractManage/IncometContract/BudgetCompletedList.aspx?PrjCode=";
			return;
		}
		if (this.path == "BMonthCompletedStatistics")
		{
			this.hfldSubUrl.Value = "/ContractManage/ContractReport/BMonthCompletedStatistics.aspx?PrjCode=";
			return;
		}
		if (this.path == "BTaskReport")
		{
			this.hfldSubUrl.Value = "/ContractManage/ContractReport/BTaskReport.aspx?PrjCode=";
		}
		if (this.path == "BMonthTaskStatistics")
		{
			this.hfldSubUrl.Value = "/ContractManage/ContractReport/BMonthTaskStatistics.aspx?PrjCode=";
		}
	}
}


