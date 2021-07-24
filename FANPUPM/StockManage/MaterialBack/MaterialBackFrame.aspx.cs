using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class StockManage_MaterialBack_MaterialBackFrame : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.uproject.UserCode = base.UserCode;
			this.uproject.SubPrjUrl = "~/StockManage/MaterialBack/SmMaterialBackAddList.aspx";
			this.uproject.TargetFrame = "InfoList";
		}
	}
}


