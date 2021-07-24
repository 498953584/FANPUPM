<%@ WebHandler Language="C#" Class="SAUserBooks" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Linq;
using System.Web;
public class SAUserBooks : IHttpHandler
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
		SAUserSalaryBooksService sAUserSalaryBooksService = new SAUserSalaryBooksService();
		new SAMonthSalaryService();
		SAPayoffService sAPayoffService = new SAPayoffService();
		string user = string.Empty;
		string text = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["user"]))
		{
			user = context.Request["user"].ToString();
			text = context.Request["booksId"].ToString();
			SAUserSalaryBooks sAUserSalaryBooks = (
				from su in sAUserSalaryBooksService
				where su.UserCode == user
				select su).FirstOrDefault<SAUserSalaryBooks>();
			if (sAUserSalaryBooks != null)
			{
				sAPayoffService.DelSaMonthNoPayoff(user);
				if (string.IsNullOrEmpty(text))
				{
					sAUserSalaryBooksService.Delete(sAUserSalaryBooks);
				}
				else
				{
					sAUserSalaryBooks.BooksId = text;
					sAUserSalaryBooksService.Update(sAUserSalaryBooks);
				}
			}
			else
			{
				sAUserSalaryBooksService.Add(new SAUserSalaryBooks
				{
					Id = Guid.NewGuid().ToString(),
					UserCode = user,
					BooksId = text
				});
			}
		}
		context.Response.Write("Hello World");
	}
}
