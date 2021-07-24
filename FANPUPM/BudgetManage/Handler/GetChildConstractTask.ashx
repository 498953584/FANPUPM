<%@ WebHandler Language="C#" Class="GetChildConstractTask" %>

using cn.justwin.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;
using System.Web;
public class GetChildConstractTask : IHttpHandler
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
		context.Response.ContentType = "Application/json";
		if (!string.IsNullOrEmpty(context.Request["TaskId"]))
		{
			string taskId = context.Request["TaskId"];
			DataTable taskChild = BudContractTask.GetTaskChild(taskId);
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
