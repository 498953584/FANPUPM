<%@ WebHandler Language="C#" Class="GetTenderFile" %>

using cn.justwin.BLL;
using System;
using System.Web;
public class GetTenderFile : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["file"]))
		{
			string s = StringUtility.FilesBind(context.Request["file"], "ProjectFile");
			context.Response.Write(s);
		}
	}
}
