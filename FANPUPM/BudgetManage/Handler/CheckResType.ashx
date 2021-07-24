<%@ WebHandler Language="C#" Class="CheckResType" %>

using cn.justwin.stockBLL.Domain;
using System;
using System.Web;
public class CheckResType : IHttpHandler
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
		string text = context.Request["resTypeId"];
		if (!string.IsNullOrEmpty(text))
		{
			string exceptId = context.Request["parent"];
			string s = CostAccounting.IsExistResType(text, exceptId) ? "1" : "0";
			context.Response.Write(s);
		}
	}
}
