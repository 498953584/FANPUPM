using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Cost_CostVerifyList : NBasePage, IRequiresSessionState
{
	private BudIndirectDiaryCostService cSer = new BudIndirectDiaryCostService();
	private string ic = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.InitPage();
		}
	}
	protected void gvwCostVerify_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwCostVerify.DataKeys[e.Row.RowIndex]["InDiaryId"].ToString();
			e.Row.Attributes["costType"] = this.gvwCostVerify.DataKeys[e.Row.RowIndex]["CostType"].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	public string GetPrjOrOrg(object inDiaryIdObj)
	{
		string inDiaryId = inDiaryIdObj.ToString();
		BudIndirectDiaryCost byId = this.cSer.GetById(inDiaryId);
		if (byId == null)
		{
			return string.Empty;
		}
		string result = string.Empty;
		if (byId.CostType == "P")
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId2 = pTPrjInfoService.GetById(byId.ProjectId);
			if (byId2 != null)
			{
				result = byId2.PrjName;
			}
		}
		else
		{
			PTdbmService pTdbmService = new PTdbmService();
			PTdbm byId3 = pTdbmService.GetById(byId.ProjectId);
			if (byId3 != null)
			{
				result = byId3.V_bmqc;
			}
		}
		return result;
	}
	public string GetPettyCashMatter(object pettyCashId)
	{
		PCPettyCashService pCPettyCashService = new PCPettyCashService();
		if (pettyCashId != null)
		{
			PCPettyCash byId = pCPettyCashService.GetById(pettyCashId.ToString());
			if (byId != null)
			{
				return byId.Matter;
			}
		}
		return string.Empty;
	}
	private void InitPage()
	{
		this.BindGvw();
	}
	private void BindGvw()
	{
		IQueryable<BudIndirectDiaryCost> diaryCostQueryable = this.GetDiaryCostQueryable();
		this.AspNetPager1.RecordCount = diaryCostQueryable.Count<BudIndirectDiaryCost>();
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		int pageSize = this.AspNetPager1.PageSize;
		this.gvwCostVerify.DataSource = (
			from d in diaryCostQueryable
			orderby d.InputDate2 descending
			select d).Skip((currentPageIndex - 1) * pageSize).Take(pageSize).ToList<BudIndirectDiaryCost>();
		this.gvwCostVerify.DataBind();
	}
	private IQueryable<BudIndirectDiaryCost> GetDiaryCostQueryable()
	{
		IQueryable<BudIndirectDiaryCost> queryable =
			from c in this.cSer
			where c.IsAudit == false && c.FlowState == 1
			select c;
		if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
		{
			queryable =
				from c in queryable
				where c.Name.Contains(this.txtName.Text.Trim())
				select c;
		}
		if (!string.IsNullOrEmpty(this.txtIssuedBy.Text.Trim()))
		{
			queryable =
				from c in queryable
				where c.IssuedBy.Contains(this.txtIssuedBy.Text.Trim())
				select c;
		}
		if (!string.IsNullOrEmpty(this.txtInputUser.Text.Trim()))
		{
			queryable =
				from c in queryable
				where c.InputUser.Contains(this.txtInputUser.Text.Trim())
				select c;
		}
		if (!string.IsNullOrEmpty(this.txtInputDate1.Text.Trim()))
		{
			queryable =
				from c in queryable
				where c.InputDate >= System.Convert.ToDateTime(this.txtInputDate1.Text.Trim())
				select c;
		}
		if (!string.IsNullOrEmpty(this.txtInputDate2.Text.Trim()))
		{
			queryable =
				from c in queryable
				where c.InputDate <= System.Convert.ToDateTime(this.txtInputDate2.Text.Trim()).AddDays(1.0)
				select c;
		}
		if (!string.IsNullOrEmpty(this.dropCostType.SelectedValue))
		{
			queryable =
				from c in queryable
				where c.CostType == this.dropCostType.SelectedValue
				select c;
		}
		if (!string.IsNullOrEmpty(this.txtCode.Text.Trim()))
		{
			queryable =
				from c in queryable
				where c.IndireCode.Contains(this.txtCode.Text.Trim())
				select c;
		}
		return queryable;
	}
}


