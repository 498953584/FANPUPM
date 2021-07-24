<%@ WebHandler Language="C#" Class="GetChildTask" %>

using cn.justwin.Domain.Services;
using cn.justwin.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;
using System.Web;
public class GetChildTask : IHttpHandler
{
	private BudTaskService budTaskSer = new BudTaskService();
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		context.Response.ContentType = "Application/json";
		if (!string.IsNullOrEmpty(context.Request["TaskId"]))
		{
			string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
			string taskId = context.Request["TaskId"];
			DataTable taskChild = this.budTaskSer.GetTaskChild(taskId, isWBSRelevance);
			if (taskChild.Rows.Count > 0)
			{
				new JsonSerializerSettings();
				string s = JsonConvert.SerializeObject(taskChild, Formatting.Indented, new JsonConverter[]
				{
					new IsoDateTimeConverter
					{
						DateTimeFormat = "yyyy-MM-dd"
					}
				});
				context.Response.Write(s);
				return;
			}
		}
		context.Response.Write("0");
	}
}
