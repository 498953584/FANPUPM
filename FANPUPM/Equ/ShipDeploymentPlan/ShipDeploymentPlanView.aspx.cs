using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_ShipDeploymentPlan_ShipDeploymentPlanView : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private PTdbmService dbmSer = new PTdbmService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty("ic"))
		{
			this.id = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string.IsNullOrEmpty(this.id);
		}
	}
}


