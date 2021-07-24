<%@ WebHandler Language="C#" Class="GetFileNames" %>

using System;
using System.IO;
using System.Text;
using System.Web;
public class GetFileNames : IHttpHandler
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
		StringBuilder stringBuilder = new StringBuilder();
		if (!string.IsNullOrEmpty(context.Request["Folder"]))
		{
			path = context.Request["Folder"];
			DirectoryInfo directoryInfo = new DirectoryInfo(context.Server.MapPath(path));
			if (directoryInfo.Exists)
			{
				FileInfo[] files = directoryInfo.GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					FileInfo fileInfo = files[i];
					stringBuilder.Append(",").Append(fileInfo.Name);
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder = stringBuilder.Remove(0, 1);
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
