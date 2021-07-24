using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_FileTypeManage : BasePage, IRequiresSessionState
{
	private string strWhere = string.Empty;

	public OAFileClassesAction mcAction
	{
		get
		{
			return new OAFileClassesAction();
		}
	}
	public string LibraryCode
	{
		get
		{
			object obj = this.ViewState["LIBRARYCODE"];
			if (obj != null)
			{
				return (string)this.ViewState["LIBRARYCODE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["LIBRARYCODE"] = value;
		}
	}
	public bool IsFirst
	{
		get
		{
			object obj = this.ViewState["ISFIRST"];
			return obj != null && (bool)this.ViewState["ISFIRST"];
		}
		set
		{
			this.ViewState["ISFIRST"] = value;
		}
	}
	public string CatalogName
	{
		get
		{
			object obj = this.ViewState["CATALOGNAME"];
			if (obj != null)
			{
				return (string)this.ViewState["CATALOGNAME"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["CATALOGNAME"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["lc"] == null || base.Request["ln"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.LibraryCode = base.Request["lc"].ToString().Trim();
		this.CatalogName = base.Request["ln"].ToString().Trim();
		this.IsFirst = Convert.ToBoolean(base.Request["isFirst"].ToString());
		bool arg_A5_0 = this.Page.IsPostBack;
		if (this.IsFirst)
		{
			this.btnAdd.Enabled = false;
		}
		else
		{
			this.btnAdd.Enabled = true;
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add','" + this.LibraryCode + "')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd','" + this.LibraryCode + "')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该纸质档案类型吗?')) return false;";
	}
	protected void GVLibrary_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["ClassID"].ToString(),
				"','",
				dataRowView["LibraryCode"].ToString(),
				"','",
				dataRowView["ClassCode"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVLibrary.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVLibrary.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.mcAction.Delete(this.LibraryCode, this.HdnClassCode.Value) > 0)
		{
			this.GVLibrary.DataBind();
		}
	}
}


