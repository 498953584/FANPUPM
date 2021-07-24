<%@ WebHandler Language="C#" Class="GetFilesCount" %>

using cn.justwin.PrjManager;
using System;
using System.Web;
public class GetFilesCount : IHttpHandler
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
		string prjId = context.Request["prjId"];
		string prjCompletedId = context.Request["prjComId"];
		int filesCount = CompletedAnnex.GetFilesCount(prjId, prjCompletedId);
		context.Response.Write(filesCount);
	}
}
