using ASP;
using PluSoft.Utils;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
public partial class project_services_load : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		string projectUID = base.Request["projectuid"];
		string version = base.Request["version"];
		Hashtable o = DBProject.LoadProject(projectUID, version);
		string s = JSON.Encode(o);
		base.Response.Write(s);
	}
}


