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
public partial class HR_Organization_JobFamily : BasePage, IRequiresSessionState
{

	public PTDRoleAction mcAction
	{
		get
		{
			return new PTDRoleAction();
		}
	}
	public string RoleTypeCode
	{
		get
		{
			object obj = this.ViewState["RoleTypeCode"];
			if (obj != null)
			{
				return (string)this.ViewState["RoleTypeCode"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["RoleTypeCode"] = value;
		}
	}
	public bool Flag
	{
		get
		{
			object obj = this.ViewState["FLAG"];
			return obj != null && (bool)this.ViewState["FLAG"];
		}
		set
		{
			this.ViewState["FLAG"] = value;
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
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该条记录吗?')) return false;";
	}
	private void Bind()
	{
		DataTable list = this.mcAction.GetList("len(RoleTypeCode)<=6");
		this.GridView1.DataSource = list;
		this.GridView1.DataBind();
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RoleTypeCode"].ToString(),
				"','",
				dataRowView["ChildNum"].ToString(),
				"');"
			});
			int num = (dataRowView["RoleTypeCode"].ToString().Length - 1) / 3 * 4;
			int i = 0;
			string text = "";
			while (i <= num)
			{
				text += "&nbsp;";
				i++;
			}
			if (num > 0)
			{
				e.Row.Cells[1].Text = dataRowView["RoleTypeName"].ToString().Insert(0, text);
				return;
			}
			e.Row.Cells[1].Text = dataRowView["RoleTypeName"].ToString();
		}
	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GridView1.PageIndex = e.NewPageIndex;
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
		if (this.mcAction.Delete(this.HdnRoleTypeCode.Value) > 0)
		{
			this.Bind();
		}
	}
}


