using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Workflow_RoleCorpEdit : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.txtUserName.Attributes["ReadOnly"] = "true";
			if (base.Request["action"].ToString() == "update")
			{
				DataTable corpRole = FlowRoleAction.GetCorpRole(base.Request["uid"].ToString());
				this.hfldUserCode.Value = corpRole.Rows[0]["UserCode"].ToString();
				this.txtUserName.Text = corpRole.Rows[0]["v_xm"].ToString();
				string text = corpRole.Rows[0]["CorpCode"].ToString();
				string[] t = (
					from c in text.Split(new char[]
					{
						','
					})
					where c.Length > 1
					select c).ToArray<string>();
				this.hfldCorpCode.Value = JsonHelper.JsonSerializer<string[]>(t);
				DataTable corpTable = FlowRoleAction.GetCorpTable(corpRole.Rows[0]["CorpCode"].ToString());
				this.gvCorpList.DataSource = corpTable;
				this.gvCorpList.DataBind();
				return;
			}
			DataTable dataSource = new DataTable();
			this.gvCorpList.DataSource = dataSource;
			this.gvCorpList.DataBind();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string text = string.Empty;
		System.Collections.Generic.List<string> list = JsonHelper.JsonDeserialize<string[]>(this.hfldCorpCode.Value).ToList<string>();
		foreach (string current in list)
		{
			text = text + current + ",";
		}
		text = text.Substring(0, text.Length - 1);
		if (base.Request["action"].ToString() == "update")
		{
			string roleUserId = base.Request["uid"].ToString();
			if (string.IsNullOrEmpty(this.hfldUserCode.Value) || string.IsNullOrEmpty(text))
			{
				base.RegisterScript("top.ui.alert('没有选择人员或所负责的部门为空')");
				return;
			}
			bool flag = FlowRoleAction.UpdateCorpUser(roleUserId, this.hfldUserCode.Value, text);
			if (flag)
			{
				base.RegisterScript("top.ui.tabSuccess({parentName: '_rolecorpuser'});");
				return;
			}
			base.RegisterScript("top.ui.tabError();");
			return;
		}
		else
		{
			int roleId = System.Convert.ToInt32(base.Request["rid"].ToString());
			if (string.IsNullOrEmpty(this.hfldUserCode.Value) || string.IsNullOrEmpty(text))
			{
				base.RegisterScript("top.ui.alert('没有选择人员或所负责的部门为空')");
				return;
			}
			bool flag2 = FlowRoleAction.AddUser(roleId, this.hfldUserCode.Value, text);
			if (flag2)
			{
				base.RegisterScript("top.ui.tabSuccess({parentName: '_rolecorpuser'});");
				return;
			}
			base.RegisterScript("top.ui.tabError();");
			return;
		}
	}
	protected void gvCorpList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["i_bmdm"].ToString() + "');";
		}
	}
	public void BindGv()
	{
		if (this.hfldCorpCode.Value.Length >= 2)
		{
			string text = this.hfldCorpCode.Value.Substring(1, this.hfldCorpCode.Value.Length - 2);
			text = text.Replace('"', '\'');
			DataTable corpTable = FlowRoleAction.GetCorpTable(text);
			this.gvCorpList.DataSource = corpTable;
			this.gvCorpList.DataBind();
			return;
		}
		DataTable dataSource = new DataTable();
		this.gvCorpList.DataSource = dataSource;
		this.gvCorpList.DataBind();
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hdnCorpId.Value))
		{
			System.Collections.Generic.List<string> list = JsonHelper.JsonDeserialize<string[]>(this.hfldCorpCode.Value).ToList<string>();
			list.Remove(this.hdnCorpId.Value);
			this.hfldCorpCode.Value = JsonHelper.JsonSerializer<string[]>(list.ToArray());
			this.BindGv();
		}
		this.BindGv();
	}
	protected void btnBindData_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


