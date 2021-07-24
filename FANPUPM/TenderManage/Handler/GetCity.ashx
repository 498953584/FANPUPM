<%@ WebHandler Language="C#" Class="GetCity" %>

using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web;
public class GetCity : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["province"]))
		{
			string provinceId = context.Request["province"].ToString();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("请选择|");
			DataTable dtbByProviceId = new BasicCityService().GetDtbByProviceId(provinceId);
			for (int i = 0; i < dtbByProviceId.Rows.Count; i++)
			{
				DataRow dataRow = dtbByProviceId.Rows[i];
				stringBuilder.Append(dataRow["Name"].ToString() + "|");
			}
			string s = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
			context.Response.Write(s);
		}
	}
}
