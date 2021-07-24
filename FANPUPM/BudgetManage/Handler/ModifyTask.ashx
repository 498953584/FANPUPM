<%@ WebHandler Language="C#" Class="ModifyTask" %>

using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
public class ModifyTask : IHttpHandler
{
	private string taskId = string.Empty;
	private string prjId = string.Empty;
	private string returnData = string.Empty;
	private string type = string.Empty;
	private string spType = string.Empty;
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		BudModifyTaskService budModifyTaskService = new BudModifyTaskService();
		context.Response.ContentType = "text/plain";
		if (!string.IsNullOrEmpty(context.Request["prjId"]))
		{
			this.prjId = context.Request["prjId"].ToString();
		}
		if (!string.IsNullOrEmpty(context.Request["spType"]))
		{
			this.spType = context.Request["spType"].ToString();
			if (!string.IsNullOrEmpty(context.Request["type"]))
			{
				this.type = context.Request["type"].ToString();
				if (this.type == "getTaskInfo")
				{
					if (!string.IsNullOrEmpty(context.Request["taskId"]))
					{
						this.taskId = context.Request["taskId"].ToString();
						cn.justwin.Domain.BudTask budTask = cn.justwin.Domain.BudTask.GetById(this.taskId);
						if (budTask == null)
						{
							BudModifyTask budModifyTask = (
								from mts in budModifyTaskService
								where mts.ModifyTaskId == this.taskId
								select mts).FirstOrDefault<BudModifyTask>();
							if (budModifyTask != null)
							{
								budTask = cn.justwin.Domain.BudTask.Create(budModifyTask.ModifyTaskId, null, null, this.prjId, budModifyTask.ModifyTaskCode, budModifyTask.ModifyTaskContent, budModifyTask.Unit, 0m, null, null, true, null, null, DateTime.Now, new decimal?(0m), new decimal?(0m));
								budTask.OrderNumber = budModifyTask.OrderNumber;
							}
						}
						if (!string.IsNullOrEmpty(budTask.Id))
						{
							if (this.spType == "out")
							{
								string arg_22C_0 = budTask.OrderNumber;
								string orderNumber = cn.justwin.Domain.BudTask.GetOrderNumber(this.prjId, this.taskId);
								if (orderNumber != "001")
								{
									budTask.OrderNumber = orderNumber;
								}
								else
								{
									budTask.OrderNumber = budModifyTaskService.GetOrderNumber(this.prjId, this.taskId);
								}
								string text = this.returnData;
								this.returnData = string.Concat(new string[]
								{
									text,
									"[{'taskId':'",
									budTask.Id,
									"','taskCode':'",
									budTask.Code,
									"','orderNumber':'",
									budTask.OrderNumber,
									"','typeName':'",
									budTask.TypeName,
									"'}]"
								});
							}
							else
							{
								string text2 = this.returnData;
								this.returnData = string.Concat(new string[]
								{
									text2,
									"[{'taskId':'",
									budTask.Id,
									"','taskCode':'",
									budTask.Code,
									"','unit':'",
									budTask.Unit,
									"'}]"
								});
							}
						}
					}
				}
				else
				{
					if (this.type == "isExist")
					{
						if (!string.IsNullOrEmpty(context.Request["taskId"]))
						{
							this.taskId = context.Request["taskId"].ToString();
							if (!string.IsNullOrEmpty(context.Request["modifyId"]))
							{
								string modifyId = context.Request["modifyId"].ToString();
								int count;
								if (this.spType == "out")
								{
									count = (
										from mts in budModifyTaskService
										where mts.TaskId == this.taskId && mts.ModifyType == (int?)1 && mts.ModifyId != modifyId
										select mts).ToList<BudModifyTask>().Count;
								}
								else
								{
									count = (
										from mts in budModifyTaskService
										where mts.TaskId == this.taskId && mts.ModifyType == (int?)0 && mts.ModifyId != modifyId
										select mts).ToList<BudModifyTask>().Count;
								}
								this.returnData = count.ToString();
							}
						}
					}
					else
					{
						if (this.type == "codeIsExistModify")
						{
							if (!string.IsNullOrEmpty(context.Request["modifyId"]))
							{
								string modifyId = context.Request["modifyId"].ToString();
								int num = 0;
								if (!string.IsNullOrEmpty(context.Request["modifyTaskCode"]))
								{
									string modifyCode = context.Request["modifyTaskCode"].ToString();
									BudModifyService inner = new BudModifyService();
									num = (
										from mts in budModifyTaskService
										join ms in inner on mts.ModifyId equals ms.ModifyId
										where mts.ModifyTaskCode == modifyCode && mts.ModifyType == (int?)0 && mts.ModifyId != modifyId && ms.PrjId == this.prjId
										select mts).ToList<BudModifyTask>().Count;
								}
								this.returnData = num.ToString();
							}
						}
						else
						{
							if (this.type == "codeIsExistBud")
							{
								int num2 = 0;
								if (!string.IsNullOrEmpty(context.Request["modifyTaskCode"]))
								{
									string code = context.Request["modifyTaskCode"].ToString();
									bool flag = cn.justwin.Domain.BudTask.CheckCode(code, this.prjId);
									if (flag)
									{
										num2 = 1;
									}
								}
								this.returnData = num2.ToString();
							}
							else
							{
								if (this.type == "getRes" && !string.IsNullOrEmpty(context.Request["modifyTaskId"]))
								{
									string modifyTaskId = context.Request["modifyTaskId"].ToString();
									StringBuilder stringBuilder = new StringBuilder();
									DataTable resByModifyTaskId = BudModifyTaskResBll.GetResByModifyTaskId(modifyTaskId);
									if (resByModifyTaskId.Rows.Count != 0)
									{
										stringBuilder.Append("\r\n                                <table border='1px' style='width:100%; border-collapse:collapse; margin-bottom:5px;'>\r\n                                <caption style=' height:20px; font-weight:bold; text-align:center; padding-top:8px;'>资源信息</caption>\r\n                                <tr class='header'><td align='center'>资源编号</td><td align='center'>资源名称</td>\r\n                                <td align='center'>单位</td><td align='center'>规格</td><td align='center'>品牌</td>\r\n                                <td align='center'>型号</td><td align='center'>技术参数</td><td align='center'>单价</td>\r\n                                <td align='center'>数量</td><td align='center'>小计</td></tr>");
										foreach (DataRow dataRow in resByModifyTaskId.Rows)
										{
											stringBuilder.Append("<tr class='rowb'>");
											for (int i = 0; i < resByModifyTaskId.Columns.Count; i++)
											{
												if (i < resByModifyTaskId.Columns.Count - 1)
												{
													stringBuilder.Append("<td align='center'>");
													stringBuilder.Append(dataRow[i].ToString());
													stringBuilder.Append("</td>");
												}
												else
												{
													stringBuilder.Append("<td align='center'>");
													stringBuilder.Append(Convert.ToDecimal(dataRow[i].ToString()).ToString("0.000"));
													stringBuilder.Append("</td>");
												}
											}
										}
										stringBuilder.Append("</tr>");
										stringBuilder.Append("</table>");
									}
									else
									{
										stringBuilder.Append("<h1>该任务节点下没有配置资源</h1>");
									}
									this.returnData = stringBuilder.ToString();
								}
							}
						}
					}
				}
			}
		}
		context.Response.Write(this.returnData);
	}
}
