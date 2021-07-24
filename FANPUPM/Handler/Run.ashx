<%@ WebHandler Language="C#" Class="Run" %>

using cn.justwin.BLL;
using System;
using System.Reflection;
using System.Web;
public class Run : IHttpHandler
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
		try
		{
			if (!string.IsNullOrEmpty(context.Request["assembly"]) && !string.IsNullOrEmpty(context.Request["className"]) && !string.IsNullOrEmpty(context.Request["method"]))
			{
				string assemblyString = context.Request["assembly"];
				string typeName = context.Request["className"];
				string name = context.Request["method"];
				string jsonString = context.Request["paras"];
				object[] args = JsonHelper.JsonDeserialize<object[]>(jsonString);
				object obj = Assembly.Load(assemblyString).CreateInstance(typeName);
				if (obj != null)
				{
					object obj2 = obj.GetType().InvokeMember(name, BindingFlags.InvokeMethod, null, obj, args);
					if (obj2 != null)
					{
						context.Response.Write(obj2.ToString());
					}
				}
			}
		}
		catch (Exception ex)
		{
			context.Response.Write("-1" + ex.Message);
		}
	}
}
