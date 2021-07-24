using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutPayment_PaymentQuery : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private PayoutPayment payoutPayment = new PayoutPayment();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private ConConfigContractService configSer = new ConConfigContractService();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.ViewState["PaymentId"] = base.Request["ic"];
		}
		if (!string.IsNullOrEmpty(base.Request["showAudit"]))
		{
			if (base.Request["showAudit"] == "1")
			{
				this.trSate.Visible = true;
			}
			new DataTable();
		}
		if (!string.IsNullOrEmpty(base.Request["PaymentId"]))
		{
			this.ViewState["PaymentId"] = base.Request["PaymentId"];
		}
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			this.contractId = base.Request["ContractId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.InintUpdateState();
			bool arg_27_0 = ContractParameter.IsPaymentAlarm;
			this.ShowGuideLine();
			this.InitProjectInfo(this.ViewState["PaymentId"].ToString());
			this.Literal1.Text = this.FilesBind(1904, this.ViewState["PaymentId"].ToString());
			this.hldfIsFundPlan.Value = ConfigHelper.Get("IsFundPlan");
			this.hfldPaymentId.Value = this.ViewState["PaymentId"].ToString();
			this.hldfContractId.Value = this.contractId;
			this.bindTarget();
		}
	}
	public string FilesBind(int moduleID, string recordCode)
	{
		string text = "";
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			moduleID.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"<a href='",
				dataRow["FilePath"].ToString(),
				dataRow["AnnexName"].ToString(),
				"' target=_blank>",
				dataRow["OriginalName"].ToString(),
				"</a> "
			});
		}
		return text;
	}
	protected void InitProjectInfo(string conId)
	{
		PayoutPaymentModel payoutPaymentModel = new PayoutPaymentModel();
		payoutPaymentModel = this.payoutPayment.GetModel(conId);
		PayoutContractModel payoutContractModel = new PayoutContractModel();
		PayoutContract payoutContract = new PayoutContract();
		payoutContractModel = payoutContract.GetModel(payoutPaymentModel.ContractID);
		if (payoutContractModel.PrjGuid != "")
		{
			string sqlString = " select PrjCode,PrjName,PrjGuid,TypeCode from pt_prjinfo where prjGuid='" + payoutContractModel.PrjGuid + "' ";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				this.LblProjectCode.Text = dataTable.Rows[0]["PrjCode"].ToString();
				this.LblProjectName.Text = dataTable.Rows[0]["PrjName"].ToString();
			}
		}
	}
	protected string InitProjectInfoS(string conId)
	{
		string result = string.Empty;
		PayoutPaymentModel payoutPaymentModel = new PayoutPaymentModel();
		payoutPaymentModel = this.payoutPayment.GetModel(conId);
		PayoutContractModel payoutContractModel = new PayoutContractModel();
		PayoutContract payoutContract = new PayoutContract();
		payoutContractModel = payoutContract.GetModel(payoutPaymentModel.ContractID);
		if (payoutContractModel.PrjGuid != "")
		{
			string sqlString = " select PrjCode,PrjName,PrjGuid,TypeCode from pt_prjinfo where prjGuid='" + payoutContractModel.PrjGuid + "' ";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				result = dataTable.Rows[0]["PrjCode"].ToString() + "|" + dataTable.Rows[0]["PrjName"].ToString();
			}
		}
		return result;
	}
	private void InitContractInfo(string conId)
	{
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(conId);
		this.lblContractCode.Text = model.ContractCode;
		this.lblContractName.Text = model.ContractName;
		this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
		string userCode = this.Session["yhdm"].ToString();
		DataTable dataTable = PersonnelAction.QueryPersonnelById(userCode);
		if (dataTable != null && dataTable.Rows.Count == 1)
		{
			this.lblBllProducer.Text = dataTable.Rows[0]["v_xm"].ToString();
		}
		decimal balancedAmount = new PayoutBalance().GetBalancedAmount(conId, false);
		this.lblBalancedSum.Text = balancedAmount.ToString("0.000");
		bool containPending = this.payoutPayment.GetModel(this.ViewState["PaymentId"].ToString()).ContainPending;
		decimal paySum = this.payoutPayment.GetPaySum(this.contractId, containPending);
		this.lblPaySum.Text = paySum.ToString("0.000");
		decimal num = balancedAmount - paySum;
		this.lblDiff.Text = num.ToString("0.000");
		this.lblContractMoney.Text = model.ModifiedMoney.ToString();
		this.lblSignedDate.Text = model.SignDate.Value.ToShortDateString();
	}
	private void InintUpdateState()
	{
		PayoutPaymentModel model = this.payoutPayment.GetModel(this.ViewState["PaymentId"].ToString());
		this.contractId = model.ContractID;
		this.lblPaymentCode.Text = model.PaymentCode;
		this.lblPaymentMoney.Text = model.PaymentMoney.ToString();
		this.lblPaymentDate.Text = model.PaymentDate.Value.ToShortDateString();
		this.lblPaymentPerson.Text = model.PaymentPerson;
		this.lblInputPerson.Text = model.InputPerson;
		this.lblInputDate.Text = model.InputDate.Value.ToShortDateString();
		this.lblNotes.Text = model.Notes;
		decimal number = 0m;
		if (model.PaymentMoney.ToString() != "")
		{
			number = Convert.ToDecimal(model.PaymentMoney);
		}
		this.LblCapitalNumber.Text = ConverRMB.Convert(number);
		if (model.Paytype == -1)
		{
			this.LblPayType.Text = "";
		}
		if (model.Paytype == 0)
		{
			this.LblPayType.Text = "现金";
		}
		if (model.Paytype == 1)
		{
			this.LblPayType.Text = "支票";
		}
		if (model.Paytype == 2)
		{
			this.LblPayType.Text = "转账";
		}
		this.hldfIsFundPlan.Value = model.MonthPlanUID;
		List<string> fundPlanByMonthPlanUID = this.payoutPayment.GetFundPlanByMonthPlanUID(this.hldfIsFundPlan.Value);
		if (fundPlanByMonthPlanUID.Count > 0)
		{
			this.lblMonthDate.Text = fundPlanByMonthPlanUID[1].ToString();
			this.lblPlanMoney.Text = fundPlanByMonthPlanUID[2].ToString();
			this.lblUsedMoney.Text = fundPlanByMonthPlanUID[4].ToString();
			this.lblUsableMoney.Text = fundPlanByMonthPlanUID[5].ToString();
			this.lblRemark.Text = fundPlanByMonthPlanUID[6].ToString();
		}
		this.lblBeneficiary.Text = model.Beneficiary.Trim();
		this.lblBank.Text = model.Bank.Trim();
		this.lalAccount.Text = model.Account.Trim();
		this.InitContractInfo(this.contractId);
	}
	private void ShowGuideLine()
	{
		try
		{
			if (!this.configSer.IsExist(this.contractId) || !(
				from p in this.configSer
				where p.ContractId == this.contractId
				select p).FirstOrDefault<ConConfigContract>().IsPaymentAlarm)
			{
				this.trSate.Visible = false;
			}
			else
			{
				string iD = this.ViewState["PaymentId"].ToString();
				PayoutPaymentModel model = this.payoutPayment.GetModel(iD);
				decimal num = decimal.Parse(this.lblContractMoney.Text);
				this.lblContractAmount.Text = this.lblContractMoney.Text;
				decimal.Parse(this.lblBalancedSum.Text);
				this.lblBalancedAmount.Text = this.lblBalancedSum.Text;
				decimal d = decimal.Parse(this.lblPaySum.Text);
				this.lblPaymentedAmount.Text = d.ToString("0.000");
				decimal num2 = model.PaymentMoney.HasValue ? model.PaymentMoney.Value : 0m;
				this.lblPaymentAmount.Text = num2.ToString("0.000");
				if (num != 0m)
				{
					decimal num3 = d / num;
					this.lblRate.Text = string.Format("{0:P}", num3);
					string payoutPayAmount = this.payoutPayment.GetPayoutPayAmount(num3, this.contractId);
					this.lblState.Text = payoutPayAmount;
					this.SetLableColor(Common2.GetColorByState(payoutPayAmount));
				}
				else
				{
					this.lblRate.Text = "无合同金额";
					this.lblState.Text = "高";
					this.SetLableColor(Common2.GetColorByState("高"));
				}
			}
		}
		catch
		{
		}
	}
	private void SetLableColor(Color c)
	{
		this.lblState.ForeColor = c;
		this.lblContractAmount.ForeColor = c;
		this.lblBalancedAmount.ForeColor = c;
		this.lblPaymentedAmount.ForeColor = c;
		this.lblPaymentAmount.ForeColor = c;
		this.lblRate.ForeColor = c;
	}
	protected void bindTarget()
	{
		PaymentTarget paymentTarget = new PaymentTarget();
		DataTable paymentTarget2 = paymentTarget.GetPaymentTarget(null, this.hldfContractId.Value.Trim(), this.hfldPaymentId.Value.Trim(), this.hfldIsWBSRelevance.Value.Trim());
		if (paymentTarget2.Rows.Count > 0)
		{
			this.lblTitlTarget.Text = "控制指标";
			this.gvBudget.DataSource = paymentTarget2;
			this.gvBudget.DataBind();
			return;
		}
		this.lblTitlTarget.Text = "";
	}
}


