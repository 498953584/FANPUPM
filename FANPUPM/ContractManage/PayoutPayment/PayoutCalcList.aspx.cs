using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutPayment_PayoutCalcList : NBasePage, IRequiresSessionState
{
	private string contractId = string.Empty;
	private ConPayoutContractService pconSer = new ConPayoutContractService();
	private BudConsReportService budRptSer = new BudConsReportService();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ContractID"]))
		{
			this.contractId = base.Request["ContractID"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindRpt();
			this.BindPrjId();
		}
	}
	protected void gvConstruct_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string value = this.gvConstruct.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			string value2 = this.gvConstruct.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["state"] = value2;
			e.Row.Attributes["prjGuid"] = this.gvConstruct.DataKeys[e.Row.RowIndex].Values[2].ToString();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldRptId.Value);
		try
		{
			foreach (string current in listFromJson)
			{
				SelfEventAction.SuperDelete(current, "Bud_ConsReport", "FlowState");
			}
			ConstructReport.Delete(listFromJson);
			base.RegisterScript("top.ui.show('删除成功');");
			this.BindRpt();
		}
		catch
		{
			base.RegisterScript("top.ui.alert('删除失败');");
		}
	}
	private void BindRpt()
	{
		List<BudConsReport> source = (
			from r in this.budRptSer
			where r.ConstractId == this.contractId && r.IsValid == (bool?)true
			select r).ToList<BudConsReport>();
		this.gvConstruct.DataSource =
			from mt in source
			orderby mt.InputDate descending
			select mt;
		this.gvConstruct.DataBind();
	}
	private void BindPrjId()
	{
		ConPayoutContract byContractID = this.pconSer.GetByContractID(this.contractId);
		if (byContractID != null)
		{
			this.hfldPrjId.Value = byContractID.PrjGuid;
		}
	}
}


