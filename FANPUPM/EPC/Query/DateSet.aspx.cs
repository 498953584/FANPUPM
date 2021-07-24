using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Query_DateSet : PageBase, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected void Calendar1_SelectionChanged(object sender, EventArgs e)
	{
		this.Txtdate.Text = this.Calendar1.SelectedDate.Date.ToString("yyyy-MM-dd");
	}
	protected void Calendar1_Unload(object sender, EventArgs e)
	{
		this.Txtdate.Text = this.Calendar1.SelectedDate.Date.ToString("yyyy-MM-dd");
	}
}


