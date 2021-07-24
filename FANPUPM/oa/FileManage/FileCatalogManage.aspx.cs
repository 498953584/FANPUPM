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
public partial class oa_FileManage_FileCatalogManage : PageBase, IRequiresSessionState
{

	public OAFileCatalogAction amAction
	{
		get
		{
			return new OAFileCatalogAction();
		}
	}
	public string strWhere
	{
		get
		{
			object obj = this.ViewState["STRWHERE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["STRWHERE"] = value;
		}
	}
	public bool IsFirst
	{
		get
		{
			object obj = this.ViewState["ISFIRST"];
			return obj == null || (bool)obj;
		}
		set
		{
			this.ViewState["ISFIRST"] = value;
		}
	}
	public string LibraryCode
	{
		get
		{
			object obj = this.ViewState["LIBRARYCODE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["LIBRARYCODE"] = value;
		}
	}
	public int FileAge
	{
		get
		{
			object obj = this.ViewState["FILEAGE"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["FILEAGE"] = value;
		}
	}
	public int TimeLimit
	{
		get
		{
			object obj = this.ViewState["TIMELIMIT"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["TIMELIMIT"] = value;
		}
	}
	public int ClassID
	{
		get
		{
			object obj = this.ViewState["CLASSID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["CLASSID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["isview"] != null && base.Request["isview"] == "view")
		{
			this.btnAdd.Width = (this.btnDel.Width = (this.btnEdit.Width = 0));
		}
		if (base.Request["isFirst"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.IsFirst = Convert.ToBoolean(base.Request["isFirst"].ToString());
		if (base.Request["lc"] != null)
		{
			this.LibraryCode = base.Request["lc"].ToString();
			this.HdnLibraryCode.Value = this.LibraryCode;
		}
		if (base.Request["y"] != null)
		{
			this.FileAge = int.Parse(base.Request["y"].ToString());
			this.HdnFileAge.Value = this.FileAge.ToString();
		}
		if (base.Request["l"] != null)
		{
			this.TimeLimit = int.Parse(base.Request["l"].ToString());
			this.HdnTimeLimit.Value = this.TimeLimit.ToString();
		}
		if (base.Request["cid"] != null)
		{
			this.ClassID = int.Parse(base.Request["cid"].ToString());
			this.HdnClassID.Value = this.ClassID.ToString();
		}
		if (!this.Page.IsPostBack)
		{
			if (this.IsFirst)
			{
				this.btnAdd.Enabled = false;
			}
			this.Bind(this.GetTerm());
		}
		this.txtotherSearch.Attributes["onblur"] = "IsInteger(this);";
		this.DDLSearch.Attributes["onchange"] = "CheckChanged(this);";
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnView.Attributes["onclick"] = "javascript:if(!OpenWin('view')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
	}
	private string GetTerm()
	{
		this.strWhere = " (1=1) ";
		if (this.LibraryCode != "")
		{
			this.strWhere = this.strWhere + " and LibraryCode='" + this.LibraryCode + "' ";
		}
		if (this.ClassID != -1)
		{
			object strWhere = this.strWhere;
			this.strWhere = string.Concat(new object[]
			{
				strWhere,
				" and ClassID='",
				this.ClassID,
				"' "
			});
		}
		if (this.FileAge != -1)
		{
			object strWhere2 = this.strWhere;
			this.strWhere = string.Concat(new object[]
			{
				strWhere2,
				" and FileAge='",
				this.FileAge,
				"' "
			});
		}
		if (this.TimeLimit != -1)
		{
			object strWhere3 = this.strWhere;
			this.strWhere = string.Concat(new object[]
			{
				strWhere3,
				" and TimeLimit='",
				this.TimeLimit,
				"' "
			});
		}
		return this.strWhere;
	}
	private void Bind(string strWhere)
	{
		DataTable dataSource = new DataTable();
		if (!this.IsFirst)
		{
			dataSource = this.amAction.GetList(strWhere);
		}
		this.GVBook.DataSource = dataSource;
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["RecordID"].ToString() + "');";
			e.Row.Attributes["ondblclick"] = "javascript:if(!OpenWin('view')) return false;";
			switch (int.Parse(dataRowView["SecretLevel"].ToString()))
			{
			case 1:
				e.Row.Cells[8].Text = "密秘";
				return;
			case 2:
				e.Row.Cells[8].Text = "机密";
				return;
			case 3:
				e.Row.Cells[8].Text = "绝密";
				break;
			default:
				return;
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind(this.strWhere);
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.strWhere = this.GetTerm();
		if (this.DDLSearch.SelectedValue == "0" && this.txtotherSearch.Text.ToString().Trim() != "")
		{
			this.strWhere = this.strWhere + " and FileCode='" + this.txtotherSearch.Text.Trim() + "' ";
		}
		if (this.DDLSearch.SelectedValue == "1")
		{
			if (this.txtotherSearch.Text.Trim() == "密秘")
			{
				this.strWhere += " and SecretLevel=1 ";
			}
			else
			{
				if (this.txtotherSearch.Text.Trim() == "机密")
				{
					this.strWhere += " and SecretLevel=2 ";
				}
				else
				{
					if (this.txtotherSearch.Text.Trim() == "绝密")
					{
						this.strWhere += " and SecretLevel=3 ";
					}
					else
					{
						this.strWhere += " and SecretLevel=0 ";
					}
				}
			}
		}
		if (this.DDLSearch.SelectedValue == "2" && this.txtotherSearch.Text.ToString().Trim() != "")
		{
			this.strWhere = this.strWhere + " and FileNumber='" + this.txtotherSearch.Text.Trim() + "' ";
		}
		if (this.DDLSearch.SelectedValue == "3" && this.txtotherSearch.Text.ToString().Trim() != "")
		{
			this.strWhere = this.strWhere + " and BoxNumber='" + this.txtotherSearch.Text.Trim() + "' ";
		}
		if (this.DDLSearch.SelectedValue == "4" && this.txtotherSearch.Text.ToString().Trim() != "")
		{
			this.strWhere = this.strWhere + " and Principal='" + this.txtotherSearch.Text.Trim() + "' ";
		}
		this.Bind(this.strWhere);
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Bind(this.GetTerm());
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.Bind(this.GetTerm());
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.amAction.Delete(int.Parse(this.HdnRecordID.Value)) > 0)
		{
			this.Bind(this.GetTerm());
		}
	}
	protected void btnView_Click(object sender, EventArgs e)
	{
		this.Bind(this.GetTerm());
	}
}


