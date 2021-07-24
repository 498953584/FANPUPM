<%@ WebHandler Language="C#" Class="GetLeafTaskType" %>

using cn.justwin.Domain;
using System;
using System.Text;
using System.Web;
public class GetLeafTaskType : IHttpHandler
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
		string text = context.Request["prjId"];
		string text2 = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["type"]))
		{
			text2 = context.Request["type"];
		}
		bool isLock = false;
		if (context.Request["budgetChange"] != null && context.Request["budgetChange"].ToLower() == "true")
		{
			isLock = true;
		}
		if (text2.ToLower() == "add")
		{
			string code = context.Request["code"].ToString();
			string name = context.Request["name"].ToString();
			string unit = context.Request["unit"].ToString();
			string value = context.Request["quantity"].ToString();
			DateTime? startDate = null;
			if (!string.IsNullOrEmpty(context.Request["start"]))
			{
				startDate = new DateTime?(Convert.ToDateTime(context.Request["start"].ToString()));
			}
			DateTime? endDate = null;
			if (!string.IsNullOrEmpty(context.Request["end"]))
			{
				endDate = new DateTime?(Convert.ToDateTime(context.Request["end"].ToString()));
			}
			if (!string.IsNullOrEmpty(context.Request["constructionPeriod"]))
			{
				Convert.ToInt32(context.Request["constructionPeriod"]);
			}
			string note = context.Request["note"].ToString();
			string text3 = context.Request["user"].ToString();
			text3 = HttpUtility.UrlDecode(text3);
			string parentTaskId = context.Request["pid"].ToString();
			string text4 = context.Request["bid"].ToString();
			decimal? unitPrice = null;
			if (!string.IsNullOrEmpty(context.Request["unitPrice"]))
			{
				unitPrice = new decimal?(Convert.ToDecimal(context.Request["unitPrice"].ToString()));
			}
			decimal? total = null;
			if (!string.IsNullOrEmpty(context.Request["unitPrice"]))
			{
				total = new decimal?(Convert.ToDecimal(context.Request["total"]));
			}
			BudTask budTask = BudTask.Create(text4, parentTaskId, null, text, code, name, unit, Convert.ToDecimal(value), startDate, endDate, true, note, text3, DateTime.Now, unitPrice, total);
			if (string.IsNullOrEmpty(context.Request["flag"]))
			{
				BudTask.Add(budTask, isLock);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append("\"Id\":\"" + text4 + "\",");
			stringBuilder.Append("\"OrderNumber\":\"" + budTask.OrderNumber + "\"");
			stringBuilder.Append("}");
			context.Response.Write(stringBuilder.ToString());
			return;
		}
		if (text2.ToLower() == "edit")
		{
			string s = "0";
			string id = context.Request["id"];
			string code2 = context.Request["code"].ToString();
			string name2 = context.Request["name"].ToString();
			string unit2 = context.Request["unit"].ToString();
			string value2 = context.Request["quantity"].ToString();
			DateTime? startDate2 = null;
			if (!string.IsNullOrEmpty(context.Request["start"]))
			{
				startDate2 = new DateTime?(Convert.ToDateTime(context.Request["start"].ToString()));
			}
			DateTime? endDate2 = null;
			if (!string.IsNullOrEmpty(context.Request["end"]))
			{
				endDate2 = new DateTime?(Convert.ToDateTime(context.Request["end"].ToString()));
			}
			if (!string.IsNullOrEmpty(context.Request["constructionPeriod"]))
			{
				Convert.ToInt32(context.Request["constructionPeriod"]);
			}
			string note2 = context.Request["note"].ToString();
			decimal? unitPrice2 = null;
			if (!string.IsNullOrEmpty(context.Request["unitPrice"]))
			{
				unitPrice2 = new decimal?(Convert.ToDecimal(context.Request["unitPrice"].ToString()));
			}
			decimal? total2 = null;
			if (!string.IsNullOrEmpty(context.Request["unitPrice"]))
			{
				total2 = new decimal?(Convert.ToDecimal(context.Request["total"]));
			}
			BudTask byId = BudTask.GetById(id);
			if (byId != null)
			{
				if (string.IsNullOrEmpty(context.Request["flag"]))
				{
					byId.Code = code2;
					byId.Name = name2;
					byId.Unit = unit2;
					byId.Quantity = Convert.ToDecimal(value2);
					byId.StartDate = startDate2;
					byId.EndDate = endDate2;
					byId.Note = note2;
					byId.UnitPrice = unitPrice2;
					byId.Total = total2;
					BudTask.Update(byId, isLock);
				}
				s = "1";
			}
			context.Response.Write(s);
			return;
		}
		if (text2.ToLower() == "getdata")
		{
			string id2 = context.Request["id"];
			BudTask byId2 = BudTask.GetById(id2);
			if (byId2 != null)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("{\"Code\":\"" + byId2.Code + "\"");
				stringBuilder2.Append(",\"Name\":\"" + byId2.Name + "\"");
				stringBuilder2.Append(",\"Note\":\"" + byId2.Note.Replace('\n', ' ') + "\"");
				if (byId2.StartDate.HasValue)
				{
					stringBuilder2.Append(",\"StartDate\":\"" + byId2.StartDate.Value.ToString("yyyy-M-d") + "\"");
				}
				else
				{
					stringBuilder2.Append(",\"StartDate\":\"\"");
				}
				if (byId2.EndDate.HasValue)
				{
					stringBuilder2.Append(",\"EndDate\":\"" + byId2.EndDate.Value.ToString("yyyy-M-d") + "\"");
				}
				else
				{
					stringBuilder2.Append(",\"EndDate\":\"\"");
				}
				stringBuilder2.Append(",\"Unit\":\"" + byId2.Unit + "\"");
				stringBuilder2.Append(",\"Quantity\":\"" + byId2.Quantity + "\"");
				stringBuilder2.Append(",\"unitPrice\":\"" + byId2.UnitPrice + "\"");
				stringBuilder2.Append(",\"total\":\"" + byId2.Total + "\"");
				stringBuilder2.Append(",\"typeId\":\"" + byId2.OrderNumber.Length / 3 + "\"}");
				context.Response.Write(stringBuilder2.ToString());
				return;
			}
		}
		else
		{
			if (text2.ToLower() == "checkreplace")
			{
				string code3 = context.Request["code"].ToString();
				bool flag = BudTask.CheckCode(code3, text);
				if (flag)
				{
					context.Response.Write("1");
					return;
				}
				context.Response.Write("0");
				return;
			}
			else
			{
				if (text2.ToLower() == "count")
				{
					int taskCount = BudTask.GetTaskCount(text);
					context.Response.Write(taskCount);
					return;
				}
				context.Response.Write("1");
			}
		}
	}
}
