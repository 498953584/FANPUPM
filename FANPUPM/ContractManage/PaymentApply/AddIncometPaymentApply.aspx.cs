using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using com.jwsoft.pm.entpm;
using System;
using System.Drawing;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PaymentApply_AddIncometPaymentApply : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private PaymentApply PaymentApply = new PaymentApply();

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
		else
		{
			this.trSate.Visible = false;
		}
		if (!string.IsNullOrEmpty(base.Request["Id"]))
		{
			this.ViewState["PaymentId"] = base.Request["Id"];
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
			if (string.Compare(this.action, "Add", true) == 0)
			{
				this.InitAddState();
				this.TxtCapitalNumber.Value = ConverRMB.Convert(Convert.ToDecimal(0.0));
			}
			else
			{
				this.InintUpdateState();
			}
			this.hlfdAction.Value = this.action;
			this.FileLink1.MID = 1913;
			this.FileLink1.FID = this.ViewState["PaymentId"].ToString();
			this.FileLink1.Type = 1;
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (string.Compare(this.action, "Add", true) == 0)
		{
			this.AddPayment();
			return;
		}
		this.UpdatePayment();
	}
	protected void chkContainPendint_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			if (string.IsNullOrEmpty(this.contractId))
			{
				IncomentPaymentApplyModel byId = this.PaymentApply.GetById(this.ViewState["PaymentId"].ToString());
				this.contractId = byId.ContractId;
			}
			IncomentPaymentApplyModel byId2 = this.PaymentApply.GetById(this.ViewState["PaymentId"].ToString());
			this.txtPaymentCode.Value = byId2.Code;
			decimal d = decimal.Parse(this.lblPaymentSum.Text);
			decimal paySum = this.PaymentApply.GetPaySum(this.contractId, this.chkContainPending.Checked);
			this.lblPaySum.Text = paySum.ToString("0.000");
			decimal num = d - paySum;
			this.txtDiff.Text = num.ToString("0.000");
		}
		catch
		{
		}
	}
	private void InitContractInfo(string conId)
	{
		IncometContractBll incometContractBll = new IncometContractBll();
		IncometContractModel model = incometContractBll.GetModel(conId);
		this.txtContractCode.Text = model.ContractCode;
		this.txtContractName.Text = model.ContractName;
		new PayoutBalance().GetBalancedAmount(conId, false);
		this.lblPaymentSum.Text = WebUtil.GetPaymentSum(model.ContractID, "Con_Incomet_Payment", "CllectionPrice");
		decimal paySum = this.PaymentApply.GetPaySum(conId, this.chkContainPending.Checked);
		this.lblPaySum.Text = paySum.ToString("0.000");
		decimal num = Convert.ToDecimal(this.lblPaymentSum.Text) - paySum;
		this.txtDiff.Text = num.ToString("0.000");
		this.txtContractMoney.Text = model.ContractPrice.ToString();
		this.txtSignedDate.Text = model.SignedTime.Value.ToShortDateString();
	}
	private void InitAddState()
	{
		this.ViewState["PaymentId"] = Guid.NewGuid().ToString();
		this.hlfdContractId.Value = this.contractId;
		this.InitContractInfo(this.contractId);
		this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
		this.txtInputDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
	}
	private void InintUpdateState()
	{
		IncomentPaymentApplyModel byId = this.PaymentApply.GetById(this.ViewState["PaymentId"].ToString());
		this.contractId = byId.ContractId;
		this.hlfdContractId.Value = byId.ContractId;
		this.txtPaymentCode.Value = byId.Code;
		this.txtPaymentPerson.Text = byId.PaymentPenson;
		this.txtPaymentDate.Text = byId.PaymentDate.ToShortDateString();
		this.txtPaymentMoney.Text = byId.PaymentAmount.ToString("0.000");
		this.txtInputDate.Text = byId.InputDate.ToString("yyyy-MM-dd");
		this.txtInputPerson.Text = byId.InputPerson;
		this.TxtCapitalNumber.Value = ConverRMB.Convert(byId.PaymentAmount);
		this.txtNotes.Text = byId.Notes;
		this.chkContainPending.Checked = byId.ContainPending;
		string arg_104_0 = byId.PaymentMode;
		if (byId.PaymentMode != null && byId.PaymentMode.ToString() != "" && byId.PaymentMode != "-1")
		{
			this.RblPayType.SelectedValue = byId.PaymentMode.ToString();
		}
		this.InitContractInfo(this.contractId);
	}
	private void AddPayment()
	{
		IncomentPaymentApplyModel incomentPaymentApplyModel = new IncomentPaymentApplyModel();
		incomentPaymentApplyModel.Id = this.ViewState["PaymentId"].ToString();
		incomentPaymentApplyModel.ContractId = this.contractId;
		incomentPaymentApplyModel.Code = this.txtPaymentCode.Value.Trim();
		incomentPaymentApplyModel.PaymentPenson = this.txtPaymentPerson.Text.Trim();
		incomentPaymentApplyModel.PaymentAmount = Convert.ToDecimal(this.txtPaymentMoney.Text.Trim());
		incomentPaymentApplyModel.PaymentDate = Convert.ToDateTime(this.txtPaymentDate.Text.Trim());
		if (this.RblPayType.SelectedIndex == -1)
		{
			incomentPaymentApplyModel.PaymentMode = "-1";
		}
		else
		{
			incomentPaymentApplyModel.PaymentMode = this.RblPayType.SelectedValue;
		}
		incomentPaymentApplyModel.FlowState = -1;
		incomentPaymentApplyModel.Notes = this.txtNotes.Text.Trim();
		incomentPaymentApplyModel.InputPerson = this.txtInputPerson.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtInputPerson.Text.Trim()))
		{
			incomentPaymentApplyModel.InputDate = DateTime.Now;
		}
		incomentPaymentApplyModel.ContainPending = this.chkContainPending.Checked;
		try
		{
			if (this.PaymentApply.IsExists(incomentPaymentApplyModel.Code, incomentPaymentApplyModel.ContractId))
			{
				base.RegisterScript("top.ui.alert('此支付编号已经存在')");
			}
			else
			{
				this.PaymentApply.Add(incomentPaymentApplyModel);
				StringBuilder stringBuilder = new StringBuilder();
				if (ContractParameter.IsBalanceAlarm && this.PaymentApply.GreaterBalanceMoney(incomentPaymentApplyModel))
				{
					stringBuilder.Append("top.ui.show('支付金额大于收入金额\\n添加成功!')");
				}
				else
				{
					stringBuilder.Append("top.ui.show('添加成功!')");
				}
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("winclose('AddIncometPaymentApply', 'ShowPaymentApplyList.aspx?ContractID=" + incomentPaymentApplyModel.ContractId + "', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
		}
		catch
		{
			base.RegisterHintScript("Add", false, string.Empty);
		}
	}
	private void UpdatePayment()
	{
		IncomentPaymentApplyModel byId = this.PaymentApply.GetById(this.ViewState["PaymentId"].ToString());
		if (!string.IsNullOrEmpty(this.txtPaymentMoney.Text.Trim()))
		{
			byId.PaymentAmount = Convert.ToDecimal(this.txtPaymentMoney.Text.Trim());
		}
		if (!string.IsNullOrEmpty(this.txtPaymentDate.Text.Trim()))
		{
			byId.PaymentDate = Convert.ToDateTime(this.txtPaymentDate.Text.Trim());
		}
		byId.PaymentPenson = this.txtPaymentPerson.Text.Trim();
		byId.Notes = this.txtNotes.Text.Trim();
		byId.ContainPending = this.chkContainPending.Checked;
		if (this.RblPayType.SelectedIndex == -1)
		{
			byId.PaymentMode = "-1";
		}
		else
		{
			byId.PaymentMode = this.RblPayType.SelectedValue;
		}
		try
		{
			this.PaymentApply.Update(byId);
			StringBuilder stringBuilder = new StringBuilder();
			if (ContractParameter.IsBalanceAlarm && this.PaymentApply.GreaterBalanceMoney(byId))
			{
				stringBuilder.Append("top.ui.show('支付金额大于结算金额\\n修改成功!')");
			}
			else
			{
				stringBuilder.Append("top.ui.show('修改成功!')");
			}
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("winclose('AddIncometPaymentApply', 'ShowPaymentApplyList.aspx?ContractID=" + byId.ContractId + "', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch (Exception)
		{
			base.RegisterHintScript("Update", false, string.Empty);
		}
	}
	private void SetLableColor(Color c)
	{
		this.lblState.ForeColor = c;
		this.lblBalancedAmount.ForeColor = c;
		this.lblPaymentedAmount.ForeColor = c;
		this.lblPaymentAmount.ForeColor = c;
		this.lblRate.ForeColor = c;
	}
}


