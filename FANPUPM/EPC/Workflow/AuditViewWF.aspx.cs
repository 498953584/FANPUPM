using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class ModuleSet_Workflow_AuditViewWF : NBasePage, IRequiresSessionState
{
	protected string BusinessCode
	{
		get
		{
			return this.ViewState["BUSINESSCODE"].ToString();
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
		if (!base.IsPostBack && base.Request["ic"] != null)
		{
			string text = base.Request["ic"].ToString();
			this.BusinessCode = base.Request["bc"].ToString();
			this.BusinessClass = base.Request["bcl"].ToString();
			this.frmAuditStatus.Attributes["src"] = string.Concat(new string[]
			{
				"AuditStatus.aspx?ic=",
				text,
				"&bc=",
				this.BusinessCode,
				"&bcl=",
				this.BusinessClass
			});
			this.frmFlowStatus.Attributes["src"] = string.Concat(new string[]
			{
				"FlowStatus.aspx?ic=",
				text,
				"&bc=",
				this.BusinessCode,
				"&bcl=",
				this.BusinessClass
			});
		}
	}
}


