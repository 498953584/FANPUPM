using ASP;
using System;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class easyui : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.GetPostBackEventReference(this);
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		base.Response.Write("Button1_Click");
	}
	protected void btnConfirm3_Click(object sender, EventArgs e)
	{
		base.Response.Write("btnConfirm3_Click");
	}
	protected void LinkButton1_Click(object sender, EventArgs e)
	{
		base.Response.Write("btnConfirm3_Click");
		new FileInfo("");
	}
}


