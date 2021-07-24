using ASP;
using PluSoft.Utils;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
public partial class Gantt_scripts_plusproject_services_load2 : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		string progressVerId = base.Request["projectuid"];
		Hashtable o = DBProject.LoadProject(progressVerId);
		string s = JSON.Encode(o);
		base.Response.Write(s);
	}
}


