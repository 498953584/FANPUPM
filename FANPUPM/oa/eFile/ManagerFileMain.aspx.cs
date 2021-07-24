using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_eFile_ManagerFileMain : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["sl"] != null)
			{
				string str = base.Request["sl"].ToString();
				this.frmFileClass.Attributes["src"] = "ManagerFileClassTree.aspx?sl=" + str;
				return;
			}
			this.frmFileClass.Attributes["src"] = "ManagerFileClassTree.aspx";
		}
	}
}


