using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_FileTypeManageEdit : BasePage, IRequiresSessionState
{
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
	public string ClassCode
	{
		get
		{
			object obj = this.ViewState["CLASSCODE"];
			if (obj != null)
			{
				return (string)this.ViewState["CLASSCODE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["CLASSCODE"] = value;
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
	public string OperateType
	{
		get
		{
			object obj = this.ViewState["OPERATETYPE"];
			if (obj != null)
			{
				return (string)this.ViewState["OPERATETYPE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["OPERATETYPE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["lc"] == null || base.Request["cc"] == null || base.Request["t"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.LibraryCode = base.Request["lc"].ToString().Trim();
		this.ClassCode = base.Request["cc"].ToString().Trim();
		this.OperateType = base.Request["t"].ToString().Trim();
		if (!this.Page.IsPostBack && this.OperateType == "upd")
		{
			this.ClassDisplay();
		}
	}
	private void ClassDisplay()
	{
		string strWhere = string.Concat(new string[]
		{
			" LibraryCode='",
			this.LibraryCode,
			"' and ClassCode='",
			this.ClassCode,
			"' "
		});
		DataTable list = this.mcAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtClassName.Text = list.Rows[0]["ClassName"].ToString();
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
		}
	}
	private OAFileClasses GetData()
	{
		OAFileClasses oAFileClasses = new OAFileClasses();
		if (this.OperateType == "upd")
		{
			oAFileClasses.ClassCode = this.ClassCode;
		}
		else
		{
			oAFileClasses.ClassCode = FileCommonClass.GetNewFileCode(this.LibraryCode);
		}
		oAFileClasses.LibraryCode = this.LibraryCode;
		oAFileClasses.ClassName = this.txtClassName.Text.Trim();
		oAFileClasses.Remark = this.txtRemark.Text.Trim();
		oAFileClasses.IsValid = "1";
		return oAFileClasses;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAFileClasses data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.mcAction.Add(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;window.close();";
			}
			else
			{
				this.JS.Text = "没有相关数据可添加!";
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.mcAction.Update(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "没有相关数据可更新!";
		}
	}
}


