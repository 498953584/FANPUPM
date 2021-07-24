using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutInvoice_InvoiceEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private string invoiceId = string.Empty;
	private PayoutInvoice invoice = new PayoutInvoice();
	private PayoutContract contract = new PayoutContract();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["ContractID"]))
		{
			this.contractId = base.Request["ContractID"];
		}
		if (!string.IsNullOrEmpty(base.Request["InvoiceId"]))
		{
			if (base.Request["InvoiceId"].Contains("["))
			{
				this.invoiceId = JsonHelper.GetListFromJson(base.Request["InvoiceId"])[0];
			}
			else
			{
				this.invoiceId = base.Request["InvoiceId"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindInvoiceType();
			if (string.Compare(this.action, "Add", true) == 0)
			{
				this.InitAddState();
			}
			else
			{
				this.InitUpdateState();
			}
		}
		this.FileLink1.MID = 1911;
		this.FileLink1.FID = this.ViewState["InvoiceId"].ToString();
		this.FileLink1.Type = 1;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (string.Compare(this.action, "Add", true) == 0)
		{
			this.AddInvoice();
			return;
		}
		this.UpdateInvoice();
	}
	private void InitAddState()
	{
		this.ViewState["InvoiceId"] = Guid.NewGuid().ToString();
		this.InitContractInfo(this.contractId);
		this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
		this.txtInputDate.Text = DateTime.Now.ToShortDateString();
	}
	private void InitUpdateState()
	{
		this.ViewState["InvoiceId"] = this.invoiceId;
		PayoutInvoiceInfo model = this.invoice.GetModel(this.invoiceId);
		this.InitContractInfo(model.ContractID);
		this.txtAmount.Text = model.Amount.ToString();
		this.txtInvoiceNo.Text = model.InvoiceNo;
		this.txtInvoiceCode.Text = model.InvoiceCode;
		this.txtPayer.Text = model.Payer;
		this.txtPayee.Text = model.Payee;
		this.dropInvoiceType.SelectedValue = model.InvoiceType;
		this.txtTaxNo.Text = model.TaxNo;
		this.txtTransactor.Text = model.Transactor;
		this.txtInvoiceDate.Text = model.InvoiceDate.ToString();
		this.txtContact.Text = model.Contact;
		this.txtBankCode.Text = model.BankCode;
		this.txtInputPerson.Text = model.InputPerson;
		this.txtInputDate.Text = Convert.ToDateTime(model.InputDate).ToShortDateString();
		this.txtNotes.Text = model.Notes;
		this.txtOrganizationCode.Text = model.OrganizationCode;
	}
	private void InitContractInfo(string contractId)
	{
		if (!string.IsNullOrEmpty(contractId))
		{
			PayoutContractModel model = new PayoutContract().GetModel(contractId);
			this.txtContractCode.Text = model.ContractCode;
			this.txtContractName.Text = model.ContractName;
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			string prjCode = pTPrjInfoService.GetById(model.PrjGuid).PrjCode;
			this.txtPrjCode.Text = prjCode;
			this.txtProject.Text = model.PrjName;
			this.txtContractMoney.Text = model.ModifiedMoney.ToString();
			this.txtSignDate.Text = Convert.ToDateTime(model.SignDate).ToShortDateString();
			PayoutPayment payoutPayment = new PayoutPayment();
			decimal? paySum = payoutPayment.GetPaySum(contractId);
			decimal? invoiceSum = this.invoice.GetInvoiceSum(contractId);
			this.txtPaymentSum.Text = paySum.ToString();
			this.txtInvoiceSum.Text = invoiceSum.ToString();
			this.txtDiff.Text = Convert.ToString(paySum - invoiceSum);
			this.txtPayer.Text = model.AName;
			this.txtPayee.Text = model.CorpName;
			XPMBasicContactCorp xPMBasicContactCorp = this.GetbasiCorp(model.BName);
			this.txtTaxNo.Text = xPMBasicContactCorp.TaxCard;
			this.txtContact.Text = xPMBasicContactCorp.Address + xPMBasicContactCorp.Telephone;
			this.txtBankCode.Text = xPMBasicContactCorp.AccountBank + xPMBasicContactCorp.BankAccounts;
		}
	}
	private void AddInvoice()
	{
		PayoutInvoiceInfo payoutInvoiceInfo = new PayoutInvoiceInfo();
		payoutInvoiceInfo.InvoiceID = this.ViewState["InvoiceId"].ToString();
		payoutInvoiceInfo.ContractID = this.contractId;
		payoutInvoiceInfo.InvoiceNo = this.txtInvoiceNo.Text.Trim();
		payoutInvoiceInfo.InvoiceCode = this.txtInvoiceCode.Text.Trim();
		payoutInvoiceInfo.InvoiceType = this.dropInvoiceType.SelectedValue;
		payoutInvoiceInfo.TaxNo = this.txtTaxNo.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtInvoiceDate.Text.Trim()))
		{
			payoutInvoiceInfo.InvoiceDate = new DateTime?(Convert.ToDateTime(this.txtInvoiceDate.Text.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtAmount.Text.Trim()))
		{
			payoutInvoiceInfo.Amount = new decimal?(Convert.ToDecimal(this.txtAmount.Text.Trim()));
		}
		payoutInvoiceInfo.Transactor = this.txtTransactor.Text.Trim();
		payoutInvoiceInfo.FlowState = new int?(-1);
		payoutInvoiceInfo.Annex = string.Empty;
		payoutInvoiceInfo.Payer = this.txtPayer.Text.Trim();
		payoutInvoiceInfo.Payee = this.txtPayee.Text.Trim();
		payoutInvoiceInfo.InputPerson = this.txtInputPerson.Text.Trim();
		payoutInvoiceInfo.Contact = this.txtContact.Text.Trim();
		payoutInvoiceInfo.BankCode = this.txtBankCode.Text.Trim();
		payoutInvoiceInfo.OrganizationCode = this.txtOrganizationCode.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtInputDate.Text.Trim()))
		{
			payoutInvoiceInfo.InputDate = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text.Trim()));
		}
		payoutInvoiceInfo.Notes = this.txtNotes.Text.Trim();
		try
		{
			this.invoice.Add(payoutInvoiceInfo);
			base.RegisterScript("top.ui.show('添加成功!'); winclose('InvoiceEdit', 'PayoutInvoiceEdit.aspx?ContractID=" + this.contractId + "', true);");
		}
		catch (Exception)
		{
			base.RegisterHintScript("Add", false, string.Empty);
		}
	}
	private void UpdateInvoice()
	{
		PayoutInvoiceInfo model = this.invoice.GetModel(this.invoiceId);
		model.InvoiceNo = this.txtInvoiceNo.Text.Trim();
		model.InvoiceCode = this.txtInvoiceCode.Text.Trim();
		model.InvoiceType = this.dropInvoiceType.SelectedValue;
		model.TaxNo = this.txtTaxNo.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtInvoiceDate.Text.Trim()))
		{
			model.InvoiceDate = new DateTime?(Convert.ToDateTime(this.txtInvoiceDate.Text.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtAmount.Text.Trim()))
		{
			model.Amount = new decimal?(Convert.ToDecimal(this.txtAmount.Text.Trim()));
		}
		model.Transactor = this.txtTransactor.Text.Trim();
		model.Payer = this.txtPayer.Text.Trim();
		model.Payee = this.txtPayee.Text.Trim();
		model.Contact = this.txtContact.Text.Trim();
		model.BankCode = this.txtBankCode.Text.Trim();
		model.Notes = this.txtNotes.Text.Trim();
		model.OrganizationCode = this.txtOrganizationCode.Text.Trim();
		try
		{
			this.invoice.Update(model);
			base.RegisterScript("top.ui.show('更新成功!'); winclose('InvoiceEdit', 'PayoutInvoiceEdit.aspx?ContractID=" + model.ContractID + "', true);");
		}
		catch (Exception)
		{
			base.RegisterHintScript("Update", false, string.Empty);
		}
	}
	private void DataBindInvoiceType()
	{
		InvoiceType invoiceType = new InvoiceType();
		List<InvoiceTypeInfo> invoiceType2 = invoiceType.GetInvoiceType();
		this.dropInvoiceType.DataSource = invoiceType2;
		this.dropInvoiceType.DataBind();
		DropDownlistUtil.AddHintItem(this.dropInvoiceType, "发票类型");
	}
	protected XPMBasicContactCorp GetbasiCorp(string BName)
	{
		XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
		int id = Convert.ToInt32(BName);
		return xPMBasicContactCorpService.GetById(id);
	}
}


