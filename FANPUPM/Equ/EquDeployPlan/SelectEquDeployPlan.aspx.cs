using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_EquDeployPlan_SelectEquDeployPlan : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize3;
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
		this.txtPlanCode.Text.Trim();
		this.txtEquipmentCode.Text.Trim();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void gvwEquDeploymentPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwEquDeploymentPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["code"] = this.gvwEquDeploymentPlan.DataKeys[e.Row.RowIndex].Values["Code"].ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGvw();
	}
}


