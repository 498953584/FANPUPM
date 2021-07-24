<%@ WebHandler Language="C#" Class="ReturnConsRep" %>

using cn.justwin.Domain;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
public class ReturnConsRep : IHttpHandler
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
		string a = ConfigHelper.Get("IsWBSRelevance");
		string text = string.Empty;
		string arg_26_0 = string.Empty;
		string value = string.Empty;
		if (context.Request["consId"] != null && !string.IsNullOrEmpty(context.Request["consId"]))
		{
			text = context.Request["consId"];
		}
		if (context.Request["consTaskId"] != null && !string.IsNullOrEmpty(context.Request["consTaskId"]))
		{
			value = context.Request["consTaskId"];
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (string.IsNullOrEmpty(value))
		{
			if (!string.IsNullOrEmpty(text))
			{
				List<string> taskIdsByReportId = ConstructTask.GetTaskIdsByReportId(text);
				string text2 = "";
				foreach (string current in taskIdsByReportId)
				{
					if (current != "")
					{
						text2 += "'";
						text2 += current;
						text2 += "',";
					}
				}
				if (text2 != "")
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				else
				{
					text2 = "''";
				}
				DataTable allByTaskIds = ConstructTask.GetAllByTaskIds(text2, text, false, false);
				if (allByTaskIds.Rows.Count > 0)
				{
					stringBuilder.Append("<table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'>\r\n                        <caption style=' height:20px; font-weight:bold; text-align:center; padding-top:8px;'>工程量</caption>\r\n                        <tr class='header'><td align='center'>序号</td><td align='center'>任务编码</td><td align='center'>分项工程</td><td align='center'>总工程量</td>\r\n                        <td align='center'>单位</td><td align='center'>累计审核量</td><td align='center'>剩余工程量</td><td align='center'>本期报量</td>\r\n                        <td align='center' style='display:none;'>审核量</td></tr>");
					int num = 1;
					foreach (DataRow dataRow in allByTaskIds.Rows)
					{
						stringBuilder.Append("<tr class='rowb'>");
						for (int i = 0; i < allByTaskIds.Columns.Count; i++)
						{
							if (i == 0)
							{
								stringBuilder.Append("<td align='center'>");
								stringBuilder.Append(num.ToString());
								stringBuilder.Append("</td>");
							}
							else
							{
								if (i != 3 && i != 4)
								{
									if (i != 10)
									{
										stringBuilder.Append("<td align='center'>");
										stringBuilder.Append(dataRow[i].ToString());
										stringBuilder.Append("</td>");
									}
									else
									{
										stringBuilder.Append("<td align='center' style=' display:none;'>");
										stringBuilder.Append(dataRow[i].ToString());
										stringBuilder.Append("</td>");
									}
								}
							}
						}
						num++;
					}
					stringBuilder.Append("</tr>");
					stringBuilder.Append("</table>");
				}
				else
				{
					stringBuilder.Append("<h1>该报量没有添加任务节点</h1>");
				}
				if (a == "1")
				{
					DataTable infoByConsReportIds = ConstructResource.GetInfoByConsReportIds(text);
					if (infoByConsReportIds.Rows.Count > 0)
					{
						stringBuilder.Append("<table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'><caption style=' height:20px; font-weight:bold; text-align:center; padding-top:8px;'>资源汇总</caption><tr class='header'><td align='center'>序号</td><td align='center'>资源名称</td><td align='center'>单位</td><td align='center'>规格</td><td align='center'>品牌</td><td align='center'>型号</td><td align='center'>技术参数</td><td align='center'>单价</td><td align='center'>数量</td><td align='center'>小计</td></tr>");
						int num2 = 1;
						foreach (DataRow dataRow2 in infoByConsReportIds.Rows)
						{
							stringBuilder.Append("<tr class='rowb'>");
							for (int j = 0; j < infoByConsReportIds.Columns.Count; j++)
							{
								if (j == 0)
								{
									stringBuilder.Append("<td align='center'>");
									stringBuilder.Append(num2.ToString());
									stringBuilder.Append("</td>");
								}
								else
								{
									if (j == 1 || j == 3 || j == 4 || j == 5 || j == 6)
									{
										stringBuilder.Append("<td align='left'>");
									}
									else
									{
										stringBuilder.Append("<td align='center'>");
									}
									stringBuilder.Append(dataRow2[j].ToString());
									stringBuilder.Append("</td>");
								}
							}
							num2++;
						}
						stringBuilder.Append("</tr>");
						stringBuilder.Append("</table>");
					}
					else
					{
						stringBuilder.Append("<h1>该报量没有使用资源</h1>");
					}
				}
			}
		}
		else
		{
			List<string> resourceIds = ConstructResource.GetResourceIds(text);
			List<ConstructResource> all = ConstructResource.GetAll(text, resourceIds);
			if (all.Count > 0)
			{
				stringBuilder.Append("<table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'><caption style=' height:20px; font-weight:bold; text-align:center; padding-top:8px;'>资源汇总</caption><tr class='header'><td align='center'>序号</td><td align='center'>资源编号</td><td align='center'>资源名称</td><td align='center'>单位</td><td align='center'>规格</td><td align='center'>品牌</td><td align='center'>型号</td><td align='center'>技术参数</td><td align='center'>数量</td><td align='center'>单价</td><td align='center'>小计</td></tr>");
				int num3 = 1;
				foreach (ConstructResource current2 in all)
				{
					stringBuilder.Append("<tr class='rowb'>");
					stringBuilder.Append("<td align='center'>");
					stringBuilder.Append(num3.ToString());
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='center'>");
					stringBuilder.Append(current2.Resource.Code);
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='left'>");
					stringBuilder.Append(current2.Resource.Name);
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='center'>");
					stringBuilder.Append(current2.UnitName);
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='left'>");
					stringBuilder.Append(current2.Resource.Specification);
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='left'>");
					stringBuilder.Append(current2.Resource.Brand);
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='left'>");
					stringBuilder.Append(current2.Resource.ModelNumber);
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='left'>");
					stringBuilder.Append(current2.Resource.TechnicalParameter);
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='center'>");
					stringBuilder.Append(current2.Quantity.ToString("0.000"));
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='center'>");
					stringBuilder.Append(current2.UnitPrice.ToString("0.000"));
					stringBuilder.Append("</td>");
					stringBuilder.Append("<td align='center'>");
					stringBuilder.Append((current2.Quantity * current2.UnitPrice).ToString());
					stringBuilder.Append("</td>");
					num3++;
				}
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
