using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Fund.AccounIncome.BLL;
using cn.justwin.Fund.AccounIncome.Model;
using cn.justwin.Fund.Account;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Fund.Account;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccounIncome_AccounIncomeEdit : NBasePage, IRequiresSessionState
{
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private Fund_Plan_MonthMainAction PlanAction = new Fund_Plan_MonthMainAction();
	private AccounIncomeModel accountModel;
	private AccounIncome AccountBll = new AccounIncome();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private AccounModel aM;
	private AccountLogic al = new AccountLogic();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ddlType.Attributes.Add("onchange", "TypeChange()");
			this.hdnZHID.Value = base.Request.QueryString["ZHID"].ToString();
			if (base.Request.QueryString["Action"] == "Add")
			{
				this.txtAccCode.Text = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				this.hdnAccountID.Value = Guid.NewGuid().ToString();
				this.txtInDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtInPeople.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, base.UserCode);
				this.hdnPeopleCode.Value = base.UserCode;
				this.DaGet.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
				this.txtAccCode.Text = this.accountModel.InCode.ToString();
				this.hdnAccountID.Value = this.accountModel.InUid.ToString();
				this.txtInDate.Text = Convert.ToDateTime(this.accountModel.InDate).ToString("yyyy-MM-dd");
				this.txtInPeople.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.accountModel.InPeople);
				this.hdnPeopleCode.Value = this.accountModel.InPeople;
				this.DaGet.Text = Convert.ToDateTime(this.accountModel.GetDate).ToString("yyyy-MM-dd");
				this.hdnProjectCode.Value = this.accountModel.PrjGuid.ToString();
				PrjInfoModel modelByPrjGuid2 = this.ptPrjInfo.GetModelByPrjGuid(this.accountModel.PrjGuid);
				if (modelByPrjGuid2 != null)
				{
					this.txtPrjName.Text = modelByPrjGuid2.PrjName;
				}
				this.hdnContCode.Value = this.accountModel.ContractID.ToString();
				this.hdnPlanUid.Value = this.accountModel.PlanUid.ToString();
				this.ddlType.SelectedValue = this.accountModel.Subject.ToString();
				this.txtRemark.Text = this.accountModel.Remark.ToString();
				DataTable allNews = this.AccountBll.GetAllNews(this.accountModel.ContractID);
				if (allNews.Rows.Count > 0)
				{
					this.hdnPlanUid.Value = allNews.Rows[0]["UID"].ToString();
					if (!string.IsNullOrEmpty(allNews.Rows[0]["PlanDate"].ToString()))
					{
						this.txtPlan.Text = Convert.ToDateTime(allNews.Rows[0]["PlanDate"].ToString()).ToString("yyyy年MM月");
						this.txtPlanMoney.Text = allNews.Rows[0]["ThisBalance"].ToString();
					}
					else
					{
						this.txtPlan.Text = "";
						this.txtPlanMoney.Text = "0.00";
					}
					this.txtContName.Text = allNews.Rows[0]["cllectionCode"].ToString();
				}
				this.txtInMoney.Text = this.accountModel.InMoney.ToString();
				this.txtGetMoney.Text = this.accountModel.GetMoney.ToString();
			}
			this.FileUpload1.Class = "AccounIncome";
			this.FileUpload1.RecordCode = this.hdnAccountID.Value;
		}
	}
	private AccounIncomeModel getModel()
	{
		this.accountModel = new AccounIncomeModel();
		this.accountModel.ContractID = this.hdnContCode.Value;
		this.accountModel.GetDate = new DateTime?(Convert.ToDateTime(this.DaGet.Text.ToString()));
		if (string.IsNullOrEmpty(this.txtGetMoney.Text.ToString()))
		{
			this.accountModel.GetMoney = new decimal?(0.00m);
		}
		else
		{
			this.accountModel.GetMoney = new decimal?(Convert.ToDecimal(this.txtGetMoney.Text.ToString()));
		}
		this.accountModel.InCode = this.txtAccCode.Text.ToString();
		this.accountModel.InDate = new DateTime?(Convert.ToDateTime(this.txtInDate.Text.ToString()));
		this.accountModel.InMoney = new decimal?(Convert.ToDecimal(this.txtInMoney.Text.ToString()));
		this.accountModel.InPeople = this.hdnPeopleCode.Value;
		this.accountModel.InUid = new Guid(this.hdnAccountID.Value);
		if (this.hdnPlanUid.Value != "")
		{
			this.accountModel.PlanUid = this.hdnPlanUid.Value;
		}
		else
		{
			this.accountModel.PlanUid = default(Guid).ToString();
		}
		this.accountModel.PrjGuid = this.hdnProjectCode.Value;
		this.accountModel.Remark = this.txtRemark.Text.ToString();
		this.accountModel.Subject = this.ddlType.SelectedValue;
		return this.accountModel;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (this.ddlType.SelectedValue == "0")
		{
			if (string.IsNullOrEmpty(this.hdnContCode.Value))
			{
				stringBuilder.Append("top.ui.alert('合同入账，请选择依据合同');");
				base.RegisterScript(stringBuilder.ToString());
				return;
			}
			if (string.IsNullOrEmpty(this.txtGetMoney.Text) || Convert.ToDecimal(this.txtGetMoney.Text) <= 0m)
			{
				base.RegisterScript("top.ui.alert('回款金额不能为空或小于等于零');");
				return;
			}
			if (Convert.ToDecimal(this.txtInMoney.Text) - Convert.ToDecimal(this.txtGetMoney.Text) > 0m)
			{
				base.RegisterScript("top.ui.alert('入账金额不能大于回款金额');");
				return;
			}
		}
		this.accountModel = this.getModel();
		if (base.Request.QueryString["Action"] == "Add")
		{
			this.accountModel.FlowState = new int?(-1);
			if (!this.AccountBll.Exists(this.accountModel.InCode))
			{
				this.AccountBll.Add(this.accountModel);
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_accountincome' });");
			}
			else
			{
				base.RegisterScript("top.ui.alert('入账单编号重复，请重新输入');");
			}
		}
		else
		{
			if (this.AccountBll.Update(this.accountModel))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_accountincome' });");
			}
		}
		base.RegisterScript(stringBuilder.ToString());
	}
	protected void btnPlan_Click(object sender, EventArgs e)
	{
		string value = this.hdnContCode.Value;
		DataTable allNews = this.AccountBll.GetAllNews(value);
		if (allNews.Rows.Count > 0)
		{
			this.hdnPlanUid.Value = allNews.Rows[0]["UID"].ToString();
			if (!string.IsNullOrEmpty(allNews.Rows[0]["PlanDate"].ToString()))
			{
				this.txtPlan.Text = Convert.ToDateTime(allNews.Rows[0]["PlanDate"].ToString()).ToString("yyyy年MM月");
				this.txtPlanMoney.Text = allNews.Rows[0]["ThisBalance"].ToString();
			}
			else
			{
				this.txtPlan.Text = "";
				this.txtPlanMoney.Text = "0.00";
			}
			this.txtContName.Text = allNews.Rows[0]["cllectionCode"].ToString();
			this.txtGetMoney.Text = allNews.Rows[0]["cllectionPrice"].ToString();
		}
	}
}


