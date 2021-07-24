using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_InStoreroom : BasePage, IRequiresSessionState
{
	private string strWhere = "";

	public ptOfficeResInStorageAction amAction
	{
		get
		{
			return new ptOfficeResInStorageAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除图书分类吗?')) return false;";
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList("");
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
				dataRowView["DepotID"].ToString(),
				"','",
				dataRowView["InStorageID"].ToString(),
				"','",
				dataRowView["BillCode"].ToString(),
				"','",
				dataRowView["IsConfirm"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		this.amAction.UpdateStock(int.Parse(this.HdnInStorageID.Value), int.Parse(this.HdnDepotID.Value));
		if (this.amAction.Delete(int.Parse(this.HdnInStorageID.Value)) > 0)
		{
			this.HdnInStorageID.Value = "0";
			this.Bind();
		}
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		if (this.amAction.UpdateConfirm(int.Parse(this.HdnInStorageID.Value)) > 0)
		{
			this.Bind();
			this.Page.RegisterStartupScript("", "<script>alert('确认成功!');</script>");
		}
	}
}


