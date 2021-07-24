using ASP;
using System;
using System.Configuration;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class UploadifyDemo : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string str = ConfigurationManager.AppSettings["Mail"];
			string str2 = "annexId";
			this.hfldFolder.Value = str + "/" + str2;
		}
	}
}


