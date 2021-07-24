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
using Wuqi.Webdiyer;
public partial class BudgetManage_Construct_ContractReportDetail : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private BudContractConsReportService rSer = new BudContractConsReportService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initial();
			this.BindReport();
		}
	}
	protected void gvwReport_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwReport.DataKeys[e.Row.RowIndex]["RptId"].ToString();
			e.Row.Attributes["guid"] = this.gvwReport.DataKeys[e.Row.RowIndex]["RptId"].ToString();
			e.Row.Attributes["prjGuid"] = this.gvwReport.DataKeys[e.Row.RowIndex]["PrjId"].ToString();
			e.Row.Attributes["state"] = this.gvwReport.DataKeys[e.Row.RowIndex]["FlowState"].ToString();
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldRptId.Value);
		foreach (string current in listFromJson)
		{
			BudContractConsReport byId = this.rSer.GetById(current);
			this.rSer.Delete(byId);
		}
		base.RegisterScript("top.ui.show( '删除成功');");
		this.BindReport();
	}
	private void Initial()
	{
		BudContractTaskAuditService budContractTaskAuditService = new BudContractTaskAuditService();
		BudContractTaskAudit byProject = budContractTaskAuditService.GetByProject(this.prjId);
		if (byProject != null && byProject.FlowState.HasValue)
		{
			this.hfldFlowState.Value = byProject.FlowState.Value.ToString();
		}
	}
	private void BindReport()
	{
		int recordCount = (
			from r in this.rSer
			where r.PrjId == this.prjId && r.IsValid
			orderby r.InputDate descending
			select r).Count<BudContractConsReport>();
		this.AspNetPager1.RecordCount = recordCount;
		IOrderedQueryable<BudContractConsReport> source =
			from r in this.rSer
			where r.PrjId == this.prjId && r.IsValid
			orderby r.InputDate descending
			select r;
		this.gvwReport.DataSource = source.ToList<BudContractConsReport>().Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvwReport.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindReport();
	}
}


