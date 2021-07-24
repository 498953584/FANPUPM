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
public partial class HR_Organization_JobFamilyEdit : BasePage, IRequiresSessionState
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
		if (base.Request["cc"] == null || base.Request["t"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.RoleTypeCode = base.Request["cc"].ToString().Trim();
		this.OperateType = base.Request["t"].ToString().Trim();
		if (!this.Page.IsPostBack && this.OperateType == "upd")
		{
			this.ClassDisplay();
		}
	}
	private void ClassDisplay()
	{
		string strWhere = " RoleTypeCode='" + this.RoleTypeCode + "' ";
		DataTable list = this.mcAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtBookLibraryName.Text = list.Rows[0]["RoleTypeName"].ToString();
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
		}
	}
	private PTDRole GetData()
	{
		PTDRole pTDRole = new PTDRole();
		if (this.OperateType == "upd")
		{
			pTDRole.RoleTypeCode = this.RoleTypeCode;
		}
		else
		{
			pTDRole.RoleTypeCode = this.mcAction.GetNewRoleTypeCode(this.RoleTypeCode);
			pTDRole.ParentCode = this.RoleTypeCode;
		}
		pTDRole.RoleTypeName = this.txtBookLibraryName.Text.Trim();
		pTDRole.Remark = this.txtRemark.Text;
		return pTDRole;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		PTDRole data = this.GetData();
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


