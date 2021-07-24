using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class EPC_UserControl1_webTreeTS : PageBase, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack && base.Request["fj"] != null)
		{
		}
	}
}


