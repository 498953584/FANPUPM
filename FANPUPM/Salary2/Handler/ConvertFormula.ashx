<%@ WebHandler Language="C#" Class="ConvertFormula" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class ConvertFormula : IHttpHandler
{
	public class SaBooksItem
	{
		public string Id
		{
			get;
			set;
		}
		public string ItemName
		{
			get;
			set;
		}
	}
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
		string s = string.Empty;
		SASalaryBooksItemService source = new SASalaryBooksItemService();
		if (!string.IsNullOrEmpty(context.Request["type"]))
		{
			a = context.Request["type"].ToString();
			if (a == "ReplaceFormula")
			{
				string text = string.Empty;
				if (!string.IsNullOrEmpty(context.Request["formula"]) && !string.IsNullOrEmpty(context.Request["saBooksId"]))
				{
					text = context.Request["formula"].ToString().Replace("%2C", "+");
					string sabooksId = context.Request["saBooksId"];
					string[] array = text.Split(new char[]
					{
						'['
					});
					List<string> list = new List<string>();
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text2 = array2[i];
						if (text2.Contains(']'))
						{
							string item = text2.Substring(0, text2.IndexOf(']'));
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
					}
					List<ConvertFormula.SaBooksItem> list2 = new List<ConvertFormula.SaBooksItem>();
					SASalaryItemService source2 = new SASalaryItemService();
					new SAUserSalaryBooksService();
					foreach (string itemName in list)
					{
						SASalaryItem saItem = (
							from si in source2
							where si.Name == itemName
							select si).FirstOrDefault<SASalaryItem>();
						if (saItem != null)
						{
							SASalaryBooksItem sASalaryBooksItem = (
								from sbi in source
								where sbi.BooksId == sabooksId && sbi.ItemId == saItem.Id
								select sbi).FirstOrDefault<SASalaryBooksItem>();
							if (sASalaryBooksItem != null)
							{
								ConvertFormula.SaBooksItem saBooksItem = new ConvertFormula.SaBooksItem();
								if (saItem != null)
								{
									saBooksItem.Id = "[" + sASalaryBooksItem.Id + "]";
									saBooksItem.ItemName = "[" + saItem.Name + "]";
									list2.Add(saBooksItem);
								}
							}
						}
					}
					foreach (ConvertFormula.SaBooksItem current in list2)
					{
						text = text.Replace(current.ItemName, current.Id);
					}
					s = text;
				}
			}
		}
		context.Response.Write(s);
	}
}
