using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_EquDeployPlan_EquDeployPlanView : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;

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
		bool arg_06_0 = base.IsPostBack;
	}
}


