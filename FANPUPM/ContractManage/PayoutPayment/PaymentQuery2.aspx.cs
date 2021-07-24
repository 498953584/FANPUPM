using ASP;
using cn.justwin.contractDAL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutPayment_PaymentQuery2 : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private PayoutPayment payoutPayment = new PayoutPayment();
	private BasicSerialNumberService ser = new BasicSerialNumberService();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitProjectInfo(this.ViewState["PaymentId"].ToString());
			this.hfldPaymentIds.Value = this.ViewState["PaymentId"].ToString();
			this.InitPageInfor(this.contractId);
			this.InitDepartment();
			this.InitPageInfor(this.contractId);
			this.hldfContractId.Value = this.contractId;
			this.InitRptAudit();
			this.GetNamePath(base.UserCode);
		}
	}
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
				this.lblProjectCode.Text = dataTable.Rows[0]["PrjCode"].ToString();
				this.lblDepartMent.Text = dataTable.Rows[0]["PrjName"].ToString();
			}
		}
	}
	public void InitDepartment()
	{
		PTdbmService pTdbmService = new PTdbmService();
		PTdbm byId = pTdbmService.GetById(1);
		this.lblTitle.Text = byId.V_BMMC + "付款报销单";
	}
	public void InitPageInfor(string conId)
	{
		this.InitDepartment();
		this.lblPayMentDate.Text = DateTime.Now.ToShortDateString();
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(conId);
		this.lblContent.Text = model.ContractName;
		PayoutPaymentModel model2 = this.payoutPayment.GetModel(this.ViewState["PaymentId"].ToString());
		if (model2.Paytype == -1)
		{
			this.lblPayment.Text = "";
		}
		if (model2.Paytype == 0)
		{
			this.lblPayment.Text = "现金";
		}
		if (model2.Paytype == 1)
		{
			this.lblPayment.Text = "支票";
		}
		if (model2.Paytype == 2)
		{
			this.lblPayment.Text = "转账";
		}
		decimal number = 0m;
		if (model2.PaymentMoney.ToString() != "")
		{
			number = Convert.ToDecimal(model2.PaymentMoney);
		}
		this.lblMoney.Text = model2.PaymentMoney.ToString();
		this.lblOperator.Text = model2.InputPerson;
		this.lblCapital.Text = ConverRMB.Convert(number);
		this.lblRecivePeople.Text = model2.Beneficiary.Trim();
		this.lblBank.Text = model2.Bank.Trim();
		this.lblAccount.Text = model2.Account.Trim();
		this.lblTotalMoney.Text = model2.PaymentMoney.ToString();
		this.lblFileNumber.Text = this.ser.GetNo("Con_Payout_Payment", this.ViewState["PaymentId"].ToString());
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
	public void InitRptAudit()
	{
		WFInstanceMainService wFInstanceMainService = new WFInstanceMainService();
		WFInstanceService source = new WFInstanceService();
		IList<WFInstanceMain> byInstanceCode = wFInstanceMainService.GetByInstanceCode(this.ViewState["PaymentId"]);
		List<WFInstance> list = new List<WFInstance>();
		foreach (WFInstanceMain im in byInstanceCode)
		{
			List<WFInstance> list2 = (
				from i in source
				where i.ID == (int?)im.ID && i.AuditResult == (int?)1
				orderby i.AuditDate descending
				select i).ToList<WFInstance>();
			foreach (WFInstance current in list2)
			{
				if ((im.ID == byInstanceCode.Last<WFInstanceMain>().ID || current.Sing.Value != -1) && !string.IsNullOrEmpty(this.GetNamePath(current.Operator)))
				{
					list.Add(current);
				}
			}
		}
		if (list.Count > 0)
		{
			this.rptAudit.DataSource = list;
			this.rptAudit.DataBind();
		}
	}
	public string GetNamePath(object userCode)
	{
		string result = string.Empty;
		PTLOGINService pTLOGINService = new PTLOGINService();
		PTLOGIN byUserCode = pTLOGINService.GetByUserCode(userCode.ToString());
		if (byUserCode != null && !string.IsNullOrEmpty(byUserCode.AuditNameImagePath))
		{
			result = "../.." + byUserCode.AuditNameImagePath;
		}
		else
		{
			result = "";
		}
		return result;
	}
}


