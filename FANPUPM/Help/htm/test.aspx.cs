using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Help_htm_test : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
	}
	protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.Page.RegisterStartupScript("script", "<script>alert('" + this.TreeView1.SelectedNode.ValuePath + "!');</script>");
	}
}


