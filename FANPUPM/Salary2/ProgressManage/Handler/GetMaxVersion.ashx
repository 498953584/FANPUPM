<%@ WebHandler Language="C#" Class="GetMaxVersion" %>

using System;
using System.Web;
public class GetMaxVersion : IHttpHandler
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
		string s = "1";
		string text = context.Request["prjUID"];
		if (!string.IsNullOrEmpty(text))
		{
			s = DBProject.GetMaxVersion(text).ToString();
		}
		context.Response.Write(s);
	}
}
