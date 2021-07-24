<%@ WebHandler Language="C#" Class="GetUserSetting" %>

using cn.justwin.stockBLL.TableTopBLL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
public class GetUserSetting : IHttpHandler, IRequiresSessionState
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
			string userCode = context.Session["yhdm"].ToString();
			string a = context.Request["type"];
			DesktopBLL desktopBLL = new DesktopBLL();
			if (a == "1")
			{
				string ault = "1200";
				if (!string.IsNullOrEmpty(context.Request["width"]))
				{
					ault = context.Request["width"];
				}
				DataTable userSet = desktopBLL.GetUserSet(userCode, ault);
				string s = JsonConvert.SerializeObject(userSet);
				context.Response.Write(s);
			}
			else
			{
				DataTable userModelInfo = desktopBLL.GetUserModelInfo(userCode);
				string s2 = JsonConvert.SerializeObject(userModelInfo);
				context.Response.Write(s2);
			}
		}
		catch
		{
			context.Response.Write("-1");
		}
	}
}