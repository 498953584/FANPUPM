using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class PlatForm_ShowInfoFrame : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.top.Attributes["src"] = "ptcs.txt";
			this.bottom.Attributes["src"] = "ptcs.txt";
		}
	}
}


