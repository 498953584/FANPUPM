<%@ WebHandler Language="C#" Class="IsLeafNode" %>

using cn.justwin.contractBLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
public class IsLeafNode : IHttpHandler
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
		if (context.Request["Type"] != null && !string.IsNullOrEmpty(context.Request["Type"]))
		{
			text = context.Request["Type"];
		}
		if (context.Request["Id"] != null && !string.IsNullOrEmpty(context.Request["Id"]))
		{
			text2 = context.Request["Id"];
		}
		int num = 0;
		if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2))
		{
			if (text == "Task")
			{
				if (!BudTask.CheckChilds(text2))
				{
					num = 1;
				}
			}
			else
			{
				if (text == "Template")
				{
					text2 = HttpUtility.UrlDecode(text2);
					if (BudTemplateItem.IsLeafNode(text2))
					{
						num = 1;
					}
				}
				else
				{
					if (text == "Resource")
					{
						if (BudTask.GetResourcesByTaskId(text2).Count == 0)
						{
							num = 1;
						}
					}
					else
					{
						if (text == "tempResource")
						{
							if (BudTemplateItem.GetResourcesByTempItemId(text2).Count == 0)
							{
								num = 1;
							}
						}
						else
						{
							if (text == "TemplateName")
							{
								if (BudTemplate.GetByName(text2).Count == 0)
								{
									num = 1;
								}
							}
							else
							{
								if (text == "ContractTask")
								{
									string cmdText = string.Format("SELECT COUNT(*) FROM Bud_ContractResource WHERE TaskId = '{0}'", text2);
									object obj = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]);
									num = DBHelper.GetInt(obj);
								}
								else
								{
									if (text == "isUsed")
									{
										bool flag = ConstructTask.GetIsUsedByTask(text2);
										if (flag)
										{
											num = 0;
										}
										else
										{
											PayoutTarget payoutTarget = new PayoutTarget();
											flag = payoutTarget.GetIsUsedByTaskId(text2);
											if (flag)
											{
												num = 0;
											}
											else
											{
												num = 1;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		context.Response.Write(num);
	}
}
