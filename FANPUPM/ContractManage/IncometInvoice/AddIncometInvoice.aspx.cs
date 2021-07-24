using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using cn.justwin.XPMBasicContactCorp;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometInvoice_AddIncometInvoice : NBasePage, IRequiresSessionState
{
	private IncometInvoiceBll incometInvoiceBll = new IncometInvoiceBll();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private BasicContactCorp BasicCorp = new BasicContactCorp();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindDdl();
		IncometContractModel model = this.incometContractBll.GetModel(base.Request.QueryString["ContractID"]);
		if (base.Request.QueryString["id"] != null)
		{
			this.lblTitle.Text = "编辑发票";
			IncometInvoiceModel model2 = this.incometInvoiceBll.GetModel(base.Request.QueryString["id"]);
			this.txtAmount.Text = model2.Amount.ToString();
			this.txtInputDate.Text = Common2.GetTime(model2.InputDate.ToString());
			this.txtInputPerson.Text = model2.InputPerson;
			this.txtInvoiceDate.Text = Common2.GetTime(model2.InvoiceDate.ToString());
			this.txtNotes.Text = model2.Notes;
			this.txtTransactor.Text = model2.Transactor;
			this.hdGuid.Value = model2.InvoiceID;
			this.txtInvoiceNo.Text = model2.InvoiceNo;
			this.hdChangeCode.Value = model2.InvoiceNo;
			this.txtInvoiceCode.Text = model2.InvoiceCode;
			this.txtParty.Text = model2.Payer;
			this.txtSecond.Text = model2.Payee;
			this.ddlInvoiceType.SelectedValue = model2.InvoiceType;
			this.txtTaxNo.Text = model2.TaxNo;
			this.txtContact.Text = model2.Contact;
			this.txtBankCode.Text = model2.BankCode;
			this.txtOrganizationCode.Text = model2.OrganizationCode;
		}
		else
		{
			this.lblTitle.Text = "开票登记";
			this.txtInputDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
			this.hdGuid.Value = Guid.NewGuid().ToString();
			this.txtSecond.Text = model.Second;
			BasicContactCorpModel basicModel = this.GetBasicModel(this.txtSecond.Text.Trim());
			if (basicModel != null)
			{
				this.txtTaxNo.Text = basicModel.TaxCard;
				this.txtContact.Text = basicModel.Address + basicModel.Telephone;
				this.txtBankCode.Text = basicModel.AccountBank + basicModel.BankAccounts;
			}
			if (model.Party != null)
			{
				this.txtParty.Text = model.Party.CorpName;
			}
		}
		this.txtPrjCode.Text = model.Project.PrjCode;
		this.txtPrjName.Text = model.Project.PrjName;
		this.txtContractCode.Text = model.ContractCode;
		this.txtContractName.Text = model.ContractName;
		this.txtContractPrice.Text = WebUtil.GetEnPrice(model.ContractPrice.ToString(), model.ContractID);
		this.txtSignedTime.Text = Common2.GetTime(model.SignedTime.ToString());
		this.txtPaymentSum.Text = WebUtil.GetPaymentSum(model.ContractID, "Con_Incomet_Payment", "CllectionPrice");
		this.txtInvoiceSum.Text = WebUtil.GetPaymentSum(model.ContractID, "Con_Incomet_Invoice", "Amount");
		this.txtBalance.Text = string.Concat(Convert.ToDecimal(this.txtPaymentSum.Text) - Convert.ToDecimal(this.txtInvoiceSum.Text));
		this.FileLink1.MID = 1912;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
	}
	public void BindDdl()
	{
		this.ddlInvoiceType.DataSource = Common2.GetTable("dbo.XPM_Basic_CodeList", "JOIN XPM_Basic_CodeType ON XPM_Basic_CodeType.TypeID = XPM_Basic_CodeList.TypeID WHERE XPM_Basic_CodeType.SignCode = 'InvoiceType' AND XPM_Basic_CodeList.IsValid = 1");
		this.ddlInvoiceType.DataBind();
		WebUtil.AddItem(this.ddlInvoiceType, "发票类型");
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		int num;
		if (base.Request.QueryString["id"] != null)
		{
			num = this.UpdateModel();
		}
		else
		{
			num = this.AddModel();
		}
		if (num != 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("top.ui.show('" + this.SetMsg() + "成功');\n");
			stringBuilder.Append("winclose('AddIncometInvoice','ShowInvoiceList.aspx?ContractID=" + base.Request.QueryString["ContractID"] + "',true)");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		base.RegisterScript("top.ui.alert('" + this.SetMsg() + "失败');");
	}
	public int AddModel()
	{
		return this.incometInvoiceBll.Add(this.GetModel());
	}
	public int UpdateModel()
	{
		return this.incometInvoiceBll.Update(this.GetModel());
	}
	public IncometInvoiceModel GetModel()
	{
		return new IncometInvoiceModel
		{
			Amount = new decimal?(Convert.ToDecimal(this.txtAmount.Text.Trim())),
			Annex = "",
			ContractID = base.Request.QueryString["ContractID"],
			FlowState = new int?(0),
			InputDate = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text.Trim())),
			InputPerson = this.txtInputPerson.Text.Trim(),
			InvoiceCode = this.txtInvoiceCode.Text.Trim(),
			InvoiceDate = new DateTime?(Convert.ToDateTime(this.txtInvoiceDate.Text.Trim())),
			InvoiceID = this.hdGuid.Value.Trim(),
			Notes = this.txtNotes.Text.Trim(),
			Transactor = this.txtTransactor.Text.Trim(),
			InvoiceNo = this.txtInvoiceNo.Text.Trim(),
			Payee = this.txtSecond.Text.Trim(),
			Payer = this.txtParty.Text.Trim(),
			InvoiceType = this.ddlInvoiceType.SelectedValue,
			TaxNo = this.txtTaxNo.Text.Trim(),
			Project = "",
			BankCode = this.txtBankCode.Text.Trim(),
			Contact = this.txtContact.Text.Trim(),
			OrganizationCode = this.txtOrganizationCode.Text.Trim()
		};
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
	public BasicContactCorpModel GetBasicModel(string corpname)
	{
		List<BasicContactCorpModel> source = this.BasicCorp.BasicCorpList();
		return (
			from p in source
			where p.CorpName == corpname
			select p).FirstOrDefault<BasicContactCorpModel>();
	}
}


