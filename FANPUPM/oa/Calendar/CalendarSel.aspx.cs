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
public partial class oa_Calendar_CalendarSel : BasePage, IRequiresSessionState
{
	private CalendarInfoAction cia
	{
		get
		{
			return new CalendarInfoAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request["dt"] != null)
		{
			this.btnClose.Attributes["onclick"] = "window.close();returnValue=true";
			this.btnAdd.Attributes["onclick"] = "openCalendarEdit('Add')";
			this.btnEdit.Attributes["onclick"] = "openCalendarEdit('Edit')";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
			this.btnCalendar.Attributes["onclick"] = "openCalendar()";
			this.HdnRecordDate.Value = base.Request["dt"].ToString();
		}
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			string text = ((DataRowView)e.Row.DataItem)["InfoGuid"].ToString();
			string text2 = ((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				text2,
				"','",
				e.Row.Cells[0].Text,
				"','",
				text,
				"');"
			});
			e.Row.Cells[3].Text = ((e.Row.Cells[3].Text == "1") ? "是" : "否");
			e.Row.Cells[2].ToolTip = "单击查看详细信息";
			Label label = (Label)e.Row.Cells[2].FindControl("Label1");
			e.Row.Cells[2].Attributes["onclick"] = "getContent('" + text2 + "');";
			if (label.Text.Length > 50)
			{
				label.Text = label.Text.Substring(0, 50) + "...";
			}
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GridView1.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.cia.Delete(Convert.ToInt32(this.hdnRecordID.Value)) == 1)
		{
			PTDBSJAction pTDBSJAction = new PTDBSJAction();
			pTDBSJAction.Delete(string.Concat(new object[]
			{
				" v_lxbm = '001' and v_YHDM = '",
				this.Session["yhdm"].ToString(),
				"' and i_XGID = '",
				Convert.ToInt32(this.hdnRecordID.Value),
				"'"
			}), 1);
			this.JS.Text = "alert('删除成功!');";
			this.GridView1.DataBind();
			return;
		}
		this.JS.Text = "alert('删除失败!');";
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GridView1.DataBind();
	}
}


