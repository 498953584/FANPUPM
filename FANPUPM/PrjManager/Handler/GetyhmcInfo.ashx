<%@ WebHandler Language="C#" Class="GetyhmcInfo" %>

using cn.justwin.PrjManager;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
public class GetyhmcInfo : IHttpHandler
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
		string text = context.Request["id"];
		if (!string.IsNullOrEmpty(text))
		{
			DataTable memberInfo = PrjMember.GetMemberInfo(text);
			if (memberInfo.Rows.Count > 0)
			{
				context.Response.Write(JsonConvert.SerializeObject(memberInfo));
				return;
			}
			context.Response.Write("0");
		}
	}
}
