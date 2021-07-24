<%@ WebHandler Language="C#" Class="ConvertRMB" %>

using System;
using System.Web;
public class ConvertRMB : IHttpHandler
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
		try
		{
			context.Response.ContentType = "text/plain";
			decimal number = Convert.ToDecimal(0.0);
			if (!string.IsNullOrEmpty(context.Request["para"]))
			{
				number = Convert.ToDecimal(context.Request["para"]);
			}
			string s = ConverRMB.Convert(number);
			context.Response.Write(s);
		}
		catch
		{
			context.Response.Write("零元整");
		}
	}
}
