<%@ WebHandler Language="C#" Class="BidSuccess" %>

using cn.justwin.opm.CallBids.Bid;
using System;
using System.Web;
using System.Web.SessionState;
public class BidSuccess : IHttpHandler, IRequiresSessionState
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
		BidCorpAction bidCorpAction = new BidCorpAction();
		string text = context.Request["uid"];
		string text2 = context.Request["bidid"];
		if (!this.IsGuid(text) || !this.IsGuid(text2) || bidCorpAction.CallBidSuccess(text, text2, context.Request["reason"]) <= 0)
		{
			context.Response.Write("false");
			context.Response.End();
			return;
		}
		context.Response.Write("true");
		context.Response.End();
	}
	private bool IsGuid(string strSrc)
	{
		if (string.IsNullOrEmpty(strSrc) || strSrc.Length != 36)
		{
			return false;
		}
		string[] array = strSrc.Split(new char[]
		{
			'-'
		});
		if (array.Length != 5)
		{
			return false;
		}
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < array[i].Length; j++)
			{
				char c = array[i][j];
				if ((c < '0' || c > '9') && (c < 'A' || c > 'Z') && (c < 'a' || c > 'z'))
				{
					return false;
				}
			}
		}
		return true;
	}
}
