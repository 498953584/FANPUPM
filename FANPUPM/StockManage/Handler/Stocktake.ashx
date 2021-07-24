<%@ WebHandler Language="C#" Class="Stocktake" %>

using cn.justwin.stockBLL;
using System;
using System.Web;
public class Stocktake : IHttpHandler
{
	private StocktakeBll stocktakeBll = new StocktakeBll();
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
		string treasuryCode = string.Empty;
		int num = 0;
		string stocktakeDate = string.Empty;
		string a = string.Empty;
		if (context.Request["type"] != null && !string.IsNullOrEmpty(context.Request["type"]))
		{
			a = context.Request["type"];
		}
		if (context.Request["treasuryCode"] != null && !string.IsNullOrEmpty(context.Request["treasuryCode"]))
		{
			treasuryCode = context.Request["treasuryCode"];
		}
		if (context.Request["stocktakeDate"] != null && !string.IsNullOrEmpty(context.Request["stocktakeDate"]))
		{
			stocktakeDate = context.Request["stocktakeDate"];
		}
		if (a == "equal")
		{
			int countByStDate = this.stocktakeBll.GetCountByStDate(treasuryCode, stocktakeDate);
			if (countByStDate > 0)
			{
				num = 1;
			}
		}
		else
		{
			int countByMoreStDate = this.stocktakeBll.GetCountByMoreStDate(treasuryCode, stocktakeDate);
			if (countByMoreStDate > 0)
			{
				num = 1;
			}
		}
		context.Response.Write(num);
	}
}
