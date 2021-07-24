using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_FileLibrary : BasePage, IRequiresSessionState
{

	public OAFileLibraryAction mcAction
	{
		get
		{
			return new OAFileLibraryAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnPopedomSet.Attributes["onclick"] = "javascript:if(!OpenPopedomSet()) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该档案室吗?')) return false;";
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
				dataRowView["LibraryCode"].ToString(),
				"','",
				dataRowView["Manager"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[2].Text = BooksCommonClass.GetUserName(dataRowView["Manager"].ToString());
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.mcAction.Delete(this.HdnLibraryCode.Value) > 0)
		{
			this.GVBook.DataBind();
		}
	}
	protected void btnPopedomSet_Click(object sender, EventArgs e)
	{
		if (this.mcAction.PopedomPersonSet(this.HdnLibraryCode.Value, this.HdnPopedomPerson.Value) > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('权限设置成功!');</script>");
			this.GVBook.DataBind();
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('权限设置失败!');</script>");
	}
}


