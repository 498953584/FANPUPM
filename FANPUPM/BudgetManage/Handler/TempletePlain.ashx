<%@ WebHandler Language="C#" Class="TempletePlain" %>

using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
public class TempletePlain : IHttpHandler
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
		string text2 = context.Request["type"];
		if (text2.ToLower() == "add")
		{
			string code = context.Request["code"].ToString();
			string name = context.Request["name"].ToString();
			string unit = context.Request["unit"].ToString();
			string value = context.Request["quantity"].ToString();
			string note = context.Request["note"].ToString();
			string parentId = context.Request["pid"].ToString();
			string text3 = Guid.NewGuid().ToString();
			string description = context.Request["description"].ToString();
			BudTemplate byId = BudTemplate.GetById(text);
			BudTemplateItem budTemplateItem = BudTemplateItem.Create(text3, parentId, byId, code, name, unit, Convert.ToDecimal(value), new decimal?(0m), note, description);
			BudTemplateItem.Add(budTemplateItem);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append("\"Id\":\"" + text3 + "\",");
			stringBuilder.Append("\"OrderNumber\":\"" + budTemplateItem.OrderNumber + "\"");
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
			string note2 = HttpUtility.UrlDecode(context.Request["note"]);
			string featureDescription = context.Request["description"].ToString();
			BudTemplateItem byId2 = BudTemplateItem.GetById(id, text);
			if (byId2 != null)
			{
				byId2.Code = code2;
				byId2.Name = name2;
				byId2.Unit = unit2;
				byId2.Quantity = Convert.ToDecimal(value2);
				byId2.FeatureDescription = featureDescription;
				byId2.Note = note2;
				byId2.FeatureDescription = featureDescription;
				BudTemplateItem.Update(byId2);
				s = "1";
			}
			context.Response.Write(s);
			return;
		}
		if (text2.ToLower() == "getdata")
		{
			string id2 = context.Request["id"];
			BudTemplateItem byId3 = BudTemplateItem.GetById(id2, text);
			if (byId3 != null)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("{\"Code\":\"" + byId3.Code + "\"");
				stringBuilder2.Append(",\"Name\":\"" + byId3.Name + "\"");
				stringBuilder2.Append(",\"Note\":\"" + byId3.Note.Replace('\n', ' ') + "\"");
				stringBuilder2.Append(",\"Unit\":\"" + byId3.Unit + "\"");
				stringBuilder2.Append(",\"Quantity\":\"" + byId3.Quantity + "\"");
				stringBuilder2.Append(",\"typeId\":\"" + byId3.OrderNumber.Length / 3 + "\"}");
				context.Response.Write(stringBuilder2.ToString());
				return;
			}
		}
		else
		{
			if (text2.ToLower() == "checkreplace")
			{
				string code3 = context.Request["code"].ToString();
				IList<BudTemplateItem> byCode = BudTemplateItem.GetByCode(code3, text);
				if (byCode.Count > 0)
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
					int itemsCount = BudTemplateItem.GetItemsCount(text);
					context.Response.Write(itemsCount);
				}
			}
		}
	}
}
