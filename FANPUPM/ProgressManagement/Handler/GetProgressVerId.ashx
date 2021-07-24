<%@ WebHandler Language="C#" Class="GetProgressVerId" %>

using cn.justwin.BLL.ProgressManagement;
using System;
using System.Web;
public class GetProgressVerId : IHttpHandler
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
		string progressId = context.Request["id"];
		string progressVersionId = cn.justwin.BLL.ProgressManagement.Version.GetProgressVersionId(progressId);
		context.Response.Write(progressVersionId);
	}
}
