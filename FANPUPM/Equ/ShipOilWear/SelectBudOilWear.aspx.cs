using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_ShipOilWear_SelectBudOilWear : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["PrjId"]))
		{
			this.prjId = base.Request["PrjId"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGV();
		}
	}
	private void BindGV()
	{
		this.gvEquOilWear.DataBind();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGV();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	protected void gvEquOilWear_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvEquOilWear.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["code"] = this.gvEquOilWear.DataKeys[e.Row.RowIndex].Values["Code"].ToString();
			e.Row.Attributes["taskId"] = this.gvEquOilWear.DataKeys[e.Row.RowIndex].Values["TaskId"].ToString();
			e.Row.Attributes["quotaOilWear"] = this.gvEquOilWear.DataKeys[e.Row.RowIndex].Values["QuotaOilWear"].ToString();
		}
	}
}


