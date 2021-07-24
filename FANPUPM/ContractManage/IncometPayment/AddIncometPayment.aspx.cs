using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometPayment_AddIncometPayment : NBasePage, IRequiresSessionState
{
	private string contractId = string.Empty;
	private IncometPaymentBll incometPaymentBll = new IncometPaymentBll();
	private IncometContractBll incometContractBll = new IncometContractBll();

	protected override void OnInit(EventArgs e)
	{
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
			this.hldfIsFundPlan.Value = ConfigHelper.Get("IsFundPlan");
			this.hldfIsExamineApprove.Value = "0";
			this.InitPage();
		}
	}
	public void InitPage()
	{
		if (base.Request.QueryString["id"] != null)
		{
			this.lblTitle.Text = "编辑收入合同收款";
			IncometPaymentModel model = this.incometPaymentBll.GetModel(base.Request.QueryString["id"]);
			this.txtCllectionCode.Text = model.CllectionCode;
			this.hdCode.Value = model.CllectionCode;
			this.txtCllectionPrice.Text = model.CllectionPrice.ToString();
			this.txtCllectionTime.Text = Common2.GetTime(model.CllectionTime.ToString());
			this.txtCllectionUser.Text = model.CllectionUser;
			this.txtInputDate.Text = Common2.GetTime(model.InputDate.ToString());
			this.txtInputPerson.Text = model.InputPerson;
			this.txtRemark.Text = model.Remark;
			this.hdGuid.Value = model.ID;
			this.hlfdFundPlanUID.Value = model.MonthPlanUID;
			if (this.hldfIsExamineApprove.Value == "0")
			{
				List<string> fundPlanByMonthPlanUID = this.incometPaymentBll.GetFundPlanByMonthPlanUID(this.hlfdFundPlanUID.Value.Trim(), false);
				if (fundPlanByMonthPlanUID.Count > 0)
				{
					this.txtMonthDate.Value = fundPlanByMonthPlanUID[1].ToString();
					this.txtPlanMoney.Value = fundPlanByMonthPlanUID[2].ToString();
					this.txtCollectedMoney.Value = fundPlanByMonthPlanUID[4].ToString();
					this.lblAllowCollectMoney.Value = fundPlanByMonthPlanUID[5].ToString();
					this.txtFundPlanRemark.Value = fundPlanByMonthPlanUID[6].ToString();
				}
			}
		}
		else
		{
			this.lblTitle.Text = "新增收入合同收款";
			this.txtInputDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
			this.hdGuid.Value = Guid.NewGuid().ToString();
			if (this.hldfIsExamineApprove.Value == "0")
			{
				this.hlfdFundPlanUID.Value = this.incometPaymentBll.GetCurrentMonthUID(this.contractId, false);
				List<string> fundPlanByMonthPlanUID2 = this.incometPaymentBll.GetFundPlanByMonthPlanUID(this.hlfdFundPlanUID.Value.Trim(), false);
				if (fundPlanByMonthPlanUID2.Count > 0)
				{
					this.txtMonthDate.Value = fundPlanByMonthPlanUID2[1].ToString();
					this.txtPlanMoney.Value = fundPlanByMonthPlanUID2[2].ToString();
					this.txtCollectedMoney.Value = fundPlanByMonthPlanUID2[4].ToString();
					this.lblAllowCollectMoney.Value = fundPlanByMonthPlanUID2[5].ToString();
					this.txtFundPlanRemark.Value = fundPlanByMonthPlanUID2[6].ToString();
				}
			}
		}
		IncometContractModel model2 = this.incometContractBll.GetModel(base.Request.QueryString["ContractID"]);
		this.txtContractCode.Text = model2.ContractCode;
		this.txtContractName.Text = model2.ContractName;
		this.txtContractPrice.Text = WebUtil.GetEnPrice(model2.ContractPrice.ToString(), model2.ContractID);
		this.txtSignedTime.Text = Common2.GetTime(model2.SignedTime.ToString());
		this.txtPrjCode.Text = ((model2.Project != null) ? model2.Project.PrjCode : string.Empty);
		this.txtPrjName.Text = ((model2.Project != null) ? model2.Project.PrjName : string.Empty);
		this.txtSumCllection.Text = WebUtil.GetPaymentSum(model2.ContractID, "Con_Incomet_Payment", "CllectionPrice");
		this.txtDiffAmount.Text = string.Concat(Convert.ToDecimal(this.txtContractPrice.Text) - Convert.ToDecimal(this.txtSumCllection.Text));
		this.FileLink1.MID = 1908;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
		this.hldfContractId.Value = model2.ContractID;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		int num;
		if (base.Request.QueryString["id"] != null)
		{
			if (this.hdCode.Value != this.txtCllectionCode.Text && this.incometPaymentBll.GetListArray(string.Concat(new string[]
			{
				" where CllectionCode='",
				this.txtCllectionCode.Text,
				"' and ContractID='",
				base.Request.QueryString["ContractID"],
				"'"
			})).Count > 0)
			{
				base.RegisterScript("hideFundPlan(); top.ui.alert('收款编号已存在')");
				return;
			}
			num = this.UpdateModel();
		}
		else
		{
			if (this.incometPaymentBll.GetListArray(string.Concat(new string[]
			{
				" where CllectionCode='",
				this.txtCllectionCode.Text,
				"' and ContractID='",
				base.Request.QueryString["ContractID"],
				"'"
			})).Count > 0)
			{
				base.RegisterScript("hideFundPlan(); top.ui.alert('收款编号已存在')");
				return;
			}
			num = this.AddModel();
		}
		if (num != 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.hldfIsFundPlan.Value != "0" && Convert.ToDecimal(this.txtCllectionPrice.Text.Trim()) > Convert.ToDecimal(this.txtPlanMoney.Value.Trim()))
			{
				stringBuilder.Append("收款金额已经超出计划金额<br />");
			}
			stringBuilder.Append(this.SetMsg() + "成功");
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append("hideFundPlan(); top.ui.show('" + stringBuilder + "'\n);");
			stringBuilder2.Append("winclose('AddIncometPayment','ShowPaymentList.aspx?ContractID=" + base.Request.QueryString["ContractID"] + "',true)");
			base.RegisterScript(stringBuilder2.ToString());
			return;
		}
		base.RegisterScript("hideFundPlan(); top.ui.alert('" + this.SetMsg() + "失败');");
	}
	public int AddModel()
	{
		return this.incometPaymentBll.Add(this.GetModel());
	}
	public int UpdateModel()
	{
		return this.incometPaymentBll.Update(this.GetModel());
	}
	public IncometPaymentModel GetModel()
	{
		IncometPaymentModel incometPaymentModel = new IncometPaymentModel();
		incometPaymentModel.Annex = "";
		incometPaymentModel.CllectionCode = this.txtCllectionCode.Text;
		incometPaymentModel.CllectionPrice = new decimal?(Convert.ToDecimal(this.txtCllectionPrice.Text));
		incometPaymentModel.CllectionTime = new DateTime?(Convert.ToDateTime(this.txtCllectionTime.Text));
		incometPaymentModel.CllectionUser = this.txtCllectionUser.Text;
		incometPaymentModel.ContractID = base.Request.QueryString["ContractID"];
		incometPaymentModel.ID = this.hdGuid.Value;
		incometPaymentModel.InputDate = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text));
		incometPaymentModel.InputPerson = this.txtInputPerson.Text;
		incometPaymentModel.Remark = this.txtRemark.Text;
		if (this.hldfIsFundPlan.Value != "0")
		{
			if (this.hlfdFundPlanUID.Value.Trim() != "")
			{
				incometPaymentModel.MonthPlanUID = this.hlfdFundPlanUID.Value;
			}
			else
			{
				incometPaymentModel.MonthPlanUID = null;
			}
		}
		else
		{
			incometPaymentModel.MonthPlanUID = null;
		}
		return incometPaymentModel;
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
}


