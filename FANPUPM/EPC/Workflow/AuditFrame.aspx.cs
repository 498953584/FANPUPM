using ASP;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
public partial class EPC_Workflow_AuditFrame : Page, IRequiresSessionState
{
	public string infoURL = "";
	public string auditURL = "";

	public System.Guid InstanceCode
	{
		get
		{
			object obj = this.ViewState["InstanceCode"];
			if (obj != null)
			{
				return (System.Guid)obj;
			}
			return System.Guid.Empty;
		}
		set
		{
			this.ViewState["InstanceCode"] = value;
		}
	}
	public int InstanceID
	{
		get
		{
			object obj = this.ViewState["InstanceID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["InstanceID"] = value;
		}
	}
	public string IsAllPass
	{
		get
		{
			object obj = this.ViewState["IsAllPass"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["IsAllPass"] = value;
		}
	}
	public string UserCode
	{
		get
		{
			object obj = this.ViewState["UserCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["UserCode"] = value;
		}
	}
	public string BusinessCode
	{
		get
		{
			object obj = this.ViewState["BUSINESSCODE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["BUSINESSCODE"] = value;
		}
	}
	protected string BusinessClass
	{
		get
		{
			return this.ViewState["BUSINESSCLASS"].ToString();
		}
		set
		{
			this.ViewState["BUSINESSCLASS"] = value;
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InstanceCode = new System.Guid(base.Request["ic"]);
			this.InstanceID = System.Convert.ToInt32(base.Request["id"]);
			this.Session["InstanceId"] = base.Request["id"];
			this.IsAllPass = base.Request["pass"];
			this.BusinessCode = base.Request["bc"].ToString();
			this.BusinessClass = base.Request["bcl"].ToString();
			this.UserCode = System.Convert.ToString(this.Session["yhdm"]);
			this.infoURL = string.Concat(new object[]
			{
				FlowAuditAction.DoWithUrl(this.BusinessCode),
				"ic=",
				this.InstanceCode,
				"&id=",
				this.InstanceID,
				"&pass=",
				this.IsAllPass
			});
			this.auditURL = string.Concat(new object[]
			{
				"/EPC/Workflow/AuditMain.aspx?ic=",
				this.InstanceCode,
				"&id=",
				this.InstanceID,
				"&nid=",
				base.Request["nid"],
				"&pass=",
				this.IsAllPass,
				"&bc=",
				this.BusinessCode,
				"&bcl=",
				this.BusinessClass
			});
		}
	}
}


