using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_StorageManage : BasePage, IRequiresSessionState
{
	public ptOfficeResDepotAction mcAction
	{
		get
		{
			return new ptOfficeResDepotAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该仓库吗?')) return false;";
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["DepotID"].ToString() + "');";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
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
		if (this.mcAction.Delete(int.Parse(this.HdnLibraryCode.Value)) > 0)
		{
			this.GVBook.DataBind();
		}
	}
}


