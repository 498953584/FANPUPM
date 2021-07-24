using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_MatterClassManage : BasePage, IRequiresSessionState
{
	public ptOfficeResClassAction mcAction
	{
		get
		{
			return new ptOfficeResClassAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该物资类型吗?')) return false;";
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
				dataRowView["TypeCode"].ToString(),
				"','",
				dataRowView["i_ChildNum"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			int num = (dataRowView["TypeCode"].ToString().Length / 4 - 1) * 4;
			int i = 0;
			string text = "";
			while (i <= num)
			{
				text += "&nbsp;";
				i++;
			}
			if (num > 0)
			{
				e.Row.Cells[2].Text = dataRowView["TypeName"].ToString().Insert(0, text);
				return;
			}
			e.Row.Cells[2].Text = dataRowView["TypeName"].ToString();
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.HdnLibraryCode.Value = "";
		this.GVBook.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.HdnLibraryCode.Value = "";
		this.GVBook.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.mcAction.Delete(this.HdnLibraryCode.Value) > 0)
		{
			this.HdnLibraryCode.Value = "";
			this.GVBook.DataBind();
		}
	}
}


