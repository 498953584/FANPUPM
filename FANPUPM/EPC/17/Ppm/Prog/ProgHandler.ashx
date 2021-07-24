<%@ WebHandler Language="C#" Class="ProgHandler" %>

using cn.justwin.epm.prog;
using System;
using System.Text;
using System.Web;
public class ProgHandler : IHttpHandler
{
	private progLogic bll = new progLogic();
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (context.Request.Form["ProgSortName"] != null)
		{
			string text = HttpUtility.UrlDecode(context.Request.Form["ProgSortName"].ToString());
			if (this.bll.Exists(text))
			{
				stringBuilder.Append("false").Append("|").Append(text);
			}
			else
			{
				stringBuilder.Append("true").Append("|").Append(text);
			}
		}
		context.Response.ContentType = "text/plain";
		context.Response.Write(stringBuilder.ToString());
		context.Response.End();
	}
}
