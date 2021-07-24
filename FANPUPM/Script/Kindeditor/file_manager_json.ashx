<%@ WebHandler Language="C#" Class="FileManager" %>

using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
public class FileManager : IHttpHandler
{
	public class NameSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			FileInfo fileInfo = new FileInfo(x.ToString());
			FileInfo fileInfo2 = new FileInfo(y.ToString());
			return fileInfo.FullName.CompareTo(fileInfo2.FullName);
		}
	}
	public class SizeSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			FileInfo fileInfo = new FileInfo(x.ToString());
			FileInfo fileInfo2 = new FileInfo(y.ToString());
			return fileInfo.Length.CompareTo(fileInfo2.Length);
		}
	}
	public class TypeSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			FileInfo fileInfo = new FileInfo(x.ToString());
			FileInfo fileInfo2 = new FileInfo(y.ToString());
			return fileInfo.Extension.CompareTo(fileInfo2.Extension);
		}
	}
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
		string text = str + "../../UploadFiles/KindeditorFile/";
		string text2 = "gif,jpg,jpeg,png,bmp";
		string text3 = context.Server.MapPath(path);
		string text4 = context.Request.QueryString["dir"];
		if (!string.IsNullOrEmpty(text4))
		{
			if (Array.IndexOf<string>("image,flash,media,file".Split(new char[]
			{
				','
			}), text4) == -1)
			{
				context.Response.Write("Invalid Directory name.");
				context.Response.End();
			}
			text3 = text3 + text4 + "/";
			text = text + text4 + "/";
			if (!Directory.Exists(text3))
			{
				Directory.CreateDirectory(text3);
			}
		}
		string text5 = context.Request.QueryString["path"];
		text5 = (string.IsNullOrEmpty(text5) ? "" : text5);
		string path2;
		string value;
		string text6;
		string value2;
		if (text5 == "")
		{
			path2 = text3;
			value = text;
			text6 = "";
			value2 = "";
		}
		else
		{
			path2 = text3 + text5;
			value = text + text5;
			text6 = text5;
			value2 = Regex.Replace(text6, "(.*?)[^\\/]+\\/$", "$1");
		}
		string text7 = context.Request.QueryString["order"];
		text7 = (string.IsNullOrEmpty(text7) ? "" : text7.ToLower());
		if (Regex.IsMatch(text5, "\\.\\."))
		{
			context.Response.Write("Access is not allowed.");
			context.Response.End();
		}
		if (text5 != "" && !text5.EndsWith("/"))
		{
			context.Response.Write("Parameter is not valid.");
			context.Response.End();
		}
		if (!Directory.Exists(path2))
		{
			context.Response.Write("Directory does not exist.");
			context.Response.End();
		}
		string[] directories = Directory.GetDirectories(path2);
		string[] files = Directory.GetFiles(path2);
		string a;
		if ((a = text7) != null)
		{
			if (a == "size")
			{
				Array.Sort(directories, new FileManager.NameSorter());
				Array.Sort(files, new FileManager.SizeSorter());
				goto IL_2BA;
			}
			if (a == "type")
			{
				Array.Sort(directories, new FileManager.NameSorter());
				Array.Sort(files, new FileManager.TypeSorter());
				goto IL_2BA;
			}
			if (!(a == "name"))
			{
			}
		}
		Array.Sort(directories, new FileManager.NameSorter());
		Array.Sort(files, new FileManager.NameSorter());
		IL_2BA:
		Hashtable hashtable = new Hashtable();
		hashtable["moveup_dir_path"] = value2;
		hashtable["current_dir_path"] = text6;
		hashtable["current_url"] = value;
		hashtable["total_count"] = directories.Length + files.Length;
		List<Hashtable> list = new List<Hashtable>();
		hashtable["file_list"] = list;
		for (int i = 0; i < directories.Length; i++)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directories[i]);
			Hashtable hashtable2 = new Hashtable();
			hashtable2["is_dir"] = true;
			hashtable2["has_file"] = (directoryInfo.GetFileSystemInfos().Length > 0);
			hashtable2["filesize"] = 0;
			hashtable2["is_photo"] = false;
			hashtable2["filetype"] = "";
			hashtable2["filename"] = directoryInfo.Name;
			hashtable2["datetime"] = directoryInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
			list.Add(hashtable2);
		}
		for (int j = 0; j < files.Length; j++)
		{
			FileInfo fileInfo = new FileInfo(files[j]);
			Hashtable hashtable3 = new Hashtable();
			hashtable3["is_dir"] = false;
			hashtable3["has_file"] = false;
			hashtable3["filesize"] = fileInfo.Length;
			hashtable3["is_photo"] = (Array.IndexOf<string>(text2.Split(new char[]
			{
				','
			}), fileInfo.Extension.Substring(1).ToLower()) >= 0);
			hashtable3["filetype"] = fileInfo.Extension.Substring(1);
			hashtable3["filename"] = fileInfo.Name;
			hashtable3["datetime"] = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
			list.Add(hashtable3);
		}
		context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
		context.Response.Write(JsonMapper.ToJson(hashtable));
		context.Response.End();
	}
}
