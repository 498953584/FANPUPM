<%@ WebHandler Language="C#" Class="IsLeafNode" %>

using cn.justwin.BLL;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Web;
public class IsLeafNode : IHttpHandler
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
		string item = ConfigHelper.Get("LaborCBSCode");
		string item2 = ConfigHelper.Get("StuffCBSCode");
		string item3 = ConfigHelper.Get("MachineCBSCode");
		string item4 = ConfigHelper.Get("OutSourceCBSCode");
		string item5 = ConfigHelper.Get("ElseCBSCode");
		context.Response.ContentType = "text/plain";
		string s = string.Empty;
		string json = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["CBSCode"]))
		{
			json = context.Request["CBSCode"].ToString();
		}
		List<string> listFromJson = JsonHelper.GetListFromJson(json);
		if (listFromJson.Contains(item) || listFromJson.Contains(item2) || listFromJson.Contains(item3) || listFromJson.Contains(item4) || listFromJson.Contains(item5))
		{
			s = "0";
		}
		else
		{
			s = "1";
		}
		context.Response.Write(s);
	}
}
