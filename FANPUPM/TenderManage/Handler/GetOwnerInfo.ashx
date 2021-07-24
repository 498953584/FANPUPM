<%@ WebHandler Language="C#" Class="GetOwnerInfo" %>

using cn.justwin.Tender;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
public class GetOwnerInfo : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["ownerName"]))
		{
			HttpUtility.UrlDecode(context.Request["ownerName"]);
			DataTable ownerInfo = TenderInfo.GetOwnerInfo(context.Request["ownerName"].ToString());
			context.Response.Write(JsonConvert.SerializeObject(ownerInfo, Formatting.None));
		}
	}
}
