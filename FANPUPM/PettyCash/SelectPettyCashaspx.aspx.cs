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
public partial class PettyCash_SelectPettyCashaspx : NBasePage, IRequiresSessionState
{
	private PCPettyCashService pcSer = new PCPettyCashService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.InitPage();
		}
	}
	private void InitPage()
	{
		this.BindPettyCash();
	}
	private void BindPettyCash()
	{
		IQueryable<PCPettyCash> pettyCashQueryable = this.GetPettyCashQueryable();
		this.AspNetPager1.RecordCount = pettyCashQueryable.Count<PCPettyCash>();
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		int pageSize = this.AspNetPager1.PageSize;
		System.Collections.Generic.List<PCPettyCash> dataSource = pettyCashQueryable.Skip((currentPageIndex - 1) * pageSize).Take(pageSize).ToList<PCPettyCash>();
		this.gvwPettyCash.DataSource = dataSource;
		this.gvwPettyCash.DataBind();
	}
	private IQueryable<PCPettyCash> GetPettyCashQueryable()
	{
		IQueryable<PCPettyCash> source =
			from pc in this.pcSer
			where pc.Applicant == this.UserCode && pc.FlowState == 1 && pc.State == "0"
			select pc;
		if (!string.IsNullOrEmpty(this.txtMatter.Text.Trim()))
		{
			source =
				from pc in source
				where pc.Matter.Contains(this.txtMatter.Text.Trim())
				select pc;
		}
		if (!string.IsNullOrEmpty(this.txtApplicationDate.Text.Trim()))
		{
			source =
				from pc in source
				where pc.ApplicationDate == System.Convert.ToDateTime(this.txtApplicationDate.Text.Trim())
				select pc;
		}
		return
			from pc in source
			orderby pc.ApplicationDate descending
			select pc;
	}
	protected void gvwPettyCash_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPettyCash.DataKeys[e.Row.RowIndex]["Id"].ToString();
		}
	}
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.BindPettyCash();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPettyCash();
	}
}


