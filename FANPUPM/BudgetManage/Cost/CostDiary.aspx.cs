using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Cost_CostDiary : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
	private BudIndirectDiaryCostService IndiCostSer = new BudIndirectDiaryCostService();
	private BudIndirectDiaryDetailsService IndetailSer = new BudIndirectDiaryDetailsService();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
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
		this.BindPrjName();
		this.BindGv();
		this.SetFlow();
	}
	public void BindGv()
	{
		this.hfldPrjId.Value = this.prjId;
		this.hfldYear.Value = this.year;
		this.hfldPurchaseChecked.Value = string.Empty;
		IQueryable<BudIndirectDiaryCost> source = this.GetIndiCostSouce(this.year);
		source =
			from p in source
			orderby p.InputDate2 descending
			orderby p.Name
			select p;
		this.AspNetPager1.RecordCount = source.Count<BudIndirectDiaryCost>();
		this.gvBudget.DataSource = source.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvBudget.DataBind();
	}
	public IQueryable<BudIndirectDiaryCost> GetIndiCostSouce(string year)
	{
		string costType = (year == "zzjg") ? "O" : "P";
		IQueryable<BudIndirectDiaryCost> queryable =
			from p in this.IndiCostSer
			where p.CostType == costType && p.ProjectId == this.prjId
			select p;
		if (base.UserCode != "00000000")
		{
			queryable =
				from p in queryable
				where p.InputUserCode == this.UserCode
				select p;
		}
		return queryable;
	}
	public decimal GetSumCost(string Id)
	{
		return (
			from p in this.IndetailSer
			where p.InDiaryId == Id
			select p.Amount).ToList<decimal>().Sum();
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
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["InDiaryId"].ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex]["InDiaryId"].ToString();
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		foreach (string current in listFromJson)
		{
			BudIndirectDiaryCost byId = this.IndiCostSer.GetById(current);
			if (byId != null)
			{
				this.IndiCostSer.Delete(byId);
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
			this.WF1.BusiCode = "088";
		}
		else
		{
			this.WF1.BusiCode = "087";
		}
		this.WF1.URLParameter = "year=" + this.year + "&prjId=" + this.prjId;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	private void BindPrjName()
	{
		if (this.year != "zzjg")
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
			if (byId != null)
			{
				this.hfldPrjName.Value = byId.PrjName;
				return;
			}
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(this.prjId);
			if (byId2 != null)
			{
				this.hfldPrjName.Value = byId2.PrjName;
			}
		}
	}
}


