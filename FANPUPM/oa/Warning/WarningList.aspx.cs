using ASP;
using cn.justwin.BLL;
using cn.justwin.Warn;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class oa_Warning_WarningList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public void BindGv()
	{
		string title = this.txtTitle.Text.Trim();
		string content = this.txtContent.Text.Trim();
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable warningList = Warning.GetWarningList(base.UserCode, title, content, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.AspNetPager1.RecordCount = Warning.GetWarningListCount(base.UserCode, title, content);
		this.gvWarning.DataSource = warningList;
		this.gvWarning.DataBind();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvWarning_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvWarning.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	public string DoWithUrl(string url, string id)
	{
		url = StringUtility.DealUrl(url);
		url = url + "id=" + id;
		return url;
	}
}


