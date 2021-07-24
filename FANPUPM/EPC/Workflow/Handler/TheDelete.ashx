<%@ WebHandler Language="C#" Class="TheDelete" %>

using cn.justwin.BLL;
using com.jwsoft.pm.data;
using PmBusinessLogic;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
public class TheDelete : IHttpHandler, IRequiresSessionState
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
		string text3 = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["guid"]))
		{
			text = context.Request["guid"];
		}
		if (!string.IsNullOrEmpty(context.Request["busiCode"]))
		{
			text2 = context.Request["busiCode"];
		}
		if (!string.IsNullOrEmpty(context.Request["busiClass"]))
		{
			text3 = context.Request["busiClass"];
		}
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
		{
			try
			{
				this.SuperDelete(text, text2, text3);
				context.Response.Write("1");
			}
			catch
			{
				context.Response.Write("0");
			}
		}
	}
	private void SuperDelete(string guid, string busiCode, string busiClass)
	{
		Guid guid2 = new Guid(guid);
		string sqlString = "select LinkTable,KeyWord,StateField,BusinessName from WF_BusinessCode where BusinessCode=" + busiCode;
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		string text = dataTable.Rows[0]["LinkTable"].ToString();
		string text2 = dataTable.Rows[0]["KeyWord"].ToString();
		string text3 = dataTable.Rows[0]["StateField"].ToString();
		string text4 = " Begin ";
		object obj = text4;
		text4 = string.Concat(new object[]
		{
			obj,
			" delete from WF_Instance where ID IN  (SELECT ID FROM WF_Instance_Main WHERE BusinessCode=",
			busiCode,
			"\tAND BusinessClass=",
			busiClass,
			" AND InstanceCode='",
			guid2,
			"')"
		});
		object obj2 = text4;
		text4 = string.Concat(new object[]
		{
			obj2,
			" DELETE  FROM WF_Instance_Main WHERE BusinessCode=",
			busiCode,
			" AND BusinessClass=",
			busiClass,
			" AND InstanceCode='",
			guid2,
			"'"
		});
		string sqlString2 = "select FatherId, TableName, line, linkLine,linkTable from WF_supperDelete where BusinessCode=" + busiCode + " and  BussinessClass=" + busiClass;
		DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString2);
		if (dataTable2.Rows.Count > 0)
		{
			if (dataTable2.Rows.Count == 1)
			{
				string text5 = dataTable2.Rows[0]["TableName"].ToString();
				string text6 = dataTable2.Rows[0]["line"].ToString();
				string text7 = dataTable2.Rows[0]["linkLine"].ToString();
				string text8 = dataTable2.Rows[0]["linkTable"].ToString();
				object obj3 = text4;
				text4 = string.Concat(new object[]
				{
					obj3,
					" delete from ",
					text5,
					" where ",
					text6,
					" in (select ",
					text7,
					" from ",
					text8,
					" where ",
					text2,
					"= '",
					guid2,
					"')"
				});
			}
			else
			{
				int arg_26F_0 = dataTable2.Rows.Count;
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					string text9 = dataTable2.Rows[i]["TableName"].ToString();
					string text10 = dataTable2.Rows[i]["line"].ToString();
					string text11 = dataTable2.Rows[i]["linkLine"].ToString();
					string text12 = dataTable2.Rows[0]["linkTable"].ToString();
					string a = dataTable2.Rows[0]["Fatherid"].ToString();
					if (a == "1")
					{
						object obj4 = text4;
						text4 = string.Concat(new object[]
						{
							obj4,
							" delete from ",
							text9,
							" where ",
							text10,
							" in (select ",
							text11,
							" from ",
							text12,
							" where ",
							text2,
							"= '",
							guid2,
							"')"
						});
					}
					text4 += " ";
				}
			}
		}
		this.CustomDelete(guid, text, text3);
		text4 += string.Format(" DELETE FROM {0} WHERE {1} = '{2}' AND {3} !='-1' ", new object[]
		{
			text,
			text2,
			guid2,
			text3
		});
		text4 += " END";
		publicDbOpClass.ExecuteSQL(text4);
	}
	private void CustomDelete(string key, string tableName, string columnName)
	{
		string path = AppDomain.CurrentDomain.BaseDirectory + "/SelfEventInfo.xml";
		string typeName = SelfEventAction.GetTypeName(path, tableName, columnName);
		if (!string.IsNullOrEmpty(typeName))
		{
			ISelfEvent selfEvent = Assembly.Load("PmBusinessLogic").CreateInstance(typeName) as ISelfEvent;
			if (selfEvent != null)
			{
				selfEvent.SuperDelete(key);
			}
		}
	}
}
