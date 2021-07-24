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
public partial class HR_Organization_JobFamilyThird : BasePage, IRequiresSessionState
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
		if (base.Request["cc"] == null || base.Request["f"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		if (base.Request["cc"].ToString() != "")
		{
			this.RoleTypeCode = base.Request["cc"].ToString().Trim();
			this.HdnRoleTypeCodeAdd.Value = this.RoleTypeCode;
		}
		if (base.Request["f"].ToString() != "")
		{
			this.Flag = (base.Request["f"].ToString() == "true");
		}
		if (!this.Page.IsPostBack)
		{
			if (this.Flag)
			{
				this.btnAdd.Enabled = false;
			}
			this.Bind();
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该条记录吗?')) return false;";
	}
	private void Bind()
	{
		DataTable dataSource = new DataTable();
		if (!this.Flag)
		{
			dataSource = this.mcAction.GetList("ParentCode='" + this.RoleTypeCode + "'");
		}
		this.GridView1.DataSource = dataSource;
		this.GridView1.DataBind();
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			int num = 0;
			DataTable roleTypeList = this.mcAction.getRoleTypeList(dataRowView["RoleTypeCode"].ToString());
			if (roleTypeList.Rows.Count > 0)
			{
				num = 1;
			}
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RoleTypeCode"].ToString(),
				"','",
				dataRowView["ChildNum"].ToString(),
				"',",
				num,
				");"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
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


