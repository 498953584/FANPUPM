<%@ WebHandler Language="C#" Class="Handler" %>

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
		context.Response.ContentType = "text/plain";
		string str = context.Request["jsonpcallback"];
		context.Response.Write(str + "({\"Id\":\"1\", \"Name\": \"Bery\"})");
	}
}
