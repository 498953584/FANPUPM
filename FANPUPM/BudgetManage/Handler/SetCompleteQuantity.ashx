<%@ WebHandler Language="C#" Class="SetCompleteQuantity" %>

using cn.justwin.Domain;
using System;
using System.Web;
public class SetCompleteQuantity : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["ConsTaskId"]) && !string.IsNullOrEmpty(context.Request["Quantity"]))
		{
			string id = context.Request["ConsTaskId"];
			decimal resQuantityByConsQuantity = Convert.ToDecimal(context.Request["Quantity"]);
			ConstructTask byId = ConstructTask.GetById(id);
			byId.SetResQuantityByConsQuantity(resQuantityByConsQuantity);
			context.Response.Write("1");
			return;
		}
		context.Response.Write("0");
	}
}
