using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PettyCash_PettyCashClearList : NBasePage, IRequiresSessionState
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
			where p.Applicant == this.UserCode && p.FlowState == 1
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
		if (!string.IsNullOrEmpty(this.dropState.SelectedValue))
		{
			queryable =
				from p in queryable
				where p.State == this.dropState.SelectedValue
				select p;
		}
		return queryable;
	}
	public decimal GetAmountPCash(string pId, bool IsAmount)
    {
        List<string> lstIds = new List<string>();
        List<BudIndirectDiaryCost> source = (from p in this.budInCostSer
            where (p.PettyCashId == pId) && (p.FlowState == 1)
            select p).ToList<BudIndirectDiaryCost>();
        if (!IsAmount && (CachedAnonymousMethodDelegate8 == null))
        {
            CachedAnonymousMethodDelegate8 = p => p.IsAudit;
        }
        try
        {
            lstIds = (CachedAnonymousMethodDelegate9 != null) ? (from p in source select p.InDiaryId).Distinct<string>().ToList<string>() : source.Where<BudIndirectDiaryCost>(CachedAnonymousMethodDelegate8).Select<BudIndirectDiaryCost, string>(CachedAnonymousMethodDelegate9).Distinct<string>().ToList<string>();
        }
        catch { }
        List<BudIndirectDiaryDetails> list2 = (from p in this.indirDetailSer
            where lstIds.Contains(p.InDiaryId) && (p.PettyCashId == pId)
            select p).ToList<BudIndirectDiaryDetails>();
        if (IsAmount)
        {
            return ((IEnumerable<decimal>) (from p in list2 select p.Amount)).Sum();
        }
        return ((IEnumerable<decimal>) (from p in list2 select p.AuditAmount)).Sum();
    }
}


