using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_ErrorPage : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Application["Error"] != null)
		{
			string text = base.Application["Error"].ToString();
			this.lblErrorMsg.Text = text;
		}
	}
}


