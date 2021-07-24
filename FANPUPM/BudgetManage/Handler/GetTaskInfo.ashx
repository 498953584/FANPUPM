<%@ WebHandler Language="C#" Class="GetTaskInfo" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Web;
public class GetTaskInfo : IHttpHandler
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
		context.Response.ContentType = "text/json";
		string s = string.Empty;
		BudTaskService budTaskService = new BudTaskService();
		if (!string.IsNullOrEmpty(context.Request["taskId"]))
		{
			string taskId = context.Request["taskId"].ToString();
			BudTask taskById = budTaskService.GetTaskById(taskId);
			s = JsonConvert.SerializeObject(taskById);
		}
		context.Response.Write(s);
	}
}
