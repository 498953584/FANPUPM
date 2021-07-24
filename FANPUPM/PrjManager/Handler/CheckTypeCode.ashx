<%@ WebHandler Language="C#" Class="CheckTypeCode" %>

using cn.justwin.PrjManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
public class CheckTypeCode : IHttpHandler
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
		string value = string.Empty;
		string s = "1";
		if (!string.IsNullOrEmpty(context.Request["TypeCodes"]))
		{
			value = context.Request["TypeCodes"];
		}
		List<string> list = JsonConvert.DeserializeObject<List<string>>(value);
		foreach (string current in list)
		{
			IList<string> list2 = new List<string>();
			if (current.Length == 5)
			{
				list2 = ProjectInfo.GetChildPrjByTypeCode(current);
				foreach (string current2 in list2)
				{
					if (!list.Contains(current2))
					{
						s = "0";
					}
				}
			}
		}
		context.Response.Write(s);
	}
}
