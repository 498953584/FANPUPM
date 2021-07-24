using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Workflow_RoleCorpUser : NBasePage, IRequiresSessionState
{
	protected int RoleID
	{
		get
		{
			return System.Convert.ToInt32(this.ViewState["ROLEID"]);
		}
		set
		{
			this.ViewState["ROLEID"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["ri"]))
		{
			this.RoleID = System.Convert.ToInt32(base.Request["ri"]);
			this.hdnRoleID.Value = base.Request["ri"].ToString();
			this.BindGv();
		}
	}
	public void BindGv()
	{
		DataTable corpList = FlowRoleAction.GetCorpList(this.RoleID);
		this.gvCorpUser.DataSource = corpList;
		this.gvCorpUser.DataBind();
	}
	protected void gvCorpUser_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.gvCorpUser.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["onclick"] = "doClickRow('" + dataRowView["Role_User_ID"].ToString() + "');";
			e.Row.Cells[2].Text = FlowRoleAction.GetCorpNames(dataRowView["CorpCode"].ToString());
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		int userId = System.Convert.ToInt32(this.hdnUserID.Value);
		if (FlowRoleAction.DelUser(userId))
		{
			this.Page.RegisterClientScriptBlock("warn", "<script language=\"javascript\">alert('删除成功！');</script>");
		}
		this.BindGv();
	}
}


