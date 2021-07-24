using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class oa_Bulletin_BulletinUserDetail : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
		}
	}
	public void BindGv()
	{
		if (!string.IsNullOrEmpty(base.Request.QueryString["id"]))
		{
			this.AspNetPager1.RecordCount = BulletionAction.GetCountUserList(base.Request["id"].ToString(), this.txtName.Text.Trim(), this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim());
			DataTable bulletionUserList = BulletionAction.GetBulletionUserList(base.Request["id"].ToString(), this.txtName.Text.Trim(), this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
			this.gvData.DataSource = bulletionUserList;
			this.gvData.DataBind();
		}
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
	}
}


