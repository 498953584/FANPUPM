<%@ WebHandler Language="C#" Class="ModifyPrjState" %>

using cn.justwin.PrjManager;
using System;
using System.Web;
public class ModifyPrjState : IHttpHandler
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
		string text = string.Empty;
		string text2 = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["PrjGuid"]))
		{
			text = context.Request["PrjGuid"];
		}
		if (!string.IsNullOrEmpty(context.Request["PrjState"]))
		{
			text2 = context.Request["PrjState"];
		}
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
		{
			ProjectInfo.UpdatePrjState(text, text2);
		}
		context.Response.Write("Hello World");
	}
}
