using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PettyCash_PettyCashRepayList : NBasePage, IRequiresSessionState
{
	private PCPettyCashService pcSer = new PCPettyCashService();
	private BudIndirectDiaryDetails indirDetail = new BudIndirectDiaryDetails();
	private BudIndirectDiaryCostService budInCostSer = new BudIndirectDiaryCostService();
	private BudIndirectDiaryDetailsService indirDetailSer = new BudIndirectDiaryDetailsService();
    private static Func<BudIndirectDiaryCost, bool> CachedAnonymousMethodDelegate8;
    private static Func<BudIndirectDiaryCost, string> CachedAnonymousMethodDelegate9;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindPettyCash();
		}
	}
	protected void gvwPettyCash_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvwPettyCash.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = (e.Row.Attributes["id"] = value);
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPettyCash();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.BindPettyCash();
	}
	protected void btnExportExcel_Click(object sender, System.EventArgs e)
	{
		IQueryable<PCPettyCash> source = this.Queryable();
		source =
			from p in source
			orderby p.ApplicationDate descending
			orderby p.InputDate descending
			select p;
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("申请日期");
		dataTable.Columns.Add("申请金额");
		dataTable.Columns.Add("事项");
		dataTable.Columns.Add("用款日期");
		dataTable.Columns.Add("还款状态");
		dataTable.Columns.Add("项目");
		int num = source.Count<PCPettyCash>();
		System.Collections.Generic.List<PCPettyCash> list = source.ToList<PCPettyCash>();
		for (int i = 0; i < num; i++)
		{
			PCPettyCash pCPettyCash = list[i];
			DataRow dataRow = dataTable.NewRow();
			dataRow["序号"] = i + 1;
			dataRow["申请日期"] = pCPettyCash.ApplicationDate.ToString("yyyy-MM-dd");
			dataRow["申请金额"] = pCPettyCash.Cash;
			dataRow["事项"] = pCPettyCash.Matter;
			dataRow["用款日期"] = pCPettyCash.CashDate.ToString("yyyy-MM-dd");
			dataRow["还款状态"] = Common2.GetStateNoColor(pCPettyCash.RepayFlowState.ToString());
			if (pCPettyCash.Project != null)
			{
				dataRow["项目"] = pCPettyCash.Project.PrjName;
			}
			dataTable.Rows.Add(dataRow);
		}
		string text = "备用金申请.xls";
		if (base.Request.Browser.Browser.ToUpper() != "SAFARI")
		{
			text = HttpUtility.UrlEncode(text, System.Text.Encoding.UTF8);
		}
		ExcelHelper.ExportDataTableToExcel(dataTable, text, "");
	}
	private void BindPettyCash()
	{
		IQueryable<PCPettyCash> source = this.Queryable();
		this.AspNetPager1.RecordCount = source.Count<PCPettyCash>();
		source =
			from p in source
			orderby p.ApplicationDate descending
			orderby p.InputDate descending
			select p;
		IQueryable<PCPettyCash> dataSource = source.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvwPettyCash.DataSource = dataSource;
		this.gvwPettyCash.DataBind();
	}
	private IQueryable<PCPettyCash> Queryable()
	{
		System.DateTime? startDate = DateTimeHelper.GetDateTime(this.txtStartDate.Text);
		System.DateTime? endDate = DateTimeHelper.GetDateTime(this.txtEndDate.Text);
		decimal? startCash = DecimalUtility.ConvertDecimal(this.txtStartCash.Text);
		decimal? endCash = DecimalUtility.ConvertDecimal(this.txtEndCash.Text);
		string matter = this.txtMatter.Text.Trim();
		IQueryable<PCPettyCash> queryable =
			from p in this.pcSer
			where p.Applicant == this.UserCode && p.FlowState == 1 && p.State == "0"
			select p;
		if (startDate.HasValue)
		{
			queryable =
				from p in queryable
				where p.ApplicationDate >= startDate.Value
				select p;
		}
		if (endDate.HasValue)
		{
			queryable =
				from p in queryable
				where p.ApplicationDate < endDate.Value.AddDays(1.0)
				select p;
		}
		if (startCash.HasValue)
		{
			queryable =
				from p in queryable
				where p.Cash >= startCash.Value
				select p;
		}
		if (endCash.HasValue)
		{
			queryable =
				from p in queryable
				where p.Cash <= endCash.Value
				select p;
		}
		if (!string.IsNullOrWhiteSpace(matter))
		{
			queryable =
				from p in queryable
				where p.Matter.Contains(matter)
				select p;
		}
		return queryable;
	}
	public decimal GetAmountPCash(string pId, bool IsAmount)
	{
        System.Collections.Generic.List<string> lstIds = new System.Collections.Generic.List<string>();
		System.Collections.Generic.List<BudIndirectDiaryCost> source = (
			from p in this.budInCostSer
			where p.PettyCashId == pId && p.FlowState == 1
			select p).ToList<BudIndirectDiaryCost>();
		if (!IsAmount)
		{
            lstIds = (
				from p in source
				where p.IsAudit
				select p.InDiaryId).Distinct<string>().ToList<string>();
		}
		else
		{
            lstIds = (
				from p in source
				select p.InDiaryId).Distinct<string>().ToList<string>();
		}
		System.Collections.Generic.List<BudIndirectDiaryDetails> source2 = (
			from p in this.indirDetailSer
			where lstIds.Contains(p.InDiaryId) && p.PettyCashId == pId
			select p).ToList<BudIndirectDiaryDetails>();
		decimal result;
		if (IsAmount)
		{
			result = (
				from p in source2
				select p.Amount).Sum();
		}
		else
		{
			result = (
				from p in source2
				select p.AuditAmount).Sum();
		}
		return result;
	}

}


