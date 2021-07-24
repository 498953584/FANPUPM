<%@ WebHandler Language="C#" Class="verificationHandler" %>

using cn.justwin.stockBLL.Fund.MonthPlan;
using System;
using System.Data;
using System.Web;
public class verificationHandler : IHttpHandler
{
	private MonthDetailLogic bll = new MonthDetailLogic();
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		string s = "ed";
		if (context.Request.Form["mpid"] != null && context.Request.Form["mpid"].ToString() != "" && context.Request.Form["mpid"].ToString().Length == 36)
		{
			if (context.Request.Form["ContractID"] != null && context.Request.Form["ContractID"].ToString() != "")
			{
				s = this.verificationPrjcode(context.Request.Form["mpid"].ToString(), context.Request.Form["ContractID"].ToString());
			}
			else
			{
				if (context.Request.Form["Plansubject"] != null && context.Request.Form["Plansubject"].ToString() != "")
				{
					s = this.verificationPlansubject(context.Request.Form["mpid"].ToString(), context.Request.Form["Plansubject"].ToString());
				}
				else
				{
					s = this.verificationPrjcode(context.Request.Form["mpid"].ToString(), "");
				}
			}
		}
		context.Response.ContentType = "text/plain";
		context.Response.Write(s);
		context.Response.End();
	}
	private string verificationPrjcode(string _MonthPlanID, string _ContractID)
	{
		string result = string.Empty;
		new DataTable();
		if (this.bll.verificationIsPrjGuid(_MonthPlanID, _ContractID))
		{
			result = "true";
		}
		else
		{
			result = "false";
		}
		return result;
	}
	private string verificationPlansubject(string _MonthPlanID, string _Plansubject)
	{
		string result = string.Empty;
		new DataTable();
		if (this.bll.verificationIsPlansubject(_MonthPlanID, _Plansubject))
		{
			result = "true";
		}
		else
		{
			result = "false";
		}
		return result;
	}
}
