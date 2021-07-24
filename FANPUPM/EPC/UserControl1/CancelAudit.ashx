<%@ WebHandler Language="C#" Class="CandcelAudit" %>

using com.jwsoft.pm.entpm.action;
using System;
using System.Web;
using System.Web.SessionState;
public class CandcelAudit : IHttpHandler, IRequiresSessionState
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
		string g = context.Request["fid"];
		string businessCode = context.Request["bcode"];
		string businessClass = context.Request["bclass"];
		int num = FlowAuditAction.DealAudit(new Guid(g), businessCode, businessClass, -1);
		if (num > 0)
		{
			HttpContext.Current.Response.Write("1");
			return;
		}
		HttpContext.Current.Response.Write("0");
	}
}
