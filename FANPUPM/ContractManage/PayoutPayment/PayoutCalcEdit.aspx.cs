using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutPayment_PayoutCalcEdit : NBasePage, IRequiresSessionState
{
	private string rptId = string.Empty;
	private string contractId = string.Empty;
	private string type = string.Empty;
	private ConPayoutContractService pconSer = new ConPayoutContractService();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["rptId"]))
		{
			this.rptId = base.Request["rptId"];
		}
		if (!string.IsNullOrEmpty(base.Request["contractId"]))
		{
			this.contractId = base.Request["contractId"];
		}
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitData();
		}
	}
	private void InitData()
	{
		ConPayoutContract byContractID = this.pconSer.GetByContractID(this.contractId);
		if (byContractID != null)
		{
			this.lblContractName.Text = byContractID.ContractName;
			this.hfldPrjId.Value = byContractID.PrjGuid;
		}
	}
	protected void btnRefreshPage_Click(object sender, EventArgs e)
	{
		string value = this.hfldRetTaskId.Value;
		this.gvwTask.DataSource = cn.justwin.Domain.BudTask.GetTaskInfoByParent(value);
		this.gvwTask.DataBind();
	}
}


