using ASP;
using cn.justwin.BLL;
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
public partial class BudgetManage_Cost_SelectCode : NBasePage, IRequiresSessionState
{
	private BudPreReimburseApplyService InPreApplySer = new BudPreReimburseApplyService();

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
	}
	public void BindGv()
	{
		IQueryable<BudPreReimburseApply> pettyCashQueryable = this.GetPettyCashQueryable();
		this.AspNetPager1.RecordCount = pettyCashQueryable.Count<BudPreReimburseApply>();
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		int pageSize = this.AspNetPager1.PageSize;
		System.Collections.Generic.List<BudPreReimburseApply> dataSource = pettyCashQueryable.Skip((currentPageIndex - 1) * pageSize).Take(pageSize).ToList<BudPreReimburseApply>();
		this.gvwModify.DataSource = dataSource;
		this.gvwModify.DataBind();
	}
	public IQueryable<BudPreReimburseApply> GetPettyCashQueryable()
	{
		IQueryable<BudPreReimburseApply> source =
			from pc in this.InPreApplySer
			where pc.RptUser == PageHelper.QueryUser(this, this.UserCode) && pc.FlowState == 1
			select pc;
		if (!string.IsNullOrEmpty(this.txtApplyDate.Text.Trim()))
		{
			source =
				from pc in source
				where pc.ApplyDate == System.Convert.ToDateTime(this.txtApplyDate.Text.Trim())
				select pc;
		}
		if (!string.IsNullOrEmpty(this.txtCode.Text.Trim()))
		{
			source =
				from pc in source
				where pc.Code.Contains(this.txtCode.Text.Trim())
				select pc;
		}
		return
			from pc in source
			orderby pc.ApplyDate descending
			select pc;
	}
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvwModify_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwModify.DataKeys[e.Row.RowIndex]["Id"].ToString();
		}
	}
}


