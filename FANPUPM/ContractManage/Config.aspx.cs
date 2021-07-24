using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_Config : NBasePage, IRequiresSessionState
{
	private ConConfigContractService configSer = new ConConfigContractService();
	private string contype = string.Empty;
	private string type = string.Empty;
	private string Id = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.Id = base.Request.QueryString["id"];
		}
		if (!string.IsNullOrEmpty(base.Request["contype"]))
		{
			this.contype = base.Request["contype"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (string.IsNullOrEmpty(this.type))
			{
				this.Init();
				return;
			}
			if (!string.IsNullOrEmpty(this.type) && !string.IsNullOrEmpty(this.Id))
			{
				this.ConfigInit(this.Id);
			}
		}
	}
	protected void ConfigInit(string ContractId)
	{
		ConConfigContract conConfigContract = (
			from p in this.configSer
			where p.ContractId == ContractId
			select p).FirstOrDefault<ConConfigContract>();
		if (conConfigContract != null)
		{
			this.chkPayoutPay.Checked = conConfigContract.IsPaymentAlarm;
			this.txtPayoutAlarmDays.Text = ((!conConfigContract.PayoutAlarmDays.HasValue) ? string.Empty : conConfigContract.PayoutAlarmDays.ToString());
			this.chkIncomePay.Checked = conConfigContract.IsIncomeAlarm;
			this.txtIncomeAlarmDays.Text = ((!conConfigContract.IncomeAlarmDays.HasValue) ? string.Empty : conConfigContract.IncomeAlarmDays.ToString());
			this.chkIsBalanceAlarm.Checked = conConfigContract.IsBalanceAlarm;
			this.txtHighBalanceAlarmLimit.Enabled = this.chkIsBalanceAlarm.Checked;
			this.txtHighBalanceAlarmLimit.Text = ((!conConfigContract.HighBalanceAlarmLimit.HasValue) ? string.Empty : conConfigContract.HighBalanceAlarmLimit.ToString());
			this.txtMidBalanceAlarmLowerLimit.Enabled = this.chkIsBalanceAlarm.Checked;
			this.txtMidBalanceAlarmLowerLimit.Text = ((!conConfigContract.MidBalanceAlarmLowerLimit.HasValue) ? string.Empty : conConfigContract.MidBalanceAlarmLowerLimit.ToString());
			this.txtMidBalanceAlarmUpperLimit.Enabled = this.chkIsBalanceAlarm.Checked;
			this.txtMidBalanceAlarmUpperLimit.Text = ((!conConfigContract.MidBalanceAlarmUpperLimit.HasValue) ? string.Empty : conConfigContract.MidBalanceAlarmUpperLimit.ToString());
			this.txtLowBalanceAlarmLowerLimit.Enabled = this.chkIsBalanceAlarm.Checked;
			this.txtLowBalanceAlarmLowerLimit.Text = ((!conConfigContract.LowBalanceAlarmLowerLimit.HasValue) ? string.Empty : conConfigContract.LowBalanceAlarmLowerLimit.ToString());
			this.txtLowBalanceAlarmUpperLimit.Enabled = this.chkIsBalanceAlarm.Checked;
			this.txtLowBalanceAlarmUpperLimit.Text = ((!conConfigContract.LowBalanceAlarmUpperLimit.HasValue) ? string.Empty : conConfigContract.LowBalanceAlarmUpperLimit.ToString());
			this.chkIsPaymentAlarm.Checked = conConfigContract.IsPaymentAlarm;
			this.txtHighPayAlarmLimit.Enabled = conConfigContract.IsPaymentAlarm;
			this.txtHighPayAlarmLimit.Text = ((!conConfigContract.HighPayAlarmLimit.HasValue) ? string.Empty : conConfigContract.HighPayAlarmLimit.ToString());
			this.txtMidPayAlarmLowerLimit.Enabled = conConfigContract.IsPaymentAlarm;
			this.txtMidPayAlarmLowerLimit.Text = ((!conConfigContract.MidPayAlarmLowerLimit.HasValue) ? string.Empty : conConfigContract.MidPayAlarmLowerLimit.ToString());
			this.txtMidPayAlarmUpperLimit.Enabled = conConfigContract.IsPaymentAlarm;
			this.txtMidPayAlarmUpperLimit.Text = ((!conConfigContract.MidPayAlarmUpperLimit.HasValue) ? string.Empty : conConfigContract.MidPayAlarmUpperLimit.ToString());
			this.txtLowPayAlarmLowerLimit.Enabled = conConfigContract.IsPaymentAlarm;
			this.txtLowPayAlarmLowerLimit.Text = ((!conConfigContract.LowPayAlarmLowerLimit.HasValue) ? string.Empty : conConfigContract.LowPayAlarmLowerLimit.ToString());
			this.txtLowPayAlarmUpperLimit.Enabled = conConfigContract.IsPaymentAlarm;
			this.txtLowPayAlarmUpperLimit.Text = ((!conConfigContract.LowPayAlarmUpperLimit.HasValue) ? string.Empty : conConfigContract.LowPayAlarmUpperLimit.ToString());
			this.txtPayoutAlarmDays.Enabled = this.chkPayoutPay.Checked;
			this.txtIncomeAlarmDays.Enabled = this.chkIncomePay.Checked;
		}
	}
	protected void butSave_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(this.type))
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("IsPayoutAlarm", this.GetIntFromBool(this.chkIncomePay.Checked));
			dictionary.Add("PayoutAlarmDays", this.txtPayoutAlarmDays.Text.Trim());
			dictionary.Add("IsIncomeAlarm", this.GetIntFromBool(this.chkIncomePay.Checked));
			dictionary.Add("IncomeAlarmDays", this.txtIncomeAlarmDays.Text.Trim());
			dictionary.Add("IsBalanceAlarm", this.GetIntFromBool(this.chkIsBalanceAlarm.Checked));
			dictionary.Add("IsHighBalanceAlarm", this.GetIntFromBool(this.chkIsBalanceAlarm.Checked));
			dictionary.Add("HighBalanceAlarmLimit", this.txtHighBalanceAlarmLimit.Text.Trim());
			dictionary.Add("IsMidBalanceAlarm", this.GetIntFromBool(this.chkIsBalanceAlarm.Checked));
			dictionary.Add("MidBalanceAlarmLowerLimit", this.txtMidBalanceAlarmLowerLimit.Text.Trim());
			dictionary.Add("MidBalanceAlarmUpperLimit", this.txtMidBalanceAlarmUpperLimit.Text.Trim());
			dictionary.Add("IsLowBalanceAlarm", this.GetIntFromBool(this.chkIsBalanceAlarm.Checked));
			dictionary.Add("LowBalanceAlarmLowerLimit", this.txtLowBalanceAlarmLowerLimit.Text.Trim());
			dictionary.Add("LowBalanceAlarmUpperLimit", this.txtLowBalanceAlarmUpperLimit.Text.Trim());
			dictionary.Add("IsPaymentAlarm", this.GetIntFromBool(this.chkIsPaymentAlarm.Checked));
			dictionary.Add("IsHighPayAlarm", this.GetIntFromBool(this.chkIsPaymentAlarm.Checked));
			dictionary.Add("HighPayAlarmLimit", this.txtHighPayAlarmLimit.Text.Trim());
			dictionary.Add("IsMidPayAlarm", this.GetIntFromBool(this.chkIsPaymentAlarm.Checked));
			dictionary.Add("MidPayAlarmLowerLimit", this.txtMidPayAlarmLowerLimit.Text.Trim());
			dictionary.Add("MidPayAlarmUpperLimit", this.txtMidPayAlarmUpperLimit.Text.Trim());
			dictionary.Add("IsLowPayAlarm", this.GetIntFromBool(this.chkIsPaymentAlarm.Checked));
			dictionary.Add("LowPayAlarmLowerLimit", this.txtLowPayAlarmLowerLimit.Text.Trim());
			dictionary.Add("LowPayAlarmUpperLimit", this.txtLowPayAlarmUpperLimit.Text.Trim());
			try
			{
				ContractParameter.Update(dictionary);
				base.RegisterScript("top.ui.show('更新成功')");
				return;
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder("top.ui.alert('");
				stringBuilder.Append(ex.Message).Append("')");
				base.RegisterScript(stringBuilder.ToString());
				return;
			}
		}
		string arg_2C1_0 = string.Empty;
		string str = string.Empty;
		if (this.contype == "IncometContractList")
		{
			str = "IncometContract/IncometContractList.aspx";
		}
		else
		{
			if (this.contype == "PayoutContract")
			{
				str = "PayoutContract/PayoutContract.aspx";
			}
		}
		if (!string.IsNullOrEmpty(this.Id))
		{
			bool flag = this.configSer.IsExist(this.Id);
			ConConfigContract item = this.CreatModel(this.Id, flag);
			if (flag)
			{
				this.configSer.Update(item);
			}
			else
			{
				this.configSer.Add(item);
			}
		}
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder2.Append("top.ui.show('更新成功'); \n");
		stringBuilder2.Append("winclose('Config','" + str + "',true)");
		base.RegisterScript(stringBuilder2.ToString());
	}
	private ConConfigContract CreatModel(string contractId, bool isExist)
	{
		ConConfigContract conConfigContract = new ConConfigContract();
		string id = isExist ? (
			from p in this.configSer
			where p.ContractId == contractId
			select p).FirstOrDefault<ConConfigContract>().Id : Guid.NewGuid().ToString();
		conConfigContract.Id = id;
		conConfigContract.ContractId = contractId;
		conConfigContract.IsBalanceAlarm = this.chkIsBalanceAlarm.Checked;
		conConfigContract.IsIncomeAlarm = this.chkIncomePay.Checked;
		conConfigContract.IsPaymentAlarm = this.chkIsPaymentAlarm.Checked;
		conConfigContract.IsPayoutAlarm = this.chkPayoutPay.Checked;
		conConfigContract.HighPayAlarmLimit = Common2.ConvertToDecimal(this.txtHighPayAlarmLimit.Text.Trim());
		conConfigContract.HighBalanceAlarmLimit = Common2.ConvertToDecimal(this.txtHighBalanceAlarmLimit.Text.Trim());
		if (string.IsNullOrEmpty(this.txtPayoutAlarmDays.Text.Trim()))
		{
			conConfigContract.PayoutAlarmDays = null;
		}
		else
		{
			conConfigContract.PayoutAlarmDays = new int?(Convert.ToInt32(this.txtPayoutAlarmDays.Text.Trim()));
		}
		if (string.IsNullOrEmpty(this.txtIncomeAlarmDays.Text.Trim()))
		{
			conConfigContract.IncomeAlarmDays = null;
		}
		else
		{
			conConfigContract.IncomeAlarmDays = new int?(Convert.ToInt32(this.txtIncomeAlarmDays.Text.Trim()));
		}
		conConfigContract.MidPayAlarmUpperLimit = Common2.ConvertToDecimal(this.txtMidPayAlarmUpperLimit.Text.Trim());
		conConfigContract.MidPayAlarmLowerLimit = Common2.ConvertToDecimal(this.txtMidPayAlarmLowerLimit.Text.Trim());
		conConfigContract.MidBalanceAlarmUpperLimit = Common2.ConvertToDecimal(this.txtMidBalanceAlarmUpperLimit.Text.Trim());
		conConfigContract.MidBalanceAlarmLowerLimit = Common2.ConvertToDecimal(this.txtMidBalanceAlarmLowerLimit.Text.Trim());
		conConfigContract.LowPayAlarmUpperLimit = Common2.ConvertToDecimal(this.txtLowPayAlarmUpperLimit.Text.Trim());
		conConfigContract.LowPayAlarmLowerLimit = Common2.ConvertToDecimal(this.txtLowPayAlarmLowerLimit.Text.Trim());
		conConfigContract.LowBalanceAlarmUpperLimit = Common2.ConvertToDecimal(this.txtLowBalanceAlarmUpperLimit.Text.Trim());
		conConfigContract.LowBalanceAlarmLowerLimit = Common2.ConvertToDecimal(this.txtLowBalanceAlarmLowerLimit.Text.Trim());
		return conConfigContract;
	}
	private new void Init()
	{
		this.chkPayoutPay.Checked = ContractParameter.IsPayoutAlarm;
		this.txtPayoutAlarmDays.Text = ContractParameter.PayoutAlarmDays.ToString();
		this.chkIncomePay.Checked = ContractParameter.IsIncomeAlarm;
		this.txtIncomeAlarmDays.Text = ContractParameter.IncomeAlarmDays.ToString();
		this.chkIsBalanceAlarm.Checked = ContractParameter.IsBalanceAlarm;
		this.txtHighBalanceAlarmLimit.Enabled = this.chkIsBalanceAlarm.Checked;
		this.txtHighBalanceAlarmLimit.Text = ContractParameter.HighBalanceAlarmLimit.ToString();
		this.txtMidBalanceAlarmLowerLimit.Enabled = this.chkIsBalanceAlarm.Checked;
		this.txtMidBalanceAlarmLowerLimit.Text = ContractParameter.MidBalanceAlarmLowerLimit.ToString();
		this.txtMidBalanceAlarmUpperLimit.Enabled = this.chkIsBalanceAlarm.Checked;
		this.txtMidBalanceAlarmUpperLimit.Text = ContractParameter.MidBalanceAlarmUpperLimit.ToString();
		this.txtLowBalanceAlarmLowerLimit.Enabled = this.chkIsBalanceAlarm.Checked;
		this.txtLowBalanceAlarmLowerLimit.Text = ContractParameter.LowBalanceAlarmLowerLimit.ToString();
		this.txtLowBalanceAlarmUpperLimit.Enabled = this.chkIsBalanceAlarm.Checked;
		this.txtLowBalanceAlarmUpperLimit.Text = ContractParameter.LowBalanceAlarmUpperLimit.ToString();
		this.chkIsPaymentAlarm.Checked = ContractParameter.IsPaymentAlarm;
		this.txtHighPayAlarmLimit.Enabled = ContractParameter.IsPaymentAlarm;
		this.txtHighPayAlarmLimit.Text = ContractParameter.HighPayAlarmLimit.ToString();
		this.txtMidPayAlarmLowerLimit.Enabled = ContractParameter.IsPaymentAlarm;
		this.txtMidPayAlarmLowerLimit.Text = ContractParameter.MidPayAlarmLowerLimit.ToString();
		this.txtMidPayAlarmUpperLimit.Enabled = ContractParameter.IsPaymentAlarm;
		this.txtMidPayAlarmUpperLimit.Text = ContractParameter.MidPayAlarmUpperLimit.ToString();
		this.txtLowPayAlarmLowerLimit.Enabled = ContractParameter.IsPaymentAlarm;
		this.txtLowPayAlarmLowerLimit.Text = ContractParameter.LowPayAlarmLowerLimit.ToString();
		this.txtLowPayAlarmUpperLimit.Enabled = ContractParameter.IsPaymentAlarm;
		this.txtLowPayAlarmUpperLimit.Text = ContractParameter.LowPayAlarmUpperLimit.ToString();
		this.txtPayoutAlarmDays.Enabled = this.chkPayoutPay.Checked;
		this.txtIncomeAlarmDays.Enabled = this.chkIncomePay.Checked;
	}
	protected void butCancel_Click(object sender, EventArgs e)
	{
		this.Init();
	}
	protected void chkPayoutPay_CheckedChanged(object sender, EventArgs e)
	{
		this.txtPayoutAlarmDays.Enabled = this.chkPayoutPay.Checked;
	}
	protected void chkIncomePay_CheckedChanged(object sender, EventArgs e)
	{
		this.txtIncomeAlarmDays.Enabled = this.chkIncomePay.Checked;
	}
	protected void chkIsBalanceAlarm_CheckedChanged(object sender, EventArgs e)
	{
		this.SetIsBalanceAlarm();
	}
	private void SetIsBalanceAlarm()
	{
		bool @checked = this.chkIsBalanceAlarm.Checked;
		this.txtHighBalanceAlarmLimit.Enabled = @checked;
		this.txtMidBalanceAlarmLowerLimit.Enabled = @checked;
		this.txtMidBalanceAlarmUpperLimit.Enabled = @checked;
		this.txtLowBalanceAlarmLowerLimit.Enabled = @checked;
		this.txtLowBalanceAlarmUpperLimit.Enabled = @checked;
	}
	protected void chkIsPaymentAlarm_CheckedChanged(object sender, EventArgs e)
	{
		this.SetIsPaymentAlarm();
	}
	private void SetIsPaymentAlarm()
	{
		bool @checked = this.chkIsPaymentAlarm.Checked;
		this.txtHighPayAlarmLimit.Enabled = @checked;
		this.txtMidPayAlarmLowerLimit.Enabled = @checked;
		this.txtMidPayAlarmUpperLimit.Enabled = @checked;
		this.txtLowPayAlarmLowerLimit.Enabled = @checked;
		this.txtLowPayAlarmUpperLimit.Enabled = @checked;
	}
	private string GetIntFromBool(bool b)
	{
		if (b)
		{
			return "1";
		}
		return "0";
	}
}


