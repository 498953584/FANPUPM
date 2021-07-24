using ASP;
using System;
using System.Net;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Z_Demo_TestRun : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		string str = HttpContext.Current.Request.ServerVariables["HTTP_VIA"];
		string str2 = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
		string str3 = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
		string userHostAddress = HttpContext.Current.Request.UserHostAddress;
		IPAddress[] hostAddresses = Dns.GetHostAddresses("fe80::39e4:600c:1681:5c07%11");
		string str4 = hostAddresses[0].ToString();
		base.Response.Write("HTTP_VIA: " + str + "</br>");
		base.Response.Write("HTTP_X_FORWARDED_FOR: " + str2 + "</br>");
		base.Response.Write("REMOTE_ADDR: " + str3 + "</br>");
		base.Response.Write("UserHostAddress: " + userHostAddress + "</br>");
		base.Response.Write("strgetIP: " + str4 + "</br>");
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		string ip = Z_Demo_TestRun.getIp();
		base.Response.Write("ip: " + ip + "</br>");
	}
	private static string getIp()
	{
		if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
		{
			return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[]
			{
				','
			})[0];
		}
		return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
	}
}


