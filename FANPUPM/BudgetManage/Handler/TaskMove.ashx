<%@ WebHandler Language="C#" Class="TaskMove" %>

using cn.justwin.Domain;
using System;
using System.Data;
using System.Text;
using System.Web;
public class TaskMove : IHttpHandler
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
		context.Response.ContentType = "application/json";
		if (!string.IsNullOrEmpty(context.Request["comId"]))
		{
			string a = context.Request["type"];
			string text = context.Request["comId"];
			string text2 = context.Request["selId"];
			string replaceOrderNumber = context.Request["comOrderNumber"];
			string replaceOrderNumber2 = context.Request["selOrderNumber"];
			StringBuilder stringBuilder = new StringBuilder();
			DataTable dataTable = new DataTable();
			if (a == "budget")
			{
				BudTask.UpdateChangeTask(text, replaceOrderNumber2);
				BudTask.UpdateChangeTask(text2, replaceOrderNumber);
				string prjId = context.Request["prjId"];
				dataTable = BudTask.GetChangeTaskNo(prjId, text, text2);
			}
			else
			{
				if (a == "templete")
				{
					BudTemplateItem.UpdateChangeTask(text, replaceOrderNumber2);
					BudTemplateItem.UpdateChangeTask(text2, replaceOrderNumber);
					string templateId = context.Request["tempType"];
					dataTable = BudTemplateItem.GetChangeTaskNo(templateId, text, text2);
				}
			}
			if (dataTable != null && dataTable.Rows.Count == 2)
			{
				stringBuilder.Append("{\"comId\":\"" + dataTable.Rows[0][0].ToString() + "\"");
				stringBuilder.Append(",\"selId\":\"" + dataTable.Rows[1][0].ToString() + "\"");
				stringBuilder.Append("}");
				context.Response.Write(stringBuilder.ToString());
				return;
			}
			context.Response.Write("{\"comId\":\"0000\"}");
		}
	}
}
