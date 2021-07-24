using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Fund.AccounPayout.BLL;
using cn.justwin.Fund.AccounPayout.Model;
using cn.justwin.Fund.Account;
using cn.justwin.Fund.RequestPayment.BLL;
using cn.justwin.Fund.RequestPayment.Model;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Domain;
using cn.justwin.stockBLL.Fund.Account;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccountPayout_AccountPayoutEdit : NBasePage, IRequiresSessionState
{
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private AccounPayoutModel accountModel;
	private AccounPayoutBLL AccountBll = new AccounPayoutBLL();
	private RequestPayMain RPBLL = new RequestPayMain();
	private RequestPayMainModel RPModel;
	private AccounModel aM;
	private AccountLogic al = new AccountLogic();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdnZHID.Value = base.Request.QueryString["ZHID"].ToString();
			this.HdnSub.Value = base.Request.QueryString["Sub"].ToString();
			string strwhere = " and AccountID='" + base.Request["ZHID"].ToString() + "' ";
			Fund_Prj_Accoun fund_Prj_Accoun = new Fund_Prj_Accoun();
			DataTable accounSumInfo = fund_Prj_Accoun.getAccounSumInfo(strwhere);
			this.hdnyue.Value = accounSumInfo.Rows[0]["JE"].ToString();
			if (this.HdnSub.Value == "0")
			{
				this.lblSel.Text = "资金支付申请";
			}
			else
			{
				this.lblSel.Text = "费用名称";
			}
			if (base.Request.QueryString["Action"] == "Add")
			{
				this.txtAccCode.Text = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				this.hdnAccountID.Value = Guid.NewGuid().ToString();
				this.txtInDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtInPeople.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, base.UserCode);
				this.hdnPeopleCode.Value = base.UserCode;
				this.aM = this.al.GetModel(this.hdnZHID.Value);
				if (!this.aM.PrjGuid.StartsWith("'"))
				{
					PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(this.aM.PrjGuid);
					if (modelByPrjGuid != null)
					{
						this.txtPrjName.Text = modelByPrjGuid.PrjName;
						this.hdnProjectCode.Value = this.aM.PrjGuid;
					}
				}
			}
			else
			{
				this.hdnAccountID.Value = base.Request.QueryString["AccountID"].ToString();
				this.accountModel = this.AccountBll.GetModel(new Guid(this.hdnAccountID.Value));
				this.txtAccCode.Text = this.accountModel.PayOutCode.ToString();
				this.txtInDate.Text = Convert.ToDateTime(this.accountModel.PayOutTime).ToString("yyyy-MM-dd");
				this.txtInPeople.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.accountModel.PayOutPeople);
				this.hdnPeopleCode.Value = this.accountModel.PayOutPeople;
				this.hdnProjectCode.Value = this.accountModel.prjGuid.ToString();
				PrjInfoModel modelByPrjGuid2 = this.ptPrjInfo.GetModelByPrjGuid(this.accountModel.prjGuid);
				if (modelByPrjGuid2 != null)
				{
					this.txtPrjName.Text = modelByPrjGuid2.PrjName;
				}
				this.txtRemark.Text = this.accountModel.Remark.ToString();
				this.txtInMoney.Text = this.accountModel.PayOutMoney.ToString();
				this.txtHandler.Text = this.accountModel.Handler.ToString();
				this.hdnRPUID.Value = this.accountModel.RPGuid.ToString();
				this.txtPayOut.Text = this.AccountBll.getMoneyByPayCode(this.accountModel.RPGuid.ToString()).ToString();
				this.txtPayMoney.Text = "0.00";
				if (this.HdnSub.Value == "0")
				{
					PayoutPayment payoutPayment = new PayoutPayment();
					PayoutPaymentModel model = payoutPayment.GetModel(this.accountModel.RPGuid.ToString());
					if (model != null)
					{
						this.txtRPCode.Text = model.PaymentCode.ToString();
						this.txtPayMoney.Text = model.PaymentMoney.ToString();
					}
				}
				else
				{
					string strwhere2 = " inDiaryId ='" + this.accountModel.RPGuid.ToString() + "' ";
					DataTable dtByWhere = OrganizationDiary.getDtByWhere(strwhere2);
					if (dtByWhere.Rows.Count > 0)
					{
						if (string.IsNullOrEmpty(dtByWhere.Rows[0]["Total"].ToString()))
						{
							this.txtPayMoney.Text = "0.00";
						}
						else
						{
							this.txtPayMoney.Text = dtByWhere.Rows[0]["Total"].ToString();
						}
						this.txtRPCode.Text = dtByWhere.Rows[0]["Name"].ToString();
					}
				}
				this.txtJianMoney.Text = (Convert.ToDecimal(this.txtPayMoney.Text) - Convert.ToDecimal(this.txtPayOut.Text)).ToString();
			}
			this.FileUpload1.Class = "AccountPayOut";
			this.FileUpload1.RecordCode = this.hdnAccountID.Value;
		}
	}
	private AccounPayoutModel getModel()
	{
		this.accountModel = new AccounPayoutModel();
		this.accountModel.PayOutCode = this.txtAccCode.Text.ToString();
		this.accountModel.PayOutTime = new DateTime?(Convert.ToDateTime(this.txtInDate.Text.ToString()));
		this.accountModel.PayOutMoney = new decimal?(Convert.ToDecimal(this.txtInMoney.Text.ToString()));
		this.accountModel.PayOutPeople = this.hdnPeopleCode.Value;
		this.accountModel.PayOutGuid = new Guid(this.hdnAccountID.Value);
		this.accountModel.prjGuid = this.hdnProjectCode.Value;
		this.accountModel.Remark = this.txtRemark.Text.ToString();
		this.accountModel.Handler = this.txtHandler.Text.ToString();
		this.accountModel.RPGuid = this.hdnRPUID.Value.ToString();
		this.accountModel.UpdateTime = new DateTime?(DateTime.Now);
		this.accountModel.UpdateUser = base.UserCode;
		this.accountModel.Type = new int?(Convert.ToInt32(base.Request.QueryString["Sub"].ToString()));
		return this.accountModel;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.accountModel = this.getModel();
		if (this.AccountBll.Exists(this.accountModel.PayOutCode, this.accountModel.PayOutGuid))
		{
			stringBuilder.Append("top.ui.alert('账单编号重复，请重新输入');");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		if (base.Request.QueryString["Action"] == "Add")
		{
			this.accountModel.FloeState = new int?(-1);
			this.AccountBll.Add(this.accountModel);
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_accountpayout' }); ");
			return;
		}
		this.AccountBll.Update(this.accountModel);
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_accountpayout' }); ");
	}
	protected void btnPlan_Click(object sender, EventArgs e)
	{
		string value = this.hdnRPUID.Value;
		if (this.HdnSub.Value == "0")
		{
			PayoutPayment payoutPayment = new PayoutPayment();
			PayoutPaymentModel model = payoutPayment.GetModel(value);
			this.txtRPCode.Text = model.PaymentCode.ToString();
			this.txtPayMoney.Text = model.PaymentMoney.ToString();
			this.txtPayOut.Text = this.AccountBll.getMoneyByPayCode(model.ID).ToString();
			this.txtInMoney.Text = (model.PaymentMoney - Convert.ToDecimal(this.txtPayOut.Text)).ToString();
			this.txtJianMoney.Text = this.txtInMoney.Text;
			this.txtHandler.Text = model.InputPerson;
			return;
		}
		this.txtPayOut.Text = this.AccountBll.getMoneyByPayCode(value).ToString();
		string strwhere = " inDiaryId ='" + value + "' ";
		DataTable dtByWhere = OrganizationDiary.getDtByWhere(strwhere);
		if (dtByWhere.Rows.Count > 0)
		{
			if (string.IsNullOrEmpty(dtByWhere.Rows[0]["Total"].ToString()))
			{
				this.txtPayMoney.Text = "0.00";
			}
			else
			{
				this.txtPayMoney.Text = dtByWhere.Rows[0]["Total"].ToString();
			}
			this.txtRPCode.Text = dtByWhere.Rows[0]["Name"].ToString();
			this.txtInMoney.Text = (Convert.ToDecimal(this.txtPayMoney.Text) - Convert.ToDecimal(this.txtPayOut.Text)).ToString();
			this.txtJianMoney.Text = this.txtInMoney.Text;
			this.txtHandler.Text = dtByWhere.Rows[0]["issuedBy"].ToString();
		}
	}
}


