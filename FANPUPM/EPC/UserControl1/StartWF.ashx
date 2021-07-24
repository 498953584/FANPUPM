<%@ WebHandler Language="C#" Class="StartWF" %>

using System;
using System.Web;
public class StartWF : IHttpHandler
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
		context.Response.ContentType = "text/plain";
		context.Response.Write("Hello World");
	}
}
