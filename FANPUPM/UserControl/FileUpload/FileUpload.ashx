<%@ WebHandler Language="C#" Class="FileUpload" %>

using System;
using System.IO;
using System.Web;
public class FileUpload : IHttpHandler
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
		context.Response.Charset = "utf-8";
		HttpPostedFile httpPostedFile = context.Request.Files["Filedata"];
		string text = HttpContext.Current.Server.MapPath(context.Request["folder"]) + "\\";
		if (httpPostedFile != null)
		{
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			httpPostedFile.SaveAs(text + httpPostedFile.FileName);
			context.Response.Write("1");
			return;
		}
		context.Response.Write("0");
	}
}
