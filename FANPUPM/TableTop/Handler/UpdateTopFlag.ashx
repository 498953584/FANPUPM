<%@ WebHandler Language="C#" Class="UpdateTopFlag" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web;
using System.Web.SessionState;
public class UpdateTopFlag : IHttpHandler, IRequiresSessionState
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
			context.Response.ContentType = "text/plain";
			string a = string.Empty;
			string value = string.Empty;
			if (!string.IsNullOrEmpty(context.Request["modelId"]))
			{
				a = context.Request["modelId"];
			}
			if (!string.IsNullOrEmpty(context.Request["pk"]))
			{
				value = context.Request["pk"];
			}
			if (a == "2801")
			{
				PTDBSJService pTDBSJService = new PTDBSJService();
				PTDBSJ byId = pTDBSJService.GetById(Convert.ToInt32(value));
				if (byId != null)
				{
					byId.IsOpened = true;
					pTDBSJService.Update(byId);
				}
				PTDBSJTodayService pTDBSJTodayService = new PTDBSJTodayService();
				PTDBSJToday byId2 = pTDBSJTodayService.GetById(Convert.ToInt32(value));
				if (byId2 != null)
				{
					byId2.IsOpened = true;
					pTDBSJTodayService.Update(byId2);
				}
			}
			else
			{
				if (a == "2842")
				{
					PTWarningService pTWarningService = new PTWarningService();
					PTWarning byId3 = pTWarningService.GetById(Convert.ToInt32(value));
					if (byId3 != null)
					{
						byId3.IsOpened = true;
						pTWarningService.Update(byId3);
					}
				}
			}
			context.Response.Write("1");
		}
		catch
		{
			context.Response.Write("0");
		}
	}
}
