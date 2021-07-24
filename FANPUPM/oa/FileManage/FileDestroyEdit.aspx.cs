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
public partial class oa_FileManage_FileDestroyEdit : BasePage, IRequiresSessionState
{

	public OAFileDestroyMainAction amAction
	{
		get
		{
			return new OAFileDestroyMainAction();
		}
	}
	public Guid RecordCode
	{
		get
		{
			object obj = this.ViewState["RECORDCODE"];
			if (obj != null)
			{
				return (Guid)this.ViewState["RECORDCODE"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDCODE"] = value;
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
		if (!this.Page.IsPostBack)
		{
			if (base.Request["t"] == null || base.Request["ac"] == null || base.Request["lc"] == null)
			{
				this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
				return;
			}
			if (base.Request["ac"].ToString() != "")
			{
				this.RecordCode = new Guid(base.Request["ac"].ToString().Trim());
			}
			else
			{
				this.RecordCode = Guid.NewGuid();
			}
			this.LibraryCode = base.Request["lc"].ToString().Trim();
			this.OperateType = base.Request["t"].ToString().Trim();
			if (this.OperateType == "add")
			{
				this.BookAddDisplay();
			}
			if (this.OperateType == "upd")
			{
				this.BookEditDisplay();
			}
			this.Page.RegisterStartupScript("", string.Concat(new string[]
			{
				"<script>document.getElementById('frmLibrary').src='FileDestroyAssTabEdit.aspx?isSave=0&op=",
				this.OperateType,
				"&ac=",
				this.RecordCode.ToString(),
				"&lc=",
				this.LibraryCode,
				"'</script>"
			}));
		}
	}
	private void BookAddDisplay()
	{
		this.txtApplyPerson.Text = PTMultiClassesAction.GetNameOfUser(this.Session["yhdm"].ToString());
		this.txtApplyDate.Text = DateTime.Now.ToShortDateString();
	}
	private void BookEditDisplay()
	{
		string strWhere = string.Concat(new object[]
		{
			" RecordID='",
			this.RecordCode,
			"' and LibraryCode='",
			this.LibraryCode,
			"' "
		});
		DataTable list = this.amAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtApplyPerson.Text = PTMultiClassesAction.GetNameOfUser(dataRow["UserCode"].ToString());
			this.txtApplyDate.Text = Convert.ToDateTime(dataRow["RecordDate"].ToString()).ToShortDateString();
			this.txtRemark.Text = dataRow["Remark"].ToString();
		}
	}
	private OAFileDestroyMain GetData()
	{
		return new OAFileDestroyMain
		{
			AuditState = -1,
			IsConfirm = "0",
			RecordDate = (this.txtApplyDate.Text == "") ? DateTime.Now : Convert.ToDateTime(this.txtApplyDate.Text),
			RecordID = this.RecordCode,
			Remark = this.txtRemark.Text.Trim(),
			UserCode = this.Session["yhdm"].ToString(),
			LibraryCode = this.LibraryCode
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAFileDestroyMain data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.amAction.Add(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;";
				this.Page.RegisterStartupScript("", string.Concat(new string[]
				{
					"<script>document.getElementById('frmLibrary').src='FileDestroyAssTabEdit.aspx?isSave=1&op=",
					this.OperateType,
					"&ac=",
					this.RecordCode.ToString(),
					"&lc=",
					this.LibraryCode,
					"';</script>"
				}));
				this.JS.Text = "alert('保存成功!');";
			}
			else
			{
				this.JS.Text = "alert('没有相关数据可添加!');";
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.amAction.Update(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('没有相关数据可更新!');";
		}
	}
}


