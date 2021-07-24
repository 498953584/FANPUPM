using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class ModuleSet_Workflow_ViewDBInfo : BasePage, IRequiresSessionState
{
	protected RecieveMsgAction rma
	{
		get
		{
			return new RecieveMsgAction();
		}
	}
	protected int RecordID
	{
		get
		{
			return System.Convert.ToInt32(this.ViewState["RECORDID"].ToString());
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
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
		if (!base.IsPostBack && base.Request["rid"] != null)
		{
			this.RecordID = System.Convert.ToInt32(base.Request["rid"].ToString());
			this.BusinessCode = base.Request["bc"].ToString();
			this.BusinessClass = base.Request["bcl"].ToString();
			DataTable dataTable = FlowAuditAction.OrganigerCode(this.RecordID);
			if (dataTable.Rows.Count > 0)
			{
				this.frmPage.Attributes["src"] = string.Concat(new string[]
				{
					"AuditViewPrint.aspx?ic=",
					dataTable.Rows[0]["InstanceCode"].ToString(),
					"&bc=",
					this.BusinessCode,
					"&bcl=",
					this.BusinessClass
				});
			}
			PTDBSJAction pTDBSJAction = new PTDBSJAction();
			pTDBSJAction.Delete(string.Concat(new object[]
			{
				" v_lxbm = '017' and v_YHDM = '",
				this.Session["yhdm"].ToString(),
				"' and i_XGID = '",
				this.RecordID,
				"'"
			}), 1);
		}
	}
}


