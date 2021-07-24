<%@ WebHandler Language="C#" Class="GetDepEmpChildren" %>

using cn.justwin.Domain.EasyUI;
using cn.justwin.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class GetDepEmpChildren : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["id"]))
		{
			string id = context.Request["id"];
			DepEmployee depEmployee = new DepEmployee();
			IList<DepEmployee> children = depEmployee.GetChildren(id);
			JsonSerializer jsonSerializer = new JsonSerializer();
			s = jsonSerializer.Serialize<DepEmployee[]>(children.ToArray<DepEmployee>());
		}
		context.Response.Write(s);
	}
}
