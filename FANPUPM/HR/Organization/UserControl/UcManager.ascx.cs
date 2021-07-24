using ASP;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_UserControl_UcManager : System.Web.UI.UserControl
{

	public HROrgPostLevelAction mcAction
	{
		get
		{
			return new HROrgPostLevelAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenManageWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenManageWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
	}
	protected void GVManager_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('1','" + dataRowView["RecordID"].ToString() + "');";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVManager.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVManager.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.mcAction.Delete(int.Parse(this.HdnRecordID.Value)) > 0)
		{
			this.GVManager.DataBind();
			this.Page.RegisterStartupScript("", "<script>alert('删除成功!');</script>");
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('删除失败!');</script>");
	}
}


