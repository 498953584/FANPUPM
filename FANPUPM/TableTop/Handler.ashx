<%@ WebHandler Language="C#" Class="Handler" %>

using cn.justwin.stockBLL.TableTopBLL;
using System;
using System.Web;
public class Handler : IHttpHandler
{
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		int num = 0;
		if (context.Request.QueryString["orderid"] != null)
		{
			DesktopBLL desktopBLL = new DesktopBLL();
			num = desktopBLL.updateMenuOrderID(context.Request.QueryString["orderid"].ToString());
		}
		context.Response.ContentType = "text/plain";
		context.Response.Write(num);
		context.Response.End();
	}
}
