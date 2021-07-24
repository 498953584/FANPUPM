using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.sysManage.common;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_zdgl_depttree : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		CreatDepTree creatDepTree = new CreatDepTree(this.tv.Nodes);
		creatDepTree.EnabledLink = true;
		creatDepTree.Target = "rFrame";
	}
}


