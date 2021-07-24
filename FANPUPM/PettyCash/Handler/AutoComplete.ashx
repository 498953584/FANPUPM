<%@ WebHandler Language="C#" Class="AutoComplete" %>

using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class AutoComplete : IHttpHandler
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
		string strA = string.Empty;
		string s = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["type"]))
		{
			strA = context.Request["type"];
			PCPettyCashService source = new PCPettyCashService();
			List<string> list = new List<string>();
			if (string.Compare(strA, "payer", true) == 0)
			{
				list = (
					from c in source
					select c.Payer).Distinct<string>().ToList<string>();
			}
			else
			{
				if (string.Compare(strA, "account", true) == 0)
				{
					list = (
						from c in source
						select c.Account).Distinct<string>().ToList<string>();
				}
				else
				{
					if (string.Compare(strA, "bank", true) == 0)
					{
						list = (
							from c in source
							select c.Bank).Distinct<string>().ToList<string>();
					}
				}
			}
			if (list.Count > 0)
			{
				s = JsonHelper.JsonSerializer<string[]>(list.ToArray());
			}
		}
		context.Response.Write(s);
	}
}
