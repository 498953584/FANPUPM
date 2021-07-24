using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class EPC_Workflow_SelectCorpUser : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["bm"]))
		{
			this.BindGV();
		}
	}
	public void BindGV()
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable corpUserList = FlowRoleAction.GetCorpUserList(base.Request["bm"].ToString(), this.txtUserName.Text, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvUser.DataSource = corpUserList;
		this.gvUser.DataBind();
	}
	protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvUser.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request.QueryString["bm"]))
		{
			string text = base.Request["bm"].ToString();
			for (int i = 0; i < this.gvUser.Rows.Count; i++)
			{
				CheckBox checkBox = (CheckBox)this.gvUser.Rows[i].FindControl("cbBox");
				if (checkBox.Checked && !FlowRoleAction.IsHaveCorpUser(this.gvUser.DataKeys[i].Value.ToString(), text))
				{
					FlowRoleAction.AddUser(0, this.gvUser.DataKeys[i].Value.ToString(), text);
				}
			}
		}
		base.RegisterScript("parent.location=parent.location;divClose(parent);");
	}
}


