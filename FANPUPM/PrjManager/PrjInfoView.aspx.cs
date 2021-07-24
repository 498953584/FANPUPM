using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using cn.justwin.Web;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_PrjInfoView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["ic"]))
		{
			this.BindLbl(base.Request["ic"].ToString());
			this.upload.InnerHtml = this.FilesBind(base.Request["ic"].ToString());
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
		ProjectInfo byId = ProjectInfo.GetById(PrjId);
		if (byId != null)
		{
			this.lblBllProducer.Text = byId.GetUserName(base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			this.lblPrjState.Text = TypeList.GetTypeName(byId.PrjState.ToString(), "2", "ProjectState");
			this.lblBudgetWay.Text = TypeList.GetTypeName(byId.BudgetWay, "1", "ysType");
			this.lblContractWay.Text = TypeList.GetTypeName(byId.ContractWay, "1", "cbType");
			this.lblKeyPart.Text = TypeList.GetTypeName(byId.KeyPart, "1", "primaryGrade");
			this.lblPayCondition.Text = TypeList.GetTypeName(byId.PayCondition, "1", "payment");
			this.lblPayWay.Text = TypeList.GetTypeName(byId.PayWay, "1", "jsType");
			if (byId.PrjTypes != null)
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				System.Text.StringBuilder stringBuilder2 = new System.Text.StringBuilder();
				foreach (ProjectKind current in byId.PrjTypes)
				{
					stringBuilder2.Append(current.ProfessionalCost.ToString()).Append("<br />");
					stringBuilder.Append(TypeList.GetTypeName(current.PrjKind, "1", "ProjectType")).Append("<br />");
				}
				this.lblPrjKindClass.Text = stringBuilder.ToString();
				this.lblPrjProfessionalCost.Text = stringBuilder2.ToString();
			}
			this.lblProperty.Text = TypeList.GetTypeName(byId.PrjProperty, "1", "ProjectProperty");
			this.lblInfoOrigin.Text = this.ReplaceTxt(byId.ProjInfoOrigin);
			this.lblElseRequest.Text = this.ReplaceTxt(byId.ProjElseRequest);
			this.lblQualityClass.Text = TypeList.GetTypeName(byId.QualityClass, "1", "ProjectQuality");
			if (byId.PrjRanks != null)
			{
				System.Text.StringBuilder stringBuilder3 = new System.Text.StringBuilder();
				foreach (ProjectRank current2 in byId.PrjRanks)
				{
					stringBuilder3.Append(TypeList.GetTypeName(current2.Rank, "1", "zzGrade") + " " + current2.RankLevel).Append("<br />");
				}
				this.lblRank.Text = stringBuilder3.ToString();
			}
			this.lblTenderWay.Text = TypeList.GetTypeName(byId.TenderWay, "1", "zbType");
			this.lblXmgroup.Text = TypeList.GetXmlGroupName(byId.Xmgroup);
			if (byId.EngineeringTypes != null)
			{
				System.Text.StringBuilder stringBuilder4 = new System.Text.StringBuilder();
				foreach (EngineeringType current3 in byId.EngineeringTypes)
				{
					stringBuilder4.Append(current3.ToString()).Append("<br />");
				}
				this.lblBuildingType.Text = stringBuilder4.ToString();
			}
			this.lblTelphone.Text = byId.Telephone;
			if (byId.ProgAgent != "")
			{
				this.lblAgent.Text = byId.ProgAgentName;
			}
			this.lblBuildingArea.Text = byId.BuildingArea;
			this.lblDesigner.Text = byId.Designer;
			this.lblDuration.Text = byId.Duration;
			this.lblEndDate.Text = Common2.GetTime(byId.EndDate);
			this.lblgrade.Text = byId.PrjGrade;
			this.lblInspector.Text = byId.Inspector;
			this.lblOtherStatement.Text = StringUtility.ReplaceTxt(byId.OtherStatement);
			this.lblPrjCode.Text = byId.PrjCode;
			if (byId.PrjCost.ToString() != "0" && byId.PrjCost.ToString() != "")
			{
				this.lblPrjCost.Text = byId.PrjCost.ToString();
			}
			this.lblPrjFundInfo.Text = byId.PrjFundInfo;
			this.lblPrjInfo.Text = StringUtility.ReplaceTxt(byId.PrjInfo);
			if (byId.PrjManager != "")
			{
				this.lblPrjManager.Text = byId.PrjManagerName;
			}
			if (byId.Businessman != "")
			{
				this.lblBusinessman.Text = byId.BusinessmanName;
			}
			this.lblPrjName.Text = byId.PrjName;
			this.lblPrjPlace.Text = byId.PrjPlace;
			this.lblRemark1.Text = StringUtility.ReplaceTxt(byId.Remark);
			this.lblStartDate.Text = Common2.GetTime(byId.StartDate);
			this.lblTotalHouseNum.Text = byId.TotalHouseNum;
			this.lblUndergroundArea.Text = byId.UndergroundArea;
			this.lblUsegrounArea.Text = byId.UsegrounArea;
			this.lblAfforestArea.Text = byId.AfforestArea;
			this.lblParkArea.Text = byId.ParkArea;
			if (byId.WorkUnit != "")
			{
				this.lblWorkUnit.Text = byId.WorkUnitName;
			}
			this.lblForecastProfitRate.Text = byId.ForecastProfitRate.ToString();
			this.lblEngineeringEstimates.Text = byId.EngineeringEstimates;
			if (byId.PrjDutyPerson != "")
			{
				this.lblDutyPerson.Text = byId.PrjDutyPersonName;
			}
			this.lblApprovalOf.Text = byId.PrjApprovalOf;
			this.lblFundWorkable.Text = byId.PrjFundWorkable;
			this.lblManagementMargin.Text = byId.ManagementMargin.ToString();
			this.lblMigrantQualityMarginRate.Text = byId.MigrantQualityMarginRate.ToString();
			this.lblWithholdingTaxRate.Text = byId.WithholdingTaxRate.ToString();
			this.lblPerformanceBond.Text = byId.PerformanceBond.ToString();
			this.lblElseMargin.Text = byId.ElseMargin.ToString();
			this.lblOwner.Text = byId.OwnerName;
			this.lblOwnerLinkMan.Text = byId.OwnerLinkMan;
			this.lblOwnerLinkManPhone.Text = byId.OwnerLinkPhone;
			this.lblOwnerAddress.Text = byId.OwnerAddress;
			this.lblCorp.Text = byId.ProjPeopleCorp;
			this.lblPhone.Text = byId.ProjPeopleTel;
			this.lblDuty.Text = byId.ProjPeopleDuty;
			this.lblName.Text = byId.ProjPeopleUserName;
			this.lblPrjManagerRequire.Text = byId.PrjManagerRequire;
			this.lblTechnicalLeaderRequire.Text = byId.TechnicalLeaderRequire;
			this.lblPrjReadOne.Text = WebUtil.GetUserNames(byId.PrjReadOne);
			if (!string.IsNullOrEmpty(byId.Province))
			{
				if (byId.City == "北京" || byId.City == "上海" || byId.City == "天津" || byId.City == "重庆" || byId.City == "香港" || byId.City == "澳门")
				{
					this.lblArea.Text = byId.City;
					return;
				}
				this.lblArea.Text = byId.Province + byId.City;
			}
		}
	}
}


