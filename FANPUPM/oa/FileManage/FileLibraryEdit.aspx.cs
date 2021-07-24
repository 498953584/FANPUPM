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
public partial class oa_FileManage_FileLibraryEdit : BasePage, IRequiresSessionState
{

	public OAFileLibraryAction mcAction
	{
		get
		{
			return new OAFileLibraryAction();
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
		if (base.Request["lc"] == null || base.Request["t"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.LibraryCode = base.Request["lc"].ToString().Trim();
		this.OperateType = base.Request["t"].ToString().Trim();
		if (!this.Page.IsPostBack && this.OperateType == "upd")
		{
			this.ClassDisplay();
		}
	}
	private void ClassDisplay()
	{
		string strWhere = " LibraryCode='" + this.LibraryCode + "' ";
		DataTable list = this.mcAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtBookLibraryName.Text = list.Rows[0]["LibraryName"].ToString();
			this.txtRemark.Text = list.Rows[0]["Content"].ToString();
		}
	}
	private OAFileLibrary GetData()
	{
		OAFileLibrary oAFileLibrary = new OAFileLibrary();
		oAFileLibrary.Content = this.txtRemark.Text.Trim();
		oAFileLibrary.IsValid = "1";
		if (this.OperateType == "upd")
		{
			oAFileLibrary.LibraryCode = this.LibraryCode;
		}
		else
		{
			oAFileLibrary.LibraryCode = FileCommonClass.GetNewLibraryCode();
		}
		oAFileLibrary.LibraryName = this.txtBookLibraryName.Text.Trim();
		oAFileLibrary.Manager = "";
		return oAFileLibrary;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAFileLibrary data = this.GetData();
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


