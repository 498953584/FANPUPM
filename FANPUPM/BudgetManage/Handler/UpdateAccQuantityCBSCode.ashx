<%@ WebHandler Language="C#" Class="UpdateAccQuantityCBSCode" %>

using cn.justwin.Domain;
using System;
using System.Web;
public class UpdateAccQuantityCBSCode : IHttpHandler
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
		string text = context.Request["id"];
		decimal accountingQuantity = 0m;
		decimal.TryParse(context.Request["quantity"], out accountingQuantity);
		string cBSCode = context.Request["code"];
		if (!string.IsNullOrEmpty(text))
		{
			try
			{
				ConstructResource.UpdateAccountingQuantityCBSCode(text, accountingQuantity, cBSCode);
			}
			catch
			{
			}
		}
		context.Response.Write("");
	}
}
