using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class demo_DeleteProject : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		string text = System.Convert.ToString(base.Request["id"]);
		if (!string.IsNullOrEmpty(text))
		{
			DBProject.DeleteProject(text);
			base.Response.Write("项目删除成功。返回<a href='Projects.aspx'>项目列表</a>。");
			return;
		}
		base.Response.Write("请传入正确的项目UID");
	}
}


