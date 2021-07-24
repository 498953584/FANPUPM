<%@ WebHandler Language="C#" Class="GetProgressName" %>

using cn.justwin.BLL.ProgressManagement;
using System;
using System.Web;
public class GetProgressName : IHttpHandler
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
		string id = context.Request["id"];
		string progressName = Progress.GetProgressName(id);
		context.Response.Write(progressName);
	}
}
