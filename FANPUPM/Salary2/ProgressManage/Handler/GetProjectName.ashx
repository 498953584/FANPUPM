<%@ WebHandler Language="C#" Class="GetProjectName" %>

using cn.justwin.PrjManager;
using System;
using System.Web;
public class GetProjectName : IHttpHandler
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
		string prjId = context.Request["prjId"];
		string s = string.Empty;
		s = ProjectInfo.GetProjectName(prjId);
		context.Response.Write(s);
	}
}
