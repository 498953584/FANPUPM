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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutPayment_PaymentEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private PayoutPayment payoutPayment = new PayoutPayment();
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
		else
		{
			this.trSate.Visible = false;
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
			if (string.Compare(this.action, "Add", true) == 0)
			{
				this.InitAddState();
			}
			else
			{
				this.InintUpdateState();
				this.ShowGuideLine();
			}
			this.hldfContractId.Value = this.contractId;
			this.FileLink1.MID = 1904;
			this.FileLink1.FID = this.ViewState["PaymentId"].ToString();
			this.hfldPaymentId.Value = this.ViewState["PaymentId"].ToString();
			this.FileLink1.Type = 1;
			this.hldfIsFundPlan.Value = ConfigHelper.Get("IsFundPlan");
			this.BindTarget();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (string.Compare(this.action, "Add", true) == 0)
		{
			this.AddPayment();
		}
		else
		{
			this.UpdatePayment();
		}
		this.SaveTarget();
	}
	protected void chkContainPendint_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			if (string.IsNullOrEmpty(this.contractId))
			{
				PayoutPaymentModel model = this.payoutPayment.GetModel(this.ViewState["PaymentId"].ToString());
				this.contractId = model.ContractID;
			}
			decimal d = decimal.Parse(this.txtBalancedSum.Text);
			decimal paySum = this.payoutPayment.GetPaySum(this.contractId, this.chkContainPending.Checked);
			this.txtPaySum.Text = paySum.ToString();
			decimal num = d - paySum;
			this.txtDiff.Text = num.ToString();
			if (Convert.ToDecimal(this.txtPlanMoney.Value.Trim()) < Convert.ToDecimal(this.txtUsedMoney.Value.Trim()))
			{
				this.lblUsableMoney.Attributes.Add("style", "display:inline;color:Red;");
			}
		}
		catch
		{
		}
	}
	private void InitContractInfo(string conId)
	{
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(conId);
		this.txtContractCode.Text = model.ContractCode;
		this.txtContractName.Text = model.ContractName;
		decimal balancedAmount = new PayoutBalance().GetBalancedAmount(conId, false);
		this.txtBalancedSum.Text = balancedAmount.ToString();
		decimal paySum = this.payoutPayment.GetPaySum(conId, this.chkContainPending.Checked);
		this.txtPaySum.Text = paySum.ToString();
		decimal num = balancedAmount - paySum;
		this.txtDiff.Text = num.ToString();
		this.txtContractMoney.Text = model.ModifiedMoney.ToString();
		this.txtSignedDate.Text = model.SignDate.Value.ToShortDateString();
	}
	private void InitAddState()
	{
		this.ViewState["PaymentId"] = Guid.NewGuid().ToString();
		this.InitContractInfo(this.contractId);
		this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
		this.txtInputDate.Text = DateTime.Now.ToShortDateString();
		this.hlfdFundPlanUID.Value = this.payoutPayment.GetCurrentMonthUID(this.contractId);
		List<string> fundPlanByMonthPlanUID = this.payoutPayment.GetFundPlanByMonthPlanUID(this.hlfdFundPlanUID.Value);
		if (fundPlanByMonthPlanUID.Count > 0)
		{
			this.txtMonthDate.Value = fundPlanByMonthPlanUID[1].ToString();
			this.txtPlanMoney.Value = fundPlanByMonthPlanUID[2].ToString();
			this.txtUsedMoney.Value = fundPlanByMonthPlanUID[4].ToString();
			this.lblUsableMoney.Value = fundPlanByMonthPlanUID[5].ToString();
			this.txtRemark.Value = fundPlanByMonthPlanUID[6].ToString();
			Convert.ToDecimal(fundPlanByMonthPlanUID[4]);
			decimal d = Convert.ToDecimal(fundPlanByMonthPlanUID[5]);
			if (d < 0m)
			{
				this.lblUsableMoney.Attributes.Add("style", "display:inline;color:Red;");
			}
		}
	}
	private void InintUpdateState()
	{
		PayoutPaymentModel model = this.payoutPayment.GetModel(this.ViewState["PaymentId"].ToString());
		this.contractId = model.ContractID;
		this.txtPaymentCode.Text = model.PaymentCode;
		this.txtPaymentMoney.Text = model.PaymentMoney.ToString();
		this.txtPaymentDate.Text = model.PaymentDate.Value.ToShortDateString();
		this.txtPaymentPerson.Text = model.PaymentPerson;
		this.txtInputPerson.Text = model.InputPerson;
		this.txtInputDate.Text = model.InputDate.Value.ToShortDateString();
		this.txtNotes.Text = model.Notes;
		this.chkContainPending.Checked = model.ContainPending;
		this.txtCapitalNumber.Text = model.Capitalnumber;
		this.txtBeneficiary.Text = model.Beneficiary;
		this.txtBank.Text = model.Bank;
		this.txtAccount.Text = model.Account;
		int arg_131_0 = model.Paytype;
		if (model.Paytype.ToString() != "" && model.Paytype != -1)
		{
			this.RblPayType.SelectedValue = model.Paytype.ToString();
		}
		this.hlfdFundPlanUID.Value = model.MonthPlanUID;
		List<string> fundPlanByMonthPlanUID = this.payoutPayment.GetFundPlanByMonthPlanUID(this.hlfdFundPlanUID.Value);
		if (fundPlanByMonthPlanUID.Count > 0)
		{
			this.txtMonthDate.Value = fundPlanByMonthPlanUID[1].ToString();
			this.txtPlanMoney.Value = fundPlanByMonthPlanUID[2].ToString();
			this.txtUsedMoney.Value = fundPlanByMonthPlanUID[4].ToString();
			this.lblUsableMoney.Value = fundPlanByMonthPlanUID[5].ToString();
			this.txtRemark.Value = fundPlanByMonthPlanUID[6].ToString();
			Convert.ToDecimal(fundPlanByMonthPlanUID[4]);
			decimal d = Convert.ToDecimal(fundPlanByMonthPlanUID[5]);
			if (d < 0m)
			{
				this.lblUsableMoney.Attributes.Add("style", "display:inline;color:Red;");
			}
		}
		this.InitContractInfo(this.contractId);
		this.txtPaymentCode.Enabled = false;
	}
	private void AddPayment()
	{
		PayoutPaymentModel payoutPaymentModel = new PayoutPaymentModel();
		payoutPaymentModel.ID = this.ViewState["PaymentId"].ToString();
		payoutPaymentModel.ContractID = this.contractId;
		payoutPaymentModel.PaymentCode = this.txtPaymentCode.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtPaymentMoney.Text.Trim()))
		{
			payoutPaymentModel.PaymentMoney = new decimal?(Convert.ToDecimal(this.txtPaymentMoney.Text.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtPaymentDate.Text.Trim()))
		{
			payoutPaymentModel.PaymentDate = new DateTime?(Convert.ToDateTime(this.txtPaymentDate.Text.Trim()));
		}
		payoutPaymentModel.PaymentPerson = this.txtPaymentPerson.Text.Trim();
		payoutPaymentModel.Annex = string.Empty;
		payoutPaymentModel.FlowState = new int?(-1);
		payoutPaymentModel.Notes = this.txtNotes.Text.Trim();
		payoutPaymentModel.InputPerson = this.txtInputPerson.Text.Trim();
		payoutPaymentModel.ContainPending = this.chkContainPending.Checked;
		payoutPaymentModel.Beneficiary = this.txtBeneficiary.Text.Trim();
		payoutPaymentModel.Bank = this.txtBank.Text.Trim();
		payoutPaymentModel.Account = this.txtAccount.Text.Trim();
		if (this.RblPayType.SelectedIndex == -1)
		{
			payoutPaymentModel.Paytype = -1;
		}
		else
		{
			payoutPaymentModel.Paytype = int.Parse(this.RblPayType.SelectedValue);
		}
		payoutPaymentModel.Capitalnumber = this.txtCapitalNumber.Text.Trim().ToString();
		if (this.hldfIsFundPlan.Value != "0")
		{
			if (this.hlfdFundPlanUID.Value.Trim() != "")
			{
				payoutPaymentModel.MonthPlanUID = this.hlfdFundPlanUID.Value;
			}
			else
			{
				payoutPaymentModel.MonthPlanUID = null;
			}
		}
		else
		{
			payoutPaymentModel.MonthPlanUID = null;
		}
		if (!string.IsNullOrEmpty(this.txtInputPerson.Text.Trim()))
		{
			payoutPaymentModel.InputDate = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text.Trim()));
		}
		try
		{
			if (this.payoutPayment.IsExists(payoutPaymentModel.PaymentCode, payoutPaymentModel.ContractID))
			{
				base.RegisterScript("hideFundPlan(); top.ui.alert('此支付编号已经存在')");
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = this.configSer.IsExist(this.contractId) && (
					from p in this.configSer
					where p.ContractId == this.contractId
					select p).FirstOrDefault<ConConfigContract>().IsPaymentAlarm;
				if (this.hldfIsFundPlan.Value != "0" && Convert.ToDecimal(this.txtPaymentMoney.Text.Trim()) > Convert.ToDecimal(this.lblUsableMoney.Value.Trim()))
				{
					base.RegisterScript("hideFundPlan(); top.ui.alert('支付金额大于计划可用金额')");
				}
				this.payoutPayment.Add(payoutPaymentModel);
				if (flag && this.payoutPayment.GreaterBalanceMoney(payoutPaymentModel))
				{
					stringBuilder.Append("hideFundPlan(); top.ui.alert('支付金额大于结算金额\\n添加成功')");
				}
				else
				{
					stringBuilder.Append("hideFundPlan(); top.ui.show('添加成功')");
				}
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("winclose('PaymentEdit', 'PayoutPaymentEdit.aspx?ContractID=" + this.contractId + "', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
		}
		catch (Exception)
		{
			base.RegisterHintScript("Add", false, string.Empty);
		}
	}
	private void UpdatePayment()
	{
		PayoutPaymentModel payment = this.payoutPayment.GetModel(this.ViewState["PaymentId"].ToString());
		if (!string.IsNullOrEmpty(this.txtPaymentMoney.Text.Trim()))
		{
			payment.PaymentMoney = new decimal?(Convert.ToDecimal(this.txtPaymentMoney.Text.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtPaymentDate.Text.Trim()))
		{
			payment.PaymentDate = new DateTime?(Convert.ToDateTime(this.txtPaymentDate.Text.Trim()));
		}
		payment.PaymentPerson = this.txtPaymentPerson.Text.Trim();
		payment.Notes = this.txtNotes.Text.Trim();
		payment.ContainPending = this.chkContainPending.Checked;
		if (this.hldfIsFundPlan.Value.Trim() != "0")
		{
			if (this.hlfdFundPlanUID.Value.Trim() != "")
			{
				payment.MonthPlanUID = this.hlfdFundPlanUID.Value;
			}
			else
			{
				payment.MonthPlanUID = null;
			}
		}
		else
		{
			payment.MonthPlanUID = null;
		}
		if (this.RblPayType.SelectedIndex == -1)
		{
			payment.Paytype = -1;
		}
		else
		{
			payment.Paytype = int.Parse(this.RblPayType.SelectedValue);
		}
		payment.Capitalnumber = this.txtCapitalNumber.Text.Trim();
		payment.Beneficiary = this.txtBeneficiary.Text.Trim();
		payment.Bank = this.txtBank.Text.Trim();
		payment.Account = this.txtAccount.Text.Trim();
		try
		{
			if (this.hldfIsFundPlan.Value != "0" && Convert.ToDecimal(this.txtPaymentMoney.Text.Trim()) > Convert.ToDecimal(this.lblUsableMoney.Value.Trim()))
			{
				base.RegisterScript("hideFundPlan(); top.ui.alert('本次支付金额大于计划可用金额');");
			}
			this.payoutPayment.Update(payment);
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = this.configSer.IsExist(payment.ContractID) && (
				from p in this.configSer
				where p.ContractId == payment.ContractID
				select p).FirstOrDefault<ConConfigContract>().IsPaymentAlarm;
			if (flag && this.payoutPayment.GreaterBalanceMoney(payment))
			{
				stringBuilder.Append("hideFundPlan(); top.ui.alert('支付金额大于结算金额\\n修改成功');");
			}
			else
			{
				stringBuilder.Append("hideFundPlan(); top.ui.show('修改成功');");
			}
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("winclose('PaymentEdit', 'PayoutPaymentEdit.aspx?ContractID=" + payment.ContractID + "', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch (Exception)
		{
			base.RegisterHintScript("Update", false, string.Empty);
		}
	}
	private void ShowGuideLine()
	{
		try
		{
			string iD = this.ViewState["PaymentId"].ToString();
			PayoutPaymentModel model = this.payoutPayment.GetModel(iD);
			decimal num = decimal.Parse(this.txtBalancedSum.Text);
			this.lblBalancedAmount.Text = this.txtBalancedSum.Text;
			decimal d = decimal.Parse(this.txtPaySum.Text);
			this.lblPaymentedAmount.Text = this.txtPaySum.Text;
			decimal d2 = model.PaymentMoney.HasValue ? model.PaymentMoney.Value : 0m;
			this.lblPaymentAmount.Text = d2.ToString();
			if (num != 0m)
			{
				decimal num2 = (num - d - d2) / num;
				this.lblRate.Text = string.Format("{0:P}", num2);
				string stateByBalanceAmount = this.payoutPayment.GetStateByBalanceAmount(num2);
				this.lblState.Text = stateByBalanceAmount;
				this.SetLableColor(Common2.GetColorByState(stateByBalanceAmount));
			}
			else
			{
				this.lblRate.Text = "无支付结算";
				this.lblState.Text = "高";
				this.SetLableColor(Common2.GetColorByState("高"));
			}
		}
		catch
		{
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
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnBindTarget_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldTargetCheckeds.Value))
		{
			List<string> list = new List<string>();
			if (this.hfldTargetCheckeds.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.hfldTargetCheckeds.Value);
			}
			else
			{
				list.Add(this.hfldTargetCheckeds.Value);
			}
			PaymentTarget paymentTarget = new PaymentTarget();
			DataTable dataTable = paymentTarget.GetPaymentTarget(list, this.hldfContractId.Value, "", this.hfldIsWBSRelevance.Value.Trim());
			this.UpdateTargetTable();
			DataTable dataTable2 = this.ViewState["dtTarget"] as DataTable;
			if (dataTable2 != null)
			{
				dataTable2.PrimaryKey = new DataColumn[]
				{
					dataTable2.Columns["TargetId"]
				};
				dataTable.PrimaryKey = new DataColumn[]
				{
					dataTable.Columns["TargetId"]
				};
				dataTable2.Merge(dataTable, true);
				dataTable = dataTable2;
			}
			this.ViewState["dtTarget"] = dataTable;
			this.gvBudget.DataSource = dataTable;
			this.gvBudget.DataBind();
		}
	}
	protected void btnDelTarget_Click(object sender, EventArgs e)
	{
		this.UpdateTargetTable();
		this.delRow();
	}
	protected void delRow()
	{
		DataTable dataTable = (DataTable)this.ViewState["dtTarget"];
		if (this.gvBudget.Rows.Count > 0)
		{
			for (int i = this.gvBudget.Rows.Count - 1; i >= 0; i--)
			{
				GridViewRow gridViewRow = this.gvBudget.Rows[i];
				CheckBox checkBox = (CheckBox)gridViewRow.FindControl("chkBox");
				if (checkBox.Checked)
				{
					dataTable.Rows.RemoveAt(gridViewRow.RowIndex);
				}
			}
			this.ViewState["dtTarget"] = dataTable;
			this.gvBudget.DataSource = (DataTable)this.ViewState["dtTarget"];
			this.gvBudget.DataBind();
		}
	}
	protected void UpdateTargetTable()
	{
		DataTable dataTable = this.ViewState["dtTarget"] as DataTable;
		if (dataTable != null && dataTable.Rows.Count == this.gvBudget.Rows.Count)
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				GridViewRow gridViewRow = this.gvBudget.Rows[i];
				HtmlInputText htmlInputText = (HtmlInputText)gridViewRow.FindControl("txtTheMoney");
				if (htmlInputText != null)
				{
					if (!string.IsNullOrEmpty(htmlInputText.Value.Trim()))
					{
						dataRow["CurrentPaymentAmount"] = htmlInputText.Value.Trim();
					}
					else
					{
						dataRow["CurrentPaymentAmount"] = 0m;
					}
				}
			}
		}
	}
	protected void SaveTarget()
	{
		this.UpdateTargetTable();
		List<PaymentTargetModel> list = new List<PaymentTargetModel>();
		for (int i = 0; i < this.gvBudget.Rows.Count; i++)
		{
			PaymentTargetModel paymentTargetModel = new PaymentTargetModel();
			paymentTargetModel.Id = Guid.NewGuid().ToString();
			paymentTargetModel.ConTargetId = this.gvBudget.DataKeys[i].Value.ToString();
			paymentTargetModel.PaymentId = this.hfldPaymentId.Value;
			string value = ((HtmlInputText)this.gvBudget.Rows[i].FindControl("txtTheMoney")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				paymentTargetModel.PaymentAmount = Convert.ToDecimal(value);
			}
			else
			{
				paymentTargetModel.PaymentAmount = 0m;
			}
			paymentTargetModel.InputDate = DateTime.Now;
			paymentTargetModel.InputUser = base.UserCode;
			list.Add(paymentTargetModel);
		}
		PaymentTarget paymentTarget = new PaymentTarget();
		paymentTarget.Add(list, this.hfldPaymentId.Value);
	}
	private void BindTarget()
	{
		PaymentTarget paymentTarget = new PaymentTarget();
		DataTable paymentTarget2 = paymentTarget.GetPaymentTarget(null, this.hldfContractId.Value.Trim(), this.hfldPaymentId.Value.Trim(), this.hfldIsWBSRelevance.Value.Trim());
		this.ViewState["dtTarget"] = paymentTarget2;
		this.gvBudget.DataSource = paymentTarget2;
		this.gvBudget.DataBind();
	}
}


