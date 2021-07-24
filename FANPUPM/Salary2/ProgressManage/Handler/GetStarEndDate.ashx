<%@ WebHandler Language="C#" Class="GetStarEndDate" %>

using cn.justwin.PrjManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;
using System.Web;
public class GetStarEndDate : IHttpHandler
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
		context.Response.ContentType = "application/json";
		string prjId = context.Request["prjId"];
		DataTable projectDate = ProjectInfo.GetProjectDate(prjId);
		if (projectDate == null || projectDate.Rows.Count == 0)
		{
			context.Response.Write("0");
			return;
		}
		new JsonSerializerSettings();
		IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
		isoDateTimeConverter.DateTimeFormat = "yyyy-M-d";
		DataTable value = projectDate;
		context.Response.Write(JsonConvert.SerializeObject(value, Formatting.Indented, new JsonConverter[]
		{
			isoDateTimeConverter
		}));
	}
}
