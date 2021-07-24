using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_OilWearManage_SelectPurchase : NBasePage, IRequiresSessionState
{
	private Purchase purchase = new Purchase();
	private string contract = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["contract"]))
		{
			this.contract = base.Request["contract"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGvw();
		}
	}
	private void BindGvw()
	{
		System.Collections.Generic.List<PurchaseModel> list = this.purchase.GetList("contract = '" + this.contract + "'");
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			System.DateTime startDate = System.Convert.ToDateTime(this.txtStartDate.Text.Trim());
			(
				from p in list
				where p.intime > startDate
				select p).ToList<PurchaseModel>();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			System.DateTime endDate = System.Convert.ToDateTime(this.txtEndDate.Text.Trim()).AddDays(1.0);
			(
				from p in list
				where p.intime < endDate
				select p).ToList<PurchaseModel>();
		}
		if (!string.IsNullOrEmpty(this.txtPcode.Text.Trim()))
		{
			string pcode = this.txtPcode.Text.Trim();
			list = (
				from p in list
				where p.pcode.Contains(pcode)
				select p).ToList<PurchaseModel>();
		}
		this.gvwPurchase.DataSource = list;
		this.gvwPurchase.DataBind();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGvw();
	}
	protected void gvwPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["pid"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["pcode"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Values["pcode"].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
}


