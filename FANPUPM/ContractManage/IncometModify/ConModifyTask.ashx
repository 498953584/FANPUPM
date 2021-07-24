<%@ WebHandler Language="C#" Class="ConModifyTask" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Web;
public class ConModifyTask : IHttpHandler
{
	private string taskId = string.Empty;
	private string prjId = string.Empty;
	private string returnData = string.Empty;
	private string type = string.Empty;
	private string spType = string.Empty;
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		new BudConModifyTaskService();
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		context.Response.ContentType = "text/plain";
		if (!string.IsNullOrEmpty(context.Request["prjId"]))
		{
			this.prjId = context.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(context.Request["spType"]))
		{
			this.spType = context.Request["spType"];
		}
		if (!string.IsNullOrEmpty(context.Request["taskId"]))
		{
			this.taskId = context.Request["taskId"];
		}
		if (!string.IsNullOrEmpty(this.spType) && !string.IsNullOrEmpty(this.taskId))
		{
			BudContractTask taskById = budContractTaskService.GetTaskById(this.taskId);
			if (taskById != null)
			{
				this.returnData = JsonConvert.SerializeObject(taskById);
			}
		}
		context.Response.Write(this.returnData);
	}
}
