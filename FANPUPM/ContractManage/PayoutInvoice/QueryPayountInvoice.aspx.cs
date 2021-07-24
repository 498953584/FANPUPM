using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutInvoice_QueryPayountInvoice : NBasePage, IRequiresSessionState
{
	private PayoutInvoice invoice = new PayoutInvoice();
	private PayoutContract contract = new PayoutContract();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lblBllProducer.Text = WebUtil.GetUserNames(this.Session["yhdm"].ToString());
			this.lblPrintDate.Text = Common2.GetTime(DateTime.Now);
			this.BindPage();
		}
	}
	protected void BindPage()
	{
		PayoutInvoiceInfo model = this.invoice.GetModel(base.Request["InvoiceId"]);
		if (model != null)
		{
			PayoutContractModel model2 = new PayoutContract().GetModel(model.ContractID);
			if (model2 != null)
			{
				this.lblContractCode.Text = model2.ContractCode;
				this.lblContractName.Text = model2.ContractName;
				PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
				string prjCode = pTPrjInfoService.GetById(model2.PrjGuid).PrjCode;
				this.lblPrjCode.Text = prjCode;
				this.lblPrjName.Text = model2.PrjName;
				this.lblContractMoney.Text = model2.ModifiedMoney.ToString();
				this.lblSignedDate.Text = Convert.ToDateTime(model2.SignDate).ToShortDateString();
				PayoutPayment payoutPayment = new PayoutPayment();
				decimal? paySum = payoutPayment.GetPaySum(model2.ContractID);
				decimal? invoiceSum = this.invoice.GetInvoiceSum(model2.ContractID);
				this.lblPaymentSum.Text = paySum.ToString();
				this.lblInvoiceSum.Text = invoiceSum.ToString();
				this.lblDiff.Text = Convert.ToString(paySum - invoiceSum);
			}
			this.lblAmountMoney.Text = model.Amount.ToString();
			this.lblInvoiceNo.Text = model.InvoiceNo;
			this.lblInvoiceCode.Text = model.InvoiceCode;
			this.lblParty.Text = model.Payer;
			this.lblSecond.Text = model.Payee;
			this.lblInvoiceType.Text = this.getInvoiceType(model.InvoiceType);
			this.lblTaxNo.Text = model.TaxNo;
			this.lblTransactor.Text = model.Transactor;
			this.lblInvoiceDate.Text = model.InvoiceDate.ToString();
			this.lblAddress.Text = model.Contact;
			this.lblBankCode.Text = model.BankCode;
			this.lblInputUser.Text = model.InputPerson;
			this.lblInputTime.Text = Convert.ToDateTime(model.InputDate).ToShortDateString();
			this.lblNote.Text = model.Notes;
			this.lblOrganizationCode.Text = model.OrganizationCode;
			this.lblUpFiled.Text = FileView.FilesBind(1911, model.InvoiceID);
		}
	}
	private string getInvoiceType(string key)
	{
		InvoiceType invoiceType = new InvoiceType();
		List<InvoiceTypeInfo> invoiceType2 = invoiceType.GetInvoiceType();
		InvoiceTypeInfo invoiceTypeInfo = (
			from data in invoiceType2
			select data).FirstOrDefault((InvoiceTypeInfo data) => data.NoteID.Equals(key));
		if (invoiceTypeInfo != null)
		{
			return invoiceTypeInfo.CodeName;
		}
		return string.Empty;
	}
}


