<%@ WebHandler Language="C#" Class="GetFiles" %>

using cn.justwin.Serialize;
using cn.justwin.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
public class GetFiles : IHttpHandler
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
		context.Response.ContentType = "Application/json";
		string text = string.Empty;
		string s = string.Empty;
		string arg_21_0 = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["Path"]))
		{
			text = context.Request["Path"];
		}
		List<MergerFolder> list = null;
		if (!string.IsNullOrEmpty(context.Request["merger"]))
		{
			list = JsonConvert.DeserializeObject<List<MergerFolder>>(context.Request["merger"]);
		}
		List<Annex> list2 = new List<Annex>();
		if (list != null)
		{
			string str = text.Substring(text.LastIndexOf('/') + 1);
			foreach (MergerFolder current in list)
			{
				try
				{
					DirectoryUtility directoryUtility = new DirectoryUtility(current.Path + str);
					list2.AddRange(directoryUtility.GetAnnex(true));
				}
				catch
				{
				}
			}
		}
		try
		{
			DirectoryUtility directoryUtility = new DirectoryUtility(text);
			list2.AddRange(directoryUtility.GetAnnex());
		}
		catch
		{
		}
		ISerializable serializable = new cn.justwin.Serialize.JsonSerializer();
		s = serializable.Serialize<List<Annex>>(list2);
		context.Response.Write(s);
	}
}
