using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometBalance_ConInMeasureList : NBasePage, IRequiresSessionState
{
	private BudContractConsReportService budConReportSev = new BudContractConsReportService();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request["contractId"]))
		{
			this.hfldContractID.Value = base.Request.QueryString["contractId"];
			this.hfldBalanceId.Value = base.Request["BalanceId"].ToString();
			this.BindGV();
		}
	}
	private void BindGV()
	{
		string value = this.hfldContractID.Value;
		string value2 = this.hfldBalanceId.Value;
		if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2))
		{
			this.gvConract.DataSource = this.budConReportSev.GetContractReportByConIDAndBalIDEmpty(value, value2);
			this.gvConract.DataBind();
		}
	}
	public void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex]["RptId"].ToString();
			e.Row.Attributes["guid"] = this.gvConract.DataKeys[e.Row.RowIndex]["RptId"].ToString();
			e.Row.Attributes["state"] = this.gvConract.DataKeys[e.Row.RowIndex]["FlowState"].ToString();
		}
	}
}


