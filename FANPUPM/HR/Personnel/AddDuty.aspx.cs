using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Personnel_AddDuty : BasePage, IRequiresSessionState
{
	public System.Guid RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (System.Guid)this.ViewState["RECORDID"];
			}
			return System.Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	public string EmployeeCode
	{
		get
		{
			object obj = this.ViewState["OPERATETYPE"];
			if (obj != null)
			{
				return (string)this.ViewState["OPERATETYPE"];
			}
			return "";
		}
		set
		{
			this.ViewState["OPERATETYPE"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (base.Request["ec"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			this.EmployeeCode = base.Request["ec"].ToString().Trim();
		}
	}
	protected void btnAdd_Click(object sender, System.EventArgs e)
	{
		int num = HRPersonnelCommonAction.AddDuty(this.EmployeeCode, this.HdnDeptID.Value, this.HdnDutyID.Value);
		if (num > 0)
		{
			this.JS.Text = "returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('没有相关数据可添加!');";
	}
}


