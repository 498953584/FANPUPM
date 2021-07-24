using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_Repair_RepairReportView : NBasePage, IRequiresSessionState
{
	private EquRepairReportService reportSer = new EquRepairReportService();
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.id = base.Request["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.lblBllProducer.Text = WebUtil.GetUserNames(base.UserCode);
			EquRepairReport byId = this.reportSer.GetById(this.id);
			this.BindGV();
		}
	}
	private void BindGV()
	{
		EquRepairStockService equRepairStockService = new EquRepairStockService();
		DataTable repairStock = equRepairStockService.GetRepairStock(this.id, string.Empty, string.Empty, 0, 0);
		this.gvRepairStock.DataSource = repairStock;
		this.gvRepairStock.DataBind();
	}
	private string GetContractName(string contractID)
	{
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(contractID);
		if (model == null)
		{
			return string.Empty;
		}
		return model.ContractName;
	}
}


