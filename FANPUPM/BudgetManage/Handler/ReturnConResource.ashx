<%@ WebHandler Language="C#" Class="ReturnConResource" %>

using cn.justwin.Domain;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
public class ReturnConResource : IHttpHandler
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
		string text = context.Request["taskId"];
		string text2 = context.Request["type"];
		string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
		string OutSourceCBSCode = ConfigHelper.Get("OutSourceCBSCode");
		string ElseCBSCode = ConfigHelper.Get("ElseCBSCode");
		StringBuilder stringBuilder = new StringBuilder();
		if (!string.IsNullOrEmpty(text))
		{
			if (text2.ToLower() == "resources")
			{
				string resourcesInfoByTaskId = BudContractResource.GetResourcesInfoByTaskId(text);
				if (resourcesInfoByTaskId != "")
				{
					string[] array = new string[0];
					if (resourcesInfoByTaskId.Contains("⊙"))
					{
						array = resourcesInfoByTaskId.Split(new char[]
						{
							'⊙'
						});
					}
					stringBuilder.Append("<table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'><caption style=' height:20px; font-weight:bold; text-align:center; padding-top:8px;'>资源信息</caption><tr class='header'><td align='center'>资源编号</td><td align='center'>资源名称</td><td align='center'>单位</td><td align='center'>规格</td><td align='center'>品牌</td><td align='center'>型号</td><td align='center'>技术参数</td><td align='center'>单价</td><td align='center'>数量</td><td align='center'>小计</td></tr>");
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text3 = array2[i];
						if (text3 != "")
						{
							stringBuilder.Append("<tr class='rowb'>");
							string[] array3 = text3.Split(new char[]
							{
								','
							});
							string[] array4 = array3;
							for (int j = 0; j < array4.Length; j++)
							{
								string text4 = array4[j];
								stringBuilder.Append("<td align='left'>");
								if (text4 != " ")
								{
									stringBuilder.Append(text4);
								}
								else
								{
									stringBuilder.Append(" ");
								}
								stringBuilder.Append("</td>");
							}
							stringBuilder.Append("</tr>");
						}
					}
					stringBuilder.Append("</table>");
				}
				else
				{
					stringBuilder.Append("<h1>该任务节点下没有配置资源</h1>");
				}
			}
			else
			{
				if (text2.ToLower() == "resourcetotal")
				{
					List<CostAccounting> byD = CostAccounting.GetByD();
					byD.RemoveAll((CostAccounting c) => c.Code == OutSourceCBSCode);
					byD.RemoveAll((CostAccounting c) => c.Code == ElseCBSCode);
					int num = 100 / (byD.Count + 2);
					string prjId = context.Request["prjId"];
					Hashtable allSum = BudContractTask.GetAllSum(prjId, byD);
					Hashtable hashtable = new Hashtable();
					if (Convert.ToDecimal(allSum["Total"]) > 0m)
					{
						hashtable = BudContractTask.GetCurrentSum(text, byD);
					}
					else
					{
						hashtable.Add("Total", 0m);
						foreach (CostAccounting current in byD)
						{
							hashtable.Add(current.Code, 0m);
						}
					}
					stringBuilder.Append("<table class='gvdata' border='1px' style='width:100%; border-collapse:collapse;border-right:0px; margin-bottom:5px;'>");
					stringBuilder.Append(" <tr class='header'>");
					stringBuilder.Append("<td align='center' style='width:" + num + "%;'></td>");
					stringBuilder.Append(" <td align='center' style='width:" + num + "%;'>总成本</td>");
					foreach (CostAccounting current2 in byD)
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"<td align='center' style='width:",
							num,
							"%;'>",
							current2.Name,
							"</td>"
						}));
					}
					stringBuilder.Append("</tr>");
					for (int k = 0; k < 2; k++)
					{
						stringBuilder.Append("<tr class='rowb'>");
						for (int l = 0; l < byD.Count + 2; l++)
						{
							if (k == 0 && l == 0)
							{
								stringBuilder.Append("<td align='right'>");
								stringBuilder.Append("当前投标项");
								stringBuilder.Append("</td>");
							}
							else
							{
								if (k == 1 && l == 0)
								{
									stringBuilder.Append("<td align='right'>");
									stringBuilder.Append("所有投标项");
									stringBuilder.Append("</td>");
								}
								else
								{
									if (k == 0 && l == 1)
									{
										stringBuilder.Append("<td align='right'>");
										stringBuilder.Append(Convert.ToDecimal(hashtable["Total"]).ToString("0.000"));
									}
									else
									{
										if (k == 1 && l == 1)
										{
											stringBuilder.Append("<td align='right'>");
											stringBuilder.Append(Convert.ToDecimal(allSum["Total"]).ToString("0.000"));
											stringBuilder.Append("</td>");
										}
										else
										{
											if (k == 0)
											{
												stringBuilder.Append("<td align='right'>");
												stringBuilder.Append(Convert.ToDecimal(hashtable[byD[l - 2].Code]).ToString("0.000"));
												stringBuilder.Append("</td>");
											}
											else
											{
												stringBuilder.Append("<td align='right'>");
												stringBuilder.Append(Convert.ToDecimal(allSum[byD[l - 2].Code]).ToString("0.000"));
												stringBuilder.Append("</td>");
											}
										}
									}
								}
							}
						}
						stringBuilder.Append("</tr>");
					}
					stringBuilder.Append("</table>");
				}
			}
		}
		else
		{
			if (text2.ToLower() == "budtask")
			{
				List<CostAccounting> byD2 = CostAccounting.GetByD();
				byD2.RemoveAll((CostAccounting c) => c.Code == OutSourceCBSCode);
				byD2.RemoveAll((CostAccounting c) => c.Code == ElseCBSCode);
				float num2;
				if (byD2.Count != 0)
				{
					num2 = (float)(100 / byD2.Count);
				}
				else
				{
					num2 = 33f;
				}
				string prjId2 = context.Request["prjId"];
				int version = 1;
				try
				{
					version = Convert.ToInt32(context.Request["version"]);
				}
				catch
				{
				}
				string priceType = string.Empty;
				if (!string.IsNullOrEmpty(context.Request["priceType"]))
				{
					priceType = context.Request["priceType"];
				}
				Hashtable sumTotal = BudTask.GetSumTotal(prjId2, version, priceType, byD2, isWBSRelevance);
				stringBuilder.Append("<table class='gvdata' border='1px' style='width:100%; border-collapse:collapse;border-right:0px; margin-bottom:5px;'>");
				stringBuilder.Append(" <tr class='header'>");
				foreach (CostAccounting current3 in byD2)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"<td align='center' style='width:",
						num2,
						"%;'>",
						current3.Name,
						"</td>"
					}));
				}
				stringBuilder.Append("</tr>");
				stringBuilder.Append("<tr class='rowb'>");
				for (int m = 0; m < byD2.Count; m++)
				{
					stringBuilder.Append("<td align='right'>");
					stringBuilder.Append(Convert.ToDecimal(sumTotal[byD2[m].Code]).ToString("0.000"));
					stringBuilder.Append("</td>");
				}
				stringBuilder.Append("</tr>");
				stringBuilder.Append("</table>");
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
