<%@ WebHandler Language="C#" Class="GetCalendars" %>

using System;
using System.Web;
public class GetCalendars : IHttpHandler
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
		string version = context.Request["version"];
		string calendars = DBProject.GetCalendars(prjId, version);
		context.Response.Write(calendars);
	}
}
