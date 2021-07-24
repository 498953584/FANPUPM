using ASP;
using cn.justwin.Web;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_SysAdmin_UserSet_PasswordSet_UserSetFrame : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (ConfigHelper.ProjectType == "ZdContract")
			{
				this.tr_projectTree.Visible = false;
			}
			if (ConfigHelper.RTXEnabled == "1")
			{
				this.tr_rtxsetting.Visible = true;
			}
		}
	}
}


