using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometInvoice_QueryInvoice : NBasePage, IRequiresSessionState
{
	private IncometInvoiceBll incometInvoiceBll = new IncometInvoiceBll();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lblBllProducer.Text = WebUtil.GetUserNames(this.Session["yhdm"].ToString());
			this.lblPrintDate.Text = Common2.GetTime(DateTime.Now);
			this.InitPage();
		}
	}
	protected void InitPage()
	{
		IncometContractModel model = this.incometContractBll.GetModel(base.Request.QueryString["ContractID"]);
		if (model != null)
		{
			this.lblPrjCode.Text = model.Project.PrjCode;
			this.lblPrjName.Text = model.Project.PrjName;
			this.lblContractCode.Text = model.ContractCode;
			this.lblContractName.Text = model.ContractName;
			this.lblContractMoney.Text = WebUtil.GetEnPrice(model.ContractPrice.ToString(), model.ContractID);
			this.lblSignedDate.Text = Common2.GetTime(model.SignedTime.ToString());
			this.lblPaymentSum.Text = WebUtil.GetPaymentSum(model.ContractID, "Con_Incomet_Payment", "CllectionPrice");
			this.lblInvoiceSum.Text = WebUtil.GetPaymentSum(model.ContractID, "Con_Incomet_Invoice", "Amount");
			this.lblBalance.Text = string.Concat(Convert.ToDecimal(this.lblPaymentSum.Text) - Convert.ToDecimal(this.lblInvoiceSum.Text));
		}
		IncometInvoiceModel model2 = this.incometInvoiceBll.GetModel(base.Request.QueryString["id"]);
		if (model2 != null)
		{
			this.lblAmountMoney.Text = model2.Amount.ToString();
			this.lblInputTime.Text = Common2.GetTime(model2.InputDate.ToString());
			this.lblInputUser.Text = model2.InputPerson;
			this.lblInvoiceDate.Text = Common2.GetTime(model2.InvoiceDate.ToString());
			this.lblNote.Text = model2.Notes;
			this.lblTransactor.Text = model2.Transactor;
			this.lblInvoiceNo.Text = model2.InvoiceNo;
			this.lblInvoiceCode.Text = model2.InvoiceCode;
			this.lblParty.Text = model2.Payer;
			this.lblSecond.Text = model2.Payee;
			this.lblInvoiceType.Text = this.GetInvoiceType(model2.InvoiceType);
			this.lblTaxNo.Text = model2.TaxNo;
			this.lblAddress.Text = model2.Contact;
			this.lblBankCode.Text = model2.BankCode;
			this.lblOrganizationCode.Text = model2.OrganizationCode;
			this.lblUpFiled.Text = FileView.FilesBind(1912, model2.InvoiceID);
		}
	}
	private string GetInvoiceType(string key)
	{
		DataTable table = Common2.GetTable("dbo.XPM_Basic_CodeList", "WHERE NoteID = " + key);
		if (table.Rows.Count != 0)
		{
			return table.Rows[0]["CodeName"].ToString();
		}
		return string.Empty;
	}
}


