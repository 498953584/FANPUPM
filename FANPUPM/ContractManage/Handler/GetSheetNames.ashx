<%@ WebHandler Language="C#" Class="GetSheetNames" %>

using cn.justwin.contractBLL;
using cn.justwin.Serialize;
using System;
using System.Web;
public class GetSheetNames : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["SheetIds"]))
		{
			string text = context.Request["SheetIds"];
			if (!text.Contains("["))
			{
				text = "[" + text + "]";
			}
			IncometModifyBll incometModifyBll = new IncometModifyBll();
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(text);
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = incometModifyBll.GetSheetNameById(Convert.ToInt32(array[i]));
			}
			context.Response.Write(serializable.Serialize<string[]>(array2));
			return;
		}
		context.Response.Write("0");
	}
}
