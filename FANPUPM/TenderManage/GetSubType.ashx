<%@ WebHandler Language="C#" Class="GetSubType" %>

using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Web;
public class GetSubType : IHttpHandler
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
		if (context.Request["Type"] == null || string.IsNullOrEmpty(context.Request["Type"]))
		{
			context.Response.Write("");
			return;
		}
		System.Collections.Generic.List<TypeList> codeList = TypeList.GetCodeList(context.Request["Type"]);
		string text = "";
		for (int i = 0; i < codeList.Count; i++)
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				codeList[i].Value,
				",",
				codeList[i].Text,
				"|"
			});
		}
		if (text.Length == 0)
		{
			context.Response.Write(text);
			return;
		}
		context.Response.Write(text.Substring(0, text.Length - 1));
	}
}
