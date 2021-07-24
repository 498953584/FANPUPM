using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Project;
using cn.justwin.Tender;
using cn.justwin.Warn;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_InfoView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["ic"]))
		{
			this.BindLbl(base.Request["ic"].ToString());
			Warning.Remove(base.Request["ic"], base.UserCode);
		}
	}
	public string FilesBind(string recordCode)
	{
		string text = ConfigHelper.Get("ProjectFile");
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(base.Server.MapPath(text) + recordCode);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + recordCode;
				string str2 = str + "/" + text3;
				text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>"
				});
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
			}
			string text4 = string.Empty;
			if (stringBuilder.Length > 2)
			{
				text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
			}
			result = text4;
		}
		catch
		{
			result = "";
		}
		return result;
	}
	private string GetXpmName(int codeId)
	{
		XpmCodeServices xpmCodeServices = new XpmCodeServices();
		System.Collections.Generic.IList<XpmCode> bySignCode = xpmCodeServices.GetBySignCode("TenderAppraiseMethod");
		string result = string.Empty;
		foreach (XpmCode current in bySignCode)
		{
			if (current.CodeID == codeId)
			{
				result = current.CodeName;
			}
		}
		return result;
	}
	public string ReplaceTxt(string str)
	{
		if (!string.IsNullOrEmpty(str))
		{
			str = str.Replace(" ", "&nbsp;&nbsp;");
			str = str.Replace("\r\n", "<br/>");
		}
		return str;
	}
	private void BindLbl(string PrjId)
	{
		TenderInfo byId = TenderInfo.GetById(PrjId);
		string state = byId.PrjState.ToString();
		this.lblOwner.Text = byId.OwnerName;
		this.lblOwnerLinkMan.Text = byId.OwnerLinkManName;
		this.lblOwnerLinkManPhone.Text = byId.OwnerLinkPhone;
		this.lblOwnerAddress.Text = byId.OwnerAddress;
		this.lblCorp.Text = byId.ProjPeopleCorp;
		this.lblPhone.Text = byId.ProjPeopleTel;
		this.lblDuty.Text = byId.ProjPeopleDuty;
		this.lblName.Text = byId.ProjPeopleUserName;
		this.lblForecastProfitRate.Text = byId.ForecastProfitRate.ToString();
		this.lblEngineeringEstimates.Text = byId.EngineeringEstimates;
		this.lblApprovalOf.Text = byId.PrjApprovalOf;
		this.lblManagementMargin.Text = byId.ManagementMargin.ToString();
		this.lblMigrantQualityMarginRate.Text = byId.MigrantQualityMarginRate.ToString();
		this.lblWithholdingTaxRate.Text = byId.WithholdingTaxRate.ToString();
		this.lblPerformanceBond.Text = byId.PerformanceBond.ToString();
		this.lblElseMargin.Text = byId.ElseMargin.ToString();
		this.lblFundWorkable.Text = byId.PrjFundWorkable;
		this.lblPrjState.Text = TypeList.GetTypeName(byId.PrjProperty, "1", "ProjectProperty");
		this.lblBllProducer.Text = byId.GetUserName(base.UserCode);
		this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
		if (!string.IsNullOrEmpty(byId.Province))
		{
			this.lblArea.Text = byId.Province + byId.City;
		}
		this.lblBudgetWay.Text = TypeList.GetTypeName(byId.BudgetWay, "1", "ysType");
		this.lblBuildingArea.Text = byId.BuildingArea;
		if (byId.EngineeringTypes != null)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			foreach (EngineeringType current in byId.EngineeringTypes)
			{
				stringBuilder.Append(current.ToString()).Append("<br />");
			}
			this.lblBuildingType.Text = stringBuilder.ToString();
		}
		this.lblContractWay.Text = TypeList.GetTypeName(byId.ContractWay, "1", "cbType");
		this.lblCounsellor.Text = byId.Counsellor;
		this.lblDesigner.Text = byId.Designer;
		this.lblDuration.Text = byId.Duration;
		this.lblElseRequest.Text = this.ReplaceTxt(byId.ProjElseRequest);
		this.lblEndDate.Text = Common2.GetTime(byId.EndDate.ToString());
		if (byId.BuildingTypeNo.ToString() != "0")
		{
			this.lblgrade.Text = byId.BuildingTypeNo.ToString();
		}
		this.lblInfoOrigin.Text = this.ReplaceTxt(byId.ProjInfoOrigin);
		this.lblInspector.Text = byId.Inspector;
		this.lblKeyPart.Text = TypeList.GetTypeName(byId.KeyPart, "1", "primaryGrade");
		this.lblOtherStatement.Text = this.ReplaceTxt(byId.OtherStatement);
		this.lblPayCondition.Text = TypeList.GetTypeName(byId.PayCondition, "1", "payment");
		this.lblPayWay.Text = TypeList.GetTypeName(byId.PayWay, "1", "jsType");
		this.lblPhone.Text = byId.ProjPeopleTel;
		this.lblPrjCode.Text = byId.PrjCode;
		if (byId.PrjCost.ToString() != "" && byId.PrjCost.ToString() != "0")
		{
			this.lblPrjCost.Text = byId.PrjCost.ToString();
		}
		this.lblPrjFundInfo.Text = byId.PrjFundInfo;
		this.lblPrjInfo.Text = this.ReplaceTxt(byId.PrjInfo);
		this.lblDutyPerson.Text = byId.PrjDutyPersonName;
		if (byId.PrjKinds != null)
		{
			System.Text.StringBuilder stringBuilder2 = new System.Text.StringBuilder();
			System.Text.StringBuilder stringBuilder3 = new System.Text.StringBuilder();
			foreach (ProjectKind current2 in byId.PrjKinds)
			{
				stringBuilder2.Append(current2.ProfessionalCost.ToString());
				stringBuilder3.Append(TypeList.GetTypeName(current2.PrjKind, "1", "ProjectType"));
			}
			this.lblPrjProfessionalCost.Text = stringBuilder2.ToString();
			this.lblPrjKindClass.Text = stringBuilder3.ToString();
		}
		this.lblPrjManager.Text = byId.PrjManagerName;
		this.lblPrjName.Text = byId.PrjName;
		this.lblPrjPlace.Text = byId.PrjPlace;
		this.lblQualityClass.Text = TypeList.GetTypeName(byId.QualityClass, "1", "ProjectQuality");
		if (byId.PrjRanks != null)
		{
			System.Text.StringBuilder stringBuilder4 = new System.Text.StringBuilder();
			foreach (ProjectRank current3 in byId.PrjRanks)
			{
				stringBuilder4.Append(TypeList.GetTypeName(current3.Rank, "1", "zzGrade") + " " + current3.RankLevel).Append("<br />");
			}
			this.lblRank.Text = stringBuilder4.ToString();
		}
		this.lblRemark1.Text = this.ReplaceTxt(byId.Remark);
		this.lblStartDate.Text = Common2.GetTime(byId.StartDate.ToString());
		this.lblTelphone.Text = byId.Telephone;
		this.lblTenderWay.Text = TypeList.GetTypeName(byId.TenderWay, "1", "zbType");
		this.lblTotalHouseNum.Text = byId.TotalHouseNum;
		this.lblUndergroundArea.Text = byId.UndergroundArea;
		this.lblUsegrounArea.Text = byId.UsegrounArea;
		this.lblAfforestArea.Text = byId.AfforestArea;
		this.lblParkArea.Text = byId.ParkArea;
		this.lblPrjReadOne.Text = WebUtil.GetUserNames(byId.PrjReadOne);
		if (byId.WorkUnit != "")
		{
			this.lblWorkUnit.Text = byId.WorkUnitName;
		}
		this.upload.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.PreApproval);
		this.lblPrjManagerRequire.Text = byId.PrjManagerRequire;
		this.lblTechnicalLeaderRequire.Text = byId.TechnicalLeaderRequire;
		this.showInfo(byId, state, PrjId);
		PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
		System.Guid id = new System.Guid(PrjId);
		PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(id);
		if (byId2 != null && byId2.PrjState == System.Convert.ToInt32(ProjectParameter.GiveUpState))
		{
			this.tr_fq.Visible = true;
			if (!byId2.GiveUpTime.HasValue)
			{
				this.lbGiveUpTime.Text = string.Empty;
			}
			else
			{
				this.lbGiveUpTime.Text = Common2.GetTime(byId2.GiveUpTime);
			}
			this.lbOperator.Text = WebUtil.GetUserNames(byId2.Operator);
			this.lbGiveUpReason.Text = byId2.GiveUpReason;
			this.lbGiveUpNote.Text = byId2.GiveUpNote;
			string state2 = byId2.OldState.ToString();
			this.showInfo(byId, state2, PrjId);
			this.trAudit_giveUp.Visible = true;
			this.file_GiveUp.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.GiveUpState);
		}
	}
	private void showInfo(TenderInfo tender, string State, string PrjId)
	{
		if (State == ProjectParameter.Initiate)
		{
			this.tr_qd.Visible = true;
			this.trAudit.Visible = true;
			this.trAudit_initate.Visible = true;
			this.lblProjStartDate.Text = Common2.GetTime(tender.ProjStartDate.ToString());
			this.lblProjApplyDate.Text = Common2.GetTime(tender.ProjApplyDate.ToString());
			this.lblStartRemark.Text = this.ReplaceTxt(tender.ProjStartRemark);
			this.file_qd.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Initiate);
		}
		if (State == ProjectParameter.InitiateFail)
		{
			this.tr_bt.Visible = true;
			this.trAudit_initate.Visible = true;
			this.lblProjStartDate1.Text = Common2.GetTime(tender.ProjStartDate.ToString());
			this.lblProjApplyDate1.Text = Common2.GetTime(tender.ProjApplyDate.ToString());
			this.lblStartRemark1.Text = this.ReplaceTxt(tender.ProjStartRemark);
			this.file_qd1.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.InitiateFail);
		}
		if (State == ProjectParameter.Prequalification)
		{
			this.tr_qd.Visible = true;
			this.tr_ys.Visible = true;
			this.trAudit_initate.Visible = true;
			this.lblProjStartDate.Text = Common2.GetTime(tender.ProjStartDate.ToString());
			this.lblStartRemark.Text = this.ReplaceTxt(tender.ProjStartRemark);
			this.lblApplyDate.Text = Common2.GetTime(tender.ProjApplyDate.ToString());
			if (tender.ProgAgent != "")
			{
				this.lblAgent.Text = tender.ProgAgentName;
			}
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblApprovalDate.Text = Common2.GetTime(tender.ProjApprovalDate.ToString());
			this.lblQualificationMargin.Text = tender.QualificationMargin.ToString();
			if (tender.ProjRegistDeadline.ToString() != "" && tender.ProjRegistDeadline.ToString() != "0")
			{
				this.lblRegistDeadline.Text = tender.ProjRegistDeadline.ToString() + "天";
			}
			this.lblPrequalificationRequire.Text = tender.PrequalificationRequire;
			this.file_qd.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Initiate);
			this.file_ys.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Prequalification);
		}
		if (State == ProjectParameter.QualificationPass)
		{
			this.tr_qd.Visible = true;
			this.tr_ys.Visible = true;
			this.tr_pass.Visible = true;
			this.trAudit_pft.Visible = true;
			this.trAudit_initate.Visible = true;
			this.lblProjStartDate.Text = Common2.GetTime(tender.ProjStartDate.ToString());
			this.lblStartRemark.Text = this.ReplaceTxt(tender.ProjStartRemark);
			this.lblApplyDate.Text = Common2.GetTime(tender.ProjApplyDate.ToString());
			if (tender.ProgAgent != "")
			{
				this.lblAgent.Text = tender.ProgAgentName;
			}
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblApprovalDate.Text = Common2.GetTime(tender.ProjApprovalDate.ToString());
			this.lblQualificationMargin.Text = tender.QualificationMargin.ToString();
			if (tender.ProjRegistDeadline.ToString() != "" && tender.ProjRegistDeadline.ToString() != "0")
			{
				this.lblRegistDeadline.Text = tender.ProjRegistDeadline.ToString() + "天";
			}
			this.lblPrequalificationRequire.Text = tender.PrequalificationRequire;
			this.lblPassDate.Text = Common2.GetTime(tender.QualificationPassDate.ToString());
			this.lblPassReason.Text = tender.QualificationPassReason;
			this.file_qd.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Initiate);
			this.file_ys.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Prequalification);
			this.file_pass.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.QualificationPass);
		}
		if (State == ProjectParameter.QualificationFail)
		{
			this.tr_qd.Visible = true;
			this.tr_ys.Visible = true;
			this.tr_fail.Visible = true;
			this.trAudit_pft.Visible = true;
			this.trAudit_initate.Visible = true;
			this.lblProjStartDate.Text = Common2.GetTime(tender.ProjStartDate.ToString());
			this.lblStartRemark.Text = this.ReplaceTxt(tender.ProjStartRemark);
			this.lblApplyDate.Text = Common2.GetTime(tender.ProjApplyDate.ToString());
			if (tender.ProgAgent != "")
			{
				this.lblAgent.Text = tender.ProgAgentName;
			}
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblApprovalDate.Text = Common2.GetTime(tender.ProjApprovalDate.ToString());
			this.lblQualificationMargin.Text = tender.QualificationMargin.ToString();
			if (tender.ProjRegistDeadline.ToString() != "" && tender.ProjRegistDeadline.ToString() != "0")
			{
				this.lblRegistDeadline.Text = tender.ProjRegistDeadline.ToString() + "天";
			}
			this.lblPrequalificationRequire.Text = tender.PrequalificationRequire;
			this.lblFailDate.Text = Common2.GetTime(tender.QualificationFailData.ToString());
			this.lblFailReason.Text = tender.QualificationFailReason;
			this.file_qd.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Initiate);
			this.file_ys.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Prequalification);
			this.file_fail.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.QualificationFail);
		}
		if (State == ProjectParameter.Bid)
		{
			this.tr_qd.Visible = true;
			this.tr_ys.Visible = true;
			this.tr_pass.Visible = true;
			this.tr_tb.Visible = true;
			this.trAudit_pft.Visible = true;
			this.trAudit_initate.Visible = true;
			this.lblProjStartDate.Text = Common2.GetTime(tender.ProjStartDate.ToString());
			this.lblStartRemark.Text = this.ReplaceTxt(tender.ProjStartRemark);
			this.lblApplyDate.Text = Common2.GetTime(tender.ProjApplyDate.ToString());
			if (tender.ProgAgent != "")
			{
				this.lblAgent.Text = tender.ProgAgentName;
			}
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblApprovalDate.Text = Common2.GetTime(tender.ProjApprovalDate.ToString());
			this.lblQualificationMargin.Text = tender.QualificationMargin.ToString();
			if (tender.ProjRegistDeadline.ToString() != "" && tender.ProjRegistDeadline.ToString() != "0")
			{
				this.lblRegistDeadline.Text = tender.ProjRegistDeadline.ToString() + "天";
			}
			this.lblPrequalificationRequire.Text = tender.PrequalificationRequire;
			this.lblPassDate.Text = Common2.GetTime(tender.QualificationPassDate.ToString());
			this.lblPassReason.Text = tender.QualificationPassReason;
			this.lblTenderAnswerDate.Text = Common2.GetTime(tender.ProjTenderAnswerDate.ToString());
			if (!string.IsNullOrEmpty(tender.TenderAppraiseMethod))
			{
				this.lblTenderAppraiseMethod.Text = this.GetXpmName(System.Convert.ToInt32(tender.TenderAppraiseMethod));
			}
			this.lblTenderAverage.Text = tender.TenderAverage.ToString();
			this.lblTenderBeginDate.Text = Common2.GetTime(tender.ProjTenderBeginDate.ToString());
			this.lblTenderCeilingPrice.Text = tender.TenderCeilingPrice.ToString();
			this.lblTenderContent.Text = this.ReplaceTxt(tender.ProjTenderContent);
			this.lblTenderCostContent.Text = this.ReplaceTxt(tender.ProjTenderCostContent);
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblTenderEarnestMoney.Text = tender.ProjTenderEarnestMoney.ToString();
			if (tender.ProjTenderPayWay == "0")
			{
				this.lblTenderPayWay.Text = "现金";
			}
			if (tender.ProjTenderPayWay == "1")
			{
				this.lblTenderPayWay.Text = "支票";
			}
			if (tender.ProjTenderPayWay == "2")
			{
				this.lblTenderPayWay.Text = "转账";
			}
			this.lblTenderQuote.Text = tender.TenderQuote.ToString();
			if (tender.TenderProspect.HasValue)
			{
				this.lblTenderProspect.Text = tender.TenderProspect.Value.ToString("yyyy-MM-dd");
			}
			this.lblTenderReadOne.Text = WebUtil.GetUserNames(tender.TenderReadOne);
			this.lblTenderRemark.Text = this.ReplaceTxt(tender.ProjTenderRemark);
			this.lblTenderUnit.Text = tender.TenderUnit;
			this.file_qd.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Initiate);
			this.file_ys.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Prequalification);
			this.file_pass.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.QualificationPass);
			this.file_tb.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Bid);
		}
		if (State == ProjectParameter.WinBid)
		{
			this.tr_qd.Visible = true;
			this.tr_ys.Visible = true;
			this.tr_pass.Visible = true;
			this.tr_tb.Visible = true;
			this.tr_zb.Visible = true;
			this.trAudit_pft.Visible = true;
			this.trAudit_initate.Visible = true;
			this.trAudit_bit.Visible = true;
			this.lblProjStartDate.Text = Common2.GetTime(tender.ProjStartDate.ToString());
			this.lblStartRemark.Text = this.ReplaceTxt(tender.ProjStartRemark);
			this.lblApplyDate.Text = Common2.GetTime(tender.ProjApplyDate.ToString());
			if (tender.ProgAgent != "")
			{
				this.lblAgent.Text = tender.ProgAgentName;
			}
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblApprovalDate.Text = Common2.GetTime(tender.ProjApprovalDate.ToString());
			this.lblQualificationMargin.Text = tender.QualificationMargin.ToString();
			if (tender.ProjRegistDeadline.ToString() != "" && tender.ProjRegistDeadline.ToString() != "0")
			{
				this.lblRegistDeadline.Text = tender.ProjRegistDeadline.ToString() + "天";
			}
			this.lblPrequalificationRequire.Text = tender.PrequalificationRequire;
			this.lblPassDate.Text = Common2.GetTime(tender.QualificationPassDate.ToString());
			this.lblPassReason.Text = tender.QualificationPassReason;
			this.lblTenderAnswerDate.Text = Common2.GetTime(tender.ProjTenderAnswerDate.ToString());
			if (!string.IsNullOrEmpty(tender.TenderAppraiseMethod))
			{
				this.lblTenderAppraiseMethod.Text = this.GetXpmName(System.Convert.ToInt32(tender.TenderAppraiseMethod));
			}
			this.lblTenderAverage.Text = tender.TenderAverage.ToString();
			this.lblTenderBeginDate.Text = Common2.GetTime(tender.ProjTenderBeginDate.ToString());
			this.lblTenderCeilingPrice.Text = tender.TenderCeilingPrice.ToString();
			this.lblTenderContent.Text = this.ReplaceTxt(tender.ProjTenderContent);
			this.lblTenderCostContent.Text = this.ReplaceTxt(tender.ProjTenderCostContent);
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblTenderEarnestMoney.Text = tender.ProjTenderEarnestMoney.ToString();
			if (tender.ProjTenderPayWay == "0")
			{
				this.lblTenderPayWay.Text = "现金";
			}
			if (tender.ProjTenderPayWay == "1")
			{
				this.lblTenderPayWay.Text = "支票";
			}
			if (tender.ProjTenderPayWay == "2")
			{
				this.lblTenderPayWay.Text = "转账";
			}
			this.lblTenderQuote.Text = tender.TenderQuote.ToString();
			this.lblTenderRemark.Text = this.ReplaceTxt(tender.ProjTenderRemark);
			this.lblTenderUnit.Text = tender.TenderUnit;
			this.lblSuccessBidDate.Text = Common2.GetTime(tender.SuccessBidDate.ToString());
			this.lblSuccessBidPrice.Text = tender.SuccessBidPrice.ToString();
			this.lblSuccessBidRemark.Text = this.ReplaceTxt(tender.SuccessBidRemark);
			this.file_qd.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Initiate);
			this.file_ys.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Prequalification);
			this.file_pass.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.QualificationPass);
			this.file_tb.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Bid);
			this.file_zb.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.WinBid);
		}
		if (State == ProjectParameter.OutBid)
		{
			this.tr_qd.Visible = true;
			this.tr_ys.Visible = true;
			this.tr_pass.Visible = true;
			this.tr_tb.Visible = true;
			this.tr_lb.Visible = true;
			this.trAudit_pft.Visible = true;
			this.trAudit_initate.Visible = true;
			this.trAudit_bit.Visible = true;
			this.lblProjStartDate.Text = Common2.GetTime(tender.ProjStartDate.ToString());
			this.lblStartRemark.Text = this.ReplaceTxt(tender.ProjStartRemark);
			this.lblApplyDate.Text = Common2.GetTime(tender.ProjApplyDate.ToString());
			if (tender.ProgAgent != "")
			{
				this.lblAgent.Text = tender.ProgAgentName;
			}
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblApprovalDate.Text = Common2.GetTime(tender.ProjApprovalDate.ToString());
			this.lblQualificationMargin.Text = tender.QualificationMargin.ToString();
			if (tender.ProjRegistDeadline.ToString() != "" && tender.ProjRegistDeadline.ToString() != "0")
			{
				this.lblRegistDeadline.Text = tender.ProjRegistDeadline.ToString() + "天";
			}
			this.lblPrequalificationRequire.Text = tender.PrequalificationRequire;
			this.lblPassDate.Text = Common2.GetTime(tender.QualificationPassDate.ToString());
			this.lblPassReason.Text = tender.QualificationPassReason;
			this.lblTenderAnswerDate.Text = Common2.GetTime(tender.ProjTenderAnswerDate.ToString());
			if (!string.IsNullOrEmpty(tender.TenderAppraiseMethod))
			{
				this.lblTenderAppraiseMethod.Text = this.GetXpmName(System.Convert.ToInt32(tender.TenderAppraiseMethod));
			}
			this.lblTenderAverage.Text = tender.TenderAverage.ToString();
			this.lblTenderBeginDate.Text = Common2.GetTime(tender.ProjTenderBeginDate.ToString());
			this.lblTenderCeilingPrice.Text = tender.TenderCeilingPrice.ToString();
			this.lblTenderContent.Text = this.ReplaceTxt(tender.ProjTenderContent);
			this.lblTenderCostContent.Text = this.ReplaceTxt(tender.ProjTenderCostContent);
			this.lblTenderDate.Text = Common2.GetTime(tender.ProjTenderDate.ToString());
			this.lblTenderEarnestMoney.Text = tender.ProjTenderEarnestMoney.ToString();
			if (tender.ProjTenderPayWay == "0")
			{
				this.lblTenderPayWay.Text = "现金";
			}
			if (tender.ProjTenderPayWay == "1")
			{
				this.lblTenderPayWay.Text = "支票";
			}
			if (tender.ProjTenderPayWay == "2")
			{
				this.lblTenderPayWay.Text = "转账";
			}
			this.lblTenderQuote.Text = tender.TenderQuote.ToString();
			this.lblTenderRemark.Text = this.ReplaceTxt(tender.ProjTenderRemark);
			this.lblTenderUnit.Text = tender.TenderUnit;
			this.lblOutBidDate.Text = Common2.GetTime(tender.OutBidDate.ToString());
			if (tender.OutBidIsReturn == true)
			{
				this.lblOutBidIsReturn.Text = "是";
			}
			if (tender.OutBidIsReturn == false)
			{
				this.lblOutBidIsReturn.Text = "否";
			}
			this.lblOutBidRemark.Text = this.ReplaceTxt(tender.OutBidRemark);
			this.file_qd.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Initiate);
			this.file_ys.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Prequalification);
			this.file_pass.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.QualificationPass);
			this.file_tb.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.Bid);
			this.file_lb.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.OutBid);
			this.file_GiveUp.InnerHtml = this.FilesBind(PrjId + "_" + ProjectParameter.GiveUpState);
		}
	}
}


