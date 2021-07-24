<%@ WebHandler Language="C#" Class="GetMainPlan" %>

using cn.justwin.BLL.ProgressManagement;
using System;
using System.Web;
public class GetMainPlan : IHttpHandler
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
		string text = context.Request["prjId"];
		string s = string.Empty;
		if (!string.IsNullOrEmpty(text))
		{
			s = Progress.GetMainPlanName(text);
		}
		context.Response.Write(s);
	}
}
