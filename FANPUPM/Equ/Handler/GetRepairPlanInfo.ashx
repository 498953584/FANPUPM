<%@ WebHandler Language="C#" Class="GetRepairPlanInfo" %>

using cn.justwin.Domain.Services;
using System;
using System.Web;
public class GetRepairPlanInfo : IHttpHandler
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
		EquRepairPlanService equRepairPlanService = new EquRepairPlanService();
		if (!string.IsNullOrEmpty(context.Request["id"]))
		{
			string id = context.Request["id"].ToString();
			equRepairPlanService.GetById(id);
		}
		context.Response.Write(empty);
	}
}
