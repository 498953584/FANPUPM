<%@ WebHandler Language="C#" Class="Handler" %>

using cn.justwin.epm.bll.datum;
using System;
using System.Web;
public class Handler : IHttpHandler
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
		if (context.Request.Form["TYPE_ID"] != null && context.Request.Form["TYPE_ID"].ToString() != "")
		{
			string s = this.checkTypeIDState(int.Parse(context.Request.Form["TYPE_ID"].ToString()));
			context.Response.ContentType = "text/plain";
			context.Response.Write(s);
		}
	}
	private string checkTypeIDState(int tid)
	{
		datumClass datumClass = new datumClass();
		string result = string.Empty;
		if (tid > 0)
		{
			if (datumClass.Exists(tid))
			{
				result = "false|改类型下已存在子类型";
			}
			else
			{
				int typePID = datumClass.GetTypePID("TypeId=" + tid);
				if (typePID == 1)
				{
					result = "false|该类型不可删除";
				}
				else
				{
					result = "true|....";
				}
			}
		}
		return result;
	}
}
