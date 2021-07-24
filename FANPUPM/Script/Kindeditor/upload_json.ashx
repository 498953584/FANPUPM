<%@ WebHandler Language="C#" Class="Upload" %>

using LitJson;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Web;
public class Upload : IHttpHandler
{
	private HttpContext context;
	public bool IsReusable
	{
		get
		{
			return true;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		string str = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);
		string path = "../../UploadFiles/KindeditorFile/";
		string str2 = str + "../../UploadFiles/KindeditorFile/";
		Hashtable hashtable = new Hashtable();
		hashtable.Add("image", "gif,jpg,jpeg,png,bmp");
		hashtable.Add("flash", "swf,flv");
		hashtable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
		hashtable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");
		int num = 1000000;
		this.context = context;
		HttpPostedFile httpPostedFile = context.Request.Files["imgFile"];
		if (httpPostedFile == null)
		{
			this.showError("请选择文件。");
		}
		string text = context.Server.MapPath(path);
		if (!Directory.Exists(text))
		{
			this.showError("上传目录不存在。");
		}
		string text2 = context.Request.QueryString["dir"];
		if (string.IsNullOrEmpty(text2))
		{
			text2 = "image";
		}
		if (!hashtable.ContainsKey(text2))
		{
			this.showError("目录名不正确。");
		}
		string fileName = httpPostedFile.FileName;
		string text3 = Path.GetExtension(fileName).ToLower();
		if (httpPostedFile.InputStream == null || httpPostedFile.InputStream.Length > (long)num)
		{
			this.showError("上传文件大小超过限制。");
		}
		if (string.IsNullOrEmpty(text3) || Array.IndexOf<string>(((string)hashtable[text2]).Split(new char[]
		{
			','
		}), text3.Substring(1).ToLower()) == -1)
		{
			this.showError("上传文件扩展名是不允许的扩展名。\n只允许" + (string)hashtable[text2] + "格式。");
		}
		text = text + text2 + "/";
		str2 = str2 + text2 + "/";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string str3 = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
		text = text + str3 + "/";
		str2 = str2 + str3 + "/";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string str4 = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + text3;
		string filename = text + str4;
		httpPostedFile.SaveAs(filename);
		string value = str2 + str4;
		Hashtable hashtable2 = new Hashtable();
		hashtable2["error"] = 0;
		hashtable2["url"] = value;
		context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
		context.Response.Write(JsonMapper.ToJson(hashtable2));
		context.Response.End();
	}
	private void showError(string message)
	{
		Hashtable hashtable = new Hashtable();
		hashtable["error"] = 1;
		hashtable["message"] = message;
		this.context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
		this.context.Response.Write(JsonMapper.ToJson(hashtable));
		this.context.Response.End();
	}
}
