using ASP;
using System;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class GetChart : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		string name = base.Request.QueryString["Chart"];
		if (this.Session[name] != null)
		{
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
			memoryStream = (System.IO.MemoryStream)this.Session[name];
			base.Response.OutputStream.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
			memoryStream.Close();
			this.Session.Remove(name);
		}
	}
}


