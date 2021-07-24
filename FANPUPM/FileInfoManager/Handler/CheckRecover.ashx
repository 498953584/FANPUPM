<%@ WebHandler Language="C#" Class="CheckRecover" %>

using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using System;
using System.Web;
public class CheckRecover : IHttpHandler
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
		string json = context.Request["id"];
		string arrayToInStr = StringUtility.GetArrayToInStr(JsonHelper.GetListFromJson(json).ToArray());
		int repeatDirectoryCount = FileInfoBll.GetRepeatDirectoryCount(arrayToInStr);
		context.Response.Write(repeatDirectoryCount);
	}
}
