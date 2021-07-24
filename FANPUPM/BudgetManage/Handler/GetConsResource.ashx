<%@ WebHandler Language="C#" Class="GetConsResource" %>

using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
public class GetConsResource : IHttpHandler
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
		string consTaskId = string.Empty;
		string value = string.Empty;
		string taskId = string.Empty;
		if (context.Request["type"] != null && !string.IsNullOrEmpty(context.Request["type"]))
		{
			value = context.Request["type"];
		}
		if (context.Request["taskId"] != null && !string.IsNullOrEmpty(context.Request["taskId"]))
		{
			taskId = context.Request["taskId"];
		}
		if (string.IsNullOrEmpty(context.Request["ConsTaskId"]))
		{
			context.Response.Write("0");
			return;
		}
		consTaskId = context.Request["ConsTaskId"];
		if (string.IsNullOrEmpty(value))
		{
			IList<ConstructResource> listByConsTaskId = ConstructResource.GetListByConsTaskId(consTaskId);
			StringBuilder stringBuilder = new StringBuilder();
			if (listByConsTaskId.Count > 0)
			{
				stringBuilder.Append("<table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'>").Append("<caption style=' height:20px; font-weight:bold; text-align:center; padding-top:5px;'>").Append("资源信息").Append("</caption>").Append("<tr class='header'><td align='center'>资源名称</td><td align='center'>规格</td><td align='center'>品牌</td>\r\n                                      <td align='center'>型号</td><td align='center'>技术参数</td><td align='center'>单价</td><td align='center'>数量</td>\r\n                                      <td align='center'>小计</td></tr>");
				foreach (ConstructResource current in listByConsTaskId)
				{
					stringBuilder.Append("<tr>");
					stringBuilder.AppendFormat("<td align='left'>{0}</td>", current.Resource.Name);
					stringBuilder.AppendFormat("<td align='left'>{0}</td>", current.Resource.Specification);
					stringBuilder.AppendFormat("<td align='left'>{0}</td>", current.Resource.Brand);
					stringBuilder.AppendFormat("<td align='left'>{0}</td>", current.Resource.ModelNumber);
					stringBuilder.AppendFormat("<td align='left'>{0}</td>", current.Resource.TechnicalParameter);
					stringBuilder.AppendFormat("<td>{0}</td>", current.UnitPrice);
					stringBuilder.AppendFormat("<td>{0}</td>", current.Quantity);
					stringBuilder.AppendFormat("<td>{0}</td>", (current.Quantity * current.UnitPrice).ToString("0.000"));
					stringBuilder.Append("</tr>");
				}
				stringBuilder.Append("</table>");
			}
			else
			{
				stringBuilder.Append("<h1>该节点下没有配置资源!</h1>");
			}
			context.Response.Write(stringBuilder.ToString());
			return;
		}
		List<ConstructResource> byConsTask = ConstructResource.GetByConsTask(consTaskId, taskId);
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder2.Append("<table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'>").Append("<caption style=' height:20px; font-weight:bold; text-align:center; padding-top:5px;'>").Append("资源信息").Append("</caption>").Append("<tr class='header'><td align='center'>资源编号</td><td align='center'>资源名称</td><td align='center'>规格</td>\r\n                                <td align='center'>品牌</td><td align='center'>型号</td><td align='center'>技术参数</td><td align='center'>单价</td>\r\n                                <td align='center'>预算数量</td><td align='center'>上报数量</td><td align='center' style='display:none;'>审核数量</td>\r\n                                <td align='center'>预算金额</td><td align='center'>上报金额</td><td align='center' style='display:none;'>审核金额</td>\r\n                                <td align='center'>成本分类</td></tr>");
		foreach (ConstructResource current2 in byConsTask)
		{
			stringBuilder2.Append("<tr>");
			stringBuilder2.AppendFormat("<td>{0}</td>", current2.Resource.Code);
			stringBuilder2.AppendFormat("<td align='left'>{0}</td>", current2.Resource.Name);
			stringBuilder2.AppendFormat("<td align='left'>{0}</td>", current2.Resource.Specification);
			stringBuilder2.AppendFormat("<td align='left'>{0}</td>", current2.Resource.Brand);
			stringBuilder2.AppendFormat("<td align='left'>{0}</td>", current2.Resource.ModelNumber);
			stringBuilder2.AppendFormat("<td align='left'>{0}</td>", current2.Resource.TechnicalParameter);
			stringBuilder2.AppendFormat("<td>{0}</td>", current2.UnitPrice);
			stringBuilder2.AppendFormat("<td>{0}</td>", current2.BudQuantity);
			stringBuilder2.AppendFormat("<td>{0}</td>", current2.Quantity);
			stringBuilder2.AppendFormat("<td style='display:none;'>{0}</td>", current2.AccountingQuantity);
			stringBuilder2.AppendFormat("<td>{0}</td>", (current2.BudQuantity * current2.UnitPrice).ToString("0.000"));
			stringBuilder2.AppendFormat("<td>{0}</td>", (current2.Quantity * current2.UnitPrice).ToString("0.000"));
			stringBuilder2.AppendFormat("<td style='display:none;'>{0}</td>", (Convert.ToDecimal(current2.AccountingQuantity) * current2.UnitPrice).ToString("0.000"));
			stringBuilder2.AppendFormat("<td>{0}</td>", current2.CbsName);
			stringBuilder2.Append("</tr>");
		}
		stringBuilder2.Append("</table>");
		context.Response.Write(stringBuilder2.ToString());
	}
}
