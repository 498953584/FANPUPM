using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_BooksManage_ClassList : BasePage, IRequiresSessionState
{
	private string strWhere = string.Empty;

	public PTMultiClassesAction mcAction
	{
		get
		{
			return new PTMultiClassesAction();
		}
	}
	public string ClassTypeCode
	{
		get
		{
			object obj = this.ViewState["CLASSTYPECODE"];
			if (obj != null)
			{
				return (string)this.ViewState["CLASSTYPECODE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["CLASSTYPECODE"] = value;
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
		if (base.Request["ctc"] == null || base.Request["f"] == null || base.Request["cn"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.ClassTypeCode = base.Request["ctc"].ToString().Trim();
		this.Flag = (base.Request["f"].ToString().Trim() == "1");
		this.CatalogName = "分类";
		this.HdnClassTypeCode.Value = this.ClassTypeCode;
		if (!this.Page.IsPostBack)
		{
			this.Label1.Text = base.Server.UrlDecode(this.CatalogName) + "管理";
			if (this.Flag)
			{
				SqlDataSource expr_F9 = this.SQLDataSource;
				expr_F9.SelectCommand = expr_F9.SelectCommand + " and  CorpCode = '" + this.Session["CorpCode"].ToString() + "'";
				this.GridView1.DataBind();
			}
		}
		this.btnAdd.Attributes["onclick"] = string.Concat(new string[]
		{
			"javascript:if(!OpenWin('add','",
			this.ClassTypeCode,
			"','",
			this.CatalogName,
			"')) return false;"
		});
		this.btnEdit.Attributes["onclick"] = string.Concat(new string[]
		{
			"javascript:if(!OpenWin('upd','",
			this.ClassTypeCode,
			"','",
			this.CatalogName,
			"')) return false;"
		});
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除吗?删除分类将删除该分类下所有的制度!请谨慎操作')) return false;";
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
				dataRowView["ClassID"].ToString(),
				"','",
				dataRowView["ClassCode"].ToString(),
				"','",
				dataRowView["ChildNum"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			dataRowView["ClassName"].ToString();
			e.Row.Cells[2].Text = dataRowView["ClassName"].ToString().Replace("^", "&nbsp;");
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.HdnClassCode.Value = "";
		SqlDataSource expr_16 = this.SQLDataSource;
		expr_16.SelectCommand = expr_16.SelectCommand + " and  CorpCode = '" + this.Session["CorpCode"].ToString() + "'";
		this.GridView1.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.HdnClassCode.Value = "";
		SqlDataSource expr_16 = this.SQLDataSource;
		expr_16.SelectCommand = expr_16.SelectCommand + " and  CorpCode = '" + this.Session["CorpCode"].ToString() + "'";
		this.GridView1.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.mcAction.Delete(this.HdnClassTypeCode.Value, this.HdnClassCode.Value) > 0)
		{
			this.mcAction.DeleteAllzd(this.HdnID.ToString());
			this.HdnClassCode.Value = "";
			SqlDataSource expr_51 = this.SQLDataSource;
			expr_51.SelectCommand = expr_51.SelectCommand + " and  CorpCode = '" + this.Session["CorpCode"].ToString() + "'";
			this.GridView1.DataBind();
		}
	}
}


