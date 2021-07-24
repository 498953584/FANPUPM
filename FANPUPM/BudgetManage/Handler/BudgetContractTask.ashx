<%@ WebHandler Language="C#" Class="BudgetContractTask" %>

using cn.justwin.Domain;
using System;
using System.Text;
using System.Web;
public class BudgetContractTask : IHttpHandler
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
		string text = string.Empty;
		string text2 = string.Empty;
		string code = string.Empty;
		string name = string.Empty;
		string unit = string.Empty;
		string note = string.Empty;
		string inputUser = string.Empty;
		decimal quantity = 0m;
		DateTime? start = null;
		DateTime? end = null;
		decimal value = 0m;
		decimal value2 = 0m;
		string id = string.Empty;
		string text3 = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["prjId"]))
		{
			text = context.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(context.Request["type"]))
		{
			text2 = context.Request["type"];
		}
		if (!string.IsNullOrEmpty(context.Request["code"]))
		{
			code = context.Request["code"];
		}
		if (!string.IsNullOrEmpty(context.Request["name"]))
		{
			name = context.Request["name"];
		}
		if (!string.IsNullOrEmpty(context.Request["unit"]))
		{
			unit = context.Request["unit"];
		}
		if (!string.IsNullOrEmpty(context.Request["note"]))
		{
			note = context.Request["note"];
		}
		if (!string.IsNullOrEmpty(context.Request["user"]))
		{
			inputUser = context.Request["user"];
		}
		if (!string.IsNullOrEmpty(context.Request["quantity"]))
		{
			quantity = Convert.ToDecimal(context.Request["quantity"]);
		}
		if (!string.IsNullOrEmpty(context.Request["start"]))
		{
			start = new DateTime?(Convert.ToDateTime(context.Request["start"]));
		}
		if (!string.IsNullOrEmpty(context.Request["consPeriod"]))
		{
			new int?(Convert.ToInt32(context.Request["consPeriod"]));
		}
		if (!string.IsNullOrEmpty(context.Request["end"]))
		{
			end = new DateTime?(Convert.ToDateTime(context.Request["end"]));
		}
		if (!string.IsNullOrEmpty(context.Request["unitPrice"]))
		{
			value = Convert.ToDecimal(context.Request["unitPrice"]);
		}
		if (!string.IsNullOrEmpty(context.Request["total"]))
		{
			value2 = Convert.ToDecimal(context.Request["total"]);
		}
		if (!string.IsNullOrEmpty(context.Request["id"]))
		{
			id = context.Request["id"];
		}
		if (!string.IsNullOrEmpty(context.Request["pid"]))
		{
			text3 = context.Request["pid"];
		}
		if (text2.ToLower() == "add")
		{
			string text4 = Guid.NewGuid().ToString();
			string orderNumber = BudContractTask.GetOrderNumber(text, text3);
			BudContractTask budContractTask;
			if (!string.IsNullOrEmpty(text3))
			{
				budContractTask = BudContractTask.Create(text4, code, name, unit, new decimal?(value), quantity, new decimal?(value2), note, text3, text, orderNumber, start, end, DateTime.Now, inputUser);
			}
			else
			{
				budContractTask = BudContractTask.Create(text4, code, name, unit, new decimal?(value), quantity, new decimal?(value2), note, null, text, orderNumber, start, end, DateTime.Now, inputUser);
			}
			BudContractTask.Add(budContractTask);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append("\"Id\":\"" + text4 + "\",");
			stringBuilder.Append("\"OrderNumber\":\"" + budContractTask.OrderNumber + "\"");
			stringBuilder.Append("}");
			context.Response.Write(stringBuilder.ToString());
			return;
		}
		if (text2.ToLower() == "edit")
		{
			string s = "1";
			context.Response.Write(s);
			return;
		}
		if (text2.ToLower() == "getdata")
		{
			BudContractTask byId = BudContractTask.GetById(id);
			if (byId != null)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("{\"Code\":\"" + byId.Code + "\"");
				stringBuilder2.Append(",\"Name\":\"" + byId.Name + "\"");
				stringBuilder2.Append(",\"Note\":\"" + byId.Note.Replace('\n', ' ') + "\"");
				if (byId.StartDate.HasValue)
				{
					stringBuilder2.Append(",\"StartDate\":\"" + byId.StartDate.Value.ToString("yyyy-M-d") + "\"");
				}
				else
				{
					stringBuilder2.Append(",\"StartDate\":\"\"");
				}
				if (byId.EndDate.HasValue)
				{
					stringBuilder2.Append(",\"EndDate\":\"" + byId.EndDate.Value.ToString("yyyy-M-d") + "\"");
				}
				else
				{
					stringBuilder2.Append(",\"EndDate\":\"\"");
				}
				stringBuilder2.Append(",\"Unit\":\"" + byId.Unit + "\"");
				stringBuilder2.Append(",\"Quantity\":\"" + byId.Quantity + "\"");
				stringBuilder2.Append(",\"typeId\":\"" + byId.OrderNumber.Length / 3 + "\"}");
				context.Response.Write(stringBuilder2.ToString());
				return;
			}
		}
		else
		{
			if (text2.ToLower() == "checkreplace")
			{
				bool flag = BudContractTask.CheckCode(code, text);
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
					int taskCount = BudContractTask.GetTaskCount(text);
					context.Response.Write(taskCount);
				}
			}
		}
	}
}
