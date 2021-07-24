<%@ WebHandler Language="C#" Class="RunSql" %>

using cn.justwin.DAL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
public class RunSql : IHttpHandler
{
	private string retSqlType = "scalar";
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
		string s = string.Empty;
		string id = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["id"]))
		{
			id = context.Request["id"];
		}
		string sql = this.GetSql(id, context);
		try
		{
			if (this.retSqlType == "scalar")
			{
				object obj = SqlHelper.ExecuteScalar(CommandType.Text, sql, new SqlParameter[0]);
				s = DBHelper.GetString(obj);
			}
			else
			{
				if (this.retSqlType == "datatable")
				{
					DataTable value = SqlHelper.ExecuteQuery(CommandType.Text, sql, new SqlParameter[0]);
					s = JsonConvert.SerializeObject(value);
				}
			}
		}
		catch
		{
			s = "-1";
		}
		context.Response.Write(s);
	}
	private string GetSql(string id, HttpContext context)
	{
		string result = string.Empty;
		if (id == "GetTaskResCount")
		{
			string arg = context.Request["taskId"];
			result = string.Format("SELECT COUNT(*) FROM (\r\n\t\t\t\tSELECT TaskResourceId FROM Bud_TaskResource WHERE TaskId = '{0}'\r\n\t\t\t\tUNION\r\n\t\t\t\tSELECT ModifyTaskResId FROM Bud_ModifyTaskRes WHERE ModifyTaskId IN (SELECT ModifyTaskId FROM Bud_ModifyTask WHERE TaskId='{0}')\r\n\t\t\t) AS T ", arg);
		}
		return result;
	}
}
