<%@ WebHandler Language="C#" Class="GetChildTemplateItem" %>

using cn.justwin.Domain;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
public class GetChildTemplateItem : IHttpHandler
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
		context.Response.ContentType = "Application/json";
		if (!string.IsNullOrEmpty(context.Request["TaskId"]))
		{
			string itemId = context.Request["TaskId"];
			DataTable childrenTemplateItem = BudTemplate.GetChildrenTemplateItem(itemId);
			childrenTemplateItem.Columns["TemplateItemId"].ColumnName = "TaskId";
			childrenTemplateItem.Columns["ItemCode"].ColumnName = "TaskCode";
			childrenTemplateItem.Columns["ItemName"].ColumnName = "TaskName";
			if (childrenTemplateItem.Rows.Count > 0)
			{
				context.Response.Write(JsonConvert.SerializeObject(childrenTemplateItem));
			}
		}
	}
}
