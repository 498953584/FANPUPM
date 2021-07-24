<%@ WebHandler Language="C#" Class="ConModifyTask" %>

using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class ConModifyTask : IHttpHandler
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
		BudConModifyTaskService budConModifyTaskService = new BudConModifyTaskService();
		BudContractTaskService budContractTaskService = new BudContractTaskService();
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
						cn.justwin.Domain.Entities.BudContractTask budContractTask = budContractTaskService.GetTaskById(this.taskId);
						if (budContractTask == null)
						{
							BudConModifyTask budConModifyTask = (
								from mts in budConModifyTaskService
								where mts.ModifyTaskId == this.taskId
								select mts).FirstOrDefault<BudConModifyTask>();
							if (budConModifyTask != null)
							{
								budContractTask = new cn.justwin.Domain.Entities.BudContractTask();
								budContractTask.TaskId = budConModifyTask.TaskId;
								budContractTask.TaskCode = budConModifyTask.ModifyTaskCode;
								budContractTask.Unit = budConModifyTask.Unit;
								budContractTask.OrderNumber = budConModifyTask.OrderNumber;
							}
						}
						if (!string.IsNullOrEmpty(budContractTask.TaskId))
						{
							if (this.spType == "out")
							{
								string arg_1FF_0 = budContractTask.OrderNumber;
								string orderNumber = cn.justwin.Domain.BudContractTask.GetOrderNumber(this.prjId, this.taskId);
								if (orderNumber != "001")
								{
									budContractTask.OrderNumber = orderNumber;
								}
								else
								{
									budContractTask.OrderNumber = budConModifyTaskService.GetOrderNumber(this.prjId, this.taskId);
								}
								object obj = this.returnData;
								this.returnData = string.Concat(new object[]
								{
									obj,
									"[{'taskId':'",
									budContractTask.TaskId,
									"','taskCode':'",
									budContractTask.TaskCode,
									"','orderNumber':'",
									budContractTask.OrderNumber,
									"','typeName':'",
									this.GetTypeName(budContractTask.OrderNumber),
									"','UnitPrice':'",
									budContractTask.UnitPrice,
									"'}]"
								});
							}
							else
							{
								object obj2 = this.returnData;
								this.returnData = string.Concat(new object[]
								{
									obj2,
									"[{'taskId':'",
									budContractTask.TaskId,
									"','taskCode':'",
									budContractTask.TaskCode,
									"','unit':'",
									budContractTask.Unit,
									"','UnitPrice':'",
									budContractTask.UnitPrice,
									"'}]"
								});
							}
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
								BudConModifyService inner = new BudConModifyService();
								num = (
									from mts in budConModifyTaskService
									join ms in inner on mts.ModifyId equals ms.ModifyId
									where mts.ModifyTaskCode == modifyCode && mts.ModifyType == (int?)0 && mts.ModifyId != modifyId && ms.PrjId == this.prjId
									select mts).ToList<BudConModifyTask>().Count;
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
								bool flag = cn.justwin.Domain.BudContractTask.CheckCode(code, this.prjId);
								if (flag)
								{
									num2 = 1;
								}
							}
							this.returnData = num2.ToString();
						}
					}
				}
			}
		}
		context.Response.Write(this.returnData);
	}
	protected string GetTypeName(string orderNum)
	{
		XpmCodeServices xpmCodeServices = new XpmCodeServices();
		IList<XpmCode> bySignCode = xpmCodeServices.GetBySignCode("taskType");
		string result = string.Empty;
		foreach (XpmCode current in bySignCode)
		{
			if (current.CodeID == orderNum.Length / 3)
			{
				result = current.CodeName;
				break;
			}
		}
		return result;
	}
}
