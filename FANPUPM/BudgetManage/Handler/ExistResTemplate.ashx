<%@ WebHandler Language="C#" Class="ExistResTemplate" %>

using cn.justwin.Domain;
using System;
using System.Web;
public class ExistResTemplate : IHttpHandler
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
		string s = string.Empty;
		string text = context.Request["templateName"];
		if (!string.IsNullOrEmpty(text))
		{
			bool flag = ResTemplate.isExistTemplate(text);
			if (flag)
			{
				s = "1";
			}
			else
			{
				s = "0";
			}
		}
		context.Response.Write(s);
	}
}
