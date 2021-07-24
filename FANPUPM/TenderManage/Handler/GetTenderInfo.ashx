<%@ WebHandler Language="C#" Class="GetTenderInfo" %>

using cn.justwin.Tender;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;
using System.Web;
public class GetTenderInfo : IHttpHandler
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
		string text = context.Request["guid"];
		string text2 = context.Request["part"];
		string userCode = context.Request["userCode"];
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
		{
			new JsonSerializerSettings();
			IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
			isoDateTimeConverter.DateTimeFormat = "yyyy-M-d";
			DataTable partTender = TenderInfo.GetPartTender(text, text2, userCode);
			context.Response.Write(JsonConvert.SerializeObject(partTender, Formatting.Indented, new JsonConverter[]
			{
				isoDateTimeConverter
			}));
		}
	}
}
