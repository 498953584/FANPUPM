<%@ WebHandler Language="C#" Class="Popup" %>

using cn.justwin.Popup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
public class Popup : IHttpHandler, IRequiresSessionState
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
		try
		{
			context.Response.ContentType = "Application/json";
			string userCode = context.Session["yhdm"].ToString();
			List<Caution> list = CautionFactory.CreateCautions(userCode);
			CautionFactory.SetPopupRecord(list, userCode);
			context.Response.Write(JsonConvert.SerializeObject(list));
		}
		catch (Exception)
		{
			context.Response.ContentType = "text/plain";
			context.Response.Write("-1");
		}
	}
}
