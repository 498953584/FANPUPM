<%@ WebHandler Language="C#" Class="GetDepFullName" %>

using cn.justwin.BLL;
using System;
using System.Web;
public class GetDepFullName : IHttpHandler
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
		string s = string.Empty;
		if (string.IsNullOrEmpty(context.Request["depCode"]))
		{
			int depCode = Convert.ToInt32(context.Request["depCode"]);
			Department department = new Department();
			s = department.GetDepFullName(depCode);
		}
		context.Response.Write(s);
	}
}
