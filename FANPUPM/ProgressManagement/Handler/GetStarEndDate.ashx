<%@ WebHandler Language="C#" Class="GetStarEndDate" %>

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
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
		string progressVerId = context.Request["verId"];
		ArrayList startFinish = DBProject.GetStartFinish(progressVerId);
		if (startFinish == null || startFinish.Count == 0)
		{
			context.Response.Write("0");
			return;
		}
		new JsonSerializerSettings();
		IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
		isoDateTimeConverter.DateTimeFormat = "yyyy-M-d";
		context.Response.Write(JsonConvert.SerializeObject(startFinish, Formatting.Indented, new JsonConverter[]
		{
			isoDateTimeConverter
		}));
	}
}
