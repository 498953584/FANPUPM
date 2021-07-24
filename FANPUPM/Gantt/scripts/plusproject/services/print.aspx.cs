using ASP;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
public partial class project_services_print : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		int totalBytes = base.Request.TotalBytes;
		byte[] buffer = base.Request.BinaryRead(totalBytes);
		string str = "plusgantt.jpg";
		base.Response.ContentType = "application/octet-stream";
		base.Response.ContentEncoding = Encoding.UTF8;
		base.Response.AppendHeader("Content-Disposition", "attachment; filename=" + str);
		base.Response.Clear();
		base.Response.BinaryWrite(buffer);
	}
}


