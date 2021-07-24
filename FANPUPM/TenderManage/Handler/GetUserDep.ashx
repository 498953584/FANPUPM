<%@ WebHandler Language="C#" Class="GetUserDep" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web;
public class GetUserDep : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["usercode"]))
		{
			PTYhmcService pTYhmcService = new PTYhmcService();
			PTyhmc byId = pTYhmcService.GetById(context.Request["usercode"]);
			context.Response.Write(byId.Bm.V_BMMC);
		}
	}
}
