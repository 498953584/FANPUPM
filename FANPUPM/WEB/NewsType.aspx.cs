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
public partial class WEB_NewsType : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除新闻分类吗?')) return false;";
	}
	protected void Dbg_item_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			System.Data.DataRowView dataRowView = (System.Data.DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["c_xwlxdm"].ToString() + "');";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			string text = dataRowView["c_xwlxmc"].ToString();
			string text2 = dataRowView["c_xwlxdm"].ToString().Trim();
			for (int i = 0; i < text2.Length - 2; i++)
			{
				text = "&nbsp;&nbsp;" + text;
			}
			e.Row.Cells[2].Text = text.ToString();
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.HdnID.Value = "";
		this.Dbg_item.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.HdnID.Value = "";
		this.Dbg_item.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		NewsAction newsAction = new NewsAction();
		if (newsAction.delNewsType(this.HdnID.Value))
		{
			this.JS.Text = "alert('删除成功!')";
		}
		this.HdnID.Value = "";
		this.Dbg_item.DataBind();
	}
}


