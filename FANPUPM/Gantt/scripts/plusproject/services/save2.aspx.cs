using ASP;
using PluSoft.Utils;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class Gantt_scripts_plusproject_services_save2 : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		string text = base.Request["project"];
		if (!string.IsNullOrEmpty(text))
		{
			Hashtable dataProject = (Hashtable)JSON.Decode(text);
			try
			{
				DBProject.SavePart2(dataProject);
				base.Response.Write("success");
			}
			catch
			{
				base.Response.Write("error");
			}
		}
	}
}


