using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_WorkManage_StorageManage_StorageInitializtionFrame : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.WUCStorage.TargetFrame = "frmMatter";
			this.WUCStorage.PrjUrl = "StorageInitializtion.aspx";
		}
	}
}


