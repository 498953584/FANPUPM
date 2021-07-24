<%@ WebHandler Language="C#" Class="GetPrjInfoZTB" %>

using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Text;
using System.Web;
public class GetPrjInfoZTB : IHttpHandler
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
		PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
		context.Response.ContentType = "application/json";
		string text = context.Request["prjId"];
		if (!string.IsNullOrEmpty(text))
		{
			Guid id = new Guid(text);
			PTPrjInfoZTB byId = pTPrjInfoZTBService.GetById(id);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.AppendFormat("\"{0}\":\"{1}\",", "GiveUpTime", (!byId.GiveUpTime.HasValue) ? DateTime.Now.ToString("yyyy-MM-dd") : Common2.GetTime(byId.GiveUpTime));
			stringBuilder.AppendFormat("\"{0}\":\"{1}\",", "Operator", byId.Operator);
			stringBuilder.AppendFormat("\"{0}\":\"{1}\",", "OperatorName", WebUtil.GetUserNames(byId.Operator));
			stringBuilder.AppendFormat("\"{0}\":\"{1}\",", "GiveUpReason", byId.GiveUpReason);
			stringBuilder.AppendFormat("\"{0}\":\"{1}\"", "GiveUpNote", byId.GiveUpNote);
			stringBuilder.Append("}");
			context.Response.Write(stringBuilder);
		}
	}
}
