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
public partial class oa_Bulletin_BulletinUserQuery : NBasePage, IRequiresSessionState
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
		this.AspNetPager1.RecordCount = BulletionAction.GetCountBulletion(this.txtTitle.Text.Trim(), this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), this.droprange.SelectedValue, this.txtName.Text.Trim(), this.dropWFState.SelectedValue);
		DataTable bulletionList = BulletionAction.GetBulletionList(this.txtTitle.Text.Trim(), this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), this.droprange.SelectedValue, this.txtName.Text.Trim(), this.dropWFState.SelectedValue, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvBulletin.DataSource = bulletionList;
		this.gvBulletin.DataBind();
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvBulletin_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBulletin.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


