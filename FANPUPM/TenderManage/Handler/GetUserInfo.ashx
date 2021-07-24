<%@ WebHandler Language="C#" Class="GetUserInfo" %>

using cn.justwin.Tender;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
public class GetUserInfo : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["usercode"]))
		{
			DataTable userInfo = TenderInfo.GetUserInfo(context.Request["usercode"].ToString());
			context.Response.Write(JsonConvert.SerializeObject(userInfo, Formatting.None));
		}
	}
}
