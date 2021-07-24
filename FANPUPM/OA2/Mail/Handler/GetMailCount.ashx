<%@ WebHandler Language="C#" Class="GetMailCount" %>

using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.SessionState;
public class GetMailCount : IHttpHandler, IRequiresSessionState
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
		MailService mailService = new MailService();
		string userCode = context.Session["yhdm"].ToString();
		int[] array = new int[5];
		array[0] = mailService.GetCount("", "", "", WebUtil.GetUserNames(userCode), null, null, "I", new bool?(true), new bool?(false));
		array[1] = mailService.GetCount("", "", WebUtil.GetUserNames(userCode), "", null, null, "D", new bool?(true), null);
		array[2] = mailService.GetCount("", "", WebUtil.GetUserNames(userCode), "", null, null, "R", new bool?(true), new bool?(false));
		array[3] = mailService.GetCount("", "", WebUtil.GetUserNames(userCode), "", null, null, "O", new bool?(true), null);
		IQueryable<Mail> source = mailService.AsQueryable<Mail>();
		source = 
			from m in source
			where m.IsValid == false && ((m.MailType == "I" && m.MailTo == userCode) || (m.MailType == "O" && m.MailFrom == userCode))
			select m;
		source = 
			from m in source
			orderby m.InputDate descending
			select m;
		array[4] = source.Count<Mail>();
		string s = JsonHelper.JsonSerializer<int[]>(array);
		context.Response.Write(s);
	}
}
