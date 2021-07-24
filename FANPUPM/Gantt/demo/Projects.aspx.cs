using ASP;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class demo_Projects : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
	}
	public string GetProjectListHTML()
	{
		System.Collections.ArrayList arrayList = DBProject.LoadProjects();
		string text = "<table border=1 ><tr><td>项目名称</td><td>项目UID</td><td></td></tr>";
		foreach (System.Collections.Hashtable hashtable in arrayList)
		{
			string text2 = hashtable["UID_"].ToString();
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"<tr><td>",
				hashtable["NAME_"],
				"</td><td>",
				text2,
				"</td><td><a href='Project.html?id=",
				text2,
				"'>打开</a> <a href='DeleteProject.aspx?id=",
				text2,
				"'>删除</a> <a href='ExportProject.aspx?id=",
				text2,
				"'>导出</a></td></tr>"
			});
		}
		text += "</table>";
		return text;
	}
}


