using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_MyLoanList : BasePage, IRequiresSessionState
{

	public OAFileLendAction amAction
	{
		get
		{
			return new OAFileLendAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该条记录吗?')) return false;";
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
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["LendState"].ToString(),
				"');"
			});
			switch (int.Parse(dataRowView["LendState"].ToString()))
			{
			case 1:
				e.Row.Cells[5].Text = "未借出";
				return;
			case 2:
				e.Row.Cells[5].Text = "已借出";
				return;
			case 3:
				e.Row.Cells[5].Text = "归还申请";
				return;
			case 4:
				e.Row.Cells[5].Text = "已归还";
				break;
			default:
				return;
			}
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.amAction.Delete(int.Parse(this.HdnRecordID.Value)) > 0)
		{
			this.GVBook.DataBind();
		}
	}
	protected void btnBack_Click(object sender, EventArgs e)
	{
		if (this.amAction.FileBack(int.Parse(this.HdnRecordID.Value)) > 0)
		{
			this.GVBook.DataBind();
		}
	}
}


