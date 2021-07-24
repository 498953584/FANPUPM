<%@ WebHandler Language="C#" Class="GetSalaryInfoPlain" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web;
public class GetSalaryInfoPlain : IHttpHandler
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
		string a = string.Empty;
		string arg_1B_0 = string.Empty;
		string s = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["type"]))
		{
			a = context.Request["type"].ToString();
			if (a == "IsAllowEdit")
			{
				if (!string.IsNullOrEmpty(context.Request["saItemId"]))
				{
					string id = context.Request["saItemId"].ToString();
					SASalaryItemService sASalaryItemService = new SASalaryItemService();
					SASalaryItem byId = sASalaryItemService.GetById(id);
					if (byId.Code == "TaxRate" || byId.Code == "Deduct")
					{
						s = "0";
					}
					else
					{
						s = "1";
					}
				}
			}
			else
			{
				if (a == "GetCountSaMonthNoPayoff")
				{
					if (!string.IsNullOrEmpty(context.Request["saBookId"]))
					{
						string id2 = context.Request["saBookId"].ToString();
						SASalaryBooksService sASalaryBooksService = new SASalaryBooksService();
						s = sASalaryBooksService.GetCountSaMonthNoPayoff(id2).ToString();
					}
				}
				else
				{
					if (a == "DelCountSaMonthNoPayoff" && !string.IsNullOrEmpty(context.Request["saBookId"]))
					{
						string id3 = context.Request["saBookId"].ToString();
						SASalaryBooksService sASalaryBooksService2 = new SASalaryBooksService();
						sASalaryBooksService2.DelSaMonthNoPayoff(id3);
					}
				}
			}
		}
		context.Response.Write(s);
	}
}
