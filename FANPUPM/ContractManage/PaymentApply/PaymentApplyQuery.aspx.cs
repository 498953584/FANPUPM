using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PaymentApply_PaymentApplyQuery : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private PaymentApply paymentApply = new PaymentApply();

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
		if (!string.IsNullOrEmpty(base.Request["showAudit"]))
		{
			new DataTable();
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
			this.InintUpdateState();
			this.Literal1.Text = this.FilesBind(1913, this.ViewState["PaymentId"].ToString());
		}
	}
	public string FilesBind(int moduleID, string recordCode)
	{
		string text = "";
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			moduleID.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"<a href='",
				dataRow["FilePath"].ToString(),
				dataRow["AnnexName"].ToString(),
				"' target=_blank>",
				dataRow["OriginalName"].ToString(),
				"</a> "
			});
		}
		return text;
	}
	private void InitContractInfo(string conId)
	{
		IncometContractBll incometContractBll = new IncometContractBll();
		IncometContractModel model = incometContractBll.GetModel(conId);
		this.lblContractCode.Text = model.ContractCode;
		this.lblContractName.Text = model.ContractName;
		this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
		string userCode = this.Session["yhdm"].ToString();
		DataTable dataTable = PersonnelAction.QueryPersonnelById(userCode);
		if (dataTable != null && dataTable.Rows.Count == 1)
		{
			this.lblBllProducer.Text = dataTable.Rows[0]["v_xm"].ToString();
		}
		new PayoutBalance().GetBalancedAmount(conId, false);
		this.lblBalancedSum.Text = WebUtil.GetPaymentSum(model.ContractID, "Con_Incomet_Payment", "CllectionPrice");
		bool containPending = this.paymentApply.GetById(this.ViewState["PaymentId"].ToString()).ContainPending;
		decimal paySum = this.paymentApply.GetPaySum(conId, containPending);
		this.lblPaySum.Text = paySum.ToString();
		decimal num = Convert.ToDecimal(this.lblBalancedSum.Text) - paySum;
		this.lblDiff.Text = num.ToString();
		this.lblContractMoney.Text = model.ContractPrice.ToString();
		this.lblSignedDate.Text = model.SignedTime.Value.ToShortDateString();
	}
	private void InintUpdateState()
	{
		IncomentPaymentApplyModel byId = this.paymentApply.GetById(this.ViewState["PaymentId"].ToString());
		this.contractId = byId.ContractId;
		this.lblPaymentCode.Text = byId.Code;
		this.lblPaymentMoney.Text = byId.PaymentAmount.ToString("0.000");
		this.lblPaymentDate.Text = byId.PaymentDate.ToShortDateString();
		this.lblPaymentPerson.Text = byId.PaymentPenson;
		this.lblInputPerson.Text = byId.InputPerson;
		this.lblInputDate.Text = byId.InputDate.ToShortDateString();
		this.lblNotes.Text = byId.Notes;
		decimal number = 0m;
		if (byId.PaymentAmount.ToString() != "")
		{
			number = Convert.ToDecimal(byId.PaymentAmount);
		}
		this.LblCapitalNumber.Text = ConverRMB.Convert(number);
		if (byId.PaymentMode == "-1")
		{
			this.LblPayType.Text = "";
		}
		if (byId.PaymentMode == "0")
		{
			this.LblPayType.Text = "现金";
		}
		if (byId.PaymentMode == "1")
		{
			this.LblPayType.Text = "支票";
		}
		if (byId.PaymentMode == "2")
		{
			this.LblPayType.Text = "转账";
		}
		this.InitContractInfo(this.contractId);
	}
}


