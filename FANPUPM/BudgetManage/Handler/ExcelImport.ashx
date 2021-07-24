<%@ WebHandler Language="C#" Class="ExcelImport" %>

using cn.justwin.Excel;
using System;
using System.Data;
using System.Web;
public class ExcelImport : IHttpHandler
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
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["ExcelName"]))
		{
			text = context.Request["ExcelName"];
		}
		if (!string.IsNullOrEmpty(context.Request["SheetName"]))
		{
			text2 = context.Request["SheetName"];
		}
		if (!string.IsNullOrEmpty(context.Request["Columns"]))
		{
			text3 = context.Request["Columns"];
		}
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
		{
			ExcelUtility excelUtility = new ExcelUtility(text);
			DataTable data = excelUtility.GetData(text2);
			string[] array = text3.Split(new char[]
			{
				','
			});
			if (array.Length != data.Columns.Count)
			{
				context.Response.Write(array.Length + "|" + data.Columns.Count);
			}
			context.Response.Write("1");
			return;
		}
		context.Response.Write(string.Concat(new object[]
		{
			text,
			'|',
			text2,
			'|',
			text3
		}));
	}
}
