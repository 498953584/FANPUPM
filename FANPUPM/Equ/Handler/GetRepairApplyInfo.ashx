<%@ WebHandler Language="C#" Class="GetRepairApplyInfo" %>

using cn.justwin.Domain.Services;
using System;
using System.Web;
public class GetRepairApplyInfo : IHttpHandler
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
		string empty = string.Empty;
		EquRepairApplyService equRepairApplyService = new EquRepairApplyService();
		if (!string.IsNullOrEmpty(context.Request["id"]))
		{
			string id = context.Request["id"].ToString();
			equRepairApplyService.GetById(id);
		}
		context.Response.Write(empty);
	}
}
