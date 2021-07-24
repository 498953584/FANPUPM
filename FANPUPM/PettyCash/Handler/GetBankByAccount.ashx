<%@ WebHandler Language="C#" Class="GetBankByAccount" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Linq;
using System.Web;
public class GetBankByAccount : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["account"]))
		{
			string account = context.Request["account"];
			PCPettyCashService source = new PCPettyCashService();
			string s = (
				from c in source
				where c.Account == account
				select c.Bank).FirstOrDefault<string>();
			context.Response.Write(s);
		}
	}
}
