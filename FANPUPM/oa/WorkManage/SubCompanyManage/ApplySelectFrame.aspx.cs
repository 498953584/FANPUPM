using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_SubCompanyManage_ApplySelectFrame : BasePage, IRequiresSessionState
{

	public OAOfficeResPersonalApplicationAction amAction
	{
		get
		{
			return new OAOfficeResPersonalApplicationAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["ic"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			this.lblMsg.Text = "个人申请记录";
			this.Bind();
		}
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList("IsComplete='0'");
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				base.Request["ic"].ToString(),
				"','",
				dataRowView["PARecordID"].ToString(),
				"','",
				dataRowView["IsSubmit"].ToString(),
				"','",
				dataRowView["UseMan"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[2].Text = BooksCommonClass.GetUserName(dataRowView["ApplyMan"].ToString());
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
}


