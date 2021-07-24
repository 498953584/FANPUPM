using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_RoleProjectList : NBasePage, IRequiresSessionState
{
	protected userManageDb user = new userManageDb();
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
	private RoleProjectAction rpa
	{
		get
		{
			return new RoleProjectAction();
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.RoleID = System.Convert.ToInt32(base.Request["ri"]);
			this.btnAdd.Attributes["onclick"] = "return openEdit('Add'," + this.RoleID + ");";
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit'," + this.RoleID + ");";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删吗？');";
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		if (this.rpa.Delete(System.Convert.ToInt32(this.HdnRoleProjectID.Value)) == 1)
		{
			this.JS.Text = "alert('删除成功！');";
			this.GVRoleProject.DataBind();
			return;
		}
		this.JS.Text = "alert('删除失败！');";
	}
	protected void GVRoleProject_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[0].Text = (e.Row.RowIndex + 1 + this.GVRoleProject.PageIndex * this.GVRoleProject.PageSize).ToString();
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["RoleProjectID"].ToString() + "');";
			e.Row.Cells[1].Text = this.user.GetUserName(dataRowView["UserCode"].ToString());
			e.Row.Cells[2].Text = this.GetPrjName(dataRowView["ProjectCodes"].ToString());
			if (this.GetPrjName(dataRowView["ProjectCodes"].ToString()).Length > 40)
			{
				e.Row.Cells[2].Text = this.GetPrjName(dataRowView["ProjectCodes"].ToString()).Substring(4, 30) + "...";
				e.Row.Cells[2].ToolTip = this.GetPrjName(dataRowView["ProjectCodes"].ToString());
			}
		}
	}
	protected string GetPrjName(string prjID)
	{
		DataTable prjNameList = this.rpa.GetPrjNameList(prjID);
		string text = "";
		for (int i = 0; i < prjNameList.Rows.Count; i++)
		{
			text = text + prjNameList.Rows[i]["PrjName"].ToString() + ",";
		}
		if (text != "")
		{
			text = text.Substring(0, text.Length - 1);
		}
		return text;
	}
}


