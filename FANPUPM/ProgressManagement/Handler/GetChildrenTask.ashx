<%@ WebHandler Language="C#" Class="GetChildrenTask" %>

using cn.justwin.ProgressManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;
using System.Web;
public class GetChildrenTask : IHttpHandler
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
			string taskUID = context.Request["TaskId"];
			string proVerId = context.Request["projectId"];
			DataTable childTask = ReportDetail.GetChildTask(proVerId, taskUID);
			if (childTask.Rows.Count > 0)
			{
				new JsonSerializerSettings();
				IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
				isoDateTimeConverter.DateTimeFormat = "yyyy-M-d";
				context.Response.Write(JsonConvert.SerializeObject(childTask, Formatting.Indented, new JsonConverter[]
				{
					isoDateTimeConverter
				}));
				return;
			}
		}
		context.Response.Write("0");
	}
}
