<%@ WebHandler Language="C#" Class="GetEditProgress" %>

using cn.justwin.BLL.ProgressManagement;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
public class GetEditProgress : IHttpHandler
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
		context.Response.ContentType = "application/json";
		string verId = context.Request["verId"];
		DataTable partById = Progress.GetPartById(verId);
		string s = JsonConvert.SerializeObject(partById);
		context.Response.Write(s);
	}
}
