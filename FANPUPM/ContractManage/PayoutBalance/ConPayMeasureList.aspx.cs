using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutBalance_ConPayMeasureList : NBasePage, IRequiresSessionState
{
	private BudConsReportService budConReportSev = new BudConsReportService();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request["contractId"]))
		{
			this.hfldContractID.Value = base.Request.QueryString["contractId"];
			this.BindGV();
		}
	}
	private void BindGV()
	{
		string contractID = this.hfldContractID.Value;
		if (!string.IsNullOrEmpty(contractID))
		{
			List<BudConsReport> dataSource = (
				from r in this.budConReportSev
				where r.ConstractId == contractID && r.FlowState == 1 && r.BalanceId == null
				select r).ToList<BudConsReport>();
			this.gvConract.DataSource = dataSource;
			this.gvConract.DataBind();
		}
	}
	public void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex]["ConsReportId"].ToString();
			e.Row.Attributes["guid"] = this.gvConract.DataKeys[e.Row.RowIndex]["ConsReportId"].ToString();
			e.Row.Attributes["state"] = this.gvConract.DataKeys[e.Row.RowIndex]["State"].ToString();
		}
	}
}


