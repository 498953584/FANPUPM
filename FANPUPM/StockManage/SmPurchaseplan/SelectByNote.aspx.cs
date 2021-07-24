using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_SmPurchaseplan_SelectByNote : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.uproject.UserCode = base.UserCode;
			this.uproject.SubPrjUrl = this.hdType.Value;
			if (base.Request.QueryString["Type"] == "check")
			{
				this.uproject.SubPrjUrl = "~/StockManage/SmPurchaseplan/SelectByNoteList.aspx?Type=check";
			}
			else
			{
				this.uproject.SubPrjUrl = "~/StockManage/SmPurchaseplan/SelectByNoteList.aspx";
			}
			this.uproject.TargetFrame = "InfoList";
		}
	}
}


