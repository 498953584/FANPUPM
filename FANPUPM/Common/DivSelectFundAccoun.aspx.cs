using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Fund.Account;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_DivSelectFundAccoun : NBasePage, IRequiresSessionState
{
	public AccountLogic AL = new AccountLogic();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.grdModules.PageSize = NBasePage.pagesize;
			this.ShowTaskList("", "", base.UserCode);
		}
	}
	protected void SearchBt_Click(object sender, System.EventArgs e)
	{
		this.ShowTaskList(this.AccountCode.Text.Trim(), this.AccountName.Text.Trim(), base.UserCode);
	}
	public void ShowTaskList(string Code, string Name, string authorizer)
	{
		this.grdModules.DataSource = this.AL.QueryAccount(Code, Name, authorizer);
		this.grdModules.DataBind();
	}
	protected void grdModules_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.grdModules.PageIndex = e.NewPageIndex;
		this.ShowTaskList(this.AccountCode.Text.Trim(), this.AccountName.Text.Trim(), base.UserCode);
	}
	protected void grdModules_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = dataRowView["AccountID"].ToString();
			e.Row.Attributes["ondblclick"] = string.Concat(new string[]
			{
				"dbClickRow(this,'",
				dataRowView["AccountID"].ToString(),
				"','",
				dataRowView["acountName"].ToString(),
				"')"
			});
		}
	}
}


