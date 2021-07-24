using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometPayment_ViewShowPayment : NBasePage, IRequiresSessionState
{
	private IncometPaymentBll incometPaymentBll = new IncometPaymentBll();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private string contractId = string.Empty;
	private string Id = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		this.contractId = (string.IsNullOrEmpty(base.Request["ContractId"]) ? string.Empty : base.Request["ContractId"]);
		this.Id = (string.IsNullOrEmpty(base.Request["Id"]) ? string.Empty : base.Request["Id"]);
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lblBllProducer.Text = WebUtil.GetUserNames(this.Session["yhdm"].ToString());
			this.lblPrintDate.Text = Common2.GetTime(DateTime.Now);
			IncometContractModel model = this.incometContractBll.GetModel(base.Request.QueryString["ContractID"]);
			if (model != null)
			{
				this.lblContractCode.Text = model.ContractCode;
				this.lblContractName.Text = model.ContractName;
				this.lblContractMoney.Text = WebUtil.GetEnPrice(model.ContractPrice.ToString(), model.ContractID);
				this.lblSignedDate.Text = Common2.GetTime(model.SignedTime.ToString());
				this.lblPrjCode.Text = ((model.Project != null) ? model.Project.PrjCode : string.Empty);
				this.lblPrjName.Text = ((model.Project != null) ? model.Project.PrjName : string.Empty);
				this.lblPaymentSum.Text = WebUtil.GetPaymentSum(model.ContractID, "Con_Incomet_Payment", "CllectionPrice");
				this.lbldiff.Text = string.Concat(Convert.ToDecimal(this.lblContractMoney.Text) - Convert.ToDecimal(this.lblPaymentSum.Text));
			}
			IncometPaymentModel model2 = this.incometPaymentBll.GetModel(base.Request.QueryString["id"]);
			if (model2 != null)
			{
				this.lblPaymentCode.Text = model2.CllectionCode;
				this.lblPayMoney.Text = model2.CllectionPrice.ToString();
				this.lblPayTime.Text = Common2.GetTime(model2.CllectionTime.ToString());
				this.lblPaymentUser.Text = model2.CllectionUser;
				this.lblInputTime.Text = Common2.GetTime(model2.InputDate.ToString());
				this.lblInputUser.Text = model2.InputPerson;
				this.lblnote.Text = model2.Remark;
				List<string> fundPlanByMonthPlanUID = this.incometPaymentBll.GetFundPlanByMonthPlanUID(model2.MonthPlanUID, false);
				if (fundPlanByMonthPlanUID.Count > 0)
				{
					this.lblPlanMonth.Text = fundPlanByMonthPlanUID[1].ToString();
					this.lblPlanMoney.Text = fundPlanByMonthPlanUID[2].ToString();
					this.lblFinshMoney.Text = fundPlanByMonthPlanUID[4].ToString();
					this.lblAllowCollectMoney.Text = fundPlanByMonthPlanUID[5].ToString();
					this.lblPlanote.Text = fundPlanByMonthPlanUID[6].ToString();
				}
			}
			this.lblUpFiled.Text = FileView.FilesBind(1908, model2.ID);
		}
	}
}


