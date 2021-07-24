<%@ WebHandler Language="C#" Class="ReturnResource" %>

using cn.justwin.contractBLL;
using cn.justwin.Domain;
using cn.justwin.Web;
using System;
using System.Data;
using System.Text;
using System.Web;
public class ReturnResource : IHttpHandler
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
		string text = string.Empty;
		string text2 = string.Empty;
		string a = string.Empty;
		string a2 = ConfigHelper.Get("IsWBSRelevance");
		if (context.Request["taskId"] != null && !string.IsNullOrEmpty(context.Request["taskId"]))
		{
			text = context.Request["taskId"];
		}
		if (context.Request["type"] != null && !string.IsNullOrEmpty(context.Request["type"]))
		{
			a = context.Request["type"];
		}
		if (context.Request["priceType"] != null && !string.IsNullOrEmpty(context.Request["priceType"]))
		{
			text2 = context.Request["priceType"];
		}
		string text3 = "";
		StringBuilder stringBuilder = new StringBuilder();
		if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2))
		{
			if (a2 == "1")
			{
				if (text2 == "")
				{
					text3 = BudTask.GetResourcesInfoByTaskId(text);
				}
				if (text3 != "")
				{
					string[] array = new string[0];
					if (text3.Contains("⊙"))
					{
						array = text3.Split(new char[]
						{
							'⊙'
						});
					}
					stringBuilder.Append("<table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'><caption style=' height:20px; font-weight:bold; text-align:center; padding-top:8px;'>资源信息</caption><tr class='header'><td align='center'>资源编号</td><td align='center'>资源名称</td><td align='center'>单位</td><td align='center'>规格</td><td align='center'>品牌</td><td align='center'>型号</td><td align='center'>技术参数</td><td align='center'>单价</td><td align='center'>数量</td><td align='center'>损耗系数</td><td align='center'>合价</td></tr>");
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text4 = array2[i];
						if (text4 != "")
						{
							stringBuilder.Append("<tr class='rowb'>");
							string[] array3 = text4.Split(new char[]
							{
								','
							});
							int num = 0;
							string[] array4 = array3;
							for (int j = 0; j < array4.Length; j++)
							{
								string text5 = array4[j];
								stringBuilder.Append("<td align='left'>");
								if (text5 != " ")
								{
									if (num != 9)
									{
										stringBuilder.Append(text5);
									}
									else
									{
										stringBuilder.Append(Convert.ToDecimal(text5.ToString()).ToString("0.000"));
									}
								}
								else
								{
									stringBuilder.Append(" ");
								}
								stringBuilder.Append("</td>");
								num++;
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
			if (a != "deploy")
			{
				PayoutTarget payoutTarget = new PayoutTarget();
				DataTable contractInfoByTaskId = payoutTarget.GetContractInfoByTaskId(text);
				if (contractInfoByTaskId.Rows.Count != 0)
				{
					stringBuilder.Append("<table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'><caption style=' height:20px; font-weight:bold; text-align:center; padding-top:8px;'>合同信息</caption><tr class='header'><td align='center'>合同编码</td><td align='center'>合同名称</td><td align='center'>合同金额</td><td align='center'>变更后金额</td><td align='center'>结算累计</td><td align='center'>付款累计</td></tr>");
					foreach (DataRow dataRow in contractInfoByTaskId.Rows)
					{
						stringBuilder.Append("<tr class='rowb'>");
						for (int k = 0; k < contractInfoByTaskId.Columns.Count; k++)
						{
							stringBuilder.Append("<td align='center'>");
							stringBuilder.Append(dataRow[k].ToString());
							stringBuilder.Append("</td>");
						}
					}
					stringBuilder.Append("</tr>");
					stringBuilder.Append("</table>");
				}
				else
				{
					stringBuilder.Append("<h1>该任务节点下没有与之相关联的合同信息</h1>");
				}
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
