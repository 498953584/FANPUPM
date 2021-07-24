<%@ WebHandler Language="C#" Class="GetDesktopData" %>

using cn.justwin.stockBLL.TableTopBLL;
using com.jwsoft.pm.entpm.action;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
public class GetDesktopData : IHttpHandler, IRequiresSessionState
{
	private string dbsjId = "2801";
	private string waringId = "2842";
	private string outlinkId = "381705";
	private string wfId = "283818";
	private string bulletionId = "280305";
	private string newId = "2827";
	private string regimeId = "281103";
	private string[] apps = new string[]
	{
		"381703",
		"381707",
		"381708",
		"381709",
		"381710",
		"381711"
	};
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
		DesktopBLL desktopBLL = new DesktopBLL();
		string text = string.Empty;
		string userCode = context.Session["yhdm"].ToString();
		if (!string.IsNullOrEmpty(context.Request["id"]))
		{
			text = context.Request["id"];
		}
		int num = 6;
		if (!string.IsNullOrEmpty(context.Request["rowNum"]))
		{
			num = Convert.ToInt32(context.Request["rowNum"]);
		}
		DataTable dataTable = new DataTable();
		if (text == this.dbsjId)
		{
			dataTable = desktopBLL.GetDBInfo(userCode, num);
			dataTable.Columns["I_DBSJ_ID"].ColumnName = "Id";
			dataTable.Columns["V_Content"].ColumnName = "Content";
			dataTable.Columns["DTM_DBSJ"].ColumnName = "Date";
			dataTable.Columns["V_DBLJ"].ColumnName = "Url";
		}
		if (text == this.waringId)
		{
			dataTable = desktopBLL.GetWarningInfo(userCode, num);
			dataTable.Columns["WarningId"].ColumnName = "Id";
			dataTable.Columns["WarningTitle"].ColumnName = "Content";
			dataTable.Columns["InputDate"].ColumnName = "Date";
			dataTable.Columns["URI"].ColumnName = "Url";
		}
		if (text == this.outlinkId)
		{
			dataTable = desktopBLL.GetWebLinkInfo(userCode, num);
			dataTable.Columns["linkId"].ColumnName = "Id";
			dataTable.Columns["webName"].ColumnName = "Content";
			dataTable.Columns["addTime"].ColumnName = "Date";
			dataTable.Columns["WebAddr"].ColumnName = "Url";
		}
		if (text == this.wfId)
		{
			dataTable = desktopBLL.GetWFInfo(userCode, num);
			dataTable.Columns["InstanceCode"].ColumnName = "Id";
			dataTable.Columns["BusinessClassName"].ColumnName = "Content";
			dataTable.Columns["rq"].ColumnName = "Date";
			dataTable.Columns.Add("Url");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string value = string.Concat(new object[]
				{
					"../EPC/Workflow/AuditFrame.aspx?ic=",
					dataTable.Rows[i]["Id"],
					"&id=",
					dataTable.Rows[i]["NoteID"],
					"&nid=",
					dataTable.Rows[i]["NodeID"],
					"&pass=",
					dataTable.Rows[i]["IsAllPass"],
					"&bc=",
					dataTable.Rows[i]["BusinessCode"],
					"&bcl=",
					dataTable.Rows[i]["BusinessClass"]
				});
				dataTable.Rows[i]["Url"] = value;
			}
		}
		if (text == this.bulletionId)
		{
			new BulletionAction();
			dataTable = BulletionAction.ReturnTable(userCode, " AND dtm_expriesdate>getdate()-1 AND auditstate=1", num);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				dataTable.Columns["I_BULLETINID"].ColumnName = "Id";
				dataTable.Columns["V_TITLE"].ColumnName = "Content";
				dataTable.Columns["rq"].ColumnName = "Date";
				dataTable.Columns["URL"].ColumnName = "Url";
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					dataTable.Rows[j]["Url"] = "../oa/bulletin/BulletinLock.aspx?ic=" + dataTable.Rows[j]["Id"];
				}
			}
		}
		if (text == this.newId)
		{
			dataTable = desktopBLL.GetNews(num);
		}
		if (text == this.regimeId)
		{
			dataTable = desktopBLL.GetRegime(num);
		}
		if (this.apps.Contains(text))
		{
			int num2 = 0;
			for (int k = 0; k < this.apps.Length; k++)
			{
				if (text == this.apps[k])
				{
					num2 = k + 1;
				}
			}
			dataTable = desktopBLL.GetMenuLinkInfo(userCode, num, num2.ToString());
			dataTable.Columns["Id"].ColumnName = "Id";
			dataTable.Columns["MNewName"].ColumnName = "Content";
			dataTable.Columns["addTime"].ColumnName = "Date";
			dataTable.Columns["v_cdlj"].ColumnName = "Url";
		}
		string s = JsonConvert.SerializeObject(dataTable);
		context.Response.Write(s);
	}
}