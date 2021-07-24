<%@ WebHandler Language="C#" Class="TemplateConfirm" %>

using com.jwsoft.pm.entpm.action;
using System;
using System.Web;
public class TemplateConfirm : IHttpHandler
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
		string value = context.Request["tid"];
		bool flag = false;
		if (!string.IsNullOrEmpty(value))
		{
			flag = FlowTemplateAction.IsDelTemplate(System.Convert.ToInt32(value));
		}
		if (flag)
		{
			context.Response.Write("1");
			return;
		}
		context.Response.Write("0");
	}
}
