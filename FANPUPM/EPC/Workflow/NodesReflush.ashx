<%@ WebHandler Language="C#" Class="NodesReflush" %>

using cn.justwin.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
public class NodesReflush : IHttpHandler
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
		string arg_15_0 = string.Empty;
		string s = null;
		if (context.Request["TemplateId"] != null && !string.IsNullOrEmpty(context.Request["TemplateId"]))
		{
			string arg_51_0 = context.Request["TemplateId"];
			s = this.getNodes(int.Parse(context.Request["TemplateId"]));
		}
		context.Response.Write(s);
	}
	public string getNodes(int TemplateId)
	{
		DataTable dataTable = SqlHelper.ExecuteQuery(CommandType.Text, "select * from WF_FlowChart where templateid=" + TemplateId + " order by RowNum asc", new SqlParameter[0]);
		string text = "";
		for (int i = 0; i < dataTable.Columns.Count; i++)
		{
			try
			{
				string[] array = dataTable.Rows[0][i].ToString().Split(new char[]
				{
					';'
				});
				text = text + array[1] + ",";
			}
			catch
			{
			}
		}
		if (text != "")
		{
			return text.Substring(0, text.Length - 1);
		}
		return null;
	}
}
