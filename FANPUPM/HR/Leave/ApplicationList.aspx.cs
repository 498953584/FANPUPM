using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_ApplicationList : BasePage, IRequiresSessionState
{

	private ApplicationAction aa
	{
		get
		{
			return new ApplicationAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前请假记录数据吗？');";
			this.BtnConfirm.Attributes["onclick"] = "openConfirm();";
			this.btnView.Attributes["onclick"] = "OpenLock();";
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVApplication.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVApplication.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.aa.Delete(new Guid(this.HdnRecoreId.Value)) == 1)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "top.ui.show( '删除成功');", true);
			this.GVApplication.DataBind();
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "top.ui.alert('删除失败');", true);
	}
	protected void GVApplication_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			userManageDb userManageDb = new userManageDb();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"','",
				userManageDb.GetUserName(e.Row.Cells[1].Text),
				"');"
			});
			e.Row.Cells[1].Text = userManageDb.GetUserName(e.Row.Cells[1].Text);
			e.Row.Attributes["ondblclick"] = "OpenLock();";
			e.Row.ToolTip = "双击查看详细信息";
			string text;
			switch (text = e.Row.Cells[2].Text)
			{
			case "1":
				e.Row.Cells[2].Text = "事假";
				break;
			case "2":
				e.Row.Cells[2].Text = "婚假";
				break;
			case "3":
				e.Row.Cells[2].Text = "年休假";
				break;
			case "4":
				e.Row.Cells[2].Text = "工伤";
				break;
			case "5":
				e.Row.Cells[2].Text = "病假";
				break;
			case "6":
				e.Row.Cells[2].Text = "产假";
				break;
			case "7":
				e.Row.Cells[2].Text = "丧假";
				break;
			}
			int num2 = AduitAction.SetOkState(dataRowView["AuditState"].ToString()).LastIndexOf('#');
			string value = AduitAction.SetOkState(dataRowView["AuditState"].ToString()).Substring(num2);
			e.Row.Cells[8].Style.Add("color", value);
			e.Row.Cells[8].Text = AduitAction.SetOkState(dataRowView["AuditState"].ToString()).Substring(0, num2);
			string text2 = e.Row.Cells[7].Text.ToString();
			if (text2.Length > 10)
			{
				e.Row.Cells[7].Text = text2.Substring(0, 10) + "...";
				e.Row.Cells[7].ToolTip = text2;
			}
		}
	}
	protected void btnRefresh_Click(object sender, EventArgs e)
	{
		this.GVApplication.DataBind();
	}
}


