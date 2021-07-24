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
public partial class oa_BooksManage_ClassEdit : BasePage, IRequiresSessionState
{

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
		if (base.Request["ctc"] == null || base.Request["cc"] == null || base.Request["cn"] == null || base.Request["t"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.ClassTypeCode = base.Request["ctc"].ToString().Trim();
		this.ClassCode = base.Request["cc"].ToString().Trim();
		this.CatalogName = base.Request["cn"].ToString().Trim();
		this.OperateType = base.Request["t"].ToString().Trim();
		this.Page.Header.Title = this.CatalogName;
		if (!this.Page.IsPostBack)
		{
			this.LblMsg.Text = this.CatalogName + "类型维护";
			if (this.OperateType == "upd")
			{
				this.ClassDisplay();
			}
		}
	}
	private void ClassDisplay()
	{
		string strWhere = string.Concat(new string[]
		{
			" ClassTypeCode='",
			this.ClassTypeCode,
			"' and ClassCode='",
			this.ClassCode,
			"' "
		});
		DataTable list = this.mcAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtClassName.Text = base.Server.HtmlDecode(list.Rows[0]["ClassName"].ToString()).Trim();
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString().Trim();
		}
	}
	private PTMultiClasses GetData()
	{
		PTMultiClasses pTMultiClasses = new PTMultiClasses();
		pTMultiClasses.ClassTypeCode = this.ClassTypeCode;
		pTMultiClasses.ClassCode = this.ClassCode;
		pTMultiClasses.ParentClassCode = "";
		pTMultiClasses.ClassName = this.txtClassName.Text.Trim();
		pTMultiClasses.Remark = this.txtRemark.Text.Trim();
		pTMultiClasses.IsValid = "1";
		string corpCode = "%";
		DataTable dataTable = DocumentAction.QueryCorpCode(this.Session["yhdm"].ToString());
		if (dataTable.Rows.Count > 0)
		{
			corpCode = dataTable.Rows[0]["corpCode"].ToString();
		}
		pTMultiClasses.CorpCode = corpCode;
		return pTMultiClasses;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		PTMultiClasses data = this.GetData();
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


