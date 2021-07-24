using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometContract_ContractMeasureList : NBasePage, IRequiresSessionState
{
	private BudContractConsReportService budConRptSev = new BudContractConsReportService();
	private BudContractConsTaskService budContractConTaskSev = new BudContractConsTaskService();
	private ConIncometContractService conIncometSev = new ConIncometContractService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			this.BindGV();
		}
	}
	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			this.hfldContractID.Value = base.Request["ContractId"];
			ConIncometContract byContractId = this.conIncometSev.GetByContractId(base.Request["ContractId"]);
			this.hfldPrjGuid.Value = byContractId.Project;
		}
		base.OnInit(e);
	}
	public void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex]["RptId"].ToString();
			e.Row.Attributes["guid"] = this.gvConract.DataKeys[e.Row.RowIndex]["RptId"].ToString();
			e.Row.Attributes["ContractId"] = this.gvConract.DataKeys[e.Row.RowIndex]["ContractId"].ToString();
			e.Row.Attributes["BalanceId"] = this.gvConract.DataKeys[e.Row.RowIndex]["BalanceId"].ToString();
			e.Row.Attributes["state"] = this.gvConract.DataKeys[e.Row.RowIndex]["FlowState"].ToString();
		}
	}
	public void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldMeasureID.Value);
		foreach (string current in listFromJson)
		{
			BudContractConsReport byId = this.budConRptSev.GetById(current);
			if (byId != null)
			{
				this.budContractConTaskSev.DeleteByReport(byId.RptId);
				this.budConRptSev.Delete(byId);
			}
		}
		this.hfldMeasureID.Value = "";
		this.hfldContractID.Value = base.Request["ContractId"].ToString();
		this.hfldPrjGuid.Value = base.Request["prjId"].ToString();
		this.BindGV();
	}
	private void BindGV()
	{
		string cmdText = string.Concat(new string[]
		{
			"select * from Bud_ContractConsReport \r\n                          where ContractId='",
			this.hfldContractID.Value,
			"'  AND PrjId='",
			this.hfldPrjGuid.Value,
			"' order by InputDate desc "
		});
		DataTable dataSource = this.budConRptSev.ExecuteQuery(cmdText, new SqlParameter[0]);
		this.gvConract.DataSource = dataSource;
		this.gvConract.DataBind();
	}
}


