<%@ WebHandler Language="C#" Class="GetPrjStartDate" %>

using cn.justwin.Tender;
using System;
using System.Web;
public class GetPrjStartDate : IHttpHandler
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
			s = TenderInfo.GetStartDate(text);
		}
		context.Response.Write(s);
	}
}
