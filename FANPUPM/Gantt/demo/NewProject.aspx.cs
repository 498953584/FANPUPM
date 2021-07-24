using ASP;
using PluSoft.Data;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class demo_NewProject : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		Project project = new Project();
		System.Collections.Hashtable data = project.GetData();
		string str = DBProject.SaveProject(data);
		base.Response.Write("新项目UID：" + str + ", 您可以在<a href='Projects.aspx'>项目列表</a>页面打开本项目");
	}
}


