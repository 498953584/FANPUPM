<%@ WebHandler Language="C#" Class="SaveAs" %>

using System;
using System.Web;
public class SaveAs : IHttpHandler
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
		string prjId = context.Request["id"];
		DBProject.SaveAsTask(prjId);
		context.Response.Write("1");
	}
}
