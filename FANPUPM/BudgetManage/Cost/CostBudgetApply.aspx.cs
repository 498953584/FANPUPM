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
public partial class BudgetManage_Cost_CostBudgetApply : NBasePage, IRequiresSessionState
{
	private BudPreReimburseApplyService IndiApplySer = new BudPreReimburseApplyService();
	private BudPreReimburseApplyDetailService InApplydetailSer = new BudPreReimburseApplyDetailService();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.prjId = base.Request["prjId"];
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindGv();
		this.SetFlow();
	}
	public void BindGv()
	{
		this.hfldPrjId.Value = this.prjId;
		this.hfldYear.Value = this.year;
		this.hfldPurchaseChecked.Value = string.Empty;
		IQueryable<BudPreReimburseApply> source = this.GetIndiCostSouce(this.year);
		source =
			from p in source
			orderby p.InputDate descending
			orderby p.Name
			select p;
		this.AspNetPager1.RecordCount = source.Count<BudPreReimburseApply>();
		this.gvBudget.DataSource = source.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvBudget.DataBind();
	}
	public IQueryable<BudPreReimburseApply> GetIndiCostSouce(string year)
	{
		string costType = (year == "zzjg") ? "O" : "P";
		IQueryable<BudPreReimburseApply> queryable =
			from p in this.IndiApplySer
			where p.CostType == costType && p.PrjId == this.prjId
			select p;
		if (base.UserCode != "00000000")
		{
			queryable =
				from p in queryable
				where p.RptUser == this.UserCode
				select p;
		}
		return queryable;
	}
	public decimal GetSumCost(string Id)
	{
		return (
			from p in this.InApplydetailSer
			where p.ApplyId == Id
			select p.Cost).ToList<decimal>().Sum();
	}
	private void DisabledBtnAdd(int dataCount)
	{
		if (dataCount >= 1)
		{
			this.btnAdd.Disabled = false;
			return;
		}
		this.btnAdd.Disabled = true;
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex]["Id"].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		foreach (string current in listFromJson)
		{
			BudPreReimburseApply byId = this.IndiApplySer.GetById(current);
			if (byId != null)
			{
				this.IndiApplySer.Delete(byId);
			}
			else
			{
				base.RegisterScript("找不到要删除的组织机构日记对象");
			}
		}
		this.BindGv();
	}
	protected void SetFlow()
	{
		if (this.year == "zzjg")
		{
			this.WF1.BusiCode = "169";
		}
		else
		{
			this.WF1.BusiCode = "168";
		}
		this.WF1.URLParameter = "year=" + this.year + "&prjId=" + this.prjId;
	}
}


