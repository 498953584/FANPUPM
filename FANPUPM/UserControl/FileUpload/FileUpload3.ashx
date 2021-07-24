<%@ WebHandler Language="C#" Class="FileUpload3" %>

using System;
using System.IO;
using System.Web;
public class FileUpload3 : IHttpHandler
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
			string text2 = text + httpPostedFile.FileName;
			text2 = this.GetFile(text2, text, 1);
			string[] array = text2.Split(new char[]
			{
				';'
			});
			text2 = array[array.Length - 1];
			httpPostedFile.SaveAs(text2);
			context.Response.Write("1");
			return;
		}
		context.Response.Write("0");
	}
	private string GetFile(string filepath, string path, int number)
	{
		bool flag = true;
		string[] files = Directory.GetFiles(path);
		string[] array = files;
		for (int i = 0; i < array.Length; i++)
		{
			string a = array[i];
			if (a == filepath)
			{
				string[] array2 = filepath.Split(new char[]
				{
					'.'
				});
				filepath = array2[0];
				for (int j = 1; j < array2.Length - 1; j++)
				{
					filepath += array2[j];
				}
				if (number > 1)
				{
					filepath = filepath.Substring(0, filepath.Length - 2);
					filepath = filepath + number.ToString() + ")." + array2[array2.Length - 1];
				}
				else
				{
					filepath = filepath + "(1)." + array2[array2.Length - 1];
				}
				number++;
				flag = false;
			}
		}
		if (number == 1)
		{
			return filepath;
		}
		if (!flag)
		{
			filepath = filepath + ";" + this.GetFile(filepath, path, number);
		}
		return filepath;
	}
}
