<%@ WebHandler Language="C#" Class="GetNextTaskType" %>

using cn.justwin.Domain;
using System;
using System.Web;
public class GetNextTaskType : IHttpHandler
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
		if (context.Request["layer"] != null)
		{
			int layer = Convert.ToInt32(context.Request["layer"].ToString());
			string typeIdByLayer = BudTaskType.GetTypeIdByLayer(layer);
			context.Response.Write(typeIdByLayer);
		}
	}
}
