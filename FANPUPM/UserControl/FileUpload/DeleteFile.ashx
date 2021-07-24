<%@ WebHandler Language="C#" Class="DeleteFile" %>

using System;
using System.IO;
using System.Web;
public class DeleteFile : IHttpHandler
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
		string path = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["File"]))
		{
			path = context.Request["file"];
			if (!File.Exists(path))
			{
				path = context.Server.MapPath(context.Request["File"]);
			}
		}
		try
		{
			if (File.Exists(path) && (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
			{
				File.SetAttributes(path, FileAttributes.Normal);
			}
			File.Delete(path);
		}
		catch
		{
			context.Response.Write("0");
		}
		context.Response.Write("1");
	}
}
