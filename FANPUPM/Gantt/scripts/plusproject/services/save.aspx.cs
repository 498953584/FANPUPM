using ASP;
using PluSoft.Utils;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
public partial class project_services_save : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		string text = base.Request["project"];
		if (!string.IsNullOrEmpty(text))
		{
			Hashtable dataProject = (Hashtable)JSON.Decode(text);
			try
			{
				string s = DBProject.SaveProject(dataProject);
				base.Response.Write(s);
			}
			catch
			{
				base.Response.Write("error");
			}
		}
	}
}


