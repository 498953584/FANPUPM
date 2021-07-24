<%@ WebHandler Language="C#" Class="AddConsResource" %>

using cn.justwin.BLL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Web;
public class AddConsResource : IHttpHandler
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
		string value = string.Empty;
		string id = string.Empty;
		string cBScode = string.Empty;
		string value2 = string.Empty;
		if (context.Request["type"] != null && !string.IsNullOrEmpty(context.Request["type"]))
		{
			value = context.Request["type"];
		}
		if (context.Request["Id"] != null && !string.IsNullOrEmpty(context.Request["Id"]))
		{
			id = context.Request["Id"];
		}
		if (context.Request["cbscode"] != null && !string.IsNullOrEmpty(context.Request["cbscode"]))
		{
			cBScode = context.Request["cbscode"];
		}
		if (context.Request["AccountingQuantity"] != null && !string.IsNullOrEmpty(context.Request["AccountingQuantity"]))
		{
			value2 = context.Request["AccountingQuantity"];
		}
		if (!string.IsNullOrEmpty(value))
		{
			List<ConstructResource> list = new List<ConstructResource>();
			ConstructResource constructResource = ConstructResource.Create(id, " ", " ", 0m, 0m);
			constructResource.CBScode = cBScode;
			constructResource.AccountingQuantity = new decimal?(Convert.ToDecimal(value2));
			list.Add(constructResource);
			ConstructResource.Update(list);
			return;
		}
		if (!string.IsNullOrEmpty(context.Request["ResourceIds"]) && !string.IsNullOrEmpty(context.Request["consTaskId"]))
		{
			string json = context.Request["ResourceIds"];
			string text = context.Request["ConsTaskId"];
			ConstructTask byId = ConstructTask.GetById(text);
			IList<string> listFromJson = JsonHelper.GetListFromJson(json);
			for (int i = 0; i < byId.ResourceList.Count; i++)
			{
				byId.ResourceList[i].Id = Guid.NewGuid().ToString();
			}
			foreach (string current in listFromJson)
			{
				ConstructResource item = ConstructResource.Create(Guid.NewGuid().ToString(), text, current, 0m, 0m);
				if (!byId.ResourceList.Contains(item))
				{
					byId.ResourceList.Add(item);
				}
				else
				{
					byId.ResourceList.Remove(item);
					byId.ResourceList.Add(item);
				}
			}
			ConstructTask.AddResource(byId);
			context.Response.Write("1");
			return;
		}
		context.Response.Write("0");
	}
}
