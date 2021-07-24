<%@ WebHandler Language="C#" Class="SalaryInfoNoMove" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web;
public class SalaryInfoNoMove : IHttpHandler
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
		string id = string.Empty;
		string id2 = string.Empty;
		string arg_21_0 = string.Empty;
		string a = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["page"]))
		{
			a = context.Request["page"];
		}
		if (!string.IsNullOrEmpty(context.Request["targetTr"]))
		{
			id = context.Request["targetTr"];
		}
		if (!string.IsNullOrEmpty(context.Request["currentTr"]))
		{
			id2 = context.Request["currentTr"];
		}
		if (!string.IsNullOrEmpty(context.Request["targetTr"]) && !string.IsNullOrEmpty(context.Request["currentTr"]))
		{
			if (a == "salaryItem")
			{
				SASalaryItemService sASalaryItemService = new SASalaryItemService();
				SASalaryItem byId = sASalaryItemService.GetById(id);
				int no = byId.No;
				SASalaryItem byId2 = sASalaryItemService.GetById(id2);
				int no2 = byId2.No;
				byId.No = no2;
				byId2.No = no;
				sASalaryItemService.Update(byId);
				sASalaryItemService.Update(byId2);
				return;
			}
			if (a == "salaryBooksItem")
			{
				SASalaryBooksItemService sASalaryBooksItemService = new SASalaryBooksItemService();
				SASalaryBooksItem byId3 = sASalaryBooksItemService.GetById(id);
				int no3 = byId3.No;
				SASalaryBooksItem byId4 = sASalaryBooksItemService.GetById(id2);
				int no4 = byId4.No;
				byId3.No = no4;
				byId4.No = no3;
				sASalaryBooksItemService.Update(byId3);
				sASalaryBooksItemService.Update(byId4);
			}
		}
	}
}
